using System;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Matrix2x3 : IMatrix
	{
		public float Value11 { get; set; }
		public float Value12 { get; set; }
		public float Value13 { get; set; }
		public float Value21 { get; set; }
		public float Value22 { get; set; }
		public float Value23 { get; set; }

		public static Matrix2x3 Zero => new Matrix2x3(0f, 0f, 0f, 0f, 0f, 0f);
		public int Columns => 3;
		public int Entries => 6;
		public int Rows => 2;
		public IMatrix ZeroMatrix => Zero;

		public Matrix2x3(Matrix2x3 matrix)
		{
			this.Value11 = matrix.Value11;
			this.Value12 = matrix.Value12;
			this.Value13 = matrix.Value13;
			this.Value21 = matrix.Value21;
			this.Value22 = matrix.Value22;
			this.Value23 = matrix.Value23;
		}
		public Matrix2x3(float a11, float a12, float a13,
						 float a21, float a22, float a23)
		{
			this.Value11 = a11;
			this.Value12 = a12;
			this.Value13 = a13;
			this.Value21 = a21;
			this.Value22 = a22;
			this.Value23 = a23;
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
					case 3: return this.Value21;
					case 4: return this.Value22;
					case 5: return this.Value23;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 5");
				}
			}
			set
			{
				switch (index)
				{
					case 0: this.Value11 = value; return;
					case 1: this.Value12 = value; return;
					case 2: this.Value13 = value; return;
					case 3: this.Value21 = value; return;
					case 4: this.Value22 = value; return;
					case 5: this.Value23 = value; return;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 5");
				}
			}
		}
		public float this[int row, int column]
		{
			get
			{
				switch (row)
				{
					case 1:
						switch (column)
						{
							case 1: return this.Value11;
							case 2: return this.Value12;
							case 3: return this.Value13;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 3");
						}

					case 2:
						switch (column)
						{
							case 1: return this.Value21;
							case 2: return this.Value22;
							case 3: return this.Value23;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 3");
						}

					default:
						throw new ArgumentOutOfRangeException(nameof(row), "Row index should be in range 1 to 2");
				}
			}
			set
			{
				switch (row)
				{
					case 1:
						switch (column)
						{
							case 1: this.Value11 = value; return;
							case 2: this.Value12 = value; return;
							case 3: this.Value13 = value; return;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 3");
						}

					case 2:
						switch (column)
						{
							case 1: this.Value21 = value; return;
							case 2: this.Value22 = value; return;
							case 3: this.Value23 = value; return;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 3");
						}

					default:
						throw new ArgumentOutOfRangeException(nameof(row), "Row index should be in range 1 to 2");
				}
			}
		}

		public Matrix2x3 Clone() => new Matrix2x3(this);
		public override bool Equals(object obj) => obj is Matrix2x3 matrix && this == matrix;
		public bool Equals(Matrix2x3 matrix) => this == matrix;
		public override int GetHashCode()
		{
			int result = HashCode.Combine(this.Value11, this.Value12, this.Value13);
			return HashCode.Combine(result, this.Value21, this.Value22, this.Value23);
		}
		public Matrix3x2 Transpose()
		{
			return new Matrix3x2(this.Value11, this.Value21,
								 this.Value12, this.Value22,
								 this.Value13, this.Value23);
		}
		IMatrix IMatrix.Transpose() => this.Transpose();
		public override string ToString() => this.ToString(null);
		public string ToString(string format) => Matrix.ToString(this, format);

		public static bool operator ==(Matrix2x3 a, Matrix2x3 b)
		{
			bool result = true;
			result &= a.Value11 == b.Value11;
			result &= a.Value12 == b.Value12;
			result &= a.Value13 == b.Value13;
			if (!result) return false;
			result &= a.Value21 == b.Value21;
			result &= a.Value22 == b.Value22;
			result &= a.Value23 == b.Value23;
			return result;
		}
		public static bool operator !=(Matrix2x3 a, Matrix2x3 b) => !(a == b);
		public static Matrix2x3 operator +(Matrix2x3 a, Matrix2x3 b)
		{
			return new Matrix2x3(a.Value11 + b.Value11, a.Value12 + b.Value12, a.Value13 + b.Value13,
								 a.Value21 + b.Value21, a.Value22 + b.Value22, a.Value23 + b.Value23);
		}
		public static Matrix2x3 operator -(Matrix2x3 a, Matrix2x3 b)
		{
			return new Matrix2x3(a.Value11 - b.Value11, a.Value12 - b.Value12, a.Value13 - b.Value13,
								 a.Value21 - b.Value21, a.Value22 - b.Value22, a.Value23 - b.Value23);
		}
		public static Matrix2x3 operator *(Matrix2x3 m, float scalar)
		{
			return new Matrix2x3(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar, m.Value23 * scalar);
		}
		public static Matrix2x3 operator *(Matrix2x3 m, int scalar)
		{
			return new Matrix2x3(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar, m.Value23 * scalar);
		}
	}
}
