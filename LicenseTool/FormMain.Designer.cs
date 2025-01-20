namespace LicenseTool
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
            label1 = new Label();
            dtTimeout = new DateTimePicker();
            label2 = new Label();
            txtRequestFile = new TextBox();
            btnBrowser = new Button();
            btnGenLicense = new Button();
            btnInit = new Button();
            ofd = new OpenFileDialog();
            sfd = new SaveFileDialog();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 45);
            label1.Name = "label1";
            label1.Size = new Size(80, 17);
            label1.TabIndex = 0;
            label1.Text = "授权到期时间";
            // 
            // dtTimeout
            // 
            dtTimeout.Location = new Point(97, 40);
            dtTimeout.Name = "dtTimeout";
            dtTimeout.Size = new Size(200, 23);
            dtTimeout.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 70);
            label2.Name = "label2";
            label2.Size = new Size(80, 17);
            label2.TabIndex = 2;
            label2.Text = "授权申请文件";
            // 
            // txtRequestFile
            // 
            txtRequestFile.Location = new Point(97, 67);
            txtRequestFile.Name = "txtRequestFile";
            txtRequestFile.Size = new Size(200, 23);
            txtRequestFile.TabIndex = 3;
            // 
            // btnBrowser
            // 
            btnBrowser.Location = new Point(303, 67);
            btnBrowser.Name = "btnBrowser";
            btnBrowser.Size = new Size(75, 23);
            btnBrowser.TabIndex = 4;
            btnBrowser.Text = "浏览";
            btnBrowser.UseVisualStyleBackColor = true;
            btnBrowser.Click += btnBrowser_Click;
            // 
            // btnGenLicense
            // 
            btnGenLicense.Location = new Point(384, 67);
            btnGenLicense.Name = "btnGenLicense";
            btnGenLicense.Size = new Size(75, 23);
            btnGenLicense.TabIndex = 5;
            btnGenLicense.Text = "生成授权文件";
            btnGenLicense.UseVisualStyleBackColor = true;
            btnGenLicense.Click += btnGenLicense_Click;
            // 
            // btnInit
            // 
            btnInit.Location = new Point(12, 12);
            btnInit.Name = "btnInit";
            btnInit.Size = new Size(139, 23);
            btnInit.TabIndex = 6;
            btnInit.Text = "授权工具初始化";
            btnInit.UseVisualStyleBackColor = true;
            btnInit.Click += btnInit_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(479, 114);
            Controls.Add(btnInit);
            Controls.Add(btnGenLicense);
            Controls.Add(btnBrowser);
            Controls.Add(txtRequestFile);
            Controls.Add(label2);
            Controls.Add(dtTimeout);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FormMain";
            Text = "授权文件生成工具";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DateTimePicker dtTimeout;
        private Label label2;
        private TextBox txtRequestFile;
        private Button btnBrowser;
        private Button btnGenLicense;
        private Button btnInit;
        private OpenFileDialog ofd;
        private SaveFileDialog sfd;
    }
}
