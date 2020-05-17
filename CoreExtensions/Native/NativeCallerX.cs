using System;
using System.Collections.Generic;
using System.Text;

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
	}
}
