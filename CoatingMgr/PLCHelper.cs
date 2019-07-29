using HslCommunication;
using HslCommunication.Profinet.Keyence;
using System;
using System.IO.Ports;

namespace CoatingMgr
{
    class PLCHelper
    {
        public static PLCHelper mInstance = null;

        //private MelsecMcNet melsecMc = new MelsecMcNet("192.168.1.110", 6000);//三菱PLC

        //private SiemensS7Net siemens = new SiemensS7Net(SiemensPLCS.S1200, " 192.168.1.110");//西门子PLC

        //基恩士PLC
        //private KeyenceNanoSerial PLC = null;// new KeyenceNanoSerial();////基恩士PLC串口
        //如果采用了二进制读写，那么就实例化KeyenceMcNet类，如果采用ASCII来读写数据，请使用KeyenceMcAsciiNet类
        private KeyenceMcNet PLC = null;// = new KeyenceMcNet("192.168.1.110", 8500);


        /// <summary>
        /// 构造函数
        /// </summary>
        public PLCHelper()
        {
            if (PLC == null)
            {
                PLC = new KeyenceMcNet("192.168.1.110", 8500);//基恩士PLC TCP
                PLCConnect();
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

        public bool PLCConnect()
        {
            bool result = false;
            OperateResult connect = PLC.ConnectServer();
            if (connect.IsSuccess)
            {
                result = true;
            }
            else
            {
                Console.WriteLine("connect failed:" + connect.Message);
            }
            return result;
        }

        public void PLCClose()
        {
                PLC.ConnectClose();
        }

        public void PLCRead()
        {
            // 此处以D寄存器作为示例
            short short_D1000 = PLC.ReadInt16("DM18").Content;         // 读取D1000的short值
            ushort ushort_D1000 = PLC.ReadUInt16("DM18").Content;      // 读取D1000的ushort值
            int int_D1000 = PLC.ReadInt32("DM18").Content;             // 读取D1000-D1001组成的int数据
            uint uint_D1000 = PLC.ReadUInt32("DM18").Content;          // 读取D1000-D1001组成的uint数据
            float float_D1000 = PLC.ReadFloat("DM18").Content;         // 读取D1000-D1001组成的float数据
            long long_D1000 = PLC.ReadInt64("DM18").Content;           // 读取D1000-D1003组成的long数据
            ulong ulong_D1000 = PLC.ReadUInt64("DM18").Content;        // 读取D1000-D1003组成的long数据
            double double_D1000 = PLC.ReadDouble("DM18").Content;      // 读取D1000-D1003组成的double数据
            string str_D1000 = PLC.ReadString("DM18", 10).Content;     // 读取D1000-D1009组成的条码数据

            /*
            // 读取数组
            short[] short_D1000_array = PLC.ReadInt16("D1000", 10).Content;         // 读取D1000的short值
            ushort[] ushort_D1000_array = PLC.ReadUInt16("D1000", 10).Content;      // 读取D1000的ushort值
            int[] int_D1000_array = PLC.ReadInt32("D1000", 10).Content;             // 读取D1000-D1001组成的int数据
            uint[] uint_D1000_array = PLC.ReadUInt32("D1000", 10).Content;          // 读取D1000-D1001组成的uint数据
            float[] float_D1000_array = PLC.ReadFloat("D1000", 10).Content;         // 读取D1000-D1001组成的float数据
            long[] long_D1000_array = PLC.ReadInt64("D1000", 10).Content;           // 读取D1000-D1003组成的long数据
            ulong[] ulong_D1000_array = PLC.ReadUInt64("D1000", 10).Content;        // 读取D1000-D1003组成的long数据
            double[] double_D1000_array = PLC.ReadDouble("D1000", 10).Content;      // 读取D1000-D1003组成的double数据
            */
        }

        public void PLCWrite()
        {
            // 此处以D寄存器作为示例
            PLC.Write("DM18", (short)1);                // 写入D1000  short值  ,W3C0,R3C0 效果是一样的
            PLC.Write("DM18", (ushort)1);              // 写入D1000  ushort值
            PLC.Write("DM18", 1);                    // 写入D1000  int值
            PLC.Write("DM18", (uint)1);               // 写入D1000  uint值
            PLC.Write("DM18", 123.456f);                    // 写入D1000  float值
            PLC.Write("DM18", 123.456d);                    // 写入D1000  double值
            PLC.Write("DM18", 123456661235123534L);          // 写入D1000  long值
            PLC.Write("DM18", 523456661235123534UL);          // 写入D1000  ulong值
            PLC.Write("DM18", "K123456789");                // 写入D1000  string值
            /*
            // 读取数组
            PLC.Write("D1000", new short[] { 123, 3566, -123 });                // 写入D1000  short值  ,W3C0,R3C0 效果是一样的
            PLC.Write("D1000", new ushort[] { 12242, 42321, 12323 });              // 写入D1000  ushort值
            PLC.Write("D1000", new int[] { 1234312312, 12312312, -1237213 });                    // 写入D1000  int值
            PLC.Write("D1000", new uint[] { 523123212, 213, 13123 });               // 写入D1000  uint值
            PLC.Write("D1000", new float[] { 123.456f, 35.3f, -675.2f });                    // 写入D1000  float值
            PLC.Write("D1000", new double[] { 12343.542312d, 213123.123d, -231232.53432d });                    // 写入D1000  double值
            PLC.Write("D1000", new long[] { 1231231242312, 34312312323214, -1283862312631823 });          // 写入D1000  long值
            PLC.Write("D1000", new ulong[] { 1231231242312, 34312312323214, 9731283862312631823 });          // 写入D1000  ulong值
            */
        }

        //各色剂分注入快慢分别设置重量
        public void PLCSetWeight()
        {

        }

    }
}
