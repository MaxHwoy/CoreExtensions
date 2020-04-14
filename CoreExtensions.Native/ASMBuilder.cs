using System;
using System.Collections.Generic;
using System.Text;

namespace CoreExtensions.Native
{
	public class ASMBuilder
	{
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
            public static readonly byte MOV_TO_EDP = 0xBD;
            public static readonly byte MOV_TO_ESI = 0xBE;
            public static readonly byte MOV_TO_EDO = 0xBF;



            public static readonly byte RET_WORD = 0xC2;
			public static readonly byte RET_N = 0xC3;
			public static readonly byte CALL = 0xE8;
			public static readonly byte JMP = 0xE9;

			public static readonly byte[] ADD_TO_ESP = new byte[] { 0x83, 0xC4 };
			public static readonly byte[] PUSH_WORD_PTR_DS = new byte[] { 0xFF, 0x35 };

			public static readonly byte[] CALL_EAX = new byte[] { 0xFF, 0xD0 };
		}

		private List<byte> _asm;

		public void Write(byte[] asm) => this._asm.AddRange(asm);


        #region Push Registers

        /// <summary>
        /// Push ESI to the stack.
        /// </summary>
        public void PushEAX() => this._asm.Add(ASMInstr.PUSH_EAX);

        /// <summary>
        /// Push ECX to the stack.
        /// </summary>
        public void PushECX() => this._asm.Add(ASMInstr.PUSH_ECX);

        /// <summary>
        /// Push EDX to the stack.
        /// </summary>
        public void PushEDX() => this._asm.Add(ASMInstr.PUSH_EDX);

        /// <summary>
        /// Push EBX to the stack.
        /// </summary>
        public void PushEBX() => this._asm.Add(ASMInstr.PUSH_EBX);

        /// <summary>
        /// Push ESP to the stack.
        /// </summary>
        public void PushESP() => this._asm.Add(ASMInstr.PUSH_ESP);

        /// <summary>
        /// Push EBP to the stack.
        /// </summary>
        public void PushEBP() => this._asm.Add(ASMInstr.PUSH_EBP);

        /// <summary>
        /// Push ESI to the stack.
        /// </summary>
        public void PushESI() => this._asm.Add(ASMInstr.PUSH_ESI);

        /// <summary>
        /// Push EDI to the stack.
        /// </summary>
        public void PushEDI() => this._asm.Add(ASMInstr.PUSH_EDI);

        #endregion

        #region Pop Registers

        /// <summary>
        /// Pop ESI to the stack.
        /// </summary>
        public void PopEAX() => this._asm.Add(ASMInstr.POP_EAX);

        /// <summary>
        /// Pop ECX to the stack.
        /// </summary>
        public void PopECX() => this._asm.Add(ASMInstr.POP_ECX);

        /// <summary>
        /// Pop EDX to the stack.
        /// </summary>
        public void PopEDX() => this._asm.Add(ASMInstr.POP_EDX);

        /// <summary>
        /// Pop EBX to the stack.
        /// </summary>
        public void PopEBX() => this._asm.Add(ASMInstr.POP_EBX);

        /// <summary>
        /// Pop ESP to the stack.
        /// </summary>
        public void PopESP() => this._asm.Add(ASMInstr.POP_ESP);

        /// <summary>
        /// Pop EBP to the stack.
        /// </summary>
        public void PopEBP() => this._asm.Add(ASMInstr.POP_EBP);

        /// <summary>
        /// Pop ESI to the stack.
        /// </summary>
        public void PopESI() => this._asm.Add(ASMInstr.POP_ESI);

        /// <summary>
        /// Pop EDI to the stack.
        /// </summary>
        public void PopEDI() => this._asm.Add(ASMInstr.POP_EDI);

        #endregion

        #region Push Operands

        /// <summary>
        /// Pushes byte to the stack.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushBYTE(byte value) => this._asm.AddRange(new byte[] { ASMInstr.PUSH_BYTE, value });

        /// <summary>
        /// Pushes sbyte to the stack.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushBYTE(sbyte value) => this._asm.AddRange(new byte[] { ASMInstr.PUSH_BYTE, (byte)value });

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
            this._asm.Add(ASMInstr.PUSH_WORD);
            this._asm.AddRange(BitConverter.GetBytes(value));
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
            this._asm.Add(ASMInstr.PUSH_WORD);
            this._asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Pushes float to the stack.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushDWORD(float value)
        {
            this._asm.Add(ASMInstr.PUSH_WORD);
            this._asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Pushes double to the stack.
        /// </summary>
        /// <param name="value">Value to push.</param>
        public void PushQWORD(double value)
        {
            this._asm.Add(ASMInstr.PUSH_WORD);
            this._asm.AddRange(BitConverter.GetBytes(value));
        }

		#endregion

		#region Mov 16/32 Registers

		/// <summary>
		/// Movie a value to the EAX registry.
		/// </summary>
		/// <param name="value">Value to move.</param>
		public void MovToEAX(int value) => this.MovToEAX((uint)value);

        /// <summary>
        /// Move a value to the EAX registry
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToEAX(uint value)
        {
            this._asm.Add(ASMInstr.MOV_TO_EAX);
            this._asm.AddRange(BitConverter.GetBytes(value));
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
            this._asm.Add(ASMInstr.MOV_TO_ECX);
            this._asm.AddRange(BitConverter.GetBytes(value));
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
            this._asm.Add(ASMInstr.MOV_TO_EDX);
            this._asm.AddRange(BitConverter.GetBytes(value));
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
            this._asm.Add(ASMInstr.MOV_TO_EBX);
            this._asm.AddRange(BitConverter.GetBytes(value));
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
            this._asm.Add(ASMInstr.MOV_TO_ESP);
            this._asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move a value to the EDP registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToEDP(int value) => this.MovToEDP((uint)value);

        /// <summary>
        /// Move a value to the EDP registry.
        /// </summary>
        /// <param name="value">Value to move.</param>
        public void MovToEDP(uint value)
        {
            this._asm.Add(ASMInstr.MOV_TO_EDP);
            this._asm.AddRange(BitConverter.GetBytes(value));
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
            this._asm.Add(ASMInstr.MOV_TO_ESI);
            this._asm.AddRange(BitConverter.GetBytes(value));
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
            this._asm.Add(ASMInstr.MOV_TO_EDI);
            this._asm.AddRange(BitConverter.GetBytes(value));
        }

        #endregion


		/// <summary>
		/// Calls EAX registry.
		/// </summary>
		public void CallEAX() => this._asm.AddRange(ASMInstr.CALL_EAX);

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
            this._asm.AddRange(ASMInstr.PUSH_WORD_PTR_DS);
            this._asm.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Move the value stored at EAX to an address.
        /// </summary>
        /// <param name="address"></param>
        public void MovEAXToAddress(uint address)
        {
            this._asm.Add(0xA3);
            this._asm.AddRange(BitConverter.GetBytes(address));
        }

        /// <summary>
        /// Adds a value to ESP.
        /// </summary>
        /// <param name="value">Value to add.</param>
        public void AddToESP(int value)
        {
            this._asm.AddRange(ASMInstr.ADD_TO_ESP);
            this._asm.AddRange(BitConverter.GetBytes(value));
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
            this._asm.Add(0xE9);
            this._asm.AddRange(BitConverter.GetBytes(address));
        }

        /// <summary>
        /// Calls retn.
        /// </summary>
        /// <returns></returns>
        public void Return() => this._asm.Add(ASMInstr.RET_N);

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
            this._asm.Add(ASMInstr.RET_WORD);
            this._asm.AddRange(BitConverter.GetBytes(value));
        }
    }
}
