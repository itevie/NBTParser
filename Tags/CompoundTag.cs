using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTParser.Tags
{
    internal class CompoundTag : BaseTag
    {
        public List<BaseTag> Items = new();

        public CompoundTag(List<BaseTag> items, string? name = null) : base(TagTypes.TAG_Compound, name)
        {
            Items = items;
        }

        public CompoundTag(string? name = null) : base(TagTypes.TAG_Compound, name)
        {
            Items = new();
        }
    }
}
