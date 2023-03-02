using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSetter.Runtime.DataStructrues
{
    class TTBroadcast : DataPackage
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

        public override Type InstructionType { get; set; } = typeof(Instructions.DeviceBroadcastInstructions);

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

        public UInt16 GetSourceAddressID()
        {
            return (UInt16)((SourceAddress[0] << 8) | SourceAddress[1]);
        }


        public Int16 GetPower()
        {
            return Convert.ToInt16(Payload[1] << 8 | Payload[2]);
        }





        public Byte GetFuncCodeFromTTBArray(Byte[] TTBArray)
        {
            return TTBArray[1 + 2 + this.PayloadOffset];
        }

        public override object LoadFromByteArray(byte[] data)
        {
            TTBroadcast output = new TTBroadcast();
            output.SourceDeviceType = data[1];

            output.SourceAddress[0] = data[2];
            output.SourceAddress[1] = data[3];

            output.DestDeviceType = data[4];
            output.DestAddress[0] = data[5];
            output.DestAddress[1] = data[6];
            output.FuncCode = data[7];
            Int32 InstructionLength = GetInstructLength(data);
            output.Payload = new Byte[InstructionLength - MinPackageLength];
            Array.Copy(data, PayloadOffset, output.Payload, 0, InstructionLength - MinPackageLength);

            output.CheckBit[0] = data[InstructionLength - 3];
            output.CheckBit[1] = data[InstructionLength - 2];

            return output;
        }

        public override bool Validate(byte[] data, out Object output)
        {
            output = null;
            ControllerSerialPort csp = new ControllerSerialPort();
            Object CSPOutput = null;
            if (csp.Validate(data, out CSPOutput))
            {
                csp = (ControllerSerialPort)CSPOutput;
                Byte[] SubInstruction = new Byte[csp.Payload.Length - 2];

                Array.Copy(csp.Payload,2, SubInstruction,0, SubInstruction.Length);

                return base.Validate(SubInstruction, out output);
            }
            return false;
        }


        public static Byte[] AssembleSetPChannelIns(String UID, Byte PC)
        {
            // 14 + n
            Byte[] instruction = new Byte[11 + 13];
            instruction[0] = 0xf5;
            instruction[1] = 0x03;
            instruction[2] = 0x00;
            instruction[3] = 0x01;
            instruction[4] = 0x02;
            instruction[5] = 0x00;
            instruction[6] = 0x00;
            instruction[7] = 0x51;
            Array.Copy(ToolKit.Tools.HexStr2Hex(UID),0, instruction,8,12);
            instruction[20] = PC;
            instruction[instruction.Length - 1] = 0x55;
            Byte[] CheckSum = DataPackage.CalculateChecksum(instruction, 1, instruction.Length - 4);
            instruction[instruction.Length - 3] = CheckSum[0];
            instruction[instruction.Length - 2] = CheckSum[1];
            return instruction;
        }


        public static Byte[] AssembleSetDeviceBaudRateIns(Byte BaudRate)
        {
            // 14 + n
            Byte[] instruction = new Byte[11 + 1];
            instruction[0] = 0xf5;
            instruction[1] = 0x01;
            instruction[2] = 0x00;
            instruction[3] = 0x02;
            instruction[4] = 0x02;
            instruction[5] = 0x00;
            instruction[6] = 0x01;
            instruction[7] = (Byte)Instructions.DeviceBroadcastInstructions.REQ_SetDeviceBroadCastBaudRate;
            instruction[8] = BaudRate;
            instruction[instruction.Length - 1] = 0x55;
            Byte[] CheckSum = DataPackage.CalculateChecksum(instruction, 1, instruction.Length - 4);
            instruction[instruction.Length - 3] = CheckSum[0];
            instruction[instruction.Length - 2] = CheckSum[1];
            return instruction;
        }
    }
}
