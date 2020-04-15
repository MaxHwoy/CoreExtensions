using System;
using System.Linq;
using System.Collections;
using System.Diagnostics;
using CoreExtensions.Text;
using CoreExtensions.Conversions;



namespace CoreExtensions.Native
{
    /// <summary>
    /// <see cref="Enum"/> that returns result of processing InjectorX methods.
    /// </summary>
    public enum InjectResult : byte
    {
        /// <summary>
        /// Indicates zero result.
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Indicates ASM success.
        /// </summary>
        Success = 1,
        
        /// <summary>
        /// Indicates that memory writing failed.
        /// </summary>
        WritingFailed = 2,
        
        /// <summary>
        /// Indicates that value passed was of invalid size.
        /// </summary>
        InvalidSize = 3,
        
        /// <summary>
        /// Indicates failure to create or gain access to a remote thread.
        /// </summary>
        RemoteThreadFailure = 4,
        
        /// <summary>
        /// Indicates failure to allocate memory in the process.
        /// </summary>
        AllocationFaliure = 5,
        
        /// <summary>
        /// Indicates that value passed could not be casted to a byte array.
        /// </summary>
        ByteCastFailure = 6,
    }

    /// <summary>
    /// Class with methods of writing to thread memory.
    /// </summary>
    public static class InjectorX
	{
        /// <summary>
        /// Finds process in the system by name provided. The result process will be the 
        /// first one opened if there are multiple instances.
        /// </summary>
        /// <returns>Process with the name specified.</returns>
		public static Process FindProcess(string name) 
			=> Process.GetProcesses()?.ToList().Find(p => p.ProcessName == name);

        /// <summary>
        /// Gets hProcess handle from <see cref="Process"/> provided.
        /// </summary>
        /// <param name="process"><see cref="Process"/> to get handle from.</param>
        /// <returns>Pointer to the base address of the process.</returns>
        public static IntPtr GetHandle(Process process)
            => NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);

        /// <summary>
        /// Closes hProcess handle with pointer provided.
        /// </summary>
        /// <param name="hProcess">Pointer to base address of a <see cref="Process"/>.</param>
        /// <returns>Result of closing.</returns>
        public static int CloseHandle(IntPtr hProcess) => NativeCall.CloseHandle(hProcess);

        /// <summary>
        /// Represents <see cref="Enum"/> of all possible InjectorX instructions.
        /// </summary>
        private enum InjectInstr : byte
        {
            NOP = 0x90,
            MOV = 0xB8,
            RET = 0xC2,
            RETN = 0xC3,
            CALL = 0xE8,
            JMP = 0xE9,
        }

        #region WriteMemory

        /// <summary>
        /// Writes memory of an object passed to the process specified at the address provided.
        /// </summary>
        /// <param name="process">Process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="value">Object to write. This object should be either of type 
        /// <see cref="IConvertible"/> or <see cref="IEnumerable"/>.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult WriteMemory(Process process, uint address, object value)
        {
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            var array = value.GetMemory();
            if (array == null) return InjectResult.ByteCastFailure;
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            NativeCall.CloseHandle(hProcess);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        /// <summary>
        /// Writes memory of an object passed to the process specified at the address provided.
        /// </summary>
        /// <param name="hProcess">Pointer to process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="value">Object to write. This object should be either of type 
        /// <see cref="IConvertible"/> or <see cref="IEnumerable"/>.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult WriteMemory(IntPtr hProcess, uint address, object value)
        {
            var array = value.GetMemory();
            if (array == null) return InjectResult.ByteCastFailure;
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        /// <summary>
        /// Writes byte array passed to the process specified at the address provided.
        /// </summary>
        /// <param name="process">Process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="array">Byte array to write to memory.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult WriteMemory(Process process, uint address, byte[] array)
        {
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            if (array == null) return InjectResult.ByteCastFailure;
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            NativeCall.CloseHandle(hProcess);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        /// <summary>
        /// Writes byte array passed to the process specified at the address provided.
        /// </summary>
        /// <param name="hProcess">Pointer to process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="array">Byte array to write to memory..</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult WriteMemory(IntPtr hProcess, uint address, byte[] array)
        {
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        #endregion

        #region ReturnValue

        /// <summary>
        /// Makes function at the specified address of the process provided return a 
        /// specific value passed.
        /// </summary>
        /// <param name="process">Process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="value">Value that function should return. This value should 
        /// be convertible to 4-byte unsigned integer type.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult ReturnValue(Process process, uint address, object value)
        {
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            var array = new byte[6] { (byte)InjectInstr.MOV, 0, 0, 0, 0, (byte)InjectInstr.RETN };
            var val = value.ReinterpretCast(typeof(uint));
            if (val == null) return InjectResult.ByteCastFailure;
            var diff = BitConverter.GetBytes((uint)val);
            for (int a1 = 0; a1 < 4; ++a1) array[1 + a1] = diff[a1];
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            NativeCall.CloseHandle(hProcess);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        /// <summary>
        /// Makes function at the specified address of the process provided return a 
        /// specific value passed.
        /// </summary>
        /// <param name="hProcess">Pointer to process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="value">Value that function should return. This value should 
        /// be convertible to 4-byte unsigned integer type.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult ReturnValue(IntPtr hProcess, uint address, object value)
        {
            var array = new byte[6] { (byte)InjectInstr.MOV, 0, 0, 0, 0, (byte)InjectInstr.RETN };
            var val = value.ReinterpretCast(typeof(uint));
            if (val == null) return InjectResult.ByteCastFailure;
            var diff = BitConverter.GetBytes((uint)val);
            for (int a1 = 0; a1 < 4; ++a1) array[1 + a1] = diff[a1];
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        /// <summary>
        /// Makes function at the specified address of the process provided return a 
        /// specific value passed.
        /// </summary>
        /// <param name="process">Process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="value">Value that function should return.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult ReturnValue(Process process, uint address, uint value)
        {
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            var array = new byte[6] { (byte)InjectInstr.MOV, 0, 0, 0, 0, (byte)InjectInstr.RETN };
            var diff = BitConverter.GetBytes(value);
            for (int a1 = 0; a1 < 4; ++a1) array[1 + a1] = diff[a1];
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            NativeCall.CloseHandle(hProcess);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        /// <summary>
        /// Makes function at the specified address of the process provided return a 
        /// specific value passed.
        /// </summary>
        /// <param name="hProcess">Pointer to process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="value">Value that function should return.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult ReturnValue(IntPtr hProcess, uint address, uint value)
        {
            var array = new byte[6] { (byte)InjectInstr.MOV, 0, 0, 0, 0, (byte)InjectInstr.RETN };
            var diff = BitConverter.GetBytes(value);
            for (int a1 = 0; a1 < 4; ++a1) array[1 + a1] = diff[a1];
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        #endregion

        #region MakeInstr

        /// <summary>
        /// Writes JMP instruction to the address provided in the process specified.
        /// </summary>
        /// <param name="process">Process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="function">Function to where JMP point.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult MakeJMP(Process process, uint address, uint function)
        {
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            var array = new byte[5] { (byte)InjectInstr.JMP, 0, 0, 0, 0 };
            var diff = BitConverter.GetBytes(function - address - 5);
            for (int a1 = 0; a1 < 4; ++a1) array[1 + a1] = diff[a1];
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            NativeCall.CloseHandle(hProcess);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        /// <summary>
        /// Writes JMP instruction to the address provided in the process specified.
        /// </summary>
        /// <param name="hProcess">Pointer to process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="function">Function to where JMP point.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult MakeJMP(IntPtr hProcess, uint address, uint function)
        {
            var array = new byte[5] { (byte)InjectInstr.JMP, 0, 0, 0, 0 };
            var diff = BitConverter.GetBytes(function - address - 5);
            for (int a1 = 0; a1 < 4; ++a1) array[1 + a1] = diff[a1];
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        /// <summary>
        /// Writes CALL instruction to the address provided in the process specified.
        /// </summary>
        /// <param name="process">Process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="function">Function that should be called.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult MakeCALL(Process process, uint address, uint function)
        {
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            var array = new byte[5] { (byte)InjectInstr.CALL, 0, 0, 0, 0 };
            var diff = BitConverter.GetBytes(function - address - 5);
            for (int a1 = 0; a1 < 4; ++a1) array[1 + a1] = diff[a1];
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            NativeCall.CloseHandle(hProcess);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        /// <summary>
        /// Writes CALL instruction to the address provided in the process specified.
        /// </summary>
        /// <param name="hProcess">Pointer to process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="function">Function that should be called.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult MakeCALL(IntPtr hProcess, uint address, uint function)
        {
            var array = new byte[5] { (byte)InjectInstr.CALL, 0, 0, 0, 0 };
            var diff = BitConverter.GetBytes(function - address - 5);
            for (int a1 = 0; a1 < 4; ++a1) array[1 + a1] = diff[a1];
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        /// <summary>
        /// Writes RETN instruction to the address provided in the process specified.
        /// </summary>
        /// <param name="process">Process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="size">Size of the return. 0 by default.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult MakeRETN(Process process, uint address, ushort size = 0)
        {
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            if (size != 0)
            {
                var array = new byte[3] { (byte)InjectInstr.RET, (byte)(size & 0xFF), (byte)(size >> 8) };
                NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
                NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
                NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
                NativeCall.CloseHandle(hProcess);
                if (array.Length == num) return InjectResult.Success;
                else return InjectResult.WritingFailed;
            }
            else
            {
                var array = new byte[1] { (byte)InjectInstr.RETN };
                NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
                NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
                NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
                NativeCall.CloseHandle(hProcess);
                if (array.Length == num) return InjectResult.Success;
                else return InjectResult.WritingFailed;
            }
        }

        /// <summary>
        /// Writes RETN instruction to the address provided in the process specified.
        /// </summary>
        /// <param name="hProcess">Pointer to process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="size">Size of the return. 0 by default.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult MakeRETN(IntPtr hProcess, uint address, ushort size = 0)
        {
            if (size != 0)
            {
                var array = new byte[3] { (byte)InjectInstr.RET, (byte)(size & 0xFF), (byte)(size >> 8) };
                NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
                NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
                NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
                if (array.Length == num) return InjectResult.Success;
                else return InjectResult.WritingFailed;
            }
            else
            {
                var array = new byte[1] { (byte)InjectInstr.RETN };
                NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
                NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
                NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
                if (array.Length == num) return InjectResult.Success;
                else return InjectResult.WritingFailed;
            }
        }

        /// <summary>
        /// Writes NOP instructions to the address provided in the process specified.
        /// </summary>
        /// <param name="process">Process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="size">Size of the NOP.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult MakeNOP(Process process, uint address, int size)
        {
            if (size <= 0) return InjectResult.InvalidSize;
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            var array = new byte[size];
            for (int a1 = 0; a1 < size; ++a1) array[a1] = (byte)InjectInstr.NOP;
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            NativeCall.CloseHandle(hProcess);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        /// <summary>
        /// Writes NOP instructions to the address provided in the process specified.
        /// </summary>
        /// <param name="hProcess">Pointer to process where write memory.</param>
        /// <param name="address">Address of the process at which memory writing should occur.</param>
        /// <param name="size">Size of the NOP.</param>
        /// <returns><see cref="InjectResult"/> of the memory writing.</returns>
        public static InjectResult MakeNOP(IntPtr hProcess, uint address, int size)
        {
            if (size <= 0) return InjectResult.InvalidSize;
            var array = new byte[size];
            for (int a1 = 0; a1 < size; ++a1) array[a1] = (byte)InjectInstr.NOP;
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            if (array.Length == num) return InjectResult.Success;
            else return InjectResult.WritingFailed;
        }

        #endregion
    }
}
