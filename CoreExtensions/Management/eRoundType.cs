namespace CoreExtensions.Management
{
	/// <summary>
	/// Specifies type of rounding values.
	/// </summary>
	public enum eRoundType : int
	{
		/// <summary>
		/// Specifies that value should be rounded to the nearest possible result.
		/// </summary>
		Nearest = 0,

		/// <summary>
		/// Specifies that value should be rounded up to the closest matching result.
		/// </summary>
		Up = 1,

		/// <summary>
		/// Specifies that value should be rounded down to the closest matching result.
		/// </summary>
		Down = 2,
	}
}
