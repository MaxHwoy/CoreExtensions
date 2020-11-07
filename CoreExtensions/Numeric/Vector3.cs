using System;
using System.Text;
using System.Globalization;
using System.Runtime.InteropServices;



namespace CoreExtensions.Numeric
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Vector3 : IVector, IEquatable<Vector3>, IFormattable
	{
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

		public static Vector3 Zero => new Vector3(0f, 0f, 0f);
		public static Vector3 One => new Vector3(1f, 1f, 1f);
		public static Vector3 UnitX => new Vector3(1f, 0f, 0f);
		public static Vector3 UnitY => new Vector3(0f, 1f, 0f);
		public static Vector3 UnitZ => new Vector3(0f, 0f, 1f);
		public int Components => 3;
		public float Length => (float)Math.Sqrt(this.LengthSquared);
		public float LengthSquared => this.X * this.X + this.Y * this.Y + this.Z * this.Z;
		public IVector ZeroVector => Zero;
		public IVector OneVector => One;

		public float DirectionAngleI => this.X / this.Length;
		public float DirectionAngleJ => this.Y / this.Length;
		public float DirectionAngleK => this.Z / this.Length;

		public Vector3(Vector3 other)
		{
			this.X = other.X;
			this.Y = other.Y;
			this.Z = other.Z;
		}

		public Vector3(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public float this[int component]
		{
			get
			{
				switch (component)
				{
					case 0: return this.X;
					case 1: return this.Y;
					case 2: return this.Z;
					default: throw new ArgumentOutOfRangeException(nameof(component), "Index of the component should be in range 0 to 2");
				}
			}
			set
			{
				switch (component)
				{
					case 0: this.X = value; return;
					case 1: this.Y = value; return;
					case 2: this.Z = value; return;
					default: throw new ArgumentOutOfRangeException(nameof(component), "Index of the component should be in range 0 to 2");
				}
			}
		}

		public static float AngleCosine(Vector3 a, Vector3 b)
		{
			// cos(x) = (a * b) / (|a||b|)
			return Dot(a, b) / (a.Length * b.Length);
		}

		public static float AngleSine(Vector3 a, Vector3 b)
		{
			// sin(x) = |a x b| / (|a||b|)
			return Cross(a, b).Length / (a.Length * b.Length);
		}

		public static Vector3 Cross(Vector3 a, Vector3 b)
		{
			return new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
		}

		public static float Distance(Vector3 a, Vector3 b)
		{
			return (float)Math.Sqrt(DistanceSquared(a, b));
		}

		public static float DistanceSquared(Vector3 a, Vector3 b)
		{
			var dx = a.X - b.X;
			var dy = a.Y - b.Y;
			var dz = a.Z - b.Z;
			return dx * dx + dy * dy + dz * dz;
		}

		public static float Dot(Vector3 a, Vector3 b)
		{
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
		}

		public bool Equals(Vector3 other) => this == other;

		public override bool Equals(object obj) => obj is Vector3 vector && this == vector;

		public override int GetHashCode() => HashCode.Combine(this.X, this.Y, this.Z);

		public static float InverseLerp(Vector3 a, Vector3 b, Vector3 result)
		{
			float dx = b.X - a.X;
			float dy = b.Y - a.Y;
			float dz = b.Z - a.Z;

			// If one of differences is zero, return 0 b/c otherwise division by 0 later on
			if (dx == 0f || dy == 0f || dz == 0f) return 0f;

			dx = (result.X - a.X) / dx;
			dy = (result.Y - a.Y) / dy;
			dz = (result.Z - a.Z) / dz;
			return (dx + dy + dz) / 3; // return lerp average
		}

		public static Vector3 Lerp(Vector3 a, Vector3 b, float amount)
		{
			return new Vector3(a.X + (b.X - a.X) * amount,
							   a.Y + (b.Y - a.Y) * amount,
							   a.Z + (b.Z - a.Z) * amount);
		}

		public static bool Orthogonal(Vector3 a, Vector3 b) => Dot(a, b) == 0f;

		public static bool Parallel(Vector3 a, Vector3 b) => Cross(a, b) == Zero;

		public Matrix1x3 ToVectorMatrix() => new Matrix1x3(this.X, this.Y, this.Z);

		public Matrix3x1 ToCoordinateMatrix() => new Matrix3x1(this.X, this.Y, this.Z);

		public static float ScalarProjection(Vector3 along, Vector3 comp)
		{
			// comp(a)b
			return Dot(along, comp) / along.Length;
		}

		public static Vector3 VectorProjection(Vector3 along, Vector3 comp)
		{
			// proj(a)b
			return (ScalarProjection(along, comp) / along.Length) * along;
		}

		public static float ScalarTripleProduct(Vector3 a, Vector3 b, Vector3 c)
		{
			return Dot(a, Cross(b, c));
		}

		public static Vector3 VectorTripleProduct(Vector3 a, Vector3 b, Vector3 c)
		{
			return Cross(a, Cross(b, c));
		}

		public override string ToString() => this.ToString("G", CultureInfo.CurrentCulture);

		public string ToString(string format) => this.ToString(format, CultureInfo.CurrentCulture);

		public string ToString(string format, IFormatProvider provider)
		{
			var builder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(provider).NumberGroupSeparator;
			builder.Append('<');
			builder.Append(this.X.ToString(format, provider));
			builder.Append(numberGroupSeparator);
			builder.Append(' ');
			builder.Append(this.Y.ToString(format, provider));
			builder.Append(numberGroupSeparator);
			builder.Append(' ');
			builder.Append(this.Z.ToString(format, provider));
			builder.Append('>');
			return builder.ToString();
		}

		public static bool operator ==(Vector3 a, Vector3 b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
		}
		public static bool operator !=(Vector3 a, Vector3 b) => !(a == b);
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}
		public static Vector3 operator *(Vector3 v, float scalar)
		{
			return new Vector3(v.X * scalar, v.Y * scalar, v.Z * scalar);
		}
		public static Vector3 operator *(Vector3 v, int scalar)
		{
			return new Vector3(v.X * scalar, v.Y * scalar, v.Z * scalar);
		}
		public static Vector3 operator *(float scalar, Vector3 v) => v * scalar;
		public static Vector3 operator *(int scalar, Vector3 v) => v * scalar;
	}
}
