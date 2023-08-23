using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTParser.Tags
{
    internal class BaseTag
    {
        public TagTypes TagType;
        public string Name = "Unknown Name";

        public BaseTag(TagTypes tagType, string? name)
        {
            TagType = tagType;
            Name = name;
        }
    }
}
