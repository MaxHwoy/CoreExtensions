using CoreExtensions.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;



namespace CoreExtensions.Native
{
	/// <summary>
	/// A class with native internal buffer that stores a specific number of structure elements. Useful 
	/// for fast data processing and bypassing GC.
	/// </summary>
	/// <typeparam name="T">Type of elements to store in the array. Type should be a struct</typeparam>
	public class Array<T> : IDisposable, ICloneable, IList<T>, IList, IStructuralComparable, IStructuralEquatable where T : struct
	{
		private Pointer _pointer;
		private readonly int _sizeT;
		private bool _disposed = false;

		private Array(Array<T> other)
		{
			this._sizeT = other._sizeT;
			this.Length = other.Length;
			this._pointer = Marshal.AllocHGlobal(this.Length * this._sizeT);

			for (int i = 0, k = 0; i < this.Length; ++i, k += this._sizeT)
			{

				var obj = Marshal.PtrToStructure<T>(other._pointer + k);
				var val = obj;
				Marshal.StructureToPtr(val, this._pointer + k, false);

			}
		}

		/// <summary>
		/// Creates and initializes new instance of <see cref="Array{T}"/> with size specified and 
		/// initializes all its values to default ones.
		/// </summary>
		/// <param name="size">Size of the new array.</param>
		public Array(int size)
		{
			if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));
			this._sizeT = Marshal.SizeOf<T>();
			this.Length = size;
			this._pointer = Marshal.AllocHGlobal(this.Length * this._sizeT);
			
			for (int i = 0, k = 0; i < size; ++i, k += this._sizeT)
			{

				//var def = default(T);
				//var ptr = this._pointer + k;
				//Marshal.StructureToPtr(def, ptr, false);
				Marshal.StructureToPtr(default(T), this._pointer + k, false);

			}
		}

		/// <summary>
		/// Enumerates the elements of an <see cref="Array{T}" />.
		/// </summary>
		public class Enumerator : IEnumerator<T>
		{
			private readonly Array<T> _array;
			private int _index;

			/// <inheritdoc/>
			public T Current { get; private set; }

			/// <inheritdoc/>
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._array.Length + 1)
					{

						throw new InvalidOperationException("Enumerator index was zero or beyond length of the array");

					}

					return this.Current;
				}
			}

			internal Enumerator(Array<T> arr)
			{
				this._array = arr;
				this._index = 0;
				this.Current = default;
			}

			/// <inheritdoc/>
			public void Dispose()
			{
			}

			/// <inheritdoc/>
			public bool MoveNext()
			{
				if (this._index < this._array.Length && !this._array._disposed)
				{

					this.Current = this._array[this._index++];
					return true;

				}

				this._index = this._array.Length + 1;
				this.Current = default;
				return false;
			}

			/// <inheritdoc/>
			public void Reset()
			{
				this.Current = default;
				this._index = 0;
			}
		}

		/// <inheritdoc/>
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Length) throw new ArgumentOutOfRangeException(nameof(index));
				return Marshal.PtrToStructure<T>(this._pointer + index * this._sizeT);
			}
			set
			{
				if (index < 0 || index >= this.Length) throw new ArgumentOutOfRangeException(nameof(index));
				Marshal.StructureToPtr(value, this._pointer + index * this._sizeT, true);
			}
		}
		
		/// <inheritdoc/>
		object IList.this[int index]
		{
			get => this[index];
			set
			{
				if (value is T obj) this[index] = obj;
				else throw new InvalidCastException("Object passed is not of a valid type");
			}
		}

		/// <inheritdoc/>
		public int Count => this.Length;

		/// <inheritdoc/>
		public bool IsFixedSize => true;

		/// <inheritdoc/>
		public bool IsReadOnly => false;

		/// <inheritdoc/>
		public bool IsSynchronized => false;

		/// <summary>
		/// Gets the total number of elements in this <see cref="Array{T}"/>.
		/// </summary>
		public int Length { get; private set; }

		/// <inheritdoc/>
		public object SyncRoot => this;

		/// <inheritdoc/>
		public void Add(T item) => throw new NotSupportedException("Array has a fixed size");

		/// <inheritdoc/>
		public int Add(object value) => throw new NotSupportedException("Array has a fixed size");

		/// <inheritdoc/>
		public void Clear()
		{
			for (int i = 0, k = 0; i < this.Length; ++i, k += this._sizeT)
			{

				Marshal.StructureToPtr(default, this._pointer + k, true);

			}
		}
		
		/// <inheritdoc/>
		public object Clone()
		{
			var result = new Array<T>(this);
			return result;
		}

		/// <inheritdoc/>
		public int CompareTo(object other, IComparer comparer) => comparer.Compare(this, other);

		/// <inheritdoc/>
		public bool Contains(T item) => this.IndexOf(item) != -1;

		/// <inheritdoc/>
		public bool Contains(object value) => this.IndexOf(value) != -1;

		/// <inheritdoc/>
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array is null) throw new ArgumentNullException(nameof(array));
			if (arrayIndex < 0) throw new ArgumentOutOfRangeException("Index cannot be negative");
			if (array.Length - arrayIndex < this.Length) throw new ArgumentException("Not enough space in the array passed");

			for (int i = 0, k = 0, m = arrayIndex; i < this.Length; ++i, k += this._sizeT, ++m)
			{

				array[m] = Marshal.PtrToStructure<T>(this._pointer + k);

			}
		}

		/// <inheritdoc/>
		public void CopyTo(Array array, int index)
		{
			if (array is T[] arr) this.CopyTo(arr, index);
			else throw new ArgumentException("Type of elements in the array passed does not match type of elements in this instance");
		}

		/// <inheritdoc/>
		public void Dispose()
		{
			if (!this._disposed)
			{

				Marshal.FreeHGlobal(this._pointer);
				this._disposed = true;

			}
		}

		/// <inheritdoc/>
		public override bool Equals(object obj)
		{
			if (obj is Array<T> arr)
			{

				if (arr._disposed || this._disposed) return false;
				if (arr.Length != this.Length) return false;
				if (arr._sizeT != this._sizeT) return false;
				if (arr._pointer == this._pointer) return true;

				for (int i = 0, k = 0; i < this.Length; ++i, k += this._sizeT)
				{

					var a = Marshal.PtrToStructure<T>(this._pointer + k);
					var b = Marshal.PtrToStructure<T>(arr._pointer + k);
					if (!a.Equals(b)) return false;

				}

				return true;

			}
			
			return false;
		}

		/// <summary>
		/// Determines whether an object is structurally equal to the current instance.
		/// </summary>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <param name="comparer">An object that determines whether the current instance and <paramref name="other" /> are equal.</param>
		/// <returns>
		///   <see langword="true" /> if the two objects are equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="ArgumentNullException">
		///   <see cref="IEqualityComparer"/> passed was null.</exception>
		public bool Equals(object other, IEqualityComparer comparer)
		{
			if (comparer is null) throw new ArgumentNullException("IEqualityComparer passed was null");
			return comparer.Equals(this, other);
		}

		/// <inheritdoc/>
		public IEnumerator<T> GetEnumerator() => new Enumerator(this);

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			int result = 1492020552;

			for (int i = 0, k = 0; i < this.Length; ++i, k += this._sizeT)
			{

				result = HashCode.Combine(result, Marshal.PtrToStructure<T>(this._pointer + k).GetHashCode());

			}

			return result;
		}

		/// <summary>
		/// Returns a hash code for the current instance.
		/// </summary>
		/// <param name="comparer">An object that computes the hash code of the current object.</param>
		/// <returns>The hash code for the current instance.</returns>
		/// <exception cref="ArgumentNullException">
		///   <see cref="IEqualityComparer"/> passed was null.</exception>
		public int GetHashCode(IEqualityComparer comparer)
		{
			if (comparer is null) throw new ArgumentNullException("IEqualityComparer passed was null");
			return comparer.GetHashCode(this);
		}

		/// <inheritdoc/>
		public int IndexOf(T item)
		{
			for (int i = 0, k = 0; i < this.Length; ++i, k += this._sizeT)
			{

				var str = Marshal.PtrToStructure<T>(this._pointer + k);
				if (item.Equals(str)) return i;

			}

			return -1;
		}

		/// <inheritdoc/>
		public int IndexOf(object value)
		{
			if (value is T obj) return this.IndexOf(obj);
			else throw new InvalidCastException("Object passed is not of a valid type");
		}

		/// <inheritdoc/>
		public void Insert(int index, T item) => throw new NotSupportedException("Array has a fixed size");

		/// <inheritdoc/>
		public void Insert(int index, object value) => throw new NotSupportedException("Array has a fixed size");

		/// <inheritdoc/>
		public bool Remove(T item) => throw new NotSupportedException("Array has a fixed size");

		/// <inheritdoc/>
		public void Remove(object value) => throw new NotSupportedException("Array has a fixed size");

		/// <inheritdoc/>
		public void RemoveAt(int index) => throw new NotSupportedException("Array has a fixed size");

		/// <summary>
		/// Changes the number of elements of this <see cref="Array{T}"/> to the specified new size.
		/// </summary>
		/// <param name="newSize">The new size of the array.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		///   <paramref name="newSize" /> is less than zero.</exception>
		public void Resize(int newSize)
		{
			if (newSize < 0) throw new ArgumentOutOfRangeException(nameof(newSize));
			if (newSize == this.Length) return;
			this._pointer = Marshal.ReAllocHGlobal(this._pointer, (IntPtr)(newSize * this._sizeT));
			this.Length = newSize;
		}

		/// <summary>
		/// Returns length of this <see cref="Array{T}"/> as a string value.
		/// </summary>
		/// <returns>Length of this <see cref="Array{T}"/> as a string value.</returns>
		public override string ToString() => this.Length.ToString();
	}
}
