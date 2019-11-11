using System;

namespace IntComSys.Computing
{
	public partial struct Matf
	{
		public readonly int rows;
		public readonly int cols;
		public readonly int size;

		float[] elements;

		public float this[int i]
		{
			get
			{
				return elements[i];
			}
			set
			{
				elements[i] = value;
			}
		}
		public float this[int y, int x]
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

		public Matf(int n, int m)
		{
			rows = n;
			cols = m;
			size = n * m;
			elements = new float[size];
			for (int i = 0; i < size; i++)
			{
				elements[i] = 0;
			}
		}

		public override int GetHashCode()
		{
			return elements.GetHashCode();
		}

		public override string ToString()
		{
			return $"M{rows}x{cols}";
		}

		public override bool Equals(object obj)
		{
			Matf m = (Matf)obj;

			if (m == null)
				return false;

			return this == m;
		}

		public float[,] ToArray()
		{
			float[,] result = new float[rows, cols];
			for (int y = 0; y < rows; y++)
			{
				for (int x = 0, index = y * cols; x < cols; x++, index++)
				{
					result[y, x] = elements[index];
				}
			}
			return result;
		}

		#region map

		public void Map(Func<float> fn)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] = fn();
			}
		}

		public void Map(int start, int length, Func<float> fn)
		{
			for (int i = start; i < length; i++)
			{
				elements[i] = fn();
			}
		}

		public void Map(Func<float, float> fn)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] = fn(elements[i]);
			}
		}

		public void Map(int start, int length, Func<float, float> fn)
		{
			for (int i = start; i < length; i++)
			{
				elements[i] = fn(elements[i]);
			}
		}

		#endregion

		#region get / set

		public float[] GetRow(int row = 0)
		{
			float[] vec = new float[cols];
			for (int col = 0, index = row * cols; col < cols; col++, index++)
			{
				vec[col] = elements[index];
			}
			return vec;
		}

		public float[] GetCol(int col = 0)
		{
			float[] vec = new float[rows];
			for (int row = 0, index = col; row < rows; row++, index = cols * row + col)
			{
				vec[row] = elements[index];
			}
			return vec;
		}

		public void SetRow(int row, float[] v)
		{
			for (int col = 0, index = row * cols; col < cols; col++, index++)
			{
				elements[index] = v[col];
			}
		}

		public void SetCol(int col, float[] v)
		{
			for (int row = 0, index = col; row < rows; row++, index = cols * row + col)
			{
				elements[index] = v[row];
			}
		}

		#endregion
	}
}
