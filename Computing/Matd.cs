using System;

namespace IntComSys.Computing
{
	public partial struct Matd
	{
		public readonly int rows;
		public readonly int cols;
		public readonly int size;

		double[] elements;

		public double this[int i]
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
		public double this[int y, int x]
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

		public Matd(int n, int m)
		{
			rows = n;
			cols = m;
			size = n * m;
			elements = new double[size];
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
			Matd m = (Matd)obj;

			if (m == null)
				return false;

			return this == m;
		}

		public double[,] ToArray()
		{
			double[,] result = new double[rows, cols];
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

		public void Map(Func<double> fn)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] = fn();
			}
		}

		public void Map(int start, int length, Func<double> fn)
		{
			for (int i = start; i < length; i++)
			{
				elements[i] = fn();
			}
		}

		public void Map(Func<double, double> fn)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] = fn(elements[i]);
			}
		}

		public void Map(int start, int length, Func<double, double> fn)
		{
			for (int i = start; i < length; i++)
			{
				elements[i] = fn(elements[i]);
			}
		}

		#endregion

		#region get / set

		public double[] GetRow(int row = 0)
		{
			double[] vec = new double[cols];
			for (int col = 0, index = row * cols; col < cols; col++, index++)
			{
				vec[col] = elements[index];
			}
			return vec;
		}

		public double[] GetCol(int col = 0)
		{
			double[] vec = new double[rows];
			for (int row = 0, index = col; row < rows; row++, index = cols * row + col)
			{
				vec[row] = elements[index];
			}
			return vec;
		}

		public void SetRow(int row, double[] v)
		{
			for (int col = 0, index = row * cols; col < cols; col++, index++)
			{
				elements[index] = v[col];
			}
		}

		public void SetCol(int col, double[] v)
		{
			for (int row = 0, index = col; row < rows; row++, index = cols * row + col)
			{
				elements[index] = v[row];
			}
		}

		#endregion

		public static Matd Transpose(Matd m)
		{
			Matd result = new Matd(m.cols, m.rows);
			for (int y = 0; y < m.rows; y++)
			{
				for (int x = 0, leftIndex = y * m.cols; x < m.cols; x++, leftIndex++)
				{
					result.elements[x * result.cols + y] = m.elements[leftIndex];
				}
			}
			return result;
		}
	}
}
