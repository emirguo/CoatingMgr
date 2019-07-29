using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoatingMgr
{
    class KeyenceSerialPort
    {
        private SerialPort sp = null;

        public KeyenceSerialPort()
        {
            if (sp == null)
            {
                sp = new SerialPort();
                sp.PortName = "COM4";  //端口号
                sp.BaudRate = 9600;   //比特率
                sp.DataBits = 8;//数据位8
                sp.Parity = Parity.None;//奇偶校验
                sp.StopBits = StopBits.One;//停止位
                sp.Handshake = Handshake.None; //握手方式，也就是数据流控制方式，可以选取枚举Handshake中的值
                sp.ReadTimeout = 1000; //读超时，即在1000内未读到数据就引起超时异常


                sp.DataReceived += SP_DataReceived;
                sp.Open();
                sp.RtsEnable = true;
            }

        }
            /// <summary>  
            /// 向串口内写入  
            /// </summary>  
            /// <param name="send">写入数据</param>  
            /// <param name="offSet">偏移量</param>  
            /// <param name="count">写入数量</param>  
            public void Write(byte[] send, int offSet, int count)
            {
                if (this.sp.IsOpen)
                {
                    sp.Write(send, offSet, count);
                }
            }


            public void Dispose()
            {
                if (this.sp == null)
                    return;
                if (this.sp.IsOpen)
                    this.sp.Close();
                this.sp.Dispose();
                this.sp = null;
            }

            void SP_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
            {
                // 等待100ms，防止读取不全的情况  
                Thread.Sleep(100);
                ReceiveDate();
            }


             void ReceiveDate()
            {
                byte[] m_recvBytes = new byte[sp.BytesToRead]; //定义缓冲区大小  
                int result = sp.Read(m_recvBytes, 0, m_recvBytes.Length); //从串口读取数据  
                if (result <= 0)
                    return;
                string strResult = Encoding.UTF8.GetString(m_recvBytes, 0, m_recvBytes.Length); //对数据进行转换  
                sp.DiscardInBuffer();

            /*
                if (this.DataReceived != null)
                    this.DataReceived(this, new SerialSortEventArgs() { Code = strResult });
                    */
            }

            //public event EventHandler<SerialSortEventArgs> DataReceived;

    }
}
