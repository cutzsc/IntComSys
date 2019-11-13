using System;

namespace IntComSys.Computing
{
	public abstract class Vec<T> where T : struct
	{
		public readonly int size;
		public readonly T[] elements;

		private Vec() { }

		public Vec(int size)
		{
			this.size = size;
			elements = new T[size];
		}

		public abstract Vec<T> Copy();

		public override string ToString()
		{
			return $"V({size})";
		}

		public T[] ToArray()
		{
			T[] result = new T[size];
			for (int i = 0; i < size; i++)
			{
				result[i] = elements[i];
			}
			return result;
		}

		public virtual Vec<T> SubVec(int start)
		{
			return SubVec(start, size - start);
		}

		public abstract Vec<T> SubVec(int start, int length);

		public abstract T Dot(Vec<T> v1, Vec<T> v2);

		#region Set

		public void Set(T[] arr)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] = arr[i];
			}
		}

		public void Set(Vec<T> v)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] = v.elements[i];
			}
		}

		#endregion

		#region Mapping

		public void Map(Func<T> fn)
		{
			Map(0, size, fn);
		}

		public void Map(int start, Func<T> fn)
		{
			Map(start, size - start, fn);
		}

		public void Map(int start, int length, Func<T> fn)
		{
			int end = start + length;
			for (int i = 0; i < end; i++)
			{
				elements[i] = fn();
			}
		}

		public void Map(Func<T, T> fn)
		{
			Map(0, size, fn);
		}

		public void Map(int start, Func<T, T> fn)
		{
			Map(start, size - start, fn);
		}

		public void Map(int start, int length, Func<T, T> fn)
		{
			int end = start + length;
			for (int i = start; i < end; i++)
			{
				elements[i] = fn(elements[i]);
			}
		}

		#endregion
	}
}
