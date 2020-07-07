using System;
using System.Diagnostics;
using System.Threading;



namespace CoreExtensions.Native
{
	/// <summary>
	/// Class with methods of calling native functions.
	/// </summary>
	public static class NativeCallerX
	{
		/// <summary>
		/// Allocates console for the current process.
		/// </summary>
		public static void AllocConsole() => NativeCall.AllocConsole();

		/// <summary>
		/// Deallocates console for the current process.
		/// </summary>
		public static void FreeConsole() => NativeCall.FreeConsole();

		/// <summary>
		/// Gets ID of the current thread.
		/// </summary>
		/// <returns>ID as a 4-byte signed integer value.</returns>
		public static int GetCurrentThreadID() => (int)NativeCall.GetCurrentThreadId();

		/// <summary>
		/// Terminates <see cref="Thread"/> specified.
		/// </summary>
		/// <param name="thread"><see cref="Thread"/> to terminate.</param>
		/// <returns><see langword="true"/> if thread was successfully terminated; 
		/// <see langword="false"/> otherwise.</returns>
		public static bool TerminateThread(Thread thread)
		{
			int code = 0;
			var id = thread.ManagedThreadId;
			var handle = NativeCall.OpenThread(0x40000000, false, (uint)id);
			var result = NativeCall.TerminateThread(handle, code);
			NativeCall.CloseHandle(handle);
			return result != 0;
		}

		/// <summary>
		/// Terminates <see cref="Thread"/> with ID specified.
		/// </summary>
		/// <param name="id">ID of the <see cref="Thread"/> to terminate.</param>
		/// <returns><see langword="true"/> if thread was successfully terminated; 
		/// <see langword="false"/> otherwise.</returns>
		public static bool TerminateThread(int id)
		{
			var handle = NativeCall.OpenThread(0x40000000, false, (uint)id);
			var result = NativeCall.CancelSynchronousIo(handle);
			NativeCall.CloseHandle(handle);
			return result;
		}
	}
}
