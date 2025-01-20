using System.Runtime.InteropServices;

namespace GpioCheck
{
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
    unsafe public partial class MainForm : Form
    {
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

        // 必须要有 CallingConvention.Cdecl，否则程序会报错
        [DllImport("WinIoDllx64.dll", EntryPoint = "SetPhyMemValue", SetLastError = true)]
        private static extern uint SetPhyMemValue(ulong PhyMemAddr, ulong PortValue, MEM_PROTOCOL_WIDTH MemWidth);
        // 必须要有 CallingConvention.Cdecl，否则程序会报错
        [DllImport("WinIoDllx64.dll", EntryPoint = "GetPhyMemValue", SetLastError = true)]
        private static extern uint GetPhyMemValue(ulong PhyMemAddr, ulong* PortValue, MEM_PROTOCOL_WIDTH MemWidth);
        // 必须要有 CallingConvention.Cdecl，否则程序会报错
        [DllImport("WinIoDllx64.dll", EntryPoint = "SetIoPortValue", SetLastError = true)]
        private static extern uint SetIoPortValue(ushort PortAddr, uint PortValue, IO_PROTOCOL_WIDTH IoWidth);
        // 必须要有 CallingConvention.Cdecl，否则程序会报错
        [DllImport("WinIoDllx64.dll", EntryPoint = "GetIoPortValue", SetLastError = true)]
        private static extern uint GetIoPortValue(ushort PortAddr, uint* PortValue, IO_PROTOCOL_WIDTH IoWidth);


        //
        // GpioDataFunc => 1: Gpio Function, 0: Native function
        //
        private void SetGpioFunc(ushort GpioPinNum, bool Func)
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
        private void SetGpioDir(ushort GpioPinNum, bool Dir)
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
        private void SetGpioOut(ushort GpioPinNum, bool Out)
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
        private void SetGpioSenseEn(ushort GpioPinNum, bool SenseEn)
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
        private bool GetGpioState(ushort GpioPinNum)
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

        public MainForm()
        {
            uint RetStatus;
            uint GpioData;
            uint GpioDataDir;
            uint GpioDataOut;


            InitializeComponent();

            //
            // 默认输出方向配置, GpioDataDir => 1: Input, 0: Output
            //
            GpioDataDir = 0;
            GpioDataOut = 0;
            if (GPIO33_DIR.Checked)
            {
                GPIO33_OUT.Enabled = true;
                GpioDataDir &= 0xFFFFFFFD;
                SetGpioDir(33, OUTPUT);
                SetGpioSenseEn(33, ENABLE);
            }
            else
            {
                GPIO33_OUT.Enabled = false;
                GpioDataDir |= 0x00000002;
                SetGpioDir(33, INPUT);
                SetGpioSenseEn(33, ENABLE);
            }
            if (GPIO48_DIR.Checked)
            {
                GPIO48_OUT.Enabled = true;
                GpioDataDir &= 0xFFFEFFFF;
                SetGpioDir(48, OUTPUT);
                SetGpioSenseEn(48, ENABLE);
            }
            else
            {
                GPIO48_OUT.Enabled = false;
                GpioDataDir |= 0x00010000;
                SetGpioDir(48, INPUT);
                SetGpioSenseEn(48, ENABLE);
            }
            if (GPIO49_DIR.Checked)
            {
                GPIO49_OUT.Enabled = true;
                GpioDataDir &= 0xFFFDFFFF;
                SetGpioDir(49, OUTPUT);
                SetGpioSenseEn(49, ENABLE);
            }
            else
            {
                GPIO49_OUT.Enabled = false;
                GpioDataDir |= 0x00020000;
                SetGpioDir(49, INPUT);
                SetGpioSenseEn(49, ENABLE);
            }
            if (GPIO50_DIR.Checked)
            {
                GPIO50_OUT.Enabled = true;
                GpioDataDir &= 0xFFFBFFFF;
                SetGpioDir(50, OUTPUT);
                SetGpioSenseEn(50, ENABLE);
            }
            else
            {
                GPIO50_OUT.Enabled = false;
                GpioDataDir |= 0x00040000;
                SetGpioDir(50, INPUT);
                SetGpioSenseEn(50, ENABLE);
            }
            //
            // 默认输出电平配置
            //
            if (GPIO33_OUT.Checked)
            {
                GpioDataOut |= 0x00000002;
                SetGpioOut(33, HIGH);
            }
            else
            {
                GpioDataOut &= 0xFFFFFFFD;
                SetGpioOut(33, LOW);
            }
            if (GPIO48_OUT.Checked)
            {
                GpioDataOut |= 0x00010000;
                SetGpioOut(48, HIGH);
            }
            else
            {
                GpioDataOut &= 0xFFFEFFFF;
                SetGpioOut(48, LOW);
            }
            if (GPIO49_OUT.Checked)
            {
                GpioDataOut |= 0x00020000;
                SetGpioOut(49, HIGH);
            }
            else
            {
                GpioDataOut &= 0xFFFDFFFF;
                SetGpioOut(49, LOW);
            }
            if (GPIO50_OUT.Checked)
            {
                GpioDataOut |= 0x00040000;
                SetGpioOut(50, HIGH);
            }
            else
            {
                GpioDataOut &= 0xFFFBFFFF;
                SetGpioOut(50, LOW);
            }
            // 设置 radio button 不能选中
            GPIO33_STATE.Enabled = false;
            GPIO48_STATE.Enabled = false;
            GPIO49_STATE.Enabled = false;
            GPIO50_STATE.Enabled = false;

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
                return;
            }
            GpioData |= 0x00070002;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_USE_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);

            // Program GPIO33/GPIO48/GPIO49/GPIO50 as output mode, 1: Input, 0: Output
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                return;
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
                return;
            }
            GpioData &= 0xFFF8FFFD;
            GpioData |= GpioDataOut;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
        }

        private void GroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void FileMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void File_ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void GPIO33_DIR_CheckedChanged(object sender, EventArgs e)
        {
            uint RetStatus;
            uint GpioData;

            if (GPIO33_DIR.Checked)
            {
                GPIO33_DIR.Text = "Output";
                GPIO33_OUT.Enabled = true;
                Console.WriteLine("Set GPIO33 as [Output]");
                SetGpioDir(33, OUTPUT);
                SetGpioSenseEn(33, ENABLE);
                // Program GPIO33 as Output mode, 1: Input, 0: Output
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                    return;
                }
                GpioData &= 0xFFFFFFFD;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
            else
            {
                GPIO33_DIR.Text = "Input";
                GPIO33_OUT.Enabled = false;
                Console.WriteLine("Set GPIO33 as [Input]");
                SetGpioDir(33, INPUT);
                SetGpioSenseEn(33, ENABLE);
                // Program GPIO33 as Input mode, 1: Input, 0: Output
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                    return;
                }
                GpioData |= 0x00000002;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
        }

        private void GPIO33_OUT_CheckedChanged(object sender, EventArgs e)
        {
            uint RetStatus;
            uint GpioData;

            if (GPIO33_OUT.Checked)
            {
                GPIO33_OUT.Text = "High";
                Console.WriteLine("Set GPIO33 to [high]");
                SetGpioOut(33, HIGH);
                // Program GPIO33 output high, 1: High, 0: Low
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + " failed.");
                    return;
                }
                GpioData |= 0x00000002;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
            else
            {
                GPIO33_OUT.Text = "Low";
                Console.WriteLine("Set GPIO33 to [Low]");
                SetGpioOut(33, LOW);
                // Program GPIO33 output low, 1: High, 0: Low
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + " failed.");
                    return;
                }
                GpioData &= 0xFFFFFFFD;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
        }

        private void GPIO33_STATE_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void GPIO33_UPDATE_Click(object sender, EventArgs e)
        {
            // Read GPIO33 state, 1: High, 0: Low
            if (GetGpioState(33))
            {
                GPIO33_STATE.Checked = true;
                Console.WriteLine("Get GPIO33 State: [high]");
            }
            else
            {
                GPIO33_STATE.Checked = false;
                Console.WriteLine("Get GPIO33 State: [high]");
            }
        }

        private void GPIO48_DIR_CheckedChanged(object sender, EventArgs e)
        {
            uint RetStatus;
            uint GpioData;

            if (GPIO48_DIR.Checked)
            {
                GPIO48_DIR.Text = "Output";
                GPIO48_OUT.Enabled = true;
                Console.WriteLine("Set GPIO48 as [Output]");
                SetGpioDir(48, OUTPUT);
                SetGpioSenseEn(48, ENABLE);
                // Program GPIO48 as Output mode, 1: Input, 0: Output
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                    return;
                }
                GpioData &= 0xFFFEFFFF;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
            else
            {
                GPIO48_DIR.Text = "Input";
                GPIO48_OUT.Enabled = false;
                Console.WriteLine("Set GPIO48 as [Input]");
                SetGpioDir(48, INPUT);
                SetGpioSenseEn(48, ENABLE);
                // Program GPIO48 as Input mode, 1: Input, 0: Output
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                    return;
                }
                GpioData |= 0x00010000;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
        }

        private void GPIO48_OUT_CheckedChanged(object sender, EventArgs e)
        {
            uint RetStatus;
            uint GpioData;

            if (GPIO48_OUT.Checked)
            {
                GPIO48_OUT.Text = "High";
                Console.WriteLine("Set GPIO48 to [high]");
                SetGpioOut(48, HIGH);
                // Program GPIO48 output high, 1: High, 0: Low
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + " failed.");
                    return;
                }
                GpioData |= 0x00010000;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
            else
            {
                GPIO48_OUT.Text = "Low";
                Console.WriteLine("Set GPIO48 to [Low]");
                SetGpioOut(48, LOW);
                // Program GPIO48 output low, 1: High, 0: Low
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + " failed.");
                    return;
                }
                GpioData &= 0xFFFEFFFF;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
        }

        private void GPIO48_STATE_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void GPIO48_UPDATE_Click(object sender, EventArgs e)
        {
            // Read GPIO48 state, 1: High, 0: Low
            if (GetGpioState(48))
            {
                GPIO48_STATE.Checked = true;
                Console.WriteLine("Get GPIO48 State: [high]");
            }
            else
            {
                GPIO48_STATE.Checked = false;
                Console.WriteLine("Get GPIO48 State: [high]");
            }
        }

        private void GPIO49_DIR_CheckedChanged(object sender, EventArgs e)
        {
            uint RetStatus;
            uint GpioData;

            if (GPIO49_DIR.Checked)
            {
                GPIO49_DIR.Text = "Output";
                GPIO49_OUT.Enabled = true;
                Console.WriteLine("Set GPIO49 as [Output]");
                SetGpioDir(49, OUTPUT);
                SetGpioSenseEn(49, ENABLE);
                // Program GPIO49 as Output mode, 1: Input, 0: Output
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                    return;
                }
                GpioData &= 0xFFFDFFFF;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
            else
            {
                GPIO49_DIR.Text = "Input";
                GPIO49_OUT.Enabled = false;
                Console.WriteLine("Set GPIO49 as [Input]");
                SetGpioDir(49, INPUT);
                SetGpioSenseEn(49, ENABLE);
                // Program GPIO49 as Input mode, 1: Input, 0: Output
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                    return;
                }
                GpioData |= 0x00020000;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
        }

        private void GPIO49_OUT_CheckedChanged(object sender, EventArgs e)
        {
            uint RetStatus;
            uint GpioData;

            if (GPIO49_OUT.Checked)
            {
                GPIO49_OUT.Text = "High";
                Console.WriteLine("Set GPIO49 to [high]");
                SetGpioOut(49, HIGH);
                // Program GPIO49 output high, 1: High, 0: Low
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + " failed.");
                    return;
                }
                GpioData |= 0x00020000;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
            else
            {
                GPIO49_OUT.Text = "Low";
                Console.WriteLine("Set GPIO49 to [Low]");
                SetGpioOut(49, LOW);
                // Program GPIO49 output low, 1: High, 0: Low
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + " failed.");
                    return;
                }
                GpioData &= 0xFFFDFFFF;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
        }

        private void GPIO49_STATE_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void GPIO49_UPDATE_Click(object sender, EventArgs e)
        {
            // Read GPIO49 state, 1: High, 0: Low
            if (GetGpioState(49))
            {
                GPIO49_STATE.Checked = true;
                Console.WriteLine("Get GPIO49 State: [high]");
            }
            else
            {
                GPIO49_STATE.Checked = false;
                Console.WriteLine("Get GPIO49 State: [high]");
            }
        }

        private void GPIO50_DIR_CheckedChanged(object sender, EventArgs e)
        {
            uint RetStatus;
            uint GpioData;

            if (GPIO50_DIR.Checked)
            {
                GPIO50_DIR.Text = "Output";
                GPIO50_OUT.Enabled = true;
                Console.WriteLine("Set GPIO50 as [Output]");
                SetGpioDir(50, OUTPUT);
                SetGpioSenseEn(50, ENABLE);
                // Program GPIO50 as Output mode, 1: Input, 0: Output
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                    return;
                }
                GpioData &= 0xFFFBFFFF;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
            else
            {
                GPIO50_DIR.Text = "Input";
                GPIO50_OUT.Enabled = false;
                Console.WriteLine("Set GPIO50 as [Input]");
                SetGpioDir(50, INPUT);
                SetGpioSenseEn(50, ENABLE);
                // Program GPIO50 as Input mode, 1: Input, 0: Output
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                    return;
                }
                GpioData |= 0x00040000;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
        }

        private void GPIO50_OUT_CheckedChanged(object sender, EventArgs e)
        {
            uint RetStatus;
            uint GpioData;

            if (GPIO50_OUT.Checked)
            {
                GPIO50_OUT.Text = "High";
                Console.WriteLine("Set GPIO50 to [high]");
                SetGpioOut(50, HIGH);
                // Program GPIO50 output high, 1: High, 0: Low
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + " failed.");
                    return;
                }
                GpioData |= 0x00040000;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
            else
            {
                GPIO50_OUT.Text = "Low";
                Console.WriteLine("Set GPIO50 to [Low]");
                SetGpioOut(50, LOW);
                // Program GPIO50 output low, 1: High, 0: Low
                GpioData = 0x00000000;
                RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
                if (RetStatus != 0)
                {
                    Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + " failed.");
                    return;
                }
                GpioData &= 0xFFFBFFFF;
                SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            }
        }

        private void GPIO50_STATE_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void GPIO50_UPDATE_Click(object sender, EventArgs e)
        {
            // Read GPIO50 state, 1: High, 0: Low
            if (GetGpioState(50))
            {
                GPIO50_STATE.Checked = true;
                Console.WriteLine("Get GPIO50 State: [high]");
            }
            else
            {
                GPIO50_STATE.Checked = false;
                Console.WriteLine("Get GPIO50 State: [high]");
            }
        }


        private void PromptInfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
