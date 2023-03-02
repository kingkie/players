using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSetter.Runtime.DataStructrues
{
    class ADPlayerSerialPort : DataPackage
    {
        /// <summary>
        /// 本机设备类型	1字节
        /// 本机设备地址	2字节
        /// </summary>
        public Byte SourceDeviceType { get; set; }
        public Byte[] SourceAddress = new Byte[2] { 0x00, 0x00};
        public UInt32 SourceAddressID {
            get{
                return Convert.ToUInt32((SourceAddress[0] << 8) | (SourceAddress[1]));
            }
        }
        /// <summary>
        /// 目标设备类型	1字节
        /// 目标设备地址	2字节
        /// </summary>
        public Byte DestDeviceType { get; set; }
        public Byte[] DestAddress = new Byte[2] { 0x00, 0x00 };
        public UInt32 DestAddressID
        {
            get
            {
                return Convert.ToUInt32((DestAddress[0] << 8) | (DestAddress[1]));
            }
        }

        public Byte PackageTotalLength { get; set; }

        public Byte PackageTotalLengthOffset { get; set; } = 8;


        /// <summary>
        /// payload 偏移量
        /// </summary>
        public override Int16 PayloadOffset { get; set; } = 9;

        /// <summary>
        /// 数据包最小长度，即包体为空时，包的长度
        /// </summary>
        public override Int16 MinPackageLength { get; set; } = 12;

        public override Int16 FuncCodeIndex { get; set; } = 7;

        public override Type InstructionType { get; set; } = typeof(Instructions.ADPlayerSerialPortInstruction);

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
            ret[8] = (Byte)(MinPackageLength + Payload.Length);
            Array.Copy(this.Payload, 0, ret, 9, Payload.Length);
            this.CheckBit = CalculateChecksum(ret);
            ret[ret.Length - 3] = this.CheckBit[0];
            ret[ret.Length - 2] = this.CheckBit[1];
            ret[ret.Length - 1] = End;
            return ret;
        }

        public override object LoadFromByteArray(byte[] data)
        {
            ADPlayerSerialPort output = new ADPlayerSerialPort();
            output.SourceDeviceType = data[1];

            output.SourceAddress[0] = data[2];
            output.SourceAddress[1] = data[3];

            output.DestDeviceType = data[4];
            output.DestAddress[0] = data[5];
            output.DestAddress[1] = data[6];

            output.FuncCode = data[7];
            output.PackageTotalLength = data[8];
            Int32 InstructionLength = GetInstructLength(data);
            output.Payload = new Byte[InstructionLength - MinPackageLength];
            Array.Copy(data, PayloadOffset, output.Payload, 0, InstructionLength - MinPackageLength);

            output.CheckBit[0] = data[InstructionLength - 3];
            output.CheckBit[1] = data[InstructionLength - 2];

            return output;
        }

        public override int GetInstructLengthDynamic(byte[] data)
        {
            return data[PackageTotalLengthOffset] - MinPackageLength;
        }

        public static Byte[] AssemblePlaySmellIns(Int32 GasPath, Int32 DurationInSecond)
        {
            Byte[] Ins = new Byte[15] {
                0xf5,
                0x01,
                0x00,
                0x01,
                0x02,
                0x00,
                0x01,
                0x01,// Function Code
                (Byte)GasPath,// GPATH
                0x0f,//Length
                (Byte)((DurationInSecond >> 8) & 0xff ),//
                (Byte)((DurationInSecond) & 0xff ),// Duration
                0x00,
                0x00,
                0x55,
            };
            Byte[] CheckSum = CalculateChecksum(Ins,1,Ins.Length - 4);

            Ins[Ins.Length - 3] = CheckSum[0];
            Ins[Ins.Length - 2] = CheckSum[1];
            return Ins;
        }


        public static Byte[] AssembleStopPlaySmellIns()
        {
            Byte[] Ins = new Byte[12] {
                0xf5,
                0x01,
                0x00,
                0x01,
                0x02,
                0x00,
                0x01,
                0x02,// Function Code
                0x0c,//Length
                0x00,
                0x00,
                0x55,
            };
            Byte[] CheckSum = CalculateChecksum(Ins, 1, Ins.Length - 4);

            Ins[Ins.Length - 3] = CheckSum[0];
            Ins[Ins.Length - 2] = CheckSum[1];
            return Ins;
        }
    }
}
