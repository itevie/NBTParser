using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTParser.Tags
{
    internal class ShortTag : BaseTag
    {
        public short Value;
        public ShortTag(string name, short value) : base(TagTypes.TAG_Short, name)
        {
            Value = value;
        }
    }
}
