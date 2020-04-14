using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CoreExtensions.Conversions;



namespace CoreExtensions.Native
{
    /// <summary>
    /// <see cref="Enum"/> that returns result of processing InjectorX methods.
    /// </summary>
    public enum ASMResult : byte
    {
        None = 0,
        Success = 1,
        WritingFailed = 2,
        InvalidSize = 3,
        RemoteThreadFailure = 4,
        AllocationFaliure = 5,
        ByteCastFailure = 6,
    }

    /// <summary>
    /// Represents <see cref="Enum"/> of all possible InjectorX instructions.
    /// </summary>
    internal enum InjectInstr : byte
    {
        NOP = 0x90,
        MOV = 0xB8,
        RET = 0xC2,
        RETN = 0xC3,
        CALL = 0xE8,
        JMP = 0xE9,
    }

    /// <summary>
    /// Class with methods of writing to thread memory.
    /// </summary>
    public static class InjectorX
	{
		public static Process FindProcess(string name) 
			=> Process.GetProcesses()?.ToList().Find(p => p.ProcessName == name);

        public static byte[] GetMemory<TypeID>(TypeID value)
        {
            try
            {
                if (value is IConvertible convertible)
                {
                    return Type.GetTypeCode(typeof(TypeID)) switch
                    {
                        TypeCode.Boolean => BitConverter.GetBytes(convertible.StaticCast<bool>()),
                        TypeCode.Byte => BitConverter.GetBytes(convertible.StaticCast<byte>()),
                        TypeCode.SByte => BitConverter.GetBytes(convertible.StaticCast<sbyte>()),
                        TypeCode.Int16 => BitConverter.GetBytes(convertible.StaticCast<short>()),
                        TypeCode.UInt16 => BitConverter.GetBytes(convertible.StaticCast<ushort>()),
                        TypeCode.Int32 => BitConverter.GetBytes(convertible.StaticCast<int>()),
                        TypeCode.UInt32 => BitConverter.GetBytes(convertible.StaticCast<uint>()),
                        TypeCode.Int64 => BitConverter.GetBytes(convertible.StaticCast<long>()),
                        TypeCode.UInt64 => BitConverter.GetBytes(convertible.StaticCast<ulong>()),
                        TypeCode.Single => BitConverter.GetBytes(convertible.StaticCast<float>()),
                        TypeCode.Double => BitConverter.GetBytes(convertible.StaticCast<double>()),
                        TypeCode.String => Encoding.UTF8.GetBytes(convertible.StaticCast<string>()),
                        _ => null
                    };
                }
                else if (value is IEnumerable enumerable) return enumerable.Cast<byte>().ToArray();
                else return null;
            }
            catch (Exception) { return null; }
        }

        public static ASMResult WriteMemory<TypeID>(Process process, uint address, TypeID value)
        {
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            var array = GetMemory(value);
            if (array == null) return ASMResult.ByteCastFailure;
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            NativeCall.CloseHandle(hProcess);
            if (array.Length == num) return ASMResult.Success;
            else return ASMResult.WritingFailed;
        }
        
        public static ASMResult ReturnValue<TypeID>(Process process, uint address, TypeID value)
        {
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            var array = new byte[6] { (byte)InjectInstr.MOV, 0, 0, 0, 0, (byte)InjectInstr.RETN };
            var val = value.ReinterpretCast(typeof(uint));
            if (val == null) return ASMResult.ByteCastFailure;
            var diff = BitConverter.GetBytes((uint)val);
            for (int a1 = 0; a1 < 4; ++a1) array[1 + a1] = diff[a1];
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            NativeCall.CloseHandle(hProcess);
            if (array.Length == num) return ASMResult.Success;
            else return ASMResult.WritingFailed;
        }

        public static ASMResult MakeJMP(Process process, uint address, uint function)
        {
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            var array = new byte[5] { (byte)InjectInstr.JMP, 0, 0, 0, 0 };
            var diff = BitConverter.GetBytes(function - address - 5);
            for (int a1 = 0; a1 < 4; ++a1) array[1 + a1] = diff[a1];
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            NativeCall.CloseHandle(hProcess);
            if (array.Length == num) return ASMResult.Success;
            else return ASMResult.WritingFailed;
        }

        public static ASMResult MakeCALL(Process process, uint address, uint function)
        {
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            var array = new byte[5] { (byte)InjectInstr.CALL, 0, 0, 0, 0 };
            var diff = BitConverter.GetBytes(function - address - 5);
            for (int a1 = 0; a1 < 4; ++a1) array[1 + a1] = diff[a1];
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            NativeCall.CloseHandle(hProcess);
            if (array.Length == num) return ASMResult.Success;
            else return ASMResult.WritingFailed;
        }

        public static ASMResult MakeRETN(Process process, uint address, ushort size)
        {
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            if (size != 0)
            {
                var array = new byte[3] { (byte)InjectInstr.RET, (byte)(size & 0xFF), (byte)(size >> 8) };
                NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
                NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
                NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
                NativeCall.CloseHandle(hProcess);
                if (array.Length == num) return ASMResult.Success;
                else return ASMResult.WritingFailed;
            }
            else
            {
                var array = new byte[1] { (byte)InjectInstr.RETN };
                NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
                NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
                NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
                NativeCall.CloseHandle(hProcess);
                if (array.Length == num) return ASMResult.Success;
                else return ASMResult.WritingFailed;
            }
        }
    
        public static ASMResult MakeNOP(Process process, uint address, int size)
        {
            if (size <= 0) return ASMResult.InvalidSize;
            var hProcess = NativeCall.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            var array = new byte[size];
            for (int a1 = 0; a1 < size; ++a1) array[a1] = (byte)InjectInstr.NOP;
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, 4, out var old);
            NativeCall.WriteProcessMemory(hProcess, (IntPtr)address, array, (uint)array.Length, out var num);
            NativeCall.VirtualProtect(hProcess, (IntPtr)address, (UIntPtr)array.Length, old, out old);
            NativeCall.CloseHandle(hProcess);
            if (array.Length == num) return ASMResult.Success;
            else return ASMResult.WritingFailed;
        }      
    }
}
