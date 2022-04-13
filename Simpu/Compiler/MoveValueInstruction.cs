﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public class MoveValueInstruction : MoveInstruction
    {

        public MoveValueInstruction(ObjectFile obj, string moveAddress, int value)
            : base(obj, moveAddress)
        {
            Value = value;
        }

        public int Value { get; }

        public override int Size => 9;

        public override void Write(Stream s)
        {
            var offset = Obj.GetAddressOffset(this, MoveAddress);

            s.WriteByte(0x10);
            s.Write(BitConverter.GetBytes(offset), 0, 4);
            s.Write(BitConverter.GetBytes(Value), 0, 4);
        }

        public override string ToOpCode()
        {
            return $"MOV {MoveAddress},{Value}";
        }
    }
}