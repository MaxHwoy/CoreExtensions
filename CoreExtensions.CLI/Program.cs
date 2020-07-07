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
using Nikki.Support.MostWanted.Attributes;
using System.Reflection;
using System.Threading;
using CoreExtensions.Native;
using Nikki.Support.MostWanted.Class;
using Nikki.Utils.DDS;
using System.Runtime.CompilerServices;
using CoreExtensions.Text;

namespace CoreExtensions.CLI
{
	public static class AllIn
	{
		public static Options c_load = new Options()
		{
			File = @"E:\NFS\Need for Speed Carbon\GLOBAL\GlobalBTest.lzc",
			//File = @"E:\NFS\Need for Speed Carbon\LANGUAGES\English_Global.bin",
			//File = @"E:\NFS\Need for Speed Carbon\FRONTEND\FRONTB1.LZC",
			//File = @"E:\NFS\Need for Speed Carbon\TRACKS\STREAML5RATEST.BUN",
		};
		public static Options mw_load = new Options()
		{
			File = @"E:\NFS\Need for Speed Most Wanted\GLOBAL\GlobalBTest.lzc",
			//File = @"E:\NFS\Need for Speed Most Wanted\Cars\Supra\VinylsTest.bin",
		};
		public static Options ps_load = new Options()
		{
			File = @"E:\NFS\Need for Speed Prostreet\GLOBAL\GlobalBTest.lzc",
		};

		public static Options c_save = new Options()
		{
			File = @"E:\NFS\Need for Speed Carbon\GLOBAL\GlobalB.lzc",
			//File = @"E:\NFS\Need for Speed Carbon\TRACKS\STREAML5RATEST.BUN",
			Watermark = $"Nikki by MaxHwoy | {DateTime.Today:MM/dd/yyyy}",
			MessageShow = false,
			Compress = false,
		};
		public static Options mw_save = new Options()
		{
			File = @"E:\NFS\Need for Speed Most Wanted\GLOBAL\GlobalB.lzc",
			//File = @"E:\NFS\Need for Speed Most Wanted\Cars\Supra\Vinyls.bin",
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

		public static void Load(FileBase filebase)
		{
			switch (filebase.GameINT)
			{
				case GameINT.Carbon:
					RunCarbon();
					filebase.Load(c_load);
					return;

				case GameINT.MostWanted:
					RunMostWanted();
					filebase.Load(mw_load);
					return;

				case GameINT.Prostreet:
					RunProstreet();
					filebase.Load(ps_load);
					return;

				default:
					return;

			}
		}
		public static void Save(FileBase filebase)
		{
			switch (filebase.GameINT)
			{
				case GameINT.Carbon:
					RunCarbon();
					filebase.Save(c_save);
					return;

				case GameINT.MostWanted:
					RunMostWanted();
					filebase.Save(mw_save);
					return;

				case GameINT.Prostreet:
					RunProstreet();
					filebase.Save(ps_save);
					return;

				default:
					return;

			}
		}
	}



	class Program
	{
		static void Main2(string[] args)
		{
			var lines = File.ReadAllLines("somefile.end");
			var commands = new List<Base>();

			foreach (var line in lines)
			{

				if (String.IsNullOrEmpty(line) || line.StartsWith("//")) continue;
				var splits = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

				Base command = splits[0] switch
				{
					"command" => new Command(),
					"combobox" => new Combobox(),
					"checkbox" => new Checkbox(),
					"end" => new End(),
					_ => new Generic(),
				};

				command.Line = line;
				command.Parse(splits);
				commands.Add(command);

			}

			foreach (var command in commands)
			{

				if (command is ISelectable select)
				{

					Console.Write($"{select.Description} (");
					for (int i = 0; i < select.Options.Count; ++i) Console.Write($"{i} = {select.Options[i]}; ");
					Console.Write("): ");
					select.Choice = Convert.ToInt32(Console.ReadLine());

				}

			}

			var stack = new Stack<ISelectable>();
			
			for (int i = 0; i < commands.Count; ++i)
			{

				var command = commands[i];

				if (command is End end)
				{

					if (stack.Count > 0) stack.Pop(); // if stack is not empty
					else throw new Exception("Compiling failure"); // throw

				}

				else if (command is ISelectable select) // if selectable
				{

					stack.Push(select); // set selectable to find

					while (i < commands.Count) // bound it
					{

						// traverse till we find matching option
						var next = commands[++i]; // get next command

						if (next is Generic generic && // if matches, break
							select.Choice == select.ParseOption(generic.Type))
						{

							break;

						}

					}


				}

				else if (command is Generic generic) // if generic command
				{

					if (stack.Count == 0) throw new Exception("Invalid operation");
					var peek = stack.Peek(); // get last ISelectable

					if (peek.Contains(generic.Type)) // if contains
					{

						while (i < commands.Count) // bound it
						{

							// we traverse till we find end command
							var next = commands[++i];

							if (next is End final)
							{

								stack.Pop(); // pop from the stack
								break;

							}

						}

					}
					else throw new Exception("Invalid operation"); // throw

				}

				else
				{

					command.Execute(); // execute any other command

				}

			}



		}
	
		static void Main(string[] args)
		{
			var version = new Version("2.2");
			var compare = new Version("2.2");

			Console.WriteLine(version.CompareTo(compare));

			int aaaa = 0;
		}
	}

	public interface ISelectable
	{
		public int Choice { get; set; }
		public string Description { get; }
		public List<string> Options { get; }
		public int ParseOption(string option);
		public bool Contains(string option);
	}

	public abstract class Base
	{
		public string Line { get; set; } = String.Empty;
		public abstract void Parse(string[] splits);
		public virtual void Execute() => Console.WriteLine(this.Line);

		public Base() { }
	}

	public class Command : Base
	{
		public string Collection { get; set; }

		public override void Parse(string[] splits)
		{
			this.Collection = splits[1];
		}
	}

	public class Combobox : Base, ISelectable
	{
		private string _description = String.Empty;

		public int Choice { get; set; }
		public List<string> Options { get; }
		public string Description => this._description;
		public Combobox() => this.Options = new List<string>();
		public override void Parse(string[] splits)
		{
			for (int i = 1; i < splits.Length - 1; ++i) this.Options.Add(splits[i]);
			this._description = splits[^1];
		}
		public override void Execute()
		{
			Console.WriteLine(this.Line);
			Console.WriteLine($"Option chosen: {this.Choice}");
		}
		public int ParseOption(string option)
		{
			for (int i = 0; i < this.Options.Count; ++i) { if (this.Options[i] == option) return i; }
			return -1;
		}
		public bool Contains(string option) => this.Options.Contains(option);
	}

	public class Checkbox : Base, ISelectable
	{
		private string _description = String.Empty;

		public int Choice { get; set; }
		public List<string> Options { get; }
		public string Description => this._description;
		public Checkbox() => this.Options = new List<string>() { "disabled", "enabled" };
		public override void Parse(string[] splits) => this._description = splits[1];
		public override void Execute()
		{
			Console.WriteLine(this.Line);
			Console.WriteLine($"Option chosen: {this.Choice}");
		}
		public int ParseOption(string option)
		{
			return option switch
			{
				"disabled" => 0,
				"enabled" => 1,
				_ => -1,
			};
		}
		public bool Contains(string option) => this.Options.Contains(option);
	}

	public class Generic : Base
	{
		public string Type { get; set; }
		private bool _is_valid = false;

		public override void Parse(string[] splits)
		{
			this.Type = splits[0];
			if (splits.Length == 1) this._is_valid = true;
		}
		public override void Execute()
		{
			if (!this._is_valid) throw new Exception("Invalid operation");
		}
	}

	public class End : Base
	{
		public string Type => "end";

		public override void Parse(string[] splits) { }
	}
}
