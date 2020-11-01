namespace CoreExtensions.Numeric
{
	/// <summary>
	/// Defines a rectangular array of numbers arranged in rows and columns.
	/// </summary>
	public interface IMatrix
	{
		/// <summary>
		/// Number of columns in this <see cref="IMatrix"/>.
		/// </summary>
		int Columns { get; }

		/// <summary>
		/// Number of entries in this <see cref="IMatrix"/>. Equals <see cref="Rows"/> times <see cref="Columns"/>.
		/// </summary>
		int Entries { get; }

		/// <summary>
		/// Number of rows in this <see cref="IMatrix"/>.
		/// </summary>
		int Rows { get; }

		/// <summary>
		/// <see cref="IMatrix"/> of the same type with all zero entries.
		/// </summary>
		IMatrix ZeroMatrix { get; }

		/// <summary>
		/// Gets or sets value of an entry with index specified. Index is calculated with Row * Column - 1.
		/// </summary>
		/// <param name="index">Index of an entry to get or set.</param>
		float this[int index] { get; set; }

		/// <summary>
		/// Gets or sets value of an entry with row and column specified.
		/// </summary>
		/// <param name="row">Index of the row of an entry. Ranges 1 to <see cref="Rows"/> count.</param>
		/// <param name="column">Index of the column of an entry. Ranges 1 to <see cref="Columns"/> count.</param>
		float this[int row, int column] { get; set; }

		/// <summary>
		/// Gets transpose of this <see cref="IMatrix"/>. Transpose of a matrix is a matrix with rows and columns
		/// switched.
		/// </summary>
		/// <example>
		/// [a b c]T   [a d g]
		/// [d e f]  = [b e h]
		/// [g h i]    [c f i]
		/// </example>
		/// <returns></returns>
		IMatrix Transpose();
	}
}
