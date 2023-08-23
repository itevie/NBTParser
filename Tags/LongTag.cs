using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTParser.Tags
{
    internal class LongTag : BaseTag
    {
        public long Value;
        public LongTag(string name, long value) : base(TagTypes.TAG_Long, name)
        {
            Value = value;
        }
    }
}
