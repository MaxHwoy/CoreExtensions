using System;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Matrix4x3 : IMatrix
	{
		public float Value11 { get; set; }
		public float Value12 { get; set; }
		public float Value13 { get; set; }
		public float Value21 { get; set; }
		public float Value22 { get; set; }
		public float Value23 { get; set; }
		public float Value31 { get; set; }
		public float Value32 { get; set; }
		public float Value33 { get; set; }
		public float Value41 { get; set; }
		public float Value42 { get; set; }
		public float Value43 { get; set; }

		public static Matrix4x3 Zero => new Matrix4x3(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
		public int Columns => 3;
		public int Entries => 12;
		public int Rows => 4;
		public IMatrix ZeroMatrix => Zero;

		public Matrix4x3(Matrix4x3 matrix)
		{
			this.Value11 = matrix.Value11;
			this.Value12 = matrix.Value12;
			this.Value13 = matrix.Value13;
			this.Value21 = matrix.Value21;
			this.Value22 = matrix.Value22;
			this.Value23 = matrix.Value23;
			this.Value31 = matrix.Value31;
			this.Value32 = matrix.Value32;
			this.Value33 = matrix.Value33;
			this.Value41 = matrix.Value41;
			this.Value42 = matrix.Value42;
			this.Value43 = matrix.Value43;
		}
		public Matrix4x3(float a11, float a12, float a13,
						 float a21, float a22, float a23,
						 float a31, float a32, float a33,
						 float a41, float a42, float a43)
		{
			this.Value11 = a11;
			this.Value12 = a12;
			this.Value13 = a13;
			this.Value21 = a21;
			this.Value22 = a22;
			this.Value23 = a23;
			this.Value31 = a31;
			this.Value32 = a32;
			this.Value33 = a33;
			this.Value41 = a41;
			this.Value42 = a42;
			this.Value43 = a43;
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
					case 6: return this.Value31;
					case 7: return this.Value32;
					case 8: return this.Value33;
					case 9: return this.Value41;
					case 10: return this.Value42;
					case 11: return this.Value43;
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
					case 3: this.Value21 = value; return;
					case 4: this.Value22 = value; return;
					case 5: this.Value23 = value; return;
					case 6: this.Value31 = value; return;
					case 7: this.Value32 = value; return;
					case 8: this.Value33 = value; return;
					case 9: this.Value41 = value; return;
					case 10: this.Value42 = value; return;
					case 11: this.Value43 = value; return;
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

					case 3:
						switch (column)
						{
							case 1: return this.Value31;
							case 2: return this.Value32;
							case 3: return this.Value33;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 3");
						}

					case 4:
						switch (column)
						{
							case 1: return this.Value41;
							case 2: return this.Value42;
							case 3: return this.Value43;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 3");
						}

					default:
						throw new ArgumentOutOfRangeException(nameof(row), "Row index should be in range 1 to 4");
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

					case 3:
						switch (column)
						{
							case 1: this.Value31 = value; return;
							case 2: this.Value32 = value; return;
							case 3: this.Value33 = value; return;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 3");
						}

					case 4:
						switch (column)
						{
							case 1: this.Value41 = value; return;
							case 2: this.Value42 = value; return;
							case 3: this.Value43 = value; return;
							default: throw new ArgumentOutOfRangeException(nameof(column), "Column index should be in range 1 to 3");
						}

					default:
						throw new ArgumentOutOfRangeException(nameof(row), "Row index should be in range 1 to 4");
				}
			}
		}

		public Matrix4x3 Clone() => new Matrix4x3(this);
		public override bool Equals(object obj) => obj is Matrix4x3 matrix && this == matrix;
		public bool Equals(Matrix4x3 matrix) => this == matrix;
		public override int GetHashCode()
		{
			int result = HashCode.Combine(this.Value11, this.Value12, this.Value13);
			result = HashCode.Combine(result, this.Value21, this.Value22, this.Value23);
			result = HashCode.Combine(result, this.Value31, this.Value32, this.Value33);
			return HashCode.Combine(result, this.Value41, this.Value42, this.Value43);
		}
		public Matrix3x4 Transpose()
		{
			return new Matrix3x4(this.Value11, this.Value21, this.Value31, this.Value41,
								 this.Value12, this.Value22, this.Value32, this.Value42,
								 this.Value13, this.Value23, this.Value33, this.Value43);
		}
		IMatrix IMatrix.Transpose() => this.Transpose();
		public override string ToString() => this.ToString(null);
		public string ToString(string format) => Matrix.ToString(this, format);

		public static bool operator ==(Matrix4x3 a, Matrix4x3 b)
		{
			bool result = true;
			result &= a.Value11 == b.Value11;
			result &= a.Value21 == b.Value21;
			result &= a.Value31 == b.Value31;
			result &= a.Value41 == b.Value41;
			if (!result) return false;
			result &= a.Value12 == b.Value12;
			result &= a.Value22 == b.Value22;
			result &= a.Value32 == b.Value32;
			result &= a.Value42 == b.Value42;
			if (!result) return false;
			result &= a.Value13 == b.Value13;
			result &= a.Value23 == b.Value23;
			result &= a.Value33 == b.Value33;
			result &= a.Value43 == b.Value43;
			return result;
		}
		public static bool operator !=(Matrix4x3 a, Matrix4x3 b) => !(a == b);
		public static Matrix4x3 operator +(Matrix4x3 a, Matrix4x3 b)
		{
			return new Matrix4x3(a.Value11 + b.Value11, a.Value12 + b.Value12, a.Value13 + b.Value13,
								 a.Value21 + b.Value21, a.Value22 + b.Value22, a.Value23 + b.Value23,
								 a.Value31 + b.Value31, a.Value32 + b.Value32, a.Value33 + b.Value33,
								 a.Value41 + b.Value41, a.Value42 + b.Value42, a.Value43 + b.Value43);
		}
		public static Matrix4x3 operator -(Matrix4x3 a, Matrix4x3 b)
		{
			return new Matrix4x3(a.Value11 - b.Value11, a.Value12 - b.Value12, a.Value13 - b.Value13,
								 a.Value21 - b.Value21, a.Value22 - b.Value22, a.Value23 - b.Value23,
								 a.Value31 - b.Value31, a.Value32 - b.Value32, a.Value33 - b.Value33,
								 a.Value41 - b.Value41, a.Value42 - b.Value42, a.Value43 - b.Value43);
		}
		public static Matrix4x3 operator *(Matrix4x3 m, float scalar)
		{
			return new Matrix4x3(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar, m.Value23 * scalar,
								 m.Value31 * scalar, m.Value32 * scalar, m.Value33 * scalar,
								 m.Value41 * scalar, m.Value42 * scalar, m.Value43 * scalar);
		}
		public static Matrix4x3 operator *(Matrix4x3 m, int scalar)
		{
			return new Matrix4x3(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar, m.Value23 * scalar,
								 m.Value31 * scalar, m.Value32 * scalar, m.Value33 * scalar,
								 m.Value41 * scalar, m.Value42 * scalar, m.Value43 * scalar);
		}
	}
}
