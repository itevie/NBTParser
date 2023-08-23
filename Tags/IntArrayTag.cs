using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTParser.Tags
{
    internal class IntArrayTag : BaseTag
    {
        public List<IntTag> Values;

        public IntArrayTag(string name, List<IntTag> ints) : base(TagTypes.TAG_Int_Array, name)
        {
            Values = ints;
        }
    }
}
