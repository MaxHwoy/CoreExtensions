namespace CoreExtensions.Numeric
{
	/// <summary>
	/// Defines an <see cref="IMatrix"/> where number of rows equals number of columns.
	/// </summary>
	public interface ISquareMatrix : IMatrix
	{
		/// <summary>
		/// Determinant value of this <see cref="ISquareMatrix"/>. Determinant is a scalar value that 
		/// can be computed from the elements of a square matrix and it encodes certain properties of 
		/// the linear transformation described by the matrix.
		/// </summary>
		float Determinant { get; }

		/// <summary>
		/// Gets identity matrix with the same number of rows and columns as this <see cref="ISquareMatrix"/>.
		/// </summary>
		ISquareMatrix IdentityMatrix { get; }

		/// <summary>
		/// Checks whether this <see cref="ISquareMatrix"/> is a diagonal matrix. Matrix is diagonal if all 
		/// its entries besides main diagonal ones are 0.
		/// </summary>
		bool IsDiagonal { get; }

		/// <summary>
		/// Checks whether this <see cref="ISquareMatrix"/> is an idempotent matrix. Matrix A is idempotent
		/// if A² = A, meaning if its square equals itself.
		/// </summary>
		bool IsIdempotent { get; }

		/// <summary>
		/// Checks whether this <see cref="ISquareMatrix"/> is an identity matrix. Matrix is an identity matrix
		/// if its main diagonal entries are 1 and all other entries are 0.
		/// </summary>
		bool IsIdentity { get; }

		/// <summary>
		/// Checks whether this <see cref="ISquareMatrix"/> is invertible. Matrix A is invertible if there 
		/// exists matrix B such that AB = BA = I, meaning product of A and B equals identity matrix.
		/// </summary>
		bool IsInvertible { get; }

		/// <summary>
		/// Checks whether this <see cref="ISquareMatrix"/> is a lower triangular matrix. Matrix is lower
		/// triangular if all its entries above the main diagonal are zero.
		/// </summary>
		bool IsLowerTriangular { get; }

		/// <summary>
		/// Checks whether this <see cref="ISquareMatrix"/> is orthogonal. Matrix is orthogonal if its
		/// transpose equals its inverse. Requires matrix to be invertible, e.g. <see cref="IsInvertible"/>
		/// = <see langword="true"/>.
		/// </summary>
		bool IsOrthogonal { get; }

		/// <summary>
		/// Checks whether this <see cref="ISquareMatrix"/> is skew-symmetric. Matrix A is skew-symmetric
		/// if it equals negative reciprocal of its transpose, meaning Aᵀ = -A.
		/// </summary>
		bool IsSkewSymmetric { get; }

		/// <summary>
		/// Checks whether this <see cref="ISquareMatrix"/> is symmetric. Matrix A is symmetric if it
		/// equals its transpose, meaning A = Aᵀ.
		/// </summary>
		bool IsSymmetric { get; }

		/// <summary>
		/// Checks whether this <see cref="ISquareMatrix"/> is an upper triangular matrix. Matrix is upper
		/// triangular if all its entries below the main diagonal are zero.
		/// </summary>
		bool IsUpperTriangular { get; }

		/// <summary>
		/// Trace of a <see cref="ISquareMatrix"/> is a sum of its diagonal entries.
		/// </summary>
		float Trace { get; }

		/// <summary>
		/// Gets inverse of this <see cref="ISquareMatrix"/>. If a matrix is invertible, its inverse is a 
		/// matrix A⁻¹, such that AA⁻¹ = I, meaning product of a matrix with its inverse equals identity 
		/// matrix. Requires <see cref="IsInvertible"/> = <see langword="true"/>.
		/// </summary>
		ISquareMatrix Invert();
	}
}
