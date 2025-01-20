
namespace GpioCheck
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        private void DynamicCreateForm ()
        {

        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.ToolTitleLabel = new System.Windows.Forms.Label();
            this.PromptInfo = new System.Windows.Forms.RichTextBox();
            this.GPIO50_UPDATE = new System.Windows.Forms.Button();
            this.GPIO49_UPDATE = new System.Windows.Forms.Button();
            this.GPIO48_UPDATE = new System.Windows.Forms.Button();
            this.GPIO33_UPDATE = new System.Windows.Forms.Button();
            this.GPIO50_BOX = new System.Windows.Forms.GroupBox();
            this.GPIO50_OUT = new System.Windows.Forms.CheckBox();
            this.GPIO50_DIR = new System.Windows.Forms.CheckBox();
            this.GPIO50_STATE = new System.Windows.Forms.RadioButton();
            this.GPIO50 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GPIO49_OUT = new System.Windows.Forms.CheckBox();
            this.GPIO49_DIR = new System.Windows.Forms.CheckBox();
            this.GPIO49_STATE = new System.Windows.Forms.RadioButton();
            this.GPIO49 = new System.Windows.Forms.Label();
            this.GPIO48_BOX = new System.Windows.Forms.GroupBox();
            this.GPIO48_OUT = new System.Windows.Forms.CheckBox();
            this.GPIO48_DIR = new System.Windows.Forms.CheckBox();
            this.GPIO48_STATE = new System.Windows.Forms.RadioButton();
            this.GPIO48 = new System.Windows.Forms.Label();
            this.GPIO33_BOX = new System.Windows.Forms.GroupBox();
            this.GPIO33_OUT = new System.Windows.Forms.CheckBox();
            this.GPIO33_DIR = new System.Windows.Forms.CheckBox();
            this.GPIO33_STATE = new System.Windows.Forms.RadioButton();
            this.GPIO33 = new System.Windows.Forms.Label();
            this.GPIO_STATE = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GPIO_DIR = new System.Windows.Forms.Label();
            this.GPIO_NAME = new System.Windows.Forms.Label();
            this.TitleMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.File_ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupBox.SuspendLayout();
            this.GPIO50_BOX.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GPIO48_BOX.SuspendLayout();
            this.GPIO33_BOX.SuspendLayout();
            this.TitleMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox
            // 
            this.GroupBox.Controls.Add(this.ToolTitleLabel);
            this.GroupBox.Controls.Add(this.PromptInfo);
            this.GroupBox.Controls.Add(this.GPIO50_UPDATE);
            this.GroupBox.Controls.Add(this.GPIO49_UPDATE);
            this.GroupBox.Controls.Add(this.GPIO48_UPDATE);
            this.GroupBox.Controls.Add(this.GPIO33_UPDATE);
            this.GroupBox.Controls.Add(this.GPIO50_BOX);
            this.GroupBox.Controls.Add(this.groupBox1);
            this.GroupBox.Controls.Add(this.GPIO48_BOX);
            this.GroupBox.Controls.Add(this.GPIO33_BOX);
            this.GroupBox.Controls.Add(this.GPIO_STATE);
            this.GroupBox.Controls.Add(this.label1);
            this.GroupBox.Controls.Add(this.GPIO_DIR);
            this.GroupBox.Controls.Add(this.GPIO_NAME);
            this.GroupBox.Location = new System.Drawing.Point(12, 96);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Size = new System.Drawing.Size(1637, 1076);
            this.GroupBox.TabIndex = 0;
            this.GroupBox.TabStop = false;
            this.GroupBox.Enter += new System.EventHandler(this.GroupBox_Enter);
            // 
            // ToolTitleLabel
            // 
            this.ToolTitleLabel.AutoSize = true;
            this.ToolTitleLabel.Font = new System.Drawing.Font("宋体", 14.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ToolTitleLabel.Location = new System.Drawing.Point(597, 58);
            this.ToolTitleLabel.Name = "ToolTitleLabel";
            this.ToolTitleLabel.Size = new System.Drawing.Size(380, 48);
            this.ToolTitleLabel.TabIndex = 40;
            this.ToolTitleLabel.Text = "GPIO Check Tool";
            // 
            // PromptInfo
            // 
            this.PromptInfo.Location = new System.Drawing.Point(183, 751);
            this.PromptInfo.Name = "PromptInfo";
            this.PromptInfo.ReadOnly = true;
            this.PromptInfo.Size = new System.Drawing.Size(1178, 169);
            this.PromptInfo.TabIndex = 39;
            this.PromptInfo.Text = resources.GetString("PromptInfo.Text");
            this.PromptInfo.TextChanged += new System.EventHandler(this.PromptInfo_TextChanged);
            // 
            // GPIO50_UPDATE
            // 
            this.GPIO50_UPDATE.Location = new System.Drawing.Point(1178, 567);
            this.GPIO50_UPDATE.Name = "GPIO50_UPDATE";
            this.GPIO50_UPDATE.Size = new System.Drawing.Size(183, 75);
            this.GPIO50_UPDATE.TabIndex = 38;
            this.GPIO50_UPDATE.Text = "更新状态";
            this.GPIO50_UPDATE.UseVisualStyleBackColor = true;
            this.GPIO50_UPDATE.Click += new System.EventHandler(this.GPIO50_UPDATE_Click);
            // 
            // GPIO49_UPDATE
            // 
            this.GPIO49_UPDATE.Location = new System.Drawing.Point(1178, 461);
            this.GPIO49_UPDATE.Name = "GPIO49_UPDATE";
            this.GPIO49_UPDATE.Size = new System.Drawing.Size(183, 75);
            this.GPIO49_UPDATE.TabIndex = 38;
            this.GPIO49_UPDATE.Text = "更新状态";
            this.GPIO49_UPDATE.UseVisualStyleBackColor = true;
            this.GPIO49_UPDATE.Click += new System.EventHandler(this.GPIO49_UPDATE_Click);
            // 
            // GPIO48_UPDATE
            // 
            this.GPIO48_UPDATE.Location = new System.Drawing.Point(1178, 357);
            this.GPIO48_UPDATE.Name = "GPIO48_UPDATE";
            this.GPIO48_UPDATE.Size = new System.Drawing.Size(183, 75);
            this.GPIO48_UPDATE.TabIndex = 38;
            this.GPIO48_UPDATE.Text = "更新状态";
            this.GPIO48_UPDATE.UseVisualStyleBackColor = true;
            this.GPIO48_UPDATE.Click += new System.EventHandler(this.GPIO48_UPDATE_Click);
            // 
            // GPIO33_UPDATE
            // 
            this.GPIO33_UPDATE.Location = new System.Drawing.Point(1178, 248);
            this.GPIO33_UPDATE.Name = "GPIO33_UPDATE";
            this.GPIO33_UPDATE.Size = new System.Drawing.Size(183, 75);
            this.GPIO33_UPDATE.TabIndex = 38;
            this.GPIO33_UPDATE.Text = "更新状态";
            this.GPIO33_UPDATE.UseVisualStyleBackColor = true;
            this.GPIO33_UPDATE.Click += new System.EventHandler(this.GPIO33_UPDATE_Click);
            // 
            // GPIO50_BOX
            // 
            this.GPIO50_BOX.Controls.Add(this.GPIO50_OUT);
            this.GPIO50_BOX.Controls.Add(this.GPIO50_DIR);
            this.GPIO50_BOX.Controls.Add(this.GPIO50_STATE);
            this.GPIO50_BOX.Controls.Add(this.GPIO50);
            this.GPIO50_BOX.Location = new System.Drawing.Point(183, 544);
            this.GPIO50_BOX.Name = "GPIO50_BOX";
            this.GPIO50_BOX.Size = new System.Drawing.Size(1207, 107);
            this.GPIO50_BOX.TabIndex = 36;
            this.GPIO50_BOX.TabStop = false;
            // 
            // GPIO50_OUT
            // 
            this.GPIO50_OUT.AutoSize = true;
            this.GPIO50_OUT.Location = new System.Drawing.Point(504, 41);
            this.GPIO50_OUT.Name = "GPIO50_OUT";
            this.GPIO50_OUT.Size = new System.Drawing.Size(96, 34);
            this.GPIO50_OUT.TabIndex = 36;
            this.GPIO50_OUT.Text = "Low";
            this.GPIO50_OUT.UseVisualStyleBackColor = true;
            this.GPIO50_OUT.CheckedChanged += new System.EventHandler(this.GPIO50_OUT_CheckedChanged);
            // 
            // GPIO50_DIR
            // 
            this.GPIO50_DIR.AutoSize = true;
            this.GPIO50_DIR.Location = new System.Drawing.Point(249, 41);
            this.GPIO50_DIR.Name = "GPIO50_DIR";
            this.GPIO50_DIR.Size = new System.Drawing.Size(126, 34);
            this.GPIO50_DIR.TabIndex = 35;
            this.GPIO50_DIR.Text = "Input";
            this.GPIO50_DIR.UseVisualStyleBackColor = true;
            this.GPIO50_DIR.CheckedChanged += new System.EventHandler(this.GPIO50_DIR_CheckedChanged);
            // 
            // GPIO50_STATE
            // 
            this.GPIO50_STATE.AutoSize = true;
            this.GPIO50_STATE.Location = new System.Drawing.Point(771, 43);
            this.GPIO50_STATE.Name = "GPIO50_STATE";
            this.GPIO50_STATE.Size = new System.Drawing.Size(33, 32);
            this.GPIO50_STATE.TabIndex = 34;
            this.GPIO50_STATE.TabStop = true;
            this.GPIO50_STATE.UseVisualStyleBackColor = true;
            this.GPIO50_STATE.CheckedChanged += new System.EventHandler(this.GPIO50_STATE_CheckedChanged);
            // 
            // GPIO50
            // 
            this.GPIO50.AutoSize = true;
            this.GPIO50.Location = new System.Drawing.Point(29, 45);
            this.GPIO50.Name = "GPIO50";
            this.GPIO50.Size = new System.Drawing.Size(103, 30);
            this.GPIO50.TabIndex = 33;
            this.GPIO50.Text = "GPIO50";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GPIO49_OUT);
            this.groupBox1.Controls.Add(this.GPIO49_DIR);
            this.groupBox1.Controls.Add(this.GPIO49_STATE);
            this.groupBox1.Controls.Add(this.GPIO49);
            this.groupBox1.Location = new System.Drawing.Point(183, 438);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1207, 107);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // GPIO49_OUT
            // 
            this.GPIO49_OUT.AutoSize = true;
            this.GPIO49_OUT.Location = new System.Drawing.Point(502, 41);
            this.GPIO49_OUT.Name = "GPIO49_OUT";
            this.GPIO49_OUT.Size = new System.Drawing.Size(96, 34);
            this.GPIO49_OUT.TabIndex = 35;
            this.GPIO49_OUT.Text = "Low";
            this.GPIO49_OUT.UseVisualStyleBackColor = true;
            this.GPIO49_OUT.CheckedChanged += new System.EventHandler(this.GPIO49_OUT_CheckedChanged);
            // 
            // GPIO49_DIR
            // 
            this.GPIO49_DIR.AutoSize = true;
            this.GPIO49_DIR.Location = new System.Drawing.Point(247, 41);
            this.GPIO49_DIR.Name = "GPIO49_DIR";
            this.GPIO49_DIR.Size = new System.Drawing.Size(126, 34);
            this.GPIO49_DIR.TabIndex = 34;
            this.GPIO49_DIR.Text = "Input";
            this.GPIO49_DIR.UseVisualStyleBackColor = true;
            this.GPIO49_DIR.CheckedChanged += new System.EventHandler(this.GPIO49_DIR_CheckedChanged);
            // 
            // GPIO49_STATE
            // 
            this.GPIO49_STATE.AutoSize = true;
            this.GPIO49_STATE.Location = new System.Drawing.Point(769, 43);
            this.GPIO49_STATE.Name = "GPIO49_STATE";
            this.GPIO49_STATE.Size = new System.Drawing.Size(33, 32);
            this.GPIO49_STATE.TabIndex = 33;
            this.GPIO49_STATE.TabStop = true;
            this.GPIO49_STATE.UseVisualStyleBackColor = true;
            this.GPIO49_STATE.CheckedChanged += new System.EventHandler(this.GPIO49_STATE_CheckedChanged);
            // 
            // GPIO49
            // 
            this.GPIO49.AutoSize = true;
            this.GPIO49.Location = new System.Drawing.Point(27, 45);
            this.GPIO49.Name = "GPIO49";
            this.GPIO49.Size = new System.Drawing.Size(103, 30);
            this.GPIO49.TabIndex = 32;
            this.GPIO49.Text = "GPIO49";
            // 
            // GPIO48_BOX
            // 
            this.GPIO48_BOX.Controls.Add(this.GPIO48_OUT);
            this.GPIO48_BOX.Controls.Add(this.GPIO48_DIR);
            this.GPIO48_BOX.Controls.Add(this.GPIO48_STATE);
            this.GPIO48_BOX.Controls.Add(this.GPIO48);
            this.GPIO48_BOX.Location = new System.Drawing.Point(183, 332);
            this.GPIO48_BOX.Name = "GPIO48_BOX";
            this.GPIO48_BOX.Size = new System.Drawing.Size(1207, 107);
            this.GPIO48_BOX.TabIndex = 34;
            this.GPIO48_BOX.TabStop = false;
            // 
            // GPIO48_OUT
            // 
            this.GPIO48_OUT.AutoSize = true;
            this.GPIO48_OUT.Location = new System.Drawing.Point(502, 34);
            this.GPIO48_OUT.Name = "GPIO48_OUT";
            this.GPIO48_OUT.Size = new System.Drawing.Size(96, 34);
            this.GPIO48_OUT.TabIndex = 34;
            this.GPIO48_OUT.Text = "Low";
            this.GPIO48_OUT.UseVisualStyleBackColor = true;
            this.GPIO48_OUT.CheckedChanged += new System.EventHandler(this.GPIO48_OUT_CheckedChanged);
            // 
            // GPIO48_DIR
            // 
            this.GPIO48_DIR.AutoSize = true;
            this.GPIO48_DIR.Location = new System.Drawing.Point(247, 34);
            this.GPIO48_DIR.Name = "GPIO48_DIR";
            this.GPIO48_DIR.Size = new System.Drawing.Size(126, 34);
            this.GPIO48_DIR.TabIndex = 33;
            this.GPIO48_DIR.Text = "Input";
            this.GPIO48_DIR.UseVisualStyleBackColor = true;
            this.GPIO48_DIR.CheckedChanged += new System.EventHandler(this.GPIO48_DIR_CheckedChanged);
            // 
            // GPIO48_STATE
            // 
            this.GPIO48_STATE.AutoSize = true;
            this.GPIO48_STATE.Location = new System.Drawing.Point(769, 36);
            this.GPIO48_STATE.Name = "GPIO48_STATE";
            this.GPIO48_STATE.Size = new System.Drawing.Size(33, 32);
            this.GPIO48_STATE.TabIndex = 32;
            this.GPIO48_STATE.TabStop = true;
            this.GPIO48_STATE.UseVisualStyleBackColor = true;
            this.GPIO48_STATE.CheckedChanged += new System.EventHandler(this.GPIO48_STATE_CheckedChanged);
            // 
            // GPIO48
            // 
            this.GPIO48.AutoSize = true;
            this.GPIO48.Location = new System.Drawing.Point(27, 38);
            this.GPIO48.Name = "GPIO48";
            this.GPIO48.Size = new System.Drawing.Size(103, 30);
            this.GPIO48.TabIndex = 31;
            this.GPIO48.Text = "GPIO48";
            // 
            // GPIO33_BOX
            // 
            this.GPIO33_BOX.Controls.Add(this.GPIO33_OUT);
            this.GPIO33_BOX.Controls.Add(this.GPIO33_DIR);
            this.GPIO33_BOX.Controls.Add(this.GPIO33_STATE);
            this.GPIO33_BOX.Controls.Add(this.GPIO33);
            this.GPIO33_BOX.Location = new System.Drawing.Point(183, 225);
            this.GPIO33_BOX.Name = "GPIO33_BOX";
            this.GPIO33_BOX.Size = new System.Drawing.Size(1207, 107);
            this.GPIO33_BOX.TabIndex = 33;
            this.GPIO33_BOX.TabStop = false;
            // 
            // GPIO33_OUT
            // 
            this.GPIO33_OUT.AutoSize = true;
            this.GPIO33_OUT.Location = new System.Drawing.Point(502, 41);
            this.GPIO33_OUT.Name = "GPIO33_OUT";
            this.GPIO33_OUT.Size = new System.Drawing.Size(96, 34);
            this.GPIO33_OUT.TabIndex = 33;
            this.GPIO33_OUT.Text = "Low";
            this.GPIO33_OUT.UseVisualStyleBackColor = true;
            this.GPIO33_OUT.CheckedChanged += new System.EventHandler(this.GPIO33_OUT_CheckedChanged);
            // 
            // GPIO33_DIR
            // 
            this.GPIO33_DIR.AutoSize = true;
            this.GPIO33_DIR.Location = new System.Drawing.Point(249, 41);
            this.GPIO33_DIR.Name = "GPIO33_DIR";
            this.GPIO33_DIR.Size = new System.Drawing.Size(126, 34);
            this.GPIO33_DIR.TabIndex = 32;
            this.GPIO33_DIR.Text = "Input";
            this.GPIO33_DIR.UseVisualStyleBackColor = true;
            this.GPIO33_DIR.CheckedChanged += new System.EventHandler(this.GPIO33_DIR_CheckedChanged);
            // 
            // GPIO33_STATE
            // 
            this.GPIO33_STATE.AutoSize = true;
            this.GPIO33_STATE.Location = new System.Drawing.Point(769, 41);
            this.GPIO33_STATE.Name = "GPIO33_STATE";
            this.GPIO33_STATE.Size = new System.Drawing.Size(33, 32);
            this.GPIO33_STATE.TabIndex = 31;
            this.GPIO33_STATE.TabStop = true;
            this.GPIO33_STATE.UseVisualStyleBackColor = true;
            this.GPIO33_STATE.CheckedChanged += new System.EventHandler(this.GPIO33_STATE_CheckedChanged);
            // 
            // GPIO33
            // 
            this.GPIO33.AutoSize = true;
            this.GPIO33.Location = new System.Drawing.Point(27, 43);
            this.GPIO33.Name = "GPIO33";
            this.GPIO33.Size = new System.Drawing.Size(103, 30);
            this.GPIO33.TabIndex = 30;
            this.GPIO33.Text = "GPIO33";
            // 
            // GPIO_STATE
            // 
            this.GPIO_STATE.AutoSize = true;
            this.GPIO_STATE.Location = new System.Drawing.Point(907, 164);
            this.GPIO_STATE.Name = "GPIO_STATE";
            this.GPIO_STATE.Size = new System.Drawing.Size(163, 30);
            this.GPIO_STATE.TabIndex = 20;
            this.GPIO_STATE.Text = "GPIO STATE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(661, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 30);
            this.label1.TabIndex = 15;
            this.label1.Text = "GPIO OUT";
            // 
            // GPIO_DIR
            // 
            this.GPIO_DIR.AutoSize = true;
            this.GPIO_DIR.Location = new System.Drawing.Point(421, 164);
            this.GPIO_DIR.Name = "GPIO_DIR";
            this.GPIO_DIR.Size = new System.Drawing.Size(133, 30);
            this.GPIO_DIR.TabIndex = 14;
            this.GPIO_DIR.Text = "GPIO DIR";
            // 
            // GPIO_NAME
            // 
            this.GPIO_NAME.AutoSize = true;
            this.GPIO_NAME.Location = new System.Drawing.Point(194, 164);
            this.GPIO_NAME.Name = "GPIO_NAME";
            this.GPIO_NAME.Size = new System.Drawing.Size(148, 30);
            this.GPIO_NAME.TabIndex = 13;
            this.GPIO_NAME.Text = "GPIO NAME";
            // 
            // TitleMenuStrip
            // 
            this.TitleMenuStrip.AutoSize = false;
            this.TitleMenuStrip.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TitleMenuStrip.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.TitleMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
            this.TitleMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.TitleMenuStrip.Name = "TitleMenuStrip";
            this.TitleMenuStrip.Padding = new System.Windows.Forms.Padding(15, 15, 0, 5);
            this.TitleMenuStrip.Size = new System.Drawing.Size(1661, 93);
            this.TitleMenuStrip.TabIndex = 1;
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_ExitMenuItem});
            this.FileMenuItem.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(172, 73);
            this.FileMenuItem.Text = "File(F)";
            this.FileMenuItem.Click += new System.EventHandler(this.FileMenuItem_Click);
            // 
            // File_ExitMenuItem
            // 
            this.File_ExitMenuItem.Name = "File_ExitMenuItem";
            this.File_ExitMenuItem.Size = new System.Drawing.Size(448, 54);
            this.File_ExitMenuItem.Text = "Exit";
            this.File_ExitMenuItem.Click += new System.EventHandler(this.File_ExitMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1661, 1184);
            this.Controls.Add(this.GroupBox);
            this.Controls.Add(this.TitleMenuStrip);
            this.Name = "MainForm";
            this.Text = "GPIO RW";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.GroupBox.ResumeLayout(false);
            this.GroupBox.PerformLayout();
            this.GPIO50_BOX.ResumeLayout(false);
            this.GPIO50_BOX.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GPIO48_BOX.ResumeLayout(false);
            this.GPIO48_BOX.PerformLayout();
            this.GPIO33_BOX.ResumeLayout(false);
            this.GPIO33_BOX.PerformLayout();
            this.TitleMenuStrip.ResumeLayout(false);
            this.TitleMenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBox;
        private System.Windows.Forms.MenuStrip TitleMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem File_ExitMenuItem;
        private System.Windows.Forms.Label GPIO_DIR;
        private System.Windows.Forms.Label GPIO_NAME;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label GPIO_STATE;
        private System.Windows.Forms.GroupBox GPIO33_BOX;
        private System.Windows.Forms.CheckBox GPIO33_OUT;
        private System.Windows.Forms.CheckBox GPIO33_DIR;
        private System.Windows.Forms.RadioButton GPIO33_STATE;
        private System.Windows.Forms.Label GPIO33;
        private System.Windows.Forms.GroupBox GPIO50_BOX;
        private System.Windows.Forms.CheckBox GPIO50_OUT;
        private System.Windows.Forms.CheckBox GPIO50_DIR;
        private System.Windows.Forms.RadioButton GPIO50_STATE;
        private System.Windows.Forms.Label GPIO50;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox GPIO49_OUT;
        private System.Windows.Forms.CheckBox GPIO49_DIR;
        private System.Windows.Forms.RadioButton GPIO49_STATE;
        private System.Windows.Forms.Label GPIO49;
        private System.Windows.Forms.GroupBox GPIO48_BOX;
        private System.Windows.Forms.CheckBox GPIO48_OUT;
        private System.Windows.Forms.CheckBox GPIO48_DIR;
        private System.Windows.Forms.RadioButton GPIO48_STATE;
        private System.Windows.Forms.Label GPIO48;
        private System.Windows.Forms.Button GPIO50_UPDATE;
        private System.Windows.Forms.Button GPIO49_UPDATE;
        private System.Windows.Forms.Button GPIO48_UPDATE;
        private System.Windows.Forms.Button GPIO33_UPDATE;
        private System.Windows.Forms.RichTextBox PromptInfo;
        private System.Windows.Forms.Label ToolTitleLabel;
    }
}

