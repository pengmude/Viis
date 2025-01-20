using MvCamCtrl.NET;
using OpenCvSharp;
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
    public partial class EmFrameLearn : UserControl
    {
        //const string matchModeCreater = "G:\\GitLib\\NewProject\\Viis_sxj\\MatchModeCreater\\bin\\Debug\\net6.0-windows\\MatchModeCreater.exe";
        //const string matchModeCreater = "m:\\NewProject\\Viis_sxj\\MatchModeCreater\\bin\\Debug\\net6.0-windows\\MatchModeCreater.exe";
        //const string matchModeCreater = "MatchModeCreater.exe";
        //const string matchModeCreate = "D:\\Viis_sxj\\ViisApp\\bin\\Debug\\net6.0-windows\\MatchModeCreater.exe";
        FileSystemWatcher fileWatcher, modelWatcher;
        ConcurrentQueue<string> fileStack, modelStack;
        SettingStore settingStore = new SettingStore();
        string modelFilePath, matchedFilePath;
        Mat img, imgCopy;
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

        EmFrameLearn()
        {
            InitializeComponent();
        }
        bool isContiune = false;
        //MvsCtl ctl;
        //MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MV_CC_DEVICE_INFO_LIST();
        public EmFrameLearn(emModel model, string watchFileFolder = "")
        {
            InitializeComponent();
            modelFilePath = settingStore.GetSetting().ModelPath;
            matchedFilePath = settingStore.GetSetting().MatchedPath;
            PicModel = model;

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
                btnSave.Visible = false;
            }

            if (PicModel == emModel.Match)
            {
                labelInfo.Text = "匹配样本";
                btnSave.Visible = false;
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

                modelWatcher = new FileSystemWatcher(modelFilePath);
                modelWatcher.Filter = "*.temp.jpg";
                modelWatcher.Changed += ModelWatcher_Changed;
                modelWatcher.EnableRaisingEvents = true;
            }

            try
            {
                //ctl = new MvsCtl();

                //m_stDeviceList = ctl.EnumDevices();

                //if (m_stDeviceList.nDeviceNum == 0)
                //{
                //    constUtitly.MsgError("未找到设备。");
                //    return;
                //}

                //for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
                //{
                //    MyCamera.MV_CC_DEVICE_INFO device =
                //        (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(
                //            m_stDeviceList.pDeviceInfo[i],
                //            typeof(MyCamera.MV_CC_DEVICE_INFO));

                //    if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                //    {
                //        MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));

                //        if (gigeInfo.chUserDefinedName != "")
                //        {
                //            cbDeviceList.Items.Add("GEV: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
                //        }
                //        else
                //        {
                //            cbDeviceList.Items.Add("GEV: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                //        }
                //    }
                //    else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                //    {
                //        MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                //        if (usbInfo.chUserDefinedName != "")
                //        {
                //            cbDeviceList.Items.Add("U3V: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")");
                //        }
                //        else
                //        {
                //            cbDeviceList.Items.Add("U3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                //        }
                //    }
                //}

                //if (m_stDeviceList.nDeviceNum > 0)
                //{
                //    cbDeviceList.SelectedIndex = 0;
                //}
            }
            catch (Exception ex)
            {
                constUtitly.MsgError(ex.Message);
            }
        }


        private void ModelWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            modelStack.Enqueue(e.FullPath);
        }


        void Start()
        {
            //if (cbDeviceList.SelectedIndex >= 0)
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
            //(ParentForm as FormMain).OpenDevice();
            isContiune = true;
            btnStart.Text = "停止";
            btnStart.Type = AntdUI.TTypeMini.Warn;

            //cbDeviceList.Clear();

            fileStack = new ConcurrentQueue<string>();
            modelStack = new ConcurrentQueue<string>();

            if (!Directory.Exists(modelFilePath))
            {
                Directory.CreateDirectory(modelFilePath);
            }

            //DirectoryInfo dir = new DirectoryInfo(modelFilePath);
            //FileInfo[] files = dir.GetFiles("*.jpg");

            //if (PicModel == emModel.Learn)
            //{
            //    if (files.Count() > 0)
            //    {

            //        if (constUtitly.MsgAsk("检测到已有模型文件，是否覆盖？ \n -若选择是，则之前的模型文件将被删除。 \n -若选择否，则从最后的文件开始学习。") == DialogResult.Yes)
            //        {
            //            files = dir.GetFiles("*.*");
            //            foreach (var file in files)
            //            {
            //                try
            //                {
            //                    File.Delete(file.FullName);
            //                }
            //                catch
            //                { }
            //            }

            //            ResetIndex();
            //        }
            //        else
            //        {
            //            fileIndex = files.Count() + 1;
            //        }
            //    }
            //}

            if (fileWatcher != null)
            {
                fileWaterTimer.Enabled = true;
                fileWatcher.Created += FileWatcher_Created;
                fileWatcher.EnableRaisingEvents = true;
            }

        }

        private void ResetIndex()
        {
            fileIndex = 1;
        }

        private void Stop()
        {
            //(ParentForm as FormMain).CloseDevice();
            isContiune = false;
            btnStart.Text = "启动";
            btnStart.Type = AntdUI.TTypeMini.Default;
            fileWatcher.Created -= FileWatcher_Created;
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

                        if (panelPic.Controls.Count < 30)
                        {
                            PutPicture(fileName, fileIndex);
                        }
                        //(ParentForm as FormMain).btnPicLearn_Click(null, null);
                        fileIndex++;
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

        private void ShowPicSelect(string fileName, string index)
        {
            try
            {
                CloseWindow();

                //Process.Start(matchModeCreater, new[] { fileName, index.ToString() });

                Process.Start("D:\\Viis_sxj\\MatchModeCreater\\bin\\Debug\\net6.0-windows\\MatchModeCreater.exe", new[] { fileName, index.ToString() });

                Thread.Sleep(200);

                SetCv2Form(150, 150, 1024, 768);
            }
            catch (Exception ex)
            {
                constUtitly.MsgError("打开详细图片查看失败：" + ex.Message);
            }
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
                        //ImageFile.Image = img;
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            //CloseWindow();

            (ParentForm as FormMain).StartScan();
        }

        private void cmbTrigger_CheckedChanged(object sender, bool value)
        {
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




        private void btnSave_Click(object sender, EventArgs e)
        {
            DirectoryInfo modeDirInfo = new DirectoryInfo(modelFilePath);
            FileInfo[] files = modeDirInfo.GetFiles("*.bmp").OrderBy(t => int.Parse(t.Name.Replace(".bmp", ""))).ToArray();
            int i = 1;
            foreach (var file in files)
            {
                string fileName = file.Name.Replace(".bmp", "");
                var testI = int.Parse(fileName);
                if (testI != i)
                {
                    constUtitly.MsgError("文件名不连续，请检查文件名。");
                    return;
                }
                i++;
            }

            //CloseWindow();
            //Stop();

            btnClose_Click(sender, e);
        }

        private void btnResetIndex_Click(object sender, EventArgs e)
        {
            ResetIndex();
        }

        int sideLength = 320;
        int splitLength = 10;
        int col = 5;
        private void EmFrameLearn_Load(object sender, EventArgs e)
        {
            var files = Directory.GetFiles(settingStore.GetSetting().CamPath, "*.bmp");
            for (int i = 0; i < files.Length; i++)
            {
                PutPicture(files[i], i);

                if (panelPic.Controls.Count >= 30)
                {
                    break;
                }
            }

            var modelFiles = Directory.GetFiles(modelFilePath, "*.temp.jpg");
            foreach (var file in modelFiles)
            {
                var modelFileInfo = new FileInfo(file);
                SetModelFlag(modelFileInfo);
            }

            fileIndex = files.Length;

            Start();
        }

        SettingBase<BoWData> settingBow = new SettingBase<BoWData>();
        private void PutPicture(string file, int index)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Size = new System.Drawing.Size(sideLength, sideLength);
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new System.Drawing.Point((index % col) * (splitLength + sideLength) + splitLength, (index / col) * (splitLength + sideLength) + splitLength);
            pictureBox.ImageLocation = file;
            pictureBox.Image = ReadToImage(file);
            pictureBox.Click += PictureBox_Click;
            pictureBox.BringToFront();

            panelPic.Controls.Add(pictureBox);

            Label label = new Label();
            label.Text = (index + 1).ToString();
            label.Font = new System.Drawing.Font("微软雅黑", 20);
            label.ForeColor = Color.Blue;
            label.BackColor = Color.Transparent;
            label.Location = new System.Drawing.Point(5, 5);
            label.AutoSize = true;
            label.Name = "label";
            pictureBox.Controls.Add(label);
            label.BringToFront();



            Button btnWhite = new Button();
            btnWhite.Text = "全白";
            btnWhite.Font = new System.Drawing.Font("微软雅黑", 8);
            btnWhite.ForeColor = Color.Blue;
            btnWhite.BackColor = Color.White;
            btnWhite.Size = new System.Drawing.Size(50, 35);
            btnWhite.Location = new System.Drawing.Point(pictureBox.Width - btnWhite.Size.Width, 0);
            btnWhite.Click += btnWhite_Click;
            pictureBox.Controls.Add(btnWhite);
            btnWhite.BringToFront();

            Button btnBlack = new Button();
            btnBlack.Text = "全黑";
            btnBlack.Font = new System.Drawing.Font("微软雅黑", 8);
            btnBlack.ForeColor = Color.Blue;
            btnBlack.BackColor = Color.White;
            btnBlack.Size = new System.Drawing.Size(50, 35);
            btnBlack.Location = new System.Drawing.Point(pictureBox.Width - btnWhite.Size.Width, btnBlack.Size.Height);
            btnBlack.Click += btnBlack_Click;
            pictureBox.Controls.Add(btnBlack);
            btnWhite.BringToFront();

            Button btnClear = new Button();
            btnClear.Text = "清除";
            btnClear.Font = new System.Drawing.Font("微软雅黑", 8);
            btnClear.ForeColor = Color.Blue;
            btnClear.BackColor = Color.White;
            btnClear.Size = new System.Drawing.Size(50, 35);
            btnClear.Location = new System.Drawing.Point(pictureBox.Width - btnWhite.Size.Width, btnClear.Size.Height * 2);
            btnClear.Click += btnClear_Click;
            pictureBox.Controls.Add(btnClear);
            btnWhite.BringToFront();

            var bow = 0;
            if (settingBow.Setting.BoW.ContainsKey((index + 1).ToString()))
            {
                bow = settingBow.Setting.BoW[(index + 1).ToString()];
            }

            if (bow == BoWData.defWhite)
            {
                btnWhite.BackColor = Color.LightBlue;
            }

            if (bow == BoWData.defBlack)
            {
                btnBlack.BackColor = Color.LightBlue;
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            var picName = (sender as Button).Parent.Controls.Find("label", true)[0].Text;
            ClearBackColor(sender);

            var fileName = Path.Combine(modelFilePath, picName + ".temp.jpg");
            var fileInfo = new FileInfo(fileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                File.Delete(fileName.Replace(".temp.jpg", ".bmp"));
                File.Delete(fileName.Replace(".temp.jpg", ".jpg"));
            }

            //SetModelFlag(fileInfo);
            settingBow.Setting.BoW.Remove(picName);
            settingBow.Save();

            (ParentForm as FormMain).btnPicLearn_Click(sender, e);
        }

        private static void ClearBackColor(object? sender)
        {
            ControlCollection buttons;
            if (sender is Button)
            {
                buttons = (sender as Button).Parent.Controls;

            }
            else if (sender is PictureBox)
            {
                buttons = (sender as PictureBox).Controls;
            }
            else
            {
                return;
            }

            foreach (var button in buttons)
            {
                if (button is Button)
                {
                    (button as Button).BackColor = Color.White;
                }
            }
        }

        public void SaveModel(FileInfo file, int index)
        {
            var matchFileName = Path.Combine(modelFilePath, index.ToString() + ".jpg");
            File.Copy(file.FullName, matchFileName, true);
            File.Copy(file.FullName, matchFileName.Replace(".jpg", ".temp.jpg"), true);
        }

        private void btnWhite_Click(object sender, EventArgs e)
        {
            var picName = (sender as Button).Parent.Controls.Find("label", true)[0].Text;
            if (settingBow.Setting.BoW.ContainsKey(picName))
            {
                settingBow.Setting.BoW[picName] = BoWData.defWhite;

            }
            else
            {
                settingBow.Setting.BoW.Add(picName, BoWData.defWhite);
            }

            var pictureBox = (sender as Button).Parent as PictureBox;
            SaveModel(new FileInfo(pictureBox.ImageLocation), int.Parse(picName));

            ClearBackColor(sender);
            (sender as Button).BackColor = Color.LightBlue;
            settingBow.Save();
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            var picName = (sender as Button).Parent.Controls.Find("label", true)[0].Text;
            if (settingBow.Setting.BoW.ContainsKey(picName))
            {
                settingBow.Setting.BoW[picName] = BoWData.defBlack;

            }
            else
            {
                settingBow.Setting.BoW.Add(picName, BoWData.defBlack);
            }

            var pictureBox = (sender as Button).Parent as PictureBox;
            SaveModel(new FileInfo(pictureBox.ImageLocation), int.Parse(picName));

            ClearBackColor(sender);
            (sender as Button).BackColor = Color.LightBlue;
            settingBow.Save();
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            pictureBox.BackColor = Color.LightSkyBlue;
            //pictureBox.Refresh();
            string fileName = pictureBox.ImageLocation;
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            var modelIndex = GetFileNameFromPicture(pictureBox);

            if (settingBow.Setting.BoW.ContainsKey(modelIndex.ToString()))
            {
                if (constUtitly.MsgAsk("是否清空当前图片的模板？") == DialogResult.Yes)
                {
                    settingBow.Setting.BoW.Remove(modelIndex.ToString());
                    settingBow.Save();

                    ClearBackColor(sender);

                    ShowPicSelect(fileName, GetFileNameFromPicture(pictureBox));
                }
            }
            else
            {
                ShowPicSelect(fileName, GetFileNameFromPicture(pictureBox));
            }

        }

        bool firstTick = true;
        private void WatchPicFileTimer_Tick(object sender, EventArgs e)
        {
            fileWaterTimer.Enabled = false;
            if (!isContiune)
            {

                return;
            }
            string fileName;

            while (modelStack.TryDequeue(out fileName))
            {
                var file = new FileInfo(fileName);
                try
                {
                    if (!TryReadOpenFile(fileName))
                    {
                        constUtitly.MsgError("无法读取文件。");
                        return;
                    }

                    SetModelFlag(file);
                }
                catch (Exception ex)
                {
                    constUtitly.MsgError(ex.Message);
                }
            }

            if (firstTick)
            {
                var files = Directory.GetFiles(modelFilePath, "*.temp.jpg");
                foreach (var file in files)
                {
                    var modelFileInfo = new FileInfo(file);
                    SetModelFlag(modelFileInfo);
                }
            }

            fileWaterTimer.Enabled = true;

        }

        public string GetFileNameFromPicture(PictureBox box)
        {
            return (box.Controls.Find("label", true).First() as Label).Text;
        }

        private void SetModelFlag(FileInfo file)
        {
            foreach (var c in panelPic.Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox pictureBox = (PictureBox)c;
                    if (file.Name.Replace(".temp.jpg", "") == GetFileNameFromPicture(pictureBox))
                    {
                        try
                        {
                            var formalFile = file.FullName.Replace(".temp.jpg", ".bmp");
                            pictureBox.Image.Dispose();
                            File.Copy(file.FullName, formalFile, true);
                            pictureBox.Image = ReadToImage(formalFile);
                            pictureBox.BackColor = Color.LightGreen;
                            pictureBox.Refresh();
                        }
                        catch (Exception ex)
                        {
                            constUtitly.MsgError(ex.Message);
                        }
                    }
                }
            }
        }

        public static Bitmap ReadToImage(string fileName)
        {
            var img = Cv2.ImRead(fileName, ImreadModes.Color);

            // 步骤1: 确保Mat对象不是空的
            if (img == null || img.Empty())
            {
                throw new ArgumentNullException("Mat object is null or empty.");
            }

            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(img);
        }

        private void btnClearFolder_Click(object sender, EventArgs e)
        {
            var camPath = settingStore.GetSetting().CamPath;

            var camDir = new DirectoryInfo(camPath);
            var files = camDir.GetFiles("*.bmp");
            foreach (var file in files)
            {
                file.Delete();
            }

            var modelDir = new DirectoryInfo(modelFilePath);
            var modelFiles = modelDir.GetFiles("*.*");
            foreach (var file in modelFiles)
            {
                file.Delete();
            }

            ResetIndex();

            constUtitly.MsgInfo("已清空文件夹。");

            settingBow.Reset();
            (ParentForm as FormMain).btnPicLearn_Click(sender, e);
        }

        public void CamDispose()
        {
            if (btnStart.Text == "停止")
            {
                //constUtitly.MsgInfo("停止摄像头后才能释放资源。");
                Stop();
            }
        }

        private void BtnModel_Click(object sender, EventArgs e)
        {
            (ParentForm as FormMain).btnPicLearn_Click(sender, e);
        }
    }
}
