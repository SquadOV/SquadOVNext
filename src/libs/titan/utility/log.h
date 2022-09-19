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
#pragma once

// Don't throw anything out at compile time since we want to
// given release users the potential to turn on debug logs to help
// trace any errors when they crop up.
#define TITAN_ACTIVE_LEVEL SPDLOG_LEVEL_TRACE

#include <spdlog/spdlog.h>
#include <spdlog/async.h>

#include <fmt/core.h>
#include <fmt/std.h>

#include <filesystem>
#include <vector>
#include <thread>

#include "titan/dll.h"
#include "titan/utility/time.h"

namespace titan::utility {

// A wrapper class around the underlying log library to ensure that we configure it
// according to our needs. We want to remain flexible in terms of where we end up
// logging to and we want certain parts of the code to be able to add metadata
// to attach to their messages (e.g. as a sort of a "scope" thing).
using LoggerInstPtr = std::shared_ptr<spdlog::logger>;

class TITANEXPORT Logger {
public:
    static Logger* get();
    static void shutdown();

    Logger();
    ~Logger();

    Logger(const Logger& o) = delete;
    Logger& operator=(const Logger& o) = delete;

    Logger(Logger&& o) = delete;
    Logger& operator=(Logger&& o) = delete;

    //
    // Functions to set various verbosity levels of logging.
    // Have these separate functions so we don't expose spdlog in our interface.
    //
    void setStandardLevel();
    void setDebugLevel();
    void setTraceLevel();

    // Create a "scoped" logger with the sinks we currently have available.
    // A "scope" will prepend itself to the log message as [SCOPE].
    LoggerInstPtr createScopedLogger(const std::string& scope, std::optional<spdlog::level::level_enum> level = std::nullopt) const;

    // Add a sink that will output logs to the specified file.
    void addFileSystemSink(const std::filesystem::path& filename);

private:
    // Protect against multi-threaded access to the underlying sinks.
    mutable std::mutex _sinkMutex;

    // Sometimes we need to recreate the default logger (e.g. when we add a sink).
    // This function should be called then and on initialization.
    void refreshDefaultLogger();

    // We can have multiple sinks - I don't foresee a scenario
    // where we need to remove sinks so a simple vector should be fine.
    std::vector<spdlog::sink_ptr> _sinks;
};

template<typename... Args>
void logWrapper(spdlog::logger& logger, spdlog::source_loc loc, spdlog::level::level_enum lvl, spdlog::string_view_t fmt, Args&&... args) {
    // This will be a duplicate check with spdlog's own checks but it's better to do this than to waste time trying
    // to format stuff and pulling in the correct time.
    if (!logger.should_log(lvl)) {
        return;
    }

    spdlog::memory_buf_t buf;
    fmt::detail::vformat_to(buf, fmt, fmt::make_format_args(std::forward<Args>(args)...));

    spdlog::log_clock::time_point tm = convertClockTime<TimePoint, spdlog::log_clock::time_point>(now());
    logger.log(tm, loc, lvl, spdlog::string_view_t{buf.data(), buf.size()});
}

}

// Define our own macros here to somewhat hide the usage of spdlog underneath the hood.
// Furthermore, we don't want to rely on spdlog's view on what the current time is as it
// may be inaccurate - use our internal time tracking instead to output to logs instead.
#define TITAN_LOGGER_CALL(logger, level, ...) titan::utility::logWrapper(*logger, spdlog::source_loc{__FILE__, __LINE__, SPDLOG_FUNCTION}, level, __VA_ARGS__)

#if TITAN_ACTIVE_LEVEL <= SPDLOG_LEVEL_TRACE
#    define TITAN_LOGGER_TRACE(logger, ...) TITAN_LOGGER_CALL(logger, spdlog::level::trace, __VA_ARGS__)
#    define TITAN_TRACE(...) TITAN_LOGGER_TRACE(spdlog::default_logger_raw(), __VA_ARGS__)
#else
#    define TITAN_LOGGER_TRACE(logger, ...) (void)0
#    define TITAN_TRACE(...) (void)0
#endif

#if TITAN_ACTIVE_LEVEL <= SPDLOG_LEVEL_DEBUG
#    define TITAN_LOGGER_DEBUG(logger, ...) TITAN_LOGGER_CALL(logger, spdlog::level::debug, __VA_ARGS__)
#    define TITAN_DEBUG(...) TITAN_LOGGER_DEBUG(spdlog::default_logger_raw(), __VA_ARGS__)
#else
#    define TITAN_LOGGER_DEBUG(logger, ...) (void)0
#    define TITAN_DEBUG(...) (void)0
#endif

#if TITAN_ACTIVE_LEVEL <= SPDLOG_LEVEL_INFO
#    define TITAN_LOGGER_INFO(logger, ...) TITAN_LOGGER_CALL(logger, spdlog::level::info, __VA_ARGS__)
#    define TITAN_INFO(...) TITAN_LOGGER_INFO(spdlog::default_logger_raw(), __VA_ARGS__)
#else
#    define TITAN_LOGGER_INFO(logger, ...) (void)0
#    define TITAN_INFO(...) (void)0
#endif

#if TITAN_ACTIVE_LEVEL <= SPDLOG_LEVEL_WARN
#    define TITAN_LOGGER_WARN(logger, ...) TITAN_LOGGER_CALL(logger, spdlog::level::warn, __VA_ARGS__)
#    define TITAN_WARN(...) TITAN_LOGGER_WARN(spdlog::default_logger_raw(), __VA_ARGS__)
#else
#    define TITAN_LOGGER_WARN(logger, ...) (void)0
#    define TITAN_WARN(...) (void)0
#endif

#if TITAN_ACTIVE_LEVEL <= SPDLOG_LEVEL_ERROR
#    define TITAN_LOGGER_ERROR(logger, ...) TITAN_LOGGER_CALL(logger, spdlog::level::err, __VA_ARGS__)
#    define TITAN_ERROR(...) TITAN_LOGGER_ERROR(spdlog::default_logger_raw(), __VA_ARGS__)
#else
#    define TITAN_LOGGER_ERROR(logger, ...) (void)0
#    define TITAN_ERROR(...) (void)0
#endif

#if TITAN_ACTIVE_LEVEL <= SPDLOG_LEVEL_CRITICAL
#    define TITAN_LOGGER_CRITICAL(logger, ...) TITAN_LOGGER_CALL(logger, spdlog::level::critical, __VA_ARGS__)
#    define TITAN_CRITICAL(...) TITAN_LOGGER_CRITICAL(spdlog::default_logger_raw(), __VA_ARGS__)
#else
#    define TITAN_LOGGER_CRITICAL(logger, ...) (void)0
#    define TITAN_CRITICAL(...) (void)0
#endif