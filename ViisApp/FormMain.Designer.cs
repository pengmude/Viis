namespace ViisApp
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            windowBar1 = new AntdUI.WindowBar();
            panel1 = new AntdUI.Panel();
            btnMvs = new AntdUI.Button();
            btnSetting = new AntdUI.Button();
            btnPicScan = new AntdUI.Button();
            btnPicLearn = new AntdUI.Button();
            btnConnect = new AntdUI.Button();
            panel2 = new AntdUI.Panel();
            panelMain = new AntdUI.Panel();
            panelFunction = new AntdUI.Panel();
            button2 = new AntdUI.Button();
            button1 = new AntdUI.Button();
            btnStClear = new AntdUI.Button();
            chkDisConnect = new AntdUI.Checkbox();
            chkConnect = new AntdUI.Checkbox();
            chkScan = new AntdUI.Checkbox();
            chkLearn = new AntdUI.Checkbox();
            tabStatistics = new AntdUI.Table();
            statisticDataBindingSource = new BindingSource(components);
            timerStatus = new System.Windows.Forms.Timer(components);
            timerFilesMange = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panelFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)statisticDataBindingSource).BeginInit();
            SuspendLayout();
            // 
            // windowBar1
            // 
            resources.ApplyResources(windowBar1, "windowBar1");
            windowBar1.Icon = (Image)resources.GetObject("windowBar1.Icon");
            windowBar1.Name = "windowBar1";
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.BorderColor = SystemColors.ActiveBorder;
            panel1.BorderWidth = 1F;
            panel1.Controls.Add(btnMvs);
            panel1.Controls.Add(btnSetting);
            panel1.Controls.Add(btnPicScan);
            panel1.Controls.Add(btnPicLearn);
            panel1.Name = "panel1";
            panel1.Click += panel1_Click;
            // 
            // btnMvs
            // 
            resources.ApplyResources(btnMvs, "btnMvs");
            btnMvs.Name = "btnMvs";
            btnMvs.Type = AntdUI.TTypeMini.Warn;
            btnMvs.Click += btnMvs_Click;
            // 
            // btnSetting
            // 
            resources.ApplyResources(btnSetting, "btnSetting");
            btnSetting.Name = "btnSetting";
            btnSetting.Type = AntdUI.TTypeMini.Info;
            btnSetting.Click += btnSetting_Click;
            // 
            // btnPicScan
            // 
            resources.ApplyResources(btnPicScan, "btnPicScan");
            btnPicScan.Name = "btnPicScan";
            btnPicScan.Type = AntdUI.TTypeMini.Primary;
            btnPicScan.Click += btnPicScan_Click;
            // 
            // btnPicLearn
            // 
            resources.ApplyResources(btnPicLearn, "btnPicLearn");
            btnPicLearn.Name = "btnPicLearn";
            btnPicLearn.Type = AntdUI.TTypeMini.Primary;
            btnPicLearn.Click += btnPicLearn_Click;
            // 
            // btnConnect
            // 
            resources.ApplyResources(btnConnect, "btnConnect");
            btnConnect.Name = "btnConnect";
            btnConnect.Type = AntdUI.TTypeMini.Warn;
            btnConnect.Click += btnConnect_Click;
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.Controls.Add(panelMain);
            panel2.Controls.Add(panelFunction);
            panel2.Name = "panel2";
            // 
            // panelMain
            // 
            resources.ApplyResources(panelMain, "panelMain");
            panelMain.BackColor = SystemColors.Control;
            panelMain.BorderColor = SystemColors.ActiveBorder;
            panelMain.BorderWidth = 1F;
            panelMain.Name = "panelMain";
            // 
            // panelFunction
            // 
            resources.ApplyResources(panelFunction, "panelFunction");
            panelFunction.BackColor = SystemColors.Control;
            panelFunction.BorderColor = SystemColors.ActiveBorder;
            panelFunction.BorderWidth = 1F;
            panelFunction.Controls.Add(button2);
            panelFunction.Controls.Add(button1);
            panelFunction.Controls.Add(btnConnect);
            panelFunction.Controls.Add(btnStClear);
            panelFunction.Controls.Add(chkDisConnect);
            panelFunction.Controls.Add(chkConnect);
            panelFunction.Controls.Add(chkScan);
            panelFunction.Controls.Add(chkLearn);
            panelFunction.Controls.Add(tabStatistics);
            panelFunction.Name = "panelFunction";
            panelFunction.Click += panelFunction_Click;
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.Type = AntdUI.TTypeMini.Primary;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.Type = AntdUI.TTypeMini.Warn;
            button1.Click += button1_Click_1;
            // 
            // btnStClear
            // 
            resources.ApplyResources(btnStClear, "btnStClear");
            btnStClear.Name = "btnStClear";
            btnStClear.Type = AntdUI.TTypeMini.Warn;
            btnStClear.Click += btnStClear_Click;
            // 
            // chkDisConnect
            // 
            resources.ApplyResources(chkDisConnect, "chkDisConnect");
            chkDisConnect.Name = "chkDisConnect";
            chkDisConnect.Click += chkDisConnect_Click;
            // 
            // chkConnect
            // 
            resources.ApplyResources(chkConnect, "chkConnect");
            chkConnect.Name = "chkConnect";
            chkConnect.Click += chkConnect_Click;
            // 
            // chkScan
            // 
            resources.ApplyResources(chkScan, "chkScan");
            chkScan.Name = "chkScan";
            chkScan.Click += chkScan_Click;
            // 
            // chkLearn
            // 
            chkLearn.BackColor = SystemColors.Control;
            resources.ApplyResources(chkLearn, "chkLearn");
            chkLearn.Name = "chkLearn";
            chkLearn.Click += chkLearn_Click;
            // 
            // tabStatistics
            // 
            resources.ApplyResources(tabStatistics, "tabStatistics");
            tabStatistics.Gap = 5;
            tabStatistics.Name = "tabStatistics";
            // 
            // timerStatus
            // 
            timerStatus.Tick += timerStatus_Tick;
            // 
            // timerFilesMange
            // 
            timerFilesMange.Enabled = true;
            timerFilesMange.Interval = 1000;
            timerFilesMange.Tick += timerFilesMange_Tick;
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(windowBar1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormMain";
            FormClosed += FormMain_FormClosed;
            Load += FormMain_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panelFunction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)statisticDataBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.WindowBar windowBar1;
        private AntdUI.Panel panel1;
        private AntdUI.Button btnPicScan;
        private AntdUI.Button btnPicLearn;
        private AntdUI.Button btnConnect;
        private AntdUI.Panel panel2;
        private AntdUI.Panel panelMain;
        private AntdUI.Panel panelFunction;
        private AntdUI.Checkbox chkConnect;
        private AntdUI.Checkbox chkScan;
        private AntdUI.Checkbox chkLearn;
        private AntdUI.Button btnSetting;
        private System.Windows.Forms.Timer timerStatus;
        private BindingSource statisticDataBindingSource;
        private AntdUI.Button btnMvs;
        private AntdUI.Checkbox chkDisConnect;
        private AntdUI.Table tabStatistics;
        private AntdUI.Button btnStClear;
        private System.Windows.Forms.Timer timerFilesMange;
        private AntdUI.Button button2;
        private AntdUI.Button button1;
    }
}