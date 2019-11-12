using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computing
{
	public partial class Mat<T> where T : struct
	{
		public readonly int rows;
		public readonly int cols;
		public readonly int size;

		protected T[] elements;

		//public T this[int i]
		//{
		//	get
		//	{
		//		return elements[i];
		//	}
		//	set
		//	{
		//		elements[i] = value;
		//	}
		//}
		//public T this[int y, int x]
		//{
		//	get
		//	{
		//		return elements[y * cols + x];
		//	}
		//	set
		//	{
		//		elements[y * cols + x] = value;
		//	}
		//}

		public Mat(int n, int m)
		{
			rows = n;
			cols = m;
			size = n * m;
			elements = new T[size];
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return $"M{rows}x{cols}";
		}

		public override bool Equals(object obj)
		{
			Mat<T> m = (Mat<T>)obj;

			if (m == null)
				return false;

			return this == m;
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

		public static Mat<T> FromArray(T[,] m)
		{
			Mat<T> result = new Mat<T>(m.GetLength(0), m.GetLength(1));
			int e = 0;
			for (int i = 0; i < result.rows; i++)
			{
				for (int j = 0; j < result.cols; j++)
				{
					result.elements[e++] = m[i, j];
				}
			}
			return result;
		}

		public static Mat<T> FromArray(T[] a, int n, int m)
		{
			if (n * m != a.Length)
				throw new ArgumentException();

			Mat<T> result = new Mat<T>(n, m);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = a[i];
			}
			return result;
		}

		public Mat<T> SubMat(int y, int x)
		{
			Mat<T> result = new Mat<T>(rows - y, cols - x);
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					result.elements[i * result.cols + j] = elements[(y + i) * cols + x + j];
				}
			}
			return result;
		}

		public Mat<T> SubMat(int y, int yLen, int x, int xLen)
		{
			if (yLen > rows - y ||
				xLen > cols - x ||
				y * yLen * x * xLen < 0)
				throw new ArgumentException();

			Mat<T> result = new Mat<T>(yLen, xLen);
			for (int row = y, offset = y * cols, subRow = 0, subOffset = 0;
				row < yLen;
				row++, subRow++, offset = row * cols, subOffset = subRow * xLen)
			{
				for (int col = x; col < xLen; col++, subOffset++)
				{
					result.elements[subOffset] = elements[offset + col];
				}
			}
			return result;
		}
	}
}
