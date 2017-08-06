using System;
using System.Collections;
using System.Collections.Generic;

namespace ComposableIndicators
{
    /// <summary>
    /// Represents a collection of the last N data points added. For example,
    /// if the window size is 10, then the first 10 points are added. When the
    /// 11th point comes in, the first point would be dropped to make room for
    /// the new data point. In this way the window 'rolls' forward as data comes
    /// in.
    /// </summary>
    /// <typeparam name="T">The data type held within the window</typeparam>
    public class RollingWindow<T> : IEnumerable<T>
    {
        private int tail;
        private T mostRecentlyRemoved;
        private readonly List<T> data;

        /// <summary>
        /// Initializes a new instance of the <see cref="RollingWindow{T}"/> class
        /// </summary>
        /// <param name="size">The size of the window</param>
        public RollingWindow(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException("RollingWindow size must be greater than or equal to 1.", nameof(size));
            }

            Size = size;
            data = new List<T>(size);
        }

        /// <summary>
        /// Gets the maximum number of data points able to be held by the window
        /// </summary>
        public int Size
        {
            get;
        }

        /// <summary>
        /// Gets the current number of data points held within the window
        /// </summary>
        public int Count
        {
            get { return data.Count; }
        }

        /// <summary>
        /// Gets the total number of samples added to this window
        /// </summary>
        public int Samples
        {
            get; private set;
        }

        /// <summary>
        /// Gets whether or not this window is full (Size == Count)
        /// </summary>
        public bool IsFull
        {
            get { return Size == Count; }
        }

        /// <summary>
        /// Gets whether or not this window has overflown, meaning that
        /// <see cref="MostRecentlyRemoved"/> can be safely called.
        /// </summary>
        public bool HasOverflowed
        {
            get { return Samples > Size; }
        }

        /// <summary>
        /// Gets the last data point that 'fell off' the window. When the window reaches
        /// capacity (Size == Count), then as new data is added, old data must be removed.
        /// This provides access to the piece of data that was just removed.
        /// </summary>
        public T MostRecentlyRemoved
        {
            get
            {
                if (!HasOverflowed)
                {
                    throw new InvalidOperationException("This window has not removed any items yet.");
                }

                return mostRecentlyRemoved;
            }
        }

        /// <summary>
        /// Gets the data point at the specified index, where index 0 is the most recently
        /// added data point.
        /// </summary>
        /// <param name="index">The index to retrieve</param>
        /// <returns>The ith most recent entry</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "The index must be greater than or equal to zero and less than Count.");
                }

                return data[(Count + tail - index - 1) % Count];
            }
        }

        /// <summary>
        /// Adds a new item to this window
        /// </summary>
        /// <param name="item">The item to be added</param>
        public void Add(T item)
        {
            Samples++;
            if (Size == Count)
            {
                mostRecentlyRemoved = data[tail];
                data[tail] = item;
                tail = (tail + 1) % Size;
            }
            else
            {
                data.Add(item);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}