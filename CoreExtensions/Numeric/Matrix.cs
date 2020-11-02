using System;
using System.Text;



namespace CoreExtensions.Numeric
{
	public static class Matrix
	{
		internal static string ToString(IMatrix matrix, string format)
		{
			/*
			* Supported formats are:
			* null/String.Empty = regular matrix format
			* "-" = all entries are inlined and separated with -
			* " " = all entries are inlined and separated with whitespace
			*/

			var sb = new StringBuilder(matrix.Entries * 3);

			switch (format)
			{
				case null:
				case "":
					for (int i = 1; i <= matrix.Rows; ++i)
					{

						for (int k = 1; k <= matrix.Columns; ++k) sb.Append(matrix[i, k] + " ");
						sb.AppendLine();

					}
					return sb.ToString();

				case " ":
				case "-":
					for (int i = 0; i < matrix.Entries; ++i) { sb.Append(matrix[i]); sb.Append(format); }
					return sb.ToString();

				default:
					goto case null;

			}
		}

		private static IMatrix GetMatrixByDimensions(int rows, int columns)
		{
			switch (rows)
			{
				case 1:
					switch (columns)
					{
						case 1: return new Matrix1x1();
						case 2: return new Matrix1x2();
						case 3: return new Matrix1x3();
						case 4: return new Matrix1x4();
						default: throw new ArgumentOutOfRangeException(nameof(columns), "Column number should be in range 1 to 4");
					}

				case 2:
					switch (columns)
					{
						case 1: return new Matrix2x1();
						case 2: return new Matrix2x2();
						case 3: return new Matrix2x3();
						case 4: return new Matrix2x4();
						default: throw new ArgumentOutOfRangeException(nameof(columns), "Column number should be in range 1 to 4");
					}

				case 3:
					switch (columns)
					{
						case 1: return new Matrix3x1();
						case 2: return new Matrix3x2();
						case 3: return new Matrix3x3();
						case 4: return new Matrix3x4();
						default: throw new ArgumentOutOfRangeException(nameof(columns), "Column number should be in range 1 to 4");
					}

				case 4:
					switch (columns)
					{
						case 1: return new Matrix4x1();
						case 2: return new Matrix4x2();
						case 3: return new Matrix4x3();
						case 4: return new Matrix4x4();
						default: throw new ArgumentOutOfRangeException(nameof(columns), "Column number should be in range 1 to 4");
					}

				default:
					throw new ArgumentOutOfRangeException(nameof(rows), "Row number should be in range 1 to 4");
			}
		}

		public static IMatrix RawMultiply(IMatrix a, IMatrix b)
		{
			if (a.Columns != b.Rows)
			{

				throw new Exception("Number of columns in the first matrix does not equal number of rows in the second matrix");

			}

			var matrix = GetMatrixByDimensions(a.Rows, b.Columns);

			for (int i = 1; i <= a.Rows; ++i)
			{

				for (int k = 1; k <= b.Columns; ++k)
				{

					for (int p = 1; p <= a.Columns; ++p)
					{

						matrix[i, k] += a[i, p] * b[p, k];

					}

				}

			}

			return matrix;
		}

		public static Matrix1x1 Multiply(Matrix1x1 a, Matrix1x1 b) => (Matrix1x1)RawMultiply(a, b);

		public static Matrix1x2 Multiply(Matrix1x1 a, Matrix1x2 b) => (Matrix1x2)RawMultiply(a, b);

		public static Matrix1x3 Multiply(Matrix1x1 a, Matrix1x3 b) => (Matrix1x3)RawMultiply(a, b);

		public static Matrix1x4 Multiply(Matrix1x1 a, Matrix1x4 b) => (Matrix1x4)RawMultiply(a, b);

		public static Matrix1x1 Multiply(Matrix1x2 a, Matrix2x1 b) => (Matrix1x1)RawMultiply(a, b);

		public static Matrix1x2 Multiply(Matrix1x2 a, Matrix2x2 b) => (Matrix1x2)RawMultiply(a, b);

		public static Matrix1x3 Multiply(Matrix1x2 a, Matrix2x3 b) => (Matrix1x3)RawMultiply(a, b);

		public static Matrix1x4 Multiply(Matrix1x2 a, Matrix2x4 b) => (Matrix1x4)RawMultiply(a, b);

		public static Matrix1x1 Multiply(Matrix1x3 a, Matrix3x1 b) => (Matrix1x1)RawMultiply(a, b);

		public static Matrix1x2 Multiply(Matrix1x3 a, Matrix3x2 b) => (Matrix1x2)RawMultiply(a, b);

		public static Matrix1x3 Multiply(Matrix1x3 a, Matrix3x3 b) => (Matrix1x3)RawMultiply(a, b);

		public static Matrix1x4 Multiply(Matrix1x3 a, Matrix3x4 b) => (Matrix1x4)RawMultiply(a, b);

		public static Matrix1x1 Multiply(Matrix1x4 a, Matrix4x1 b) => (Matrix1x1)RawMultiply(a, b);

		public static Matrix1x2 Multiply(Matrix1x4 a, Matrix4x2 b) => (Matrix1x2)RawMultiply(a, b);

		public static Matrix1x3 Multiply(Matrix1x4 a, Matrix4x3 b) => (Matrix1x3)RawMultiply(a, b);

		public static Matrix1x4 Multiply(Matrix1x4 a, Matrix4x4 b) => (Matrix1x4)RawMultiply(a, b);

		public static Matrix2x1 Multiply(Matrix2x1 a, Matrix1x1 b) => (Matrix2x1)RawMultiply(a, b);

		public static Matrix2x2 Multiply(Matrix2x1 a, Matrix1x2 b) => (Matrix2x2)RawMultiply(a, b);

		public static Matrix2x3 Multiply(Matrix2x1 a, Matrix1x3 b) => (Matrix2x3)RawMultiply(a, b);

		public static Matrix2x4 Multiply(Matrix2x1 a, Matrix1x4 b) => (Matrix2x4)RawMultiply(a, b);

		public static Matrix2x1 Multiply(Matrix2x2 a, Matrix2x1 b) => (Matrix2x1)RawMultiply(a, b);

		public static Matrix2x2 Multiply(Matrix2x2 a, Matrix2x2 b) => (Matrix2x2)RawMultiply(a, b);

		public static Matrix2x3 Multiply(Matrix2x2 a, Matrix2x3 b) => (Matrix2x3)RawMultiply(a, b);

		public static Matrix2x4 Multiply(Matrix2x2 a, Matrix2x4 b) => (Matrix2x4)RawMultiply(a, b);

		public static Matrix2x1 Multiply(Matrix2x3 a, Matrix3x1 b) => (Matrix2x1)RawMultiply(a, b);

		public static Matrix2x2 Multiply(Matrix2x3 a, Matrix3x2 b) => (Matrix2x2)RawMultiply(a, b);

		public static Matrix2x3 Multiply(Matrix2x3 a, Matrix3x3 b) => (Matrix2x3)RawMultiply(a, b);

		public static Matrix2x4 Multiply(Matrix2x3 a, Matrix3x4 b) => (Matrix2x4)RawMultiply(a, b);

		public static Matrix2x1 Multiply(Matrix2x4 a, Matrix4x1 b) => (Matrix2x1)RawMultiply(a, b);

		public static Matrix2x2 Multiply(Matrix2x4 a, Matrix4x2 b) => (Matrix2x2)RawMultiply(a, b);

		public static Matrix2x3 Multiply(Matrix2x4 a, Matrix4x3 b) => (Matrix2x3)RawMultiply(a, b);

		public static Matrix2x4 Multiply(Matrix2x4 a, Matrix4x4 b) => (Matrix2x4)RawMultiply(a, b);

		public static Matrix3x1 Multiply(Matrix3x1 a, Matrix1x1 b) => (Matrix3x1)RawMultiply(a, b);

		public static Matrix3x2 Multiply(Matrix3x1 a, Matrix1x2 b) => (Matrix3x2)RawMultiply(a, b);

		public static Matrix3x3 Multiply(Matrix3x1 a, Matrix1x3 b) => (Matrix3x3)RawMultiply(a, b);

		public static Matrix3x4 Multiply(Matrix3x1 a, Matrix1x4 b) => (Matrix3x4)RawMultiply(a, b);

		public static Matrix3x1 Multiply(Matrix3x2 a, Matrix2x1 b) => (Matrix3x1)RawMultiply(a, b);

		public static Matrix3x2 Multiply(Matrix3x2 a, Matrix2x2 b) => (Matrix3x2)RawMultiply(a, b);

		public static Matrix3x3 Multiply(Matrix3x2 a, Matrix2x3 b) => (Matrix3x3)RawMultiply(a, b);

		public static Matrix3x4 Multiply(Matrix3x2 a, Matrix2x4 b) => (Matrix3x4)RawMultiply(a, b);

		public static Matrix3x1 Multiply(Matrix3x3 a, Matrix3x1 b) => (Matrix3x1)RawMultiply(a, b);

		public static Matrix3x2 Multiply(Matrix3x3 a, Matrix3x2 b) => (Matrix3x2)RawMultiply(a, b);

		public static Matrix3x3 Multiply(Matrix3x3 a, Matrix3x3 b) => (Matrix3x3)RawMultiply(a, b);

		public static Matrix3x4 Multiply(Matrix3x3 a, Matrix3x4 b) => (Matrix3x4)RawMultiply(a, b);

		public static Matrix3x1 Multiply(Matrix3x4 a, Matrix4x1 b) => (Matrix3x1)RawMultiply(a, b);

		public static Matrix3x2 Multiply(Matrix3x4 a, Matrix4x2 b) => (Matrix3x2)RawMultiply(a, b);

		public static Matrix3x3 Multiply(Matrix3x4 a, Matrix4x3 b) => (Matrix3x3)RawMultiply(a, b);

		public static Matrix3x4 Multiply(Matrix3x4 a, Matrix4x4 b) => (Matrix3x4)RawMultiply(a, b);

		public static Matrix4x1 Multiply(Matrix4x1 a, Matrix1x1 b) => (Matrix4x1)RawMultiply(a, b);

		public static Matrix4x2 Multiply(Matrix4x1 a, Matrix1x2 b) => (Matrix4x2)RawMultiply(a, b);

		public static Matrix4x3 Multiply(Matrix4x1 a, Matrix1x3 b) => (Matrix4x3)RawMultiply(a, b);

		public static Matrix4x4 Multiply(Matrix4x1 a, Matrix1x4 b) => (Matrix4x4)RawMultiply(a, b);

		public static Matrix4x1 Multiply(Matrix4x2 a, Matrix2x1 b) => (Matrix4x1)RawMultiply(a, b);

		public static Matrix4x2 Multiply(Matrix4x2 a, Matrix2x2 b) => (Matrix4x2)RawMultiply(a, b);

		public static Matrix4x3 Multiply(Matrix4x2 a, Matrix2x3 b) => (Matrix4x3)RawMultiply(a, b);

		public static Matrix4x4 Multiply(Matrix4x2 a, Matrix2x4 b) => (Matrix4x4)RawMultiply(a, b);

		public static Matrix4x1 Multiply(Matrix4x3 a, Matrix3x1 b) => (Matrix4x1)RawMultiply(a, b);

		public static Matrix4x2 Multiply(Matrix4x3 a, Matrix3x2 b) => (Matrix4x2)RawMultiply(a, b);

		public static Matrix4x3 Multiply(Matrix4x3 a, Matrix3x3 b) => (Matrix4x3)RawMultiply(a, b);

		public static Matrix4x4 Multiply(Matrix4x3 a, Matrix3x4 b) => (Matrix4x4)RawMultiply(a, b);

		public static Matrix4x1 Multiply(Matrix4x4 a, Matrix4x1 b) => (Matrix4x1)RawMultiply(a, b);

		public static Matrix4x2 Multiply(Matrix4x4 a, Matrix4x2 b) => (Matrix4x2)RawMultiply(a, b);

		public static Matrix4x3 Multiply(Matrix4x4 a, Matrix4x3 b) => (Matrix4x3)RawMultiply(a, b);

		public static Matrix4x4 Multiply(Matrix4x4 a, Matrix4x4 b) => (Matrix4x4)RawMultiply(a, b);

		public static bool Solve(Matrix2x2 system, Matrix2x1 equality, out Matrix2x1 solution)
		{
			solution = Matrix2x1.Zero;
			if (!system.IsInvertible) return false;

			// Ax = b; x = A⁻¹b
			solution = Multiply(system.Invert(), equality);
			return true;
		}

		public static bool Solve(Matrix3x3 system, Matrix3x1 equality, out Matrix3x1 solution)
		{
			solution = Matrix3x1.Zero;
			if (!system.IsInvertible) return false;

			// Ax = b; x = A⁻¹b
			solution = Multiply(system.Invert(), equality);
			return true;
		}

		public static bool Solve(Matrix4x4 system, Matrix4x1 equality, out Matrix4x1 solution)
		{
			solution = Matrix4x1.Zero;
			if (!system.IsInvertible) return false;

			// Ax = b; x = A⁻¹b
			solution = Multiply(system.Invert(), equality);
			return true;
		}
	}
}
