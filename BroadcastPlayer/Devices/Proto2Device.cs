using CinemaSetter.ToolKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSetter
{
    class Proto2Device
    {
        #region 类定义
        private class DataPackage
        {
            public static Byte header = 0xf5;
            public Byte funcCode { get; set; }
            public Byte[] payload { get; set; }
            public Byte[] checkBit = new Byte[2];
            public static Byte end = 0x55;

            public DataPackage()
            {

            }

            /// <summary>
            /// 计算校验和
            /// </summary>
            /// <param name="funcCode"></param>
            /// <param name="payload"></param>
            /// <returns></returns>
            public static Byte[] calculateChecksum(Byte funcCode, Byte[] payload)
            {
                Byte[] checksum = new Byte[2];
                int checkData = funcCode;
                if (payload != null)
                    for (int j = 0; j < payload.Length; j++)
                    {
                        checkData += payload[j];
                    }
                checksum[0] = (byte)(0xff & (checkData >> 8));
                checksum[1] = (byte)(0xff & checkData);
                return checksum;
            }
            

            public Byte[] assembleData(UInt16 address, Byte funcCode, Byte[] payload)
            {
                int length = 11;
                if (payload != null)
                    length += payload.Length;
                Byte[] data = new Byte[length];
                data[0] = header;
                data[1] = 0x01;
                data[2] = 0;
                data[3] = 0;
                data[4] = 0x02;
                data[5] = (byte)((address >> 8) & 0x00ff);
                data[6] = (byte)(address & 0x00ff);
                data[7] = funcCode;
                if (payload != null)
                    for (int i = 8, j = 0; j < payload.Length; i++, j++)
                    {
                        data[i] = payload[j];
                    }
                checkBit = CinemaSetter.Runtime.DataStructrues.DataPackage.CalculateChecksum(data, 1, (payload == null ? 0 : payload.Length) + 7);
                data[length - 3] = checkBit[0];
                data[length - 2] = checkBit[1];
                data[length - 1] = end;
                return data;
            }

            public Byte[] assembleData(Byte funcCode, Byte[] payload)
            {
                int length = 5;
                if (payload != null)
                    length += payload.Length;
                Byte[] data = new Byte[length];
                data[0] = header;
                data[1] = funcCode;
                if (payload != null)
                    for (int i = 2, j = 0; j < payload.Length; i++, j++)
                    {
                        data[i] = payload[j];
                    }
                checkBit = calculateChecksum(funcCode, payload);
                data[length - 3] = checkBit[0];
                data[length - 2] = checkBit[1];
                data[length - 1] = end;
                return data;
            }
        }
        #endregion 类定义
        #region 私有属性
        private DataPackage data;
        #endregion 私有属性

        #region 公有方法
        /// <summary>
        /// 构造函数
        /// </summary>
        public Proto2Device()
        {
            data = new DataPackage();
        }


        public Byte[] REQ_TransparentTransmission(Byte[] payload, int channel)
        {
            Byte funcCode = Convert.ToByte(CinemaSetter.Runtime.Instructions.ControllerSerialPortInstructions.REQ_TransparentTransmission);
            Byte[] payload1 = new Byte[payload.Length + 2];
            payload1[0] = (byte)channel;
            payload1[1] = (byte)payload.Length;
            Buffer.BlockCopy(payload, 0, payload1, 2, payload.Length);
            return data.assembleData(funcCode, payload1);
        }

        /// <summary>
        /// 设置设备信道
        /// </summary>
        /// <param name="deviceID">设备uuid</param>
        /// <param name="channel">信道</param>
        /// <param name="address">固定为0xffff</param>
        /// <returns></returns>
        public Byte[] REQ_SetChannel(String deviceID, Byte channel, UInt16 address)
        {
            Byte funcCode = Convert.ToByte(CinemaSetter.Runtime.Instructions.DeviceBroadcastInstructions.REQ_SetChannel);
            Byte[] deviceIDByte = Tools.HexStr2Hex(deviceID);
            Byte[] payload = new Byte[13];
            Array.Copy(deviceIDByte, payload, 12);
            payload[12] = channel;
            return data.assembleData(address, funcCode, payload);
        }



        /// <summary>
        /// 播放气味
        /// </summary>
        /// <param name="smellID">气味编号</param>
        /// <param name="duration">播放时间，单位ms</param>
        /// <returns></returns>
        public Byte[] REQ_PlaySmell(Int32 smellID, UInt32 duration, UInt16 address)
        {
            Byte funcCode = Convert.ToByte(CinemaSetter.Runtime.Instructions.DeviceBroadcastInstructions.REQ_PlaySmell);

            Byte[] payload = new Byte[] {
                (byte)(0xff & (smellID >> 24)),
                (byte)(0xff & (smellID >> 16)),
                (byte)(0xff & (smellID >> 8)),
                (byte)(0xff & smellID),
                (byte)(0xff & (duration >> 24)),
                (byte)(0xff & (duration >> 16)),
                (byte)(0xff & (duration >> 8)),
                (byte)(0xff & duration)
            };
            return data.assembleData(address, funcCode, payload);
        }
      

        /// <summary>
        /// 停止播放气味
        /// </summary>
        /// <returns></returns>
        public Byte[] REQ_StopPlay(UInt16 address)
        {
            Byte funcCode = Convert.ToByte(CinemaSetter.Runtime.Instructions.DeviceBroadcastInstructions.REQ_StopPlay);

            Byte[] payload = null;
            return data.assembleData(address, funcCode, payload);
        }


        #endregion 公有方法
    }
}
