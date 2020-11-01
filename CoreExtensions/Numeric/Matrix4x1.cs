using System;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Matrix4x1 : IMatrix
	{
		public float Value11 { get; set; }
		public float Value21 { get; set; }
		public float Value31 { get; set; }
		public float Value41 { get; set; }

		public static Matrix4x1 Zero => new Matrix4x1(0f, 0f, 0f, 0f);
		public int Columns => 1;
		public int Entries => 4;
		public int Rows => 4;
		public IMatrix ZeroMatrix => Zero;

		public Matrix4x1(Matrix4x1 matrix)
		{
			this.Value11 = matrix.Value11;
			this.Value21 = matrix.Value21;
			this.Value31 = matrix.Value31;
			this.Value41 = matrix.Value41;
		}
		public Matrix4x1(float a11,
						 float a21,
						 float a31,
						 float a41)
		{
			this.Value11 = a11;
			this.Value21 = a21;
			this.Value31 = a31;
			this.Value41 = a41;
		}

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return this.Value11;
					case 1: return this.Value21;
					case 2: return this.Value31;
					case 3: return this.Value41;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 3");
				}
			}
			set
			{
				switch (index)
				{
					case 0: this.Value11 = value; return;
					case 1: this.Value21 = value; return;
					case 2: this.Value31 = value; return;
					case 3: this.Value41 = value; return;
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 3");
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
						case 3: return this.Value31;
						case 4: return this.Value41;
						default: throw new ArgumentOutOfRangeException(nameof(row), "Row index should be in range 1 to 4");
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
						case 3: this.Value31 = value; return;
						case 4: this.Value41 = value; return;
						default: throw new ArgumentOutOfRangeException(nameof(row), "Row index should be in range 1 to 4");
					}

				}
				throw new ArgumentOutOfRangeException(nameof(column), "Column index should be 1");
			}
		}

		public Matrix4x1 Clone() => new Matrix4x1(this);
		public override bool Equals(object obj) => obj is Matrix4x1 matrix && this == matrix;
		public bool Equals(Matrix4x1 matrix) => this == matrix;
		public override int GetHashCode() => HashCode.Combine(this.Value11, this.Value21, this.Value31, this.Value41);
		public Matrix1x4 Transpose() => new Matrix1x4(this.Value11, this.Value21, this.Value31, this.Value41);
		IMatrix IMatrix.Transpose() => this.Transpose();
		public override string ToString() => this.ToString(null);
		public string ToString(string format) => Matrix.ToString(this, format);

		public static bool operator ==(Matrix4x1 a, Matrix4x1 b)
		{
			bool result = true;
			result &= a.Value11 == b.Value11;
			result &= a.Value21 == b.Value21;
			result &= a.Value31 == b.Value31;
			result &= a.Value41 == b.Value41;
			return result;
		}
		public static bool operator !=(Matrix4x1 a, Matrix4x1 b) => !(a == b);
		public static Matrix4x1 operator +(Matrix4x1 a, Matrix4x1 b)
		{
			return new Matrix4x1(a.Value11 + b.Value11,
								 a.Value21 + b.Value21,
								 a.Value31 + b.Value31,
								 a.Value41 + b.Value41);
		}
		public static Matrix4x1 operator -(Matrix4x1 a, Matrix4x1 b)
		{
			return new Matrix4x1(a.Value11 - b.Value11,
								 a.Value21 - b.Value21,
								 a.Value31 - b.Value31,
								 a.Value41 - b.Value41);
		}
		public static Matrix4x1 operator *(Matrix4x1 m, float scalar)
		{
			return new Matrix4x1(m.Value11 * scalar,
								 m.Value21 * scalar,
								 m.Value31 * scalar,
								 m.Value41 * scalar);
		}
		public static Matrix4x1 operator *(Matrix4x1 m, int scalar)
		{
			return new Matrix4x1(m.Value11 * scalar,
								 m.Value21 * scalar,
								 m.Value31 * scalar,
								 m.Value41 * scalar);
		}
	}
}
