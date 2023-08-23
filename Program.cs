using NBTParser.Tags;

namespace NBTParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser(args[0]);
            BaseTag tag = parser.Parse();
            string json = parser.ToJSON(tag);
            Console.WriteLine(json);
        }
    }
}