using BroadcastPlayer.Forms;
using BroadcastPlayer.Models;
using BroadcastPlayer.Runtime;
using CinemaSetter;
using CinemaSetter.Runtime;
using CinemaSetter.Runtime.DataStructrues;
using CinemaSetter.ToolKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace BroadcastPlayer
{
    public partial class Player : Form
    {
        UInt16 Default_ChannelID {
            get {
                return Runtime.UsingAllDevice ?  Runtime.BroadcastChannel : Runtime.CurrentDeviceChannel;
            }
        }

        Int32 Default_BaudRate
        {
            get
            {
                return Runtime.BaudRate;
            }
        }

        public BPRuntime Runtime = new BPRuntime();

        private ScentrealmBCC.SPController SPController = ScentrealmBCC.SPController.getInstance();

        private ScentRealmSDKCMD SR_SDK = new ScentRealmSDKCMD();

        //    private ScentrealmBCC.BCController bCC = ScentrealmBCC.BCController.getInstance();

        BindingList<VideoInfo> VideoPlayList = null;// new BindingList<VideoInfo>(AppManager.CreateInstance().PlayList.VideoList);

        public Player()
        {
            InitializeComponent();
            //this.MaximizedBounds = Screen.PrimaryScreen.Bounds;

            UISync.Init(this);

            TransactionMission.DefaultTimeoutMilliSecond = 1500;

            AppManager.CreateInstance().Init();

            VideoPlayList = new BindingList<VideoInfo>(AppManager.CreateInstance().PlayList.VideoList);

            dgvVideo.DataSource = VideoPlayList;//使用BindingList绑定

            // 配置
            LoadSetting();

            InitRuntime();

            Tools.TLog("Default_ChannelID="+ Default_ChannelID);

            // 拖拽
            Object[] MouseEventTargetArray = new Object[]{ ICON_PB , Title_Label , CurrentDeviceLabel , TOPmenuStrip1 };

            foreach (Object obj in MouseEventTargetArray)
            {
                if (obj is Control)
                {
                    Control ctl = obj as Control;
                    ctl.MouseMove += Player_MouseMove;
                    ctl.MouseUp += Player_MouseUp;
                    ctl.MouseDown += Player_MouseDown;
                    ctl.MouseLeave += Player_MouseLeave;
                }
            }

            if (Runtime.DeviceType == DeviceTypeEnum.DTE_AdPlyer_SP)
            {
                CurrentDeviceLabel.Visible = false;
                Connect_ADP_ToolStripMenuItem_Click(this.Connect_ADP_ToolStripMenuItem,null);
            }
            else if (Runtime.DeviceType == DeviceTypeEnum.DTE_Wireless_433_CTL)
            {
                CurrentDeviceLabel.Visible = true;
                // 自动连接遥控器
                AutoConnect_ToolStripMenuItem_Click(this.AutoConnect_ToolStripMenuItem, null);
            }
            else if (Runtime.DeviceType == DeviceTypeEnum.DTE_BLE_Module)
            {
        //        Connect_ToolStripMenuItem.Visible = false;

                SR_SDK.init();
                Int32 status = SR_SDK.GetConnectStatus();
                if (!ScentRealmSDKCMD.IsOK(status))
                {
                    // ERROR
                    ShowErrorMSG(ScentRealmSDKCMD.GetErrorMSG(status) + "未连接", 12000);
                }
            }
            WMP.uiMode = "None";

            panel2.AutoScroll = true;
            panel2.HorizontalScroll.Visible = false;
            panel2.VerticalScroll.Visible = true;

            // 动态渲染 单路控制
            RenderPlayBtns(panel2, Runtime.GasCircuitCount);

            ErrorMSG.Text = "";

            // 显示错误信息
            RunShowErrorMSGThread();

            // 播放器点击事件
            WMP.ClickEvent += WMP_ClickEvent1;

            //HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.None, Keys.Left);
            //注册热键Ctrl+B，Id号为101。HotKey.KeyModifiers.Ctrl也可以直接使用数字2来表示。   
            //HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.None, Keys.Right);

            //HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.None, Keys.Space);

            ServerManager.CreateInstance().Init();

            ServerManager.CreateInstance().OnMessageReceived += OnCtrlMessage;

            if (Runtime.AutoServer)
            {
                ServerManager.CreateInstance().RunAndStopServer();
            }

            //开启向外部设备输送指令
            if(Runtime.UsedPeripheral)
            {
                DeviceExtendHelper.CreateInstance().Init();
            }

            //Runtime.SaveToXMLFile(RuntimeFile);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键    
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:   
                            FastBackward_BTN_Click(null, null);
                            break;
                        case 101:   

                            FastForward_BTN_Click(null, null);
                            break;
                        case 102:

                            if (WMP.playState == WMPLib.WMPPlayState.wmppsPaused)
                            {
                                WMP.Ctlcontrols.play();
                            }
                            else if (WMP.playState == WMPLib.WMPPlayState.wmppsPlaying)
                            {
                                WMP.Ctlcontrols.pause();
                            }
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private object obj = new object();

        private void ExecWakeUp(SerialPort Control_SP)
        {
            if (Runtime.DeviceType != DeviceTypeEnum.DTE_Wireless_433_CTL)
                return;
            lock (obj)
            {
                var NowTS = Tools.GetTimeStamp();

                if (NowTS - LastSendInsTS < BDS_SleepIntervalMS)
                    return;

                try
                {
                    Int32 Interval = 80;
                    Int32 TotalSecond = 2000;
                    Byte[] Ins = { 0xf5, 0x71, 0x55 };
                    Byte[] instruction = spc_todev.REQ_TransparentTransmission(Ins, 0xffff);

                    Tools.TLog("ExecWakeUp " + CurrentMilliSecond);

                    for (Int32 i = 0; i < TotalSecond; i += Interval)
                    {
                        Control_SP.Write(instruction, 0, instruction.Length);
                        Thread.Sleep(Interval);
                    }

                    LastSendInsTS = Tools.GetTimeStamp();
                }
                catch
                {
                    Tools.NonBlockingMsgBox("串口已断开，唤醒中断！");
                }
            }
        }

        private long CurrentTS {
            get {
                return Tools.GetTimeStamp();
            }
        }

        private ScheduleDataSimple CurrentPauseBlock { get; set; } = null;

        private void WMP_ClickEvent1(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            AxWMPLib.AxWindowsMediaPlayer Player = sender as AxWMPLib.AxWindowsMediaPlayer;

            Tools.TLog("WMP_Click=" + Player.playState);


            if (Player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                Player.Ctlcontrols.pause();

                CurrentPauseBlock = GetCurrentScriptBlock(CurrentMilliSecond);
            }
            else if (Player.playState == WMPLib.WMPPlayState.wmppsPaused
                || Player.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                Player.Ctlcontrols.play();
                if (null != WakeUp_Thread && WakeUp_Thread.IsAlive)
                    WakeUp_Thread.Abort();
                NewThreadWakeUp(false);
                ScheduleDataSimple sds = GetCurrentScriptBlock(CurrentMilliSecond);
                if (
                    sds != null &&
                    null != CurrentPauseBlock &&
                    CurrentPauseBlock.index == sds.index)
                {
                    Int32 RestMilliSecond = sds.duration + sds.start - CurrentMilliSecond;

                    String Msg = String.Format("[C]Pre=[{0}] Cur=[{1}] ST=[{2}] Play {3} {4} [{5}]", PreCurrentMilliSecond, CurrentMilliSecond, sds.start, sds.smellID, sds.duration, RestMilliSecond);
                    Tools.TLog(Msg);

                    if (RestMilliSecond > 500)
                    {
                        PlaySmell(sds.smellID, (UInt32)RestMilliSecond, Default_ChannelID);

                        XiaoboPlay(sds.smellID, RestMilliSecond / 1000);
                    }
                    CurrentPauseBlock = null;
                }

            }
            else if (Player.playState == WMPLib.WMPPlayState.wmppsUndefined)
            {
                OpenFile_ToolStripMenuItem1_Click(null,null);
            }
            else
            {
                Tools.TLog("" + Player.playState);
            }
        }

        private void InitRuntime()
        {
            // 显示当前设备信息
            if (Runtime.UsingAllDevice)
            {
                CurrentDeviceLabel.Text = "当前设备:所有设备";
            }
            else
            {
                CurrentDeviceLabel.Text = "当前设备:" + Runtime.CurrentDevice.ToString().PadLeft(4, '0');
            }

            AdjustStep = Runtime.AdjustStep;
            if(AdjustStep == 0)
            {
                AdjustStep = 10;
            }

            Dictionary<DeviceTypeEnum, Object[]> CFG_MAP = new Dictionary<DeviceTypeEnum, Object[]>()
            {
                {DeviceTypeEnum.DTE_Wireless_433_CTL,new Object[] {
                    AutoConnect_ToolStripMenuItem,
                    CloseSerialPort_ToolStripMenuItem,
                    ChooseDevice,
                } },
                {DeviceTypeEnum.DTE_AdPlyer_SP,new Object[] {
                    Connect_ADP_ToolStripMenuItem,
                    DisConnect_ADP_ToolStripMenuItem,
                } }
            };
            Object[] AllMenuItem = new Object[] {
                AutoConnect_ToolStripMenuItem,
                CloseSerialPort_ToolStripMenuItem,
                ChooseDevice,
                Connect_ADP_ToolStripMenuItem,
                DisConnect_ADP_ToolStripMenuItem,
            };

            foreach (Object obj in AllMenuItem)
            {
                ToolStripItem mi = obj as ToolStripItem;
                mi.Visible = false;
            }

            if(CFG_MAP.ContainsKey(Runtime.DeviceType))
                foreach (Object obj in CFG_MAP[Runtime.DeviceType])
                {
                    ToolStripItem mi = obj as ToolStripItem;
                    mi.Visible = true;
                }
        }

        private String WaitToPlayFile = "";

        private SerialPort _SP;

        private SerialPort SP {
            get {
                return _SP;
            }
            set {
                _SP = value;
            }
        }

        private Boolean NextMSBNotShow = false;

        private Boolean IsSerialPortReady(Boolean ShowMsg = true)
        {
            Boolean Ret = (null != SP && SP.IsOpen);
            if (Ret)
            {

            }
            else
            {
                if (SP != null)
                {
                    try
                    {
                        SP.Open();
                        AutoConnect_ToolStripMenuItem.Text = "已连接";
                    }
                    catch
                    {

                    }
                    Ret = (null != SP && SP.IsOpen);
                    if (Ret)
                        return Ret;
                }

                cloasePort_Click(CloseSerialPort_ToolStripMenuItem,null);

                if (ShowMsg)
                {
                    Tools.BlockingMsgBox("遥控器连接失败，请重新连接！", "遥控断开");
                }
                else
                {
                    if (!NextMSBNotShow)
                        Tools.NewThread(() => {
                            if (!Tools.HasThisMessageBox("遥控断开"))
                                Tools.BlockingMsgBox("遥控器连接失败，请重新连接！", "遥控断开");
                            NextMSBNotShow = false;
                        });

                    if (WMP.playState == WMPLib.WMPPlayState.wmppsPlaying)
                    {
                        // 锁定播放器和界面
                        WMP.Ctlcontrols.pause();
                        NextMSBNotShow = true;
                    }
                }
            }
            
            return Ret;
        }

        private Boolean IsSerialPortReady_AD(Boolean ShowMsg = true)
        {
            Boolean Ret = (null != SP && SP.IsOpen);
            if (Ret)
            {

            }
            else
            {
                if (SP != null)
                {
                    try
                    {
                        SP.Open();
                        Connect_ADP_ToolStripMenuItem.Text = "已连接";
                    }
                    catch
                    {

                    }

                    Ret = (null != SP && SP.IsOpen);
                    if (Ret)
                        return Ret;
                }




                DisConnect_ADP_ToolStripMenuItem_Click(DisConnect_ADP_ToolStripMenuItem, null);
                if (ShowMsg)
                {
                    Tools.BlockingMsgBox("设备已断开连接，请重新连接！", "设备断开连接");
                }
                else
                {
                    if (!NextMSBNotShow)
                        Tools.NewThread(() => {
                            if (!Tools.HasThisMessageBox("设备断开连接"))
                                Tools.BlockingMsgBox("设备已断开连接，请重新连接！", "设备断开连接");
                            NextMSBNotShow = false;
                        });

                    if (WMP.playState == WMPLib.WMPPlayState.wmppsPlaying)
                    {
                        // 锁定播放器和界面
                        WMP.Ctlcontrols.pause();
                        NextMSBNotShow = true;
                    }
                }
            }

            return Ret;
        }

        private Boolean IsSerialPortReady_BLE(Boolean ShowMsg = true)
        {
            Int32 Ret = SR_SDK.GetConnectStatus();
            if (Ret == 0)
            {

            }
            else
            {
                try
                {
                    SR_SDK.init();
                }
                catch
                {

                }
                finally
                {
                    Ret = SR_SDK.GetConnectStatus();
                }
                if (Ret == 0)
                    return true;
                if (Ret > 0)
                {
                   
                    Tools.BlockingMsgBox(ScentRealmSDKCMD.GetErrorMSG(Ret) +"已断开连接，请重新连接！", "断开连接");
                }
                else
                {
                    if (!NextMSBNotShow)
                        Tools.NewThread(() => {
                            if (!Tools.HasThisMessageBox("断开连接"))
                                Tools.BlockingMsgBox(ScentRealmSDKCMD.GetErrorMSG(Ret) + "已断开连接，请重新连接！", "断开连接");
                            NextMSBNotShow = false;
                        });

                    if (WMP.playState == WMPLib.WMPPlayState.wmppsPlaying)
                    {
                        // 锁定播放器和界面
                        WMP.Ctlcontrols.pause();
                        NextMSBNotShow = true;
                    }
                }
            }

            return Ret == 0;
        }

        private ScriptSchedule scriptSchedule = new ScriptSchedule();

        private ScheduleSimple CurrentScript { get; set; }

        private Int64[] TimePointArray { get; set; }

        String RuntimeFile = "BPRuntime.xml";

        private void LoadSetting() {

            if (File.Exists(RuntimeFile))
            {
                Runtime = BPRuntime.LoadFromXML(RuntimeFile);
            }
        }

        public Int64 CurrentMilliSeecond
        {
            get
            {
                return Convert.ToInt64(this.WMP.Ctlcontrols.currentPosition * 1000);
            }
            set
            {
                this.WMP.Ctlcontrols.currentPosition = value / 1000;
            }
        }

        private Int64[] LoadScriptArray(ScheduleSimple CurrentScript) {
            Int64[] TimePointArray = new Int64[CurrentScript.data.Length];
            int i = 0;
            foreach (ScheduleDataSimple sds in CurrentScript.data)
            {
                TimePointArray[i++] = sds.start;
            }
            return TimePointArray;
        }

        private void NewThreadWakeUp(Boolean UsingThread = true)
        {
            if (Runtime.DeviceType != DeviceTypeEnum.DTE_Wireless_433_CTL)
                return;
            if (CurrentTS - LastSendInsTS > BDS_SleepIntervalMS)
            {
                Tools.TLog("[Diff] {0}", CurrentTS - LastSendInsTS);
                if (IsSerialPortReady(false))
                {
                    if(UsingThread)
                        Tools.NewThread(() => {
                            ExecWakeUp(SP);
                        });
                    else
                        ExecWakeUp(SP);
                }
            }
        }

        private void ScriprTimerStart()
        {
            NewThreadWakeUp();
            if(!ScriptTimer.Enabled)
                ScriptTimer.Start();
        }

        private void OpenFile_ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "视频文件(*.mp4;*.avi;*.wmv;*.rmvb;*.3gp;*.rm)|*.mp4;*.avi;*.wmv;*.rmvb;*.3gp;*.rm|(All file(*.*)|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    WaitToPlayFile = fileDialog.FileName;
                    WMP.URL = WaitToPlayFile;
                    WMP.Ctlcontrols.play();
                    
                    String ScriptName = Path.GetDirectoryName(WaitToPlayFile) + "\\"+ Path.GetFileNameWithoutExtension(WaitToPlayFile) + ".srt";
                    CurrentScript = null;
                    TimePointArray = null;
                    if (File.Exists(ScriptName))
                    {
                        CurrentScript = scriptSchedule.convertSrt2ScheduleSimple(ScriptName);

                        TimePointArray = LoadScriptArray(CurrentScript);
                        Runtime.LastPlayVideo = WaitToPlayFile;
                    }
                    ScriprTimerStart();
                }
            }
        }

        public Int32 GetCurrentMilliSeecond()
        {
            return (Int32)(this.WMP.Ctlcontrols.currentPosition * 1000);
        }

        public Int32 GetTotalMilliSeecond()
        {
            return (Int32)(this.WMP.currentMedia.duration * 1000);
        }

        Int32 PreCurrentMilliSecond = 0;

        private ScheduleDataSimple GetCurrentScriptByIndex(Int32 Index)
        {
            if (CurrentScript.data.Length <= Index)
                return null;
            return CurrentScript.data[Index];
        }

        private ScheduleDataSimple GetCurrentScriptBlock(Int32 CurrentMilliSecond)
        {
            if (TimePointArray == null)
                return null ;
            Int32 Index = Tools.DichotomySearchMaxLT(TimePointArray, CurrentMilliSecond, 0, TimePointArray.Length - 1);
            
            // 给定值 小于最小值，返回-2

            if (-1 == Index)
            {
                // 给定值 大于最大值，返回-1
                ScheduleDataSimple sds = GetCurrentScriptByIndex(TimePointArray.Length - 1);
                if (sds.start + sds.duration > CurrentMilliSecond)
                {
                    // 任然在播放期间
                    return sds;
                }
            }
            else if (Index > 0)
            {
                ScheduleDataSimple sds = GetCurrentScriptByIndex(Index - 1);

                if (sds.start + sds.duration > CurrentMilliSecond)
                {
                    // 任然在播放期间
                    return sds;
                }
            }
            return null;
        }

        private void RenderPlayBtns(Control Container, Int32 RenderNumber = 4)
        {
            Int32 Left = 6;
            Int32 Top = 5;
            for (int i = 0; i < RenderNumber; i++)
            {

                int Row = ((Int32)Math.Floor(i / 2.0));
                int Col = (i % 2) ;

                PlaySmellBtn playSmellBtn1 = new PlaySmellBtn();
                playSmellBtn1.SmellID = i + Runtime.SmellIdFrom;
                if (Runtime.Scents?.Count > i)
                {
                    playSmellBtn1.SmellName = Runtime.Scents[i];
                }
                else
                {
                    playSmellBtn1.SmellName = string.Empty;
                }
                playSmellBtn1.BackColor = System.Drawing.Color.LightGray;
                // 
                playSmellBtn1.Location = new System.Drawing.Point( (Col + 1) * Left + Col * 80, Top + (Row + 1) * Left + Row * 80);
                playSmellBtn1.Name = "playSmellBtn_" + (i+1);
                playSmellBtn1.Size = new System.Drawing.Size(80, 80);
             
                playSmellBtn1.TabIndex = 19;


                playSmellBtn1.ButtonClick += (object sender, EventArgs e) =>
                {
                    if (Runtime.DeviceType == DeviceTypeEnum.DTE_AdPlyer_SP)
                    {
                        if (SPController.IsVMConnected)
                        {
                            PlaySmellBtn psb = sender as PlaySmellBtn;
                            Int32 Duration = Convert.ToInt32(numericUpDown1.Value);
                            psb.PerformencePlaySmell(Duration);
                            OnlyPlaySmell_AD(psb.SmellID, Duration);

                            XiaoboPlay(psb.SmellID, Duration);
                        }
                        else
                            ShowErrorMSG("设备未连接", 12000);
                    }
                    else if (Runtime.DeviceType == DeviceTypeEnum.DTE_Wireless_433_CTL)
                    {
                         
                        if (IsSerialPortReady())
                        {
                            PlaySmellBtn psb = sender as PlaySmellBtn;
                            Int32 Duration = Convert.ToInt32(numericUpDown1.Value);
                            psb.PerformencePlaySmell(Duration);
                            OnlyPlaySmell(psb.SmellID, (UInt32)(Duration * 1000), Default_ChannelID);

                            XiaoboPlay(psb.SmellID, Duration);
                        }
                    }
                    else if (Runtime.DeviceType == DeviceTypeEnum.DTE_BLE_Module)
                    {

                        if (IsSerialPortReady_BLE())
                        {
                            PlaySmellBtn psb = sender as PlaySmellBtn;
                            Int32 Duration = Convert.ToInt32(numericUpDown1.Value);
                            psb.PerformencePlaySmell(Duration);
                            OnlyPlaySmell_BLE(psb.SmellID, Duration);
                        }
                    }

                    // SR_SDK

                };
                playSmellBtn1.StopButtonClick += (object sender, EventArgs e) =>
                {
                    SendStopPlay();

                    XiaoboStop();
                };

                Container.Controls.Add(playSmellBtn1);
            }

        }
        private Byte[] AssmebleStopPlay(UInt16 ChannelID = 0)
        {
            Byte[] Ins = new Byte[] {
                0xf5, 0x51, 0x00, 0x0b,
                0xf5, 0x01, 0x00, 0x00, 0x02, 0xff, 0xff, 0x02, 0x02, 0x03, 0x55,
                0x03, 0xae, 0x55
            };
            Ins[2] = (byte)ChannelID;
            Byte[] CheckSum2 = CalculateChecksum(Ins, 1, 14);

            Ins[15] = CheckSum2[0];
            Ins[16] = CheckSum2[1];
            return Ins;
        }

        private Byte[] AssmeblePlaySmellInstruction(Int32 smellID, UInt32 duration,UInt16 ChannelID = 0)
        {
            Byte[] Ins = new Byte[] {
                0xf5, 0x51,// 固定
                0x00,// 逻辑信道
                0x13, // 长度
                0xf5, 0x01, // 固定
                0x00, 0x00, 0x02, // 固定
                0xff, 0xff, 0x01, // 固定
                0x00, 0x00, 0x00, 0x01, // smellID
                0x00, 0x00, 0x27, 0x10, // duration
                0x02, 0x3a, 0x55,// 校验和
                0x04, 0x24, 0x55// 校验和
            };
            Ins[2] = (byte)ChannelID;

            Ins[12] = (byte)(0xff & (smellID >> 24));
            Ins[13] = (byte)(0xff & (smellID >> 16));
            Ins[14] = (byte)(0xff & (smellID >> 8));
            Ins[15] = (byte)(0xff & smellID);

            Ins[16] = (byte)(0xff & (duration >> 24));
            Ins[17] = (byte)(0xff & (duration >> 16));
            Ins[18] = (byte)(0xff & (duration >> 8));
            Ins[19] = (byte)(0xff & duration);

            Byte[] CheckSum1 = CalculateChecksum(Ins,5,15);

            Ins[20] = CheckSum1[0];
            Ins[21] = CheckSum1[1];

            Byte[] CheckSum2 = CalculateChecksum(Ins, 1, 22);

            Ins[23] = CheckSum2[0];
            Ins[24] = CheckSum2[1];
       //     Tools.PrintByteArray(Ins);
            return Ins;
        }

        private Boolean IsPlayingSmell = false;

        private int Tick {
            get => System.Environment.TickCount;
        }

        private void SendStopPlay() {
            LastSendInsTS = Tools.GetTimeStamp();
            if (Runtime.DeviceType == DeviceTypeEnum.DTE_AdPlyer_SP)
            {
                if (SPController.IsVMConnected)
                {
                    SPController.VehicleMounted_Stop_Addr(VM_ModuleID);
                }
                else
                    ShowErrorMSG("设备未连接", 12000);
            }
            else if (Runtime.DeviceType == DeviceTypeEnum.DTE_Wireless_433_CTL)
            {
                Byte[] Intruction = AssmebleStopPlay(Default_ChannelID);
                Tools.TLog("StopPlay");
                if (IsSerialPortReady(false))
                {
                    Queue.MissionSend(Intruction, null);
                    Queue.Run();
                }
            }
            else if (Runtime.DeviceType == DeviceTypeEnum.DTE_BLE_Module)
            {
                SR_SDK.StopPlay();
            }

        }
        ADPlayerSerialPort AP = new ADPlayerSerialPort();

        private void OnlyPlaySmell_AD(Int32 SmellID, Int32 Duration)
        {
            if (SmellID == 0)
                return;
            if (Duration == 0)
                return;
            Tools.TLog(String.Format("PlaySmell S={0} D={1}", SmellID, Duration));

            if (SPController.IsVMConnected)
            {
                VM_ModuleID = (UInt16)Math.Ceiling((SmellID / 4.0));
                UInt16 VM_SmellID = (UInt16)SmellID; //(Byte)(SmellID - (VM_ModuleID - 1) * 4);
        
                SPController.VehicleMounted_Play_Addr(VM_ModuleID, VM_SmellID, (uint)Duration*1000);
            }
        }
        private void OnlyPlaySmell_BLE(Int32 SmellID, Int32 Duration)
        {
            if (SmellID == 0)
                return;
            if (Duration == 0)
                return;
            Tools.TLog(String.Format("PlaySmell S={0} D={1}", SmellID, Duration));

            SR_SDK.PlaySmell(SmellID, Duration);
        }

        private void OnlyPlaySmell(Int32 SmellID, UInt32 Duration, UInt16 ChannelID)
        {
            Tools.TLog(String.Format("PlaySmell S={0} D={1} C={2}", SmellID, Duration, ChannelID));

            Byte[] Intruction = AssmeblePlaySmellInstruction(SmellID, Duration, ChannelID);
            {
                Queue.MissionSend(Intruction, null);
                Queue.Run();
                LastSendInsTS = Tools.GetTimeStamp();
            }
        }

        private Thread CheckPlayingThread = null;

        private long LastSendInsTS = 0;
        UInt16 VM_ModuleID { get; set; } = 1;
        private void PlaySmell(Int32 SmellID, UInt32 Duration,UInt16 ChannelID) {
            if (SmellID == 0)
            {
                if (Runtime.DeviceType != DeviceTypeEnum.DTE_Wireless_433_CTL)
                {
                    ExecWakeUp(SP);
                }
                return;
            }

            if (Duration == 0)
            {
                return;
            }

            Tools.TLog(String.Format("PlaySmell S={0} D={1} C={2}", SmellID, Duration, ChannelID));
            LastSendInsTS = Tools.GetTimeStamp();
            if (Runtime.DeviceType == DeviceTypeEnum.DTE_AdPlyer_SP)
            {
                if (SPController.IsVMConnected) {
                    VM_ModuleID = (UInt16)Math.Ceiling((SmellID / 4.0));
                    UInt16 VM_SmellID = (UInt16)SmellID;
                    SPController.VehicleMounted_Play_Addr(VM_ModuleID, VM_SmellID, (uint)Duration);

                    IsPlayingSmell = true;

                    CheckPlayingThread = new Thread(() => {
                        Thread.Sleep((int)Duration - 100);
                        IsPlayingSmell = false;
                    });
                    CheckPlayingThread.Start();
                }
            }
            else if (Runtime.DeviceType == DeviceTypeEnum.DTE_Wireless_433_CTL)
            {

                Byte[] Intruction = AssmeblePlaySmellInstruction(SmellID, Duration, ChannelID);
                if (IsSerialPortReady(false))
                {
                    Queue.MissionSend(Intruction, null);
                    Queue.Run();

                    IsPlayingSmell = true;

                    CheckPlayingThread = new Thread(() => {
                        Thread.Sleep((int)Duration - 100);
                        IsPlayingSmell = false;
                    });
                    CheckPlayingThread.Start();
                }
            }
            else if (Runtime.DeviceType == DeviceTypeEnum.DTE_BLE_Module)
            {
                if (IsSerialPortReady_BLE(false))
                {
                    Int32 DurationSecond = (Int32)Math.Round(Duration / 1000.0);
                    if (DurationSecond == 0)
                        DurationSecond = 1;
                    SR_SDK.PlaySmell(SmellID, DurationSecond);

                    IsPlayingSmell = true;

                    CheckPlayingThread = new Thread(() => {
                        Thread.Sleep((int)Duration - 100);
                        IsPlayingSmell = false;
                    });
                    CheckPlayingThread.Start();
                }
            }
        }

        public static Byte[] CalculateChecksum(Byte[] inputData, int offset, int length)
        {
            Byte[] checksum = new Byte[2];
            int checkData = 0;
            if (inputData != null)
                for (int j = offset, i = 0; i < length; j++, i++)
                {
                    checkData += inputData[j];
                }
            checksum[0] = (byte)(0xff & (checkData >> 8));
            checksum[1] = (byte)(0xff & checkData);
            return checksum;
        }

        /// <summary>
        /// 休眠间隔
        /// </summary>
        private int BDS_SleepIntervalMS { get; set; } = 299500;

        /// <summary>
        /// 唤醒线程
        /// </summary>
        private Thread WakeUp_Thread { get; set; } = null;

        private int CurrentMilliSecond {
            get  => GetCurrentMilliSeecond();
        }

        private void ScriptTimer_Tick(object sender, EventArgs e)
        {
            Int32 CurrentMilliSecond = GetCurrentMilliSeecond();
           
            if (PreCurrentMilliSecond == CurrentMilliSecond)
                return;

        //    Tools.TLog("PreCurrentMilliSecond = " + PreCurrentMilliSecond + ",CurrentMilliSecond = " + CurrentMilliSecond+","+ (NextMSBNotShow && null != SP && !SP.IsOpen));
            //  if ((SP != null && SP.IsOpen) && null != CurrentScript)
            if ( null != CurrentScript)
            {
                ScheduleDataSimple sds = GetCurrentScriptBlock(CurrentMilliSecond);
                if (sds != null)
                {
                    // 当前在播放气味

                    if (PreCurrentMilliSecond <= CurrentMilliSecond)
                    {
                        // 进度向前，或者拖拽向前
                        if (WMP.playState == WMPLib.WMPPlayState.wmppsPlaying && sds.start >= PreCurrentMilliSecond && sds.start <= CurrentMilliSecond)
                        {
                            // 开始播放时间，在前一个点和这一个之间
                            Int32 RestMilliSecond = sds.duration + sds.start - CurrentMilliSecond;

                            String Msg = String.Format("[G]Pre=[{0}] Cur=[{1}] ST=[{2}] Play {3} {4} [{5}]", PreCurrentMilliSecond, CurrentMilliSecond, sds.start, sds.smellID, sds.duration, RestMilliSecond);
                            Tools.TLog(Msg);

                            if (RestMilliSecond > 500)
                            {
                                PlaySmell(sds.smellID, (UInt32)RestMilliSecond, Default_ChannelID);
                            }

                            // 不存在下一个指令，需要在下一次播放时候唤醒
                            var NextSDS = GetCurrentScriptByIndex(sds.index);
                            if (null != NextSDS)
                            {
                                var diff = NextSDS.start - sds.start;

                                // 存在下一个指令，且下一个指令和当前的间隔大于5min
                                if (diff >= BDS_SleepIntervalMS)
                                {
                                    Tools.TLog("[Diff] = " + diff);
                                    // 移除已有的唤醒任务
                                    if (null != WakeUp_Thread && WakeUp_Thread.IsAlive)
                                        WakeUp_Thread.Abort();

                                    WakeUp_Thread = Tools.NewThread(() =>
                                    {
                                        if (Runtime.DeviceType != DeviceTypeEnum.DTE_Wireless_433_CTL)
                                            return;
                                        long StartTS = Tools.GetTimeStamp();
                                        long RestTS = diff;
                                        long EndTS = StartTS + diff;
                                        do
                                        {
                                            Thread.Sleep(BDS_SleepIntervalMS - 2000);

                                            long NowTS = Tools.GetTimeStamp();
                                            if(NowTS + 4000 <= EndTS)
                                                Thread.Sleep(2000);
                                            if (IsSerialPortReady(false))
                                                ExecWakeUp(SP);
                                            RestTS = EndTS - Tools.GetTimeStamp();
                                        } while (RestTS > BDS_SleepIntervalMS);
                                    });
                                }
                            }
                        }

                    }
                    else
                    {
                        // Player.playState == WMPLib.WMPPlayState.wmppsPlaying
                        
                        // 拖动进度条，后退
                        if (WMP.playState == WMPLib.WMPPlayState.wmppsPlaying && sds.start <= CurrentMilliSecond)
                        {
                            Int32 RestMilliSecond = sds.duration + sds.start - CurrentMilliSecond;

                            String Msg = String.Format("[B]Pre=[{0}] Cur=[{1}] ST=[{2}] Play {3} {4} [{5}]", PreCurrentMilliSecond, CurrentMilliSecond, sds.start, sds.smellID, sds.duration, RestMilliSecond);
                            Tools.TLog(Msg);

                            if (RestMilliSecond > 500)
                            {
                                PlaySmell(sds.smellID, (UInt32)RestMilliSecond, Default_ChannelID);
                            }
                        }
                    }
                }
                else
                {
                    if (IsPlayingSmell)
                    {
                        Tools.TLog("manual stop");
                        SendStopPlay();
                        XiaoboStop();
                        IsPlayingSmell = false;

                        if (null != CheckPlayingThread && CheckPlayingThread.IsAlive)
                            CheckPlayingThread.Abort();
                    }

                }
            }
            PreCurrentMilliSecond = CurrentMilliSecond;
        }

        private SerialCommunicationQueue Queue = null;
        private TTBroadcast ttb = new TTBroadcast();
        private Proto2Device spc_todev = new Proto2Device();
        public void M_SetDeviceChannel(String UUID, UInt16 ChannelID, VoidNoParamDelegate OnSuccess = null, VoidNoParamDelegate OnFailure = null)
        {
            if (!IsSerialPortReady())
            {
                Tools.NonBlockingMsgBox("串口未打开");
                if (OnFailure != null)
                    OnFailure.Invoke();
                return;
            }

            Byte[] instruction = spc_todev.REQ_TransparentTransmission(
               spc_todev.REQ_SetChannel(UUID, (Byte)ChannelID, 0xffff), 0);

            // 处理 串口未打开
            Queue.MissionSendWaitReceive(
                instruction,
                ttb.Validate,
                (Object obj) => {
                    TTBroadcast dsp = (TTBroadcast)obj;
                    
                    if ((Tools.ArrayCompare(Tools.HexStr2Hex(UUID), dsp.Payload, 12) == 0) && (dsp.Payload[12] == 0X00))
                    {
                        if (OnSuccess != null)
                            OnSuccess.Invoke();
                    }
                },
                (TransactionMission tm) => {

                    if (OnFailure != null)
                        OnFailure.Invoke();
                    else
                    {
                        Tools.NonBlockingMsgBox("信道设置失败，设备未响应，请确保设备已开机");
                    }
                });
            Queue.Run();
        }

        private void AutoConnect_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String[] ButtonText = new String[] {
                "连遥控器",
                "连接中"
            };

            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi.Text == ButtonText[0])
            {
                //if (connectToolStripMenuItem.Text != "Connect")
                //{
                //    ToolStripMenuItem_ConnectDis_Click(null, null);
                //}

                tsmi.Text = ButtonText[1];
                tsmi.Enabled = false;
                //SPACInstance.BuadRate = Runtime.BaudRate;
                SPACInstance.ConnectController((SerialPort SP,Object obj) => {

                    UISync.Execute(() => {
                        tsmi.Text = "已连接";// + SP.PortName + ">";
                        tsmi.Enabled = true;

                    });

                    this.SP = SP;
             //       Queue = new SerialCommunicationQueue(SP);
                    Queue = new SerialCommunicationQueue(SP,
                     (Byte[] Array) => {
                         Tools.TLog("[S]:");
                         Tools.PrintByteArray(Array);
                     },
                     (Byte[] Array) => {
                         Tools.TLog("[R]:");
                         Tools.PrintByteArray(Array);
                     });
                    //this.Controll_SP = SP;

                    //if (null != CommConnectHandler)
                    //    CommConnectHandler.Invoke("AUTO_CTL", SP);

                }, (Int32 Timeout) => {
                    UISync.Execute(() => {
                        tsmi.Text = ButtonText[0];
                        tsmi.Enabled = true;
                    });
                    ShowErrorMSG("遥控器未连接", 12000);
                });
            }
        }

        private void cloasePort_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem ThisTSMI = sender as ToolStripMenuItem;
            ToolStripMenuItem MainTSMI = AutoConnect_ToolStripMenuItem;
            String[] ButtonText = new String[] {
                "连遥控器",
                "连接中"
            };
            if (MainTSMI.Text != ButtonText[0])
            {
                if (SP != null && SP.IsOpen)
                    SP.Close();

                MainTSMI.Text = ButtonText[0];
                MainTSMI.Enabled = true;

         //       ThisTSMI.Visible = false;
            }
        }

        private void LoadScript_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
               // fileDialog.Filter = "字幕文件(*.srt)|(All file(*.*)|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    String ScriptName = WaitToPlayFile;
                    if (File.Exists(ScriptName))
                    {
                        CurrentScript = scriptSchedule.convertSrt2ScheduleSimple(ScriptName);

                        TimePointArray = LoadScriptArray(CurrentScript);
                    }
                }
            }
        }

        private void WMP_PositionChange(object sender, AxWMPLib._WMPOCXEvents_PositionChangeEvent e)
        {
            Tools.TLog(e.oldPosition + " "+ e.newPosition);
        }

        WMPLib.WMPPlayState LastState ;

        private void WMP_StatusChange(object sender, EventArgs e)
        {
            AxWMPLib.AxWindowsMediaPlayer AWMP = (AxWMPLib.AxWindowsMediaPlayer)sender;
            if (AWMP.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                SendStopPlay();
            }
            if (LastState == WMPLib.WMPPlayState.wmppsPaused && AWMP.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                NewThreadWakeUp();
            }
            if (AWMP.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                //if (CycleCB.Checked)
                //{
                //    int iCyc = int.Parse(txtCycTimes.Text);
                //    if(iCyc > 1)
                //    {
                //        AWMP.settings.playCount = iCyc;
                //    }
                //    else
                //    {
                //        AWMP.settings.setMode("loop", true);
                //    }
                //}
                //else
                //{
                //    AWMP.settings.setMode("loop", false);
                //}
            }
//wmppsTransitioning
//wmppsPlaying / wmppsPaused
//wmppsMediaEnded
//wmppsTransitioning
//wmppsStopped
            LastState = AWMP.playState;
            Tools.TLog("LastState = " + LastState);
            // CycleCB
        }

        private void Player_FormClosing(object sender, FormClosingEventArgs e)
        {
            SendStopPlay();
            XiaoboStop();
            if(Runtime.UsedPeripheral)
            {
                DeviceExtendHelper.CreateInstance().DisConnect();
            }
            Environment.Exit(0);
        }

        private void Player_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("Player_DragDrop");
        }

        private void WMP_ClickEvent(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            
            Console.WriteLine("WMP_ClickEvent");
        }

        private void Connect_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private bool isMouseDown = false;
        private Point FormLocation;     //form的location
        private Point mouseOffset;      //鼠标的按下位置
        private long LastMouseDownTS = 0;
        private void Player_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Player_MouseDown");
            if (e.Button == MouseButtons.Left)
            {
                var NowTS = Tools.GetTimeStamp();
                if (NowTS - LastMouseDownTS < 200)
                {
                    // double click
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.WindowState = FormWindowState.Normal;
                    }
                    else if (this.WindowState == FormWindowState.Normal)
                    {
                        this.WindowState = FormWindowState.Maximized;
                    }
                }

                LastMouseDownTS = NowTS;
                isMouseDown = true;
                FormLocation = this.Location;
                mouseOffset = Control.MousePosition;
            }
            else
                isMouseDown = false;

        }

        private void Player_MouseMove(object sender, MouseEventArgs e)
        {

          
            int _x = 0;
            int _y = 0;
            if (isMouseDown)
            {
              
                Point pt = Control.MousePosition;
                _x = mouseOffset.X - pt.X;
                _y = mouseOffset.Y - pt.Y;

   //             Console.WriteLine("(" + pt.X + "," + pt.Y + ")");

                this.Location = new Point(FormLocation.X - _x, FormLocation.Y - _y);
            }
        }

        private void Player_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Player_MouseUp");
            isMouseDown = false;
        }

        private void Player_MouseLeave(object sender, EventArgs e)
        {
            Console.WriteLine("Player_MouseLeave");
            isMouseDown = false;
        }

        private void g_Min_Btn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void g_Max_Btn_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                g_Max_Btn.Text = "□";
            }
            else
            {
                //this.MaximumSize = Screen.FromHandle(this.Handle).WorkingArea.Size;
                this.WindowState = FormWindowState.Maximized;
                g_Max_Btn.Text = "⿺";
            }
        }

        private void g_Close_Btn_Click(object sender, EventArgs e)
        {
            Runtime.SaveToXMLFile(RuntimeFile);
            System.Environment.Exit(0);
        }

        private void ChooseDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UserMasker um1 = new UserMasker();
            //um1.CoverOnForm(this);
            //return;

            SettingForm sf = new SettingForm(Runtime);
            sf.StartPosition = FormStartPosition.CenterParent;
            sf.OnSubmit += (object _sender, EventArgs _e) => {

                if (null == _sender)
                {
                    CurrentDeviceLabel.Text = "当前设备:所有设备";

                    Runtime.UsingAllDevice = true;
                    Runtime.SaveToXMLFile(RuntimeFile);
                }
                else
                {
                    String SID_UID = _sender.ToString();

                    String[] SplitArray = SID_UID.Split('|');

                    if (!IsSerialPortReady())
                        return;
                    Random rd = new Random();

                    Int32 CurrentDeviceID = Convert.ToInt32(SplitArray[0]);
                    String CurrentDeviceUID = SplitArray[1];
                    UInt16 CurrentDeviceChannel = (UInt16)((Byte)rd.Next(1, 254));
                    
                    Runtime.UpdateDeviceInfo(CurrentDeviceID, Runtime.BroadcastChannel, CurrentDeviceUID);


                    //Runtime.UsingAllDevice = false;
                    //Runtime.CurrentDevice = CurrentDeviceID;
                    //Runtime.CurrentDeviceChannel = CurrentDeviceChannel;


                    //Runtime.UsingAllDevice = false;
                    //Runtime.CurrentDevice = Convert.ToInt32(SplitArray[0]);
                    //Runtime.CurrentDeviceChannel = (UInt16)rd.Next(1,254);

                    Boolean ContinuePlay = false;
                   
                    if (WMP.playState == WMPLib.WMPPlayState.wmppsPlaying)
                    {
                        ContinuePlay = true;
                    }

                    // 锁定播放器和界面
                    WMP.Ctlcontrols.pause();

                    UserMasker um = new UserMasker();

                    um.CoverOnForm(this);


                    Tools.NewThread(() => {

                        AutoResetEvent are = new AutoResetEvent(false);
                        Boolean SetMainDeviceSuccess = false;

                        // 设置 指定的设备 的逻辑信道
                        M_SetDeviceChannel(CurrentDeviceUID, CurrentDeviceChannel, () =>
                        {
                            UISync.Execute(() => {
                                CurrentDeviceLabel.Text = "当前设备:" + CurrentDeviceID.ToString().PadLeft(4, '0');
                            });
                            Runtime.UsingAllDevice = false;
                            Runtime.CurrentDevice = CurrentDeviceID;
                            Runtime.CurrentDeviceChannel = CurrentDeviceChannel;
                            Runtime.UpdateDeviceInfo(CurrentDeviceID, CurrentDeviceChannel, CurrentDeviceUID);
                            Runtime.SaveToXMLFile(RuntimeFile);

                            are.Set();
                            SetMainDeviceSuccess = true;
                        }, () =>
                        {
                            M_SetDeviceChannel(CurrentDeviceUID, CurrentDeviceChannel, () =>
                            {
                                UISync.Execute(() => {
                                    CurrentDeviceLabel.Text = "当前设备:" + CurrentDeviceID.ToString().PadLeft(4, '0');
                                });
                                Runtime.UsingAllDevice = false;
                                Runtime.CurrentDevice = CurrentDeviceID;
                                Runtime.CurrentDeviceChannel = CurrentDeviceChannel;
                                Runtime.UpdateDeviceInfo(CurrentDeviceID, CurrentDeviceChannel, CurrentDeviceUID);
                                Runtime.SaveToXMLFile(RuntimeFile);
                                are.Set();
                                SetMainDeviceSuccess = true;
                            }, () =>
                            {
                                Tools.NonBlockingMsgBox("设备连接 " + CurrentDeviceID.ToString().PadLeft(4, '0') + " 失败，请检查设备是否开机!");
                                are.Set();
                            });
                        });

                        are.WaitOne();
                        // 设置成功
                        if (SetMainDeviceSuccess)
                        {
                            // 更改 其他已录入 设备的 逻辑信道 为 0
                            foreach (BPDevice bpd in Runtime.Devices)
                            {
                                if (bpd.DeviceID != CurrentDeviceID)
                                {
                                    if (bpd.Channel != Runtime.BroadcastChannel)
                                    {
                                        M_SetDeviceChannel(bpd.UID, Runtime.BroadcastChannel, () =>
                                        {
                                            Runtime.UpdateDeviceInfo(bpd.DeviceID, Runtime.BroadcastChannel);
                                            Runtime.SaveToXMLFile(RuntimeFile);
                                        }, () =>
                                        {
                                            M_SetDeviceChannel(bpd.UID, Runtime.BroadcastChannel, () =>
                                            {
                                                Runtime.UpdateDeviceInfo(bpd.DeviceID, Runtime.BroadcastChannel);
                                                Runtime.SaveToXMLFile(RuntimeFile);
                                            }, () =>
                                            {
                                                //    Tools.NonBlockingMsgBox("设备连接 " + bpd.DeviceID.ToString().PadLeft(4, '0') + " 失败，请检查设备是否开机!");
                                                ShowErrorMSG("设备连接 " + bpd.DeviceID.ToString().PadLeft(4, '0') + " 失败，请检查设备是否开机!");
                                                Runtime.UpdateDeviceInfo(bpd.DeviceID, Runtime.BroadcastChannel);
                                                Runtime.SaveToXMLFile(RuntimeFile);
                                            });
                                        });
                                    }
                                }
                            }
                        }

                        if (ContinuePlay)
                        {
                            Queue.MissionBlocking(() => {

                                WMP.Ctlcontrols.play();
                            });
                            Queue.Run();
                        }

                        Queue.MissionBlocking(() => {
                            UISync.Execute(() => {
                                um.Dispose();
                            });
                        });

                        Queue.Run();
                    });

                }
            };
            sf.ShowDialog();
        //    sf.Show();
        }

        private Queue<KeyValuePair<String, Int32>> ErrorMSG_Queue = new Queue<KeyValuePair<string, int>>();

        private void RunShowErrorMSGThread()
        {
            Tools.NewThread(() => {
                while (true)
                {
                    if (ErrorMSG_Queue.Count > 0)
                    {
                        KeyValuePair<String, Int32> EMSG = ErrorMSG_Queue.Dequeue();

                        UISync.Execute(() => {
                            ErrorMSG.Text = "错误：" + EMSG.Key;
                        });
                        Thread.Sleep(EMSG.Value);
                        UISync.Execute(() => {
                            ErrorMSG.Text = "";
                        });
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        private void ShowErrorMSG(String MSG,Int32 Duration = 3000)
        {
            ErrorMSG_Queue.Enqueue(new KeyValuePair<string, int>(MSG, Duration));
        }

        private void TOPmenuStrip1_Paint(object sender, PaintEventArgs e)
        {
            MenuStrip ms = sender as MenuStrip;
            Pen pen = new Pen(Brushes.Gray, 1);
            e.Graphics.DrawLine(pen, 0, 0, ms.Width, 0);
            e.Graphics.DrawLine(pen, 0, 0, 0, ms.Height);
            e.Graphics.DrawLine(pen, ms.Width - 1, 0, ms.Width - 1, ms.Height);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel ms = sender as Panel;
            Pen pen = new Pen(Brushes.Gray, 1);
            Pen pen1 = new Pen(Brushes.Silver, 1);
            e.Graphics.DrawLine(pen, ms.Width - 1, 0, ms.Width - 1, ms.Height);
            e.Graphics.DrawLine(pen1, 0, ms.Height - 1, ms.Width, ms.Height - 1);
        }

        private void Connect_ToolStripMenuItem_Paint(object sender, PaintEventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            Int32 p = 4;
            e.Graphics.DrawImage(Properties.Resources.setting_icon,new Rectangle(p,p, tsmi.Width-2*p, tsmi.Width - 2 * p));
        }

        private void Reset_config_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("确认清除配置？","确认信息",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (File.Exists(RuntimeFile))
                {
                    File.Delete(RuntimeFile);
                }
            }
        }

        private void Connect_ADP_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String[] ButtonText = new String[] {
                "连接设备",
                "连接中"
            };

            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi.Text == ButtonText[0])
            {
                //if (connectToolStripMenuItem.Text != "Connect")
                //{
                //    ToolStripMenuItem_ConnectDis_Click(null, null);
                //}

                tsmi.Text = ButtonText[1];
                tsmi.Enabled = false;

                SPController.VMType = ScentrealmBCC.VehicleMountedType.MultiWithMainBoard;
                SPController.BuadRate = Runtime.BaudRate;
                SPController.ConnectVM((Boolean R) => {

                    if (R)
                    {
                        UISync.Execute(() => {
                            tsmi.Text = "已连接";// + SP.PortName + ">";
                            tsmi.Enabled = true;

                        });
                    }
                    else {

                        UISync.Execute(() => {
                            tsmi.Text = ButtonText[0];
                            tsmi.Enabled = true;
                        });
                        //ShowErrorMSG("设备未连接", 12000);
                    }
                });
            }
        }

        private void DisConnect_ADP_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem ThisTSMI = sender as ToolStripMenuItem;
            ToolStripMenuItem MainTSMI = Connect_ADP_ToolStripMenuItem;
            String[] ButtonText = new String[] {
                "连接设备",
                "连接中"
            };
            if (MainTSMI.Text != ButtonText[0])
            {
                if (SP != null && SP.IsOpen)
                    SP.Close();

                MainTSMI.Text = ButtonText[0];
                MainTSMI.Enabled = true;

                //       ThisTSMI.Visible = false;
            }
        }

        private void WakeUpBTN_Click(object sender, EventArgs e)
        {
            Tools.NewThread(() => {
                ExecWakeUp(SP);
            });
        }

        private void WMP_ClientSizeChanged(object sender, EventArgs e)
        {
           
        }

        private void ShowMC_CB_CheckedChanged(object sender, EventArgs e)
        {
            var cb = (sender as CheckBox);
            if (cb.Checked)
            {
                WMP.uiMode = "full";
            }
            else
            {
                WMP.uiMode = "None";
            }
        }

        private int AdjustStep = 10;

        private void FastForward_BTN_Click(object sender, EventArgs e)
        {
            WMP.Ctlcontrols.currentPosition += AdjustStep;
        }

        private void FastBackward_BTN_Click(object sender, EventArgs e)
        {
            WMP.Ctlcontrols.currentPosition -= AdjustStep;
        }

        private void TOPmenuStrip1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.K)
            {
                FastBackward_BTN_Click(null,null);
            }
            if (e.KeyCode == Keys.L)
            {
                FastForward_BTN_Click(null, null);
            }
        }

        private void CycleCB_CheckedChanged(object sender, EventArgs e)
        {
            if (CycleCB.Checked)
            {
                int iCyc = int.Parse(txtCycTimes.Text);
                if (iCyc > 1)
                {
                    WMP.settings.playCount = iCyc;
                }
                else
                {
                    WMP.settings.setMode("loop", true);
                }
            }
            else
            {
                WMP.settings.setMode("loop", false);
            }
        }

        private void Server_config_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenServer openServer = new OpenServer();
            openServer.ShowDialog();
        }

        private void OnCtrlMessage(List<string> lCmd)
        {
            if(lCmd != null && lCmd.Count > 0)
            {
                Console.WriteLine("接收到指令消息：" + lCmd[0]);
                switch(lCmd[0].ToLower())
                {
                    case "play":
                        this.Invoke((EventHandler)delegate {
                            if (lCmd.Count > 1)
                            {
                                int cIndex = int.Parse(lCmd[1]);
                                if (ServerManager.CreateInstance().VideoList.Count >= cIndex && cIndex > 0)
                                {
                                    string videoName = ServerManager.CreateInstance().VideoList[cIndex - 1].VideoAddr;
                                    OpenFileAndPlay(videoName);
                                }
                            }
                        });
                        break;
                    case "stop":
                        this.Invoke((EventHandler)delegate {
                            try
                            {
                                WMP.Ctlcontrols.stop();
                            }
                            catch
                            { }
                        });
                        break;
                    case "pause":
                        this.Invoke((EventHandler)delegate {
                            try
                            {
                                WMP.Ctlcontrols.pause();
                            }
                            catch
                            { }
                        });
                        break;
                    case "continue":
                        this.Invoke((EventHandler)delegate {
                            try
                            {
                                WMP.Ctlcontrols.play();
                            }
                            catch
                            { }
                        });
                        break;
                }
            }
        }

        private void OpenFileAndPlay(string filename)
        {
            try
            {
                WaitToPlayFile = filename;
                WMP.URL = WaitToPlayFile;
                WMP.Ctlcontrols.play();

                String ScriptName = Path.GetDirectoryName(WaitToPlayFile) + "\\" + Path.GetFileNameWithoutExtension(WaitToPlayFile) + ".srt";
                CurrentScript = null;
                TimePointArray = null;
                if (File.Exists(ScriptName))
                {
                    CurrentScript = scriptSchedule.convertSrt2ScheduleSimple(ScriptName);

                    TimePointArray = LoadScriptArray(CurrentScript);
                    Runtime.LastPlayVideo = WaitToPlayFile;
                }
                ScriprTimerStart();

                Console.WriteLine("当前播放状态：" + WMP.playState.ToString());

                Tools.NewThread(() =>
                {
                    try
                    {
                        int tryTimes = 6;
                        while (tryTimes > 0)
                        {
                            Thread.Sleep(1000);
                            this.Invoke((EventHandler)delegate {
                                Console.WriteLine("当前播放状态1：" + WMP.playState.ToString());
                                if (WMP.fullScreen)
                                {
                                    tryTimes = 0;
                                }
                                else
                                {
                                    if (WMP.playState == WMPLib.WMPPlayState.wmppsPlaying)
                                    {
                                        WMP.fullScreen = true;
                                    }
                                    tryTimes--;
                                }
                            });
                        }
                    }
                    catch
                    {

                    }
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine("Err:" + ex.Message);
            }
        }

        #region 外部通信

        private void XiaoboPlay(int smellid, int duration)
        {
            if (this.Runtime.UsedPeripheral)
            {
                DeviceExtendHelper.CreateInstance().PlaySmell(smellid, duration);
            }
        }

        private void XiaoboStop()
        {
            if (this.Runtime.UsedPeripheral)
            {
                DeviceExtendHelper.CreateInstance().StopPlay();
            }
        }

        #endregion End

        #region 播放列表操作

        private void dgvVideo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = dgvVideo.CurrentRow.Index;
                if (AppManager.CreateInstance().PlayList.VideoList.Count > rowIndex)
                {
                    Console.WriteLine("点击了：" + VideoPlayList[rowIndex].VideoName);
                    WaitToPlayFile = VideoPlayList[rowIndex].VideoAddr;
                    WMP.URL = WaitToPlayFile;
                    WMP.Ctlcontrols.play();

                    string ScriptName = Path.GetDirectoryName(WaitToPlayFile) + "\\" + Path.GetFileNameWithoutExtension(WaitToPlayFile) + ".srt";
                    CurrentScript = null;
                    TimePointArray = null;
                    if (File.Exists(ScriptName))
                    {
                        CurrentScript = scriptSchedule.convertSrt2ScheduleSimple(ScriptName);

                        TimePointArray = LoadScriptArray(CurrentScript);
                        Runtime.LastPlayVideo = WaitToPlayFile;
                    }
                    ScriprTimerStart();
                }
            }
            catch
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "视频文件(*.mp4;*.avi;*.wmv;*.rmvb;*.3gp;*.rm)|*.mp4;*.avi;*.wmv;*.rmvb;*.3gp;*.rm|(All file(*.*)|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = fileDialog.SafeFileName;

                    VideoInfo vInfo = new VideoInfo();
                    vInfo.VideoName = fileName.Split(new char[] { '.' })[0];
                    vInfo.VideoAddr = fileDialog.FileName;

                    VideoPlayList.Add(vInfo);

                    dgvVideo.Refresh();

                    AppManager.CreateInstance().SavePlayList();
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVideo.SelectedRows.Count < 1)
                {
                    //MessageBox.Show("请选择要删除的行！");
                    return;
                }
                var vInfo = this.dgvVideo.CurrentRow.DataBoundItem as VideoInfo;

                VideoPlayList.Remove(vInfo);
                dgvVideo.Refresh();

                AppManager.CreateInstance().SavePlayList();
            }
            catch
            { }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                VideoPlayList.Clear();
                dgvVideo.Refresh();

                AppManager.CreateInstance().SavePlayList();
            }
            catch
            { }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                //"视频文件(*.mp4;*.avi;*.wmv;*.rmvb;*.3gp;*.rm)|*.mp4;*.avi;*.wmv;*.rmvb;*.3gp;*.rm|(All file(*.*)|*.*"; 
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                if(folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string foldString = folderBrowser.SelectedPath;
                    var videofiles = Directory.GetFiles(foldString, "*", SearchOption.AllDirectories).Where(s=> s.EndsWith(".mp4") || s.EndsWith(".avi") || s.EndsWith(".wmv") || s.EndsWith(".rmvb") || s.EndsWith(".rm"));
                    
                    foreach(var item in videofiles)
                    {
                        VideoInfo vInfo = new VideoInfo();
                        vInfo.VideoName = Path.GetFileNameWithoutExtension(item);
                        vInfo.VideoAddr = item;

                        VideoPlayList.Add(vInfo);
                        Console.WriteLine(item);
                    }

                    if(videofiles != null && videofiles.Count() > 0)
                    {
                        dgvVideo.Refresh();

                        AppManager.CreateInstance().SavePlayList();
                    }
                }
            }
            catch
            { }
        }

        private void dgvVideo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dgvVideo.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dgvVideo.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
        }

        #endregion End
    }

    public class BPDevice {
        public Int32 DeviceID { get; set; }

        public String UID { get; set; }

        public Int32 Channel { get; set; }
    }

    public enum DeviceTypeEnum : Byte
    {
        DTE_Wireless_433_CTL = 0x00,
        DTE_AdPlyer_SP = 0x01,
        DTE_BLE_Module = 0x02,
    }

    public class BPRuntime
    {
        public int GasCircuitCount { get; set; } = 12;

        public String LastPlayVideo { get; set; }

        public Boolean UsingAllDevice { get; set; } = true;

        public DeviceTypeEnum DeviceType { get; set; } = DeviceTypeEnum.DTE_AdPlyer_SP;

        public Int32 CurrentDevice { get; set; }

        public UInt16 BroadcastChannel { get; set; } = 0;

        public Int32 BaudRate { get; set; } = 19200;

        public UInt16 CurrentDeviceChannel { get; set; } = 1;

        public List<BPDevice> Devices = new List<BPDevice>();

        /// <summary>
        /// 对应气味
        /// </summary>
        public List<string> Scents = new List<string>();
        /// <summary>
        /// 编号开始位置
        /// </summary>
        public int SmellIdFrom
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 快进快退秒数
        /// </summary>
        public int AdjustStep
        {
            get;
            set;
        }
        /// <summary>
        /// 自动服务
        /// </summary>
        public bool AutoServer
        {
            get;
            set;
        }

        /// <summary>
        /// 是否启用外部设备
        /// </summary>
        public bool UsedPeripheral
        {
            get;
            set;
        } = false;

        public BPRuntime()
        {

        }

        public void UpdateDeviceInfo(Int32 SID,UInt16 Channel,String UID = "")
        {
            Int32 HasDeviceIndex = -1;
            for (int i = 0; i < Devices.Count; i++)
            {
                if (Devices[i].DeviceID == SID)
                {
                    HasDeviceIndex = i;
                    break;
                }
            }
            if (HasDeviceIndex >= 0)
            {
                Devices[HasDeviceIndex].Channel = Channel;
            }
            else
            {
                BPDevice bpd = new BPDevice();
                bpd.DeviceID = SID;
                bpd.Channel = Channel;
                bpd.UID = UID;
                Devices.Add(bpd);
            }
 
        }


        public static BPRuntime LoadFromXML(String XMLFilename)
        {
            BPRuntime er = Tools.DESerializer<BPRuntime>(Tools.file_get_content(XMLFilename));
            return er;
        }

        public void SaveToXMLFile(String XMLFilename)
        {
            String XMLString = Tools.XmlSerialize<BPRuntime>(this);
            Tools.file_put_content(XMLFilename, XMLString);
        }
    }

}
