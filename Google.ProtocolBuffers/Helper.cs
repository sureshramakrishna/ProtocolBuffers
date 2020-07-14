using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using KellermanSoftware.CompareNetObjects;
using KellermanSoftware.CompareNetObjects.TypeComparers;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Tynamix.ObjectFiller;

namespace Google.ProtocolBuffers
{
    class Helper
    {
        public static byte[] Encode<T>(T anObject) where T : IMessage
        {
            using (var stream = new MemoryStream())
            {
                anObject.WriteTo(stream);
                return stream.ToArray();
            }
        }

        public static K Decode<K>(byte[] stream, MessageParser<K> parser) where K : IMessage<K>
        {
            return parser.ParseFrom(stream);
        }

        public static T FillObject<T>() where T : class
        {
            var randomStringGenerator  = new MnemonicString(2);

            var filler = new Filler<T>();
            filler.Setup().OnType<ByteString>().Use(ByteString.CopyFrom(randomStringGenerator.GetValue(), Encoding.Unicode));
            filler.Setup().OnType<Timestamp>().Use(Timestamp.FromDateTime(DateTime.UtcNow)); //should be UTC
            filler.Setup().OnType<Duration>().Use(Duration.FromTimeSpan(new TimeSpan(100)));
            return filler.Create();
            
        }

        public static bool CompareObjects<T>(T o, T b)
        {
            CompareLogic compareLogic = new CompareLogic();
            compareLogic.Config.CustomComparers.Add(new CustomComparer<ByteString, ByteString>((o, b) => o.Equals(b)));
            var result = compareLogic.Compare(o, b);
            return result.AreEqual;
        }
    }
}
