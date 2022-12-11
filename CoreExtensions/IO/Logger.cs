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
		private StreamWriter? m_writer;

		/// <summary>
		/// Initializes new instance of <see cref="Logger"/> that writes to a file specified.
		/// </summary>
		/// <param name="file">File to write to.</param>
		public Logger(string file)
		{
			this.m_writer = File.Exists(file)
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
		public Logger(string file, string? intro, bool writedate)
		{
			string space = String.Empty;

			for (int loop = 0; loop < intro?.Length; ++loop)
            {
				space += "-";
			}

			if (File.Exists(file))
			{
				this.m_writer = new StreamWriter(File.Open(file, FileMode.Append, FileAccess.Write, FileShare.ReadWrite));

				this.m_writer.WriteLine();
				this.m_writer.WriteLine();
				this.m_writer.WriteLine();
				this.m_writer.WriteLine(intro);

				if (writedate)
                {
					this.m_writer.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
				}

				this.m_writer.WriteLine(space);
			}
			else
			{

				this.m_writer = new StreamWriter(File.Open(file, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));

				this.m_writer.WriteLine(intro);

				if (writedate)
                {
					this.m_writer.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
				}

				this.m_writer.WriteLine(space);

			}
		}

		/// <summary>
		/// Finalizer for <see cref="Logger"/>. Called by <see cref="GC"/> in case <see cref="Dispose"/> was not called.
		/// </summary>
		~Logger()
        {
			this.InternalDispose();
        }

		/// <summary>
		/// Writes a string to a file.
		/// </summary>
		/// <param name="value">String value to write.</param>
		public void Write(string? value)
		{
			if (this.m_writer is not null)
            {
				try
				{
					this.m_writer.Write(value);
				}
				catch
				{
				}
			}
		}
	
		/// <summary>
		/// Writes <see cref="Exception"/> details to a file.
		/// </summary>
		/// <param name="exception"><see cref="Exception"/> to write.</param>
		public void WriteException(Exception exception)
		{
			if (this.m_writer is not null)
            {
				try
				{
					this.m_writer.WriteLine($"Exception: {exception.GetLowestMessage()}");
					this.m_writer.WriteLine($"HResult: {exception.GetLowestHResult()}");
					this.m_writer.WriteLine($"StackTrace: {exception.GetLowestStackTrace()}");
					this.m_writer.WriteLine($"TargetSite: {exception.TargetSite}");
					this.m_writer.WriteLine($"Source: {exception.Source}");
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// Writes <see cref="Exception"/> details and <see cref="Stream"/> position to a file.
		/// </summary>
		/// <param name="exception"><see cref="Exception"/> to write.</param>
		/// <param name="stream"><see cref="Stream"/> which position should be written.</param>
		public void WriteException(Exception exception, Stream stream)
		{
			if (this.m_writer is not null)
            {
				try
				{
					this.WriteException(exception);
					this.m_writer.WriteLine($"Stream position: {stream.Position}");
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// Writes <see cref="Exception"/> details and <see cref="Delegate"/> info to a file.
		/// </summary>
		/// <param name="exception"><see cref="Exception"/> to write.</param>
		/// <param name="delegate"><see cref="Delegate"/> which information should be written.</param>
		public void WriteException(Exception exception, Delegate @delegate)
		{
			if (this.m_writer is not null)
            {
				try
				{
					this.WriteException(exception);
					this.m_writer.WriteLine($"Target: {@delegate.Target}");
					this.m_writer.WriteLine($"Method: {@delegate.Method}");
				}
				catch
				{
				}
			}
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
			if (this.m_writer is not null)
            {
				try
				{
					this.WriteException(exception, @delegate);
					this.m_writer.WriteLine($"Stream position: {stream.Position}");
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// Writes string value to a file and appends a newline to the end.
		/// </summary>
		/// <param name="value">String value to write.</param>
		public void WriteLine(string? value)
		{
			if (this.m_writer is not null)
            {
				try
				{
					this.m_writer.WriteLine(value);
				}
				catch
				{
				}
			}
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
			if (this.m_writer is not null)
            {
				this.m_writer.Flush();
				this.m_writer.Dispose();
				this.m_writer = null;
			}
		}
	}
}
