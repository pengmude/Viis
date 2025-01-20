using AntdUI;
using GpioCheck;
using ViisApp.Lib;

namespace ViisApp
{
    public partial class FormSetting : BaseForm
    {
        SettingStore store = new SettingStore();
        public FormSetting()
        {
            InitializeComponent();
        }

        public static DialogResult ModifiySetting()
        {
            FormSetting form = new FormSetting();
            return form.ShowDialog();
        }

        private void btnCamBrowser_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtCam.Text = this.fbd.SelectedPath;
            }
        }

        private void btnModelBrowser_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtModel.Text = this.fbd.SelectedPath;
            }
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            if (Owner is FormMain)
            {
                var form = Owner as FormMain;
                if (!form.isLicenseValid)
                {
                    tabs1.TabPages.Remove(tabGpio);
                    tabs1.TabPages.Remove(tabPic);
                    tabs1.TabPages.Remove(tpFolderSetting);
                }
            }

            // TODO:load
            var setting = store.GetSetting();
            txtCam.Text = setting.CamPath;
            txtModel.Text = setting.ModelPath;
            txtMatched.Text = setting.MatchedPath;
            var gpioType = 0;
            if (int.TryParse(setting.GPIOType, out gpioType))
            {
                slGpioType.SelectedIndex = gpioType;
            }

            txtImt.Text = setting.ImageMatchThreshold.ToString();
            txtMaxFileLimit.Text = setting.CAM_MAX_FILE_NUM.ToString();
            chkIsContoursMatch.Checked = setting.IsContoursMatch;
            txtGpioPorts.Text = setting.GpioPorts;
            txtCallDelay.Text = setting.GPIODelayTime.ToString();
            txtVarianceThreshold.Text = setting.VarianceThreshold.ToString();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SaveSetting();
            this.Close();
        }

        private void SaveSetting()
        {
            // TODO:save
            var setting = store.GetSetting();
            setting.CamPath = txtCam.Text;
            setting.ModelPath = txtModel.Text;
            setting.MatchedPath = txtMatched.Text;
            setting.GPIOType = slGpioType.SelectedIndex.ToString();

            double imt;
            if (double.TryParse(txtImt.Text, out imt))
            {
                setting.ImageMatchThreshold = imt;
            }

            int maxFileLimit;
            if (int.TryParse(txtMaxFileLimit.Text, out maxFileLimit))
            {
                setting.CAM_MAX_FILE_NUM = maxFileLimit;
            }
            else
            {
                setting.CAM_MAX_FILE_NUM = 100;
            }

            setting.IsContoursMatch = chkIsContoursMatch.Checked;

            setting.GpioPorts = txtGpioPorts.Text;

            int callDelay;
            if (int.TryParse(txtCallDelay.Text, out callDelay))
            {
                setting.GPIODelayTime = callDelay;
            }

            float varianceThreshold;
            if (float.TryParse(txtVarianceThreshold.Text, out varianceThreshold))
            {
                setting.VarianceThreshold = varianceThreshold;
            }


            store.SaveSetting(setting);
            constUtitly.MsgInfo("保存成功。");
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveSetting();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnMatchedBrowser_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtMatched.Text = this.fbd.SelectedPath;
            }
        }

        private void FormSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnGpioTest_Click(object sender, EventArgs e)
        {
            var gpioForm = new MainForm();
            gpioForm.ShowDialog();
        }

        private void btnRegist_Click(object sender, EventArgs e)
        {
            var str = LicenceHelper.CreateLicenseData();
            File.WriteAllText("license.txt", str);
            constUtitly.MsgInfo("生成授权申请文件成功。");
        }

        private void btnImportLicense_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var ret = LicenceHelper.CheckLicence(ofd.FileName);

                if (ret)
                {
                    File.Copy(ofd.FileName, "license.lic", true);
                    constUtitly.MsgInfo("导入成功。请重新打开软件使授权文件生效。");
                }

            }
        }
    }
}
