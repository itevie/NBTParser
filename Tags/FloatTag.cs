using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTParser.Tags
{
    internal class FloatTag : BaseTag
    {
        public float Value;
        public FloatTag(string name, float value) : base(TagTypes.TAG_Float, name)
        {
            Value = value;
        }
    }
}
