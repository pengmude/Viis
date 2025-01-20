using System.Management;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace ViisApp.Lib
{
    public class LicenceHelper
    {
        public static bool CheckLicence(string fileName)
        {
            string licenseStr = string.Empty;
            try
            {
                licenseStr = CryptoHelper.Decrypt(File.ReadAllText(fileName));
            }
            catch (Exception ex)
            {
                throw new Exception("授权文件解密失败。", ex);
            }


            if (CryptoHelper.VerifyData(licenseStr))
            {
                var license = licenseStr.Split("|")[0];
                var licenseData = JsonSerializer.Deserialize<LicenseData>(license);
                if (licenseData == null)
                {
                    throw new Exception("授权文件格式错误。");
                }
                CheckLicenseData(licenseData);
                return true;
            }
            else
            {
                throw new Exception("授权文件校验失败。");
            }


        }

        public static bool CheckLicenseData(LicenseData licenseData)
        {
            // TODO: 校验授权文件是否有效
            try
            {
                CheckProduct(licenseData.ProductName);
                CheckStartDate(licenseData.StartDate);
                CheckEndDate(licenseData.EndDate);
                CheckUserName(licenseData.UserName);
                CheckCpuId(licenseData.CpuId);
                CheckMac(licenseData.Mac);
            }
            catch (Exception ex)
            {
                throw new Exception("授权文件校验失败。", ex);
            }
            return true;
        }

        public static string CreateLicenseData(string userName = "Anyone")
        {
            var licenseData = new LicenseData()
            {
                ProductName = setting.GetSetting().AppName,
                StartDate = DateTime.Now,
                UserName = userName,
                CpuId = GetHardWareInfo("Win32_Processor", "ProcessorId"),
                Mac = LicenseData.GetMacAddress()
            };

            var str = JsonSerializer.Serialize(licenseData);
            return CryptoHelper.Encrypt(str);
        }

        public static void GenerateLicense(string licenseStr, DateTime endDate, string fileName)
        {
            var decrypted = CryptoHelper.Decrypt(licenseStr);
            var licenseData = JsonSerializer.Deserialize<LicenseData>(decrypted);
            if (licenseData == null)
            {
                throw new Exception("授权文件格式错误。");
            }

            licenseData.EndDate = endDate;

            var str = JsonSerializer.Serialize(licenseData);

            var signed = CryptoHelper.SignData(str);

            var license = str + "|"
                + signed + "|"
                + CryptoHelper.GetPublicKey();

            var ret = CryptoHelper.VerifyData(license);
            var encrypted = CryptoHelper.Encrypt(license);

            File.WriteAllText(fileName, encrypted);
        }



        static SettingStore setting = new SettingStore();
        private static void CheckProduct(string productName)
        {

            if (productName != setting.GetSetting().AppName)
            {
                throw new Exception("授权文件与当前程序不匹配。");
            }
        }

        private static void CheckStartDate(DateTime startDate)
        {
            if (startDate > DateTime.Now)
            {
                throw new Exception("授权开始日期不能晚于当前日期。");
            }
        }

        private static void CheckEndDate(DateTime endDate)
        {
            if (endDate < DateTime.Now)
            {
                throw new Exception("授权截止日期不能早于当前日期。");
            }
        }

        private static void CheckLicenseType(string licenseType)
        {
            //if (licenseType!= "Permanent" && licenseType!= "TimeLimited" && licenseType!= "Trial")
            //{
            //    throw new Exception("授权类型必须为永久。");
            //}
        }

        private static void CheckQuantity(int quantity)
        {
            //if (quantity <= 0)
            //{
            //    throw new Exception("授权数量必须大于0。");
            //}
        }

        private static void CheckUserName(string userName)
        {
            //if (string.IsNullOrEmpty(userName))
            //{
            //    throw new Exception("授权使用者不能为空。");
            //}
        }

        private static void CheckCpuId(string cpuId)
        {
            var currentCpuId = GetHardWareInfo("Win32_Processor", "ProcessorId");
            if (cpuId != currentCpuId)
            {
                throw new Exception("授权CpuId与当前CpuId不匹配。");
            }
        }

        private static void CheckMac(string mac)
        {
            var currentMac = LicenseData.GetMacAddress();
            if (mac != currentMac)
            {
                throw new Exception("授权Mac与当前Mac不匹配。");
            }
        }

        private static string GetHardWareInfo(string typePath, string key)
        {
            try
            {
                ManagementClass managementClass = new ManagementClass(typePath);
                ManagementObjectCollection mn = managementClass.GetInstances();
                PropertyDataCollection properties = managementClass.Properties;
                foreach (PropertyData property in properties)
                {
                    if (property.Name == key)
                    {
                        foreach (ManagementObject m in mn)
                        {
                            return m.Properties[property.Name].Value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //这里写异常的处理
            }
            return string.Empty;
        }

        public static bool TryDecrypt(string str, out string decryptedStr)
        {
            try
            {
                decryptedStr = CryptoHelper.Decrypt(str);
                return true;
            }
            catch
            {
                decryptedStr = null;
                return false;
            }
        }
    }

    public class LicenseData
    {
        // 产品名称
        public string ProductName { get; set; }

        // 授权开始日期
        public DateTime StartDate { get; set; }

        // 授权截止日期
        public DateTime EndDate { get; set; }

        // 授权使用者
        public string UserName { get; set; }

        // CpuId
        public string CpuId { get; set; }

        // Mac
        public string Mac { get; set; }


        public static LicenseData Load(string filePath)
        {
            var jsonData = File.ReadAllText(filePath);
            if (LicenceHelper.TryDecrypt(jsonData, out string decryptedData))
            {
                var data = JsonSerializer.Deserialize<LicenseData>(decryptedData);
                return data;
            }

            throw new Exception("授权文件格式错误。");
        }

        public static string GetMacAddress()
        {
            var mac = string.Empty;
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var network in networkInterfaces)
            {
                if (network.OperationalStatus == OperationalStatus.Up)
                {
                    var macBytes = network.GetPhysicalAddress().GetAddressBytes();
                    if (macBytes.Length == 6)
                    {
                        mac = BitConverter.ToString(macBytes).Replace("-", "").ToLower();
                        break;
                    }
                }
            }
            return mac;
        }

        public override string ToString()
        {
            return $"ProductName: {ProductName}, " +
                $"StartDate: {StartDate}, " +
                $"EndDate: {EndDate}," +
                $"UserName: {UserName}," +
                $"CpuId: {CpuId}," +
                $"Mac: {Mac}";
        }
    }
}
