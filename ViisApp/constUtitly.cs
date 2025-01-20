namespace ViisApp
{
    public static class constUtitly
    {

        static DialogResult MessageBoxBase(string message, MessageBoxButtons boxButtons, MessageBoxIcon boxIcon, MessageBoxOptions boxOptions = MessageBoxOptions.ServiceNotification)
        {
            SettingStore settings = new SettingStore();
            var caption = settings.GetSetting().AppName;
            return MessageBox.Show(message, caption, boxButtons, boxIcon);
        }

        public static DialogResult MsgInfo(string message)
        {
            return MessageBoxBase(message, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult MsgError(string message)
        {
            return MessageBoxBase(message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static DialogResult MsgAsk(string message)
        {
            return MessageBoxBase(message, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
