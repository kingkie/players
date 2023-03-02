using CinemaSetter.Runtime;
using CinemaSetter.Runtime.DataStructrues;
using CinemaSetter.ToolKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BroadcastPlayer.Forms
{
    public partial class ADPlayerForm : Form
    {
        public ADPlayerForm()
        {
            InitializeComponent();
            UISync.Init(this);
            AutoConnectDev();
        }

        private SerialPort ADPlayer_SP = null;

        SerialCommunicationQueue Queue = null;

        private Boolean IsSerialPortReady()
        {
            AutoConnectDev(false,true);
            return Queue != null && Queue.IsSerialPortOK();
        }

        private void AutoConnectDev(Boolean Alert = false,Boolean Blocking = false)
        {
            if (!(Queue != null && Queue.IsSerialPortOK()))
            {
                AutoResetEvent are = new AutoResetEvent(false);

                SPACInstance.ConnectADPlayer((SerialPort SP, Object obj) => {
                    this.ADPlayer_SP = SP;
                    Queue = new SerialCommunicationQueue(this.ADPlayer_SP,
                        (Byte[] Array) => {
                            Tools.TLog("[S]:{0}", Tools.PrintByteArray(Array, false));
                        },
                        (Byte[] Array) => {
                            Tools.TLog("[R]:{0}", Tools.PrintByteArray(Array, false));
                        });
                    UISync.Execute(() => {
                        自动连接ToolStripMenuItem.Text = "已连接（" + SP.PortName + "）";
                    });
                    are.Set();
                }, (Int32 Timeout) => {
                    if(Alert)
                        Tools.NonBlockingMsgBox("连接超时!");
                    are.Set();
                });

                if (Blocking)
                {
                    are.WaitOne(2000);
                }
            }
        }

        private void 自动连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsSerialPortReady())
            {
                断开连接ToolStripMenuItem_Click(null, null);
                AutoConnectDev(true);
            }
            else
            {
                断开连接ToolStripMenuItem_Click(null, null);
                自动连接ToolStripMenuItem.Text = "已连接（" + Queue.SP.PortName + "）";
            }
        }

        private void 断开连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null != this.Queue && this.Queue.SP.IsOpen)
                this.Queue.SP.Close();
            UISync.Execute(() => {
                自动连接ToolStripMenuItem.Text = "自动连接";
                手动连接ToolStripMenuItem.Text = "手动连接";
            });
        }
        private Int32 DurationSecond
        {
            get {

                Int32 D = (Int32)numericUpDown1.Value;

                return D;
            }
        }

        private void OPTShowStateBar(String MSG, Boolean Error = false)
        {
            UISync.Execute(() => {
                if (Error)
                {
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.ForeColor = Color.Green;
                }
                toolStripStatusLabel1.ForeColor = Color.Green;
                toolStripStatusLabel1.Text = MSG;
            });


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Byte[] Ins = ADPlayerSerialPort.AssemblePlaySmellIns(0, DurationSecond);
            ADPlayerSerialPort AP = new ADPlayerSerialPort();
            if (!IsSerialPortReady())
            {
                Tools.NonBlockingMsgBox("请先连接设备！");
                return;
            }
            Queue.MissionSendWaitReceive(Ins,
                AP.Validate,
                (Object obj) => {
                    AP = obj as ADPlayerSerialPort;
                    OPTShowStateBar("播 1 号：成功");
                },
                (TransactionMission tm) => {
                    OPTShowStateBar("播 1 号：超时");
                });
            Queue.Run();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Byte[] Ins = ADPlayerSerialPort.AssemblePlaySmellIns(1, DurationSecond);
            ADPlayerSerialPort AP = new ADPlayerSerialPort();
            if (!IsSerialPortReady())
            {
                Tools.NonBlockingMsgBox("请先连接设备！");
                return;
            }
            Queue.MissionSendWaitReceive(Ins,
                AP.Validate,
                (Object obj) => {
                    AP = obj as ADPlayerSerialPort;

                    OPTShowStateBar("播 2 号：成功");
                },
                (TransactionMission tm) => {
                    OPTShowStateBar("播 2 号：超时");
                });
            Queue.Run();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Byte[] Ins = ADPlayerSerialPort.AssemblePlaySmellIns(3, DurationSecond);
            ADPlayerSerialPort AP = new ADPlayerSerialPort();
            if (!IsSerialPortReady())
            {
                Tools.NonBlockingMsgBox("请先连接设备！");
                return;
            }
            Queue.MissionSendWaitReceive(Ins,
                AP.Validate,
                (Object obj) => {
                    AP = obj as ADPlayerSerialPort;

                    OPTShowStateBar("播 4 号：成功");
                },
                (TransactionMission tm) => {
                    OPTShowStateBar("播 4 号：超时");
                });
            Queue.Run();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Byte[] Ins = ADPlayerSerialPort.AssemblePlaySmellIns(2, DurationSecond);
            ADPlayerSerialPort AP = new ADPlayerSerialPort();
            if (!IsSerialPortReady())
            {
                Tools.NonBlockingMsgBox("请先连接设备！");
                return;
            }
            Queue.MissionSendWaitReceive(Ins,
                AP.Validate,
                (Object obj) => {
                    AP = obj as ADPlayerSerialPort;
                    OPTShowStateBar("播 1 号：成功");
                },
                (TransactionMission tm) => {
                    OPTShowStateBar("播 1 号：超时");
                });
            Queue.Run();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Byte[] Ins = ADPlayerSerialPort.AssembleStopPlaySmellIns();
            ADPlayerSerialPort AP = new ADPlayerSerialPort();
            if (!IsSerialPortReady())
            {
                Tools.NonBlockingMsgBox("请先连接设备！");
                return;
            }
            Queue.MissionSendWaitReceive(Ins,
                AP.Validate,
                (Object obj) => {
                    AP = obj as ADPlayerSerialPort;
                    OPTShowStateBar("停止播放：成功");
                },
                (TransactionMission tm) => {
                    OPTShowStateBar("停止播放：超时");
                });
            Queue.Run();
        }

        private void 手动连接ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem HeaderTSMI = sender as ToolStripMenuItem;

            if (HeaderTSMI.Text != "手动连接")
                return;

            //清空下拉列表框
            HeaderTSMI.DropDownItems.Clear();
            Int32 Rate = 19200;
            //获取当前计算机的串口名称
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            Array.Sort(ports);
            //更新串口名称下拉列表框
            foreach (string port in ports)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(port);
                tsmi.Click += (object sender1, EventArgs e1) => {
                    ToolStripMenuItem ComTSMI = sender1 as ToolStripMenuItem;
                    SerialPort CSP = new SerialPort();
                    //打开串口
                    CSP.PortName = ComTSMI.Text;
                    CSP.BaudRate = Rate;
                    try
                    {

                        CSP.Open();
                        断开连接ToolStripMenuItem_Click(null, null);
                        Queue = new SerialCommunicationQueue(CSP,
                        (Byte[] Array) => {
                            Tools.TLog("[S]:{0}", Tools.PrintByteArray(Array, false));
                        },
                        (Byte[] Array) => {
                            Tools.TLog("[R]:{0}", Tools.PrintByteArray(Array, false));
                        });
                        UISync.Execute(() => {
                            自动连接ToolStripMenuItem.Text = "自动连接";
                            手动连接ToolStripMenuItem.Text = "已连接（" + CSP.PortName + "）";
                            HeaderTSMI.DropDownItems.Clear();
                        });
                    }
                    catch (Exception ex)
                    {
                        Tools.NonBlockingMsgBox(ex.Message);
                    }
                };
                HeaderTSMI.DropDownItems.Add(tsmi);
            }
            if (HeaderTSMI.DropDownItems.Count == 0)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem("无串口");
                HeaderTSMI.DropDownItems.Add(tsmi);
            }
        }
    }
}
