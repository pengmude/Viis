using AntdUI;
using BasicDemo;
using GpioCheck;
using MvCamCtrl.NET;
using OpenCvSharp;
using System.IO;
using System.Runtime.InteropServices;
using ViisApp.Controls;
using ViisApp.Lib;
using static MvCamCtrl.NET.MyCamera;

namespace ViisApp
{
    public partial class FormMain : //Form
        AntdUI.BaseForm
    {
        public StatusData statusData = new StatusData();
        SettingStore settingStore = new SettingStore();
        AntList<StatisticData> _listStatistic = new AntList<StatisticData>();
        public MvsCtl mvs = new MvsCtl();
        public bool isLicenseValid = false;

        public FormMain()
        {
            InitializeComponent();

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Top = 0;
            Left = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;

            //try
            //{
            //    // 验证授权文件
            //    if (!LicenceHelper.CheckLicence("License.lic"))
            //    {
            //        constUtitly.MsgError("授权文件验证失败！");
            //        isLicenseValid = false;
            //        ShowDialog<FormSetting>();
            //    }else
            //    {
            //        isLicenseValid = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    constUtitly.MsgError("授权文件验证出错,请联系管理员。" + ex.Message);
            //    isLicenseValid = false;
            //    ShowDialog<FormSetting>();
            //}

            //if (!isLicenseValid)
            //{
            //    //DisableAllControls(this);
            //    Application.Exit();
            //}

            timerStatus.Enabled = true;
            chkConnect.Click += CheckClick;
            chkScan.Click += CheckClick;
            chkLearn.Click += CheckClick;

            tabStatistics.Columns = new Column[] { new Column("FieldName", "字段名"), new Column("Value", "值") };
            tabStatistics.Columns[0].Align = ColumnAlign.Left;
            tabStatistics.Columns[0].Width = "140";

            tabStatistics.Columns[1].Align = ColumnAlign.Right;
            tabStatistics.Columns[1].Width = "55";

            _listStatistic.Add(new StatisticData() { FieldName = "OK数", Value = "0" });
            _listStatistic.Add(new StatisticData() { FieldName = "NG数", Value = "0" });
            _listStatistic.Add(new StatisticData() { FieldName = "总数", Value = "0" });
            _listStatistic.Add(new StatisticData() { FieldName = "NG率", Value = "0%" });
            _listStatistic.Add(new StatisticData() { FieldName = "", Value = "" });
            _listStatistic.Add(new StatisticData() { FieldName = "检测热线", Value = "" });
            _listStatistic.Add(new StatisticData() { FieldName = "16638508809", Value = "" });

            tabStatistics.Binding(_listStatistic);

            //tabStatistics.
            tabStatistics.VisibleHeader = false;

            statusData.isConnected = false;
            statusData.isScanning = false;
            statusData.isLearning = false;
            OpenDevice();

            this.Text = settingStore.GetSetting().AppName;
            windowBar1.Text = settingStore.GetSetting().AppName;
            btnPicLearn_Click(sender, e);
        }

        public void SetStatistic(bool isSuccessed)
        {
            if (isSuccessed)
            {
                _listStatistic.FirstOrDefault(x => x.FieldName == "OK数").Value = (int.Parse(_listStatistic.FirstOrDefault(x => x.FieldName == "OK数").Value) + 1).ToString();
            }
            else
            {
                _listStatistic.FirstOrDefault(x => x.FieldName == "NG数").Value = (int.Parse(_listStatistic.FirstOrDefault(x => x.FieldName == "NG数").Value) + 1).ToString();
            }

            _listStatistic.FirstOrDefault(x => x.FieldName == "总数").Value = (int.Parse(_listStatistic.FirstOrDefault(x => x.FieldName == "总数").Value) + 1).ToString();
            _listStatistic.FirstOrDefault(x => x.FieldName == "NG率").Value = (float.Parse(_listStatistic.FirstOrDefault(x => x.FieldName == "NG数").Value) /
                float.Parse(_listStatistic.FirstOrDefault(x => x.FieldName == "总数").Value) * 100).ToString("0") + "%";

            tabStatistics.Binding(_listStatistic);
        }

        public void ClearStatistic()
        {
            foreach (var item in _listStatistic)
            {
                item.Value = "0";
            }

            tabStatistics.Binding(_listStatistic);
        }

        public void OpenDevice()
        {
            try
            {
                var devices = mvs.EnumDevices();
                if (devices.nDeviceNum == 0)
                {
                    constUtitly.MsgInfo("没有找到摄像头。");
                }
                else
                {
                    try
                    {
                        MV_CC_DEVICE_INFO device = (MV_CC_DEVICE_INFO)Marshal.PtrToStructure(devices.pDeviceInfo[0], typeof(MyCamera.MV_CC_DEVICE_INFO));
                        mvs.OpenDevice(device);
                        mvs.StartGrabbing(true);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("打开摄像头失败。" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                constUtitly.MsgError("打开摄像头失败。" + ex.Message);
            }
        }

        private void CheckClick(object sender, EventArgs e)
        {
            //保持check状态
            if (sender is CheckBox chk)
            {
                chk.Checked = !chk.Checked;
            }
        }

        //GpioHost gpioHost = new GpioHost();

        FileInfo[] testFiles;
        int testIndex = 0;
        private void btnConnect_Click(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(() =>
            //{
            //    //gpioHost.Test();
            //    FileCreateCrayzee();
            //});
            string path = settingStore.GetSetting().CamPath;
            string source = Path.Combine(Application.StartupPath, "CAM1");

            DirectoryInfo directoryInfo = new DirectoryInfo(source);
            testFiles = directoryInfo.GetFiles("*.bmp");

            File.Copy(testFiles[testIndex].FullName, Path.Combine(path, Guid.NewGuid().ToString() + ".bmp"), true);
            //string text = PerformOCR(Path.Combine(path, testFiles[testIndex].Name));
            //constUtitly.MsgInfo(text);
            testIndex++;

            if (testIndex > testFiles.Length - 1)
            {
                testIndex = 0;
            }


        }

        void ShowEmFrame()
        {
            if (panelMain.Controls.Count == 1)
            {
                panelMain.Controls[0].Dock = DockStyle.Fill;
            }

            else if (panelMain.Controls.Count == 3)
            {
                panelMain.Controls[0].Width = (int)Math.Round(panelMain.Width * ((float)2 / 3));
                panelMain.Controls[0].Height = panelMain.Height;

                var picModel = panelMain.Controls.Find("picModel", false).FirstOrDefault();
                var picPerModel = panelMain.Controls.Find("picPerModel", false).FirstOrDefault();
                picModel.Width = panelMain.Width - panelMain.Controls[0].Width;
                picModel.Left = panelMain.Controls[0].Width;
                picModel.Top = 0;
                picModel.Height = panelMain.Height / 2;

                picPerModel.Width = panelMain.Controls[1].Width;
                picPerModel.Left = panelMain.Controls[1].Left;
                picPerModel.Height = panelMain.Height - panelMain.Controls[1].Height - 10;
                picPerModel.Top = panelMain.Height - picModel.Height;
            }
        }

        public void btnPicLearn_Click(object sender, EventArgs e)
        {
            btnPicLearn.Loading = true;
            Cv2.DestroyAllWindows();
            StopDevice();

            panelMain.Controls.Clear();

            //var emPicLearn = new EmFrame(emModel.Learn, settingStore.GetSetting().CamPath);
            //panelMain.Controls.Add(emPicLearn);
            //emPicLearn.Show();

            var emPicLearn = new EmFrameLearn(emModel.Learn, settingStore.GetSetting().CamPath);
            panelMain.Controls.Add(emPicLearn);
            ShowEmFrame();
            emPicLearn.Show();
            statusData.isLearning = true;
            statusData.isScanning = false;

            //OpenDevice();
            btnPicLearn.Loading = false;
        }

        GpioHost GpioHost;
        private void StopDevice()
        {
            foreach (Control control in panelMain.Controls)
            {
                if (control is EmFrame)
                {
                    (control as EmFrame).CamDispose();
                }

                if (control is EmFrameLearn)
                {
                    (control as EmFrameLearn).CamDispose();
                }
            }
        }



        //private void FindForm(string formName)

        // 模拟生成数据
        private void FileCreateCrayzee()
        {
            string path = settingStore.GetSetting().CamPath;
            string source = Path.Combine(Application.StartupPath, "CAM1");
            int i = 0;
            var files = Directory.GetFiles(source, "*.bmp");

            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                File.Copy(file, Path.Combine(path, fileInfo.Name), true);

                Thread.Sleep(10000);
            }
        }

        private void btnPicScan_Click(object sender, EventArgs e)
        {
            btnPicScan.Loading = true;
            Cv2.DestroyAllWindows();
            //StopDevice();
            panelMain.Controls.Clear();
            var EmMatched = new EmFrame(emModel.Match, settingStore.GetSetting().CamPath);
            panelMain.Controls.Add(EmMatched);

            var picModel = new PictureBox();
            picModel.Name = "picModel";
            picModel.SizeMode = PictureBoxSizeMode.Zoom;
            picModel.BorderStyle = BorderStyle.FixedSingle;
            panelMain.Controls.Add(picModel);

            var picPerModel = new PictureBox();
            picPerModel.Name = "picPerModel";
            picPerModel.SizeMode = PictureBoxSizeMode.Zoom;
            picPerModel.BorderStyle = BorderStyle.FixedSingle;
            panelMain.Controls.Add(picPerModel);

            ShowEmFrame();
            EmMatched.Show();
            picModel.Show();
            picPerModel.Show();

            statusData.isScanning = true;
            statusData.isLearning = false;

            btnPicScan.Loading = false;
        }


        private void timerStatus_Tick(object sender, EventArgs e)
        {
            chkScan.Checked = statusData.isScanning;
            chkLearn.Checked = statusData.isLearning;

            chkConnect.Checked = statusData.isConnected;
            chkDisConnect.Checked = !statusData.isConnected;

            if (statusData.isConnected)
            {
                chkConnect.BackColor = Color.Green;
                chkDisConnect.BackColor = Color.Green;
                chkConnect.ForeColor = Color.White;
                chkDisConnect.ForeColor = Color.White;
            }
            else
            {
                chkConnect.BackColor = Color.Red;
                chkDisConnect.BackColor = Color.Red;
                chkConnect.ForeColor = Color.White;
                chkDisConnect.ForeColor = Color.White;
            }
        }

        private void ManageFolder(SettingData settingData)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(settingData.CamPath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            if (directoryInfo.GetFiles("*.bmp").Length > settingData.CAM_MAX_FILE_NUM)
            {
                var files = directoryInfo.GetFiles("*.bmp").OrderBy(f => f.CreationTime);
                foreach (var file in files.Take(files.Count() - settingData.CAM_MAX_FILE_NUM))
                {
                    file.Delete();
                }
            }

            directoryInfo = new DirectoryInfo(settingData.MatchedPath);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            if (directoryInfo.GetFiles("*.bmp").Length > settingData.CAM_MAX_FILE_NUM)
            {
                var files = directoryInfo.GetFiles("*.bmp").OrderBy(f => f.CreationTime);
                foreach (var file in files.Take(files.Count() - settingData.CAM_MAX_FILE_NUM))
                {
                    file.Delete();
                }
            }

            FileInfo gpioLogFile = new FileInfo("log.txt");
            if (gpioLogFile.Exists && gpioLogFile.Length > 1024 * 1024)
            {
                gpioLogFile.Delete();
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (FormSetting.ModifiySetting() == DialogResult.OK)
            {
                settingStore.Update();
            }
        }

        private void ShowDialog<T>()
        {
            var form = Activator.CreateInstance(typeof(T)) as BaseForm;
            form.Owner = this;
            form.ShowDialog();
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="status">设置为true或false</param>
        /// <param name="statusType">
        /// 0:isConnected
        /// 1:isScanning
        /// 2:isLearning
        /// </param>
        public void SetStatus(bool status, int statusType)
        {
            switch (statusType)
            {
                case 0:
                    statusData.isConnected = status;
                    break;
                case 1:
                    statusData.isScanning = status;
                    break;
                case 2:
                    statusData.isLearning = status;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(() =>
            //{
            //    DirectoryInfo directoryInfo = new DirectoryInfo("H:\\Project\\yoloTest\\CAM4");

            //    foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            //    {
            //        Thread.Sleep(500);
            //        File.Copy(fileInfo.FullName, Path.Combine("E:\\CAM4", fileInfo.Name), true);
            //    }
            //});

            MainForm mainForm = new MainForm();
            mainForm.ShowDialog();

        }

        private void btnMvs_Click(object sender, EventArgs e)
        {
            CloseDevice();
            new Form1().ShowDialog();
            OpenDevice();
        }



        private void btnPicTest_Click(object sender, EventArgs e)
        {
            //ShowDialog<MatchDemo>();
        }

        public void StartScan()
        {
            panelMain.Controls.Clear();
            Thread.Sleep(100);

            //EmStatistics.FindStatisticsControl().ClearStatistic();
            btnPicScan_Click(null, null);
        }



        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //StopDevice();
            CloseDevice();
        }

        public void CloseDevice()
        {
            mvs.StopGrabbing();
            mvs.CloseDevice();
            mvs.Dispose();
        }

        private void chkConnect_Click(object sender, EventArgs e)
        {
            statusData.isConnected = true;
            chkConnect.Checked = true;
        }

        private void chkDisConnect_Click(object sender, EventArgs e)
        {
            statusData.isConnected = false;
            chkConnect.Checked = false;
        }

        private void chkLearn_Click(object sender, EventArgs e)
        {
            statusData.isLearning = true;
            statusData.isScanning = false;
            btnPicLearn_Click(sender, e);
        }

        private void chkScan_Click(object sender, EventArgs e)
        {
            statusData.isLearning = false;
            statusData.isScanning = true;
            btnPicScan_Click(sender, e);
        }

        public void SetTriggerModel(bool @checked)
        {
            mvs.TriggerModel(@checked);
        }

        private void btnStClear_Click(object sender, EventArgs e)
        {
            ClearStatistic();
        }

        int clicked = 0;
        private void panel1_Click(object sender, EventArgs e)
        {
            clicked++;
            if (clicked == 5)
            {
                btnConnect.Visible = true;
            }
            else
            {
                btnConnect.Visible = false;
            }

            if (clicked == 20)
            {
                clicked = 0;
            }
        }

        private void timerFilesMange_Tick(object sender, EventArgs e)
        {
            ManageFolder(settingStore.GetSetting());

            ManageLog();
        }
        private void ManageLog()
        {

            var canDelete = DateTime.Now.AddMonths(-1);

            if (canDelete.Day == 15 && canDelete.Hour == 0 && canDelete.Minute == 0 && canDelete.Second == 0) // 15号删除
            {
                var directoryInfo = new DirectoryInfo("logs");
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }

                var files = directoryInfo.GetFiles("*.log");
                foreach (var file in files)
                {
                    if (file.CreationTime < canDelete)
                    {
                        if (file.CreationTime < canDelete)
                        {
                            file.Delete();
                        }
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            btnMvs_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnSetting_Click(sender, e);
        }

        private void panelFunction_Click(object sender, EventArgs e)
        {
            clicked++;
            if (clicked == 5)
            {
                btnConnect.Visible = true;
            }
            else
            {
                btnConnect.Visible = false;
            }

            if (clicked == 20)
            {
                clicked = 0;
            }
        }
    }

    public class StatusData
    {
        public bool isConnected { get; set; }
        public bool isScanning { get; set; }
        public bool isLearning { get; set; }
    }

    public class StatisticData
    {
        public string FieldName { get; set; }
        public string Value { get; set; }
    }

    
}