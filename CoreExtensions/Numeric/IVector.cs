namespace CoreExtensions.Numeric
{
	public interface IVector
	{
		public int Components { get; }
		public float Length { get; }
		public float LengthSquared { get; }
		public IVector ZeroVector { get; }
		public IVector OneVector { get; }

		float this[int component] { get; set; }
	}
}
