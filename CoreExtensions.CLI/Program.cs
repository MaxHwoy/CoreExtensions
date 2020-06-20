using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CoreExtensions.IO;
using CoreExtensions.Management;
using CoreExtensions.Conversions;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Enum.CP;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Enum.SlotID;
using CoreExtensions.Types;





namespace CoreExtensions.CLI
{
	public static class AllIn
	{
		public static Options c_load = new Options()
		{
			File = @"E:\NFS\Need for Speed Carbon\GLOBAL\GlobalBTest.lzc",
			//File = @"E:\NFS\Need for Speed Carbon\LANGUAGES\English_Global.bin",
		};
		public static Options mw_load = new Options()
		{
			File = @"E:\NFS\Need for Speed Most Wanted\GLOBAL\GlobalBTest.lzc",
		};
		public static Options ps_load = new Options()
		{
			File = @"E:\NFS\Need for Speed Prostreet\GLOBAL\GlobalBTest.lzc",
		};

		public static Options c_save = new Options()
		{
			File = @"E:\NFS\Need for Speed Carbon\GLOBAL\GlobalB.lzc",
			Watermark = $"Nikki by MaxHwoy | {DateTime.Today:MM/dd/yyyy}",
			MessageShow = false,
			Compress = false,
		};
		public static Options mw_save = new Options()
		{
			File = @"E:\NFS\Need for Speed Most Wanted\GLOBAL\GlobalB.lzc",
			Watermark = $"Nikki by MaxHwoy | {DateTime.Today:MM/dd/yyyy}",
			MessageShow = false,
			Compress = false,
		};
		public static Options ps_save = new Options()
		{
			File = @"E:\NFS\Need for Speed Prostreet\GLOBAL\GlobalB.lzc",
			Watermark = $"Nikki by MaxHwoy | {DateTime.Today:MM/dd/yyyy}",
			MessageShow = false,
			Compress = false,
		};

		private static void RunCarbon()
		{
			Loader.LoadBinKeys(new string[] { @"E:\MaxHwoy\source\repos\Nikki\Nikki\keys.txt" });
			Loader.LoadVaultAttributes(@"E:\NFS\Need for Speed Carbon\GLOBAL\attributes.bin");
			Loader.LoadVaultFEAttribs(@"E:\NFS\Need for Speed Carbon\GLOBAL\fe_attrib.bin");
		}
		private static void RunMostWanted()
		{
			Loader.LoadBinKeys(new string[] { @"E:\MaxHwoy\source\repos\Nikki\Nikki\keys.txt" });
			Loader.LoadVaultAttributes(@"E:\NFS\Need for Speed Most Wanted\GLOBAL\attributes.bin");
			Loader.LoadVaultFEAttribs(@"E:\NFS\Need for Speed Most Wanted\GLOBAL\fe_attrib.bin");
		}
		private static void RunProstreet()
		{
			Loader.LoadBinKeys(new string[] { @"E:\MaxHwoy\source\repos\Nikki\Nikki\keys.txt" });
			Loader.LoadVaultAttributes(@"E:\NFS\Need for Speed Prostreet\GLOBAL\attributes.bin");
			Loader.LoadVaultFEAttribs(@"E:\NFS\Need for Speed Prostreet\GLOBAL\fe_attrib.bin");
		}

		public static bool Load(FileBase filebase)
		{
			switch (filebase.GameINT)
			{
				case GameINT.Carbon:
					RunCarbon();
					return filebase.Load(c_load);

				case GameINT.MostWanted:
					RunMostWanted();
					return filebase.Load(mw_load);

				case GameINT.Prostreet:
					RunProstreet();
					return filebase.Load(ps_load);

				default:
					return false;

			}
		}
		public static bool Save(FileBase filebase)
		{
			switch (filebase.GameINT)
			{
				case GameINT.Carbon:
					RunCarbon();
					return filebase.Save(c_save);

				case GameINT.MostWanted:
					RunMostWanted();
					return filebase.Save(mw_save);

				case GameINT.Prostreet:
					RunProstreet();
					return filebase.Save(ps_save);

				default:
					return false;

			}
		}
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 0x30)]
	public struct Vector4
	{
		public int Const1 { get; set; }
		public int Const2 { get; set; }
		public short Unknown1 { get; set; }
		public short Unknown2 { get; set; }
		public int ID { get; set; }
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
		public float W { get; set; }
		public float PadX { get; set; }
		public float PadY { get; set; }
		public float PadZ { get; set; }
		public float PadW { get; set; }
		public override string ToString()
		{
			return $"{this.Const1}\t{this.Const2}\t{this.Unknown1}\t{this.Unknown2}\t" +
				$"{this.ID}\t{this.X}\t{this.Y}\t{this.Z}\t{this.W}" +
				$"{this.PadX}\t{this.PadY}\t{this.PadZ}\t{this.PadW}";

		}
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 0x58)]
	public struct CVHeader
	{
		public int Const1 { get; set; }
		public int Const2 { get; set; }
		public int Unknown0 { get; set; }
		public float Unknown1 { get; set; }
		public int Unknown2 { get; set; }
		public int NumberOfPoints { get; set; }
		public uint BinKey { get; set; }

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
		public string Name;

		public float Unknown3 { get; set; }
		public int Unknown4 { get; set; }
		public int Unknown5 { get; set; }
		public int Unknown6 { get; set; }
		public int Unknown7 { get; set; }
		public int Unknown8 { get; set; }
		public float Unknown9 { get; set; }

		public override string ToString()
		{
			return $"{this.Const1}\t{this.Const2}\t{this.Unknown0}\t{this.Unknown1}\t{this.Unknown2}\t" +
				$"{this.NumberOfPoints}\t0x{this.BinKey:X8}\t{this.Name}\t{this.Unknown3}\t" +
				$"{this.Unknown4}\t{this.Unknown5}\t{this.Unknown6}\t{this.Unknown7}\t" +
				$"{this.Unknown8}\t{this.Unknown9}\t";
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var path = @"E:\NFS\Need for Speed Carbon\scripts\NFSCExtraOptionsSettings.ini";

			using var reader = new IniReader(path)
			{
				ArraySeparator = ",|",
				CommentDelimiter = "//"
			};

			//reader.ReadAllLines();

			int aaa = 0;

		}
	}
}
