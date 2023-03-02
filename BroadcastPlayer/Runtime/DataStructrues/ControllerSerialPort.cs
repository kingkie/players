using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSetter.Runtime.DataStructrues
{
    class ControllerSerialPort : DataPackage
    {
        
        /// <summary>
        /// payload 偏移量
        /// </summary>
        public override Int16 PayloadOffset { get; set; } = 2;

        /// <summary>
        /// 数据包最小长度，即包体为空时，包的长度
        /// </summary>
        public override Int16 MinPackageLength { get; set; } = 5;

        public override Int16 FuncCodeIndex { get; set; } = 1;

        public override Type InstructionType { get; set; } = typeof(CinemaSetter.Runtime.Instructions.ControllerSerialPortInstructions);

        public override byte[] ToByteArray()
        {
            Byte[] ret = new Byte[MinPackageLength + Payload.Length];
            ret[0] = Header;
            ret[1] = this.FuncCode;
            Array.Copy(this.Payload, 0, ret, 2, Payload.Length);
            ret[Payload.Length + 2] = this.CheckBit[0];
            ret[Payload.Length + 3] = this.CheckBit[1];
            ret[Payload.Length + 4] = End;
            return ret;
        }

        public override object LoadFromByteArray(byte[] data)
        {
            ControllerSerialPort output = new ControllerSerialPort();
            
            output.FuncCode = data[1];
            Int32 InstructionLength = GetInstructLength(data);
            output.Payload = new Byte[InstructionLength - MinPackageLength];
            Array.Copy(data, PayloadOffset, output.Payload, 0, InstructionLength - MinPackageLength);

            output.CheckBit[0] = data[InstructionLength - 3];
            output.CheckBit[1] = data[InstructionLength - 2];

            return output;
        }

        public override int GetInstructLength(byte[] data)
        {
            Byte funcCode = GetFuncCode(data);
            if (funcCode == Convert.ToByte(Runtime.Instructions.ControllerSerialPortInstructions.REQ_TransparentTransmission))
            {
                return data[3] + 7;
            }
            if (funcCode == Convert.ToByte(Runtime.Instructions.ControllerSerialPortInstructions.RESP_TransparentTransmission))
            {
                return data[3] + 7;
            }
            return base.GetInstructLength(data);
        }

        //public static Byte[] AssembleInstruction(Byte FuncCode,)
        //{
        //    Byte[] Res = new Byte[1];

        //    return Res;
        //}

        public static Byte[] Ins_WriteUIDAndSequenceID(String UID, Int32 SID)
        {
            // 14 + n
            Byte[] instruction = new Byte[5 + 16];
            instruction[0] = 0xf5;
            instruction[1] = 0x62;
            Byte[] UIDArray = ToolKit.Tools.HexStr2Hex(UID);
            Array.Copy(UIDArray,0, instruction,2, UIDArray.Length);
            instruction[14] = (Byte)((SID >> 24) & 0xff);
            instruction[15] = (Byte)((SID >> 16) & 0xff);
            instruction[16] = (Byte)((SID >> 8) & 0xff);
            instruction[17] = (Byte)(SID & 0xff);
            instruction[instruction.Length - 1] = 0x55;
            Byte[] CheckSum = DataPackage.CalculateChecksum(instruction, 1, instruction.Length - 4);
            instruction[instruction.Length - 3] = CheckSum[0];
            instruction[instruction.Length - 2] = CheckSum[1];
            return instruction;
        }

        public static Byte[] Ins_ReadUIDAndSequenceID()
        {
            // 14 + n
            Byte[] instruction = new Byte[5];
            instruction[0] = 0xf5;
            instruction[1] = 0x61;
            instruction[instruction.Length - 1] = 0x55;
            Byte[] CheckSum = DataPackage.CalculateChecksum(instruction, 1, instruction.Length - 4);
            instruction[instruction.Length - 3] = CheckSum[0];
            instruction[instruction.Length - 2] = CheckSum[1];
            return instruction;
        }
    }
}
