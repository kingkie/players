using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastTool
{
    public class BPRuntime
    {
        public Int32 GasCircuitCount { get; set; } = 12;

        public string LastPlayVideo { get; set; }

        public bool UsingAllDevice { get; set; } = true;

        public DeviceTypeEnum DeviceType { get; set; } = DeviceTypeEnum.DTE_AdPlyer_SP;

        public int CurrentDevice { get; set; }

        public ushort BroadcastChannel { get; set; } = 0;

        public int BaudRate { get; set; } = 19200;

        public UInt16 CurrentDeviceChannel { get; set; } = 1;

        public List<BPDevice> Devices = new List<BPDevice>();
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

        public BPRuntime()
        {

        }


        public void UpdateDeviceInfo(Int32 SID, UInt16 Channel, String UID = "")
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
            try
            {
                BPRuntime er = Tools.DESerializer<BPRuntime>(Tools.file_get_content(XMLFilename));
                return er;
            }
            catch
            {

            }
            return null;
        }

        public void SaveToXMLFile(String XMLFilename)
        {
            String XMLString = Tools.XmlSerialize<BPRuntime>(this);
            Tools.FileWriteContent(XMLFilename, XMLString);
        }
    }

    public class BPDevice
    {
        public Int32 DeviceID { get; set; }

        public String UID { get; set; }

        public Int32 Channel { get; set; }
    }

    public enum DeviceTypeEnum : Byte
    {
        /// <summary>
        /// 遥控器-->无线433模块
        /// </summary>
        DTE_Wireless_433_CTL = 0x00,
        /// <summary>
        /// 旧模块
        /// </summary>
        DTE_AdPlyer_SP = 0x01,
        /// <summary>
        /// BLE蓝牙模块
        /// </summary>
        DTE_BLE_Module = 0x02,
    }
}
