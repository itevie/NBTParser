using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTParser.Tags
{
    internal class DoubleTag : BaseTag
    {
        public double Value;
        public DoubleTag(string name, double value) : base(TagTypes.TAG_Double, name)
        {
            Value = value;
        }
    }
}
