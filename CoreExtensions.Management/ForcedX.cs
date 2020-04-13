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
		/// <param name="blocking">true to perform a blocking garbage collection; false to perform a background
		/// garbage collection where possible.</param>
		/// <param name="compacting">true to compact the small object heap; false to sweep only.</param>
		public static void GCCollect(bool blocking = true, bool compacting = false)
		{
			for (int a1 = 0; a1 < GC.MaxGeneration; ++a1)
				GC.Collect(a1, GCCollectionMode.Forced, blocking, compacting);
		}
	}
}
