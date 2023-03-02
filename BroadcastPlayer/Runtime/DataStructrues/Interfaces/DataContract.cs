using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSetter.Runtime.DataStructrues.Interfaces
{
    interface DataContract
    {
        Boolean Validate(Byte[] data, out Object output);
        Boolean ValidateSpecificFuncCode(Byte[] data, Byte SpecificFuncCode, out object output);
        Byte[] ToByteArray();
        Byte[] CalculateChecksum(Byte[] data);
        int GetInstructLength(Byte[] data);
        Byte GetFuncCode(Byte[] data);
        String GetFuncCodeName(Byte funcCode);
        Byte[] GetPayloadData(Byte[] data);
        Object LoadFromByteArray(Byte[] Data);
    }
}
