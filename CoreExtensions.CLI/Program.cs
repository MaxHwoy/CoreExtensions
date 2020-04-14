using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CoreExtensions.IO;
using CoreExtensions.Text;
using CoreExtensions.Native;
using CoreExtensions.Reflection;
using CoreExtensions.Management;
using CoreExtensions.Conversions;


namespace CoreExtensions.CLI
{
	enum eType : int
	{
		None = 0,
		One = 1,
		Two = 2,
		Three = 3,
		Four = 4,
		Five = 5,
		Six = 6,
		Seven = 7,
		Eight = 8,
		Nine = 9
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 0xA8)]
	struct MySTR
	{
		public uint ID { get; set; }
		public int Size { get; set; }
		public uint ClassKey { get; set; }
		public int Localizer1 { get; set; }
		public int Localizer2 { get; set; }
		public uint BinKey { get; set; }
		public int Localizer3 { get; set; }

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1C)]
		private string _collection_name;

		public string CollectionName
		{
			get => this._collection_name;
			set => this._collection_name = value;
		}

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x1C)]
		private float[] _values;

		public float GetValue(int index) => this._values[index];
	}

	class Program
	{
		static void Main(string[] args)
		{
			long noX = 0;
			long yesX = 0;
			var arr = new byte[4] { 0x00, 0xEF, 0xCD, 0xAB };

			var watch = new Stopwatch();

			watch.Start();
			for (int a1 = 0; a1 < 100000000; ++a1)
				BitConverterX.ToInt32(arr, 0);
			watch.Stop();
			yesX = watch.ElapsedMilliseconds;
			watch.Reset();

			watch.Start();
			for (int a1 = 0; a1 < 100000000; ++a1)
				BitConverter.ToInt32(arr, 0);
			watch.Stop();
			noX = watch.ElapsedMilliseconds;
			watch.Reset();
			int aa = 0;
		}
	}
}
