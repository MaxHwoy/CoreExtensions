namespace CoreExtensions.Numeric
{
	public interface ISquareMatrix : IMatrix
	{
		float Determinant { get; }
		ISquareMatrix IdentityMatrix { get; }
		bool IsDiagonal { get; }
		bool IsIdempotent { get; }
		bool IsIdentity { get; }
		bool IsInvertible { get; }
		bool IsLowerTriangular { get; }
		bool IsOrthogonal { get; }
		bool IsSkewSymmetric { get; }
		bool IsSymmetric { get; }
		bool IsUpperTriangular { get; }
		float Trace { get; }

		ISquareMatrix Invert();
	}
}
