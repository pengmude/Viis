using System.Text.Json;

namespace ViisApp
{
    public class SettingStore
    {
        string basePath = Application.StartupPath;
        string fileName;
        SettingData setting;
        public SettingStore()
        {
            fileName = Path.Combine(basePath, "Settings.st");
            ReadSetting();
        }

        private void ReadSetting()
        {
            if (File.Exists(fileName))
            {
                var jsonString = File.ReadAllText(fileName);
                setting = JsonSerializer.Deserialize<SettingData>(jsonString);
            }
            else
            {
                setting = new SettingData
                {
                    AppName = "智能图文适配系统",
                    CamPath = "CAM4",
                    ModelPath = "CAM0"
                };
                var jsonString = JsonSerializer.Serialize<SettingData>(setting);

                File.WriteAllText(fileName, jsonString);
            }
        }

        public SettingData GetSetting()
        {
            return setting;
        }

        internal void SaveSetting(SettingData setting)
        {
            var jsonString = JsonSerializer.Serialize<SettingData>(setting);
            File.WriteAllText(fileName, jsonString);
            this.setting = setting;
        }

        internal void Update()
        {
            ReadSetting();
        }
    }

    public class SettingData
    {
        public string CamPath { get; set; }
        public string ModelPath { get; set; }
        public string AppName { get; set; }
        public string MatchedPath { get; set; }
        public string GPIOType { get; set; }
        public int GPIODelayTime { get; set; }


        // 图像相识度阈值
        public double ImageMatchThreshold { get; set; }
        public int CAM_MAX_FILE_NUM { get; set; }
        public bool IsContoursMatch { get; set; }
        public string GpioPorts { get; set; }
        public int DebugMode { get; set; }
        public float VarianceThreshold { get; set; }
    }
}
