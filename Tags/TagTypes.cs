using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTParser.Tags
{
    internal enum TagTypes
    {
        TAG_End,
        TAG_Byte,
        TAG_Short,
        TAG_Int,
        TAG_Long,
        TAG_Float,
        TAG_Double,
        TAG_Byte_Array,
        TAG_String,
        TAG_List,
        TAG_Compound,
        TAG_Int_Array,
        TAG_Long_Array,
    }
}
