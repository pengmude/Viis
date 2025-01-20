namespace ViisExt
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
            windowBar1 = new AntdUI.WindowBar();
            SuspendLayout();
            // 
            // windowBar1
            // 
            windowBar1.Dock = DockStyle.Top;
            windowBar1.Location = new Point(0, 0);
            windowBar1.Name = "windowBar1";
            windowBar1.Size = new Size(1143, 23);
            windowBar1.TabIndex = 0;
            windowBar1.Text = "windowBar1";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 556);
            Controls.Add(windowBar1);
            Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 4, 4, 4);
            Name = "FormMain";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.WindowBar windowBar1;
    }
}
