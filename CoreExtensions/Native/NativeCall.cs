using System;
using System.Runtime.InteropServices;



namespace CoreExtensions.Native
{
    internal static class NativeCall
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags flags, [MarshalAs(UnmanagedType.Bool)] bool inherit_handle, int process_id);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr address, byte[] buffer, uint size, out int bytes_written);

        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAlloc(IntPtr hProcess, IntPtr address, uint size, AllocationType alloc_type, MemoryProtection protect);

        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtect(IntPtr hProcess, IntPtr address, UIntPtr size, uint new_protect, out uint old_protect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int CloseHandle(IntPtr hProcess);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr thread_attrib, uint stack_size, IntPtr address, IntPtr parameter, uint flags, out uint ThreadId); // Creates a remote thread.

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint GetCurrentThreadId();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenThread(uint desiredAccess, bool inheritHandle, uint threadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int TerminateThread(IntPtr handle, int exitcode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CancelSynchronousIo(IntPtr threadHandle);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr address, byte[] buffer, uint size, int bytes_read);

        [DllImport("kernel32.dll")]
        public static extern void AllocConsole();

        [DllImport("kernel32.dll")]
        public static extern void FreeConsole();
    }

    [Flags]
    internal enum ProcessAccessFlags : uint
    {
        All = 0x001F0FFF,
        Terminate = 0x00000001,
        CreateThread = 0x00000002,
        VMOperation = 0x00000008,
        VMRead = 0x00000010,
        VMWrite = 0x00000020,
        DupHandle = 0x00000040,
        SetInformation = 0x00000200,
        QueryInformation = 0x00000400,
        Synchronize = 0x00100000
    }

    [Flags]
    internal enum AllocationType
    {
        Commit = 0x00001000,
        Reserve = 0x00002000,
        Decommit = 0x00004000,
        Release = 0x00008000,
        Reset = 0x00080000,
        TopDown = 0x00100000,
        WriteWatch = 0x00200000,
        Physical = 0x00400000,
        LargePages = 0x20000000
    }

    [Flags]
    internal enum MemoryProtection
    {
        NoAccess = 0x0001,
        ReadOnly = 0x0002,
        ReadWrite = 0x0004,
        WriteCopy = 0x0008,
        Execute = 0x0010,
        ExecuteRead = 0x0020,
        ExecuteReadWrite = 0x0040,
        ExecuteWriteCopy = 0x0080,
        GuardModifierflag = 0x0100,
        NoCacheModifierflag = 0x0200,
        WriteCombineModifierflag = 0x0400
    }

    internal enum CONTEXT_FLAGS : uint
    {
        CONTEXT_i386 = 0x10000,
        CONTEXT_i486 = 0x10000,
        CONTEXT_CONTROL = CONTEXT_i386 | 0x01,
        CONTEXT_INTEGER = CONTEXT_i386 | 0x02,
        CONTEXT_SEGMENTS = CONTEXT_i386 | 0x04,
        CONTEXT_FLOATING_POINT = CONTEXT_i386 | 0x08,
        CONTEXT_DEBUG_REGISTERS = CONTEXT_i386 | 0x10,
        CONTEXT_EXTENDED_REGISTERS = CONTEXT_i386 | 0x20,
        CONTEXT_FULL = CONTEXT_CONTROL | CONTEXT_INTEGER | CONTEXT_SEGMENTS,
        CONTEXT_ALL = CONTEXT_CONTROL | CONTEXT_INTEGER | CONTEXT_SEGMENTS | CONTEXT_FLOATING_POINT | CONTEXT_DEBUG_REGISTERS | CONTEXT_EXTENDED_REGISTERS
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 0x260)]
    internal struct CONTEXT
    {
        public uint ContextFlags;
        public uint Dr0;
        public uint Dr1;
        public uint Dr2;
        public uint Dr3;
        public uint Dr6;
        public uint Dr7;
        public uint FloatSave;
        public uint SegGs;
        public uint SegFs;
        public uint SegEs;
        public uint SegDs;
        public uint Edi;
        public uint Esi;
        public uint Ebx;
        public uint Edx;
        public uint Ecx;
        public uint Eax;
        public uint Ebp;
        public uint Eip;
        public uint SegCs;
        public uint EFlags;
        public uint Esp;
        public uint SegSs;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] ExtendedRegisters;
    }

    [Flags]
    internal enum ThreadAccessFlags : int
    {
        TERMINATE = 0x0001,
        SUSPEND_RESUME = 0x0002,
        GET_CONTEXT = 0x0008,
        SET_CONTEXT = 0x0010,
        SET_INFORMATION = 0x0020,
        QUERY_INFORMATION = 0x0040,
        SET_THREAD_TOKEN = 0x0080,
        IMPERSONATE = 0x0100,
        DIRECT_IMPERSONATION = 0x0200
    }
}
