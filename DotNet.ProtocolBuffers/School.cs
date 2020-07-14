﻿using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Google.ProtocolBuffers
{
    [ProtoContract]
    class School
    {
        [ProtoMember(1)] public Guid Id { get; set; }
        [ProtoMember(2)] public string Name { get; set; }
        [ProtoMember(3)] public int Rooms { get; set; }
        [ProtoMember(4)] public bool IsPrivate { get; set; }
        [ProtoMember(5)] public List<Student> Students { get; set; }

    }
}