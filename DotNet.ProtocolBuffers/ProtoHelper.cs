using Google.ProtocolBuffers;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;

namespace DotNet.ProtocolBuffers
{
    class ProtoHelper
    {
        public static void WriteValue(int fieldIndex, object value, ProtoWriter.State protoWriter)
        {
            if (value == null)
                return;
            switch (value)
            {
                case bool boolean:
                    protoWriter.WriteFieldHeader(fieldIndex, WireType.Varint);
                    protoWriter.WriteBoolean(boolean);
                    break;
                case DateTime dateTime:
                    protoWriter.WriteFieldHeader(fieldIndex, WireType.StartGroup);
                    BclHelpers.WriteDateTime(ref protoWriter, dateTime);
                    break;
                case int intValue:
                    protoWriter.WriteFieldHeader(fieldIndex, WireType.Varint);
                    protoWriter.WriteInt32(intValue);
                    break;
                case short shortValue:
                    protoWriter.WriteFieldHeader(fieldIndex, WireType.Varint);
                    protoWriter.WriteInt16(shortValue);
                    break;
                case double doubleValue:
                    protoWriter.WriteFieldHeader(fieldIndex, WireType.Fixed64);
                    protoWriter.WriteDouble(doubleValue);
                    break;
                case string stringValue:
                    protoWriter.WriteFieldHeader(fieldIndex, WireType.String);
                    protoWriter.WriteString(stringValue);
                    break;
                case Guid guidValue:
                    protoWriter.WriteFieldHeader(fieldIndex, WireType.StartGroup);
                    BclHelpers.WriteGuid(ref protoWriter, guidValue);
                    break;
                default:
                    throw new ProtoException("Unrecognized data type");
            }
        }
        public static byte[] CreateProtoWriterBuffer()
        {
            using (MemoryStream writeStream = new MemoryStream())
            {
                ProtoWriter.State protoWriter = ProtoWriter.State.Create(writeStream, null, null);

                var student = Helper.FillObject<Student>();
                WriteValue(1, student.Id, protoWriter); //adds field number followed by value in protowrite stream.
                WriteValue(2, student.Name, protoWriter);
                WriteValue(3, student.AdmissionDate, protoWriter);
                WriteValue(4, student.Age, protoWriter);

                student = Helper.FillObject<Student>();
                WriteValue(1, student.Id, protoWriter);
                WriteValue(2, student.Name, protoWriter);
                WriteValue(3, student.AdmissionDate, protoWriter);
                WriteValue(4, student.Age, protoWriter);

                protoWriter.Close();     //Close ProtoWriter when done with writing all the data.

                return writeStream.ToArray();
            }
        }
        public static List<Student> CreateProtoReaderBuffer(byte[] encoded)
        {
            using (MemoryStream rs = new MemoryStream(encoded))
            {
                ProtoReader.State pr = ProtoReader.State.Create(rs, null, null);
                List<Student> students = new List<Student>();
                Student student = new Student();
                while (pr.GetPosition() < rs.Length)
                {
                    var fieldHeader = pr.ReadFieldHeader();
                    if (fieldHeader == 1)
                    {
                        student = new Student();
                        students.Add(student);
                    }
                    if (fieldHeader == 1)
                        student.Id = BclHelpers.ReadGuid(ref pr);
                    else if (fieldHeader == 2)
                        student.Name = pr.ReadString();
                    else if (fieldHeader == 3)
                        student.AdmissionDate = BclHelpers.ReadDateTime(ref pr);
                    else if (fieldHeader == 4)
                        student.Age = pr.ReadInt32();
                }
                return students;
            }
        }

    }
}
