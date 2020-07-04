using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
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
            Advanced.AdvancedConcepts();
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
    class Advanced
    {
        static byte[] Encode<T>(T anObject) where T : IMessage
        {
            using (var stream = new MemoryStream())
            {
                anObject.WriteTo(stream);
                return stream.ToArray();
            }
        }
        static K Decode<K>(byte[] stream, MessageParser<K> parser) where K : IMessage<K>
        {
            return parser.ParseFrom(stream);
        }
        public static void AdvancedConcepts()
        {
            ScalarTypes scalarTypes32 = new ScalarTypes
            {
                Fixed32 = 1,
                Integer32 = 2,
                SFixed32 = 3,
                SInteger32 = 4,
                UInteger32 = 5,
                Fixed64 = 6,
                Integer64 = 7,
                SFixed64 = 8,
                SInteger64 = 9,
                UInteger64 = 10,
                Booelan = true,
                Double = 11.2,
                Float = 12.2F,
                String = "I am a hero!",
                Bytes = ByteString.CopyFrom("Some string here", Encoding.Unicode)
            };
            var scalarType32Encode = Encode(scalarTypes32);
            var scalarType32Decode = Decode(scalarType32Encode, ScalarTypes.Parser);

            ComplexTypes complexTypes = new ComplexTypes
            {
                SubClass = new ScalarTypes { Booelan = true },
                Details = Any.Pack(scalarTypes32),
                Integer32 = 4
            };
            complexTypes.Sex = Gender.Female; //default value will be Male, because field number is 0 for Male.
            complexTypes.Maps.Add(1, "Suresh");
            complexTypes.Numbers.Add(10);

            var complexTypeEncode = Encode(complexTypes);
            var complexTypeDecode = Decode(complexTypeEncode, ComplexTypes.Parser);
            if(complexTypeDecode.Details.Is(ScalarTypes.Descriptor))
                complexTypeDecode.Details.TryUnpack(out ScalarTypes details);
            var oneOfCase = complexTypeDecode.OneofCase; //returns the name of the field that is set 
        }
    }
}
