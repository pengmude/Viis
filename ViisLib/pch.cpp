// pch.cpp: 与预编译标头对应的源文件

#include "pch.h"
#include <iostream>
#include "M:\opencv\build\include\opencv2\opencv.hpp"
// 当使用预编译的头时，需要使用此源文件，编译才能成功。
int main() {
    // 读取图像

    cv::Mat image = cv::imread("path_to_image.jpg", cv::IMREAD_COLOR);

    // 检查图像是否正确加载
    if (image.empty()) {
        std::cerr << "Could not open or find the image" << std::endl;
        return -1;
    }

    // 显示图像
    cv::namedWindow("Display Image", cv::WINDOW_AUTOSIZE);
    cv::imshow("Display Image", image);

    // 等待用户按键后关闭窗口
    cv::waitKey(0);

    return 0;
}