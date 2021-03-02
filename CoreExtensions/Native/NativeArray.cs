using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;



namespace CoreExtensions.Native
{
    /// <summary>
    /// An array with elements of unmanaged type provided that allocates memory on a native heap, i.e.
    /// with no GC.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerDisplay("Length = {Length}")]
    public struct NativeArray<T> where T : unmanaged
    {
        private unsafe T* _pointer;
        private bool _disposed;
        public int Length { get; }

        /// <summary>
        /// Initializes new instance of <see cref="NativeArray{T}"/> with size and element type specified.
        /// After array is about to be destroyed, <see cref="Free"/> should be called to release natively
        /// allocated buffer.
        /// </summary>
        /// <param name="size">Size of array to allocate.</param>
        /// <exception cref="ArgumentOutOfRangeException">Size of allocation passed is less or 
        /// equals zero.</exception>
        public NativeArray(int size)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size), "Size of allocated array should be bigger than 0");

            unsafe
            {

                var len = sizeof(T);
                this._pointer = (T*)Marshal.AllocHGlobal(len * size).ToPointer();
                this.Length = size;
                this._disposed = false;

            }
        }

        /// <summary>
        /// Gets or sets element at index specified.
        /// </summary>
        /// <param name="index">Index of an element to get or set.</param>
        /// <returns>Element at index specified.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Index specified is out of range of array.</exception>
        public T this[int index]
        {
            get => this.Get(index);
            set => this.Set(index, value);
        }

        /// <summary>
        /// Gets element at index specified.
        /// </summary>
        /// <param name="index">Index of an element to get.</param>
        /// <returns>Element at index specified.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Index specified is out of range of array.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe T Get(int index)
        {
            if (index < 0 || index >= this.Length) throw new ArgumentOutOfRangeException(nameof(index));
            return *(this._pointer + index);
        }

        /// <summary>
        /// Sets element at index specified.
        /// </summary>
        /// <param name="index">Index of an element to set.</param>
        /// <param name="value">Value to set at index specified.</param>
        /// <exception cref="ArgumentOutOfRangeException">Index specified is out of range of array.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Set(int index, T value)
        {
            if (index < 0 || index >= this.Length) throw new ArgumentOutOfRangeException(nameof(index));
            *(this._pointer + index) = value;
        }

        /// <summary>
        /// Gets element at index specified without bound checks. Faster than regular <see cref="Get(int)"/>,
        /// but unsafe and can overflow length of the array. Does not throw.
        /// </summary>
        /// <param name="index">Index of an element to set.</param>
        /// <returns>Element at index specified.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe T GetUnsafe(int index) => this._pointer[index];

        /// <summary>
        /// Sets element at index specified without bound checks. Faster than regular <see cref="Set(int, T)"/>,
        /// but unsafe and can overflow length of the array. Does not throw.
        /// </summary>
        /// <param name="index">Index of an element to set.</param>
        /// <param name="value">Value to set at index specified.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SetUnsafe(int index, T value) => this._pointer[index] = value;

        /// <summary>
        /// Gets pointer of type <typeparamref name="T"/> to the beginning of allocated array.
        /// </summary>
        /// <returns></returns>
        public unsafe T* GetPointer() => this._pointer;

        /// <summary>
        /// Frees/releases this <see cref="NativeArray{T}"/> instance. This should be called when array
        /// is not needed anymore or before it goes out of local range.
        /// </summary>
        public unsafe void Free()
        {
            if (!this._disposed)
            {

                Marshal.FreeHGlobal(new IntPtr(this._pointer));
                this._disposed = true;

            }
        }
    }
}
