using CinemaSetter.Runtime.DataStructrues;
using CinemaSetter.ToolKit;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSetter.Runtime
{

    public delegate Boolean BooleanByteArrayOutObjectDelegate(Byte[] Data,out Object obj);
    public delegate Boolean BooleanByteArrayByteOutObjectDelegate(Byte[] Data,Byte SFD, out Object obj);
    public delegate void VoidTMDelegate(TransactionMission tm);


    public enum TransactionMissionType : Byte {
        TMT_SendData = 0x00,
        TMT_WaitReceive = 0x01,
        TMT_ContinuousReceive = 0x02,
        TMT_NonBlockOrdinaryThing = 0x03,
        TMT_BlockOrdinaryThing = 0x04,
    }

    public enum MissionPriority : Byte
    {
        MP_RealTime = 0x00,
        MP_BackGround = 0x01,
    }

    public class TransactionMission
    {
        public static Int32 DefaultTimeoutMilliSecond { get; set; } = 1000;

        public TransactionMissionType Type { get; set; }
        public Byte[] TMData { get; set; }
        public DateTime AddAt { get; set; }
        public Int32 TimeoutMilliSecond { get; set; } = DefaultTimeoutMilliSecond;
        public Int32 DurationMillSecond { get; set; }
        public MissionPriority Priority { get; set; } = MissionPriority.MP_RealTime;
        public BooleanByteArrayOutObjectDelegate ResponseValidateHandler { get; set; }
        public BooleanByteArrayByteOutObjectDelegate ResponseListenHandler { get; set; }
        public VoidNoParamDelegate SendFailureHandler { get; set; } = DefaultSendFailureHandler;
        public VoidTMDelegate TimeoutHandler { get; set; } = DefaultTimeoutHandler;
        public VoidNoParamDelegate MissionFinishedHandler0 { get; set; }
        public VoidObjectDelegate MissionFinishedHandler1 { get; set; }


        public static Int32 SequenceNumber { get; set; } = 0;

        public TransactionMission(TransactionMissionType Type)
        {
            this.Type = Type;
            SequenceNumber++;
        }

        public TransactionMission()
        {
            AddAt = DateTime.Now;
            SequenceNumber++;
        }


        public static void DefaultTimeoutHandler(TransactionMission tm)
        {
            Tools.NonBlockingMsgBox("应答超时（" + tm.TimeoutMilliSecond / 1000 + "）");
        }

        public static void DefaultSendFailureHandler()
        {
            Tools.NonBlockingMsgBox("串口未打开");
        }
    }

    public class TransactionMissionPriorityQueue
    {
        public Int32 Count { get; set; } = 0;
        private Type PriorityEnum { get; set; }

        private Dictionary<Int32, Queue<TransactionMission>> QueueMap { get; set; }

        public TransactionMissionPriorityQueue(Type PriorityEnum)
        {
            Array EArray = Enum.GetValues(PriorityEnum);
            QueueMap = new Dictionary<int, Queue<TransactionMission>>();
            for (int i = 0; i < EArray.Length; i++)
            {
                QueueMap.Add(Convert.ToInt32(EArray.GetValue(i)), new Queue<TransactionMission>());
            }
        }

        public TransactionMission Dequeue()
        {
            foreach (KeyValuePair<Int32, Queue<TransactionMission>> kv in QueueMap)
            {
                if (kv.Value.Count > 0)
                {
                    Count--;
                    return kv.Value.Dequeue();
                }
            }
            return null;
        }

        public void Enqueue(TransactionMission tm)
        {
            Count++;
            QueueMap[Convert.ToInt32(tm.Priority)].Enqueue(tm);
        }


        public void Clear()
        {
            foreach (KeyValuePair<Int32, Queue<TransactionMission>> kv in QueueMap)
            {
                if (kv.Value.Count > 0)
                {
                    kv.Value.Clear();
                }
            }
        }

    }

    public class SerialCommunicationQueue
    {

        public SerialPort SP { get; set; }

        public TransactionMissionPriorityQueue MissionQueue = new TransactionMissionPriorityQueue(typeof(MissionPriority));

        public VoidByteArrayDelegate SendDataOutputHandler { get; set; } = null;

        public VoidByteArrayDelegate RecivedDataOutputHandler { get; set; } = null;

        private System.Threading.Thread CurrentThread { get; set; }

        private Boolean _IsRunning { get; set; }

        public Boolean IsRunning {
            get {
                return _IsRunning;
            }
        }

        public SerialCommunicationQueue(SerialPort serialPort)
        {
            SP = serialPort;
            SP.DataReceived += SerialPort_DataReceived;
        }

        public SerialCommunicationQueue(SerialPort serialPort, VoidByteArrayDelegate SendDataOutputHandler, VoidByteArrayDelegate RecivedDataOutputHandler)
        {
            SP = serialPort;
            SP.DataReceived += SerialPort_DataReceived;
            this.SendDataOutputHandler = SendDataOutputHandler;
            this.RecivedDataOutputHandler = RecivedDataOutputHandler;
        }

        public Boolean IsSerialPortOK()
        {
            if (SP == null)
                return false;
            if(!SP.IsOpen)
                return false;
            return true;
        }

        private TransactionMission CurrentMission = null;
        private Boolean CurrentMissionSuccess = false;



        private Int64 LastCurTS = 0;
        private List<Byte> ReceiveBuffer = new List<byte>();

      

        private void AddRestData(Byte[] buf)
        {
            Int64 CurTS = Tools.GetTimeStamp();
            if (CurTS - LastCurTS >= 1000)
            {
                ReceiveBuffer.Clear();
            }
            LastCurTS = CurTS;
            ReceiveBuffer.AddRange(buf);
        }



        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] buf = null;
            //接收数据
            Int32 BytesToRead = SP.BytesToRead;
            if (BytesToRead == 0)
                return;
            buf = new byte[BytesToRead];
            SP.Read(buf, 0, BytesToRead);

            if (null != RecivedDataOutputHandler)
            {
                Tools.NewThread(() =>
                {
                    RecivedDataOutputHandler.Invoke(buf);
                });
            }
            else
            {
                Tools.TLog("SerialPort_DataReceived ");
                Tools.PrintByteArray(buf);
            }

            if (ListenHandlers.Count > 0)
            {
                foreach (KeyValuePair<String, TransactionMission> kv in ListenHandlers)
                {
                    Tools.TLog("Listen [" + kv.Key + "] Hit");
                    Object obj;
                    if (kv.Value.ResponseListenHandler.Invoke(buf, kv.Value.TMData[0], out obj))
                    {
                        Tools.NewThread(() => {
                            kv.Value.MissionFinishedHandler1.Invoke(obj);
                        });
                        return;
                    }
                }
            }
            

            if (null != CurrentMission)
            {
                Object obj;

                AddRestData(buf);
                Byte[] ProcessBuffer = ReceiveBuffer.ToArray();

                if (CurrentMission.ResponseValidateHandler(ProcessBuffer, out obj))
                {
                    ReceiveBuffer.Clear();
                    if (obj is DeviceSerialPort)
                    {
                        DeviceSerialPort dsp = obj as DeviceSerialPort;
                        //ProcessBuffer dsp.InstructionLength
                        if (ProcessBuffer.Length > dsp.InstructionLength)
                        {
                            Byte[] RestBuf = new Byte[ProcessBuffer.Length - dsp.InstructionLength];
                            Array.Copy(ProcessBuffer, 0, RestBuf, 0, ProcessBuffer.Length - dsp.InstructionLength);
                            AddRestData(RestBuf);
                        }
                    }


                    if (obj is TTBroadcast)
                    {
                        TTBroadcast ttb = obj as TTBroadcast;
                        Byte FuncCodeR = ttb.FuncCode;// ttb.GetFuncCodeFromTTBArray(buf);
                        Byte FuncCodeS = ttb.GetFuncCodeFromTTBArray(CurrentMission.TMData);

                        Console.WriteLine(">>>FuncCodeS={0},FuncCodeR={1}", FuncCodeS, FuncCodeR);
                        if (FuncCodeS + 0x80 != FuncCodeR)
                        {
                            Tools.PrintByteArray(CurrentMission.TMData);
                            Tools.PrintByteArray(buf);
                            Console.WriteLine("=======<ERROR>========");
                            return;
                        }
                    }
                    else if (obj is DataStructrues.Interfaces.DataContract)
                    {
                        var oj = obj as DataStructrues.Interfaces.DataContract;
                        Byte FuncCodeS = oj.GetFuncCode(CurrentMission.TMData);
                        Byte FuncCodeR = Convert.ToByte(obj.GetType().GetProperty("FuncCode").GetValue(obj));// oj.GetFuncCode(buf);

                        Console.WriteLine(">>>FuncCodeS={0},FuncCodeR={1}", FuncCodeS, FuncCodeR);
                        if (FuncCodeS + 0x80 != FuncCodeR)
                        {
                            Tools.PrintByteArray(CurrentMission.TMData);
                            Tools.PrintByteArray(buf);

                            Console.WriteLine("=======<ERROR>========");
                            return;
                        }
                    }

                    if (null != CurrentMission.MissionFinishedHandler1)
                    {
                        TransactionMission CTM = CurrentMission;
                        CurrentMission = null;
                        Tools.NewThread(() =>
                        {
                            Tools.TLog("=========Running CurrentMission NonBlocking");
                            CTM.MissionFinishedHandler1.Invoke(obj);
                            Tools.TLog("=========CurrentMission Over");
                        });
                    }
                    CurrentMissionSuccess = true;
                    Tools.TLog("CurrentMissionSuccess");
                }
            }
        }

        public void Run() {
            if (null != CurrentThread && CurrentThread.IsAlive)
                return;
            CurrentThread = Tools.NewThread(() => {

                _IsRunning = false;
                while (MissionQueue.Count > 0)
                {
                    _IsRunning = true;
                    TransactionMission tm = MissionQueue.Dequeue();
                    if (null == tm)
                        continue;
                    if (tm.Type == TransactionMissionType.TMT_SendData)
                    {
                        if (!_SendData(tm.TMData))
                        {
                            if (null != tm.SendFailureHandler)
                                tm.SendFailureHandler.Invoke();
                            continue;
                        }
                        Tools.NewThread(() =>
                        {
                            if (null != tm.MissionFinishedHandler0)
                                tm.MissionFinishedHandler0.Invoke();
                        });
                    }
                    else if (tm.Type == TransactionMissionType.TMT_WaitReceive)
                    {
                        if (!_SendData(tm.TMData))
                        {
                            if (null != tm.SendFailureHandler)
                                tm.SendFailureHandler.Invoke();
                            continue;
                        }
                        //  String Key = GenerateUniqueKey();
                        CurrentMission = tm;
                        CurrentMissionSuccess = false;
                        if (!Tools.SleepWithStopSignal(tm.TimeoutMilliSecond, 50, () =>
                        {
                            return CurrentMissionSuccess;
                        }))
                        {
                            if (null != tm.TimeoutHandler)
                                tm.TimeoutHandler.Invoke(tm);
                        }
                    //    CurrentMission = null;
                    }
                    else if (tm.Type == TransactionMissionType.TMT_NonBlockOrdinaryThing)
                    {
                        Tools.NewThread(() =>
                        {
                            if (null != tm.MissionFinishedHandler0)
                                tm.MissionFinishedHandler0.Invoke();
                        });
                    }
                    else if (tm.Type == TransactionMissionType.TMT_BlockOrdinaryThing)
                    {
                        if (null != tm.MissionFinishedHandler0)
                            tm.MissionFinishedHandler0.Invoke();
                    }
                }
                _IsRunning = false;

            });
        }

        private string GenerateUniqueKey()
        {
            return Tools.randomStr(8);
        }

        private Boolean _SendData(Byte[] instruction)
        {
            if (SP.IsOpen)
            {
                try
                {
                    SP.Write(instruction, 0, instruction.Length);
                }
                catch (Exception ex)
                {
                    Tools.BlockingMsgBox(ex.Message.ToString());
                    return false;
                }
                if (null != SendDataOutputHandler)
                {
                    Tools.NewThread(() => {
                        SendDataOutputHandler.Invoke(instruction);
                    });
                }
                //Tools.TLog("_SendData ");
                //Tools.PrintByteArray(instruction);
                return true;
            }
            else
                Tools.TLog("Serial Port Is Not Opened");
            return false;
        }

        public void MissionClear()
        {
            MissionQueue.Clear();
        }

        public void MissionBlocking(
           VoidNoParamDelegate MissionFinishedHandler0,
           MissionPriority Priority = MissionPriority.MP_RealTime)
        {
            TransactionMission tm = new TransactionMission();
            tm.Type = TransactionMissionType.TMT_BlockOrdinaryThing;
            tm.Priority = Priority;
            tm.MissionFinishedHandler0 = MissionFinishedHandler0;
            MissionQueue.Enqueue(tm);
        }

        public void MissionNonBlocking(
            VoidNoParamDelegate MissionFinishedHandler0,
            MissionPriority Priority = MissionPriority.MP_RealTime)
        {
            TransactionMission tm = new TransactionMission();
            tm.Type = TransactionMissionType.TMT_NonBlockOrdinaryThing;
            tm.Priority = Priority;
            tm.MissionFinishedHandler0 = MissionFinishedHandler0;
            MissionQueue.Enqueue(tm);
        }

        public void MissionSend(
            Byte[] Instruction, 
            VoidNoParamDelegate MissionFinishedHandler0,
            MissionPriority Priority = MissionPriority.MP_RealTime,
            VoidNoParamDelegate SendFailureHandler = null)
        {
            TransactionMission tm = new TransactionMission();
            tm.Type = TransactionMissionType.TMT_SendData;
            tm.TMData = Instruction;
            tm.Priority = Priority;
            tm.MissionFinishedHandler0 = MissionFinishedHandler0;
            if (null != SendFailureHandler)
                tm.SendFailureHandler = SendFailureHandler;
            MissionQueue.Enqueue(tm);
        }

        public void MissionSendWaitReceive(
            Byte[] Instruction, 
            BooleanByteArrayOutObjectDelegate ResponseValidateHandler, 
            VoidObjectDelegate MissionFinishedHandler1,
            VoidTMDelegate TimeoutHandler,
            MissionPriority Priority = MissionPriority.MP_RealTime,
            Int32 TimeoutMilliSecond = 0,
            VoidNoParamDelegate SendFailureHandler = null)
        {
            TransactionMission tm = new TransactionMission();
            tm.Type = TransactionMissionType.TMT_WaitReceive;
            tm.TMData = Instruction;
            tm.ResponseValidateHandler = ResponseValidateHandler;
            tm.Priority = Priority;
            tm.MissionFinishedHandler1 = MissionFinishedHandler1;
            if(null != SendFailureHandler)
                tm.SendFailureHandler = SendFailureHandler;
            if(TimeoutMilliSecond > 0)
                tm.TimeoutMilliSecond = TimeoutMilliSecond;
            tm.TimeoutHandler = TimeoutHandler;
            MissionQueue.Enqueue(tm);
        }

        public void MissionAdd(TransactionMission tm)
        {
            MissionQueue.Enqueue(tm);
        }


        private Dictionary<String, TransactionMission> ListenHandlers = new Dictionary<string, TransactionMission>();

        public void ListenIns(
            DataStructrues.DataPackage Type,
            Byte FuncCode, 
            VoidObjectDelegate MissionFinishedHandler1,Int32 Duration = 0, VoidTMDelegate TimeoutHandler = null)
        {
            String LKey = Type.ToString() + "-" + FuncCode;
            if (!ListenHandlers.ContainsKey(LKey))
            {

                TransactionMission tm = new TransactionMission();
                tm.Type = TransactionMissionType.TMT_ContinuousReceive;
                tm.TMData = new Byte[] { FuncCode };
                tm.ResponseListenHandler = Type.ValidateSpecificFuncCode;
                tm.MissionFinishedHandler1 = MissionFinishedHandler1;
                if (Duration > 0)
                    tm.TimeoutMilliSecond = Duration;
                tm.TimeoutHandler = TimeoutHandler;

                ListenHandlers.Add(LKey, tm);
            }
        }


    }

}
