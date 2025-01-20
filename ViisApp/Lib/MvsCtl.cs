using Microsoft.VisualBasic.Logging;
using MvCamCtrl.NET;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViisApp.Lib
{
    public class MvsCtl: IDisposable
    {
        MyCamera my = new MyCamera();

        public bool m_bGrabbing = false;
        Thread m_hReceiveThread = null;

        // 缓存
        IntPtr m_BufForDriver = IntPtr.Zero;
        static Object BufForDriverLock = new Object();
        UInt32 m_nBufSizeForDriver = 0;

        // 帧
        MyCamera.MV_FRAME_OUT_INFO_EX m_stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

        // 配置信息
        SettingStore m_stSettingStore = new SettingStore();

        
        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        public MvsCtl()
        {
            
        }

        // 枚举摄像头
        public MyCamera.MV_CC_DEVICE_INFO_LIST EnumDevices()
        {
            // ch:创建设备列表 | en:Create Device List
            System.GC.Collect();
            MyCamera.MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            m_stDeviceList.nDeviceNum = 0;
            int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_stDeviceList);
            if (0!= nRet)
            {
                throw new Exception(ShowErrorMsg("列举设备失败：", nRet));
            }

            return m_stDeviceList;
        }

        // 打开相机
        public MyCamera.MV_CC_DEVICE_INFO OpenDevice(MyCamera.MV_CC_DEVICE_INFO device)
        {
            int nRet = my.MV_CC_CreateDevice_NET(ref device);
            if (MyCamera.MV_OK != nRet)
            {
                throw new Exception("创建设备失败。");
            }

            nRet = my.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                my.MV_CC_DestroyDevice_NET();
                throw new Exception(ShowErrorMsg("打开设备失败：", nRet));
            }

            // 探测网络最佳包大小(只对GigE相机有效)
            if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                int nPacketSize = my.MV_CC_GetOptimalPacketSize_NET();
                if (nPacketSize > 0)
                {
                    nRet = my.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                    if (nRet != MyCamera.MV_OK)
                    {
                        throw new Exception(ShowErrorMsg("设置GigE包大小失败：", nRet));
                    }
                }
                else
                {
                    throw new Exception(ShowErrorMsg("获取GigE包大小失败：", nPacketSize));
                }
            }

            // 设置采集连续模式
            my.MV_CC_SetEnumValue_NET("AcquisitionMode", (uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
            my.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);

            return device;
        }

        // 关闭相机
        public void CloseDevice()
        {
            // 取流标志位清零
            if (m_bGrabbing == true)
            {
                m_bGrabbing = false;
                m_hReceiveThread.Join();
            }

            // 关闭设备
            my.MV_CC_CloseDevice_NET();
            my.MV_CC_DestroyDevice_NET();

            Marshal.FreeHGlobal(m_BufForDriver);
        }

        public void StartGrabbing(bool isTriggerModel = false)
        {
            // ch:标志位置位true | en:Set position bit true
            m_bGrabbing = true;

            m_hReceiveThread = new Thread(ReceiveThreadProcess);
            m_hReceiveThread.Start();

            m_stFrameInfo.nFrameLen = 0;//取流之前先清除帧长度
            m_stFrameInfo.enPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Undefined;

            if (isTriggerModel)
            {
                my.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
                my.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
            }

            // ch:开始采集 | en:Start Grabbing
            int nRet = my.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                m_bGrabbing = false;
                m_hReceiveThread.Join();
                new Exception(ShowErrorMsg("启动取流失败：", nRet));
                return;
            }
        }

        public void StopGrabbing()
        {
            // ch:标志位设为false | en:Set flag bit false
            m_bGrabbing = false;

            if (m_hReceiveThread != null)
            {
                m_hReceiveThread.Join();
            }

            // ch:停止采集 | en:Stop Grabbing
            int nRet = my.MV_CC_StopGrabbing_NET();
            if (nRet != MyCamera.MV_OK)
            {
                new Exception(ShowErrorMsg("停止取流失败：", nRet));
            }
        }

        public void ReceiveThreadProcess()
        {
            MyCamera.MV_FRAME_OUT stFrameInfo = new MyCamera.MV_FRAME_OUT();
            MyCamera.MV_DISPLAY_FRAME_INFO stDisplayInfo = new MyCamera.MV_DISPLAY_FRAME_INFO();
            int nRet = MyCamera.MV_OK;

            while (m_bGrabbing)
            {
                nRet = my.MV_CC_GetImageBuffer_NET(ref stFrameInfo, 1000);
                if (nRet == MyCamera.MV_OK)
                {
                    lock (BufForDriverLock)
                    {
                        if (m_BufForDriver == IntPtr.Zero || stFrameInfo.stFrameInfo.nFrameLen > m_nBufSizeForDriver)
                        {
                            if (m_BufForDriver != IntPtr.Zero)
                            {
                                Marshal.FreeHGlobal(m_BufForDriver);
                                m_BufForDriver = IntPtr.Zero;
                            }

                            m_BufForDriver = Marshal.AllocHGlobal((Int32)stFrameInfo.stFrameInfo.nFrameLen);

                            if (m_BufForDriver == IntPtr.Zero)
                            {
                                return;
                            }

                            m_nBufSizeForDriver = stFrameInfo.stFrameInfo.nFrameLen;


                        }

                        m_stFrameInfo = stFrameInfo.stFrameInfo;
                        CopyMemory(m_BufForDriver, stFrameInfo.pBufAddr, stFrameInfo.stFrameInfo.nFrameLen);

                        SaveFrame();
                    }
                    
                    my.MV_CC_FreeImageBuffer_NET(ref stFrameInfo);
                }
            }
        }

        public void SaveFrame(string picType = ".bmp")
        {
            try
            {
                if (false == m_bGrabbing)
                {
                    return;
                    //throw new Exception(ShowErrorMsg("Not Start Grabbing", 0));
                }

                if (RemoveCustomPixelFormats(m_stFrameInfo.enPixelType))
                {
                    ShowErrorMsg("Not Support!", 0);
                    return;
                }

                MyCamera.MV_SAVE_IMG_TO_FILE_PARAM stSaveFileParam = new MyCamera.MV_SAVE_IMG_TO_FILE_PARAM();

                lock (BufForDriverLock)
                {
                    if (m_stFrameInfo.nFrameLen == 0)
                    {
                        ShowErrorMsg("保存图片失败：", 0);
                        return;
                    }

                    stSaveFileParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Bmp;
                    stSaveFileParam.enPixelType = m_stFrameInfo.enPixelType;
                    stSaveFileParam.pData = m_BufForDriver;
                    stSaveFileParam.nDataLen = m_stFrameInfo.nFrameLen;
                    stSaveFileParam.nHeight = m_stFrameInfo.nHeight;
                    stSaveFileParam.nWidth = m_stFrameInfo.nWidth;
                    stSaveFileParam.iMethodValue = 2;
                    stSaveFileParam.pImagePath = Path.Combine(
                        m_stSettingStore.GetSetting().CamPath,
                            "Image_w" +
                            stSaveFileParam.nWidth.ToString() +
                            "_h" + stSaveFileParam.nHeight.ToString() +
                            "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") +
                            picType);
                    int nRet = my.MV_CC_SaveImageToFile_NET(ref stSaveFileParam);
                    if (MyCamera.MV_OK != nRet)
                    {
                        ShowErrorMsg("保存图片失败:", nRet);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMsg("保存图片失败:", 0);
            }
        }

        private bool RemoveCustomPixelFormats(MyCamera.MvGvspPixelType enPixelFormat)
        {
            Int32 nResult = ((int)enPixelFormat) & (unchecked((Int32)0x80000000));
            if (0x80000000 == nResult)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string ShowErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg;
            if (nErrorNum == 0)
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            switch (nErrorNum)
            {
                case MyCamera.MV_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                case MyCamera.MV_E_SUPPORT: errorMsg += " Not supported function "; break;
                case MyCamera.MV_E_BUFOVER: errorMsg += " Cache is full "; break;
                case MyCamera.MV_E_CALLORDER: errorMsg += " Function calling order error "; break;
                case MyCamera.MV_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                case MyCamera.MV_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                case MyCamera.MV_E_NODATA: errorMsg += " No data "; break;
                case MyCamera.MV_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                case MyCamera.MV_E_VERSION: errorMsg += " Version mismatches "; break;
                case MyCamera.MV_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                case MyCamera.MV_E_UNKNOW: errorMsg += " Unknown error "; break;
                case MyCamera.MV_E_GC_GENERIC: errorMsg += " General error "; break;
                case MyCamera.MV_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                case MyCamera.MV_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                case MyCamera.MV_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                case MyCamera.MV_E_NETER: errorMsg += " Network error "; break;
            }

            // 记录错误日志

            LogHelper.GetLogger("Camera").Error(errorMsg);

            return errorMsg;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
               // CloseDevice();
               //Marshal.FreeHGlobal(m_BufForDriver);
            }
        }

        internal void TriggerModel(bool @checked)
        {
            if (@checked)
            {
                my.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
                my.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
            }
            else
            {
                my.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
            }
        }
    }
}
