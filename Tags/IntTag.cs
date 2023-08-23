using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTParser.Tags
{
    internal class IntTag : BaseTag
    {
        public int Value;
        public IntTag(string name, int value) : base(TagTypes.TAG_Int, name)
        {
            Value = value;
        }
    }
}
