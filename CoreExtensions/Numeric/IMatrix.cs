namespace CoreExtensions.Numeric
{
	public interface IMatrix
	{
		int Columns { get; }
		int Rows { get; }
		IMatrix ZeroMatrix { get; }

		float this[int index] { get; set; }
		float this[int row, int column] { get; set; }

		IMatrix Transpose();
	}
}
