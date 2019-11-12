﻿using System;

namespace IntComSys.Computing
{
	public partial struct Matf
	{
		public static Matf Transpose(Matf m)
		{
			Matf result = new Matf(m.cols, m.rows);
			for (int y = 0; y < m.rows; y++)
			{
				for (int x = 0, leftIndex = y * m.cols; x < m.cols; x++, leftIndex++)
				{
					result.elements[x * result.cols + y] = m.elements[leftIndex];
				}
			}
			return result;
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

		#region mul

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
					for (int i = 0, leftIndex = y * left.cols; i < left.cols; i++, leftIndex++)
					{
						sum += left.elements[leftIndex] * right.elements[i * right.cols + x];
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
			for (int col = 0; col < m.cols; col++)
			{
				float sum = 0;
				for (int i = 0; i < v.size; i++)
				{
					sum += v.elements[i] * m.elements[i * m.cols + col];
				}
				result.elements[col] = sum;
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

		#endregion

		#region scale

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
