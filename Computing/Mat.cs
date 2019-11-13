using System;

namespace IntComSys.Computing
{
	public class Mat<T> where T : struct
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

		public virtual Mat<T> Copy()
		{
			Mat<T> result = new Mat<T>(rows, cols);
			for (int i = 0; i < size; i++)
			{
				result.elements[i] = elements[i];
			}
			return result;
		}

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

		public static Mat<T> FromArray(T[,] arr)
		{
			Mat<T> result = new Mat<T>(arr.GetLength(0), arr.GetLength(1));
			int e = 0;
			for (int i = 0; i < result.rows; i++)
			{
				for (int j = 0; j < result.cols; j++)
				{
					result.elements[e++] = arr[i, j];
				}
			}
			return result;
		}

		public static Mat<T> FromArray(T[] arr, int n, int m)
		{
			if (n * m != arr.Length)
				throw new ArgumentException();

			Mat<T> result = new Mat<T>(n, m);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = arr[i];
			}
			return result;
		}

		public virtual Mat<T> SubMat(int y, int x)
		{
			return SubMat(y, rows - y, x, cols - x);
		}

		public virtual Mat<T> SubMat(int row, int rows, int col, int cols)
		{
			Mat<T> result = new Mat<T>(rows, cols);
			int endRow = row + rows;
			int endCol = col + cols;
			for (int y = row, res_y = 0; y < endRow; y++, res_y++)
			{
				for (int x = col, res_index = res_y * cols, index = y * this.cols;
					x < endCol;
					x++, res_index++)
				{
					result.elements[res_index] = elements[index + x];
				}
			}
			return result;
		}

		public virtual Mat<T> Transpose()
		{
			Mat<T> result = new Mat<T>(cols, rows);
			for (int y = 0, index = 0; y < rows; y++, index += cols)
			{
				for (int x = 0, res_index = 0; x < cols; x++, res_index += rows)
				{
					result.elements[res_index + y] = elements[index + x];
				}
			}
			return result;
		}

		#region Get / Set

		public void GetRow(int row, out Mat<T> m)
		{
			Mat<T> result = new Mat<T>(1, cols);
			for (int i = 0, offset = row * cols; i < cols; i++, offset++)
			{
				result.elements[i] = elements[offset];
			}
			m = result;
		}

		public void GetRow(int row, out T[] arr)
		{
			T[] result = new T[cols];
			for (int i = 0, offset = row * cols; i < cols; i++, offset++)
			{
				result[i] = elements[offset];
			}
			arr = result;
		}

		public void GetCol(int col, out Mat<T> m)
		{
			Mat<T> result = new Mat<T>(rows, 1);
			for (int i = 0, offset = 0; i < rows; i++, offset += cols)
			{
				result.elements[i] = elements[offset + col];
			}
			m = result;
		}

		public void GetCol(int col, out T[] arr)
		{
			T[] result = new T[rows];
			for (int i = 0, offset = 0; i < rows; i++, offset += cols)
			{
				result[i] = elements[offset + col];
			}
			arr = result;
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
