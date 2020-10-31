using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;



namespace CoreExtensions.Types
{
	/// <summary>
	/// A universal pointer type that is implicitly convertible to and from any primitive type.
	/// </summary>
	public struct Pointer : ISerializable
	{
		private readonly unsafe void* _ptr;
		private unsafe int Address => (int)this._ptr;

		/// <summary>
		/// A <see langword="static"/> <see cref="Pointer"/> value with address 0.
		/// </summary>
		public static readonly Pointer Zero;

		/// <summary>
		/// Gets the size of this instance, which is 4 bytes.
		/// </summary>
		public static int Size => 4;

		/// <summary>
		/// Checks whether pointer of this instance is 0.
		/// </summary>
		/// <returns><see langword="true"/> if pointer of this instance is 0; otherwise, 
		/// <see langword="false"/>.</returns>
		public bool IsNull() => this.Address == 0;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the pointer specified.
		/// </summary>
		/// <param name="ptr">A pointer to an unspecified type.</param>
		public unsafe Pointer(void* ptr) => this._ptr = ptr;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the pointer specified.
		/// </summary>
		/// <param name="ptr">A pointer to an boolean type.</param>
		public unsafe Pointer(bool* ptr) => this._ptr = ptr;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the pointer specified.
		/// </summary>
		/// <param name="ptr">A pointer to an unsigned 8-bit integer type.</param>
		public unsafe Pointer(byte* ptr) => this._ptr = ptr;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the pointer specified.
		/// </summary>
		/// <param name="ptr">A pointer to a signed 8-bit integer type.</param>
		public unsafe Pointer(sbyte* ptr) => this._ptr = ptr;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the pointer specified.
		/// </summary>
		/// <param name="ptr">A pointer to a signed 16-bit integer type.</param>
		public unsafe Pointer(short* ptr) => this._ptr = ptr;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the pointer specified.
		/// </summary>
		/// <param name="ptr">A pointer to an unsigned 16-bit integer type.</param>
		public unsafe Pointer(ushort* ptr) => this._ptr = ptr;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the pointer specified.
		/// </summary>
		/// <param name="ptr">A pointer to a signed 32-bit integer type.</param>
		public unsafe Pointer(int* ptr) => this._ptr = ptr;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the pointer specified.
		/// </summary>
		/// <param name="ptr">A pointer to an unsigned 32-bit integer type.</param>
		public unsafe Pointer(uint* ptr) => this._ptr = ptr;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the pointer specified.
		/// </summary>
		/// <param name="ptr">A pointer to a signed 64-bit integer type.</param>
		public unsafe Pointer(long* ptr) => this._ptr = ptr;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the pointer specified.
		/// </summary>
		/// <param name="ptr">A pointer to an unsigned 64-bit integer type.</param>
		public unsafe Pointer(ulong* ptr) => this._ptr = ptr;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the pointer specified.
		/// </summary>
		/// <param name="ptr">A pointer to a 32-bit floating point type.</param>
		public unsafe Pointer(float* ptr) => this._ptr = ptr;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the pointer specified.
		/// </summary>
		/// <param name="ptr">A pointer to a 64-bit floating point type.</param>
		public unsafe Pointer(double* ptr) => this._ptr = ptr;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the pointer specified.
		/// </summary>
		/// <param name="ptr">A pointer to a 128-bit floating point type.</param>
		public unsafe Pointer(decimal* ptr) => this._ptr = ptr;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the <see cref="IntPtr"/> specified.
		/// </summary>
		/// <param name="ptr"><see cref="IntPtr"/> to initialize with.</param>
		public unsafe Pointer(IntPtr ptr) => this._ptr = ptr.ToPointer();

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the <see cref="UIntPtr"/> specified.
		/// </summary>
		/// <param name="ptr"><see cref="UIntPtr"/> to initialize with.</param>
		public unsafe Pointer(UIntPtr ptr) => this._ptr = ptr.ToPointer();

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the specified 32-bit 
		/// signed pointer or handle.
		/// </summary>
		/// <param name="address">A pointer or handle contained in a 32-bit signed integer.</param>
		public unsafe Pointer(int address) => this._ptr = (void*)address;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the specified 32-bit 
		/// unsigned pointer or handle.
		/// </summary>
		/// <param name="address">A pointer or handle contained in a 32-bit unsigned integer.</param>
		public unsafe Pointer(uint address) => this._ptr = (void*)address;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the specified 64-bit 
		/// signed pointer or handle.
		/// </summary>
		/// <param name="address">A pointer or handle contained in a 64-bit signed integer.</param>
		public unsafe Pointer(long address) => this._ptr = (void*)address;

		/// <summary>
		/// Initializes a new instance of <see cref="Pointer"/> using the specified 64-bit 
		/// unsigned pointer or handle.
		/// </summary>
		/// <param name="address">A pointer or handle contained in a 64-bit unsigned integer.</param>
		public unsafe Pointer(ulong address) => this._ptr = (void*)address;

		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> object with the data needed to serialize 
		/// the current <see cref="Pointer"/> object.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> object to populate with data.</param>
		/// <param name="context">The destination for this serialization. (This parameter is 
		/// not used; specify <see langword="null" />.)</param>
		public unsafe void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{

				throw new ArgumentNullException(nameof(info));

			}

			info.AddValue("value", (int)this._ptr);
		}

		/// <summary>
		/// Returns a value indicating whether this instance is equal to a specified object.
		/// </summary>
		/// <param name="obj">An object to compare with this instance or <see langword="null" />.</param>
		/// <returns><see langword="true"/> if object is an instance of <see cref="Pointer"/> 
		/// and equals the value of this instance; otherwise, <see langword="false"/>.</returns>
		public override unsafe bool Equals(object obj) => obj is Pointer ptr && ptr._ptr == this._ptr;

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override unsafe int GetHashCode() => this.Address;

		/// <summary>
		/// Converts the numeric value of the current <see cref="Pointer"/> object to its 
		/// equivalent string representation.
		/// </summary>
		/// <returns>The string representation of the value of this instance.</returns>
		public override unsafe string ToString() => this.Address.ToString(CultureInfo.InvariantCulture);

		/// <summary>
		/// Converts the numeric value of the current <see cref="Pointer"/> object to its 
		/// equivalent string representation.
		/// </summary>
		/// <param name="format">A format specification that governs how the current 
		/// <see cref="Pointer"/> object is converted.</param>
		/// <returns>The string representation of the value of the current <see cref="Pointer"/> object.</returns>
		public unsafe string ToString(string format) => this.Address.ToString(format, CultureInfo.InvariantCulture);

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a pointer of unspecified type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator void*(Pointer ptr) => ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a pointer of boolean type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator bool*(Pointer ptr) => (bool*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a pointer of 8-bit unsigned integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator byte*(Pointer ptr) => (byte*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a pointer of 8-bit signed integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator sbyte*(Pointer ptr) => (sbyte*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a pointer of 16-bit signed integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator short*(Pointer ptr) => (short*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a pointer of 16-bit unsigned integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator ushort*(Pointer ptr) => (ushort*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a pointer of 32-bit signed integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator int*(Pointer ptr) => (int*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a pointer of 32-bit unsigned integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator uint*(Pointer ptr) => (uint*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a pointer of 64-bit signed integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator long*(Pointer ptr) => (long*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a pointer of 64-bit unsigned integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator ulong*(Pointer ptr) => (ulong*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a pointer of 32-bit floating point type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator float*(Pointer ptr) => (float*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a pointer of 64-bit floating point type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator double*(Pointer ptr) => (double*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a pointer of 128-bit floating point type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator decimal*(Pointer ptr) => (decimal*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a boolean type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator bool(Pointer ptr) => *(bool*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to an 8-bit unsigned integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator byte(Pointer ptr) => *(byte*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to an 8-bit signed integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator sbyte(Pointer ptr) => *(sbyte*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a 16-bit signed integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator short(Pointer ptr) => *(short*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a 16-bit unsigned integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator ushort(Pointer ptr) => *(ushort*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a 32-bit signed integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator int(Pointer ptr) => *(int*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a 32-bit unsigned integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator uint(Pointer ptr) => *(uint*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a 64-bit signed integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator long(Pointer ptr) => *(long*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a 64-bit unsigned integer type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator ulong(Pointer ptr) => *(ulong*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a 32-bit floating point type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator float(Pointer ptr) => *(float*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a 64-bit floating point type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator double(Pointer ptr) => *(double*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> instance to a 128-bit floating point type.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator decimal(Pointer ptr) => *(decimal*)ptr._ptr;

		/// <summary>
		/// Converts this <see cref="Pointer"/> to a <see cref="IntPtr"/> instance.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator IntPtr(Pointer ptr) => new IntPtr(ptr._ptr);

		/// <summary>
		/// Converts this <see cref="Pointer"/> to a <see cref="UIntPtr"/> instance.
		/// </summary>
		/// <param name="ptr"><see cref="Pointer"/> to convert.</param>
		public static unsafe implicit operator UIntPtr(Pointer ptr) => new UIntPtr(ptr._ptr);

		/// <summary>
		/// Converts pointer to an unspecified type to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr">Pointer to convert.</param>
		public static unsafe implicit operator Pointer(void* ptr) => new Pointer(ptr);

		/// <summary>
		/// Converts pointer to a boolean type to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr">Pointer to convert.</param>
		public static unsafe implicit operator Pointer(bool* ptr) => new Pointer(ptr);

		/// <summary>
		/// Converts pointer to an 8-bit unsigned integer type to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr">Pointer to convert.</param>
		public static unsafe implicit operator Pointer(byte* ptr) => new Pointer(ptr);

		/// <summary>
		/// Convert pointer to an 8-bit signed integer to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr">Pointer to convert.</param>
		public static unsafe implicit operator Pointer(sbyte* ptr) => new Pointer(ptr);

		/// <summary>
		/// Convert pointer to a 16-bit signed integer type to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr">Pointer to convert.</param>
		public static unsafe implicit operator Pointer(short* ptr) => new Pointer(ptr);

		/// <summary>
		/// Convert pointer to a 16-bit unsigned integer type to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr">Pointer to convert.</param>
		public static unsafe implicit operator Pointer(ushort* ptr) => new Pointer(ptr);

		/// <summary>
		/// Convert pointer to a 32-bit signed integer type to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr">Pointer to convert.</param>
		public static unsafe implicit operator Pointer(int* ptr) => new Pointer(ptr);

		/// <summary>
		/// Convert pointer to a 32-bit unsigned integer type to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr">Pointer to convert.</param>
		public static unsafe implicit operator Pointer(uint* ptr) => new Pointer(ptr);

		/// <summary>
		/// Convert pointer to a 64-bit signed integer type to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr">Pointer to convert.</param>
		public static unsafe implicit operator Pointer(long* ptr) => new Pointer(ptr);

		/// <summary>
		/// Convert pointer to a 64-bit unsigned integer type to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr">Pointer to convert.</param>
		public static unsafe implicit operator Pointer(ulong* ptr) => new Pointer(ptr);

		/// <summary>
		/// Convert pointer to a 32-bit floating point type to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr">Pointer to convert.</param>
		public static unsafe implicit operator Pointer(float* ptr) => new Pointer(ptr);

		/// <summary>
		/// Convert pointer to a 64-bit floating point type to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr">Pointer to convert.</param>
		public static unsafe implicit operator Pointer(double* ptr) => new Pointer(ptr);

		/// <summary>
		/// Convert pointer to a 128-bit floating point type to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr">Pointer to convert.</param>
		public static unsafe implicit operator Pointer(decimal* ptr) => new Pointer(ptr);

		/// <summary>
		/// Converts <see cref="IntPtr"/> instance to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr"><see cref="IntPtr"/> to convert.</param>
		public static unsafe implicit operator Pointer(IntPtr ptr) => new Pointer(ptr);

		/// <summary>
		/// Converts <see cref="UIntPtr"/> instance to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="ptr"><see cref="UIntPtr"/> to convert.</param>
		public static unsafe implicit operator Pointer(UIntPtr ptr) => new Pointer(ptr);

		/// <summary>
		/// Converts a 32-bit signed integer address to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="address">Address to convert.</param>
		public static unsafe implicit operator Pointer(int address) => new Pointer(address);

		/// <summary>
		/// Converts a 32-bit unsigned integer address to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="address">Address to convert.</param>
		public static unsafe implicit operator Pointer(uint address) => new Pointer(address);

		/// <summary>
		/// Converts a 64-bit signed integer address to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="address">Address to convert.</param>
		public static unsafe implicit operator Pointer(long address) => new Pointer(address);

		/// <summary>
		/// Converts a 64-bit unsigned integer address to a <see cref="Pointer"/> instance.
		/// </summary>
		/// <param name="address">Address to convert.</param>
		public static unsafe implicit operator Pointer(ulong address) => new Pointer(address);

		/// <summary>
		/// Determines whether two specified instances of <see cref="Pointer"/> are equal.
		/// </summary>
		/// <param name="ptr1">The first pointer or handle to compare.</param>
		/// <param name="ptr2">The second pointer or handle to compare.</param>
		/// <returns><see langword="true"/> if first pointer equals second pointer; 
		/// otherwise, <see langword="false" />.</returns>
		public static unsafe bool operator ==(Pointer ptr1, Pointer ptr2) => ptr1._ptr == ptr2._ptr;

		/// <summary>
		/// Determines whether two specified instances of <see cref="Pointer"/> are not equal.
		/// </summary>
		/// <param name="ptr1">The first pointer or handle to compare.</param>
		/// <param name="ptr2">The second pointer or handle to compare.</param>
		/// <returns><see langword="true"/> if first pointer does not equal second pointer; 
		/// otherwise, <see langword="false" />.</returns>
		public static unsafe bool operator !=(Pointer ptr1, Pointer ptr2) => ptr1._ptr != ptr2._ptr;

		/// <summary>
		/// Adds an offset to the value of a pointer.
		/// </summary>
		/// <param name="ptr">The pointer to add the address to.</param>
		/// <param name="address">The address to add.</param>
		/// <returns>A new pointer that reflects the addition of address to pointer.</returns>
		public static unsafe Pointer operator +(Pointer ptr, int address)
		{
			return new Pointer(ptr.Address + address);
		}

		/// <summary>
		/// Adds an offset to the value of a pointer.
		/// </summary>
		/// <param name="ptr">The pointer to add the address to.</param>
		/// <param name="address">The address to add.</param>
		/// <returns>A new pointer that reflects the addition of address to pointer.</returns>
		public static unsafe Pointer operator +(Pointer ptr, uint address)
		{
			return new Pointer((uint)ptr.Address + address);
		}

		/// <summary>
		/// Adds an offset to the value of a pointer.
		/// </summary>
		/// <param name="ptr">The pointer to add the address to.</param>
		/// <param name="address">The address to add.</param>
		/// <returns>A new pointer that reflects the addition of address to pointer.</returns>
		public static unsafe Pointer operator +(Pointer ptr, long address)
		{
			return new Pointer((long)ptr.Address + address);
		}

		/// <summary>
		/// Adds an offset to the value of a pointer.
		/// </summary>
		/// <param name="ptr">The pointer to add the address to.</param>
		/// <param name="address">The address to add.</param>
		/// <returns>A new pointer that reflects the addition of address to pointer.</returns>
		public static unsafe Pointer operator +(Pointer ptr, ulong address)
		{
			return new Pointer((ulong)ptr.Address + address);
		}

		/// <summary>
		/// Subtracts an address from the value of a pointer.
		/// </summary>
		/// <param name="ptr">The pointer to subtract the offset from.</param>
		/// <param name="address">The address to subtract.</param>
		/// <returns>A new pointer that reflects the subtraction of address from pointer.</returns>
		public static unsafe Pointer operator -(Pointer ptr, int address)
		{
			return new Pointer(ptr.Address - address);
		}

		/// <summary>
		/// Subtracts an address from the value of a pointer.
		/// </summary>
		/// <param name="ptr">The pointer to subtract the offset from.</param>
		/// <param name="address">The address to subtract.</param>
		/// <returns>A new pointer that reflects the subtraction of address from pointer.</returns>
		public static unsafe Pointer operator -(Pointer ptr, uint address)
		{
			return new Pointer((uint)ptr.Address - address);
		}

		/// <summary>
		/// Subtracts an address from the value of a pointer.
		/// </summary>
		/// <param name="ptr">The pointer to subtract the offset from.</param>
		/// <param name="address">The address to subtract.</param>
		/// <returns>A new pointer that reflects the subtraction of address from pointer.</returns>
		public static unsafe Pointer operator -(Pointer ptr, long address)
		{
			return new Pointer((long)ptr.Address - address);
		}

		/// <summary>
		/// Subtracts an address from the value of a pointer.
		/// </summary>
		/// <param name="ptr">The pointer to subtract the offset from.</param>
		/// <param name="address">The address to subtract.</param>
		/// <returns>A new pointer that reflects the subtraction of address from pointer.</returns>
		public static unsafe Pointer operator -(Pointer ptr, ulong address)
		{
			return new Pointer((ulong)ptr.Address - address);
		}
	}
}
