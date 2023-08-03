using CinemaSetter.ToolKit;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastPlayer
{
    /// <summary>
    /// 遥控器控制类
    /// </summary>
    public class NeckWearManager
    {
        private static NeckWearManager instance = null;
        private static object singleLock = new object(); //锁同步

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static NeckWearManager CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new NeckWearManager();
                }
            }
            return instance;
        }
        /// <summary>
        /// 控制器
        /// </summary>
        public ScentrealmBCC.SPController SPController
        {
            get;
            set;
        } = ScentrealmBCC.SPController.getInstance();

        /// <summary>
        /// 是否使用遥控器
        /// </summary>
        public bool BeUsed
        {
            get;
            set;
        } = true;

        public SerialPort Dev
        {
            get;
            set;
        }

        public ushort VM_ModuleID { get; set; } = 1;

        public byte[] AssmebleStopPlay(UInt16 ChannelID = 0)
        {
            byte[] Ins = new byte[] {
                0xf5, 0x51, 0x00, 0x0b,
                0xf5, 0x01, 0x00, 0x00, 0x02, 0xff, 0xff, 0x02, 0x02, 0x03, 0x55,
                0x03, 0xae, 0x55
            };
            Ins[2] = (byte)ChannelID;
            byte[] CheckSum2 = CalculateChecksum(Ins, 1, 14);

            Ins[15] = CheckSum2[0];
            Ins[16] = CheckSum2[1];
            return Ins;
        }
        
        /// <summary>
        /// 播放气味
        /// </summary>
        /// <param name="smellID"></param>
        /// <param name="duration"></param>
        /// <param name="ChannelID"></param>
        /// <returns></returns>
        public byte[] AssmeblePlaySmellInstruction(Int32 smellID, UInt32 duration, UInt16 ChannelID = 0)
        {
            byte[] Ins = new byte[] {
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

            byte[] CheckSum1 = CalculateChecksum(Ins, 5, 15);

            Ins[20] = CheckSum1[0];
            Ins[21] = CheckSum1[1];

            byte[] CheckSum2 = CalculateChecksum(Ins, 1, 22);

            Ins[23] = CheckSum2[0];
            Ins[24] = CheckSum2[1];
            //     Tools.PrintByteArray(Ins);
            return Ins;
        }

        public static byte[] CalculateChecksum(byte[] inputData, int offset, int length)
        {
            byte[] checksum = new byte[2];
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

        public void OnlyPlaySmell_AD(int SmellID, int Duration)
        {
            if (SmellID == 0)
            {
                return;
            }
            if (Duration == 0)
            {
                return;
            }

            Tools.TLog(String.Format("PlaySmell S={0} D={1}", SmellID, Duration));

            if (SPController.IsVMConnected)
            {
                VM_ModuleID = (ushort)Math.Ceiling((SmellID / 4.0));
                ushort VM_SmellID = (ushort)SmellID; //(Byte)(SmellID - (VM_ModuleID - 1) * 4);

                SPController.VehicleMounted_Play_Addr(VM_ModuleID, VM_SmellID, (uint)Duration * 1000);
            }
        }

        public void WakeUp()
        {
            try
            {

            }
            catch
            { }
        }


    }
}
