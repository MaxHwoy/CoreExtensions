using System;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Matrix3x4 : IMatrix, IEquatable<Matrix3x4>
	{
		public float Value11 { get; set; }
		public float Value12 { get; set; }
		public float Value13 { get; set; }
		public float Value14 { get; set; }
		public float Value21 { get; set; }
		public float Value22 { get; set; }
		public float Value23 { get; set; }
		public float Value24 { get; set; }
		public float Value31 { get; set; }
		public float Value32 { get; set; }
		public float Value33 { get; set; }
		public float Value34 { get; set; }

		public static Matrix3x4 Zero => new Matrix3x4(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
		public int Columns => 4;
		public int Entries => 12;
		public int Rows => 3;
		public IMatrix ZeroMatrix => Zero;

		public Matrix3x4(Matrix3x4 matrix)
		{
			this.Value11 = matrix.Value11;
			this.Value12 = matrix.Value12;
			this.Value13 = matrix.Value13;
			this.Value14 = matrix.Value14;
			this.Value21 = matrix.Value21;
			this.Value22 = matrix.Value22;
			this.Value23 = matrix.Value23;
			this.Value24 = matrix.Value24;
			this.Value31 = matrix.Value31;
			this.Value32 = matrix.Value32;
			this.Value33 = matrix.Value33;
			this.Value34 = matrix.Value34;
		}
		public Matrix3x4(float a11, float a12, float a13, float a14,
						 float a21, float a22, float a23, float a24,
						 float a31, float a32, float a33, float a34)
		{
			this.Value11 = a11;
			this.Value12 = a12;
			this.Value13 = a13;
			this.Value14 = a14;
			this.Value21 = a21;
			this.Value22 = a22;
			this.Value23 = a23;
			this.Value24 = a24;
			this.Value31 = a31;
			this.Value32 = a32;
			this.Value33 = a33;
			this.Value34 = a34;
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
					case 4: return this.Value21;
					case 5: return this.Value22;
					case 6: return this.Value23;
					case 7: return this.Value24;
					case 8: return this.Value31;
					case 9: return this.Value32;
					case 10: return this.Value33;
					case 11: return this.Value34;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 11");
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
					case 4: this.Value21 = value; return;
					case 5: this.Value22 = value; return;
					case 6: this.Value23 = value; return;
					case 7: this.Value24 = value; return;
					case 8: this.Value31 = value; return;
					case 9: this.Value32 = value; return;
					case 10: this.Value33 = value; return;
					case 11: this.Value34 = value; return;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 11");
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
							case 4: return this.Value14;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 4");
						}

					case 2:
						switch (column)
						{
							case 1: return this.Value21;
							case 2: return this.Value22;
							case 3: return this.Value23;
							case 4: return this.Value24;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 4");
						}

					case 3:
						switch (column)
						{
							case 1: return this.Value31;
							case 2: return this.Value32;
							case 3: return this.Value33;
							case 4: return this.Value34;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 4");
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
							case 3: this.Value13 = value; return;
							case 4: this.Value14 = value; return;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 4");
						}

					case 2:
						switch (column)
						{
							case 1: this.Value21 = value; return;
							case 2: this.Value22 = value; return;
							case 3: this.Value23 = value; return;
							case 4: this.Value24 = value; return;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 4");
						}

					case 3:
						switch (column)
						{
							case 1: this.Value31 = value; return;
							case 2: this.Value32 = value; return;
							case 3: this.Value33 = value; return;
							case 4: this.Value34 = value; return;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 4");
						}

					default:
						throw new ArgumentOutOfRangeException(nameof(row), "Row index should be in range 1 to 3");
				}
			}
		}

		public Matrix3x4 Clone() => new Matrix3x4(this);
		public override bool Equals(object obj) => obj is Matrix3x4 matrix && this == matrix;
		public bool Equals(Matrix3x4 matrix) => this == matrix;
		public override int GetHashCode()
		{
			int result = HashCode.Combine(this.Value11, this.Value12, this.Value13, this.Value14);
			result = HashCode.Combine(result, this.Value21, this.Value22, this.Value23, this.Value24);
			return HashCode.Combine(result, this.Value31, this.Value32, this.Value33, this.Value34);
		}
		public Matrix4x3 Transpose()
		{
			return new Matrix4x3(this.Value11, this.Value21, this.Value31,
								 this.Value12, this.Value22, this.Value32,
								 this.Value13, this.Value23, this.Value33,
								 this.Value14, this.Value24, this.Value34);
		}
		IMatrix IMatrix.Transpose() => this.Transpose();
		public override string ToString() => this.ToString(null);
		public string ToString(string format) => Matrix.ToString(this, format);

		public static bool operator ==(Matrix3x4 a, Matrix3x4 b)
		{
			bool result = true;
			result &= a.Value11 == b.Value11;
			result &= a.Value12 == b.Value12;
			result &= a.Value13 == b.Value13;
			result &= a.Value14 == b.Value14;
			if (!result) return false;
			result &= a.Value21 == b.Value21;
			result &= a.Value22 == b.Value22;
			result &= a.Value23 == b.Value23;
			result &= a.Value24 == b.Value24;
			if (!result) return false;
			result &= a.Value31 == b.Value31;
			result &= a.Value32 == b.Value32;
			result &= a.Value33 == b.Value33;
			result &= a.Value34 == b.Value34;
			return result;
		}
		public static bool operator !=(Matrix3x4 a, Matrix3x4 b) => !(a == b);
		public static Matrix3x4 operator +(Matrix3x4 a, Matrix3x4 b)
		{
			return new Matrix3x4(a.Value11 + b.Value11, a.Value12 + b.Value12, a.Value13 + b.Value13, a.Value14 + b.Value14,
								 a.Value21 + b.Value21, a.Value22 + b.Value22, a.Value23 + b.Value23, a.Value24 + b.Value24,
								 a.Value31 + b.Value31, a.Value32 + b.Value32, a.Value33 + b.Value33, a.Value34 + b.Value34);
		}
		public static Matrix3x4 operator -(Matrix3x4 a, Matrix3x4 b)
		{
			return new Matrix3x4(a.Value11 - b.Value11, a.Value12 - b.Value12, a.Value13 - b.Value13, a.Value14 - b.Value14,
								 a.Value21 - b.Value21, a.Value22 - b.Value22, a.Value23 - b.Value23, a.Value24 - b.Value24,
								 a.Value31 - b.Value31, a.Value32 - b.Value32, a.Value33 - b.Value33, a.Value34 - b.Value34);
		}
		public static Matrix3x4 operator *(Matrix3x4 m, float scalar)
		{
			return new Matrix3x4(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar, m.Value14 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar, m.Value23 * scalar, m.Value24 * scalar,
								 m.Value31 * scalar, m.Value32 * scalar, m.Value33 * scalar, m.Value34 * scalar);
		}
		public static Matrix3x4 operator *(Matrix3x4 m, int scalar)
		{
			return new Matrix3x4(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar, m.Value14 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar, m.Value23 * scalar, m.Value24 * scalar,
								 m.Value31 * scalar, m.Value32 * scalar, m.Value33 * scalar, m.Value34 * scalar);
		}
		public static Matrix3x4 operator *(float scalar, Matrix3x4 m) => m * scalar;
		public static Matrix3x4 operator *(int scalar, Matrix3x4 m) => m * scalar;
	}
}
