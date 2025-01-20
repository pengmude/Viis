using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Viis.Lib
{
    public class ImageCv
    {
        Mat _cvImage;
        Scalar contoursColor = Scalar.Red;
        public static ImageCv LoadImage(string path)
        {
            var img =  Cv2.ImRead(path, ImreadModes.Color);
            var imageCv = new ImageCv();
            imageCv._cvImage = img;

            return imageCv;
        }

        // 判断图片为全黑或全白 
        public int isBlackOrWhite()
        {
            // 将图片转换为灰度图
            Mat grayImage = new Mat();
            Cv2.CvtColor(_cvImage, grayImage, ColorConversionCodes.BGR2GRAY);

            // 计算灰度图的最小和最大值
            double minVal, maxVal;
            Cv2.MinMaxLoc(grayImage, out minVal, out maxVal);

            // 定义阈值，根据实际需要调整
            double threshold = 10.0;

            // 判断是否全黑或全白
            if (maxVal < threshold)
            {
                return 1; // 全黑
            }
            else if (minVal > 255 - threshold)
            {
                return -1; // 全白
            }

            return 0; // 不是全黑或全白
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
        public (Point[][], Mat) procesImage()
        {
            Mat img = _cvImage.Clone();
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

        // 修改图片大小
        public Mat ResizeImage(int width = 720, int height = 540)
        {
            Mat img = new Mat();
            // 将图片大小改为 640x480
            Cv2.Resize(_cvImage, img, new Size(720, 540));

            return img;
        }

        // 使用模版匹配图片
        public Mat ContourMatching(ImageCv cv)
        {
            Mat result = new Mat();
            var frame = _cvImage.Clone();

            var coursImg = cv._cvImage.Clone();

            // 处理原始图像
            (Point[][], Mat) frameContours = procesImage();

            // 先创建一个与原始图像大小相同的空白图像
            var imgwhite = new Mat(frameContours.Item2.Size(), MatType.CV_8UC3, Scalar.White);
            // 填充轮廓
            Cv2.DrawContours(imgwhite, frameContours.Item1, -1, contoursColor, 1);

            Cv2.MatchTemplate(imgwhite, coursImg, result, TemplateMatchModes.CCoeff);

            Cv2.MinMaxLoc(result, out double minVal, out double maxVal, out Point minLoc, out Point maxLoc);

            double threshold = 1;// 29999999;
            if (maxVal > threshold)
            {
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
                if (maxVal < 49999999)
                {
                    matchColor = Scalar.Red;
                }

                
                Cv2.Rectangle(frame, new Point(x, y), new Point(x + w, y + h), matchColor, 2);
                Cv2.PutText(frame, (maxVal).ToString(), new Point(x, y - 5), HersheyFonts.HersheyComplex, 0.5, matchColor);
            }

            return frame;
        }

        // 保存图片
        public void SaveImage(Mat img, string path)
        {
            Cv2.ImWrite(path, img);
        }

        public void DrawContours(Point[][] contours)
        {
            Cv2.DrawContours(_cvImage, contours, -1, contoursColor, 1);
        }

        public void Save(string path)
        {
            Cv2.ImWrite(path, _cvImage);
        }
    }
}
