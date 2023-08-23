using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTParser.Tags
{
    internal class ByteTag : BaseTag
    {
        public byte Byte;

        public ByteTag(string name, byte @byte) : base(TagTypes.TAG_Byte, name)
        {
            Byte = @byte;   
        }
    }
}
