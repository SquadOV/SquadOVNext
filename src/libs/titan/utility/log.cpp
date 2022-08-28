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
#include "titan/utility/log.h"
#include "titan/utility/macros.h"

#include <memory>
#include <spdlog/sinks/stdout_sinks.h>

#ifdef NDEBUG
#define FILE_LOC_PATTERN 
#else
#define FILE_LOC_PATTERN [loc: (%@) %!:%#]
#endif 

#define LOG_PATTERN [%D %X][%l][%n][pid: %P][tid: %t]FILE_LOC_PATTERN %v 

namespace titan::utility {
namespace {

// Initialize this when the DLL is loaded pretty much - this ensures our changes to
// the logging library's global state are taken into account before any user starts
// doing funky shit.
static auto gLogger = std::make_unique<Logger>();

}

Logger* Logger::get() {
    return gLogger.get();
}

Logger::Logger() {
    spdlog::init_thread_pool(8192, 1);

    // Be careful about setting this very large - it'll cause a delay in program exit otherwise.
    spdlog::flush_every(std::chrono::seconds(1));

    // Create the default console (stdout) sink that everyone should use.
    _sinks.push_back(std::make_shared<spdlog::sinks::stdout_sink_mt>());

#ifdef NDEBUG
    setStandardLevel();
#else
    setDebugLevel();
#endif

    refreshDefaultLogger();
}

void Logger::setStandardLevel() {
    spdlog::set_level(spdlog::level::info);
}

void Logger::setDebugLevel() {
    spdlog::set_level(spdlog::level::debug);
}

void Logger::setTraceLevel() {
    spdlog::set_level(spdlog::level::trace);
}

Logger::~Logger() {
#ifdef _WIN32
    // According to the docs (https://github.com/gabime/spdlog/wiki/1.-QuickStart)
    // this is necessary in visual studio - maybe not a windows thing?
    spdlog::drop_all();
#endif
    spdlog::shutdown();
}

void Logger::refreshDefaultLogger() {
    auto logger = createScopedLogger("global");
    spdlog::set_default_logger(logger);
}

LoggerInstPtr Logger::createScopedLogger(const std::string& scope) const {
    std::scoped_lock guard(_sinkMutex);
    auto logger = std::make_shared<spdlog::async_logger>(
        scope,
        _sinks.begin(),
        _sinks.end(),
        spdlog::thread_pool(),
        spdlog::async_overflow_policy::block
    );

    logger->flush_on(spdlog::level::err);
    logger->set_pattern(XSTR(LOG_PATTERN), spdlog::pattern_time_type::utc);
    logger->set_level(spdlog::get_level());
    spdlog::register_logger(logger);
    return logger;
}

}