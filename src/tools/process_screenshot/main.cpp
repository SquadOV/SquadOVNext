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
#include <boost/program_options.hpp>
#include <filesystem>

#include <av/image/image_capture.h>
#include <av/image/image_io.h>
#include <titan/system/process.h>
#include <titan/utility/strings.h>
#include <titan/utility/log.h>

namespace po = boost::program_options;
namespace fs = std::filesystem;
int main(int argc, char** argv) {
    po::options_description desc("Options");
    desc.add_options()
        ("process", po::value<std::string>()->required(), "Process to capture.")
        ("output", po::value<std::string>()->required(), "Image output file.");

    po::variables_map vm;
    po::store(po::command_line_parser(argc, argv).options(desc).run(), vm);
    po::notify(vm);

    const auto processName = vm["process"].as<std::string>();
    const fs::path outputPath(vm["output"].as<std::string>());
    std::wstring wProcessName = titan::utility::utf8ToWcs(processName);
    TITAN_INFO("Screenshot {} to {}", processName, outputPath);

    std::vector<titan::system::Process> processes = titan::system::loadRunningProcesses();
    for (auto& p: processes) {
        if (p.path().filename().native() != wProcessName) {
            continue;
        }

        TITAN_INFO("Found Process: {}", p.path());
        p.initializeActiveWindow();

        if (!p.hasActiveWindow()) {
            continue;
        }

        av::ImageCapturePtr capturer = av::createImageCapture(p);
        if (!capturer) {
            continue;
        }

        TITAN_INFO("Made image capturer...");

        for (auto i = 0; i < 10; ++i) {
            std::optional<av::NativeImage> image = capturer->getCurrent();
            if (image) {
                TITAN_INFO("Writing image to output...");
                av::writeImageToFile(*image, outputPath);
                break;
            }
        }
        break;
    }
    return 0;
}