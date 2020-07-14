using ProtoBuf;
using System;

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

    }
}
