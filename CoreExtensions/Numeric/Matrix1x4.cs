using System;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Matrix1x4 : IMatrix, IEquatable<Matrix1x4>
	{
		public float Value11 { get; set; }
		public float Value12 { get; set; }
		public float Value13 { get; set; }
		public float Value14 { get; set; }

		public static Matrix1x4 Zero => new Matrix1x4(0f, 0f, 0f, 0f);
		public int Columns => 4;
		public int Entries => 4;
		public int Rows => 1;
		public IMatrix ZeroMatrix => Zero;

		public Matrix1x4(Matrix1x4 matrix)
		{
			this.Value11 = matrix.Value11;
			this.Value12 = matrix.Value12;
			this.Value13 = matrix.Value13;
			this.Value14 = matrix.Value14;
		}
		public Matrix1x4(float a11, float a12, float a13, float a14)
		{
			this.Value11 = a11;
			this.Value12 = a12;
			this.Value13 = a13;
			this.Value14 = a14;
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
					case 3: return this.Value14;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 3");
				}
			}
			set
			{
				switch (index)
				{
					case 0: this.Value11 = value; return;
					case 1: this.Value12 = value; return;
					case 2: this.Value13 = value; return;
					case 3: this.Value14 = value; return;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 3");
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
						case 4: return this.Value14;
						default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 4");
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
						case 4: this.Value14 = value; return;
						default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 4");
					}

				}
				throw new ArgumentOutOfRangeException(nameof(row), "Row index should be 1");
			}
		}

		public Matrix1x4 Clone() => new Matrix1x4(this);
		public override bool Equals(object obj) => obj is Matrix1x4 matrix && this == matrix;
		public bool Equals(Matrix1x4 matrix) => this == matrix;
		public override int GetHashCode() => HashCode.Combine(this.Value11, this.Value12, this.Value13, this.Value14);
		public Matrix4x1 Transpose() => new Matrix4x1(this.Value11, this.Value12, this.Value13, this.Value14);
		IMatrix IMatrix.Transpose() => this.Transpose();
		public override string ToString() => this.ToString(null);
		public string ToString(string format) => Matrix.ToString(this, format);

		public static bool operator ==(Matrix1x4 a, Matrix1x4 b)
		{
			bool result = true;
			result &= a.Value11 == b.Value11;
			result &= a.Value12 == b.Value12;
			result &= a.Value13 == b.Value13;
			result &= a.Value14 == b.Value14;
			return result;
		}
		public static bool operator !=(Matrix1x4 a, Matrix1x4 b) => !(a == b);
		public static Matrix1x4 operator +(Matrix1x4 a, Matrix1x4 b)
		{
			return new Matrix1x4(a.Value11 + b.Value11, a.Value12 + b.Value12, a.Value13 + b.Value13, a.Value14 + b.Value14);
		}
		public static Matrix1x4 operator -(Matrix1x4 a, Matrix1x4 b)
		{
			return new Matrix1x4(a.Value11 - b.Value11, a.Value12 - b.Value12, a.Value13 - b.Value13, a.Value14 - b.Value14);
		}
		public static Matrix1x4 operator *(Matrix1x4 m, float scalar)
		{
			return new Matrix1x4(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar, m.Value14 * scalar);
		}
		public static Matrix1x4 operator *(Matrix1x4 m, int scalar)
		{
			return new Matrix1x4(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar, m.Value14 * scalar);
		}
		public static Matrix1x4 operator *(float scalar, Matrix1x4 m) => m * scalar;
		public static Matrix1x4 operator *(int scalar, Matrix1x4 m) => m * scalar;
	}
}
