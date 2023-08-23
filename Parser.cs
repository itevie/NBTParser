using NBTParser.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBTParser
{
    internal class Parser
    {
        private string _fileName = "";
        private List<byte> _fileData = new();

        public Parser(string fileName)
        {
            _fileName = fileName;
        }

        public BaseTag Parse()
        {
            _fileData = File.ReadAllBytes(_fileName).ToList();
            return ReadNext();
        }

        public string ToJSON(BaseTag tag, bool isChild = false, bool valOnly = false)
        {
            string json = isChild == false ? "{" : "";

            switch (tag.TagType)
            {
                case TagTypes.TAG_Compound:
                    CompoundTag ctag = (CompoundTag)tag;
                    string cjson = $"{{";

                    int cidx = 0;

                    foreach (BaseTag cctag in ctag.Items)
                    {
                        cidx++;
                        cjson += ToJSON(cctag, true);
                        if (cidx != ctag.Items.Count)
                            cjson += ", ";
                    }

                    cjson += "}";
                    if (valOnly) return cjson;
                    json += $"\"{ctag.Name}\": {cjson}";
                    break;
                case TagTypes.TAG_Byte:
                    if (valOnly) return $"\"{((ByteTag)tag).Byte}\"";
                    json += $"\"{tag.Name}\": \"{((ByteTag)tag).Byte}\"";
                    break;
                case TagTypes.TAG_Int:
                    if (valOnly) return $"\"{((IntTag)tag).Value}\"";
                    json += $"\"{tag.Name}\": {((IntTag)tag).Value}";
                    break;
                case TagTypes.TAG_Double:
                    if (valOnly) return $"\"{((DoubleTag)tag).Value}\"";
                    json += $"\"{tag.Name}\": {((DoubleTag)tag).Value}";
                    break;
                case TagTypes.TAG_Float:
                    if (valOnly) return $"\"{((FloatTag)tag).Value}\"";
                    json += $"\"{tag.Name}\": {((FloatTag)tag).Value}";
                    break;
                case TagTypes.TAG_Long:
                    if (valOnly) return $"\"{((LongTag)tag).Value}\"";
                    json += $"\"{tag.Name}\": {((LongTag)tag).Value}";
                    break;
                case TagTypes.TAG_Short:
                    if (valOnly) return $"\"{((ShortTag)tag).Value}\"";
                    json += $"\"{tag.Name}\": {((ShortTag)tag).Value}";
                    break;
                case TagTypes.TAG_String:
                    string sval = ((StringTag)tag).Value.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\"", "\\\"") ;
                    if (valOnly) return $"\"{sval}\"";
                    json += $"\"{tag.Name}\": \"{sval}\"";
                    break;
                case TagTypes.TAG_Int_Array:
                    json += $"\"{tag.Name}\": [";
                    int idx = 0;

                    foreach (IntTag t in ((IntArrayTag)tag).Values)
                    {
                        idx++;
                        json += $"{t.Value}{(((IntArrayTag)tag).Values.Count != idx ? ", " : "")}";
                    }

                    json += "]";
                    break;
                case TagTypes.TAG_List:
                    json += $"\"{tag.Name}\": [";
                    int lidx = 0;

                    foreach (BaseTag t in ((ListTag)tag).Items)
                    {
                        lidx++;
                        json += $"{ToJSON(t, true, true)}{(((ListTag)tag).Items.Count != lidx ? ", " : "")}";
                    }

                    json += "]";
                    break;
                default:
                    return tag.TagType.ToString();
            }

            if (!isChild)
                json += "}";

            if (!isChild)
            {
                json = json.Replace("{\"\": ", "")[..^1];
            }

            return json;
        }

        private BaseTag ReadNext(TagTypes? listType = null, int indent = 0)
        {
            BaseTag tag = null;

            TagTypes type = listType == null ? (TagTypes)Convert.ToInt32(NextCharacter()) : (TagTypes)listType;
            string name = listType == null ? ReadName() : "?";

            switch (type)
            {
                case TagTypes.TAG_Compound:
                    CompoundTag compound = new CompoundTag(name);

                    while (_fileData[0] != (int)TagTypes.TAG_End)
                    {
                        compound.Items.Add(ReadNext(null, indent + 2));
                    }
                    NextCharacter();
                    tag = compound;
                    break;
                case TagTypes.TAG_Byte:
                    byte b = NextCharacter();
                    tag = new ByteTag(name, b);
                    break;
                case TagTypes.TAG_Int:
                    int ib1 = Convert.ToInt32(NextCharacter());
                    int ib2 = Convert.ToInt32(NextCharacter());
                    int ib3 = Convert.ToInt32(NextCharacter());
                    int ib4 = Convert.ToInt32(NextCharacter());

                    tag = new IntTag(name, (int)Math.Pow(2, 24) * ib1 + (int)Math.Pow(2, 16) * ib2 + (int)Math.Pow(2, 8) * ib3 + ib4);
                    break;
                case TagTypes.TAG_Double:
                    string doubleBinary = "";
                    for (int i = 0; i < 8; i++)
                    {
                        doubleBinary += Convert.ToString(Convert.ToInt64(NextCharacter()), 2).PadLeft(8, '0');
                    }

                    double doub = BitConverter.Int64BitsToDouble(Convert.ToInt64(doubleBinary, 2));
                    tag = new DoubleTag(name, doub);
                    break;
                case TagTypes.TAG_Float:
                    string floatBin = "";
                    for (int i = 0; i < 4; i++)
                    {
                        floatBin += Convert.ToString(Convert.ToInt32(NextCharacter()), 2).PadLeft(4, '0');
                    }

                    float floa = BitConverter.Int32BitsToSingle(Convert.ToInt32(floatBin, 2));
                    tag = new FloatTag(name, floa);
                    break;
                case TagTypes.TAG_Long:
                    long lb1 = Convert.ToInt64(NextCharacter());
                    long lb2 = Convert.ToInt64(NextCharacter());
                    long lb3 = Convert.ToInt64(NextCharacter());
                    long lb4 = Convert.ToInt64(NextCharacter());
                    long lb5 = Convert.ToInt64(NextCharacter());
                    long lb6 = Convert.ToInt64(NextCharacter());
                    long lb7 = Convert.ToInt64(NextCharacter());
                    long lb8 = Convert.ToInt64(NextCharacter());
                    tag = new LongTag(name, (long)Math.Pow(2, 56) * lb1 + (long)Math.Pow(2, 48) * lb2 + (long)Math.Pow(2, 40) * lb3 + (long)Math.Pow(2, 32) * lb4 +
                        (long)Math.Pow(2, 24) * lb5 + (long)Math.Pow(2, 16) * lb6 + (long)Math.Pow(2, 8) * lb7 + lb8);
                    break;
                case TagTypes.TAG_List:
                    TagTypes listType2 = (TagTypes)Convert.ToInt32(NextCharacter());

                    int tlb1 = Convert.ToInt32(NextCharacter());
                    int tlb2 = Convert.ToInt32(NextCharacter());
                    int tlb3 = Convert.ToInt32(NextCharacter());
                    int tlb4 = Convert.ToInt32(NextCharacter());

                    int listLength = (int)Math.Pow(2, 24) * tlb1 + (int)Math.Pow(2, 16) * tlb2 + (int)Math.Pow(2, 8) * tlb3 + tlb4;

                    List<BaseTag> tags = new();

                    for (int i = 0; i < listLength; i++)
                    {
                        tags.Add(ReadNext(listType2, indent + 2));
                    }

                    tag = new ListTag(name, tags, listType2);
                    break;
                case TagTypes.TAG_String:
                    int slb1 = Convert.ToInt32(NextCharacter());
                    int slb2 = Convert.ToInt32(NextCharacter());

                    short strLength = (short)(Math.Pow(2, 8) * slb1 + slb2);
                    string str = "";

                    for (int i = 0; i < strLength; i++)
                    {
                        str += ((char)Convert.ToInt32(NextCharacter())).ToString();
                    }

                    tag = new StringTag(name, str);
                    break;
                case TagTypes.TAG_Short:
                    int sb1 = Convert.ToInt32(NextCharacter());
                    int sb2 = Convert.ToInt32(NextCharacter());
                    tag = new ShortTag(name, (short)(Math.Pow(2, 8) * sb1 + sb2));
                    break;
                case TagTypes.TAG_Int_Array:
                    int iab1 = Convert.ToInt32(NextCharacter());
                    int iab2 = Convert.ToInt32(NextCharacter());
                    int iab3 = Convert.ToInt32(NextCharacter());
                    int iab4 = Convert.ToInt32(NextCharacter());

                    int intArrLength = (int)Math.Pow(2, 24) * iab1 + (int)Math.Pow(2, 16) * iab2 + (int)Math.Pow(2, 8) * iab3 + iab4;

                    List<IntTag> intArray = new();

                    for (int i = 0; i < intArrLength; i++)
                    {
                        intArray.Add((IntTag)ReadNext(TagTypes.TAG_Int, indent + 2));
                    }

                    tag = new IntArrayTag(name, intArray);
                    break;
            }

            if (tag == null)
            {
                throw new Exception($"{name} {type} not dealt with properly or something");
            }

            return tag;
        }

        private byte NextCharacter()
        {
            byte b = _fileData[0];
            _fileData.RemoveAt(0);
            return b;
        }

        private string ReadName()
        {
            byte b1 = NextCharacter();
            byte b2 = NextCharacter();

            int size = Convert.ToInt32(b1) + Convert.ToInt32(b2);
            string name = "";

            for (int i = 0; i != size; i++)
            {
                name += Convert.ToString((char)(Convert.ToInt32(NextCharacter())));
            }

            return name;
        }
    }
}
