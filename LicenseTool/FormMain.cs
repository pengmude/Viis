using ViisApp.Lib;

namespace LicenseTool
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            ViisApp.Lib.CryptoHelper.GenerateKeyPair();
            MessageBox.Show("KeyPair generated successfully!");
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtRequestFile.Text = ofd.FileName;
            }
        }

        private void btnGenLicense_Click(object sender, EventArgs e)
        {
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                var licenseStr = File.ReadAllText(txtRequestFile.Text);
                LicenceHelper.GenerateLicense(licenseStr, dtTimeout.Value, sfd.FileName);
                MessageBox.Show("License generated successfully!");
            }
        }
    }
}
