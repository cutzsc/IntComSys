﻿using System;

namespace IntComSys.Computing
{
	public partial struct Matd
	{
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

		public static bool operator ==(Matd left, Matd right)
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

		public static bool operator !=(Matd left, Matd right)
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

		public static Matd operator +(Matd left, Matd right)
		{
			if (left.rows != right.rows ||
				left.cols != right.cols)
				throw new ArgumentException();

			Matd result = new Matd(left.rows, left.cols);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] + right.elements[i];
			}
			return result;
		}

		public static Matd operator +(Matd left, double value)
		{
			Matd result = new Matd(left.rows, left.cols);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] + value;
			}
			return result;
		}

		public void Add(Matd m)
		{
			if (rows != m.rows ||
				cols != m.cols)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] += m.elements[i];
			}
		}

		public void Add(double value)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] += value;
			}
		}

		public static Matd operator -(Matd left, Matd right)
		{
			if (left.rows != right.rows ||
				left.cols != right.cols)
				throw new ArgumentException();

			Matd result = new Matd(left.rows, left.cols);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] - right.elements[i];
			}
			return result;
		}

		public static Matd operator -(Matd left, double value)
		{
			Matd result = new Matd(left.rows, left.cols);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] - value;
			}
			return result;
		}

		public void Sub(Matd m)
		{
			if (rows != m.rows ||
				cols != m.cols)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] -= m.elements[i];
			}
		}

		public void Sub(double value)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] -= value;
			}
		}

		#region mul

		public static Matd Mul(Matd left, Matd right)
		{
			if (left.cols != right.rows)
				throw new ArgumentException();

			Matd result = new Matd(left.rows, right.cols);
			for (int y = 0; y < left.rows; y++)
			{
				for (int x = 0; x < right.cols; x++)
				{
					double sum = 0;
					for (int i = 0, leftIndex = y * left.cols; i < left.cols; i++, leftIndex++)
					{
						sum += left.elements[leftIndex] * right.elements[i * right.cols + x];
					}
					result.elements[y * right.cols + x] = sum;
				}
			}
			return result;
		}

		public static Matd Mul(Vecf v, Matd m)
		{
			if (v.size != m.rows)
				throw new ArgumentException();

			Matd result = new Matd(1, m.cols);
			for (int col = 0; col < m.cols; col++)
			{
				double sum = 0;
				for (int i = 0; i < v.size; i++)
				{
					sum += v.elements[i] * m.elements[i * m.cols + col];
				}
				result.elements[col] = sum;
			}
			return result;
		}

		public static Matd Mul(Matd m, Vecf v)
		{
			if (m.cols != 1)
				throw new ArgumentException();

			Matd result = new Matd(m.rows, v.size);
			for (int row = 0; row < result.rows; row++)
			{
				for (int col = 0, index = row * result.cols; col < result.cols; col++, index++)
				{
					result.elements[index] = m.elements[row] * v.elements[col];
				}
			}
			return result;
		}

		public static Matd Mul(Vecf vertical, Vecf horizontal)
		{
			Matd result = new Matd(vertical.size, horizontal.size);
			for (int row = 0; row < result.rows; row++)
			{
				for (int col = 0, index = row * result.cols; col < result.cols; col++, index++)
				{
					result.elements[index] = vertical.elements[row] * horizontal.elements[col];
				}
			}
			return result;
		}

		#endregion

		#region scale

		public static Matd Scale(Matd m1, Matd m2)
		{
			if (m1.rows != m2.rows ||
				m1.cols != m2.cols)
				throw new ArgumentException();

			Matd m = new Matd(m1.rows, m2.cols);
			for (int i = 0; i < m.size; i++)
			{
				m.elements[i] = m1.elements[i] * m2.elements[i];
			}
			return m;
		}

		public static Matd Scale(Matd m, double value)
		{
			Matd result = new Matd(m.rows, m.cols);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = m.elements[i] * value;
			}
			return result;
		}

		public void Scale(Matd m)
		{
			if (rows != m.rows ||
				cols != m.cols)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] *= m.elements[i];
			}
		}

		public void Scale(double value)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] *= value;
			}
		}

		#endregion
	}
}
