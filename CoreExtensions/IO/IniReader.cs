using System;
using System.IO;
using System.Collections.Generic;
using CoreExtensions.Conversions;

namespace CoreExtensions.IO
{
	/// <summary>
	/// Represents a disposable .ini files reader.
	/// </summary>
	public class IniReader : IDisposable
	{
		private class Entry
		{
			public string? Category { get; set; }
			public Dictionary<string, Property> Properties { get; }

			public Entry()
            {
				this.Properties = new Dictionary<string, Property>();
			}

			public void AddProperty(Property property)
			{
				this.Properties[property.Name ?? String.Empty] = property;
			}
		}

		private struct Property
		{
			public string? Name { get; set; }
			public string? Value { get; set; }
		
			public Property(string? name, string? value)
			{
				this.Name = name;
				this.Value = value;
			}
		}

		private readonly Dictionary<string, Entry> m_entries;
		private string? m_last_category;
		private StreamReader? m_reader;

		/// <summary>
		/// Comment delimiter in the file.
		/// </summary>
		public string CommentDelimiter { get; set; } = "//";
		
		/// <summary>
		/// Array of characters that separate array elements.
		/// </summary>
		public string ArraySeparator { get; set; } = ",";
		
		/// <summary>
		/// Length of the stream.
		/// </summary>
		public long Length => this.m_reader?.BaseStream.Length ?? -1;
		
		/// <summary>
		/// Position in the stream.
		/// </summary>
		public long Position
		{
			get
            {
				return this.m_reader?.BaseStream.Position ?? -1;
			}
			set
			{
				if (this.m_reader is not null)
				{
					this.m_reader.BaseStream.Position = value;
				}
			}
		}

		/// <summary>
		/// Initializes new instance of <see cref="IniReader"/> using file path provided.
		/// </summary>
		/// <param name="path">Path of the file to read.</param>
		public IniReader(string path)
		{
			this.m_reader = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read));
			this.m_entries = new Dictionary<string, Entry>();
		}

		/// <summary>
		/// Initializes new instance of <see cref="IniReader"/> using <see cref="StreamReader"/> 
		/// provided.
		/// </summary>
		/// <param name="reader"><see cref="StreamReader"/> with opened file to read.</param>
		public IniReader(StreamReader reader)
		{
			this.m_reader = reader;
			this.m_entries = new Dictionary<string, Entry>();
		}

		/// <summary>
		/// Finalizer for <see cref="IniReader"/>. Called by <see cref="GC"/> in case <see cref="Dispose"/> was not called.
		/// </summary>
		~IniReader()
        {
			this.InternalDispose();
        }

		/// <summary>
		/// Reads a line from the stream.
		/// </summary>
		public void ReadLine()
		{
			var line = this.m_reader?.ReadLine() ?? String.Empty;

			if (line.Contains(this.CommentDelimiter))
			{
				line = line[..line.IndexOf(this.CommentDelimiter)].Trim();
			}

			if (IniReader.IsCategoryString(line))
			{
				line = line[1..^1];
				this.m_entries.Add(line, new Entry() { Category = line });
				this.m_last_category = line;
			}
			else if (this.m_entries.TryGetValue(this.m_last_category ?? String.Empty, out var entry))
			{
				var property = IniReader.GetProperty(line);
				entry.AddProperty(property);
			}
			else
			{
				throw new Exception("Property is not assigned to any category");
			}
		}

		/// <summary>
		/// Reads all lines from the stream.
		/// </summary>
		public void ReadAllLines()
		{
			if (this.m_reader is not null)
            {
				while (!this.m_reader.EndOfStream)
				{
					this.ReadLine();
				}
			}
		}

		/// <summary>
		/// Gets value of type specified from category and property provided.
		/// </summary>
		/// <typeparam name="T">Type of the value to return.</typeparam>
		/// <param name="category">Category in the file to search in.</param>
		/// <param name="property">Property to get value from.</param>
		/// <returns>Value of a type specified.</returns>
		public T? GetValue<T>(string category, string property)
		{
			if (!this.m_entries.TryGetValue(category, out var entry))
            {
				return default;
			}

			if (!entry.Properties.TryGetValue(property, out var field))
            {
				return default;
			}

			var value = field.Value;

			if (value is null)
            {
				return default;
            }

			return (T)value.ReinterpretCast(typeof(T));
		}

		/// <summary>
		/// Gets array of values of type specified from category and property provided.
		/// </summary>
		/// <typeparam name="T">Type of the values of the array to return.</typeparam>
		/// <param name="category">Category in the file to search in.</param>
		/// <param name="property">Property to get value from.</param>
		/// <returns>Array of values of a type specified.</returns>
		public T?[]? GetArray<T>(string category, string property)
		{
			if (!this.m_entries.TryGetValue(category, out var entry))
            {
				return null;
			}

			if (!entry.Properties.TryGetValue(property, out var field))
            {
				return null;
			}

			var value = field.Value;

			if (value is null)
            {
				return null;
            }

			var delim = this.ArraySeparator.ToCharArray();
			var units = value.Split(delim);

			var type = typeof(T);
			var array = new T[units.Length];

			for (int loop = 0; loop < units.Length; ++loop)
			{
				array[loop] = (T)units[loop].Trim().ReinterpretCast(type);
			}

			return array;
		}

		/// <summary>
		/// Releases all resources used by the current instance of the <see cref="IniReader"/>.
		/// </summary>
		public void Dispose()
        {
			this.InternalDispose();
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="IniReader"/>.
		/// </summary>
		protected void InternalDispose()
		{
			if (this.m_reader is not null)
            {
				this.m_reader.Dispose();

				this.m_reader = null;
            }
		}

		private static bool IsCategoryString(string line)
		{
			return line.StartsWith('[') && line.EndsWith(']');
		}

		private static Property GetProperty(string line)
		{
			var splits = line.Split("=", StringSplitOptions.RemoveEmptyEntries);

			if (splits.Length < 2)
			{
				var name = splits[0].Trim();
				return new Property(name, String.Empty);
			}
			else
			{
				var name = splits[0].Trim();
				var value = splits[1].Trim();
				return new Property(name, value);
			}
		}
	}
}
