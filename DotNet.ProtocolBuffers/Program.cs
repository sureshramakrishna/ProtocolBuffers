using ProtoBuf;
using System;
using System.IO;
using Google.ProtocolBuffers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DotNet.ProtocolBuffers
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryStream memoryStream = new MemoryStream();
            ProtoWriter.State pw = ProtoWriter.State.Create(memoryStream, null, null);
            var school = Helper.FillObject<School>();
            var list = new List<School> { school };

            using (var file = File.Create("data.txt"))
                Serializer.Serialize(file, list);

            List<School> schoolDecoded;
            using (var file = File.OpenRead("data.txt"))
                schoolDecoded = Serializer.Deserialize<List<School>>(file);

            Assert.IsTrue(Helper.CompareObjects(list, schoolDecoded));

            //WriteValue(100, 10, TypeEnum.INTEGER, pw);
            //pw.Close();
            //var bytes = memoryStream.ToArray();
            //memoryStream.Dispose();

            //MemoryStream rs = new MemoryStream(bytes);

            //ProtoReader.State pr = ProtoReader.State.Create(rs, null, null);
            //var fh = pr.ReadFieldHeader();
            //var a = pr.ReadInt32();
            // for objects that is of fixed size, we use normal Serializer.Deserialize/Serialize, when serializing a list of unknown size, we use ProtoReader and ProtoWriter. This is just a bitArray, which store, field number by doing WriteFieldNumber and value using Write[Type]
        }
    }
}
