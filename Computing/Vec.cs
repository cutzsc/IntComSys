using System;

namespace IntComSys.Computing
{
	public class Vec<T> where T : struct
	{
		public readonly int size;
		public readonly T[] elements;

		private Vec() { }

		public Vec(int size)
		{
			this.size = size;
			elements = new T[size];
		}

		public virtual Vec<T> Copy()
		{
			Vec<T> result = new Vec<T>(size);
			for (int i = 0; i < size; i++)
			{
				result.elements[i] = elements[i];
			}
			return result;
		}

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

		public static Vec<T> FromArray(T[] arr)
		{
			Vec<T> result = new Vec<T>(arr.Length);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = arr[i];
			}
			return result;
		}

		public virtual Vec<T> SubVec(int start)
		{
			return SubVec(start, size - start);
		}

		public virtual Vec<T> SubVec(int start, int length)
		{
			Vec<T> result = new Vec<T>(length);
			for (int i = 0; i < length; i++)
			{
				result.elements[i] = elements[start + i];
			}
			return result;
		}

		#region Set

		public void Set(T[] arr)
		{
			Set(0, size, arr);
		}

		public void Set(int start, T[] arr)
		{
			Set(start, size - start, arr);
		}

		/// <summary>
		/// Set value for each element from "start" to "start + length".
		/// </summary>
		public void Set(int start, int length, T[] arr)
		{
			int end = start + length;
			for (int i = start, j = 0; i < end; i++, j++)
			{
				elements[start + i] = arr[j];
			}
		}

		public void Set(Vec<T> v)
		{
			Set(0, size, v);
		}

		public void Set(int start, Vec<T> v)
		{
			Set(start, size - start, v);
		}

		/// <summary>
		/// Set value for each element from "start" to "start + length".
		/// </summary>
		public void Set(int start, int length, Vec<T> v)
		{
			int end = start + length;
			for (int i = start, j = 0; i < end; i++, j++)
			{
				elements[start + i] = v.elements[j];
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
