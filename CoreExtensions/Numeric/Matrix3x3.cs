using System;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Matrix3x3 : ISquareMatrix, IMatrix, IEquatable<Matrix3x3>
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

		public static Matrix3x3 Zero => new Matrix3x3(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
		public static Matrix3x3 Identity => new Matrix3x3(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f);
		public int Columns => 3;
		public float Determinant => this.GetDeterminant();
		public int Entries => 9;
		public ISquareMatrix IdentityMatrix => Identity;
		public bool IsDiagonal => this.CheckDiagonal();
		public bool IsIdempotent => this.CheckIdempotency();
		public bool IsIdentity => this == Identity;
		public bool IsInvertible => this.Determinant != 0f;
		public bool IsLowerTriangular => this.CheckLowerTriangular();
		public bool IsOrthogonal => this.CheckOrthogonal();
		public bool IsSkewSymmetric => this.CheckSkewSymmetry();
		public bool IsSymmetric => this.CheckSymmetry();
		public bool IsUpperTriangular => this.CheckUpperTriangular();
		public int Rows => 3;
		public float Trace => this.GetTrace();
		public IMatrix ZeroMatrix => Zero;

		public Matrix3x3(Matrix3x3 matrix)
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
		}
		public Matrix3x3(float a11, float a12, float a13,
						 float a21, float a22, float a23,
						 float a31, float a32, float a33)
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
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 8");
				};
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
					default: throw new ArgumentOutOfRangeException(nameof(index), "Index should be in range 0 to 8");
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

					default:
						throw new ArgumentOutOfRangeException(nameof(row), "Row index should be in range 1 to 3");
				}
			}
		}

		private float GetDeterminant()
		{
			/* 
			 * If matrix is triangular then
			 * |A| = a11 * a22 * a33
			 */

			if (this.IsUpperTriangular || this.IsLowerTriangular)
			{

				return this.Value11 * this.Value22 * this.Value33;

			}

			/* 3rd power matrix determinant
			 *       | a11 a12 a13 | 
			 * |A| = | a21 a22 a23 | = a11(a22a33 - a23a32) - a12(a21a33 - a23a31) + a13(a21a32 - a22a31)
			 *       | a31 a32 a33 |   
			 */

			var p1 = this.Value11 * (this.Value22 * this.Value33 - this.Value23 * this.Value32);
			var p2 = this.Value12 * (this.Value21 * this.Value33 - this.Value23 * this.Value31);
			var p3 = this.Value13 * (this.Value21 * this.Value32 - this.Value22 * this.Value31);
			return p1 - p2 + p3;
		}
		private bool CheckOrthogonal()
		{
			if (!this.IsInvertible) return false;
			else return this.Transpose() == this.Invert();
		}
		private bool CheckSymmetry()
		{
			/* Matrix is symmetric when A = AT, e.g. of form
			 * [ a b c ]
			 * [ b d e ]
			 * [ c e f ]
			 */

			if (this.Value12 != this.Value21) return false;
			if (this.Value13 != this.Value31) return false;
			return this.Value23 == this.Value32;
		}
		private bool CheckSkewSymmetry()
		{
			/* Matrix is skew-symmetric when A = -AT, e.g. of form
			 * [  0  a  b ]
			 * [ -a  0  c ]
			 * [ -b -c  0 ]
			 */

			if (this.Value11 != 0f) return false;
			if (this.Value22 != 0f) return false;
			if (this.Value33 != 0f) return false;
			if (this.Value12 != -this.Value21) return false;
			if (this.Value13 != -this.Value31) return false;
			return this.Value23 == -this.Value32;
		}
		private bool CheckIdempotency()
		{
			/* 
			 * Matrix is idempotent when A = A^2
			 */
			return this == (this ^ 2);
		}
		private bool CheckDiagonal()
		{
			/* Diagonal matrix is a matrix of form
			 * [ a 0 0 ]
			 * [ 0 b 0 ]
			 * [ 0 0 c ]
			 * 
			 * Matrix is diagonal iff it is both upper and lower triangular
			 */

			return this.IsUpperTriangular && this.IsLowerTriangular;
		}
		private bool CheckUpperTriangular()
		{
			/* Upper triangular matrix is a matrix of form
			 * [ a b c ]
			 * [ 0 d e ]
			 * [ 0 0 f ]
			 */

			if (this.Value21 != 0f) return false;
			if (this.Value31 != 0f) return false;
			return this.Value32 == 0f;
		}
		private bool CheckLowerTriangular()
		{
			/* Lower triangular matrix is a matrix of form
			 * [ a 0 0 ]
			 * [ b c 0 ]
			 * [ d e f ]
			 */

			if (this.Value12 != 0f) return false;
			if (this.Value13 != 0f) return false;
			return this.Value23 == 0f;
		}
		private float GetTrace() => this.Value11 + this.Value22 + this.Value33;

		public Matrix3x3 Clone()
		{
			return new Matrix3x3(this.Value11, this.Value12, this.Value13,
								 this.Value21, this.Value22, this.Value23,
								 this.Value31, this.Value32, this.Value33);
		}
		public override bool Equals(object obj) => obj is Matrix3x3 matrix && matrix == this;
		public bool Equals(Matrix3x3 matrix) => this == matrix;
		public override int GetHashCode()
		{
			int result = HashCode.Combine(this.Value11, this.Value12, this.Value13);
			result = HashCode.Combine(result, this.Value21, this.Value22, this.Value23);
			return HashCode.Combine(result, this.Value31, this.Value32, this.Value33);
		}
		public Matrix3x3 Invert()
		{
			if (!this.IsInvertible) return Zero;

			/*
			 * A⁻¹ = (1 / |A|) * adj(A)
			 */

			/* [ + - + ]
			 * [ - + - ]
			 * [ + - + ]
			 */

			var result = new Matrix3x3
			{
				Value11 = this.Value22 * this.Value33 - this.Value23 * this.Value32,
				Value12 = this.Value13 * this.Value32 - this.Value12 * this.Value33,
				Value13 = this.Value12 * this.Value23 - this.Value13 * this.Value22,
				Value21 = this.Value23 * this.Value31 - this.Value21 * this.Value33,
				Value22 = this.Value11 * this.Value33 - this.Value13 * this.Value31,
				Value23 = this.Value13 * this.Value21 - this.Value11 * this.Value23,
				Value31 = this.Value21 * this.Value32 - this.Value22 * this.Value31,
				Value32 = this.Value12 * this.Value31 - this.Value11 * this.Value32,
				Value33 = this.Value11 * this.Value22 - this.Value12 * this.Value21
			};

			return result * (1 / this.Determinant);
		}
		ISquareMatrix ISquareMatrix.Invert() => this.Invert();
		public Matrix3x3 Power(int power) => this ^ power;
		public Matrix3x3 Transpose()
		{
			/*
			 * [ a11 a12 a13 ]T   [ a11 a21 a31 ]
			 * [ a21 a22 a23 ]  = [ a12 a22 a32 ]
			 * [ a31 a32 a33 ]    [ a13 a23 a33 ]
			 * 
			 */
			return new Matrix3x3(this.Value11, this.Value21, this.Value31,
								 this.Value12, this.Value22, this.Value32,
								 this.Value13, this.Value23, this.Value33);
		}
		IMatrix IMatrix.Transpose() => this.Transpose();
		public override string ToString() => this.ToString(null);
		public string ToString(string format) => Matrix.ToString(this, format);

		public static bool operator ==(Matrix3x3 a, Matrix3x3 b)
		{
			bool result = true;
			result &= a.Value11 == b.Value11;
			result &= a.Value12 == b.Value12;
			result &= a.Value13 == b.Value13;
			if (!result) return false;
			result &= a.Value21 == b.Value21;
			result &= a.Value22 == b.Value22;
			result &= a.Value23 == b.Value23;
			if (!result) return false;
			result &= a.Value31 == b.Value31;
			result &= a.Value32 == b.Value32;
			result &= a.Value33 == b.Value33;
			return result;
		}
		public static bool operator !=(Matrix3x3 a, Matrix3x3 b) => !(a == b);
		public static Matrix3x3 operator +(Matrix3x3 a, Matrix3x3 b)
		{
			return new Matrix3x3(a.Value11 + b.Value11, a.Value12 + b.Value12, a.Value13 + b.Value13,
								 a.Value21 + b.Value21, a.Value22 + b.Value22, a.Value23 + b.Value23,
								 a.Value31 + b.Value31, a.Value32 + b.Value32, a.Value33 + b.Value33);
		}
		public static Matrix3x3 operator -(Matrix3x3 a, Matrix3x3 b)
		{
			return new Matrix3x3(a.Value11 - b.Value11, a.Value12 - b.Value12, a.Value13 - b.Value13,
								 a.Value21 - b.Value21, a.Value22 - b.Value22, a.Value23 - b.Value23,
								 a.Value31 - b.Value31, a.Value32 - b.Value32, a.Value33 - b.Value33);

		}
		public static Matrix3x3 operator *(Matrix3x3 m, float scalar)
		{
			return new Matrix3x3(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar, m.Value23 * scalar,
								 m.Value31 * scalar, m.Value32 * scalar, m.Value33 * scalar);
		}
		public static Matrix3x3 operator *(Matrix3x3 m, int scalar)
		{
			return new Matrix3x3(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar, m.Value23 * scalar,
								 m.Value31 * scalar, m.Value32 * scalar, m.Value33 * scalar);
		}
		public static Matrix3x3 operator *(float scalar, Matrix3x3 m) => m * scalar;
		public static Matrix3x3 operator *(int scalar, Matrix3x3 m) => m * scalar;
		public static Matrix3x3 operator *(Matrix3x3 a, Matrix3x3 b)
		{
			return new Matrix3x3
			(
				a.Value11 * b.Value11 + a.Value12 * b.Value21 + a.Value13 * b.Value31,
				a.Value11 * b.Value12 + a.Value12 * b.Value22 + a.Value13 * b.Value32,
				a.Value11 * b.Value13 + a.Value12 * b.Value23 + a.Value13 * b.Value33,
				a.Value21 * b.Value11 + a.Value22 * b.Value21 + a.Value23 * b.Value31,
				a.Value21 * b.Value12 + a.Value22 * b.Value22 + a.Value23 * b.Value32,
				a.Value21 * b.Value13 + a.Value22 * b.Value23 + a.Value23 * b.Value33,
				a.Value31 * b.Value11 + a.Value32 * b.Value21 + a.Value33 * b.Value31,
				a.Value31 * b.Value12 + a.Value32 * b.Value22 + a.Value33 * b.Value32,
				a.Value31 * b.Value13 + a.Value32 * b.Value23 + a.Value33 * b.Value33
			);
		}
		public static Matrix3x3 operator ^(Matrix3x3 m, int power)
		{
			if (power < 0) throw new ArgumentOutOfRangeException("Raised power should be greater than 0");
			if (power == 0) return Identity;

			var result = m;
			for (int i = 1; i < power; ++i) result *= m;
			return result;
		}
	}
}
