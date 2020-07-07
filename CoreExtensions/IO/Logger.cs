using System;
using System.IO;
using CoreExtensions.Management;



namespace CoreExtensions.IO
{
	/// <summary>
	/// A <see cref="Stream"/> class to write logs and exception data.
	/// </summary>
	public class Logger : IDisposable
	{
		private StreamWriter _writer;

		/// <summary>
		/// Initializes new instance of <see cref="Logger"/> that writes to a file specified.
		/// </summary>
		/// <param name="file">File to write to.</param>
		public Logger(string file)
		{
			this._writer = File.Exists(file)
				? new StreamWriter(File.Open(file, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
				: new StreamWriter(File.Open(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite));
		}

		/// <summary>
		/// Initializes new instance of <see cref="Logger"/> that writes to a file specified. 
		/// When initialized, writes introductory string to a file.
		/// </summary>
		/// <param name="file">File to write to.</param>
		/// <param name="intro">Introductory string to write on file opening.</param>
		public Logger(string file, string intro) : this(file, intro, false) { }

		/// <summary>
		/// Initializes new instance of <see cref="Logger"/> that writes to a file specified. 
		/// When initialized, writes introductory string to a file with date and time.
		/// </summary>
		/// <param name="file">File to write to.</param>
		/// <param name="intro">Introductory string to write on file opening.</param>
		/// <param name="writedate">True if write date to a file; false otherwise.</param>
		public Logger(string file, string intro, bool writedate)
		{
			string space = String.Empty;
			for (int loop = 0; loop < intro.Length; ++loop) space += "-";

			if (File.Exists(file))
			{

				this._writer = new StreamWriter(File.Open(file, FileMode.Append, FileAccess.Write, FileShare.ReadWrite));
				this._writer.WriteLine();
				this._writer.WriteLine();
				this._writer.WriteLine();
				this._writer.WriteLine(intro);
				if (writedate) this._writer.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
				this._writer.WriteLine(space);

			}
			else
			{

				this._writer = new StreamWriter(File.Open(file, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
				this._writer.WriteLine(intro);
				if (writedate) this._writer.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
				this._writer.WriteLine(space);

			}
		}

		/// <summary>
		/// Writes a string to a file.
		/// </summary>
		/// <param name="value">String value to write.</param>
		public void Write(string value)
		{
			try { this._writer.Write(value); }
			catch { }
		}
	
		/// <summary>
		/// Writes <see cref="Exception"/> details to a file.
		/// </summary>
		/// <param name="exception"><see cref="Exception"/> to write.</param>
		public void WriteException(Exception exception)
		{
			try
			{

				this._writer.WriteLine($"Exception: {exception.GetLowestMessage()}");
				this._writer.WriteLine($"HResult: {exception.GetLowestHResult()}");
				this._writer.WriteLine($"StackTrace: {exception.GetLowestStackTrace()}");
				this._writer.WriteLine($"TargetSite: {exception.TargetSite}");
				this._writer.WriteLine($"Source: {exception.Source}");

			}
			catch { }
		}

		/// <summary>
		/// Writes <see cref="Exception"/> details and <see cref="Stream"/> position to a file.
		/// </summary>
		/// <param name="exception"><see cref="Exception"/> to write.</param>
		/// <param name="stream"><see cref="Stream"/> which position should be written.</param>
		public void WriteException(Exception exception, Stream stream)
		{
			try
			{

				this.WriteException(exception);
				this._writer.WriteLine($"Stream position: {stream.Position}");

			}
			catch { }
		}

		/// <summary>
		/// Writes <see cref="Exception"/> details and <see cref="Delegate"/> info to a file.
		/// </summary>
		/// <param name="exception"><see cref="Exception"/> to write.</param>
		/// <param name="delegate"><see cref="Delegate"/> which information should be written.</param>
		public void WriteException(Exception exception, Delegate @delegate)
		{
			try
			{

				this.WriteException(exception);
				this._writer.WriteLine($"Target: {@delegate.Target}");
				this._writer.WriteLine($"Method: {@delegate.Method}");

			}
			catch { }
		}

		/// <summary>
		/// Writes <see cref="Exception"/> details, <see cref="Delegate"/> info and 
		/// <see cref="Stream"/> position to a file.
		/// </summary>
		/// <param name="exception"><see cref="Exception"/> to write.</param>
		/// <param name="delegate"><see cref="Delegate"/> which information should be written.</param>
		/// <param name="stream"><see cref="Stream"/> which position should be written.</param>
		public void WriteException(Exception exception, Delegate @delegate, Stream stream)
		{
			try
			{

				this.WriteException(exception, @delegate);
				this._writer.WriteLine($"Stream position: {stream.Position}");

			}
			catch { }
		}

		/// <summary>
		/// Writes string value to a file and appends a newline to the end.
		/// </summary>
		/// <param name="value">String value to write.</param>
		public void WriteLine(string value)
		{
			try { this._writer.WriteLine(value); }
			catch { }
		}

		/// <summary>
		/// Releases all resources used by the current instance of the <see cref="IniReader"/>.
		/// </summary>
		public void Dispose() => this.Dispose(true);

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="IniReader"/>.
		/// </summary>
		/// <param name="disposing">True if release both managed and unmanaged resources; false 
		/// if release unmanaged only.</param>
		protected void Dispose(bool disposing)
		{
			if (disposing) this._writer.Close();

			this._writer.Dispose();
			this._writer = null;
		}
	}
}
