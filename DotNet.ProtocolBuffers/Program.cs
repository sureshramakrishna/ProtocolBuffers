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
            SimpleProto();
            CollectionsOfProto();
            UsingProtoReaderWrite();
        }
        static void SimpleProto()
        {
            MemoryStream memoryStream = new MemoryStream();
            var school = Helper.FillObject<School>();

            using (var file = File.Create("data.txt"))
                Serializer.Serialize(file, school);

            School schoolDecoded;
            using (var file = File.OpenRead("data.txt"))
                schoolDecoded = Serializer.Deserialize<School>(file);

            Assert.IsTrue(Helper.CompareObjects(school, schoolDecoded));
        }
        static void CollectionsOfProto()
        {
            MemoryStream memoryStream = new MemoryStream();

            List<School> schools = new List<School>();
            for(int i = 0; i<5;i++)
                schools.Add(Helper.FillObject<School>());

            using (var file = File.Create("data.txt"))
                Serializer.Serialize(file, schools);

            List<School> schoolsDecoded;
            using (var file = File.OpenRead("data.txt"))
                schoolsDecoded = Serializer.Deserialize<List<School>>(file);

            Assert.IsTrue(Helper.CompareObjects(schools, schoolsDecoded));
        }
        static void UsingProtoReaderWrite()
        {
            var encoded = ProtoHelper.CreateProtoWriterBuffer();
            var decoded = ProtoHelper.CreateProtoReaderBuffer(encoded);

        }
    }
}
