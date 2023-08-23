using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTParser.Tags
{
    internal class ListTag : BaseTag
    {
        public List<BaseTag> Items;
        public TagTypes Type;

        public ListTag(string name, List<BaseTag> items, TagTypes type) : base(TagTypes.TAG_List, name)
        {
            Items = items;
            Type = type;
        }   
    }
}
