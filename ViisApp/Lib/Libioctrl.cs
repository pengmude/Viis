using System.Runtime.InteropServices;

namespace ViisApp.Lib
{
    public class Libioctrl
    {
        public enum GPIO_PIN_INDEX
        {
            GPIO_PIN1,
            GPIO_PIN2,
            GPIO_PIN3,
            GPIO_PIN4,
            GPIO_PIN5,
            GPIO_PIN6,
            GPIO_PIN7,
            GPIO_PIN8,
            GPIO_PIN9,
            GPIO_PIN10,
            GPIO_PIN11,
            GPIO_PIN12,
            GPIO_PIN13,
            GPIO_PIN14,
            GPIO_PIN15,
            GPIO_PIN16
        }

        public enum GPIO_LEVEL
        {
            GPIO_LOW_LEVEL,
            GPIO_HIGH_LEVEL
        }

        public enum GPIO_DERECTION_INFO
        {
            DERECT_OUPUT,
            DERECT_INPUT
        }

        public enum WDT_TIMER_MODE
        {
            WDT_SECOND_MODE,
            WDT_MINUTE_MODE
        }

        public struct __WDT_INFO
        {
            public byte nowayout; //无退出标志，0：启动后可退出 1：启动后不可退出
            public byte mode;     //秒钟模式或者分钟模式
            public byte time;     //超时值
            public byte enable;   //配置完成后，立即启动看门狗
        }


        //init system driver
        /*
        **函数：libIoCtrlInit
        **函数说明:初始化驱动及导入SDK动态库
        **参数：hlib 模块地址指针，bios_id 主板BIOS ID号，在主板BIOS setup中可以查看得到、
        **返回值: 0 成功，非0失败 ，成功时并导出HMODULE的实例hlib
        */

        [DllImport("libIoCtrlx64.dll")]
        public static extern Int32 libIoCtrlInit(ref IntPtr hlib, string bios_id);

        //for gpio 
        /*
        **函数：setPinInOut
        **函数说明:配置管脚的输入输出
        **参数：hlib 模块地址指针，index 管脚序号（参看GPIO_INDEX枚举定义），state 管脚输入输出设置(参看GPIO_INOUT的枚举定义)
        **返回值: 0 成功，非0失败
        */
        [DllImport("libIoCtrlx64.dll")]
        //public static extern Int32 setPinInOut(ref IntPtr hlib, GPIO_INDEX index, GPIO_INOUT state);
        public static extern Int32 setPinInOut(ref IntPtr hlib, GPIO_PIN_INDEX index, GPIO_DERECTION_INFO state);

        /*
        **函数：getPinLevel
        **函数说明:获取管脚的输入输出状态
        **参数：hlib 模块地址指针，index 管脚序号（参看GPIO_INDEX枚举定义），curstate 导出的管脚输入输出状态,0为低电平，1为高电平
        **返回值: 0 成功，非0失败
        */
        [DllImport("libIoCtrlx64.dll")]
        public static extern Int32 getPinLevel(ref IntPtr hlib, GPIO_PIN_INDEX index, ref byte curstate);


        /*
        **函数：setPinLevel
        **函数说明:设置管脚的输入输出状态
        **参数：hlib 模块地址指针，index 管脚序号（参看GPIO_INDEX枚举定义），curstate 设置管脚输入输出状态,0为低电平，1为高电平(仅为寄存器状态，实际电路的状态，取决于外部电路)
        **返回值: 0 成功，非0失败
        */
        [DllImport("libIoCtrlx64.dll")]
        public static extern Int32 setPinLevel(ref IntPtr hlib, GPIO_PIN_INDEX index, GPIO_LEVEL curstate);

        //for watchdog
        /*
        **函数：cfgWatchdog
        **函数说明:设置看门狗
        **参数：hlib 模块地址指针，iwdtinfo 看门狗状态定义，请参看struct __WDT_INFO的定义
        **返回值: 0 成功，非0失败
        */
        [DllImport("libIoCtrlx64.dll")]
        public static extern Int32 OpenWatchdog(ref IntPtr hlib, ref __WDT_INFO wdtinfo);


        /*
        **函数：updateWatchdog
        **函数说明:更新看门狗定时器配置参数
        **参数：hlib 模块地址指针，
        **参数：wdtinfo 看门狗状态定义，请参看struct __WDT_INFO的定义
        **返回值: 0 成功，非0失败
        */
        [DllImport("libIoCtrlx64.dll")]
        public static extern Int32 updateWatchdog(ref IntPtr hlib, ref __WDT_INFO wdtinfo);

        /*
        **函数：enableWatchdog
        **函数说明:设置看门狗，这个参数取决于struct __WDT_INFO 的nowayout的配置，如果nowayout为1时，此函数无效，任何时候都会返回成功，但是并不会做开启或者关闭动作
        **参数：hlib 模块地址指针，envalue 1 打开 0 关闭 
        **返回值: 0 成功，非0失败
        */
        [DllImport("libIoCtrlx64.dll")]
        public static extern Int32 enableWatchdog(ref IntPtr hlib, byte enValue);


        /*
        **函数：feedWatchdog
        **函数说明:喂狗函数，如果在BIOS已开启看门狗，可不调用cfgWatchdog函数配置看门狗，并直接调用此函数并传入value这个看门狗的时间(必须)，如果已经调用了cfgWatchdog函数配置看门狗，以cfgWatchdog
        **         的配置参数为准，不需要重新传入value这个看门狗的时间(这个参数此时无效)
        **参数：hlib 模块地址指针，
        **返回值: 0 成功，非0失败
        */
        [DllImport("libIoCtrlx64.dll")]
        public static extern Int32 KeepWatchdogAlive(ref IntPtr hlib);


        /*
        **函数：CloseWatchdogTimer
        **函数说明:关闭看门狗定时器(依赖于OpenWatchdog传入的参数，如果nowayout为0，停止看门狗定时器，并结束本次调用，非0，仅结束本次调用，不关闭看门狗定时器)
        **
        **参数：hlib 模块地址指针，
        **返回值: 0 成功，非0失败
        */
        [DllImport("libIoCtrlx64.dll")]
        public static extern Int32 CloseWatchdog(ref IntPtr hlib);

        //deinit system driver,exit
        /*
        **函数：libIoCtrlDeinit
        **函数说明:卸载驱动及导入SDK动态库
        **参数：hlib 模块地址指针
        **返回值: 0 成功，非0失败
        */
        [DllImport("libIoCtrlx64.dll")]
        public static extern Int32 libIoCtrlDeinit(ref IntPtr hlib);
    }

    public class GpioHelper
    {
        int retvalue;
        IntPtr m_HModule;
        Thread watchDogThread;
        public void StartWatchdog()
        {
            Libioctrl.__WDT_INFO wdtinfo;
            wdtinfo.enable = 0;
            wdtinfo.mode = (byte)Libioctrl.WDT_TIMER_MODE.WDT_SECOND_MODE;
            wdtinfo.nowayout = 1;
            wdtinfo.time = 80;

            Libioctrl.OpenWatchdog(ref m_HModule, ref wdtinfo);

            //wdtinfo.enable = 1;
            //wdtinfo.mode = (byte)Libioctrl.WDT_TIMER_MODE.WDT_MINUTE_MODE;
            //wdtinfo.nowayout = 1;
            //wdtinfo.time = 80;
            //Libioctrl.updateWatchdog(ref m_HModule, ref wdtinfo); //configuration watchdog and enable wdt
            Libioctrl.enableWatchdog(ref m_HModule, 80); //启动WDT

            watchDogThread = new Thread(new ThreadStart(WatchDogThreadStart));

            watchDogThread.Start();
        }

        public void StopWatchdog()
        {
            Libioctrl.CloseWatchdog(ref m_HModule); //退出WDT
            watchDogThread.Join(); //退出线程
        }

        public (int, int) SetPin(Libioctrl.GPIO_PIN_INDEX index)
        {
            int ret = Libioctrl.setPinLevel(ref m_HModule, index, Libioctrl.GPIO_LEVEL.GPIO_HIGH_LEVEL);

            if (ret != 0)
            {
                return (ret, -999999);
            }
            Thread.Sleep(4);

            var ret1 = Libioctrl.setPinLevel(ref m_HModule, index, Libioctrl.GPIO_LEVEL.GPIO_LOW_LEVEL);
            return (ret, ret1);
        }

        public byte GetPin(Libioctrl.GPIO_PIN_INDEX index)
        {
            byte curstate = 0;
            retvalue = Libioctrl.getPinLevel(ref m_HModule, index, ref curstate);
            return curstate;
        }

        void WatchDogThreadStart()
        {
            Libioctrl.KeepWatchdogAlive(ref m_HModule); //请在定时器或者线程函数中执行，在此仅为了表达调用顺序
                                                        //Libioctrl.CloseWatchdog(ref m_HModule); //退出WDT
        }
    }
}
