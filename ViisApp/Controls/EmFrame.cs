using MvCamCtrl.NET;
using NLog;
using OpenCvSharp;
using OpenCvSharp.Flann;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vanara.PInvoke;
using ViisApp.Lib;
using static MvCamCtrl.NET.MyCamera;
using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;

namespace ViisApp.Controls
{
    public partial class EmFrame : UserControl
    {
        FileSystemWatcher fileWatcher;
        ConcurrentQueue<string> fileStack;
        SettingStore settingStore = new SettingStore();
        string modelFilePath, matchedFilePath;
        Mat img, imgCopy;
        bool isFirstMatchFailed = true;
        public emModel PicModel { get; set; }
        int fileIndex = 1;
        // 调用Windows API查找Cv2窗体，并设置窗体位置和大小
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "MoveWindow", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        // Windows API 常量
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOOWNERZORDER = 0x0200;
        public const int HWND_TOPMOST = -1;
        public const int TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOACTIVATE | SWP_NOSIZE;
        const uint WM_CLOSE = 0x0010;
        // 查找窗体并设置窗体大小和位置
        private void SetCv2Form(int X, int Y, int Width, int Height)
        {
            try
            {
                IntPtr hWnd = FindWindowByApi();

                MoveWindow(hWnd, X, Y, Width, Height, true);
                SetWindowPos(hWnd, 0, X, Y, 0, 0, 0x0001 | 0x0002);
                // 将窗体显示在最前面
                SetWindowPos(hWnd, new IntPtr(true ? HWND_TOPMOST : 0), 0, 0, 0, 0, TOPMOST_FLAGS);
                //SetForegroundWindow(hWnd);


            }
            catch (Exception ex)
            {
                constUtitly.MsgError("设置窗体失败:" + ex.Message);
            }
        }

        private static IntPtr FindWindowByApi()
        {
            var hWnd = FindWindow(null, windowName);
            if (hWnd == IntPtr.Zero)
            {
                return IntPtr.Zero;
            }

            return hWnd;
        }

        bool CloseWindow()
        {
            //return true;
            var hwnd = FindWindowByApi();
            if (hwnd != IntPtr.Zero)
            {
                User32.PostMessage(hwnd, WM_CLOSE);
                CloseWindow();
                return true;
            }
            else
            {
                return false;
            }
        }

        bool isContiune = false;
        //MvsCtl ctl;
        //MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MV_CC_DEVICE_INFO_LIST();
        public EmFrame(emModel model, string watchFileFolder = "")
        {
            InitializeComponent();

            modelFilePath = settingStore.GetSetting().ModelPath;
            matchedFilePath = settingStore.GetSetting().MatchedPath;
            PicModel = model;

            host = new GpioHost(int.Parse(settingStore.GetSetting().GPIOType));

            var labelInfo = statusStrip.Items.Find("tsStatus", true).FirstOrDefault() as ToolStripLabel;
            if (PicModel == emModel.Learn)
            {
                labelInfo.Text = "图文学习";
            }

            if (PicModel == emModel.MatchModel)
            {
                labelInfo.Text = "匹配模板";
                btnStart.Visible = false;
                btnClose.Visible = false;
                //cbDeviceList.Visible = false;
                btnSkip.Visible = false;
                panelThumbnail.Visible = false;
                splitter.Visible = false;
                btnResetIndex.Visible = false;
            }

            if (PicModel == emModel.Match)
            {
                labelInfo.Text = "匹配样本";
                //btnSkip.Visible = false;
                //cmbTrigger.Checked = (ParentForm as FormMain).mvs.m_bGrabbing;
            }

            if (!string.IsNullOrEmpty(watchFileFolder))
            {
                if (!Directory.Exists(watchFileFolder))
                {
                    Directory.CreateDirectory(watchFileFolder);
                }
                fileWatcher = new FileSystemWatcher(watchFileFolder);
                fileWatcher.Filter = "*.bmp";
                //fileWatcher.Created += FileWatcher_Created;
            }

            //try
            //{
            //    ctl = new MvsCtl();

            //    m_stDeviceList = ctl.EnumDevices();

            //    if (m_stDeviceList.nDeviceNum == 0)
            //    {
            //        constUtitly.MsgError("未找到设备。");
            //        return;
            //    }

            //    for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
            //    {
            //        MyCamera.MV_CC_DEVICE_INFO device =
            //            (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(
            //                m_stDeviceList.pDeviceInfo[i],
            //                typeof(MyCamera.MV_CC_DEVICE_INFO));

            //        if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            //        {
            //            MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));

            //            if (gigeInfo.chUserDefinedName != "")
            //            {
            //                cbDeviceList.Items.Add("GEV: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
            //            }
            //            else
            //            {
            //                cbDeviceList.Items.Add("GEV: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
            //            }
            //        }
            //        else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
            //        {
            //            MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
            //            if (usbInfo.chUserDefinedName != "")
            //            {
            //                cbDeviceList.Items.Add("U3V: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")");
            //            }
            //            else
            //            {
            //                cbDeviceList.Items.Add("U3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
            //            }
            //        }
            //    }

            //    if (m_stDeviceList.nDeviceNum > 0)
            //    {
            //        cbDeviceList.SelectedIndex = 0;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    constUtitly.MsgError(ex.Message);
            //}
        }

        void Start()
        {
            //if (cbDeviceList.SelectedIndex >= 0 && PicModel == emModel.Match)
            //{
            //    try
            //    {
            //        if (ctl != null)
            //        {
            //            ctl.Dispose();
            //        }

            //        ctl = new MvsCtl();
            //        m_stDeviceList = ctl.EnumDevices();
            //        MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[cbDeviceList.SelectedIndex], typeof(MyCamera.MV_CC_DEVICE_INFO));
            //        ctl.OpenDevice(device);
            //        ctl.StartGrabbing(cmbTrigger.Checked);
            //    }
            //    catch (Exception ex)
            //    {
            //        constUtitly.MsgError(ex.Message);
            //        return;
            //    }
            //}
            //else
            //{
            //    cbDeviceList.Focus();
            //}

            isContiune = true;
            btnStart.Text = "停止";
            btnStart.Type = AntdUI.TTypeMini.Warn;

            //cbDeviceList.Clear();

            fileStack = new ConcurrentQueue<string>();

            if (!Directory.Exists(modelFilePath))
            {
                Directory.CreateDirectory(modelFilePath);
            }

            DirectoryInfo dir = new DirectoryInfo(modelFilePath);
            FileInfo[] files = dir.GetFiles("*.jpg");

            if (fileWatcher != null)
            {
                fileWaterTimer.Enabled = true;
                fileWatcher.Created += FileWatcher_Created;
                fileWatcher.EnableRaisingEvents = true;
            }

            PutThumbnail();

        }

        private void PutThumbnail()
        {
            var files = new DirectoryInfo(modelFilePath);
            var fileInfos = files.GetFiles("*.bmp");
            var spliteWidth = 10;
            for (int i = 0; i < fileInfos.Count(); i++)
            {
                PictureBox pictureBox = new PictureBox();
                var side = panelThumbnail.Height - ((float)spliteWidth / 2);
                pictureBox.Size = new System.Drawing.Size((int)Math.Round(side), (int)Math.Round(side));
                pictureBox.Location = new System.Drawing.Point((spliteWidth + pictureBox.Size.Width) * i, spliteWidth / 2);
                pictureBox.Image = EmFrameLearn.ReadToImage(fileInfos[i].FullName);
                pictureBox.ImageLocation = fileInfos[i].FullName;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                //pictureBox.Click += PictureBox_Click;
                panelThumbnail.Controls.Add(pictureBox);

                Label label = new Label();
                label.Text = (i + 1).ToString();
                label.Font = new System.Drawing.Font("微软雅黑", 12);
                label.ForeColor = Color.LightBlue;
                label.BackColor = Color.Black;
                label.Location = new System.Drawing.Point(2, 2);
                label.AutoSize = true;
                pictureBox.Controls.Add(label);
                label.BringToFront();
            }
        }

        private void ResetIndex()
        {
            fileIndex = 1;
            lblIndex.Text = fileIndex.ToString();
        }

        private void Stop()
        {
            isContiune = false;
            btnStart.Text = "启动";
            btnStart.Type = AntdUI.TTypeMini.Default;
            if (fileWatcher != null)
            {
                fileWatcher.Created -= FileWatcher_Created;
            }

            CloseWindow();

            //if (m_stDeviceList.nDeviceNum > 0)
            //{
            //    ctl.StopGrabbing();
            //    ctl.CloseDevice();
            //    ctl.Dispose();
            //}
        }

        private void FileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            fileStack.Enqueue(e.FullPath);

        }

        string currentFileName;
        private void fileWaterTimer_Tick(object sender, EventArgs e)
        {
            fileWaterTimer.Enabled = false;
            if (!isContiune)
            {

                return;
            }

            string fileName;
            while (fileStack.TryDequeue(out fileName))
            {
                btnSkip.Enabled = true;
                //imageFile.Image = Image.FromFile(fileName);
                var labelInfo = statusStrip.Items.Find("tsStatus", true).FirstOrDefault() as ToolStripLabel;
                // 如果文件被占用，则退出定时器，并提示错误信息
                if (!TryReadOpenFile(fileName))
                {
                    constUtitly.MsgError("无法读取文件。");
                    return;
                }

                //ImageFile.Image = Image.FromFile(fileName);
                labelInfo.Text = "已检测到新文件：" + fileName;

                try
                {
                    if (PicModel == emModel.Learn)
                    {
                        currentFileName = fileName;

                        if (string.IsNullOrEmpty(currentFileName))
                        {
                            return;
                        }

                        //Cv2.NamedWindow(windowName, WindowFlags.FreeRatio);

                        //img = Cv2.ImRead(currentFileName, ImreadModes.Color);
                        //// 初始化矩形的对角点
                        //imgCopy = img.Clone();

                        //// 鼠标回调函数
                        //Cv2.SetMouseCallback(windowName, DrawRectangle);

                        //Cv2.ImShow(windowName, img);

                        CloseWindow();

                        Process.Start("D:\\Viis_sxj\\MatchModeCreater\\bin\\Debug\\net6.0-windows\\MatchModeCreater.exe", new[] { currentFileName, fileIndex.ToString() });

                        Thread.Sleep(500);

                        SetCv2Form(150, 150, 1024, 768);
                    }
                    else
                    {
                        ProcessImage(fileName);
                    }
                }
                catch (Exception ex)
                {
                    constUtitly.MsgError("发生错误：" + ex.Message);
                    return;
                }
            }

            fileWaterTimer.Enabled = true;
        }

        SettingBase<BoWData> settingBow = new SettingBase<BoWData>();
        private void ProcessImage(string fileName)
        {
            switch (PicModel)
            {
                case emModel.Match:
                    MatchImage(fileName);
                    break;
                case emModel.MatchModel:
                    MatchModelImage(fileName);
                    (ParentForm as FormMain).SetStatus(true, 1);
                    (ParentForm as FormMain).SetStatus(false, 2);
                    break;
                default:
                    break;
            }

        }

        static object obj = new object();
        string perMatchedFilePath = string.Empty;
        private void MatchImage(string fileName)
        {
            (ParentForm as FormMain).SetStatus(true, 1);
            ImageCv imageCv = ImageCv.LoadImage(fileName);
            var img = imageCv.ResizeImage();
            var isMatched = false;
            var modelFileCount = new DirectoryInfo(modelFilePath).GetFiles("*.jpg").Count(s => !s.Name.EndsWith(".temp.jpg"));
            if (modelFileCount == 0)
            {
                throw new InvalidOperationException("尚未学习任何模板。");
            }

            var modelFileIndex = (fileIndex - 1) % modelFileCount + 1;
            //var emMatched = Parent.Controls.Find("EmModel", false).FirstOrDefault() as EmFrame;

            FileInfo matchedFileInfo = new FileInfo(fileName);
            if (!Directory.Exists(matchedFilePath))
            {
                Directory.CreateDirectory(matchedFilePath);
            }

            if (modelFileIndex == 1)
            {
                foreach (var c in panelThumbnail.Controls)
                {
                    if (c is PictureBox)
                    {
                        (c as PictureBox).BackColor = Color.White;
                    }
                }
            }

            panelThumbnail.Controls[modelFileIndex - 1].BackColor = Color.LightBlue;

            //判断是否黑白页
            if (settingBow.Setting.BoW.ContainsKey(modelFileIndex.ToString()))
            {
                var bow = settingBow.Setting.BoW[modelFileIndex.ToString()];
                int isBow = imageCv.isBlackOrWhite();
                isMatched = (isBow == bow);
                var mat = Cv2.ImRead(fileName);
                mat.SaveImage(Path.Combine(matchedFilePath, matchedFileInfo.Name));
            }
            else
            {
                //执行图像匹配
                ImageCv imageModel = ImageCv.LoadImage(Path.Combine(modelFilePath, modelFileIndex + ".jpg"));
                var proecssedImage = imageCv.procesImage();
                var matchedResult = imageCv.ContourMatching(imageModel);



                matchedResult.Item1.SaveImage(Path.Combine(matchedFilePath, matchedFileInfo.Name));
                isMatched = matchedResult.Item2;
            }

            // 显示匹配结果
            ImageFile.Image = EmFrameLearn.ReadToImage(Path.Combine(matchedFilePath, matchedFileInfo.Name));

            
            lblIndex.Text = fileIndex.ToString();
            var picModel = Parent.Controls.Find("picModel", false).FirstOrDefault() as PictureBox;
            var picPerModel = Parent.Controls.Find("picPerModel", false).FirstOrDefault() as PictureBox;

            picModel.Image = EmFrameLearn.ReadToImage(Path.Combine(modelFilePath, modelFileIndex + ".bmp"));
            //var perModelIndex = 0;
            //if (modelFileIndex == 1)
            //{
            //    perModelIndex = modelFileCount - 1;
            //}
            //else
            //{
            //    perModelIndex = modelFileIndex - 1;
            //}

            //picPerModel.Image = EmFrameLearn.ReadToImage(Path.Combine(modelFilePath, perModelIndex + ".bmp"));

            //emMatched.ImageFile.Image = EmFrameLearn.ReadToImage(Path.Combine(modelFilePath, modelFileIndex + ".bmp"));
            //emMatched.lblIndex.Text = modelFileIndex.ToString();

            if(!string.IsNullOrEmpty(perMatchedFilePath))
            {
                picPerModel.Image = EmFrameLearn.ReadToImage(perMatchedFilePath);
            }

            //if(isMatched)
            {
                perMatchedFilePath = Path.Combine(matchedFilePath, matchedFileInfo.Name);
            }
            LogHelper.GetLogger("Match").Info($"匹配结果：{isMatched}，文件名：{fileName}");

            if (isMatched)
            {
                isFirstMatchFailed = false;
                (ParentForm as FormMain).SetStatistic(isMatched);
                SetResultLabel(isMatched);
                ImageFile.BackColor = Color.Green;
                
                fileIndex++;
            }
            else
            {
                if (!isFirstMatchFailed)
                {
                    (ParentForm as FormMain).SetStatistic(isMatched);
                    SetResultLabel(isMatched);
                    LogHelper.GetLogger("Match").Info("调用GPIO。");
                    lock (obj)
                    {
                        SetGpio();
                    }
                    ImageFile.BackColor = Color.Red;
                    LogHelper.GetLogger("Match").Info("调用GPIO完成。");
                }

            }

            // (ParentForm as FormMain).SetStatus(false, 1);
        }

        public void SetResultLabel(bool isMatched)
        {
            lblResult.ForeColor = Color.White;
            if (isMatched)
            {
                lblResult.BackColor = Color.Green;
                lblResult.Text = "OK";
            }
            else
            {
                lblResult.BackColor = Color.Red;
                lblResult.Text = "NG";
            }
        }

        GpioHost host;
        private void SetGpio()
        {
            if (!(ParentForm as FormMain).statusData.isConnected)
            {
                LogHelper.GetLogger("Gpio").Info("未连接设备。");
                return;
            }

            var ports = settingStore.GetSetting().GpioPorts.Split(",");
            foreach (var port in ports)
            {
                var gPort = 0;
                if (int.TryParse(port, out gPort))
                {
                    host.SetValue(gPort, 1);
                    Thread.Sleep(settingStore.GetSetting().GPIODelayTime);
                    host.SetValue(gPort, 0);
                    Thread.Sleep(settingStore.GetSetting().GPIODelayTime);
                    LogHelper.GetLogger("Gpio").Info("触发GPIO端口号：" + gPort);
                }
                else
                {
                    constUtitly.MsgError($"GPIO端口号格式错误:{port}");
                    LogHelper.GetLogger("Gpio").Error($"GPIO端口号格式错误:{port}");
                    continue;
                }
            }
        }

        private void MatchModelImage(string fileName)
        {
            //throw new NotImplementedException();

        }

        private void LearnImage(string fileName)
        {
            (ParentForm as FormMain).SetStatus(true, 2);

            ImageCv imageCv = ImageCv.LoadImage(fileName);
            var img = imageCv.ResizeImage();
            var selected = imageCv.GetCenterImage();
            var selectedImages = selected.Item1;
            //Cv2.ImShow("截取中心图", selected.Item1);
            var contours = imageCv.procesImage(selectedImages);
            // 在原图上画出轮廓
            imageCv.DrawContours(contours.Item1, selected.Item2, true);
            //Cv2.ImShow("原画轮廓", imageCv.GetImg());
            imageCv.SaveImage(contours.Item2, Path.Combine(modelFilePath, fileIndex + ".jpg"));
            //Cv2.ImShow("保存模板", contours.Item2);
            imageCv.Save(Path.Combine(modelFilePath, fileIndex + ".bmp"));

            // 显示结果
            ImageFile.Image = EmFrameLearn.ReadToImage(Path.Combine(modelFilePath, fileIndex + ".bmp"));
            lblIndex.Text = fileIndex.ToString();
            fileIndex++;

            (ParentForm as FormMain).SetStatus(false, 2);
        }

        private bool TryReadOpenFile(string fileName)
        {
            DateTime startTime = DateTime.Now;
            TimeSpan timeout = TimeSpan.FromSeconds(10); // 设置超时时间为10秒

            while (DateTime.Now - startTime < timeout)
            {
                try
                {
                    using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        var img = Image.FromStream(fs);
                        ImageFile.Image = img;
                        return true; // 成功读取文件，返回true
                    }
                }
                catch (IOException)
                {
                    // 文件被占用或其他IO异常，等待100毫秒后重试
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    // 非预期的异常，记录错误并返回false
                    Console.WriteLine("读取文件时发生非预期错误: " + ex.Message);
                    return false;
                }
            }

            // 超时，返回false
            return false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Loading = true;
            if (isContiune)
            {
                Stop();
            }
            else
            {
                Start();
            }

            btnStart.Loading = false;
        }

        private void cbDeviceList_Click(object sender, EventArgs e)
        {

        }

        private void cbDeviceList_SelectedIndexChanged(object sender, int value)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseWindow();
            Parent.Controls.Clear();
        }

        private void cmbTrigger_CheckedChanged(object sender, bool value)
        {
            //cmbTrigger.Checked?(ParentForm as FormMain).mvs.StopGrabbing():(ParentForm as FormMain).mvs.StartGrabbing();

            if (cmbTrigger.Checked)
            {
                if (!(ParentForm as FormMain).mvs.m_bGrabbing)
                {
                    (ParentForm as FormMain).mvs.StartGrabbing();
                }
            }
            else
            {
                if ((ParentForm as FormMain).mvs.m_bGrabbing)
                {
                    (ParentForm as FormMain).mvs.StopGrabbing();
                }
            }
        }

        Point topLeft, bottomRight;
        const string windowName = "图片匹配模板创建工具";
        private void DrawRectangle(MouseEventTypes @event, int x, int y, MouseEventFlags flags, IntPtr userData)
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
                    Cv2.ImShow(windowName, img);
                    SelectImgContours();

                    break;
                case MouseEventTypes.MouseMove:
                    if ((flags & MouseEventFlags.LButton) != 0) // 鼠标左键按下时移动
                    {
                        // 清除之前的矩形
                        img = imgCopy.Clone();
                        Cv2.Rectangle(img, topLeft, new Point(x, y), ImageCv.contoursColor, 1);
                        Cv2.ImShow(windowName, img);
                    }
                    break;
                default:
                    break;
            }
        }

        private void SelectImgContours()
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
                Cv2.ImShow(windowName, img);
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
            Cv2.ImShow(windowName, img);

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

            Cv2.ImWrite(Path.Combine(modelFilePath, fileIndex + ".bmp"), img);

        }

        private void ImageFile_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(currentFileName))
            //{
            //    return;
            //}

            //Cv2.NamedWindow(windowName, WindowFlags.FreeRatio);

            //img = Cv2.ImRead(currentFileName, ImreadModes.Color);
            //// 初始化矩形的对角点
            //imgCopy = img.Clone();

            //// 鼠标回调函数
            //Cv2.SetMouseCallback(windowName, DrawRectangle);

            //Cv2.ImShow(windowName, img);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            lblIndex.Text = fileIndex.ToString();
            fileIndex++;
            CloseWindow();
        }

        private void btnResetIndex_Click(object sender, EventArgs e)
        {
            ResetIndex();
        }

        public void CamDispose()
        {
            if (btnStart.Text == "停止")
            {
                //constUtitly.MsgInfo("停止摄像头后才能释放资源。");
                Stop();
            }
        }

        private void EmFrame_Load(object sender, EventArgs e)
        {
            Start();
            if(!(ParentForm as FormMain).mvs.m_bGrabbing)
            {
                (ParentForm as FormMain).mvs.StartGrabbing();
            }

            cmbTrigger.Checked = (ParentForm as FormMain).mvs.m_bGrabbing;
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            fileIndex++;
            btnSkip.Enabled = false;
        }

        private void BtnModel_Click(object sender, EventArgs e)
        {
            (ParentForm as FormMain).btnPicLearn_Click(sender, e);
        }
    }

    public enum emModel
    {
        // 学习模板
        Learn,

        // 匹配模版
        MatchModel,

        // 匹配样本
        Match
    }
}
