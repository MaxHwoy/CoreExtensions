using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CoreExtensions.Native;
using CoreExtensions.Management;
using CoreExtensions.Conversions;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Support.Carbon.Class;
using Nikki.Support.Carbon.Parts.CarParts;



namespace CoreExtensions.CLI
{
    class Program
	{
		public static RealCarPart NewWheelsWork(DBModelPart x, string curretPartName, int size, string brand, string style, int styleNumber, int unk)
		{
			var carPart = new RealCarPart(x.Index, x)
			{
				PartName = curretPartName
			};

			carPart.TryAddAttribute("TEXTURE_NAME");
			carPart.GetAttribute("TEXTURE_NAME").SetValue("Value", Hashing.BinHash($"{brand}_{style}"));

			carPart.TryAddAttribute(0x1b0ea1a9);
			carPart.GetAttribute((uint)0x1b0ea1a9).SetValue("Value", 7);

			carPart.TryAddAttribute("LOD_CHARACTERS_OFFSET");
			carPart.GetAttribute("LOD_CHARACTERS_OFFSET").SetValue("Value", "ABCD ");
			carPart.GetAttribute("LOD_CHARACTERS_OFFSET").SetValue("ValueExists", eBoolean.True);

			carPart.TryAddAttribute("LOD_NAME_PREFIX_SELECTOR");
			carPart.GetAttribute("LOD_NAME_PREFIX_SELECTOR").SetValue("Value", 1);

			carPart.TryAddAttribute("STOCK_MATERIAL");
			carPart.GetAttribute("STOCK_MATERIAL").SetValue("Value", eBoolean.True);

			carPart.TryAddAttribute("ONLINE");
			carPart.GetAttribute("ONLINE").SetValue("Value", eBoolean.False);

			carPart.TryAddAttribute("NAME_OFFSET");
			carPart.GetAttribute("NAME_OFFSET").SetValue("Value", $"PART_WHEEL_{brand}_{styleNumber} {size} 25");
			carPart.GetAttribute("NAME_OFFSET").SetValue("ValueExists", eBoolean.True);

			carPart.TryAddAttribute(0x87557E1E);
			carPart.GetAttribute((uint)0x87557E1E).SetValue("Value", eBoolean.True);

			carPart.TryAddAttribute("PARTID_UPGRADE_GROUP");
			carPart.GetAttribute("PARTID_UPGRADE_GROUP").SetValue("Unknown", unk);
			carPart.GetAttribute("PARTID_UPGRADE_GROUP").SetValue("ID", 85);

			carPart.TryAddAttribute("PART_NAME_SELECTOR");
			carPart.GetAttribute("PART_NAME_SELECTOR").SetValue("Value", 1);

			carPart.TryAddAttribute("PART_NAME_OFFSETS");
			carPart.GetAttribute("PART_NAME_OFFSETS").SetValue("Value1", $"{style}");
			carPart.GetAttribute("PART_NAME_OFFSETS").SetValue("Value1Exists", eBoolean.True);
			carPart.GetAttribute("PART_NAME_OFFSETS").SetValue("Value2", $"{size}_25");
			carPart.GetAttribute("PART_NAME_OFFSETS").SetValue("Value2Exists", eBoolean.True);

			carPart.TryAddAttribute(0xCE7D8DB5);
			carPart.GetAttribute((uint)0xCE7D8DB5).SetValue("Value", 25);

			carPart.TryAddAttribute("MAX_LOD");
			carPart.GetAttribute("MAX_LOD").SetValue("Value", 3);

			carPart.TryAddAttribute(0xEB0101E2);
			carPart.GetAttribute((uint)0xEB0101E2).SetValue("Value", size);

			carPart.TryAddAttribute(0xEBB03E66);
			carPart.GetAttribute((uint)0xEBB03E66).SetValue("Value", Hashing.BinHash($"{brand}"));

			carPart.TryAddAttribute("LOD_BASE_NAME");
			carPart.GetAttribute("LOD_BASE_NAME").SetValue("Value1", $"{style}");
			carPart.GetAttribute("LOD_BASE_NAME").SetValue("Value1Exists", eBoolean.True);
			carPart.GetAttribute("LOD_BASE_NAME").SetValue("Value2", $"{size}_25");
			carPart.GetAttribute("LOD_BASE_NAME").SetValue("Value2Exists", eBoolean.True);

			return carPart;
		}


		static unsafe void Main(string[] args)
		{
			//var bytes = File.ReadAllBytes("jdlz_csharp.bin");
			//bytes = Interop.Decompress(bytes);



			var db = new Nikki.Database.Carbon(true);
			
			var options_load = new Options()
			{
				File = @"E:\NFS\NFSC\CARS\TRAFCEMTR\TEXTURES.BIN",
				Flags = eOptFlags.Materials | eOptFlags.CarTypeInfos | eOptFlags.PresetRides |
						eOptFlags.PresetSkins | eOptFlags.Collisions | eOptFlags.DBModelParts |
						eOptFlags.FNGroups | eOptFlags.TPKBlocks | eOptFlags.STRBlocks |
						eOptFlags.Tracks | eOptFlags.SunInfos
			};


			
			var options_save = new Options()
			{
				File = @"E:\NFS\NFSC\CARS\TRAFCEMTR\TEXTURES.BIN",
				Watermark = $"Nikki by MaxHwoy | {DateTime.Today:MM/dd/yyyy}",
				MessageShow = false,
				Compress = false,
				Flags = eOptFlags.Materials | eOptFlags.CarTypeInfos | eOptFlags.PresetRides |
						eOptFlags.PresetSkins | eOptFlags.Collisions | eOptFlags.DBModelParts |
						eOptFlags.FNGroups | eOptFlags.TPKBlocks | eOptFlags.STRBlocks |
						eOptFlags.Tracks | eOptFlags.SunInfos
			};

			//Manager.LoadBinKeys(new string[] { @"E:\MaxHwoy\source\repos\Nikki\Nikki\keys.txt" });
			//Manager.LoadVaultAttributes(@"C:\Need for Speed Carbon\GLOBAL\attributes.bin");
			//Manager.LoadVaultFEAttribs(@"C:\Need for Speed Carbon\GLOBAL\fe_attrib.bin");

			//db.Load(options_load);

			var array = File.ReadAllBytes(@"E:\NFS\Need for Speed Carbon\scripts\BlockData\0x00000A08");

			array = Interop.Decompress(array);

			var result = false;


			using (var bw = new BinaryWriter(File.Open(@"OUTPUT.BIN", FileMode.Create)))
			{
				bw.Write(array);
			}



			//db.Save(options_save);


			int aaa = 0;


		}
	}
}
