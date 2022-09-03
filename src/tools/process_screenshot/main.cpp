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
#include <av/image/processing/image_capture_source.h>
#include <av/image/processing/directx/image_dx11_ingest.h>
#include <av/image/processing/directx/compositor_dx11_node.h>
#include <av/image/processing/compositor_layer_node.h>
#include <av/image/compositor/transform_op_node.h>
#include <av/image/compositor/directx/transform_op_dx11.h>
#include <titan/system/win32/directx/device.h>
#include <titan/system/process.h>
#include <titan/utility/strings.h>
#include <titan/utility/log.h>

namespace po = boost::program_options;
namespace fs = std::filesystem;
int main(int argc, char** argv) {
    try {
        bool useCpu = false;

        po::options_description desc("Options");
        desc.add_options()
            ("process", po::value<std::string>()->required(), "Process to capture.")
            ("output", po::value<std::string>()->required(), "Image output file.")
            ("width", po::value<size_t>()->required(), "Output X resolution.")
            ("height", po::value<size_t>()->required(), "Output Y resolution.")
            ("cpu", po::bool_switch(&useCpu), "Use CPU-based processing");

        po::variables_map vm;
        po::store(po::command_line_parser(argc, argv).options(desc).run(), vm);
        po::notify(vm);

        const auto processName = vm["process"].as<std::string>();
        const fs::path outputPath(vm["output"].as<std::string>());
        const auto width = vm["width"].as<size_t>();
        const auto height = vm["height"].as<size_t>();

        std::wstring wProcessName = titan::utility::utf8ToWcs(processName);
        TITAN_INFO("Screenshot {} to {} [{}x{}]", processName, outputPath, width, height);

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

            TITAN_INFO("Creating image capturer...");
            av::ImageCapturePtr capturer = av::createImageCapture(p);
            if (!capturer) {
                continue;
            }

            TITAN_INFO("Create context...");
            auto device = titan::system::win32::loadD3d11DeviceOnLocation(useCpu ? titan::system::win32::D3d11DeviceLocation::CPU : titan::system::win32::D3d11DeviceLocation::GPU);

            TITAN_INFO("Setup processing graph...");
            auto captureNode = std::make_shared<av::ImageCaptureSource>(capturer);
            auto ingestNode = std::make_shared<av::directx::ImageDx11IngestNode>(device);
            ingestNode->connectInputTo(ingestNode->kInput, captureNode, captureNode->kOutput);

            TITAN_INFO("Setup compositor...");
            auto tNode = std::make_shared<titan::utility::ConstantValueNode<Eigen::Vector2f>>(Eigen::Vector2f::Zero());
            auto rNode = std::make_shared<titan::utility::ConstantValueNode<float>>(0.f);
            auto sNode = std::make_shared<titan::utility::ConstantValueNode<Eigen::Vector2f>>(Eigen::Vector2f::Ones());
            auto originNode = std::make_shared<titan::utility::ConstantValueNode<av::TransformOp::Origin>>(av::TransformOp::Origin::TopLeft);
            auto uvNode = std::make_shared<titan::utility::ConstantValueNode<av::TransformOp::UvMode>>(av::TransformOp::UvMode::Fill);

            auto transformOpNode = std::make_shared<av::TransformOpNode<av::directx::TransformOpDx11>>(std::make_shared<av::directx::TransformOpDx11>(device));
            transformOpNode->connectInputTo(transformOpNode->kTranslation, tNode, tNode->kOutput);
            transformOpNode->connectInputTo(transformOpNode->kRotation, rNode, rNode->kOutput);
            transformOpNode->connectInputTo(transformOpNode->kScale, sNode, sNode->kOutput);
            transformOpNode->connectInputTo(transformOpNode->kOrigin, originNode, originNode->kOutput);
            transformOpNode->connectInputTo(transformOpNode->kUvMode, uvNode, uvNode->kOutput);

            auto compositorLayerNode = std::make_shared<av::CompositorLayerNode>(1);
            compositorLayerNode->connectInputTo(compositorLayerNode->kInputImage, ingestNode, ingestNode->kOutput);
            compositorLayerNode->connectInputTo(compositorLayerNode->kOpStart, transformOpNode, transformOpNode->kOutput);

            auto compositorNode = std::make_shared<av::directx::CompositorDx11Node>(device, 1, width, height);
            compositorNode->connectInputTo(compositorNode->kLayerStart, compositorLayerNode, compositorLayerNode->kOutput);

            for (auto i = 0; i < 10; ++i) {
                titan::utility::ProcessingCacheContainer cache;
                TITAN_INFO("Capture image attempt {}...", i);
                const auto& image = compositorNode->getOutputValue<av::NativeImagePtr>(compositorNode->kOutput, cache);
                if (image) {
                    TITAN_INFO("Writing image to output...");
                    av::writeImageToFile(*image, outputPath);
                    break;
                }
            }
            break;
        }
    } catch (const std::exception& ex) {
        TITAN_ERROR(ex.what());
    }

    titan::utility::Logger::shutdown();
    return 0;
}