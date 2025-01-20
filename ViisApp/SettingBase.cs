using System.Text.Json;

namespace ViisApp
{
    public class SettingBase<T> where T : class
    {
        string basePath = Application.StartupPath;
        string fileName;
        T _setting;
        public T Setting
        {
            get { return _setting; }
        }
        public SettingBase()
        {
            fileName = Path.Combine(basePath, typeof(T).Name + ".bin");
            ReadSetting();
        }

        private void ReadSetting()
        {
            if (File.Exists(fileName))
            {
                try
                {
                    var jsonString = File.ReadAllText(fileName);
                    _setting = JsonSerializer.Deserialize<T>(jsonString);


                }
                catch
                {
                    ResetSetting();
                }
            }
            else
            {
                ResetSetting();
            }
        }

        private void ResetSetting()
        {
            _setting = Activator.CreateInstance<T>();
            var jsonString = JsonSerializer.Serialize<T>(Setting);

            File.WriteAllText(fileName, jsonString);
        }

        public void SaveSetting(T setting)
        {
            var jsonString = JsonSerializer.Serialize<T>(setting);
            File.WriteAllText(fileName, jsonString);
            _setting = setting;
        }

        public void Update()
        {
            ReadSetting();
        }

        internal void Save()
        {
            var jsonString = JsonSerializer.Serialize<T>(Setting);
            File.WriteAllText(fileName, jsonString);
        }

        public void Reset()
        {
            ResetSetting();
        }
    }
}
