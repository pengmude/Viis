namespace ViisApp.Controls
{
    partial class EmFrame
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
            ImageFile = new PictureBox();
            lblIndex = new AntdUI.Label();
            btnStart = new AntdUI.Button();
            btnClose = new AntdUI.Button();
            cmbTrigger = new AntdUI.Checkbox();
            btnSkip = new AntdUI.Button();
            btnResetIndex = new AntdUI.Button();
            panel1 = new AntdUI.Panel();
            BtnModel = new AntdUI.Button();
            panelThumbnail = new Panel();
            splitter = new Splitter();
            panel3 = new Panel();
            lblResult = new AntdUI.Label();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ImageFile).BeginInit();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { tsStatus });
            statusStrip.Location = new Point(0, 260);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(783, 22);
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
            // ImageFile
            // 
            ImageFile.Dock = DockStyle.Fill;
            ImageFile.Location = new Point(0, 0);
            ImageFile.Name = "ImageFile";
            ImageFile.Size = new Size(783, 135);
            ImageFile.SizeMode = PictureBoxSizeMode.Zoom;
            ImageFile.TabIndex = 1;
            ImageFile.TabStop = false;
            ImageFile.Click += ImageFile_Click;
            // 
            // lblIndex
            // 
            lblIndex.BackColor = Color.Transparent;
            lblIndex.Font = new Font("Microsoft YaHei UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblIndex.ForeColor = Color.Blue;
            lblIndex.Location = new Point(3, 3);
            lblIndex.Name = "lblIndex";
            lblIndex.Size = new Size(131, 46);
            lblIndex.TabIndex = 2;
            lblIndex.Text = "0";
            lblIndex.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(194, 10);
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
            btnClose.Location = new Point(801, 8);
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
            cmbTrigger.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cmbTrigger.Location = new Point(575, 10);
            cmbTrigger.Name = "cmbTrigger";
            cmbTrigger.Size = new Size(124, 33);
            cmbTrigger.TabIndex = 6;
            cmbTrigger.Text = "连续拍照";
            cmbTrigger.CheckedChanged += cmbTrigger_CheckedChanged;
            // 
            // btnSkip
            // 
            btnSkip.Font = new Font("Microsoft YaHei UI", 14.25F);
            btnSkip.Location = new Point(285, 3);
            btnSkip.Name = "btnSkip";
            btnSkip.Size = new Size(139, 45);
            btnSkip.TabIndex = 7;
            btnSkip.Text = "跳过";
            btnSkip.Type = AntdUI.TTypeMini.Primary;
            btnSkip.Click += btnSkip_Click;
            // 
            // btnResetIndex
            // 
            btnResetIndex.Font = new Font("Microsoft YaHei UI", 14.25F);
            btnResetIndex.Location = new Point(430, 3);
            btnResetIndex.Name = "btnResetIndex";
            btnResetIndex.Size = new Size(139, 45);
            btnResetIndex.TabIndex = 8;
            btnResetIndex.Text = "重置计数";
            btnResetIndex.Type = AntdUI.TTypeMini.Warn;
            btnResetIndex.Click += btnResetIndex_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(BtnModel);
            panel1.Controls.Add(lblIndex);
            panel1.Controls.Add(btnResetIndex);
            panel1.Controls.Add(btnStart);
            panel1.Controls.Add(btnSkip);
            panel1.Controls.Add(cmbTrigger);
            panel1.Controls.Add(btnClose);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(783, 49);
            panel1.TabIndex = 9;
            panel1.Text = "panel1";
            // 
            // BtnModel
            // 
            BtnModel.Font = new Font("Microsoft YaHei UI", 14.25F);
            BtnModel.Location = new Point(140, 3);
            BtnModel.Name = "BtnModel";
            BtnModel.Size = new Size(139, 45);
            BtnModel.TabIndex = 13;
            BtnModel.Text = "设置模板";
            BtnModel.Type = AntdUI.TTypeMini.Success;
            BtnModel.Click += BtnModel_Click;
            // 
            // panelThumbnail
            // 
            panelThumbnail.Dock = DockStyle.Top;
            panelThumbnail.Location = new Point(0, 49);
            panelThumbnail.Name = "panelThumbnail";
            panelThumbnail.Size = new Size(783, 71);
            panelThumbnail.TabIndex = 10;
            // 
            // splitter
            // 
            splitter.BorderStyle = BorderStyle.FixedSingle;
            splitter.Dock = DockStyle.Top;
            splitter.Location = new Point(0, 120);
            splitter.Name = "splitter";
            splitter.Size = new Size(783, 5);
            splitter.TabIndex = 11;
            splitter.TabStop = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(lblResult);
            panel3.Controls.Add(ImageFile);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 125);
            panel3.Name = "panel3";
            panel3.Size = new Size(783, 135);
            panel3.TabIndex = 12;
            // 
            // lblResult
            // 
            lblResult.Dock = DockStyle.Top;
            lblResult.Font = new Font("Microsoft YaHei UI", 72F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblResult.Location = new Point(0, 0);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(783, 80);
            lblResult.TabIndex = 2;
            lblResult.Text = "";
            lblResult.TextAlign = ContentAlignment.MiddleCenter;
            lblResult.Visible = false;
            // 
            // EmFrame
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(panel3);
            Controls.Add(splitter);
            Controls.Add(panelThumbnail);
            Controls.Add(panel1);
            Controls.Add(statusStrip);
            Name = "EmFrame";
            Size = new Size(783, 282);
            Load += EmFrame_Load;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ImageFile).EndInit();
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip;
        private ToolStripStatusLabel tsStatus;
        private System.Windows.Forms.Timer fileWaterTimer;
        private PictureBox ImageFile;
        private AntdUI.Label lblIndex;
        private AntdUI.Button btnStart;
        private AntdUI.Button btnClose;
        private AntdUI.Checkbox cmbTrigger;
        private AntdUI.Button btnSkip;
        private AntdUI.Button btnResetIndex;
        private AntdUI.Panel panel1;
        private Panel panelThumbnail;
        private Splitter splitter;
        private Panel panel3;
        private AntdUI.Label lblResult;
        private AntdUI.Button BtnModel;
    }
}
