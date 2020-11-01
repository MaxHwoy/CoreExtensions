using System;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Matrix1x2 : IMatrix
	{
		public float Value11 { get; set; }
		public float Value12 { get; set; }

		public static Matrix1x2 Zero => new Matrix1x2(0f, 0f);
		public int Columns => 2;
		public int Entries => 2;
		public int Rows => 1;
		public IMatrix ZeroMatrix => Zero;

		public Matrix1x2(Matrix1x2 matrix)
		{
			this.Value11 = matrix.Value11;
			this.Value12 = matrix.Value12;
		}
		public Matrix1x2(float a11, float a12)
		{
			this.Value11 = a11;
			this.Value12 = a12;
		}

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return this.Value11;
					case 1: return this.Value12;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 1");
				}
			}
			set
			{
				switch (index)
				{
					case 0: this.Value11 = value; return;
					case 1: this.Value12 = value; return;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 1");
				}
			}
		}
		public float this[int row, int column]
		{
			get
			{
				if (row == 1)
				{

					switch (column)
					{
						case 1: return this.Value11;
						case 2: return this.Value12;
						default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 2");
					}

				}
				throw new ArgumentOutOfRangeException(nameof(row), "Row index should be 1");
			}
			set
			{
				if (row == 1)
				{

					switch (column)
					{
						case 1: this.Value11 = value; return;
						case 2: this.Value12 = value; return;
						default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 2");
					}

				}
				throw new ArgumentOutOfRangeException(nameof(row), "Row index should be 1");
			}
		}

		public Matrix1x2 Clone() => new Matrix1x2(this);
		public override bool Equals(object obj) => obj is Matrix1x2 matrix && this == matrix;
		public bool Equals(Matrix1x2 matrix) => this == matrix;
		public override int GetHashCode() => HashCode.Combine(this.Value11, this.Value12);
		public Matrix2x1 Transpose() => new Matrix2x1(this.Value11, this.Value12);
		IMatrix IMatrix.Transpose() => this.Transpose();
		public override string ToString() => this.ToString(null);
		public string ToString(string format) => Matrix.ToString(this, format);

		public static bool operator ==(Matrix1x2 a, Matrix1x2 b)
		{
			bool result = true;
			result &= a.Value11 == b.Value11;
			result &= a.Value12 == b.Value12;
			return result;
		}
		public static bool operator !=(Matrix1x2 a, Matrix1x2 b) => !(a == b);
		public static Matrix1x2 operator +(Matrix1x2 a, Matrix1x2 b)
		{
			return new Matrix1x2(a.Value11 + b.Value11, a.Value12 + b.Value12);
		}
		public static Matrix1x2 operator -(Matrix1x2 a, Matrix1x2 b)
		{
			return new Matrix1x2(a.Value11 - b.Value11, a.Value12 - b.Value12);
		}
		public static Matrix1x2 operator *(Matrix1x2 m, float scalar)
		{
			return new Matrix1x2(m.Value11 * scalar, m.Value12 * scalar);
		}
		public static Matrix1x2 operator *(Matrix1x2 m, int scalar)
		{
			return new Matrix1x2(m.Value11 * scalar, m.Value12 * scalar);
		}
	}
}
