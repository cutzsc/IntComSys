using System;

namespace IntComSys.Computing
{
	public abstract class Mat<T> where T : struct
	{
		public readonly int rows;
		public readonly int cols;
		public readonly int size;
		public readonly T[] elements;

		public T this[int y, int x]
		{
			get
			{
				return elements[y * cols + x];
			}
			set
			{
				elements[y * cols + x] = value;
			}
		}

		private Mat() { }

		public Mat(int n, int m)
		{
			rows = n;
			cols = m;
			size = n * m;
			elements = new T[size];
		}

		public abstract Mat<T> Copy();

		public override string ToString()
		{
			return $"M{rows}x{cols}";
		}

		public T[,] ToArray()
		{
			T[,] result = new T[rows, cols];
			for (int y = 0; y < rows; y++)
			{
				for (int x = 0, index = y * cols; x < cols; x++, index++)
				{
					result[y, x] = elements[index];
				}
			}
			return result;
		}

		public virtual Mat<T> SubMat(int y, int x)
		{
			return SubMat(y, rows - y, x, cols - x);
		}

		public abstract Mat<T> SubMat(int row, int rows, int col, int cols);

		public abstract Mat<T> Transpose();

		public abstract T Dot(Mat<T> m1, Mat<T> m2);

		#region Get / Set

		public T[] GetRow(int row)
		{
			T[] result = new T[cols];
			for (int i = 0, offset = row * cols; i < cols; i++, offset++)
			{
				result[i] = elements[offset];
			}
			return result;
		}

		public T[] GetCol(int col)
		{
			T[] result = new T[rows];
			for (int i = 0, offset = 0; i < rows; i++, offset += cols)
			{
				result[i] = elements[offset + col];
			}
			return result;
		}

		public void SetRow(int row, Mat<T> m)
		{
			if (cols != m.cols)
				throw new ArgumentException();

			for (int i = 0, offset = row * cols; i < cols; i++, offset++)
			{
				elements[offset] = m.elements[offset];
			}
		}

		public void SetRow(int row, T[] arr)
		{
			if (cols != arr.Length)
				throw new ArgumentException();

			for (int i = 0, offset = row * cols; i < cols; i++, offset++)
			{
				elements[offset] = arr[i];
			}
		}

		public void SetCol(int col, Mat<T> m)
		{
			if (rows != m.rows)
				throw new ArgumentException();

			for (int y = 0, offset = 0; y < rows; y++, offset += cols)
			{
				elements[offset + col] = m.elements[offset + col];
			}
		}

		public void SetCol(int col, T[] arr)
		{
			if (rows != arr.Length)
				throw new ArgumentException();

			for (int y = 0, offset = 0; y < rows; y++, offset += cols)
			{
				elements[offset + col] = arr[y];
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
			for (int i = start; i < end; i++)
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
