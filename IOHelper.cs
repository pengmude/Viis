using SmartLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartVEye
{
    /// <summary>
    /// IO������
    /// </summary>
    unsafe public class IOHelper
    {
        #region GPIO����
        ///
        /// *******************************************************
        /// IO_PROTOCOL_WIDTH
        /// *******************************************************
        ///
        public enum IO_PROTOCOL_WIDTH
        {
            IoWidthUint8 = 0,
            IoWidthUint16 = 1,
            IoWidthUint32 = 2,
            IoWidthUint64 = 3,
            IoWidthMaximum
        };
        ///
        /// *******************************************************
        /// MEM_PROTOCOL_WIDTH
        /// *******************************************************
        ///
        public enum MEM_PROTOCOL_WIDTH
        {
            MemWidthUint8 = 0,
            MemWidthUint16 = 1,
            MemWidthUint32 = 2,
            MemWidthUint64 = 3,
            MemWidthMaximum
        }

        public const int GPIO_BASE_ADDRESS = 0x1C00;
        //
        // GPIO Init register offsets from GPIOBASE
        //
        public const int R_PCH_GPIO_USE_SEL = 0x00;
        public const int R_PCH_GPIO_IO_SEL = 0x04;
        public const int R_PCH_GPIO_LVL = 0x0C;
        public const int R_PCH_GPIO_IOAPIC_SEL = 0x10;
        public const int V_PCH_GPIO_IOAPIC_SEL = 0xFFFF;
        public const int R_PCH_GPIO_BLINK = 0x18;
        public const int R_PCH_GPIO_SER_BLINK = 0x1C;
        public const int R_PCH_GPIO_SB_CMDSTS = 0x20;
        public const int B_PCH_GPIO_SB_CMDSTS_DLS_MASK = 0x00C00000;
        public const int B_PCH_GPIO_SB_CMDSTS_DRS_MASK = 0x003F0000;
        public const int B_PCH_GPIO_SB_CMDSTS_BUSY = 0x100;
        public const int B_PCH_GPIO_SB_CMDSTS_GO = 0x01;
        public const int R_PCH_GPIO_SB_DATA = 0x24;
        public const int R_PCH_GPIO_NMI_EN = 0x28;
        public const int B_PCH_GPIO_NMI_EN = 0xFFFF;
        public const int R_PCH_GPIO_NMI_STS = 0x2A;
        public const int B_PCH_GPIO_NMI_STS = 0xFFFF;
        public const int R_PCH_GPIO_GPI_INV = 0x2C;
        public const int R_PCH_GPIO_USE_SEL2 = 0x30;
        public const int R_PCH_GPIO_IO_SEL2 = 0x34;
        public const int R_PCH_GPIO_LVL2 = 0x38;
        public const int R_PCH_GPIO_USE_SEL3 = 0x40;
        public const int R_PCH_GPIO_IO_SEL3 = 0x44;
        public const int _PCH_GPIO_LVL3 = 0x48;
        public const int R_PCH_GPIO_CONFIG_REG = 0x100;
        public const bool GPIO_FUNC = true;
        public const bool NVTIVE_FUNC = false;
        public const bool OUTPUT = true;
        public const bool INPUT = false;
        public const bool HIGH = true;
        public const bool LOW = false;
        public const bool ENABLE = true;
        public const bool DISABLE = false;

        // ����Ҫ�� CallingConvention.Cdecl���������ᱨ��
        [DllImport("WinIoDllx64.dll", EntryPoint = "SetPhyMemValue", SetLastError = true)]
        private static extern uint SetPhyMemValue(ulong PhyMemAddr, ulong PortValue, MEM_PROTOCOL_WIDTH MemWidth);
        // ����Ҫ�� CallingConvention.Cdecl���������ᱨ��
        [DllImport("WinIoDllx64.dll", EntryPoint = "GetPhyMemValue", SetLastError = true)]
        private static extern uint GetPhyMemValue(ulong PhyMemAddr, ulong* PortValue, MEM_PROTOCOL_WIDTH MemWidth);
        // ����Ҫ�� CallingConvention.Cdecl���������ᱨ��
        [DllImport("WinIoDllx64.dll", EntryPoint = "SetIoPortValue", SetLastError = true)]
        private static extern uint SetIoPortValue(ushort PortAddr, uint PortValue, IO_PROTOCOL_WIDTH IoWidth);
        // ����Ҫ�� CallingConvention.Cdecl���������ᱨ��
        [DllImport("WinIoDllx64.dll", EntryPoint = "GetIoPortValue", SetLastError = true)]
        private static extern uint GetIoPortValue(ushort PortAddr, uint* PortValue, IO_PROTOCOL_WIDTH IoWidth);

        #endregion

        /// <summary>
        /// IO��ʼ�� ����������IOΪ���ģʽ
        /// </summary>
        /// <returns></returns>
        unsafe public static Response IOInit()
        {
            uint RetStatus;
            uint GpioData;
            uint GpioDataDir;
            uint GpioDataOut;

            //
            // Ĭ�������������, GpioDataDir => 1: Input, 0: Output
            //
            GpioDataDir = 0;
            GpioDataOut = 0;
            GpioDataDir &= 0xFFFFFFFD;
            SetGpioDir(33, OUTPUT);
            SetGpioSenseEn(33, ENABLE);

            GpioDataDir &= 0xFFFEFFFF;
            SetGpioDir(48, OUTPUT);
            SetGpioSenseEn(48, ENABLE);

            GpioDataDir &= 0xFFFDFFFF;
            SetGpioDir(49, OUTPUT);
            SetGpioSenseEn(49, ENABLE);

            GpioDataDir &= 0xFFFBFFFF;
            SetGpioDir(50, OUTPUT);
            SetGpioSenseEn(50, ENABLE);
            //
            // Ĭ�������ƽ����
            //
            GpioDataOut &= 0xFFFFFFFD;
            SetGpioOut(33, LOW);


            GpioDataOut &= 0xFFFEFFFF;
            SetGpioOut(48, LOW);

            GpioDataOut &= 0xFFFDFFFF;
            SetGpioOut(49, LOW);

            GpioDataOut &= 0xFFFBFFFF;
            SetGpioOut(50, LOW);

            // Program GPIO33/GPIO48/GPIO49/GPIO50 as GPIO mode
            SetGpioFunc(33, GPIO_FUNC);
            SetGpioFunc(48, GPIO_FUNC);
            SetGpioFunc(49, GPIO_FUNC);
            SetGpioFunc(50, GPIO_FUNC);

            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_USE_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_USE_SEL2), 16) + " failed.");
                return Response.Fail("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_USE_SEL2), 16) + " failed.");
            }
            GpioData |= 0x00070002;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_USE_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);

            // Program GPIO33/GPIO48/GPIO49/GPIO50 as output mode, 1: Input, 0: Output
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                return Response.Fail("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
            }
            GpioData &= 0xFFF8FFFD;
            GpioData |= GpioDataDir;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);

            // Program GPIO33/GPIO48/GPIO49/GPIO50 output high/low, 1: High, 0: Low
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + " failed.");
                return Response.Fail("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + " failed.");
            }
            GpioData &= 0xFFF8FFFD;
            GpioData |= GpioDataOut;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);

            return Response.Ok();
        }

        /// <summary>
        /// ��IO
        /// </summary>
        /// <param name="ioIdx">IO���</param>
        /// <returns></returns>
        unsafe public static Response SetBitOn(int ioIdx)
        {
            if (ioIdx == 1) return SetBitOn1();
            else if (ioIdx == 2) return SetBitOn2();
            else if (ioIdx == 3) return SetBitOn3();
            else if (ioIdx == 4) return SetBitOn4();
            else return Response.Fail("������IO:" + ioIdx);
        }
        /// <summary>
        /// �ر�IO
        /// </summary>
        /// <param name="ioIdx">IO���</param>
        /// <returns></returns>
        unsafe public static Response SetBitOff(int ioIdx)
        {
            if (ioIdx == 1) return SetBitOff1();
            else if (ioIdx == 2) return SetBitOff2();
            else if (ioIdx == 3) return SetBitOff3();
            else if (ioIdx == 4) return SetBitOff4();
            else return Response.Fail("������IO:" + ioIdx);
        }

        unsafe private static Response SetBitOn1()
        {
            uint RetStatus;
            uint GpioData;
            SetGpioOut(33, HIGH);
            // Program GPIO33 output high, 1: High, 0: Low
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                return Response.Fail($"IO[{1}]������쳣! 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + ";RetStatus: " + RetStatus);
            }
            GpioData |= 0x00000002;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            return Response.Ok();
        }
        unsafe private static Response SetBitOff1()
        {
            uint RetStatus;
            uint GpioData;
            SetGpioOut(33, LOW);
            // Program GPIO33 output low, 1: High, 0: Low
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                return Response.Fail($"IO[{1}]�ر�����쳣! 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + ";RetStatus: " + RetStatus);
            }
            GpioData &= 0xFFFFFFFD;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            return Response.Ok();
        }

        unsafe private static Response SetBitOn2()
        {
            uint RetStatus;
            uint GpioData;
            SetGpioOut(48, HIGH);
            // Program GPIO48 output high, 1: High, 0: Low
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                return Response.Fail($"IO[{2}]������쳣! 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + ";RetStatus: " + RetStatus);
            }
            GpioData |= 0x00010000;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            return Response.Ok();
        }
        unsafe private static Response SetBitOff2()
        {
            uint RetStatus;
            uint GpioData;
            SetGpioOut(48, LOW);
            // Program GPIO48 output low, 1: High, 0: Low
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                return Response.Fail($"IO[{2}]�ر�����쳣! 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + ";RetStatus: " + RetStatus);
            }
            GpioData &= 0xFFFEFFFF;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            return Response.Ok();
        }

        unsafe private static Response SetBitOn3()
        {
            uint RetStatus;
            uint GpioData;
            SetGpioOut(49, HIGH);
            // Program GPIO49 output high, 1: High, 0: Low
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                return Response.Fail($"IO[{3}]������쳣! 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + ";RetStatus: " + RetStatus);
            }
            GpioData |= 0x00020000;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            return Response.Ok();
        }
        unsafe private static Response SetBitOff3()
        {
            uint RetStatus;
            uint GpioData;
            SetGpioOut(49, LOW);
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                return Response.Fail($"IO[{3}]�ر�����쳣! 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + ";RetStatus: " + RetStatus);
            }
            GpioData &= 0xFFFDFFFF;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            return Response.Ok();
        }

        unsafe private static Response SetBitOn4()
        {
            uint RetStatus;
            uint GpioData;
            SetGpioOut(50, HIGH);
            // Program GPIO50 output high, 1: High, 0: Low
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                return Response.Fail($"IO[{4}]������쳣! 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + ";RetStatus: " + RetStatus);
            }
            GpioData |= 0x00040000;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            return Response.Ok();
        }
        unsafe private static Response SetBitOff4()
        {
            uint RetStatus;
            uint GpioData;
            SetGpioOut(50, LOW);
            // Program GPIO50 output low, 1: High, 0: Low
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                return Response.Fail($"IO[{4}]�ر�����쳣! 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + ";RetStatus: " + RetStatus);
            }
            GpioData &= 0xFFFBFFFF;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            return Response.Ok();
        }



        //
        // GpioDataFunc => 1: Gpio Function, 0: Native function
        //
        private static void SetGpioFunc(ushort GpioPinNum, bool Func)
        {
            uint RetStatus;
            uint GpioData;

            // Program GPIO Dir
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8), &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString(((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8)), 16) + " failed.");
                return;
            }

            //
            // BIT0 is GPIO function control, 1: GPIO function, 0: Native function
            //
            if (Func == GPIO_FUNC)
            {
                GpioData |= 0x00000001;
            }
            else
            {
                GpioData &= 0xFFFFFFFE;
            }
            SetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8), GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
        }

        //
        // GpioDataDir => 1: Input, 0: Output
        //
        private static void SetGpioDir(ushort GpioPinNum, bool Dir)
        {
            uint RetStatus;
            uint GpioData;

            // Program GPIO Dir
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8), &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString(((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8)), 16) + " failed.");
                return;
            }

            //
            // BIT2 is GPIO DIR control, 1: input, 0: output
            //
            if (Dir == OUTPUT)
            {
                GpioData &= 0xFFFFFFFB;
            }
            else
            {
                GpioData |= 0x00000004;
            }
            SetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8), GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
        }

        //
        // GpioDataDir => 1: Output High, 0: Output Low
        //
        private static void SetGpioOut(ushort GpioPinNum, bool Out)
        {
            uint RetStatus;
            uint GpioData;

            // Program GPIO Dir
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8), &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString(((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8)), 16) + " failed.");
                return;
            }

            //
            // BIT31 is GPIO OUTPUT state control, 1: HIGH, 0: LOW
            //
            if (Out == HIGH)
            {
                GpioData |= 0x80000000;
            }
            else
            {
                GpioData &= 0x7FFFFFFF;
            }
            SetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8), GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
        }

        //
        // GpioSenseEn => 1: Input sensing disable, 0: Input sensing enable.
        //
        private static void SetGpioSenseEn(ushort GpioPinNum, bool SenseEn)
        {
            uint RetStatus;
            uint GpioData;

            // Program GPIO Dir
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + 0x04 + GpioPinNum * 8), &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString(((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + 0x04 + GpioPinNum * 8)), 16) + " failed.");
                return;
            }

            //
            // BIT2 is GPIO sense input state control, 1: Input sensing disable, 0: Input sensing enable.
            //
            if (SenseEn == ENABLE)
            {
                GpioData &= 0xFFFFFFFB;
            }
            else
            {
                GpioData |= 0x00000004;
            }
            SetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + 0x04 + GpioPinNum * 8), GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
        }

        //
        // GpioDataState => 1: Output High, 0: Output Low
        //
        private static bool GetGpioState(ushort GpioPinNum)
        {
            uint RetStatus;
            uint GpioData;

            // Program GPIO Dir
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8), &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString(((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8)), 16) + " failed.");
                return false;
            }

            //
            // BIT30 is GPIO input State, 1: HIGH, 0: LOW
            //
            if ((GpioData & 0x40000000) != 0x00)
            {
                return true;
            }

            return false;
        }

    }
}
