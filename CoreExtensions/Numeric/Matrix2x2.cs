using System;
using System.Text;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Matrix2x2 : ISquareMatrix, IMatrix
	{
		public float Value11 { get; set; }
		public float Value12 { get; set; }
		public float Value21 { get; set; }
		public float Value22 { get; set; }

		public static Matrix2x2 Zero => new Matrix2x2(0f, 0f, 0f, 0f);
		public static Matrix2x2 Identity => new Matrix2x2(1f, 0f, 0f, 1f);
		public int Columns => 2;
		public float Determinant => this.GetDeterminant();
		public ISquareMatrix IdentityMatrix => Identity;
		public bool IsDiagonal => this.CheckDiagonal();
		public bool IsIdempotent => this.CheckIdempotency();
		public bool IsIdentity => this == Identity;
		public bool IsInvertible => this.Determinant != 0f;
		public bool IsLowerTriangular => this.CheckLowerTriangular();
		public bool IsOrthogonal => this.Transpose() == this.Invert();
		public bool IsSkewSymmetric => this.CheckSkewSymmetry();
		public bool IsSymmetric => this.CheckSymmetry();
		public bool IsUpperTriangular => this.CheckUpperTriangular();
		public int Rows => 2;
		public float Trace => this.GetTrace();
		public IMatrix ZeroMatrix => Zero;

		public Matrix2x2(Matrix2x2 matrix)
		{
			this.Value11 = matrix.Value11;
			this.Value12 = matrix.Value12;
			this.Value21 = matrix.Value21;
			this.Value22 = matrix.Value22;
		}
		public Matrix2x2(float a11, float a12,
						 float a21, float a22)
		{
			this.Value11 = a11;
			this.Value12 = a12;
			this.Value21 = a21;
			this.Value22 = a22;
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
					default: throw new ArgumentOutOfRangeException("Index should be in range 0 to 3");
				};
			}
			set
			{
				switch (index)
				{
					case 0: this.Value11 = value; return;
					case 1: this.Value12 = value; return;
					case 2: this.Value21 = value; return;
					case 3: this.Value22 = value; return;
					default: throw new ArgumentOutOfRangeException("Index should be in range 0 to 3");
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
							default: throw new ArgumentOutOfRangeException("Column index should be in range 1 to 2");
						}

					case 2:
						switch (column)
						{
							case 1: return this.Value21;
							case 2: return this.Value22;
							default: throw new ArgumentOutOfRangeException("Column index should be in range 1 to 2");
						}

					default:
						throw new ArgumentOutOfRangeException("Row index should be in range 1 to 2");
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
							default: throw new ArgumentOutOfRangeException("Column index should be in range 1 to 2");
						}

					case 2:
						switch (column)
						{
							case 1: this.Value21 = value; return;
							case 2: this.Value22 = value; return;
							default: throw new ArgumentOutOfRangeException("Column index should be in range 1 to 2");
						}

					default:
						throw new ArgumentOutOfRangeException("Row index should be in range 1 to 2");
				}
			}
		}

		private float GetDeterminant()
		{
			// |A| = ad - bc
			return this.Value11 * this.Value22 - this.Value12 * this.Value21;
		}
		private bool CheckSymmetry() => this.Value12 == this.Value21;
		private bool CheckSkewSymmetry()
		{
			bool result = true;
			result &= this.Value11 == 0f;
			result &= this.Value22 == 0f;
			return result && this.Value12 == -this.Value21;
		}
		private bool CheckIdempotency() => this == (this ^ 2);
		private bool CheckDiagonal() => this.Value12 == 0f && this.Value21 == 0f;
		private bool CheckUpperTriangular() => this.Value21 == 0f;
		private bool CheckLowerTriangular() => this.Value12 == 0f;
		private float GetTrace() => this.Value11 + this.Value22;

		public Matrix2x2 Clone()
		{
			return new Matrix2x2(this.Value11, this.Value12,
								 this.Value21, this.Value22);
		}
		public override bool Equals(object obj) => obj is Matrix2x2 matrix && matrix == this;
		public bool Equals(Matrix2x2 matrix) => this == matrix;
		public override int GetHashCode()
		{
			int result = HashCode.Combine(this.Value11, this.Value12);
			return HashCode.Combine(result, this.Value21, this.Value22);
		}
		public Matrix2x2 Invert()
		{
			if (!this.IsInvertible) return Zero;

			/*
			 * A^(-1) = (1 / |A|) * adj(A)
			 */

			var result = new Matrix2x2(this.Value22, -this.Value12, -this.Value21, this.Value11);
			return result * (1 / this.Determinant);
		}
		ISquareMatrix ISquareMatrix.Invert() => this.Invert();
		public Matrix2x2 Power(int power) => this ^ power;

		public Matrix2x2 Transpose()
		{
			/*
			 * [ a11 a12 ]T = [ a11 a21 ]
			 * [ a21 a22 ]  = [ a12 a22 ]
			 * 
			 */
			return new Matrix2x2(this.Value11, this.Value21,
								 this.Value12, this.Value22);
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

			var sb = new StringBuilder(40);

			switch (format)
			{
				case null:
				case "":
					for (int i = 1; i <= 2; ++i)
					{

						for (int k = 1; k <= 2; ++k) sb.Append(this[i, k]);
						sb.AppendLine();

					}
					return sb.ToString();

				case " ":
				case "-":
					for (int i = 0; i < 4; ++i) { sb.Append(this[i]); sb.Append(format); }
					return sb.ToString();

				default:
					goto case null;

			}
		}

		public static bool operator ==(Matrix2x2 a, Matrix2x2 b)
		{
			bool result = true;
			result &= a.Value11 == b.Value11;
			result &= a.Value12 == b.Value12;
			if (!result) return false;
			result &= a.Value21 == b.Value21;
			result &= a.Value22 == b.Value22;
			return result;
		}
		public static bool operator !=(Matrix2x2 a, Matrix2x2 b) => !(a == b);
		public static Matrix2x2 operator +(Matrix2x2 a, Matrix2x2 b)
		{
			return new Matrix2x2(a.Value11 + b.Value11, a.Value12 + b.Value12,
								 a.Value21 + b.Value21, a.Value22 + b.Value22);
		}
		public static Matrix2x2 operator -(Matrix2x2 a, Matrix2x2 b)
		{
			return new Matrix2x2(a.Value11 - b.Value11, a.Value12 - b.Value12,
								 a.Value21 - b.Value21, a.Value22 - b.Value22);

		}
		public static Matrix2x2 operator *(Matrix2x2 m, float scalar)
		{
			return new Matrix2x2(m.Value11 * scalar, m.Value12 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar);
		}
		public static Matrix2x2 operator *(Matrix2x2 m, int scalar)
		{
			return new Matrix2x2(m.Value11 * scalar, m.Value12 * scalar,
								 m.Value21 * scalar, m.Value22 * scalar);
		}
		public static Matrix2x2 operator *(Matrix2x2 a, Matrix2x2 b)
		{
			return new Matrix2x2
			(
				a.Value11 * b.Value11 + a.Value12 * b.Value21,
				a.Value11 * b.Value12 + a.Value12 * b.Value22,
				a.Value21 * b.Value11 + a.Value22 * b.Value21,
				a.Value21 * b.Value12 + a.Value22 * b.Value22
			);
		}
		public static Matrix2x2 operator ^(Matrix2x2 m, int power)
		{
			if (power < 0) throw new ArgumentOutOfRangeException("Raised power should be greater than 0");
			if (power == 0) return Identity;

			var result = m;
			for (int i = 1; i < power; ++i) result *= m;
			return result;
		}
	}
}
