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
    public unsafe struct NativeArray<T> where T : unmanaged
    {
        private readonly T* m_pointer;

        private bool m_disposed;

        public readonly int Length;

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
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Size of allocated array should be bigger than 0");
            }

            var len = sizeof(T);
            this.m_pointer = (T*)Marshal.AllocHGlobal(len * size).ToPointer();
            this.Length = size;
            this.m_disposed = false;
        }

        /// <summary>
        /// Gets or sets element at index specified.
        /// </summary>
        /// <param name="index">Index of an element to get or set.</param>
        /// <returns>Element at index specified.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Index specified is out of range of array.</exception>
        public ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ref this.m_pointer[index];
        }

        /// <summary>
        /// Gets element at index specified.
        /// </summary>
        /// <param name="index">Index of an element to get.</param>
        /// <returns>Element at index specified.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Index specified is out of range of array.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get(int index)
        {
            if (index < 0 || index >= this.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index was out of range");
            }

            return *(this.m_pointer + index);
        }

        /// <summary>
        /// Sets element at index specified.
        /// </summary>
        /// <param name="index">Index of an element to set.</param>
        /// <param name="value">Value to set at index specified.</param>
        /// <exception cref="ArgumentOutOfRangeException">Index specified is out of range of array.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(int index, T value)
        {
            if (index < 0 || index >= this.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index was out of range");
            }

            *(this.m_pointer + index) = value;
        }

        /// <summary>
        /// Gets element at index specified without bound checks. Faster than regular <see cref="Get(int)"/>,
        /// but unsafe and can overflow length of the array. Does not throw.
        /// </summary>
        /// <param name="index">Index of an element to set.</param>
        /// <returns>Element at index specified.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetUnsafe(int index) => this.m_pointer[index];

        /// <summary>
        /// Sets element at index specified without bound checks. Faster than regular <see cref="Set(int, T)"/>,
        /// but unsafe and can overflow length of the array. Does not throw.
        /// </summary>
        /// <param name="index">Index of an element to set.</param>
        /// <param name="value">Value to set at index specified.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUnsafe(int index, T value) => this.m_pointer[index] = value;

        /// <summary>
        /// Gets pointer of type <typeparamref name="T"/> to the beginning of allocated array.
        /// </summary>
        /// <returns></returns>
        public T* GetPointer() => this.m_pointer;

        /// <summary>
        /// Frees/releases this <see cref="NativeArray{T}"/> instance. This should be called when array
        /// is not needed anymore or before it goes out of local range.
        /// </summary>
        public void Free()
        {
            if (!this.m_disposed)
            {
                Marshal.FreeHGlobal(new IntPtr(this.m_pointer));
                
                this.m_disposed = true;
            }
        }
    }
}
