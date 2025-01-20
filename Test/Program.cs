using System.Runtime.InteropServices;
using Viis.Test;
using ViisApp;
using ViisApp.Lib;

namespace Test
{
    internal class Program
    {
        [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
        static extern Boolean CloseHandle(IntPtr h);
        static void Main(string[] args)
        {
            //TestMarshal();

            //TestNLog();

            //TestGpio();

            //TestGpioOem();

            //TestCrypto();

            TestLicense();

            Console.ReadKey();
        }

        private static void TestLicense()
        {
            var licenseStr = LicenceHelper.CreateLicenseData();
            Console.WriteLine(licenseStr);
            LicenceHelper.GenerateLicense(licenseStr, DateTime.Now.AddHours(1), "ViisApp");
            var result = LicenceHelper.CheckLicence("ViisApp");
        }

        public static void TestCrypto()
        {
            //CryptoHelper.GenerateKeyPair();
            //var signData = CryptoHelper.SignData("Hello, world!");
            //Console.WriteLine(signData);
            //var verifyResult = CryptoHelper.VerifyData("Hello, world!", signData);
            //Console.WriteLine(verifyResult);
            //string plainText = "Hello, world!";
            //string cipher = CryptoHelper.Encrypt(plainText);
            //string decrypted = CryptoHelper.Decrypt(cipher);
            //Console.WriteLine(cipher);
            //Console.WriteLine(decrypted);

            



        }

        private static void TestGpioOem()
        {
            GpioHost host = new GpioHost(1);
            //for (int i = 0; i < 4; i++)
            //{
            //    Console.WriteLine($"get {i}:{host.GetValue(i)}");

            //    host.SetValue(i, 1);
            //    Console.WriteLine($"set {i}:1");
            //    //if(1 == host.GetValue(i))
            //    //{
            //    //    Console.WriteLine("set success");
            //    //}else
            //    //{
            //    //    Console.WriteLine("set failed");
            //    //}

            //    Thread.Sleep(1000);

            //    host.SetValue(i, 0);
            //    Console.WriteLine($"set {i}:0");
            //    Thread.Sleep(1000);
            //    //if (0 == host.GetValue(i))
            //    //{
            //    //    Console.WriteLine("set success");
            //    //}
            //    //else
            //    //{
            //    //    Console.WriteLine("set failed");
            //    //}
            //}

            var task= Task.Factory.StartNew(() =>
            {
                Console.ReadKey();
                Console.WriteLine("set 2 pin");
                host.SetValue(2, 1);
                Thread.Sleep(1000);
                host.SetValue(2, 0);
                Thread.Sleep(1000);
                Console.WriteLine("reset 2 pin");

                host.ShutdownWinIoDriver();
            });

            task.Wait();

            
        }

        //private static void TestGpio()
        //{
        //    IOHelper helper = new IOHelper();
        //    helper.SetGpio33Change();
        //    helper.SetGpio48Change();
        //    helper.SetGpio49Change();
        //    helper.SetGpio50Change();
        //}

        private static void TestNLog()
        {
            NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration("nlog.config");
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Hello, NLog!");
        }

        
    }
    public struct Point
        {
            public Int32 x, y;
        }
}
