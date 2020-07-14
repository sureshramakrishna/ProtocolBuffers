using System;
using ProtoBuf;

namespace Google.ProtocolBuffers
{
    [ProtoContract]
    class Student
    {
        [ProtoMember(1)] public Guid Id { get; set; }
        [ProtoMember(2)] public string Name { get; set; }
        [ProtoMember(3)] public DateTime AdmissionDate { get; set; }
        [ProtoMember(4)] public int Age { get; set; }
        [ProtoIgnore]    public bool IsDead { get; }
    }
}
