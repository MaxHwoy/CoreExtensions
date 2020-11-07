using System;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Matrix3x2 : IMatrix, IEquatable<Matrix3x2>
	{
		public float Value11 { get; set; }
		public float Value12 { get; set; }
		public float Value21 { get; set; }
		public float Value22 { get; set; }
		public float Value31 { get; set; }
		public float Value32 { get; set; }

		public static Matrix3x2 Zero => new Matrix3x2(0f, 0f, 0f, 0f, 0f, 0f);
		public int Columns => 2;
		public int Entries => 6;
		public int Rows => 3;
		public IMatrix ZeroMatrix => Zero;

		public Matrix3x2(Matrix3x2 matrix)
		{
			this.Value11 = matrix.Value11;
			this.Value12 = matrix.Value12;
			this.Value21 = matrix.Value21;
			this.Value22 = matrix.Value22;
			this.Value31 = matrix.Value31;
			this.Value32 = matrix.Value32;
		}
		public Matrix3x2(float a11, float a12,
						 float a21, float a22,
						 float a31, float a32)
		{
			this.Value11 = a11;
			this.Value12 = a12;
			this.Value21 = a21;
			this.Value22 = a22;
			this.Value31 = a31;
			this.Value32 = a32;
		}

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return this.Value11;
					case 1: return this.Value12;
					case 2: return this.Value21;
					case 3: return this.Value22;
					case 4: return this.Value31;
					case 5: return this.Value32;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 5");
				}
			}
			set
			{
				switch (index)
				{
					case 0: this.Value11 = value; return;
					case 1: this.Value12 = value; return;
					case 2: this.Value21 = value; return;
					case 3: this.Value22 = value; return;
					case 4: this.Value31 = value; return;
					case 5: this.Value32 = value; return;
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
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 2");
						}

					case 2:
						switch (column)
						{
							case 1: return this.Value21;
							case 2: return this.Value22;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 2");
						}

					case 3:
						switch (column)
						{
							case 1: return this.Value31;
							case 2: return this.Value32;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 2");
						}

					default:
						throw new ArgumentOutOfRangeException(nameof(row), "Row index should be in range 1 to 3");
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
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 2");
						}

					case 2:
						switch (column)
						{
							case 1: this.Value21 = value; return;
							case 2: this.Value22 = value; return;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 2");
						}

					case 3:
						switch (column)
						{
							case 1: this.Value31 = value; return;
							case 2: this.Value32 = value; return;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 2");
						}

					default:
						throw new ArgumentOutOfRangeException(nameof(row), "Row index should be in range 1 to 3");
				}
			}
		}

		public Matrix3x2 Clone() => new Matrix3x2(this);
		public override bool Equals(object obj) => obj is Matrix3x2 matrix && this == matrix;
		public bool Equals(Matrix3x2 matrix) => this == matrix;
		public override int GetHashCode()
		{
			int result = HashCode.Combine(this.Value11, this.Value12);
			result = HashCode.Combine(result, this.Value21, this.Value22);
			return HashCode.Combine(result, this.Value31, this.Value32);
		}
		public Matrix2x3 Transpose()
		{
			return new Matrix2x3(this.Value11, this.Value21, this.Value31,
								 this.Value12, this.Value22, this.Value32);
		}
		IMatrix IMatrix.Transpose() => this.Transpose();
		public override string ToString() => this.ToString(null);
		public string ToString(string format) => Matrix.ToString(this, format);

		public static bool operator ==(Matrix3x2 a, Matrix3x2 b)
		{
			bool result = true;
			result &= a.Value11 == b.Value11;
			result &= a.Value12 == b.Value12;
			result &= a.Value21 == b.Value21;
			if (!result) return false;
			result &= a.Value22 == b.Value22;
			result &= a.Value31 == b.Value31;
			result &= a.Value32 == b.Value32;
			return result;
		}
		public static bool operator !=(Matrix3x2 a, Matrix3x2 b) => !(a == b);
		public static Matrix3x2 operator +(Matrix3x2 a, Matrix3x2 b)
		{
			return new Matrix3x2(a.Value11 + b.Value11, a.Value12 + b.Value12,
								 a.Value21 + b.Value21, a.Value22 + b.Value22,
								 a.Value31 + b.Value31, a.Value32 + b.Value32);
		}
		public static Matrix3x2 operator -(Matrix3x2 a, Matrix3x2 b)
		{
			return new Matrix3x2(a.Value11 - b.Value11, a.Value12 - b.Value12,
								 a.Value21 - b.Value21, a.Value22 - b.Value22,
								 a.Value31 - b.Value31, a.Value32 - b.Value32);
		}
		public static Matrix3x2 operator *(Matrix3x2 m, float scalar)
		{
			return new Matrix3x2(m.Value11 * scalar, m.Value12 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar,
								 m.Value31 * scalar, m.Value32 * scalar);
		}
		public static Matrix3x2 operator *(Matrix3x2 m, int scalar)
		{
			return new Matrix3x2(m.Value11 * scalar, m.Value12 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar,
								 m.Value31 * scalar, m.Value32 * scalar);
		}
		public static Matrix3x2 operator *(float scalar, Matrix3x2 m) => m * scalar;
		public static Matrix3x2 operator *(int scalar, Matrix3x2 m) => m * scalar;
	}
}
