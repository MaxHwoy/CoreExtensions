using System;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Matrix1x3 : IMatrix, IEquatable<Matrix1x3>
	{
		public float Value11 { get; set; }
		public float Value12 { get; set; }
		public float Value13 { get; set; }

		public static Matrix1x3 Zero => new Matrix1x3(0f, 0f, 0f);
		public int Columns => 3;
		public int Entries => 3;
		public int Rows => 1;
		public IMatrix ZeroMatrix => Zero;

		public Matrix1x3(Matrix1x3 matrix)
		{
			this.Value11 = matrix.Value11;
			this.Value12 = matrix.Value12;
			this.Value13 = matrix.Value13;
		}
		public Matrix1x3(float a11, float a12, float a13)
		{
			this.Value11 = a11;
			this.Value12 = a12;
			this.Value13 = a13;
		}

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return this.Value11;
					case 1: return this.Value12;
					case 2: return this.Value13;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 2");
				}
			}
			set
			{
				switch (index)
				{
					case 0: this.Value11 = value; return;
					case 1: this.Value12 = value; return;
					case 2: this.Value13 = value; return;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 2");
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
						case 3: return this.Value13;
						default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 3");
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
						case 3: this.Value13 = value; return;
						default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 3");
					}

				}
				throw new ArgumentOutOfRangeException(nameof(row), "Row index should be 1");
			}
		}

		public Matrix1x3 Clone() => new Matrix1x3(this);
		public override bool Equals(object obj) => obj is Matrix1x3 matrix && this == matrix;
		public bool Equals(Matrix1x3 matrix) => this == matrix;
		public override int GetHashCode() => HashCode.Combine(this.Value11, this.Value12, this.Value13);
		public Matrix3x1 Transpose() => new Matrix3x1(this.Value11, this.Value12, this.Value13);
		IMatrix IMatrix.Transpose() => this.Transpose();
		public override string ToString() => this.ToString(null);
		public string ToString(string format) => Matrix.ToString(this, format);

		public static bool operator ==(Matrix1x3 a, Matrix1x3 b)
		{
			bool result = true;
			result &= a.Value11 == b.Value11;
			result &= a.Value12 == b.Value12;
			result &= a.Value13 == b.Value13;
			return result;
		}
		public static bool operator !=(Matrix1x3 a, Matrix1x3 b) => !(a == b);
		public static Matrix1x3 operator +(Matrix1x3 a, Matrix1x3 b)
		{
			return new Matrix1x3(a.Value11 + b.Value11, a.Value12 + b.Value12, a.Value13 + b.Value13);
		}
		public static Matrix1x3 operator -(Matrix1x3 a, Matrix1x3 b)
		{
			return new Matrix1x3(a.Value11 - b.Value11, a.Value12 - b.Value12, a.Value13 - b.Value13);
		}
		public static Matrix1x3 operator *(Matrix1x3 m, float scalar)
		{
			return new Matrix1x3(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar);
		}
		public static Matrix1x3 operator *(Matrix1x3 m, int scalar)
		{
			return new Matrix1x3(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar);
		}
		public static Matrix1x3 operator *(float scalar, Matrix1x3 m) => m * scalar;
		public static Matrix1x3 operator *(int scalar, Matrix1x3 m) => m * scalar;
	}
}
