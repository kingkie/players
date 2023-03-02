using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSetter.Runtime.DataStructrues
{
    class DeviceSerialPort : DataPackage
    {
        /// <summary>
        /// 本机设备类型	1字节
        /// 本机设备地址	2字节
        /// </summary>
        public Byte SourceDeviceType { get; set; }
        public Byte[] SourceAddress = new Byte[2] { 0x00, 0x00 };

        /// <summary>
        /// 目标设备类型	1字节
        /// 目标设备地址	2字节
        /// </summary>
        public Byte DestDeviceType { get; set; }
        public Byte[] DestAddress = new Byte[2] { 0x00, 0x00 };

        /// <summary>
        /// payload 偏移量
        /// </summary>
        public override Int16 PayloadOffset { get; set; } = 8;

        /// <summary>
        /// 数据包最小长度，即包体为空时，包的长度
        /// </summary>
        public override Int16 MinPackageLength { get; set; } = 11;

        public override Int16 FuncCodeIndex { get; set; } = 7;

        public override Type InstructionType { get; set; } = typeof(Instructions.DeviceSerialPortInstructions);


        public UInt16 GetSourceAddressID()
        {
            return (UInt16)((SourceAddress[0] << 8) | SourceAddress[1]);
        }

        public override byte[] ToByteArray()
        {
            Byte[] ret = new Byte[MinPackageLength + Payload.Length];
            ret[0] = Header;
            ret[1] = this.SourceDeviceType;
            ret[2] = this.SourceAddress[0];
            ret[3] = this.SourceAddress[1];
            ret[4] = this.DestDeviceType;
            ret[5] = this.DestAddress[0];
            ret[6] = this.DestAddress[1];
            ret[7] = this.FuncCode;
            Array.Copy(this.Payload, 0, ret, 8, Payload.Length);
            ret[Payload.Length + 8] = this.CheckBit[0];
            ret[Payload.Length + 9] = this.CheckBit[1];
            ret[Payload.Length + 10] = End;
            return ret;
        }

        public override object LoadFromByteArray(byte[] data)
        {
            DeviceSerialPort output = new DeviceSerialPort();
            output.SourceDeviceType = data[1];

            output.SourceAddress[0] = data[2];
            output.SourceAddress[1] = data[3];

            output.DestDeviceType = data[4];
            output.DestAddress[0] = data[5];
            output.DestAddress[1] = data[6];
            output.FuncCode = data[7];
            Int32 InstructionLength = GetInstructLength(data);
        //    Int32 InstructionLength = MinPackageLength + Payload.Length;
            output.Payload = new Byte[InstructionLength - MinPackageLength];
            Array.Copy(data, PayloadOffset, output.Payload, 0, InstructionLength - MinPackageLength);

            output.CheckBit[0] = data[InstructionLength - 3];
            output.CheckBit[1] = data[InstructionLength - 2];

            return output;
        }
    }
}
