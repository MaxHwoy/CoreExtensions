using System;
using System.Collections.Generic;

namespace CoreExtensions.Native
{
    /// <summary>
    /// Class to build ASM code based on operations given.
    /// </summary>
    public class ASMBuilder
    {
        /// <summary>
        /// Class with all main opcodes used in ASM.
        /// </summary>
        private static class ASMInstr
        {
            // Increment registers
            public static readonly byte INC_EAX = 0x40;
            public static readonly byte INC_ECX = 0x41;
            public static readonly byte INC_EDX = 0x42;
            public static readonly byte INC_EBX = 0x43;
            public static readonly byte INC_ESP = 0x44;
            public static readonly byte INC_EBP = 0x45;
            public static readonly byte INC_ESI = 0x46;
            public static readonly byte INC_EDI = 0x47;

            // Decrement registers
            public static readonly byte DEC_EAX = 0x48;
            public static readonly byte DEC_ECX = 0x49;
            public static readonly byte DEC_EDX = 0x4A;
            public static readonly byte DEC_EBX = 0x4B;
            public static readonly byte DEC_ESP = 0x4C;
            public static readonly byte DEC_EBP = 0x4D;
            public static readonly byte DEC_ESI = 0x4E;
            public static readonly byte DEC_EDI = 0x4F;

            // Push registers
            public static readonly byte PUSH_EAX = 0x50;
            public static readonly byte PUSH_ECX = 0x51;
            public static readonly byte PUSH_EDX = 0x52;
            public static readonly byte PUSH_EBX = 0x52;
            public static readonly byte PUSH_ESP = 0x52;
            public static readonly byte PUSH_EBP = 0x52;
            public static readonly byte PUSH_ESI = 0x56;
            public static readonly byte PUSH_EDI = 0x57;

            // Pop registers
            public static readonly byte POP_EAX = 0x58;
            public static readonly byte POP_ECX = 0x59;
            public static readonly byte POP_EDX = 0x5A;
            public static readonly byte POP_EBX = 0x5B;
            public static readonly byte POP_ESP = 0x5C;
            public static readonly byte POP_EBP = 0x5D;
            public static readonly byte POP_ESI = 0x5E;
            public static readonly byte POP_EDI = 0x5F;


            public static readonly byte PUSH_WORD = 0x68;
            public static readonly byte PUSH_BYTE = 0x6A;
            public static readonly byte JL = 0x7C;

            public static readonly byte NOP = 0x90;
            public static readonly byte MOV_EAX_TO_PTR = 0xA3;

            // Mov 8-bit registers
            public static readonly byte MOV_TO_AL = 0xB0;
            public static readonly byte MOV_TO_CL = 0xB1;
            public static readonly byte MOV_TO_DL = 0xB2;
            public static readonly byte MOV_TO_BL = 0xB3;
            public static readonly byte MOV_TO_AH = 0xB4;
            public static readonly byte MOV_TO_CH = 0xB5;
            public static readonly byte MOV_TO_DH = 0xB6;
            public static readonly byte MOV_TO_BH = 0xB7;

            // Mov 16/32-bit registers
            public static readonly byte MOV_TO_EAX = 0xB8;
            public static readonly byte MOV_TO_ECX = 0xB9;
            public static readonly byte MOV_TO_EDX = 0xBA;
            public static readonly byte MOV_TO_EBX = 0xBB;
            public static readonly byte MOV_TO_ESP = 0xBC;
            public static readonly byte MOV_TO_EBP = 0xBD;
            public static readonly byte MOV_TO_ESI = 0xBE;
            public static readonly byte MOV_TO_EDI = 0xBF;



            public static readonly byte RET_WORD = 0xC2;
            public static readonly byte RET_N = 0xC3;
            public static readonly byte CALL = 0xE8;
            public static readonly byte JMP = 0xE9;

            public static readonly byte[] ADD_TO_ESP = new byte[] { 0x83, 0xC4 };
            public static readonly byte[] PUSH_WORD_PTR_DS = new byte[] { 0xFF, 0x35 };
            public static readonly byte[] CALL_EAX = new byte[] { 0xFF, 0xD0 };
        }

        /// <summary>
        /// ASM code.
        /// </summary>
        private readonly List<byte> m_asm;

        /// <summary>
        /// Initializes new instance of <see cref="ASMBuilder"/> with default capacity 0x100.
        /// </summary>
        public ASMBuilder() : this(0x100) { }

        /// <summary>
        /// Initializes new instance of <see cref="ASMBuilder"/> with capacity specified.
        /// </summary>
        /// <param name="capacity">Initial capacity of the <see cref="ASMBuilder"/>. 
        /// If the number is zero or negative, capacity will be defaulted to 0x100.</param>
        public ASMBuilder(int capacity)
        {
            if (capacity <= 0)
            {
                capacity = 0x100;
            }

            this.m_asm = new List<byte>(capacity);
        }

        /// <summary>
        /// Writes ASM assembly.
        /// </summary>
        /// <param name="asm">ASM to write.</param>
        public void Write(byte[] asm) => this.m_asm.AddRange(asm);

        /// <summary>
        /// Gets generated ASM code.
        /// </summary>
        /// <returns>ASM as a byte array of opcodes.</returns>
        public byte[] Get() => this.m_asm.ToArray();

        #region Increment Registers

        /// <summary>
        /// Increments value at EAX registry.
        /// </summary>
        public void IncEAX() => this.m_asm.Add(ASMInstr.INC_EAX);

        /// <summary>
        /// Increments value at ECX registry.
        /// </summary>
        public void IncECX() => this.m_asm.Add(ASMInstr.INC_ECX);

        /// <summary>
        /// Increments value at EDX registry.
        /// </summary>
        public void IncEDX() => this.m_asm.Add(ASMInstr.INC_EDX);

        /// <summary>
        /// Increments value at EBX registry.
        /// </summary>
        public void IncEBX() => this.m_asm.Add(ASMInstr.INC_EBX);

        /// <summary>
        /// Increments value at ESP registry.
        /// </summary>
        public void IncESP() => this.m_asm.Add(ASMInstr.INC_ESP);

        /// <summary>
        /// Increments value at EBP registry.
        /// </summary>
        public void IncEBP() => this.m_asm.Add(ASMInstr.INC_EBP);

        /// <summary>
        /// Increments value at ESI registry.
        /// </summary>
        public void IncESI() => this.m_asm.Add(ASMInstr.INC_ESI);

        /// <summary>
        /// Increments value at EDI registry.
        /// </summary>
        public void IncEDI() => this.m_asm.Add(ASMInstr.INC_EDI);

        #endregion

        #region Decrement Registers

        /// <summary>
        /// Decrements value at EAX registry.
        /// </summary>
        public void DecEAX() => this.m_asm.Add(ASMInstr.DEC_EAX);

        /// <summary>
        /// Decrements value at ECX registry.
        /// </summary>
        public void DecECX() => this.m_asm.Add(ASMInstr.DEC_ECX);

        /// <summary>
        /// Decrements value at EDX registry.
        /// </summary>
        public void DecEDX() => this.m_asm.Add(ASMInstr.DEC_EDX);

        /// <summary>
        /// Decrements value at EBX registry.
        /// </summary>
        public void DecEBX() => this.m_asm.Add(ASMInstr.DEC_EBX);

        /// <summary>
        /// Decrements value at ESP registry.
        /// </summary>
        public void DecESP() => this.m_asm.Add(ASMInstr.DEC_ESP);

        /// <summary>
        /// Decrements value at EBP registry.
        /// </summary>
        public void DecEBP() => this.m_asm.Add(ASMInstr.DEC_EBP);

        /// <summary>
        /// Decrements value at ESI registry.
        /// </summary>
        public void DecESI() => this.m_asm.Add(ASMInstr.DEC_ESI);

        /// <summary>
        /// Decrements value at EDI registry.
        /// </summary>
        public void DecEDI() => this.m_asm.Add(ASMInstr.DEC_EDI);

        #endregion

        #region Push Registers

        /// <summary>
        /// Push ESI to the stack.
        /// </summary>
        public void PushEAX() => this.m_asm.Add(ASMInstr.PUSH_EAX);

        /// <summary>
        /// Push ECX to the stack.
        /// </summary>
        public void PushECX() => this.m_asm.Add(ASMInstr.PUSH_ECX);

        /// <summary>
        /// Push EDX to the stack.
        /// </summary>
        public void PushEDX() => this.m_asm.Add(ASMInstr.PUSH_EDX);

        /// <summary>
        /// Push EBX to the stack.
        /// </summary>
        public void PushEBX() => this.m_asm.Add(ASMInstr.PUSH_EBX);

        /// <summary>
        /// Push ESP to the stack.
        /// </summary>
        public void PushESP() => this.m_asm.Add(ASMInstr.PUSH_ESP);

        /// <summary>
        /// Push EBP to the stack.
        /// </summary>
        public void PushEBP() => this.m_asm.Add(ASMInstr.PUSH_EBP);

        /// <summary>
        /// Push ESI to the stack.
        /// </summary>
        public void PushESI() => this.m_asm.Add(ASMInstr.PUSH_ESI);

        /// <summary>
        /// Push EDI to the stack.
        /// </summary>
        public void PushEDI() => this.m_asm.Add(ASMInstr.PUSH_EDI);

        #endregion

        #region Pop Registers

        /// <summary>
        /// Pop ESI to the stack.
        /// </summary>
        public void PopEAX() => this.m_asm.Add(ASMInstr.POP_EAX);

        /// <summary>
        /// Pop ECX to the stack.
        /// </summary>
        public void PopECX() => this.m_asm.Add(ASMInstr.POP_ECX);

        /// <summary>
        /// Pop EDX to the stack.
        /// </summary>
        public void PopEDX() => this.m_asm.Add(ASMInstr.POP_EDX);

        /// <summary>
        /// Pop EBX to the stack.
        /// </summary>
        public void PopEBX() => this.m_asm.Add(ASMInstr.POP_EBX);

        /// <summary>
        /// Pop ESP to the stack.
        /// </summary>
        public void PopESP() => this.m_asm.Add(ASMInstr.POP_ESP);

        /// <summary>
        /// Pop EBP to the stack.
        /// </summary>
        public void PopEBP() => this.m_asm.Add(ASMInstr.POP_EBP);

        /// <summary>
        /// Pop ESI to the stack.
        /// </summary>
        public void PopESI() => this.m_asm.Add(ASMInstr.POP_ESI);

        /// <summary>
        /// Pop EDI to the stack.
        /// </summary>
        public void PopEDI() => this.m_asm.Add(ASMInstr.POP_EDI);

        #endregion

        #region Push Operands

        /// <summary>
        /// Pushes byte to the stack.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushBYTE(byte value) => this.m_asm.AddRange(new byte[] { ASMInstr.PUSH_BYTE, value });

        /// <summary>
        /// Pushes sbyte to the stack.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushBYTE(sbyte value) => this.m_asm.AddRange(new byte[] { ASMInstr.PUSH_BYTE, (byte)value });

        /// <summary>
        /// Pushes short to the stack.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushWORD(short value) => this.PushWORD((ushort)value);

        /// <summary>
        /// Pushes ushort to the stack.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushWORD(ushort value)
        {
            this.m_asm.Add(ASMInstr.PUSH_WORD);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Pushes int to the stack.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushDWORD(int value) => this.PushDWORD((uint)value);

        /// <summary>
        /// Pushes uint to the stack.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushDWORD(uint value)
        {
            this.m_asm.Add(ASMInstr.PUSH_WORD);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Pushes float to the stack.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushDWORD(float value)
        {
            this.m_asm.Add(ASMInstr.PUSH_WORD);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Pushes double to the stack.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushQWORD(double value)
        {
            this.m_asm.Add(ASMInstr.PUSH_WORD);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        #endregion

        #region Mov 8 Registers

        /// <summary>
        /// Move a value to the AL registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToAL(sbyte value) => this.MovToAL((byte)value);

        /// <summary>
        /// Move a value to the AL registry
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToAL(byte value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_AL);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the CL registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToCL(sbyte value) => this.MovToCL((byte)value);

        /// <summary>
        /// Move a value to the CL registry
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToCL(byte value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_CL);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the DL registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToDL(sbyte value) => this.MovToDL((byte)value);

        /// <summary>
        /// Move a value to the DL registry
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToDL(byte value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_DL);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the BL registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToBL(sbyte value) => this.MovToBL((byte)value);

        /// <summary>
        /// Move a value to the BL registry
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToBL(byte value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_BL);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the AH registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToAH(sbyte value) => this.MovToAH((byte)value);

        /// <summary>
        /// Move a value to the AH registry
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToAH(byte value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_AH);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the CH registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToCH(sbyte value) => this.MovToCH((byte)value);

        /// <summary>
        /// Move a value to the CH registry
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToCH(byte value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_CH);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the DH registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToDH(sbyte value) => this.MovToDH((byte)value);

        /// <summary>
        /// Move a value to the DH registry
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToDH(byte value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_DH);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the BH registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToBH(sbyte value) => this.MovToBH((byte)value);

        /// <summary>
        /// Move a value to the BH registry
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToBH(byte value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_BH);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        #endregion

        #region Mov 16/32 Registers

        /// <summary>
        /// Move a value to the EAX registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToEAX(int value) => this.MovToEAX((uint)value);

        /// <summary>
        /// Move a value to the EAX registry
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToEAX(uint value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_EAX);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the ECX registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToECX(int value) => this.MovToECX((uint)value);

        /// <summary>
        /// Move a value to the ECX registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToECX(uint value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_ECX);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the EDX registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToEDX(int value) => this.MovToEDX((uint)value);

        /// <summary>
        /// Move a value to the EDX registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToEDX(uint value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_EDX);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the EBX registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToEBX(int value) => this.MovToEBX((uint)value);

        /// <summary>
        /// Move a value to the EBX registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToEBX(uint value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_EBX);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the ESP registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToESP(int value) => this.MovToESP((uint)value);

        /// <summary>
        /// Move a value to the ESP registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToESP(uint value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_ESP);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the EBP registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToEBP(int value) => this.MovToEBP((uint)value);

        /// <summary>
        /// Move a value to the EBP registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToEBP(uint value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_EBP);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the ESI registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToESI(int value) => this.MovToESI((uint)value);

        /// <summary>
        /// Move a value to the ESI registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToESI(uint value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_ESI);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the EDI registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToEDI(int value) => this.MovToEDI((uint)value);

        /// <summary>
        /// Move a value to the EDI registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToEDI(uint value)
        {
            this.m_asm.Add(ASMInstr.MOV_TO_EDI);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

		#endregion

		#region Misc Methods

		/// <summary>
		/// Calls EAX registry.
		/// </summary>
		public void CallEAX() => this.m_asm.AddRange(ASMInstr.CALL_EAX);

        /// <summary>
        /// Pushes word value to PTR_DS.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushWORDPTRDS(int value) => this.PushWORDPTRDS((uint)value);

        /// <summary>
        /// Pushes word value to PTR_DS.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushWORDPTRDS(uint value)
        {
            this.m_asm.AddRange(ASMInstr.PUSH_WORD_PTR_DS);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move the value stored at EAX to an address.
        /// </summary>
        /// <param name="address"></param>
        public void MovEAXToAddress(uint address)
        {
            this.m_asm.Add(0xA3);
            this.m_asm.AddRange(BitConverter.GetBytes(address));
        }

        /// <summary>
        /// Adds a value to ESP.
        /// </summary>
        /// <param name="value">Value to add.</param>
        public void AddToESP(int value)
        {
            this.m_asm.AddRange(ASMInstr.ADD_TO_ESP);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

		#endregion

		#region Conventional

		/// <summary>
		/// Makes ranged NOP of size specified.
		/// </summary>
		/// <param name="size">Size of NOP operations.</param>
		public void NOP(int size)
        {
            var arr = new byte[size];

            for (int a1 = 0; a1 < size; ++a1)
            {
                arr[a1] = ASMInstr.NOP;
            }

            this.m_asm.AddRange(arr);
        }

        /// <summary>
        /// JMP command.
        /// </summary>
        /// <param name="address">Address to jump to.</param>
        public void JMP(int address) => this.JMP((uint)address);

        /// <summary>
        /// JMP command.
        /// </summary>
        /// <param name="address">Address to jump to.</param>
        public void JMP(uint address)
        {
            this.m_asm.Add(0xE9);
            this.m_asm.AddRange(BitConverter.GetBytes(address));
        }

        /// <summary>
        /// Calls retn.
        /// </summary>
        /// <returns></returns>
        public void Return() => this.m_asm.Add(ASMInstr.RET_N);

        /// <summary>
        /// Calls return based on word passed.
        /// </summary>
        /// <param name="value">Value to make return based on.</param>
        public void RetWORD(short value) => this.RetWORD((ushort)value);
        
        /// <summary>
        /// Calls return based on word passed.
        /// </summary>
        /// <param name="value">Value to make return based on.</param>
        public void RetWORD(ushort value)
        {
            this.m_asm.Add(ASMInstr.RET_WORD);
            this.m_asm.AddRange(BitConverter.GetBytes(value));
        }

		#endregion
	}
}
