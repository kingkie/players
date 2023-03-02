using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BroadcastTool
{
    public partial class mainFrm : Form
    {
        public BPRuntime Runtime = null;

        public mainFrm()
        {
            InitializeComponent();
        }

        public string RuntimeFile = "BPRuntime.xml";

        private void mainFrm_Load(object sender, EventArgs e)
        {
            Runtime = BPRuntime.LoadFromXML(RuntimeFile);
            if(Runtime == null)
            {
                Runtime = new BPRuntime();
            }
            InitConfig();

        }

        private void InitConfig()
        {
            switch (Runtime.DeviceType)
            {
                case DeviceTypeEnum.DTE_Wireless_433_CTL:
                    cboDevType.SelectedIndex = 0;
                    break;
                case DeviceTypeEnum.DTE_AdPlyer_SP:
                    cboDevType.SelectedIndex = 1;
                    break;
                case DeviceTypeEnum.DTE_BLE_Module:
                    //cboDevType.SelectedIndex = 2;
                    break;
                default:
                    cboDevType.SelectedIndex = 0;
                    break;
            }

            chkAllDevs.Checked = Runtime.UsingAllDevice;

            nuGasTotalCount.Value = Runtime.GasCircuitCount;

            nuDevChl.Value = Runtime.BroadcastChannel;

            nuLogicChl.Value = Runtime.CurrentDeviceChannel;

            txtIdFrom.Text = Runtime.SmellIdFrom.ToString();
            switch(Runtime.BaudRate)
            {
                case 9600:
                    cboBraudRate.SelectedIndex = 0;
                    break;
                case 19200:
                    cboBraudRate.SelectedIndex = 1;
                    break;
                case 115200:
                    cboBraudRate.SelectedIndex = 2;
                    break;
                default:
                    break;
            }

            nuAdjustStep.Value = Runtime.AdjustStep;

            txtVideo.Text = Runtime.LastPlayVideo;

            txtDevSn.Text = Runtime.CurrentDevice.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(cboDevType.SelectedIndex > -1)
            {
                switch (cboDevType.SelectedIndex)
                {
                    case 0:
                        Runtime.DeviceType = DeviceTypeEnum.DTE_Wireless_433_CTL;
                        break;
                    case 1:
                        Runtime.DeviceType = DeviceTypeEnum.DTE_AdPlyer_SP;
                        break;
                    case 2:
                        Runtime.DeviceType = DeviceTypeEnum.DTE_BLE_Module;
                        break;
                    default:
                        Runtime.DeviceType = DeviceTypeEnum.DTE_Wireless_433_CTL;
                        break;
                }
            }
            else
            {
                MessageBox.Show("请选择设备类型！");
            }

            Runtime.UsingAllDevice = chkAllDevs.Checked; //

            Runtime.GasCircuitCount = (int)nuGasTotalCount.Value;

            Runtime.BroadcastChannel = (ushort)nuDevChl.Value;

            Runtime.CurrentDeviceChannel = (ushort)nuLogicChl.Value;

            Runtime.AdjustStep = (int)nuAdjustStep.Value;

            try
            {
                Runtime.SmellIdFrom = int.Parse(txtIdFrom.Text);
                if(Runtime.SmellIdFrom < 0)
                {
                    Runtime.SmellIdFrom = 0;
                }
            }
            catch
            {
                MessageBox.Show("请输入正确的开始编号！");
            }

            if(cboBraudRate.SelectedIndex > -1)
            {
                switch (cboBraudRate.SelectedIndex)
                {
                    case 0:
                        Runtime.BaudRate = 9600;
                        break;
                    case 1:
                        Runtime.BaudRate = 19200;
                        break;
                    case 2:
                        Runtime.BaudRate = 115200;
                        break;
                    default:
                        if(Runtime.DeviceType == DeviceTypeEnum.DTE_Wireless_433_CTL)
                        {
                            Runtime.BaudRate = 115200;
                        }
                        else if (Runtime.DeviceType == DeviceTypeEnum.DTE_AdPlyer_SP)
                        {
                            Runtime.BaudRate = 9600;
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("请选择波特率！");
                return;
            }

            Runtime.SaveToXMLFile(RuntimeFile);

            MessageBox.Show("设置完成！");

        }

        private void txtVideo_MouseUp(object sender, MouseEventArgs e)
        {
            //Runtime.LastPlayVideo = txtVideo.Text;
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "视频文件(*.mp4;*.avi;*.wmv;*.rmvb;*.3gp;*.rm)|*.mp4;*.avi;*.wmv;*.rmvb;*.3gp;*.rm|(All file(*.*)|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtVideo.Text = fileDialog.FileName;
                }
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            Runtime.LastPlayVideo = txtVideo.Text;
            try
            {
                int devSn = int.Parse(txtDevSn.Text.Trim());
                Runtime.CurrentDevice = devSn;
            }
            catch
            { }

            Runtime.SaveToXMLFile(RuntimeFile);

            MessageBox.Show("设置完成！");
        }
    }
}
