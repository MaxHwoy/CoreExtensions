using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Enum.CP;
using Nikki.Support.Carbon.Framework;
using Nikki.Support.Shared.Parts.CarParts;
using CoreExtensions.Conversions;



namespace CoreExtensions.CLI
{
	class Program
	{
		static void Main(string[] args)
		{
			var db = new Nikki.Database.Carbon(true);

			var options_load = new Options()
			{
				File = @"C:\Need for Speed Carbon\Global\GlobalBTest.lzc",
				Flags = eOptFlags.Materials | eOptFlags.CarTypeInfos | eOptFlags.PresetRides |
						eOptFlags.PresetSkins | eOptFlags.Collisions | eOptFlags.DBModelParts |
						eOptFlags.FNGroups | eOptFlags.TPKBlocks | eOptFlags.STRBlocks |
						eOptFlags.Tracks | eOptFlags.SunInfos
			};

			var options_save = new Options()
			{
				File = @"C:\Need for Speed Carbon\Global\GlobalB.lzc",
				Watermark = $"Nikki by MaxHwoy | {DateTime.Today:MM/dd/yyyy}",
				MessageShow = false,
				Compress = false,
				Flags = eOptFlags.Materials | eOptFlags.CarTypeInfos | eOptFlags.PresetRides |
						eOptFlags.PresetSkins | eOptFlags.Collisions | eOptFlags.DBModelParts |
						eOptFlags.FNGroups | eOptFlags.TPKBlocks | eOptFlags.STRBlocks |
						eOptFlags.Tracks | eOptFlags.SunInfos
			};

			Manager.LoadBinKeys(new string[] { @"E:\MaxHwoy\source\repos\Nikki\Nikki\keys.txt" });
			Manager.LoadVaultAttributes(@"C:\Need for Speed Carbon\Global\attributes.bin");
			Manager.LoadVaultFEAttribs(@"C:\Need for Speed Carbon\Global\fe_attrib.bin");

			db.Load(options_load);

			int aa = 0;

			db.Save(options_save);

			int aaa = 0;


		}
	}
}
