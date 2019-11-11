namespace Computing
{
	public partial struct Matf
	{
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
			Matf result = new Matf(1, m.cols);
			for (int col = 0; col < m.cols; col++)
			{
				float sum = 0;
				for (int i = 0; i < v.size; i++)
				{
					sum += v[i] * m.elements[i * m.cols + col];
				}
				result.elements[col] = sum;
			}
			return result;
		}

		public static Matf Mul(Matf m, Vecf v)
		{
			Matf result = new Matf(m.rows, v.size);
			for (int row = 0; row < result.rows; row++)
			{
				for (int col = 0, index = row * result.cols; col < result.cols; col++, index++)
				{
					result.elements[index] = m.elements[row] * v[col];
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
					result.elements[index] = vertical[row] * horizontal[col];
				}
			}
			return result;
		}

		#endregion

		#region scale

		public static Matf Scale(Matf m1, Matf m2)
		{
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
