using System;



namespace CoreExtensions.Management
{
	/// <summary>
	/// Provides all major helper methods to force system thread.
	/// </summary>
	public static class ForcedX
	{
		/// <summary>
		/// Forces <see cref="GC"/> to collect unused data.
		/// </summary>
		public static void GCCollect()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}
	}
}
