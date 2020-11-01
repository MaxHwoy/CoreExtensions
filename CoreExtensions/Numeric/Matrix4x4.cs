using System;
using System.Text;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Matrix4x4 : ISquareMatrix, IMatrix
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
		public float Value41 { get; set; }
		public float Value42 { get; set; }
		public float Value43 { get; set; }
		public float Value44 { get; set; }

		public static Matrix4x4 Zero => new Matrix4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
		public static Matrix4x4 Identity => new Matrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
		public int Columns => 4;
		public float Determinant => this.GetDeterminant();
		public ISquareMatrix IdentityMatrix => Identity;
		public bool IsDiagonal => this.CheckDiagonal();
		public bool IsIdempotent => this.CheckIdempotency();
		public bool IsIdentity => this == Identity;
		public bool IsInvertible => this.Determinant != 0;
		public bool IsLowerTriangular => this.CheckLowerTriangular();
		public bool IsSkewSymmetric => this.CheckSkewSymmetry();
		public bool IsSymmetric => this.CheckSymmetry();
		public bool IsUpperTriangular => this.CheckUpperTriangular();
		public int Rows => 4;
		public float Trace => this.GetTrace();
		public IMatrix ZeroMatrix => Zero;

		public Matrix4x4(Matrix4x4 matrix)
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
			this.Value41 = matrix.Value41;
			this.Value42 = matrix.Value42;
			this.Value43 = matrix.Value43;
			this.Value44 = matrix.Value44;
		}
		public Matrix4x4(float a11, float a12, float a13, float a14,
						 float a21, float a22, float a23, float a24,
						 float a31, float a32, float a33, float a34,
						 float a41, float a42, float a43, float a44)
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
			this.Value41 = a41;
			this.Value42 = a42;
			this.Value43 = a43;
			this.Value44 = a44;
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
					case 12: return this.Value41;
					case 13: return this.Value42;
					case 14: return this.Value43;
					case 15: return this.Value44;
					default: throw new ArgumentOutOfRangeException("Index should be in range 0 to 15");
				};
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
					case 12: this.Value41 = value; return;
					case 13: this.Value42 = value; return;
					case 14: this.Value43 = value; return;
					case 15: this.Value44 = value; return;
					default: throw new ArgumentOutOfRangeException("Index should be in range 0 to 15");
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
							default: throw new ArgumentOutOfRangeException("Column index should be in range 1 to 4");
						}

					case 2:
						switch (column)
						{
							case 1: return this.Value21;
							case 2: return this.Value22;
							case 3: return this.Value23;
							case 4: return this.Value24;
							default: throw new ArgumentOutOfRangeException("Column index should be in range 1 to 4");
						}

					case 3:
						switch (column)
						{
							case 1: return this.Value31;
							case 2: return this.Value32;
							case 3: return this.Value33;
							case 4: return this.Value34;
							default: throw new ArgumentOutOfRangeException("Column index should be in range 1 to 4");
						}

					case 4:
						switch (column)
						{
							case 1: return this.Value41;
							case 2: return this.Value42;
							case 3: return this.Value43;
							case 4: return this.Value44;
							default: throw new ArgumentOutOfRangeException("Column index should be in range 1 to 4");
						}

					default:
						throw new ArgumentOutOfRangeException("Row index should be in range 1 to 4");
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
							default: throw new ArgumentOutOfRangeException("Column index should be in range 1 to 4");
						}

					case 2:
						switch (column)
						{
							case 1: this.Value21 = value; return;
							case 2: this.Value22 = value; return;
							case 3: this.Value23 = value; return;
							case 4: this.Value24 = value; return;
							default: throw new ArgumentOutOfRangeException("Column index should be in range 1 to 4");
						}

					case 3:
						switch (column)
						{
							case 1: this.Value31 = value; return;
							case 2: this.Value32 = value; return;
							case 3: this.Value33 = value; return;
							case 4: this.Value34 = value; return;
							default: throw new ArgumentOutOfRangeException("Column index should be in range 1 to 4");
						}

					case 4:
						switch (column)
						{
							case 1: this.Value41 = value; return;
							case 2: this.Value42 = value; return;
							case 3: this.Value43 = value; return;
							case 4: this.Value44 = value; return;
							default: throw new ArgumentOutOfRangeException("Column index should be in range 1 to 4");
						}

					default:
						throw new ArgumentOutOfRangeException("Row index should be in range 1 to 4");
				}
			}
		}

		private float GetDeterminant()
		{
			/* 
			 * If matrix is triangular then
			 * |A| = a11 * a22 * a33 * a44
			 */

			if (this.IsUpperTriangular || this.IsLowerTriangular)
			{

				return this.Value11 * this.Value22 * this.Value33 * this.Value44;

			}

			/* 4th power matrix determinant
			 * | a11 a12 a13 a14 |      | a22 a23 a24 |      | a21 a23 a24 |      | a21 a22 a24 |      | a21 a22 a23 |
			 * | a21 a22 a23 a24 | = a11| a32 a33 a34 | - a12| a31 a33 a34 | + a13| a31 a32 a34 | - a14| a31 a32 a33 |
			 * | a31 a32 a33 a34 |      | a42 a43 a44 |      | a41 a43 a44 |      | a41 a42 a44 |      | a41 a42 a43 |
			 * | a41 a42 a43 a44 |   
			 *
			 * 3rd power matrix determinant
			 * | a11 a12 a13 |
			 * | a21 a22 a23 | = a11(a22a33 - a23a32) - a12(a21a33 - a23a31) + a13(a21a32 - a22a31)
			 * | a31 a32 a33 |
			 * 
			 * Thus
			 * |A| = a11(a22(a33*a44 - a34*a43) - a23(a32*a44 - a34*a42) + a24(a32*a43 - a33*a42)) -
			 *       a12(a21(a33*a44 - a34*a43) - a23(a31*a44 - a34*a41) + a24(a31*a43 - a33*a41)) +
			 *       a13(a21(a32*a44 - a34*a42) - a22(a31*a44 - a34*a41) + a24(a31*a42 - a32*a41)) -
			 *       a14(a21(a32*a43 - a33*a42) - a22(a31*a43 - a33*a41) + a23(a31*a42 - a32*a41))
			 */

			var a33a44 = this.Value33 * this.Value44;
			var a34a43 = this.Value34 * this.Value43;
			var a32a44 = this.Value32 * this.Value44;
			var a34a42 = this.Value34 * this.Value42;
			var a32a43 = this.Value32 * this.Value43;
			var a33a42 = this.Value33 * this.Value42;
			var a31a44 = this.Value31 * this.Value44;
			var a34a41 = this.Value34 * this.Value41;
			var a31a43 = this.Value31 * this.Value43;
			var a33a41 = this.Value33 * this.Value41;
			var a31a42 = this.Value31 * this.Value42;
			var a32a41 = this.Value32 * this.Value41;

			var a33a44_a34a43 = a33a44 - a34a43;
			var a32a44_a34a42 = a32a44 - a34a42;
			var a32a43_a33a42 = a32a43 - a33a42;
			var a31a44_a34a41 = a31a44 - a34a41;
			var a31a43_a33a41 = a31a43 - a33a41;
			var a31a42_a32a41 = a31a42 - a32a41;

			var p1 = this.Value11 * (this.Value22 * a33a44_a34a43 - this.Value23 * a32a44_a34a42 + this.Value24 * a32a43_a33a42);
			var p2 = this.Value12 * (this.Value21 * a33a44_a34a43 - this.Value23 * a31a44_a34a41 + this.Value24 * a31a43_a33a41);
			var p3 = this.Value13 * (this.Value21 * a32a44_a34a42 - this.Value22 * a31a44_a34a41 + this.Value24 * a31a42_a32a41);
			var p4 = this.Value14 * (this.Value21 * a32a43_a33a42 - this.Value22 * a31a43_a33a41 + this.Value23 * a31a42_a32a41);

			return p1 - p2 + p3 - p4;
		}
		private bool CheckSymmetry()
		{
			/* Matrix is symmetric when A = AT, e.g. of form
			 * [ a b c d ]
			 * [ b e f g ]
			 * [ c f h i ]
			 * [ d g i j ]
			 */

			if (this.Value12 != this.Value21) return false;
			if (this.Value13 != this.Value31) return false;
			if (this.Value14 != this.Value41) return false;
			if (this.Value23 != this.Value32) return false;
			if (this.Value24 != this.Value42) return false;
			return this.Value34 == this.Value43;
		}
		private bool CheckSkewSymmetry()
		{
			/* Matrix is skew-symmetric when A = -AT, e.g. of form
			 * [  0  b  c  d ]
			 * [ -b  0  e  f ]
			 * [ -c -e  0  g ]
			 * [ -d -f -g  0 ]
			 */

			if (this.Value11 != 0f) return false;
			if (this.Value22 != 0f) return false;
			if (this.Value33 != 0f) return false;
			if (this.Value44 != 0f) return false;
			if (this.Value12 != -this.Value21) return false;
			if (this.Value13 != -this.Value31) return false;
			if (this.Value14 != -this.Value41) return false;
			if (this.Value23 != -this.Value32) return false;
			if (this.Value24 != -this.Value42) return false;
			return this.Value34 == -this.Value43;
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
			 * [ a 0 0 0 ]
			 * [ 0 b 0 0 ]
			 * [ 0 0 c 0 ]
			 * [ 0 0 0 d ]
			 * 
			 * Matrix is diagonal iff it is both upper and lower triangular
			 */

			return this.IsUpperTriangular && this.IsLowerTriangular;
		}
		private bool CheckUpperTriangular()
		{
			/* Upper triangular matrix is a matrix of form
			 * [ a b c d ]
			 * [ 0 e f g ]
			 * [ 0 0 h i ]
			 * [ 0 0 0 j ]
			 */

			if (this.Value21 != 0f) return false;
			if (this.Value31 != 0f) return false;
			if (this.Value41 != 0f) return false;
			if (this.Value32 != 0f) return false;
			if (this.Value42 != 0f) return false;
			return this.Value43 == 0f;
		}
		private bool CheckLowerTriangular()
		{
			/* Lower triangular matrix is a matrix of form
			 * [ a 0 0 0 ]
			 * [ b c 0 0 ]
			 * [ d e f 0 ]
			 * [ g h i j ]
			 */

			if (this.Value12 != 0f) return false;
			if (this.Value13 != 0f) return false;
			if (this.Value14 != 0f) return false;
			if (this.Value23 != 0f) return false;
			if (this.Value24 != 0f) return false;
			return this.Value34 == 0f;
		}
		private float GetTrace() => this.Value11 + this.Value22 + this.Value33 + this.Value44;

		public Matrix4x4 Clone()
		{
			return new Matrix4x4(this.Value11, this.Value12, this.Value13, this.Value14,
								 this.Value21, this.Value22, this.Value23, this.Value24,
								 this.Value31, this.Value32, this.Value33, this.Value34,
								 this.Value41, this.Value42, this.Value43, this.Value44);
		}
		public override bool Equals(object obj) => obj is Matrix4x4 matrix && matrix == this;
		public bool Equals(Matrix4x4 matrix) => this == matrix;
		public override int GetHashCode()
		{
			int result = HashCode.Combine(this.Value11, this.Value12, this.Value13, this.Value14);
			result = HashCode.Combine(result, this.Value21, this.Value22, this.Value23, this.Value24);
			result = HashCode.Combine(result, this.Value31, this.Value32, this.Value33, this.Value34);
			return HashCode.Combine(result, this.Value41, this.Value42, this.Value43, this.Value44);
		}
		public Matrix4x4 Invert()
		{
			if (!this.IsInvertible) return Zero;

			/*
			 * A^(-1) = (1 / |A|) * adj(A)
			 */

			/* [ + - + - ]
			 * [ - + - + ]
			 * [ + - + - ]
			 * [ - + - + ]
			 */

			var result = new Matrix4x4();

			var a33a44 = this.Value33 * this.Value44;
			var a34a43 = this.Value34 * this.Value43;
			var a34a42 = this.Value34 * this.Value42;
			var a32a44 = this.Value32 * this.Value44;
			var a32a43 = this.Value32 * this.Value43;
			var a33a42 = this.Value33 * this.Value42;
			var a23a44 = this.Value23 * this.Value44;
			var a24a43 = this.Value24 * this.Value43;
			var a24a42 = this.Value24 * this.Value42;
			var a22a44 = this.Value22 * this.Value44;
			var a22a43 = this.Value22 * this.Value43;
			var a23a42 = this.Value23 * this.Value42;
			var a24a33 = this.Value24 * this.Value33;
			var a23a34 = this.Value23 * this.Value34;
			var a22a34 = this.Value22 * this.Value34;
			var a24a32 = this.Value24 * this.Value32;
			var a23a32 = this.Value23 * this.Value32;
			var a22a33 = this.Value22 * this.Value33;
			var a31a44 = this.Value31 * this.Value44;
			var a34a41 = this.Value34 * this.Value41;
			var a33a41 = this.Value33 * this.Value41;
			var a31a43 = this.Value31 * this.Value43;
			var a21a44 = this.Value21 * this.Value44;
			var a24a41 = this.Value24 * this.Value41;
			var a23a41 = this.Value23 * this.Value41;
			var a21a43 = this.Value21 * this.Value43;
			var a24a31 = this.Value24 * this.Value31;
			var a21a34 = this.Value21 * this.Value34;
			var a21a33 = this.Value21 * this.Value33;
			var a23a31 = this.Value23 * this.Value31;
			var a31a42 = this.Value31 * this.Value42;
			var a32a41 = this.Value32 * this.Value41;
			var a21a42 = this.Value21 * this.Value42;
			var a22a41 = this.Value22 * this.Value41;
			var a22a31 = this.Value22 * this.Value31;
			var a21a32 = this.Value21 * this.Value32;

			result.Value11 = this.Value22 * (a33a44 - a34a43) + this.Value23 * (a34a42 - a32a44) + this.Value24 * (a32a43 - a33a42);
			result.Value12 = this.Value12 * (a34a43 - a33a44) + this.Value13 * (a32a44 - a34a42) + this.Value14 * (a33a42 - a32a43);
			result.Value13 = this.Value12 * (a23a44 - a24a43) + this.Value13 * (a24a42 - a22a44) + this.Value14 * (a22a43 - a23a42);
			result.Value14 = this.Value12 * (a24a33 - a23a34) + this.Value13 * (a22a34 - a24a32) + this.Value14 * (a23a32 - a22a33);
			result.Value21 = this.Value21 * (a34a43 - a33a44) + this.Value23 * (a31a44 - a34a41) + this.Value24 * (a33a41 - a31a43);
			result.Value22 = this.Value11 * (a33a44 - a34a43) + this.Value13 * (a34a41 - a31a44) + this.Value14 * (a31a43 - a33a41);
			result.Value23 = this.Value11 * (a24a43 - a23a44) + this.Value13 * (a21a44 - a24a41) + this.Value14 * (a23a41 - a21a43);
			result.Value24 = this.Value11 * (a23a34 - a24a33) + this.Value13 * (a24a31 - a21a34) + this.Value14 * (a21a33 - a23a31);
			result.Value31 = this.Value21 * (a32a44 - a34a42) + this.Value22 * (a34a41 - a31a44) + this.Value24 * (a31a42 - a32a41);
			result.Value32 = this.Value11 * (a34a42 - a32a44) + this.Value12 * (a31a44 - a34a41) + this.Value14 * (a32a41 - a31a42);
			result.Value33 = this.Value11 * (a22a44 - a24a42) + this.Value12 * (a24a41 - a21a44) + this.Value14 * (a21a42 - a22a41);
			result.Value34 = this.Value11 * (a24a32 - a22a34) + this.Value12 * (a21a34 - a24a31) + this.Value14 * (a22a31 - a21a32);
			result.Value41 = this.Value21 * (a33a42 - a32a43) + this.Value22 * (a31a43 - a33a41) + this.Value23 * (a32a41 - a31a42);
			result.Value42 = this.Value11 * (a32a43 - a33a42) + this.Value12 * (a33a41 - a31a43) + this.Value13 * (a31a42 - a32a41);
			result.Value43 = this.Value11 * (a23a42 - a22a43) + this.Value12 * (a21a43 - a23a41) + this.Value13 * (a22a41 - a21a42);
			result.Value44 = this.Value11 * (a22a33 - a23a32) + this.Value12 * (a23a31 - a21a33) + this.Value13 * (a21a32 - a22a31);

			return result * (1 / this.Determinant);
		}
		ISquareMatrix ISquareMatrix.Invert() => this.Invert();
		public Matrix4x4 Power(int power) => this ^ power;
		
		public Matrix4x4 Transpose()
		{
			/*
			 * [ a11 a12 a13 a14 ]T   [ a11 a21 a31 a41 ]
			 * [ a21 a22 a23 a24 ]  = [ a12 a22 a32 a42 ]
			 * [ a31 a32 a33 a34 ]    [ a13 a23 a33 a43 ]
			 * [ a41 a42 a43 a44 ]    [ a14 a24 a34 a44 ]
			 * 
			 */
			return new Matrix4x4(this.Value11, this.Value21, this.Value31, this.Value41,
								 this.Value12, this.Value22, this.Value32, this.Value42,
								 this.Value13, this.Value23, this.Value33, this.Value43,
								 this.Value14, this.Value24, this.Value34, this.Value44);
		}
		IMatrix IMatrix.Transpose() => this.Transpose();
		public override string ToString() => this.ToString(null);
		public string ToString(string format)
		{
			/*
			 * Supported formats are:
			 * null/String.Empty = regular matrix format
			 * "-" = all entries are inlined and separated with -
			 * " " = all entries are inlined and separated with whitespace
			 */

			var sb = new StringBuilder(100);

			switch (format)
			{
				case null:
				case "":
					for (int i = 1; i <= 4; ++i)
					{

						for (int k = 1; k <= 4; ++k) sb.Append(this[i, k]);
						sb.AppendLine();

					}
					return sb.ToString();

				case " ":
				case "-":
					for (int i = 0; i < 16; ++i) { sb.Append(this[i]); sb.Append(format); }
					return sb.ToString();

				default:
					goto case null;

			}
		}

		public static bool operator ==(Matrix4x4 a, Matrix4x4 b)
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
			if (!result) return false;
			result &= a.Value41 == b.Value41;
			result &= a.Value42 == b.Value42;
			result &= a.Value43 == b.Value43;
			result &= a.Value44 == b.Value44;
			return result;
		}
		public static bool operator !=(Matrix4x4 a, Matrix4x4 b) => !(a == b);
		public static Matrix4x4 operator +(Matrix4x4 a, Matrix4x4 b)
		{
			return new Matrix4x4(a.Value11 + b.Value11, a.Value12 + b.Value12, a.Value13 + b.Value13, a.Value14 + b.Value14,
								 a.Value21 + b.Value21, a.Value22 + b.Value22, a.Value23 + b.Value23, a.Value24 + b.Value24,
								 a.Value31 + b.Value31, a.Value32 + b.Value32, a.Value33 + b.Value33, a.Value34 + b.Value34,
								 a.Value41 + b.Value41, a.Value42 + b.Value42, a.Value43 + b.Value43, a.Value44 + b.Value44);
		}
		public static Matrix4x4 operator -(Matrix4x4 a, Matrix4x4 b)
		{
			return new Matrix4x4(a.Value11 - b.Value11, a.Value12 - b.Value12, a.Value13 - b.Value13, a.Value14 - b.Value14,
								 a.Value21 - b.Value21, a.Value22 - b.Value22, a.Value23 - b.Value23, a.Value24 - b.Value24,
								 a.Value31 - b.Value31, a.Value32 - b.Value32, a.Value33 - b.Value33, a.Value34 - b.Value34,
								 a.Value41 - b.Value41, a.Value42 - b.Value42, a.Value43 - b.Value43, a.Value44 - b.Value44);

		}
		public static Matrix4x4 operator *(Matrix4x4 m, float scalar)
		{
			return new Matrix4x4(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar, m.Value14 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar, m.Value23 * scalar, m.Value24 * scalar,
								 m.Value31 * scalar, m.Value32 * scalar, m.Value33 * scalar, m.Value34 * scalar,
								 m.Value41 * scalar, m.Value42 * scalar, m.Value43 * scalar, m.Value44 * scalar);
		}
		public static Matrix4x4 operator *(Matrix4x4 m, int scalar)
		{
			return new Matrix4x4(m.Value11 * scalar, m.Value12 * scalar, m.Value13 * scalar, m.Value14 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar, m.Value23 * scalar, m.Value24 * scalar,
								 m.Value31 * scalar, m.Value32 * scalar, m.Value33 * scalar, m.Value34 * scalar,
								 m.Value41 * scalar, m.Value42 * scalar, m.Value43 * scalar, m.Value44 * scalar);
		}
		public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
		{
			return new Matrix4x4
			(
				a.Value11 * b.Value11 + a.Value12 * b.Value21 + a.Value13 * b.Value31 + a.Value14 * b.Value41,
				a.Value11 * b.Value12 + a.Value12 * b.Value22 + a.Value13 * b.Value32 + a.Value14 * b.Value42,
				a.Value11 * b.Value13 + a.Value12 * b.Value23 + a.Value13 * b.Value33 + a.Value14 * b.Value43,
				a.Value11 * b.Value14 + a.Value12 * b.Value24 + a.Value13 * b.Value34 + a.Value14 * b.Value44,
				a.Value21 * b.Value11 + a.Value22 * b.Value21 + a.Value23 * b.Value31 + a.Value24 * b.Value41,
				a.Value21 * b.Value12 + a.Value22 * b.Value22 + a.Value23 * b.Value32 + a.Value24 * b.Value42,
				a.Value21 * b.Value13 + a.Value22 * b.Value23 + a.Value23 * b.Value33 + a.Value24 * b.Value43,
				a.Value21 * b.Value14 + a.Value22 * b.Value24 + a.Value23 * b.Value34 + a.Value24 * b.Value44,
				a.Value31 * b.Value11 + a.Value32 * b.Value21 + a.Value33 * b.Value31 + a.Value34 * b.Value41,
				a.Value31 * b.Value12 + a.Value32 * b.Value22 + a.Value33 * b.Value32 + a.Value34 * b.Value42,
				a.Value31 * b.Value13 + a.Value32 * b.Value23 + a.Value33 * b.Value33 + a.Value34 * b.Value43,
				a.Value31 * b.Value14 + a.Value32 * b.Value24 + a.Value33 * b.Value34 + a.Value34 * b.Value44,
				a.Value41 * b.Value11 + a.Value42 * b.Value21 + a.Value43 * b.Value31 + a.Value44 * b.Value41,
				a.Value41 * b.Value12 + a.Value42 * b.Value22 + a.Value43 * b.Value32 + a.Value44 * b.Value42,
				a.Value41 * b.Value13 + a.Value42 * b.Value23 + a.Value43 * b.Value33 + a.Value44 * b.Value43,
				a.Value41 * b.Value14 + a.Value42 * b.Value24 + a.Value43 * b.Value34 + a.Value44 * b.Value44
			);
		}
		public static Matrix4x4 operator ^(Matrix4x4 m, int power)
		{
			if (power < 0) throw new ArgumentOutOfRangeException("Raised power should be greater than 0");
			if (power == 0) return Identity;

			var result = m;
			for (int i = 1; i < power; ++i) result *= m;
			return result;
		}
	}
}
