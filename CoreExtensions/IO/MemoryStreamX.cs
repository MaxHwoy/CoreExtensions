using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CoreExtensions.Types;



namespace CoreExtensions.IO
{
	/// <summary>
	/// Helper extensions for <see cref="MemoryStream"/> and <see cref="Stream"/> classes.
	/// </summary>
	public static class MemoryStreamX
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pointer"></param>
		/// <param name="numBytes"></param>
		/// <returns></returns>
		public static byte[] ReadBytes(Pointer pointer, int numBytes)
		{
			if (pointer.IsNull()) return null;
			var array = new byte[numBytes];
			Marshal.Copy(pointer, array, 0, numBytes);
			return array;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="pointer"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static T[] MarshalArray<T>(Pointer pointer, int length) where T : struct =>
			MarshalArray<T>(pointer, length, false);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="pointer"></param>
		/// <param name="length"></param>
		/// <param name="doubleIndirection"></param>
		/// <returns></returns>
		public static T[] MarshalArray<T>(Pointer pointer, int length, bool doubleIndirection) where T : struct
		{
			if (pointer.IsNull()) return null;

			try
			{

				var type = typeof(T);

				int size = doubleIndirection ? Pointer.Size : Marshal.SizeOf(type);
				T[] array = new T[length];

				for (int loop = 0; loop < length; loop++)
				{

					var ptr = pointer + size * loop;
					if (doubleIndirection) ptr = Marshal.ReadInt32(ptr);
					array[loop] = (T)Marshal.PtrToStructure(ptr, type);
				
				}
				
				return array;
			
			}
			catch { return null; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ptr"></param>
		/// <returns></returns>
		public static T MarshalStructure<T>(Pointer ptr) where T : struct
		{
			return ptr.IsNull() ? default : Marshal.PtrToStructure<T>(ptr);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ptr"></param>
		/// <returns></returns>
		public static T MarshalClass<T>(Pointer ptr) where T : class
		{
			return ptr.IsNull() ? null : Marshal.PtrToStructure<T>(ptr);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static byte[] GetBuffer(Stream stream)
		{
			stream.Position = 0;
			var array = new byte[stream.Length];
			stream.Read(array, 0, (int)stream.Length);
			return array;
		}
	}
}
