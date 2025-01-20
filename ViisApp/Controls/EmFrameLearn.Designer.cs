namespace ViisApp.Controls
{
    partial class EmFrameLearn
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            statusStrip = new StatusStrip();
            tsStatus = new ToolStripStatusLabel();
            fileWaterTimer = new System.Windows.Forms.Timer(components);
            btnStart = new AntdUI.Button();
            btnClose = new AntdUI.Button();
            cmbTrigger = new AntdUI.Checkbox();
            btnSave = new AntdUI.Button();
            WatchPicFileTimer = new System.Windows.Forms.Timer(components);
            panelPic = new Panel();
            btnClearFolder = new AntdUI.Button();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { tsStatus });
            statusStrip.Location = new Point(0, 1578);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1900, 22);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip1";
            // 
            // tsStatus
            // 
            tsStatus.Name = "tsStatus";
            tsStatus.Size = new Size(56, 17);
            tsStatus.Text = "检测样本";
            // 
            // fileWaterTimer
            // 
            fileWaterTimer.Tick += fileWaterTimer_Tick;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(60, 3);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(79, 33);
            btnStart.TabIndex = 3;
            btnStart.Text = "启动程序";
            btnStart.Visible = false;
            btnStart.Click += btnStart_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Location = new Point(1862, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(33, 33);
            btnClose.TabIndex = 5;
            btnClose.Text = "X";
            btnClose.Type = AntdUI.TTypeMini.Error;
            btnClose.Click += btnClose_Click;
            // 
            // cmbTrigger
            // 
            cmbTrigger.Checked = true;
            cmbTrigger.Font = new Font("Microsoft YaHei UI", 14.25F);
            cmbTrigger.Location = new Point(290, 11);
            cmbTrigger.Name = "cmbTrigger";
            cmbTrigger.Size = new Size(124, 33);
            cmbTrigger.TabIndex = 6;
            cmbTrigger.Text = "连续拍照";
            cmbTrigger.CheckedChanged += cmbTrigger_CheckedChanged;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Microsoft YaHei UI", 14.25F);
            btnSave.Location = new Point(3, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(139, 45);
            btnSave.TabIndex = 7;
            btnSave.Text = "开始检测";
            btnSave.Type = AntdUI.TTypeMini.Primary;
            btnSave.Click += btnSave_Click;
            // 
            // WatchPicFileTimer
            // 
            WatchPicFileTimer.Enabled = true;
            WatchPicFileTimer.Interval = 500;
            WatchPicFileTimer.Tick += WatchPicFileTimer_Tick;
            // 
            // panelPic
            // 
            panelPic.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelPic.AutoScroll = true;
            panelPic.BorderStyle = BorderStyle.FixedSingle;
            panelPic.Location = new Point(3, 50);
            panelPic.Name = "panelPic";
            panelPic.Size = new Size(1892, 1525);
            panelPic.TabIndex = 8;
            // 
            // btnClearFolder
            // 
            btnClearFolder.Font = new Font("Microsoft YaHei UI", 14.25F);
            btnClearFolder.Location = new Point(145, 3);
            btnClearFolder.Name = "btnClearFolder";
            btnClearFolder.Size = new Size(139, 45);
            btnClearFolder.TabIndex = 9;
            btnClearFolder.Text = "清空目录";
            btnClearFolder.Type = AntdUI.TTypeMini.Warn;
            btnClearFolder.Click += btnClearFolder_Click;
            // 
            // EmFrameLearn
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(btnClearFolder);
            Controls.Add(panelPic);
            Controls.Add(btnSave);
            Controls.Add(cmbTrigger);
            Controls.Add(btnClose);
            Controls.Add(btnStart);
            Controls.Add(statusStrip);
            Name = "EmFrameLearn";
            Size = new Size(1900, 1600);
            Load += EmFrameLearn_Load;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip;
        private ToolStripStatusLabel tsStatus;
        private System.Windows.Forms.Timer fileWaterTimer;
        private AntdUI.Button btnStart;
        private AntdUI.Button btnClose;
        private AntdUI.Checkbox cmbTrigger;
        private AntdUI.Button btnSave;
        private System.Windows.Forms.Timer WatchPicFileTimer;
        private Panel panelPic;
        private AntdUI.Button btnClearFolder;
    }
}
