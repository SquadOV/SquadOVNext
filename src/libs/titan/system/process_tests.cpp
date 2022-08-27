//
//  Copyright (C) 2022 Michael Bao
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

#include <doctest/doctest.h>
#include <unordered_map>

#include "titan/system/types.h"
#include "titan/system/process_di.h"
#include "titan/system/process.h"

struct TestCase{
    std::string name;
    NativeString path;
    int64_t startTime;
};

class TestProcessDI: public titan::system::NativeProcessDI {
public:
    using TestMap = std::unordered_map<NativeProcessId, TestCase>;

    explicit TestProcessDI(const TestMap& data):
        _data(data)
    {
        for (const auto& kvp: _data) {
            _friendlyNames[kvp.second.path] = kvp.second.name;
        }
    }

    titan::system::NativeProcessHandleWrapper openProcessHandleLimited(NativeProcessId id) override {
        // Hacky hacky LMAO.
        return titan::system::NativeProcessHandleWrapper{idToHandle(id), shared_from_this()};
    }

    void closeProcessHandle(NativeProcessHandle handle) override {}

    std::vector<NativeProcessId> enumProcesses() override {
        std::vector<NativeProcessId> p(_data.size());
        size_t idx = 0;
        for (const auto& d: _data) {
            p[idx++] = d.first;
        }
        return p;
    }

    NativeString getProcessPath(NativeProcessHandle handle) override {
        auto it = _data.find(handleToId(handle));
        REQUIRE(it != _data.end());
        return it->second.path;
    }

    std::string getProcessFriendlyName(const NativeString& fullPath) override {
        auto it = _friendlyNames.find(fullPath);
        REQUIRE(it != _friendlyNames.end());
        return it->second;
    }

    int64_t getProcessStartTime(NativeProcessHandle handle) override {
        auto it = _data.find(handleToId(handle));
        REQUIRE(it != _data.end());
        return it->second.startTime;
    }

private:
    NativeProcessHandle idToHandle(NativeProcessId id) {
        return reinterpret_cast<NativeProcessHandle>(static_cast<int64_t>(id));
    }

    NativeProcessId handleToId(NativeProcessHandle handle) {
        return static_cast<NativeProcessId>(reinterpret_cast<int64_t>(handle));
    }

    TestMap _data;
    std::unordered_map<NativeString, std::string> _friendlyNames;
};

TEST_CASE("Process constructor -> accessor") {
    TestProcessDI::TestMap cases = {
        {256, {"Test1", L"/Path/123", 54421}},
        {15243123, {"Test2", L"Test5", 1}}
    };
    auto di = std::make_shared<TestProcessDI>(cases);

    {
        titan::system::Process process(256, di);
        CHECK(process.name() == "Test1");
        CHECK(process.path() == L"/Path/123");
        CHECK(process.startTime() == 54421);
    }

    {
        titan::system::Process process(15243123, di);
        CHECK(process.name() == "Test2");
        CHECK(process.path() == L"Test5");
        CHECK(process.startTime() == 1);
    }
}

TEST_CASE("Load Running Processes Order") {
    TestProcessDI::TestMap cases = {
        {256, {"Test1", L"/Path/123", 54421}},
        {15243123, {"Test2", L"Test5", 1}}
    };
    auto di = std::make_shared<TestProcessDI>(cases);
    auto processes = titan::system::loadRunningProcesses(di);
    CHECK(processes.size() == 2);
    CHECK(processes[0].startTime() == 54421);
    CHECK(processes[1].startTime() == 1);
}