using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSetter.Runtime.DataStructrues
{

    /// <summary>
    /// 串口通信，结构体
    /// </summary>
    public abstract class DataPackage : Interfaces.DataContract
    {
        /// <summary>
        /// 起始头
        /// </summary>
        public static Byte Header = 0xf5;

        /// <summary>
        /// 结束包尾
        /// </summary>
        public static Byte End = 0x55;

        /// <summary>
        /// 功能码
        /// </summary>
        public Byte FuncCode { get; set; }

        /// <summary>
        /// 包体
        /// </summary>
        public Byte[] Payload { get; set; }

        /// <summary>
        /// 校验和
        /// </summary>
        public Byte[] CheckBit = new Byte[2] { 0x00, 0x00 };

        /// <summary>
        /// Payload 起始下标
        /// </summary>
        public abstract Int16 PayloadOffset { get; set; }

        /// <summary>
        /// 数据包最小长度，即包体为空时，包的长度
        /// </summary>
        public abstract Int16 MinPackageLength { get; set; }

        /// <summary>
        /// 功能码下标
        /// </summary>
        public abstract Int16 FuncCodeIndex { get; set; }

        public abstract Type InstructionType { get; set; }

        public abstract Object LoadFromByteArray(Byte[] Data);


        public string FuncCodeName
        {
            get
            {
                return Enum.GetName(InstructionType, FuncCode);
            }
        }


        public Int32 InstructionLength
        {
            get
            {
                Int32 PayloadLength = GetPayloadLength(FuncCode);
                if (PayloadLength >= 0)
                    return MinPackageLength + PayloadLength;
                return -1;
            }
        }



        public virtual bool ValidateSpecificFuncCode(Byte[] data, Byte SpecificFuncCode, out object output)
        {
            output = null;
            String FS = "VSF";

            if (data.Length >= MinPackageLength && Header == data[0])
            {
                Byte FuncCode = GetFuncCode(data);

                if (SpecificFuncCode != FuncCode)
                {
                    Console.WriteLine(FS + ":FuncCode Not Match " + String.Format("({0:X2},expected {1:X2} )", FuncCode, SpecificFuncCode));
                    return false;
                }
                Console.WriteLine(FS + ":Instruction Name :" + GetFuncCodeName(FuncCode));
                int instructionLength = GetInstructLength(data);
                Console.WriteLine(FS + ":Expected Instruction Length :" + instructionLength);
                if (instructionLength <= 0)
                {
                    // instruction not match 
                    return false;
                }
                if (data.Length < instructionLength)
                {
                    // Data Length Not Enough 
                    Console.WriteLine(FS + ":ValidateInstruction Error : Length Error");
                    return false;
                }
                if (data[instructionLength - 1] != End)
                {
                    Console.WriteLine(FS + ":ValidateInstruction Error : End Byte Not Match");
                    return false;
                }

                Byte[] CheckBit = CalculateChecksum(data);
                Console.WriteLine(String.Format(FS + ":CheckBit 0x{0:X2} 0x{1:X2}", CheckBit[0], CheckBit[1]));
                if (CheckBit[0] == data[instructionLength - 3] && CheckBit[1] == data[instructionLength - 2])
                {
                    // checksum valid
                    Console.WriteLine(FS + ":Checksum verified ");
                    output = LoadFromByteArray(data);
                    return true;
                }
                else
                {
                    Console.WriteLine(FS + ":Checksum Wrong ");
                    return false;
                }
            }
            return false;
        }

        public static ToolKit.StatisticsCounter SCounter = new ToolKit.StatisticsCounter();//{ get; set; }

        public virtual bool Validate(byte[] data, out object output)
        {
            output = null;

            if (data.Length >= MinPackageLength && Header == data[0])
            {
                Byte FuncCode = GetFuncCode(data);
                String FuncCodeName = GetFuncCodeName(FuncCode);
                SCounter.AddItem(FuncCodeName);
                SCounter.AddTotal(FuncCodeName);
                Console.WriteLine("DP:Instruction Name :" + GetFuncCodeName(FuncCode));

                int instructionLength = GetInstructLength(data);

                Console.WriteLine("DP:Expected Instruction Length :" + instructionLength);
                if (instructionLength <= 0)
                {
                    // instruction not match 
                    return false;
                }
                if (data.Length < instructionLength)
                {
                    // Data Length Not Enough 
                    Console.WriteLine("DP:ValidateInstruction Error : Length Error(" + data.Length + ")");
                    return false;
                }
                if (data[instructionLength - 1] != End)
                {
                    Console.WriteLine("DP:ValidateInstruction Error : End Byte Not Match");
                    return false;
                }

                Byte[] CheckBit = CalculateChecksum(data);

                if (CheckBit[0] == data[instructionLength - 3] && CheckBit[1] == data[instructionLength - 2])
                {
                    // checksum valid
                    Console.WriteLine("DP:Checksum verified ");
                    output = LoadFromByteArray(data);

                    SCounter.AddSuccess(FuncCodeName);
                    return true;
                }
                else
                {
                    Console.WriteLine("DP:Checksum Wrong ");
                    return false;
                }
            }
            return false;
        }
        public abstract byte[] ToByteArray();
        public virtual byte[] CalculateChecksum(Byte[] Data)
        {
            return CalculateChecksum(Data, 1, GetInstructLength(Data) - 4);
        }

        public static byte[] CalculateChecksum(Byte[] Data, Int32 StartIndex, Int32 Length)
        {
            Byte[] checksum = new Byte[2];
            int checkData = 0;
            if (Data != null)
                for (int j = StartIndex, i = 0; i < Length; j++, i++)
                {
                    checkData += Data[j];
                }
            checksum[0] = (byte)(0xff & (checkData >> 8));
            checksum[1] = (byte)(0xff & checkData);
            return checksum;
        }
        public int GetPayloadLength(byte funcCode)
        {
            Type type = InstructionType;
            String funcCodeStr = Enum.GetName(type, funcCode);
            if (funcCodeStr != null && funcCodeStr != "")
            {
                Object obj = Enum.Parse(type, funcCodeStr, true);
                //Console.WriteLine("type = " + obj);
                int instructLength = 0;
                Dictionary<int, int> CFG = Instructions.Definition.GetInstructionTypeCFG(type);
                CFG.TryGetValue((int)funcCode, out instructLength);
                //.WriteLine("instructLength = " + instructLength);
                return instructLength;
            }
            return -1;
        }
        public virtual int GetInstructLength(byte[] data)
        {
            Int32 PayloadLength = GetPayloadLength(GetFuncCode(data));

            if (PayloadLength >= 0)
            {
                return MinPackageLength + PayloadLength;
            }
            else if (PayloadLength == -2)
            {
                Int32 DynamicLength = GetInstructLengthDynamic(data);
                if (DynamicLength >= 0)
                {
                    return MinPackageLength + DynamicLength;
                }
            }
            return -1;
        }

        public virtual int GetInstructLengthDynamic(byte[] data)
        {
            return -1;
        }

        public byte GetFuncCode(byte[] data)
        {
            return data[FuncCodeIndex];
        }



        public string GetFuncCodeName(byte funcCode)
        {
            return Enum.GetName(InstructionType, funcCode);
        }
        public byte[] GetPayloadData(byte[] data)
        {
            Byte[] payload = new Byte[data.Length - MinPackageLength];
            Array.Copy(data, PayloadOffset, payload, 0, data.Length - MinPackageLength);
            return payload;
        }
    }

}
