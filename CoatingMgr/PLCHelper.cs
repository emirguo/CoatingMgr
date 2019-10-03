using HslCommunication;
using HslCommunication.Profinet.Keyence;
using System;

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
        private readonly string ipaddr = string.Empty;
        private readonly int port = 0;

        private OperateResult connect;
        /// <summary>
        /// 构造函数
        /// </summary>
        public PLCHelper()
        {
            if (PLC == null)
            {
                if (!Properties.Settings.Default.PLCIP.Equals(string.Empty) && !Properties.Settings.Default.PLCPort.Equals(string.Empty))
                {
                    ipaddr = Properties.Settings.Default.PLCIP;
                    port = Properties.Settings.Default.PLCPort;

                    PLC = new KeyenceMcNet(ipaddr, port);//基恩士PLC TCP
                    PLC.ConnectTimeOut = 5000;
                    //PLC = new KeyenceMcAsciiNet("192.168.0.12", 5000);//基恩士PLC TCP
                    Connect();

                    /*
                    PLC = new KeyenceNanoSerial();//基恩士PLC串口
                    PLC.SerialPortInni("COM4");//COM1,9600
                    PLC.Open();
                    */
                }
            }
        }

        public static PLCHelper GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new PLCHelper();
            }
            if (!mInstance.IsPLCConnect())
            {
                mInstance.Connect();
            }
            return mInstance;
        }

        public bool IsPLCConnect()
        {
            return connect.IsSuccess;
        }

        private bool Connect()
        {
            bool result = false;
            connect = PLC.ConnectServer();
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
        public bool SetWeight(int w1, int w2, int w3, int w4, int sw1, int sw2, int sw3, int sw4)
        {
            bool result = false;
            if (IsPLCConnect())
            {
                PLC.Write("D170", (uint)w1);              // 设定油漆重量
                PLC.Write("D171", (uint)sw1);             // 慢速油漆重量

                PLC.Write("D172", (uint)w2);              // 设定固化剂重量
                PLC.Write("D173", (uint)sw2);             // 慢速固化剂

                PLC.Write("D174", (uint)w3);              // 设定稀释剂Ａ重量
                PLC.Write("D175", (uint)sw3);             // 慢速稀释剂Ａ

                PLC.Write("D176", (uint)w4);              // 设定稀释剂Ｂ重量
                PLC.Write("D177", (uint)sw4);             // 慢速稀释剂Ｂ
                result = true;
            }
            return result;
        }

        public void CoatingFastOn()
        {
            PLC.Write("M0", (uint)1);               // 手动油漆泵开
            PLC.Write("M4", (uint)1);               // 手动快速油漆阀开
        }

        public void CoatingFastOff()
        {
            PLC.Write("M0", (uint)0);            // 手动油漆泵关
            PLC.Write("M4", (uint)0);               // 手动快速油漆阀关
        }

        public void CoatingSlowOn()
        {
            PLC.Write("M0", (uint)1);               // 手动油漆泵开
            PLC.Write("M5", (uint)1);               // 手动慢速油漆阀开
        }

        public void CoatingSlowOff()
        {
            PLC.Write("M0", (uint)0);               // 手动油漆泵关
            PLC.Write("M5", (uint)0);               // 手动慢速油漆阀关
        }

        public void HardeningAgentFastOn()
        {
            PLC.Write("M1", (uint)1);                // 手动固化剂泵开
            PLC.Write("M6", (uint)1);               // 手动快速固化剂阀开
        }

        public void HardeningAgentFastOff()
        {
            PLC.Write("M1", (uint)0);               // 手动固化剂泵关
            PLC.Write("M6", (uint)0);               // 手动快速固化剂阀关
        }

        public void HardeningAgentSlowOn()
        {
            PLC.Write("M1", (uint)1);               // 手动固化剂泵开
            PLC.Write("M7", (uint)1);               // 手动慢速固化剂阀开
        }

        public void HardeningAgentSlowOff()
        {
            PLC.Write("M1", (uint)0);               // 手动固化剂泵关
            PLC.Write("M7", (uint)0);                // 手动慢速固化剂阀关
        }

        public void ThinnerAFastOn()
        {
            PLC.Write("M2", (uint)1);               // 手动稀释剂Ａ泵开
            PLC.Write("M8", (uint)1);               // 手动快速稀释剂A阀开
        }

        public void ThinnerAFastOff()
        {
            PLC.Write("M2", (uint)0);               // 手动稀释剂Ａ泵关
            PLC.Write("M8", (uint)0);               // 手动快速稀释剂A阀关
        }

        public void ThinnerASlowOn()
        {
            PLC.Write("M2", (uint)1);               // 手动稀释剂Ａ泵开
            PLC.Write("M9", (uint)1);               // 手动慢速稀释剂Ａ阀开
        }

        public void ThinnerASlowOff()
        {
            PLC.Write("M2", (uint)0);               // 手动稀释剂Ａ泵关
            PLC.Write("M9", (uint)0);               // 手动慢速稀释剂Ａ阀关
        }

        public void ThinnerBFastOn()
        {
            PLC.Write("M3", (uint)1);               // 手动稀释剂Ｂ泵开
            PLC.Write("M10", (uint)1);               // 手动快速稀释剂Ｂ阀开
        }

        public void ThinnerBFastOff()
        {
            PLC.Write("M3", (uint)0);               // 手动稀释剂Ｂ泵关
            PLC.Write("M10", (uint)0);               // 手动快速稀释剂Ｂ阀关
        }

        public void ThinnerBSlowOn()
        {
            PLC.Write("M3", (uint)1);               // 手动稀释剂Ｂ泵开
            PLC.Write("M11", (uint)1);               // 手动慢速稀释剂Ｂ阀开
        }

        public void ThinnerBSlowOff()
        {
            PLC.Write("M3", (uint)0);               // 手动稀释剂Ｂ泵关
            PLC.Write("M11", (uint)0);               // 手动慢速稀释剂Ｂ阀关
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

        public void Stir()
        {
            PLC.Write("M12", (uint)1);             // 一键搅拌
        }

        public void SetStirTime(uint time)
        {
            PLC.Write("D722", time);             //设置搅拌时间，time*10
        }

        public void Clear()
        {
            PLC.Write("M13", (uint)1);            // 一键清洗
        }

        public void Blow()
        {
            PLC.Write("M14", (uint)1);            // 一键吹气
        }
    }
}
