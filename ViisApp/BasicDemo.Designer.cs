namespace BasicDemo
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            cbDeviceList = new ComboBox();
            pictureBox1 = new PictureBox();
            groupBox1 = new GroupBox();
            bnClose = new Button();
            bnOpen = new Button();
            bnEnum = new Button();
            groupBox2 = new GroupBox();
            bnTriggerExec = new Button();
            cbSoftTrigger = new CheckBox();
            bnStopGrab = new Button();
            bnStartGrab = new Button();
            bnTriggerMode = new RadioButton();
            bnContinuesMode = new RadioButton();
            groupBox3 = new GroupBox();
            bnSavePng = new Button();
            bnSaveTiff = new Button();
            bnSaveJpg = new Button();
            bnSaveBmp = new Button();
            groupBox4 = new GroupBox();
            bnSetParam = new Button();
            bnGetParam = new Button();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            tbFrameRate = new TextBox();
            tbGain = new TextBox();
            tbExposure = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // cbDeviceList
            // 
            resources.ApplyResources(cbDeviceList, "cbDeviceList");
            cbDeviceList.FormattingEnabled = true;
            cbDeviceList.Name = "cbDeviceList";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.BackColor = SystemColors.ControlDarkDark;
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(bnClose);
            groupBox1.Controls.Add(bnOpen);
            groupBox1.Controls.Add(bnEnum);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // bnClose
            // 
            resources.ApplyResources(bnClose, "bnClose");
            bnClose.Name = "bnClose";
            bnClose.UseVisualStyleBackColor = true;
            bnClose.Click += bnClose_Click;
            // 
            // bnOpen
            // 
            resources.ApplyResources(bnOpen, "bnOpen");
            bnOpen.Name = "bnOpen";
            bnOpen.UseVisualStyleBackColor = true;
            bnOpen.Click += bnOpen_Click;
            // 
            // bnEnum
            // 
            resources.ApplyResources(bnEnum, "bnEnum");
            bnEnum.Name = "bnEnum";
            bnEnum.UseVisualStyleBackColor = true;
            bnEnum.Click += bnEnum_Click;
            // 
            // groupBox2
            // 
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Controls.Add(bnTriggerExec);
            groupBox2.Controls.Add(cbSoftTrigger);
            groupBox2.Controls.Add(bnStopGrab);
            groupBox2.Controls.Add(bnStartGrab);
            groupBox2.Controls.Add(bnTriggerMode);
            groupBox2.Controls.Add(bnContinuesMode);
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // bnTriggerExec
            // 
            resources.ApplyResources(bnTriggerExec, "bnTriggerExec");
            bnTriggerExec.Name = "bnTriggerExec";
            bnTriggerExec.UseVisualStyleBackColor = true;
            bnTriggerExec.Click += bnTriggerExec_Click;
            // 
            // cbSoftTrigger
            // 
            resources.ApplyResources(cbSoftTrigger, "cbSoftTrigger");
            cbSoftTrigger.Name = "cbSoftTrigger";
            cbSoftTrigger.UseVisualStyleBackColor = true;
            cbSoftTrigger.CheckedChanged += cbSoftTrigger_CheckedChanged;
            // 
            // bnStopGrab
            // 
            resources.ApplyResources(bnStopGrab, "bnStopGrab");
            bnStopGrab.Name = "bnStopGrab";
            bnStopGrab.UseVisualStyleBackColor = true;
            bnStopGrab.Click += bnStopGrab_Click;
            // 
            // bnStartGrab
            // 
            resources.ApplyResources(bnStartGrab, "bnStartGrab");
            bnStartGrab.Name = "bnStartGrab";
            bnStartGrab.UseVisualStyleBackColor = true;
            bnStartGrab.Click += bnStartGrab_Click;
            // 
            // bnTriggerMode
            // 
            resources.ApplyResources(bnTriggerMode, "bnTriggerMode");
            bnTriggerMode.Name = "bnTriggerMode";
            bnTriggerMode.TabStop = true;
            bnTriggerMode.UseMnemonic = false;
            bnTriggerMode.UseVisualStyleBackColor = true;
            bnTriggerMode.CheckedChanged += bnTriggerMode_CheckedChanged;
            // 
            // bnContinuesMode
            // 
            resources.ApplyResources(bnContinuesMode, "bnContinuesMode");
            bnContinuesMode.Name = "bnContinuesMode";
            bnContinuesMode.TabStop = true;
            bnContinuesMode.UseVisualStyleBackColor = true;
            bnContinuesMode.CheckedChanged += bnContinuesMode_CheckedChanged;
            // 
            // groupBox3
            // 
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Controls.Add(bnSavePng);
            groupBox3.Controls.Add(bnSaveTiff);
            groupBox3.Controls.Add(bnSaveJpg);
            groupBox3.Controls.Add(bnSaveBmp);
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // bnSavePng
            // 
            resources.ApplyResources(bnSavePng, "bnSavePng");
            bnSavePng.Name = "bnSavePng";
            bnSavePng.UseVisualStyleBackColor = true;
            bnSavePng.Click += bnSavePng_Click;
            // 
            // bnSaveTiff
            // 
            resources.ApplyResources(bnSaveTiff, "bnSaveTiff");
            bnSaveTiff.Name = "bnSaveTiff";
            bnSaveTiff.UseVisualStyleBackColor = true;
            bnSaveTiff.Click += bnSaveTiff_Click;
            // 
            // bnSaveJpg
            // 
            resources.ApplyResources(bnSaveJpg, "bnSaveJpg");
            bnSaveJpg.Name = "bnSaveJpg";
            bnSaveJpg.UseVisualStyleBackColor = true;
            bnSaveJpg.Click += bnSaveJpg_Click;
            // 
            // bnSaveBmp
            // 
            resources.ApplyResources(bnSaveBmp, "bnSaveBmp");
            bnSaveBmp.Name = "bnSaveBmp";
            bnSaveBmp.UseVisualStyleBackColor = true;
            bnSaveBmp.Click += bnSaveBmp_Click;
            // 
            // groupBox4
            // 
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Controls.Add(bnSetParam);
            groupBox4.Controls.Add(bnGetParam);
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(label2);
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(tbFrameRate);
            groupBox4.Controls.Add(tbGain);
            groupBox4.Controls.Add(tbExposure);
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            // 
            // bnSetParam
            // 
            resources.ApplyResources(bnSetParam, "bnSetParam");
            bnSetParam.Name = "bnSetParam";
            bnSetParam.UseVisualStyleBackColor = true;
            bnSetParam.Click += bnSetParam_Click;
            // 
            // bnGetParam
            // 
            resources.ApplyResources(bnGetParam, "bnGetParam");
            bnGetParam.Name = "bnGetParam";
            bnGetParam.UseVisualStyleBackColor = true;
            bnGetParam.Click += bnGetParam_Click;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // tbFrameRate
            // 
            resources.ApplyResources(tbFrameRate, "tbFrameRate");
            tbFrameRate.Name = "tbFrameRate";
            // 
            // tbGain
            // 
            resources.ApplyResources(tbGain, "tbGain");
            tbGain.Name = "tbGain";
            // 
            // tbExposure
            // 
            resources.ApplyResources(tbExposure, "tbExposure");
            tbExposure.Name = "tbExposure";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            Controls.Add(cbDeviceList);
            MaximizeBox = false;
            Name = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox cbDeviceList;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bnClose;
        private System.Windows.Forms.Button bnOpen;
        private System.Windows.Forms.Button bnEnum;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton bnTriggerMode;
        private System.Windows.Forms.RadioButton bnContinuesMode;
        private System.Windows.Forms.CheckBox cbSoftTrigger;
        private System.Windows.Forms.Button bnStopGrab;
        private System.Windows.Forms.Button bnStartGrab;
        private System.Windows.Forms.Button bnTriggerExec;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button bnSaveJpg;
        private System.Windows.Forms.Button bnSaveBmp;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbFrameRate;
        private System.Windows.Forms.TextBox tbGain;
        private System.Windows.Forms.TextBox tbExposure;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bnSetParam;
        private System.Windows.Forms.Button bnGetParam;
        private System.Windows.Forms.Button bnSavePng;
        private System.Windows.Forms.Button bnSaveTiff;
    }
}

