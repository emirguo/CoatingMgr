using HslCommunication;
using HslCommunication.Profinet.Keyence;
using System;
using System.IO.Ports;

namespace CoatingMgr
{
    class PLCHelper
    {
        public static PLCHelper mInstance = null;

        //基恩士PLC
        //private KeyenceNanoSerial PLC = null;// new KeyenceNanoSerial();////基恩士PLC串口

        //如果采用了二进制读写，那么就实例化KeyenceMcNet类，如果采用ASCII来读写数据，请使用KeyenceMcAsciiNet类
        private KeyenceMcNet PLC = null;
        //private KeyenceMcAsciiNet PLC = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public PLCHelper()
        {
            if (PLC == null)
            {
                PLC = new KeyenceMcNet("192.168.0.12", 5000);//基恩士PLC TCP
                //PLC = new KeyenceMcAsciiNet("192.168.0.12", 5000);//基恩士PLC TCP
                Connect();
                
                /*
                PLC = new KeyenceNanoSerial();//基恩士PLC串口
                PLC.SerialPortInni("COM4");//COM1,9600
                PLC.Open();
                */
            }
        }

        public static PLCHelper GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new PLCHelper();
            }
            return mInstance;
        }

        private bool Connect()
        {
            bool result = false;
            OperateResult connect = PLC.ConnectServer();
            if (connect.IsSuccess)
            {
                PLC.SetPersistentConnection();//设置为长连接
                result = true;
            }
            else
            {
                Console.WriteLine("connect failed:" + connect.Message);
            }
            return result;
        }

        public void Close()
        {
                PLC.ConnectClose();
        }

        public int GetWeight()
        {
            uint uint_DM18 = PLC.ReadUInt32("D00018").Content;          // 读取称重整体重量
            return (int)uint_DM18;
        }

        //各色剂分注入快慢分别设置重量
        public void SetWeight(int w1, int w2, int w3, int w4, int fw1, int fw2, int fw3, int fw4)
        {
            PLC.Write("D20", (uint)w1);              // 初始油漆重量
            PLC.Write("D26", (uint)w2);              // 固化剂重量
            PLC.Write("D28", (uint)w3);              // 稀释剂Ａ重量
            PLC.Write("D30", (uint)w4);              // 稀释剂Ｂ重量

            PLC.Write("D202", (uint)fw1);             //快速油漆重量
            PLC.Write("D206", (uint)fw2);             // 快速固化剂
            PLC.Write("D212", (uint)fw3);             // 快速稀释剂Ａ
            PLC.Write("D300", (uint)fw4);             // 快速稀释剂Ｂ
        }

        public void CoatingFastOn()
        {
            PLC.Write("M0", (uint)1);               // 手动快速油漆阀开
            PLC.Write("M3", (uint)1);               // 手动慢速油漆阀关
            PLC.Write("M100", (float)1);             // 手动油漆泵开
        }

        public void CoatingFastOff()
        {
            PLC.Write("M100", (float)0);               // 手动油漆泵关
            PLC.Write("M1", (uint)1);               // 手动快速油漆阀关
        }

        public void CoatingSlowOn()
        {
            PLC.Write("M1", (uint)1);               // 手动快速油漆阀关
            PLC.Write("M2", (uint)1);               // 手动慢速油漆阀开
            PLC.Write("M100", (float)1);             // 手动油漆泵开
        }

        public void CoatingSlowOff()
        {
            PLC.Write("M100", (float)0);               // 手动油漆泵关
            PLC.Write("M3", (uint)1);               // 手动慢速油漆阀关
        }

        public void HardeningAgentFastOn()
        {
            PLC.Write("M7", (uint)1);                // 手动慢速固化剂阀关
            PLC.Write("M4", (uint)1);               // 手动快速固化剂阀开
            PLC.Write("M102", (float)1);             // 手动固化剂泵开
        }

        public void HardeningAgentFastOff()
        {
            PLC.Write("M102", (float)0);               // 手动固化剂泵关
            PLC.Write("M5", (uint)1);               // 手动快速固化剂阀关
        }

        public void HardeningAgentSlowOn()
        {
            PLC.Write("M5", (uint)1);               // 手动快速固化剂阀关
            PLC.Write("M6", (uint)1);               // 手动慢速固化剂阀开
            PLC.Write("M102", (float)1);             // 手动固化剂泵开
        }

        public void HardeningAgentSlowOff()
        {
            PLC.Write("M102", (float)0);               // 手动固化剂泵关
            PLC.Write("M7", (uint)1);                // 手动慢速固化剂阀关
        }

        public void ThinnerAFastOn()
        {
            PLC.Write("M11", (uint)1);               // 手动慢速稀释剂Ａ阀关
            PLC.Write("M8", (uint)1);               // 手动快速稀释剂A阀开
            PLC.Write("M104", (float)1);             // 手动稀释剂Ａ泵开
        }

        public void ThinnerAFastOff()
        {
            PLC.Write("M104", (float)0);               // 手动稀释剂Ａ泵关
            PLC.Write("M9", (uint)1);               // 手动快速稀释剂A阀关
        }

        public void ThinnerASlowOn()
        {
            PLC.Write("M9", (uint)1);               // 手动快速稀释剂A阀关
            PLC.Write("M10", (uint)1);               // 手动慢速稀释剂Ａ阀开
            PLC.Write("M104", (float)1);             // 手动稀释剂Ａ泵开
        }

        public void ThinnerASlowOff()
        {
            PLC.Write("M104", (float)0);               // 手动稀释剂Ａ泵关
            PLC.Write("M11", (uint)1);               // 手动慢速稀释剂Ａ阀关
        }

        public void ThinnerBFastOn()
        {
            PLC.Write("M15", (uint)1);               // 手动慢速稀释剂Ｂ阀关
            PLC.Write("M12", (uint)1);               // 手动快速稀释剂Ｂ阀开
            PLC.Write("M106", (float)1);             // 手动稀释剂Ｂ泵开
        }

        public void ThinnerBFastOff()
        {
            PLC.Write("M106", (float)0);               // 手动稀释剂Ｂ泵关
            PLC.Write("M13", (uint)1);               // 手动快速稀释剂Ｂ阀关
        }

        public void ThinnerBSlowOn()
        {
            PLC.Write("M13", (uint)1);               // 手动快速稀释剂Ｂ阀关
            PLC.Write("M14", (uint)1);               // 手动慢速稀释剂Ｂ阀开
            PLC.Write("M106", (float)1);             // 手动稀释剂Ｂ泵开
        }

        public void ThinnerBSlowOff()
        {
            PLC.Write("M106", (float)0);               // 手动稀释剂Ｂ泵关
            PLC.Write("M15", (uint)1);               // 手动慢速稀释剂Ｂ阀关
        }

        public void Stop()
        {
            CoatingFastOff();
            CoatingSlowOff();
            HardeningAgentFastOff();
            HardeningAgentSlowOff();
            ThinnerAFastOff();
            ThinnerASlowOff();
            ThinnerBFastOff();
            ThinnerBSlowOff();
        }
    }
}
