using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTParser.Tags
{
    internal class StringTag : BaseTag
    {
        public string Value;
        public StringTag(string name, string val) : base(TagTypes.TAG_String, name)
        {
            Value = val;
        }
    }
}
