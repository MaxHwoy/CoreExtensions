using System;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Matrix2x1 : IMatrix
	{
		public float Value11 { get; set; }
		public float Value21 { get; set; }

		public static Matrix2x1 Zero => new Matrix2x1(0f, 0f);
		public int Columns => 1;
		public int Entries => 2;
		public int Rows => 2;
		public IMatrix ZeroMatrix => Zero;

		public Matrix2x1(Matrix2x1 matrix)
		{
			this.Value11 = matrix.Value11;
			this.Value21 = matrix.Value21;
		}
		public Matrix2x1(float a11,
						 float a21)
		{
			this.Value11 = a11;
			this.Value21 = a21;
		}

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return this.Value11;
					case 1: return this.Value21;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 1");
				}
			}
			set
			{
				switch (index)
				{
					case 0: this.Value11 = value; return;
					case 1: this.Value21 = value; return;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 1");
				}
			}
		}
		public float this[int row, int column]
		{
			get
			{
				if (column == 1)
				{

					switch (row)
					{
						case 1: return this.Value11;
						case 2: return this.Value21;
						default: throw new ArgumentOutOfRangeException(nameof(row), "Row index should be in range 1 to 2");
					}

				}
				throw new ArgumentOutOfRangeException(nameof(column), "Column index should be 1");
			}
			set
			{
				if (column == 1)
				{

					switch (row)
					{
						case 1: this.Value11 = value; return;
						case 2: this.Value21 = value; return;
						default: throw new ArgumentOutOfRangeException(nameof(row), "Row index should be in range 1 to 2");
					}

				}
				throw new ArgumentOutOfRangeException(nameof(column), "Column index should be 1");
			}
		}

		public Matrix2x1 Clone() => new Matrix2x1(this);
		public override bool Equals(object obj) => obj is Matrix2x1 matrix && this == matrix;
		public bool Equals(Matrix2x1 matrix) => this == matrix;
		public override int GetHashCode() => HashCode.Combine(this.Value11, this.Value21);
		public Matrix1x2 Transpose() => new Matrix1x2(this.Value11, this.Value21);
		IMatrix IMatrix.Transpose() => this.Transpose();
		public override string ToString() => this.ToString(null);
		public string ToString(string format) => Matrix.ToString(this, format);

		public static bool operator ==(Matrix2x1 a, Matrix2x1 b)
		{
			bool result = true;
			result &= a.Value11 == b.Value11;
			result &= a.Value21 == b.Value21;
			return result;
		}
		public static bool operator !=(Matrix2x1 a, Matrix2x1 b) => !(a == b);
		public static Matrix2x1 operator +(Matrix2x1 a, Matrix2x1 b)
		{
			return new Matrix2x1(a.Value11 + b.Value11,
								 a.Value21 + b.Value21);
		}
		public static Matrix2x1 operator -(Matrix2x1 a, Matrix2x1 b)
		{
			return new Matrix2x1(a.Value11 - b.Value11,
								 a.Value21 - b.Value21);
		}
		public static Matrix2x1 operator *(Matrix2x1 m, float scalar)
		{
			return new Matrix2x1(m.Value11 * scalar,
								 m.Value21 * scalar);
		}
		public static Matrix2x1 operator *(Matrix2x1 m, int scalar)
		{
			return new Matrix2x1(m.Value11 * scalar,
								 m.Value21 * scalar);
		}
	}
}
