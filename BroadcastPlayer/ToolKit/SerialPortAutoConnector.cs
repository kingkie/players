using CinemaSetter.Runtime.DataStructrues;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CinemaSetter.ToolKit
{

    public class SPACInstance
    {
        /// <summary>
        /// 遥控器匹配
        /// </summary>
        /// <param name="OnSuccess"></param>
        /// <param name="OnTimeout"></param>
        public static void ConnectController(VoidSerialPortObjectDelegate OnSuccess, VoidInt32Delegate OnTimeout)
        {
            SerialPortAutoConnector SPAC = new SerialPortAutoConnector(115200);//波特率
            SPAC.DataMatcher = MatchController;
            SPAC.OnSuccess = OnSuccess;
            SPAC.SendIns = MatchControllerData();
            SPAC.OnTimeout = OnTimeout;
            SPAC.AsyncRun();
        }
        /// <summary>
        /// 设备匹配
        /// </summary>
        /// <param name="OnSuccess"></param>
        /// <param name="OnTimeout"></param>
        /// <param name="BaudRate"></param>
        public static void ConnectDevice(
            VoidSerialPortObjectDelegate OnSuccess,
            VoidInt32Delegate OnTimeout,
            Int32 BaudRate = 19200)
        {

            SerialPortAutoConnector SPAC = new SerialPortAutoConnector(BaudRate);
            SPAC.DataMatcher = MatchDevice;
            SPAC.OnSuccess = OnSuccess;
            SPAC.SendIns = MatchDeviceData();
            SPAC.OnTimeout = OnTimeout;
            SPAC.AsyncRun();
        }


        public static void ConnectADPlayer(VoidSerialPortObjectDelegate OnSuccess, VoidInt32Delegate OnTimeout)
        {
            SerialPortAutoConnector SPAC = new SerialPortAutoConnector();
            SPAC.DataMatcher = MatchADPlayer;
            SPAC.OnSuccess = OnSuccess;
            SPAC.SendIns = ADPlayerSerialPort.AssembleStopPlaySmellIns();
            SPAC.OnTimeout = OnTimeout;
            SPAC.AsyncRun();
        }
        #region 遥控器匹配
        public static Byte[] MatchControllerData()
        {
            return new Byte[] { 0xf5, 0x34, 0x00, 0x34, 0x55 };
        }

        public static Boolean MatchController(Byte[] buf, out object obj)
        {
            obj = null;
            if (buf.Length == 6
                && 0xf5 == buf[0]
                && 0xb4 == buf[1]
                && 0x55 == buf[5])
            {
                return true;
            }
            return false;
        }

        #endregion End

        static ADPlayerSerialPort adsp = new ADPlayerSerialPort();

        public static Byte[] MatchDeviceData()
        {
            return new Byte[] { 0xf5, 0x01, 0x00, 0x00, 0x02, 0x00, 0x01, 0x01, 0x00, 0x05, 0x55 };
        }
        public static Boolean MatchDevice(Byte[] buf, out object obj)
        {
            obj = null;
            if (buf.Length >= 27 && buf[0] == 0x49
            && buf[1] == 0x44
            && buf[2] == 0x3d
            )
            {
                string aaa = "";
                foreach (byte item in buf)
                {
                    aaa += (char)item;
                }
                aaa = aaa.Substring(3, 24);
                obj = aaa;
                return true;
            }
            return false;
        }


        public static Boolean MatchADPlayer(Byte[] buf, out object obj)
        {
            return adsp.Validate(buf, out obj);
        }

    }

    public class SerialPortAutoConnector
    {

        private SerialPort SP { get; set; }

        private AutoResetEvent ARE = new AutoResetEvent(false);
        private List<SerialPort> ProcessingPorts = new List<SerialPort>();

        public BooleanByteArrayOutObjDelegate DataMatcher { get; set; }

        public VoidSerialPortObjectDelegate OnSuccess { get; set; }

        public VoidInt32Delegate OnTimeout { get; set; }

        public static Int32 DefaultBuadRate { get; set; } = 19200;

        public Int32 BuadRate { get; set; } = DefaultBuadRate;

        public Byte[] SendIns { get; set; }

        public Int32 Timeout { get; set; } = 1500;


        public SerialPortAutoConnector()
        {

        }

        public SerialPortAutoConnector(Int32 BuadRate)
        {
            this.BuadRate = BuadRate;
        }

        public void AsyncRun()
        {
            Tools.NewThread(() => {
                RunAutoConnect();
            });
        }

        public void RunAutoConnect()
        {
            String[] Names = SerialPort.GetPortNames();
            ARE.Reset();
            foreach (String Name in Names)
            {
                SerialPort SP = new SerialPort();
                SP.PortName = Name;
                SP.BaudRate = BuadRate;
                try
                {
                    SP.DataReceived += SerialPort_DataRecived;
                    SP.Open();
                    SP.Write(SendIns, 0, SendIns.Length);
                    ProcessingPorts.Add(SP);
                }
                catch (Exception Ex)
                {
                    Tools.TLog(Ex.Message);
                }
            }
            Boolean Ret = ARE.WaitOne(Timeout);
            foreach (SerialPort sp in ProcessingPorts)
            {
                if (SP != null && SP.PortName == sp.PortName)
                {
                    continue;
                }

                if (sp.IsOpen)
                    sp.Close();
            }
            if (!Ret && null != OnTimeout)
                OnTimeout.Invoke(Timeout);
            SP = null;
        }

        private Int64 LastCurTS = 0;
        private List<Byte> ReceiveBuffer = new List<byte>();

        private Dictionary<String, Int64> LastCurTSForSPS = new Dictionary<string, long>();
        private Dictionary<String, List<Byte>> ReceiveBufferForSPS = new Dictionary<string, List<byte>>();

        private void AddRestData(String PortName, Byte[] buf)
        {
            Int64 CurTS = Tools.GetTimeStamp();
            if (CurTS - GetLastCurTS(PortName) >= 500)
            {
                ClearRestData(PortName);
            }
            LastCurTSForSPS[PortName] = CurTS;
            GetReceiveBuffer(PortName).AddRange(buf);
        }

        private List<Byte> GetReceiveBuffer(String PortName)
        {
            if (!ReceiveBufferForSPS.ContainsKey(PortName))
                ReceiveBufferForSPS.Add(PortName, new List<byte>());

            return ReceiveBufferForSPS[PortName];
        }

        private Int64 GetLastCurTS(String PortName)
        {
            if (!LastCurTSForSPS.ContainsKey(PortName))
                LastCurTSForSPS.Add(PortName, 0);

            return LastCurTSForSPS[PortName];
        }

        private void ClearRestData(String PortName)
        {
            if (ReceiveBufferForSPS.ContainsKey(PortName))
                ReceiveBufferForSPS[PortName].Clear();

        }


        private void SerialPort_DataRecived(Object Sender, EventArgs e)
        {
            SerialPort serialPort1 = (SerialPort)Sender;
            byte[] buf = null;
            //接收数据
            if (!serialPort1.IsOpen)
                return;
            Int32 BytesToRead = serialPort1.BytesToRead;
            buf = new byte[BytesToRead];
            serialPort1.Read(buf, 0, BytesToRead);
            Object outObj = null;

            String PortName = serialPort1.PortName;
            AddRestData(PortName, buf);
            Byte[] ProcessBuffer = GetReceiveBuffer(PortName).ToArray();
            Tools.TLog("AUTO[R]:");
            Tools.PrintByteArray(ProcessBuffer);


            if (DataMatcher != null && DataMatcher(ProcessBuffer, out outObj))
            {
                ClearRestData(PortName);
                if (null != OnSuccess)
                {
                    SP = serialPort1;
                    SP.DataReceived -= SerialPort_DataRecived;
                    OnSuccess.Invoke(serialPort1, outObj);
                    ARE.Set();
                }
            }
        }
    }
}
