using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using OpenCvSharp;
using Size = OpenCvSharp.Size;
using Point = OpenCvSharp.Point;
using System.IO;
using System.Drawing;
using System.Collections.Concurrent;

namespace ViisApp.Lib
{
    public class ImageCv
    {
        Mat _cvImage;
        public static Scalar contoursColor = Scalar.Red;
        SettingStore _settingStore;
        public static ImageCv LoadImage(string path)
        {
            var img = Cv2.ImRead(path, ImreadModes.Color);
            var imageCv = new ImageCv();
            imageCv._cvImage = img;
            imageCv._settingStore = new SettingStore();
            return imageCv;
        }

        public static ImageCv LoadMat(Mat mat)
        {
            var imageCv = new ImageCv();
            imageCv._cvImage = mat;
            imageCv._settingStore = new SettingStore();
            return imageCv;
        }

        // 判断图片为全黑或全白 
        public int isBlackOrWhite()
        {
            // 将图片转换为灰度图
            Mat grayImage = new Mat();
            Cv2.CvtColor(_cvImage, grayImage, ColorConversionCodes.BGR2GRAY);

            //去除噪点
            Cv2.MedianBlur(grayImage, dst: grayImage, 3);



            // 寻找轮廓
            (Point[][], Mat) ret = procesImage();
            var pureRet = IsPureColorExt(grayImage);
            //var pureRet = IsPureColorByAt(grayImage);

            if (!pureRet.Item2)
            {
                return 0; // 不是纯色图片
            }

            return IsPureColorProcess(ret.Item2);
        }

        SettingStore settingStore = new SettingStore();
        public (Mat, bool) IsPureColor(Mat grayImage)
        {
            // 图像尺寸
            int width = grayImage.Width;
            int height = grayImage.Height;

            // 滑动窗口的尺寸
            int windowSize = 50; // 可以根据需要调整窗口大小

            // 遍历图像，计算每个窗口的局部方差
            float averageVariance = 0.0f;
            int step = 1; // 滑动步长


            for (int y = 0; y < height - windowSize; y += step)
            {
                for (int x = 0; x < width - windowSize; x += step)
                {
                    Mat window = grayImage.SubMat(new Rect(x, y, windowSize, windowSize));
                    double variance = Cv2.Mean(window).Val0;
                    averageVariance += (float)variance;
                }
            }


            // 计算平均方差
            averageVariance /= ((width - windowSize) / step) * ((height - windowSize) / step);
            
            // 设置方差阈值
            float varianceThreshold = settingStore.GetSetting().VarianceThreshold; // 这个值可以根据实际情况调整

            LogHelper.GetLogger("cv").Info($"averageVariance: {averageVariance}, varianceThreshold: {varianceThreshold}");

            // 输出结果
            Cv2.PutText(grayImage, $"{averageVariance}", new Point(10, 10), HersheyFonts.HersheyComplex, 0.5, Scalar.Blue);


            if (averageVariance > varianceThreshold)
            {
                return (grayImage, true);
            }
            else
            {
                return (grayImage, false);
            }

        }


        public (Mat, bool) IsPureColorExt(Mat grayImage)
        {
            // 图像尺寸
            int width = grayImage.Width;
            int height = grayImage.Height;

            // 滑动窗口的尺寸
            int windowSize = 20; // 可以根据需要调整窗口大小

            // 滑动步长
            int step = 2;

            // 用于存储每个窗口的局部方差
            ConcurrentBag<float> variances = new ConcurrentBag<float>();

            // 使用Parallel.ForEach进行多线程处理
            Parallel.For(0, (height - windowSize) / step, y =>
            {
                for (int x = 0; x < width - windowSize; x += step)
                {
                    Mat window = grayImage.SubMat(new Rect(x, y * step, windowSize, windowSize));
                    double variance = Cv2.Mean(window).Val0;
                    variances.Add((float)variance);
                }
            });

            // 计算平均方差
            float averageVariance = variances.Sum() / variances.Count;
            File.AppendAllLines("log1.txt", new List<string> { $"averageVariance: {averageVariance}" });

            // 设置方差阈值
            float varianceThreshold = settingStore.GetSetting().VarianceThreshold; // 这个值可以根据实际情况调整

            // 输出结果
            Cv2.PutText(grayImage, $"{averageVariance}", new Point(10, 10), HersheyFonts.HersheyComplex, 0.5, Scalar.Blue);

            if (averageVariance > varianceThreshold)
            {
                return (grayImage, true);
            }
            else
            {
                return (grayImage, false);
            }
        }

        public (Mat, bool) IsPureColorByAt(Mat grayImage)
        {
            // 图像尺寸
            int width = grayImage.Width;
            int height = grayImage.Height;

            // 滑动窗口的尺寸
            int windowSize = 20; // 可以根据需要调整窗口大小

            // 创建积分图像
            Mat integralImage = new Mat();
            Cv2.Integral(grayImage, integralImage);

            // 计算平均方差
            float averageVariance = 0.0f;
            int step = 1; // 滑动步长

            for (int y = 0; y < height - windowSize; y += step)
            {
                for (int x = 0; x < width - windowSize; x += step)
                {
                    // 计算窗口内的像素和
                    int x1 = x;
                    int y1 = y;
                    int x2 = x + windowSize;
                    int y2 = y + windowSize;
                    double sum = integralImage.At<double>(y1, x1) + integralImage.At<double>(y2, x2) - integralImage.At<double>(y1, x2) - integralImage.At<double>(y2, x1);
                    double mean = sum / (windowSize * windowSize);

                    // 计算窗口内的平方和
                    double sumOfSquares = 0.0;
                    for (int wy = 0; wy < windowSize; wy++)
                    {
                        for (int wx = 0; wx < windowSize; wx++)
                        {
                            double pixelValue = grayImage.At<byte>(y + wy, x + wx);
                            sumOfSquares += (pixelValue - mean) * (pixelValue - mean);
                        }
                    }

                    double variance = sumOfSquares / (windowSize * windowSize);
                    averageVariance += (float)variance;
                }
            }

            // 计算平均方差
            averageVariance /= ((width - windowSize) / step) * ((height - windowSize) / step);
            File.AppendAllLines("log1.txt", new List<string> { $"averageVariance: {averageVariance}" });

            // 设置方差阈值
            float varianceThreshold = settingStore.GetSetting().VarianceThreshold; // 这个值可以根据实际情况调整

            // 输出结果
            Cv2.PutText(grayImage, $"{averageVariance}", new Point(10, 10), HersheyFonts.HersheyComplex, 0.5, Scalar.Blue);

            if (averageVariance > varianceThreshold)
            {
                return (grayImage, true);
            }
            else
            {
                return (grayImage, false);
            }
        }



        int IsPureColorProcess(Mat img)
        {
            // 对图像求平均值
            Scalar mean = Cv2.Mean(_cvImage);

            // 计算图像的亮度
            double brightness = (mean.Val0 + mean.Val1 + mean.Val2) / 3.0;


            File.AppendAllLines("log1.txt", new List<string> { $"Brightness: {brightness}" });
            // 根据亮度和对比度判断是否为全黑和全白
            if (brightness < 100)
            {
                // 全黑图片
                if (mean.Val0 < 100 && mean.Val1 < 100 && mean.Val2 < 100)
                {
                    return 1;
                }
            }
            else if (brightness > 200)
            {
                // 处理全白图片
                return -1;
            }

            //Cv2.ImShow("img", img);
            return 0;
        }

        // 设置图片识别区域，默认为中心位置 100x100大小
        public Mat SetImageArea(bool isDefault = true, int x = 0, int y = 0, int width = 0, int height = 0)
        {
            if (isDefault)
            {
                x = _cvImage.Width / 2 - 50;
                y = _cvImage.Height / 2 - 50;
                width = 100;
                height = 100;
            }
            return _cvImage.SubMat(new Rect(x, y, width, height));
        }

        // 寻找图片轮廓图
        public (Point[][], Mat) procesImage(Mat img = null)
        {
            if (img == null)
            {
                img = _cvImage.Clone();
            }
            // 转化为灰度图
            var tempImg = img.Clone();
            Cv2.CvtColor(img, tempImg, ColorConversionCodes.BGR2GRAY);

            //去除噪点
            Cv2.MedianBlur(tempImg, tempImg, 1);

            //二值化，对灰度图进行阈值处理，将非黑色部分设置为白色
            var thresholdImg = new Mat();
            Cv2.Threshold(tempImg, thresholdImg, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);

            // 取反，将黑色变为白色，白色变为黑色
            Cv2.BitwiseNot(thresholdImg, thresholdImg);

            // 寻找轮廓
            Point[][] contoursPoints;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(thresholdImg, out contoursPoints, out hierarchy, RetrievalModes.List, ContourApproximationModes.ApproxSimple);

            return (contoursPoints, thresholdImg);
        }

        // 截取图片中心位置的图片
        public (Mat, Point) GetCenterImage(int width = 200, int height = 200)
        {
            int x = _cvImage.Width / 2 - width / 2;
            int y = _cvImage.Height / 2 - height / 2;
            return (_cvImage.SubMat(new Rect(x, y, width, height)), new Point(x, y));
        }

        // 修改图片大小
        public Mat ResizeImage(int width = 720, int height = 540)
        {
            Mat img = new Mat();
            // 将图片大小改为 640x480
            Cv2.Resize(_cvImage, img, new Size(720, 540));

            return img;
        }

        // 使用模版匹配图片
        public (Mat, bool) ContourMatching(ImageCv cv, TemplateMatchModes matchMethod = TemplateMatchModes.CCorrNormed)
        {
            Mat result = new Mat();
            bool matchedResult = false;
            var frame = _cvImage.Clone();

            var coursImg = cv._cvImage.Clone();

            //// 对比度调整参数
            //double alpha = 1.0; // 对比度增强系数
            //double beta = 0;    // 亮度偏移量

            //// 应用对比度调整
            //Mat adjustedImage = new Mat();
            //Cv2.ConvertScaleAbs(frame, frame, alpha, beta);

            // 处理原始图像
            (Point[][], Mat) frameContours = procesImage();

            var imgwhite = new Mat(frameContours.Item2.Size(), MatType.CV_8UC3, Scalar.White);
            if (_settingStore.GetSetting().IsContoursMatch)
            {
                // 先创建一个与原始图像大小相同的空白图像


                // 填充轮廓
                Cv2.DrawContours(imgwhite, frameContours.Item1, -1, ImageCv.contoursColor, 1);



                //Cv2.ImShow("imgwhite", imgwhite);
                //Cv2.ImShow("coursImg", coursImg);


                //Cv2.ImShow("frame", imgwhite);
                //Cv2.ImShow("coursImg", coursImg);


            }
            else
            {
                Cv2.CopyTo(frameContours.Item2, imgwhite);
                Cv2.CvtColor(imgwhite, imgwhite, ColorConversionCodes.GRAY2BGR);
            }

            // 匹配模板
            var matchMode = matchMethod;
            Cv2.MatchTemplate(imgwhite, coursImg, result, matchMode);

            Cv2.MinMaxLoc(result, out double minVal, out double maxVal, out Point minLoc, out Point maxLoc);

            //计算模板轮廓的宽高
            //int w = contours[0].Max(p => p.X) - contours[0].Min(p => p.X);
            //int h = contours[0].Max(p => p.Y) - contours[0].Min(p => p.Y);

            int w = coursImg.Width;
            int h = coursImg.Height;

            //计算匹配结果的左上角坐标
            int x = maxLoc.X;
            int y = maxLoc.Y;

            //绘制矩形
            var matchColor = Scalar.LightGreen;
            var matchText = "OK";
            if (maxVal < _settingStore.GetSetting().ImageMatchThreshold)
            {
                matchColor = Scalar.Red;
                matchedResult = false;
                matchText = "NG";
            }
            else
            {
                matchedResult = true;
            }


            Cv2.Rectangle(frame, new Point(x, y), new Point(x + w, y + h), matchColor, 2);
            Cv2.PutText(frame, maxVal.ToString("0.00"), new Point(x, y - 5), HersheyFonts.HersheyComplex, 0.5, matchColor);
            Cv2.PutText(frame, matchText, new Point(x + w / 2 - 20, y + h + 30), HersheyFonts.HersheyTriplex, 1, matchColor);
            
            return (frame, matchedResult);
        }

        // 保存图片
        public void SaveImage(Mat img, string path)
        {
            Cv2.ImWrite(path, img);
        }

        public void DrawContours(Point[][] contours, Point topLeft, bool isTransform = false)
        {
            if (isTransform)
            {
                for (int i = 0; i < contours.Length; i++)
                {
                    for (int j = 0; j < contours[i].Length; j++)
                    {
                        contours[i][j] = new Point(contours[i][j].X + topLeft.X, contours[i][j].Y + topLeft.Y);
                    }
                }
            }
            Cv2.DrawContours(_cvImage, contours, -1, contoursColor, 1);
        }

        public void Save(string path)
        {
            Cv2.ImWrite(path, _cvImage);
        }

        public Mat GetImg()
        {
            return _cvImage;
        }
    }
}
