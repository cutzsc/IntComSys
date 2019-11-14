using System;
using System.Threading.Tasks;

namespace IntComSys.Computing
{
	public class Matf : Mat<float>
	{
		public Matf(int n, int m)
			: base(n, m) { }

		public static Matf FromArray(float[,] arr)
		{
			Matf result = new Matf(arr.GetLength(0), arr.GetLength(1));
			for (int i = 0, e = 0; i < result.rows; i++)
			{
				for (int j = 0; j < result.cols; j++)
				{
					result.elements[e++] = arr[i, j];
				}
			}
			return result;
		}

		public static Matf FromArray(float[] arr, int n, int m)
		{
			if (n * m != arr.Length)
				throw new ArgumentException();

			Matf result = new Matf(n, m);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = arr[i];
			}
			return result;
		}

		public override Mat<float> Copy()
		{
			Matf result = new Matf(rows, cols);
			for (int i = 0; i < size; i++)
			{
				result.elements[i] = elements[i];
			}
			return result;
		}

		public override Mat<float> SubMat(int row, int rows, int col, int cols)
		{
			Matf result = new Matf(rows, cols);
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

		public override Mat<float> Transpose()
		{
			Matf result = new Matf(cols, rows);
			for (int y = 0, index = 0; y < rows; y++, index += cols)
			{
				for (int x = 0, res_index = 0; x < cols; x++, res_index += rows)
				{
					result.elements[res_index + y] = elements[index + x];
				}
			}
			return result;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Matf m = (Matf)obj;

			if (m == null)
				return false;

			return this == m;
		}

		public override float Dot(Mat<float> m1, Mat<float> m2)
		{
			if (m1.rows != m2.rows ||
				m1.cols != m2.cols)
				throw new ArgumentException();

			float sum = 0;
			for (int i = 0; i < size; i++)
			{
				sum += m1.elements[i] * m2.elements[i];
			}
			return sum;
		}

		public static bool operator ==(Matf left, Matf right)
		{
			if (left.rows != right.rows ||
				left.cols != right.cols)
				return false;

			for (int i = 0; i < left.size; i++)
			{
				if (left.elements[i] != right.elements[i])
					return false;
			}
			return true;
		}

		public static bool operator !=(Matf left, Matf right)
		{
			if (left.rows != right.rows ||
				left.cols != right.cols)
				return true;

			for (int i = 0; i < left.size; i++)
			{
				if (left.elements[i] != right.elements[i])
					return true;
			}
			return false;
		}

		public static Matf operator +(Matf left, Matf right)
		{
			if (left.rows != right.rows ||
				left.cols != right.cols)
				throw new ArgumentException();

			Matf result = new Matf(left.rows, left.cols);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] + right.elements[i];
			}
			return result;
		}

		public static Matf operator +(Matf left, float value)
		{
			Matf result = new Matf(left.rows, left.cols);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] + value;
			}
			return result;
		}

		public void Add(Matf m)
		{
			if (rows != m.rows ||
				cols != m.cols)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] += m.elements[i];
			}
		}

		public void Add(float value)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] += value;
			}
		}

		public static Matf operator -(Matf left, Matf right)
		{
			if (left.rows != right.rows ||
				left.cols != right.cols)
				throw new ArgumentException();

			Matf result = new Matf(left.rows, left.cols);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] - right.elements[i];
			}
			return result;
		}

		public static Matf operator -(Matf left, float value)
		{
			Matf result = new Matf(left.rows, left.cols);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] - value;
			}
			return result;
		}

		public void Sub(Matf m)
		{
			if (rows != m.rows ||
				cols != m.cols)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] -= m.elements[i];
			}
		}

		public void Sub(float value)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] -= value;
			}
		}

		#region Multiplication

		public static Matf Mul(Matf left, Matf right)
		{
			if (left.cols != right.rows)
				throw new ArgumentException();

			Matf result = new Matf(left.rows, right.cols);
			for (int y = 0; y < left.rows; y++)
			{
				for (int x = 0; x < right.cols; x++)
				{
					float sum = 0;
					for (int i = 0, left_index = y * left.cols; i < left.cols; i++, left_index++)
					{
						sum += left.elements[left_index] * right.elements[i * right.cols + x];
					}
					result.elements[y * right.cols + x] = sum;
				}
			}
			return result;
		}

		public static Matf Mul(Vecf v, Matf m)
		{
			if (v.size != m.rows)
				throw new ArgumentException();

			Matf result = new Matf(1, m.cols);
			for (int x = 0; x < m.cols; x++)
			{
				float sum = 0;
				for (int i = 0; i < v.size; i++)
				{
					sum += v.elements[i] * m.elements[i * m.cols + x];
				}
				result.elements[x] = sum;
			}
			return result;
		}

		public static Matf Mul(Matf m, Vecf v)
		{
			if (m.cols != 1)
				throw new ArgumentException();

			Matf result = new Matf(m.rows, v.size);
			for (int row = 0; row < result.rows; row++)
			{
				for (int col = 0, index = row * result.cols; col < result.cols; col++, index++)
				{
					result.elements[index] = m.elements[row] * v.elements[col];
				}
			}
			return result;
		}

		public static Matf Mul(Vecf vertical, Vecf horizontal)
		{
			Matf result = new Matf(vertical.size, horizontal.size);
			for (int row = 0; row < result.rows; row++)
			{
				for (int col = 0, index = row * result.cols; col < result.cols; col++, index++)
				{
					result.elements[index] = vertical.elements[row] * horizontal.elements[col];
				}
			}
			return result;
		}

		public static Matf ParallelMul(Vecf vertical, Vecf horizontal)
		{
			Matf result = new Matf(vertical.size, horizontal.size);

			Parallel.For(0, result.rows, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, (row) =>
			{
				for (int col = 0, index = row * result.cols; col < result.cols; col++, index++)
				{
					result.elements[index] = vertical.elements[row] * horizontal.elements[col];
				}
			});
			return result;
		}

		#endregion

		#region Scale

		public static Matf Scale(Matf m1, Matf m2)
		{
			if (m1.rows != m2.rows ||
				m1.cols != m2.cols)
				throw new ArgumentException();

			Matf m = new Matf(m1.rows, m2.cols);
			for (int i = 0; i < m.size; i++)
			{
				m.elements[i] = m1.elements[i] * m2.elements[i];
			}
			return m;
		}

		public static Matf Scale(Matf m, float value)
		{
			Matf result = new Matf(m.rows, m.cols);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = m.elements[i] * value;
			}
			return result;
		}

		public void Scale(Matf m)
		{
			if (rows != m.rows ||
				cols != m.cols)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] *= m.elements[i];
			}
		}

		public void Scale(float value)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] *= value;
			}
		}

		#endregion
	}
}
