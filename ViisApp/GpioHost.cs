using System.Runtime.InteropServices;

namespace ViisApp
{
    public class GpioHost
    {
        // intel6+
        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        static extern int GetGpio(int index);
        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        static extern int SetGpio(int index, int bv);

        ////////////////////////////////////////////////////////////////////
        //SIO Gpio
        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        static extern int SioGetGpio(int index);
        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        static extern int SioSetGpio(int index, int bv);

        //intel 1~5
        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        static extern int PchIoGetGpio(int index);
        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        static extern int PchIoSetGpio(int index, int bv);

        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        static extern int SioWatchDogSetting(int StartWdt, int WdtMode, int WdtTime);

        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        static extern void ShutdownWinIo();
        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        static extern bool RemoveWinIoDriver();

        SettingStore settingStore = new SettingStore();

        int gpioType = 0;
        public GpioHost(int gpioType)
        {
            this.gpioType = gpioType;
        }
        public int GetValue(int index)
        {
            switch (gpioType)
            {
                case 0:
                    return GetGpio(index);
                case 1:
                    return PchIoGetGpio(index);
                case 2:
                    return SioGetGpio(index);
                default:
                    return 0;
            }
        }

        public void SetValue(int index, int bv)
        {
            switch (gpioType)
            {
                case 0:
                    SetGpio(index, bv);
                    break;
                case 1:
                    PchIoSetGpio(index, bv);
                    break;
                case 2:
                    SioSetGpio(index, bv);
                    break;
            }
        }

        public void SetWatchDog(int StartWdt, int WdtMode, int WdtTime)
        {
            SioWatchDogSetting(StartWdt, WdtMode, WdtTime);
        }

        public void ShutdownWinIoDriver()
        {
            ShutdownWinIo();
            RemoveWinIoDriver();
        }
    }
}