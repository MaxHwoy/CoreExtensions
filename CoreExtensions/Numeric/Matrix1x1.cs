using System;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Matrix1x1 : ISquareMatrix, IMatrix
	{
		public float Value11 { get; set; }

		public static Matrix1x1 Zero => new Matrix1x1(0f);
		public static Matrix1x1 Identity => new Matrix1x1(1f);
		public int Columns => 1;
		public float Determinant => this.Value11;
		public int Entries => 1;
		public ISquareMatrix IdentityMatrix => Identity;
		public bool IsDiagonal => true;
		public bool IsIdempotent => this.Value11 == 1f || this.Value11 == 0f;
		public bool IsIdentity => this == Identity;
		public bool IsInvertible => this.Determinant != 0f;
		public bool IsLowerTriangular => true;
		public bool IsOrthogonal => this.Value11 == 1f;
		public bool IsSkewSymmetric => this.Value11 == 0f;
		public bool IsSymmetric => true;
		public bool IsUpperTriangular => true;
		public int Rows => 1;
		public float Trace => this.Value11;
		public IMatrix ZeroMatrix => Zero;

		public Matrix1x1(Matrix1x1 matrix) => this.Value11 = matrix.Value11;
		public Matrix1x1(float a11) => this.Value11 = a11;

		public float this[int index]
		{
			get
			{
				if (index == 0) return this.Value11;
				else throw new ArgumentOutOfRangeException(nameof(index), "Index can only be 0");
			}
			set
			{
				if (index == 0) this.Value11 = value;
				else throw new ArgumentOutOfRangeException(nameof(index), "Index can only be 0");
			}
		}
		public float this[int row, int column]
		{
			get
			{
				if (row != 0) throw new ArgumentOutOfRangeException(nameof(row), "Row index should be 0");
				if (column != 0) throw new ArgumentOutOfRangeException(nameof(column), "Column index should be 0");
				return this.Value11;
			}
			set
			{
				if (row != 0) throw new ArgumentOutOfRangeException(nameof(row), "Row index should be 0");
				if (column != 0) throw new ArgumentOutOfRangeException(nameof(column), "Column index should be 0");
				this.Value11 = value;
			}
		}

		public Matrix1x1 Clone() => new Matrix1x1(this.Value11);
		public override bool Equals(object obj) => obj is Matrix1x1 matrix && matrix == this;
		public bool Equals(Matrix1x1 matrix) => this == matrix;
		public override int GetHashCode() => this.Value11.GetHashCode();
		public Matrix1x1 Invert()
		{
			if (!this.IsInvertible) return Zero;
			else return new Matrix1x1(1 / this.Value11);
		}
		ISquareMatrix ISquareMatrix.Invert() => this.Invert();
		public Matrix1x1 Power(int power) => this ^ power;
		public Matrix1x1 Transpose() => new Matrix1x1(this.Value11);
		IMatrix IMatrix.Transpose() => this.Transpose();
		public override string ToString() => this.Value11.ToString();

		public static bool operator ==(Matrix1x1 a, Matrix1x1 b) => a.Value11 == b.Value11;
		public static bool operator !=(Matrix1x1 a, Matrix1x1 b) => a.Value11 != b.Value11;
		public static Matrix1x1 operator +(Matrix1x1 a, Matrix1x1 b) => new Matrix1x1(a.Value11 + b.Value11);
		public static Matrix1x1 operator -(Matrix1x1 a, Matrix1x1 b) => new Matrix1x1(a.Value11 - b.Value11);
		public static Matrix1x1 operator *(Matrix1x1 m, float scalar) => new Matrix1x1(m.Value11 * scalar);
		public static Matrix1x1 operator *(Matrix1x1 m, int scalar) => new Matrix1x1(m.Value11 * scalar);
		public static Matrix1x1 operator *(Matrix1x1 a, Matrix1x1 b) => new Matrix1x1(a.Value11 * b.Value11);
		public static Matrix1x1 operator ^(Matrix1x1 m, int power)
		{
			if (power < 0) throw new ArgumentOutOfRangeException("Raised power should be greater than 0");
			if (power == 0) return Identity;
			return new Matrix1x1((float)Math.Pow(m.Value11, power));
		}
	}
}
