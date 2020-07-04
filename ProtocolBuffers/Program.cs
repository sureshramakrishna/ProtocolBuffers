using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolBuffers
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person { Id = 1, Name = "Suresh" };
            var binary = Encode(person);
            var outPerson = Decode(binary);
        }
        static byte[] Encode(Person person)
        {
            using (var stream = new MemoryStream())
            {
                person.WriteTo(stream);
                return stream.ToArray();
            }
        }
        static Person Decode(byte[] stream)
        {
            return Person.Parser.ParseFrom(stream);
        }
    }
}
