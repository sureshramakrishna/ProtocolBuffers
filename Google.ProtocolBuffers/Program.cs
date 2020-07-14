using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Google.ProtocolBuffers
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = Helper.FillObject<Person>();
            var binary = Helper.Encode(person);
            var decode = Helper.Decode(binary, Person.Parser);
            Assert.IsTrue(Helper.CompareObjects(person, decode));

            Advanced.AdvancedConcepts();
        }
    }
    class Advanced
    {
        public static void AdvancedConcepts()
        {
            ScalarTypes scalarType = Helper.FillObject<ScalarTypes>();
            var scalarTypeEncode = Helper.Encode(scalarType);
            var scalarTypeDecode = Helper.Decode(scalarTypeEncode, ScalarTypes.Parser);
            Assert.IsTrue(Helper.CompareObjects(scalarType, scalarTypeDecode));

            ComplexTypes complexType = Helper.FillObject<ComplexTypes>();
            complexType.Maps.Add(1, "Suresh");
            complexType.Numbers.Add(10);
            complexType.Details = Any.Pack(Helper.FillObject<Person>());
            var complexTypeEncode = Helper.Encode(complexType);
            var complexTypeDecode = Helper.Decode(complexTypeEncode, ComplexTypes.Parser);
            Assert.IsTrue(Helper.CompareObjects(complexType, complexTypeDecode));

            if (complexTypeDecode.Details.Is(Person.Descriptor))
                complexTypeDecode.Details.TryUnpack(out Person person);
            var oneOfCase = complexTypeDecode.OneofCase;    //returns the name of the field that is set 

            NullableTypes nullableType = Helper.FillObject<NullableTypes>();
            var nullableTypeEncode = Helper.Encode(nullableType);
            var nullableTypeDecode = Helper.Decode(nullableTypeEncode, NullableTypes.Parser);
            Assert.IsTrue(Helper.CompareObjects(nullableType, nullableTypeDecode));

            TimeTypes timeType = Helper.FillObject<TimeTypes>();
            var timeTypeEncode = Helper.Encode(timeType);
            var timeTypeDecode = Helper.Decode(timeTypeEncode, TimeTypes.Parser);
            Assert.IsTrue(Helper.CompareObjects(timeType, timeTypeDecode));


            //Write a collections
            List<ScalarTypes> sts = new List<ScalarTypes>();
            sts.Add(Helper.FillObject<ScalarTypes>());
            sts.Add(Helper.FillObject<ScalarTypes>());

            MemoryStream ipStream = new MemoryStream();
            foreach (var st in sts)
                st.WriteDelimitedTo(ipStream);

            using(var file = File.Create("data.bin"))
                file.Write(ipStream.ToArray());
            ipStream.Close();

            List<ScalarTypes> stsDecoded = new List<ScalarTypes>();
            using (var stream = File.OpenRead("data.bin"))
            {
                while(stream.Position < stream.Length)
                {
                    var sto = ScalarTypes.Parser.ParseDelimitedFrom(stream);
                    stsDecoded.Add(sto);
                }
            }
            Assert.IsTrue(Helper.CompareObjects(sts, stsDecoded));
        }
    }
}
