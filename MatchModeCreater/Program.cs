using OpenCvSharp;
using ViisApp;
using ViisApp.Lib;

namespace MatchModeCreater
{
    internal class Program
    {
        const string WindowName = "图片匹配模板创建工具";
        static Mat img, imgCopy;
        static Point topLeft, bottomRight;
        static SettingStore settingStore = new SettingStore();
        static string modelFilePath = settingStore.GetSetting().ModelPath;
        static int fileIndex = 0;

        static void Main(string[] args)
        {
            try
            {
                //args = new string[] { @"E:\CAM4\3565cc49-cdb8-45d6-8b8a-b9fe22afe226.bmp", "1" };
                Cv2.NamedWindow(WindowName);
                if (args.Length == 2)
                {
                    var picPath = args[0];
                    fileIndex = int.Parse(args[1]);
                    var img = Cv2.ImRead(picPath, ImreadModes.Color);
                    imgCopy = img.Clone();
                    Cv2.SetMouseCallback(WindowName, DrawRectangle);

                    //Cv2.DestroyAllWindows();
                    Cv2.ImShow(WindowName, img);
                }


                Cv2.WaitKey();
            }catch(Exception ex)
            {
                LogHelper.GetLogger("MatchModeCreater").Error(ex.Message);
                LogHelper.GetLogger("MatchModeCreater").Error(ex.StackTrace);
            }
        }
        private static void DrawRectangle(MouseEventTypes @event, int x, int y, MouseEventFlags flags, IntPtr userData)
        {

            switch (@event)
            {
                case MouseEventTypes.LButtonDown: // 鼠标左键按下
                    topLeft = new Point(x, y);
                    break;
                case MouseEventTypes.LButtonUp: // 鼠标左键释放
                    bottomRight = new Point(x, y);
                    // 清除之前的矩形
                    img = imgCopy.Clone();

                    // 显示选择的矩形
                    Cv2.Rectangle(img, topLeft, bottomRight, ImageCv.contoursColor, 2);
                    Cv2.ImShow(WindowName, img);
                    SelectImgContours();

                    break;
                case MouseEventTypes.MouseMove:
                    if ((flags & MouseEventFlags.LButton) != 0) // 鼠标左键按下时移动
                    {
                        // 清除之前的矩形
                        img = imgCopy.Clone();
                        Cv2.Rectangle(img, topLeft, new Point(x, y), ImageCv.contoursColor, 1);
                        Cv2.ImShow(WindowName, img);
                    }
                    break;
                default:
                    break;
            }
        }

        private static void SelectImgContours()
        {
            try
            {
                var newSize = new Size(Math.Abs(bottomRight.X - topLeft.X), Math.Abs(bottomRight.Y - topLeft.Y));

                // 裁剪出矩形区域
                Mat selectImg;
                try
                {
                    selectImg = img[new Rect(topLeft, newSize)];
                }
                catch
                {
                    constUtitly.MsgError("请正确选择矩形区域。");
                    img = imgCopy.Clone();
                    Cv2.ImShow(WindowName, img);
                    return;
                };

                var imageCv = new ImageCv();
                
                var ret = imageCv.procesImage(selectImg);
                var contours = ret.Item1;
                // 复制轮廓
                var tempContours = new OpenCvSharp.Point[contours.Length][];
                for (int i = 0; i < contours.Length; i++)
                {
                    tempContours[i] = new OpenCvSharp.Point[contours[i].Length];
                    for (int j = 0; j < contours[i].Length; j++)
                    {
                        tempContours[i][j] = new OpenCvSharp.Point(contours[i][j].X, contours[i][j].Y);
                    }
                }

                // 将轮廓图转换为原始图像的位置
                for (int i = 0; i < contours.Length; i++)
                {
                    for (int j = 0; j < contours[i].Length; j++)
                    {
                        contours[i][j] = new Point(contours[i][j].X + topLeft.X, contours[i][j].Y + topLeft.Y);
                    }
                }

                // 显示轮廓
                Cv2.DrawContours(img, contours, -1, ImageCv.contoursColor, 1);
                Cv2.ImShow(WindowName, img);

                if (settingStore.GetSetting().IsContoursMatch)
                {

                    //var sharp = new NumSharp.Shape(selectImg.Width, selectImg.Height, 3);
                    // 先创建一个与原始图像大小相同的空白图像
                    var imgwhite = new Mat(selectImg.Size(), MatType.CV_8UC3, Scalar.White);
                    // 填充轮廓
                    Cv2.DrawContours(imgwhite, tempContours, -1, ImageCv.contoursColor, 1);
                    // 显示处理后的图像
                    //Cv2.ImShow("sharp", imgwhite);

                    // 显示原始图像
                    //Cv2.ImShow("original", selectImg);
                    //Cv2.ImShow("cours", imgwhite);

                    // 保存模板
                    Cv2.ImWrite(Path.Combine(modelFilePath, fileIndex + ".jpg"), imgwhite);
                }
                else
                {
                    Cv2.ImWrite(Path.Combine(modelFilePath, fileIndex + ".jpg"), ret.Item2);
                }

                Cv2.ImWrite(Path.Combine(modelFilePath, fileIndex + ".temp.jpg"), img);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
