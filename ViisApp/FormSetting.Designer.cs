namespace ViisApp
{
    partial class FormSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabs1 = new AntdUI.Tabs();
            tpFolderSetting = new TabPage();
            txtMaxFileLimit = new AntdUI.Input();
            label7 = new AntdUI.Label();
            btnMatchedBrowser = new AntdUI.Button();
            txtMatched = new AntdUI.Input();
            label4 = new AntdUI.Label();
            btnModelBrowser = new AntdUI.Button();
            txtModel = new AntdUI.Input();
            label2 = new AntdUI.Label();
            btnCamBrowser = new AntdUI.Button();
            txtCam = new AntdUI.Input();
            label1 = new AntdUI.Label();
            tabPic = new TabPage();
            txtVarianceThreshold = new AntdUI.Input();
            label10 = new AntdUI.Label();
            chkIsContoursMatch = new AntdUI.Checkbox();
            txtImt = new AntdUI.Input();
            label6 = new AntdUI.Label();
            tabGpio = new TabPage();
            btnGpioTest = new AntdUI.Button();
            txtCallDelay = new AntdUI.Input();
            label9 = new AntdUI.Label();
            txtGpioPorts = new AntdUI.Input();
            label8 = new AntdUI.Label();
            slGpioType = new AntdUI.Select();
            label5 = new AntdUI.Label();
            tabPage1 = new TabPage();
            btnImportLicense = new AntdUI.Button();
            btnRegist = new AntdUI.Button();
            btnConfirm = new AntdUI.Button();
            btnApply = new AntdUI.Button();
            btnCancel = new AntdUI.Button();
            fbd = new FolderBrowserDialog();
            ofd = new OpenFileDialog();
            tabs1.SuspendLayout();
            tpFolderSetting.SuspendLayout();
            tabPic.SuspendLayout();
            tabGpio.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // tabs1
            // 
            tabs1.Controls.Add(tpFolderSetting);
            tabs1.Controls.Add(tabPic);
            tabs1.Controls.Add(tabGpio);
            tabs1.Controls.Add(tabPage1);
            tabs1.Location = new Point(12, 12);
            tabs1.Name = "tabs1";
            tabs1.SelectedIndex = 0;
            tabs1.Size = new Size(260, 458);
            tabs1.TabIndex = 0;
            // 
            // tpFolderSetting
            // 
            tpFolderSetting.Controls.Add(txtMaxFileLimit);
            tpFolderSetting.Controls.Add(label7);
            tpFolderSetting.Controls.Add(btnMatchedBrowser);
            tpFolderSetting.Controls.Add(txtMatched);
            tpFolderSetting.Controls.Add(label4);
            tpFolderSetting.Controls.Add(btnModelBrowser);
            tpFolderSetting.Controls.Add(txtModel);
            tpFolderSetting.Controls.Add(label2);
            tpFolderSetting.Controls.Add(btnCamBrowser);
            tpFolderSetting.Controls.Add(txtCam);
            tpFolderSetting.Controls.Add(label1);
            tpFolderSetting.Location = new Point(4, 26);
            tpFolderSetting.Name = "tpFolderSetting";
            tpFolderSetting.Padding = new Padding(3);
            tpFolderSetting.Size = new Size(252, 428);
            tpFolderSetting.TabIndex = 0;
            tpFolderSetting.Text = "文件";
            tpFolderSetting.UseVisualStyleBackColor = true;
            // 
            // txtMaxFileLimit
            // 
            txtMaxFileLimit.Location = new Point(6, 275);
            txtMaxFileLimit.Name = "txtMaxFileLimit";
            txtMaxFileLimit.Size = new Size(236, 38);
            txtMaxFileLimit.TabIndex = 10;
            // 
            // label7
            // 
            label7.Location = new Point(6, 246);
            label7.Name = "label7";
            label7.Size = new Size(89, 23);
            label7.TabIndex = 9;
            label7.Text = "照片文件限制";
            // 
            // btnMatchedBrowser
            // 
            btnMatchedBrowser.Location = new Point(204, 202);
            btnMatchedBrowser.Name = "btnMatchedBrowser";
            btnMatchedBrowser.Size = new Size(38, 38);
            btnMatchedBrowser.TabIndex = 8;
            btnMatchedBrowser.Text = "...";
            btnMatchedBrowser.Type = AntdUI.TTypeMini.Info;
            btnMatchedBrowser.Click += btnMatchedBrowser_Click;
            // 
            // txtMatched
            // 
            txtMatched.Location = new Point(6, 202);
            txtMatched.Name = "txtMatched";
            txtMatched.ReadOnly = true;
            txtMatched.Size = new Size(201, 38);
            txtMatched.TabIndex = 7;
            // 
            // label4
            // 
            label4.Location = new Point(6, 173);
            label4.Name = "label4";
            label4.Size = new Size(89, 23);
            label4.TabIndex = 6;
            label4.Text = "匹配结果目录";
            // 
            // btnModelBrowser
            // 
            btnModelBrowser.Location = new Point(204, 114);
            btnModelBrowser.Name = "btnModelBrowser";
            btnModelBrowser.Size = new Size(38, 38);
            btnModelBrowser.TabIndex = 5;
            btnModelBrowser.Text = "...";
            btnModelBrowser.Type = AntdUI.TTypeMini.Info;
            btnModelBrowser.Click += btnModelBrowser_Click;
            // 
            // txtModel
            // 
            txtModel.Location = new Point(6, 114);
            txtModel.Name = "txtModel";
            txtModel.ReadOnly = true;
            txtModel.Size = new Size(201, 38);
            txtModel.TabIndex = 4;
            // 
            // label2
            // 
            label2.Location = new Point(6, 85);
            label2.Name = "label2";
            label2.Size = new Size(89, 23);
            label2.TabIndex = 3;
            label2.Text = "模型保存目录";
            // 
            // btnCamBrowser
            // 
            btnCamBrowser.Location = new Point(204, 35);
            btnCamBrowser.Name = "btnCamBrowser";
            btnCamBrowser.Size = new Size(38, 38);
            btnCamBrowser.TabIndex = 2;
            btnCamBrowser.Text = "...";
            btnCamBrowser.Type = AntdUI.TTypeMini.Info;
            btnCamBrowser.Click += btnCamBrowser_Click;
            // 
            // txtCam
            // 
            txtCam.Location = new Point(6, 35);
            txtCam.Name = "txtCam";
            txtCam.ReadOnly = true;
            txtCam.Size = new Size(201, 38);
            txtCam.TabIndex = 1;
            // 
            // label1
            // 
            label1.Location = new Point(6, 6);
            label1.Name = "label1";
            label1.Size = new Size(75, 23);
            label1.TabIndex = 0;
            label1.Text = "照片目录";
            // 
            // tabPic
            // 
            tabPic.Controls.Add(txtVarianceThreshold);
            tabPic.Controls.Add(label10);
            tabPic.Controls.Add(chkIsContoursMatch);
            tabPic.Controls.Add(txtImt);
            tabPic.Controls.Add(label6);
            tabPic.Location = new Point(4, 26);
            tabPic.Name = "tabPic";
            tabPic.Padding = new Padding(3);
            tabPic.Size = new Size(252, 428);
            tabPic.TabIndex = 2;
            tabPic.Text = "图像";
            tabPic.UseVisualStyleBackColor = true;
            // 
            // txtVarianceThreshold
            // 
            txtVarianceThreshold.Location = new Point(6, 148);
            txtVarianceThreshold.Name = "txtVarianceThreshold";
            txtVarianceThreshold.Size = new Size(240, 38);
            txtVarianceThreshold.TabIndex = 7;
            txtVarianceThreshold.TextAlign = HorizontalAlignment.Right;
            // 
            // label10
            // 
            label10.Location = new Point(6, 119);
            label10.Name = "label10";
            label10.Size = new Size(75, 23);
            label10.TabIndex = 6;
            label10.Text = "空白页阈值";
            // 
            // chkIsContoursMatch
            // 
            chkIsContoursMatch.Location = new Point(6, 90);
            chkIsContoursMatch.Name = "chkIsContoursMatch";
            chkIsContoursMatch.Size = new Size(136, 23);
            chkIsContoursMatch.TabIndex = 5;
            chkIsContoursMatch.Text = "启动轮廓图对比";
            // 
            // txtImt
            // 
            txtImt.Location = new Point(6, 35);
            txtImt.Name = "txtImt";
            txtImt.Size = new Size(240, 38);
            txtImt.TabIndex = 4;
            txtImt.TextAlign = HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.Location = new Point(6, 6);
            label6.Name = "label6";
            label6.Size = new Size(75, 23);
            label6.TabIndex = 3;
            label6.Text = "相识度阈值";
            // 
            // tabGpio
            // 
            tabGpio.Controls.Add(btnGpioTest);
            tabGpio.Controls.Add(txtCallDelay);
            tabGpio.Controls.Add(label9);
            tabGpio.Controls.Add(txtGpioPorts);
            tabGpio.Controls.Add(label8);
            tabGpio.Controls.Add(slGpioType);
            tabGpio.Controls.Add(label5);
            tabGpio.Location = new Point(4, 26);
            tabGpio.Name = "tabGpio";
            tabGpio.Padding = new Padding(3);
            tabGpio.Size = new Size(252, 428);
            tabGpio.TabIndex = 3;
            tabGpio.Text = "GPIO";
            tabGpio.UseVisualStyleBackColor = true;
            // 
            // btnGpioTest
            // 
            btnGpioTest.Location = new Point(162, 225);
            btnGpioTest.Name = "btnGpioTest";
            btnGpioTest.Size = new Size(80, 38);
            btnGpioTest.TabIndex = 13;
            btnGpioTest.Text = "测试Gpio";
            btnGpioTest.Type = AntdUI.TTypeMini.Warn;
            btnGpioTest.Click += btnGpioTest_Click;
            // 
            // txtCallDelay
            // 
            txtCallDelay.Location = new Point(6, 181);
            txtCallDelay.Name = "txtCallDelay";
            txtCallDelay.Size = new Size(236, 38);
            txtCallDelay.TabIndex = 12;
            // 
            // label9
            // 
            label9.Location = new Point(6, 152);
            label9.Name = "label9";
            label9.Size = new Size(240, 23);
            label9.TabIndex = 11;
            label9.Text = "调用间隔";
            // 
            // txtGpioPorts
            // 
            txtGpioPorts.Location = new Point(6, 108);
            txtGpioPorts.Name = "txtGpioPorts";
            txtGpioPorts.Size = new Size(240, 38);
            txtGpioPorts.TabIndex = 10;
            // 
            // label8
            // 
            label8.Location = new Point(6, 79);
            label8.Name = "label8";
            label8.Size = new Size(240, 23);
            label8.TabIndex = 9;
            label8.Text = "GPIO针脚序号（多序号以半角逗号分隔）";
            // 
            // slGpioType
            // 
            slGpioType.Items.AddRange(new object[] { "Intel 6代以上", "Intel 1~5代", "SIO GPIO" });
            slGpioType.Location = new Point(6, 35);
            slGpioType.Name = "slGpioType";
            slGpioType.SelectedIndex = 0;
            slGpioType.SelectedValue = "Intel 6代以上";
            slGpioType.Size = new Size(240, 38);
            slGpioType.TabIndex = 8;
            slGpioType.Text = "Intel 6代以上";
            // 
            // label5
            // 
            label5.Location = new Point(6, 6);
            label5.Name = "label5";
            label5.Size = new Size(75, 23);
            label5.TabIndex = 7;
            label5.Text = "GPIO类型";
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnImportLicense);
            tabPage1.Controls.Add(btnRegist);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(252, 428);
            tabPage1.TabIndex = 4;
            tabPage1.Text = "软件注册";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnImportLicense
            // 
            btnImportLicense.Location = new Point(70, 93);
            btnImportLicense.Name = "btnImportLicense";
            btnImportLicense.Size = new Size(96, 23);
            btnImportLicense.TabIndex = 1;
            btnImportLicense.Text = "导入授权文件";
            btnImportLicense.Click += btnImportLicense_Click;
            // 
            // btnRegist
            // 
            btnRegist.Location = new Point(70, 64);
            btnRegist.Name = "btnRegist";
            btnRegist.Size = new Size(96, 23);
            btnRegist.TabIndex = 0;
            btnRegist.Text = "生成注册文件";
            btnRegist.Click += btnRegist_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(16, 472);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(80, 38);
            btnConfirm.TabIndex = 3;
            btnConfirm.Text = "确定";
            btnConfirm.Type = AntdUI.TTypeMini.Primary;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnApply
            // 
            btnApply.Location = new Point(102, 472);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(80, 38);
            btnApply.TabIndex = 4;
            btnApply.Text = "应用";
            btnApply.Type = AntdUI.TTypeMini.Primary;
            btnApply.Click += btnApply_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(188, 472);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 38);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "取消";
            btnCancel.Click += btnCancel_Click;
            // 
            // ofd
            // 
            ofd.FileName = "openFileDialog1";
            // 
            // FormSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 527);
            Controls.Add(btnCancel);
            Controls.Add(btnApply);
            Controls.Add(btnConfirm);
            Controls.Add(tabs1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FormSetting";
            Text = "设置";
            FormClosing += FormSetting_FormClosing;
            Load += FormSetting_Load;
            tabs1.ResumeLayout(false);
            tpFolderSetting.ResumeLayout(false);
            tabPic.ResumeLayout(false);
            tabGpio.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Tabs tabs1;
        private TabPage tpFolderSetting;
        private AntdUI.Button btnModelBrowser;
        private AntdUI.Input txtModel;
        private AntdUI.Label label2;
        private AntdUI.Button btnCamBrowser;
        private AntdUI.Input txtCam;
        private AntdUI.Label label1;
        private AntdUI.Button btnConfirm;
        private AntdUI.Button btnApply;
        private AntdUI.Button btnCancel;
        private FolderBrowserDialog fbd;
        private AntdUI.Button btnMatchedBrowser;
        private AntdUI.Input txtMatched;
        private AntdUI.Label label4;
        private TabPage tabPic;
        private AntdUI.Input txtImt;
        private AntdUI.Label label6;
        private AntdUI.Input txtMaxFileLimit;
        private AntdUI.Label label7;
        private AntdUI.Checkbox chkIsContoursMatch;
        private TabPage tabGpio;
        private AntdUI.Select slGpioType;
        private AntdUI.Label label5;
        private AntdUI.Input txtCallDelay;
        private AntdUI.Label label9;
        private AntdUI.Input txtGpioPorts;
        private AntdUI.Label label8;
        private AntdUI.Input txtVarianceThreshold;
        private AntdUI.Label label10;
        private AntdUI.Button btnGpioTest;
        private TabPage tabPage1;
        private AntdUI.Button btnRegist;
        private AntdUI.Button btnImportLicense;
        private OpenFileDialog ofd;
    }
}