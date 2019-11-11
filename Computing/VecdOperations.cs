using System;

namespace IntComSys.Computing
{
	public partial struct Vecd
	{
		public static bool operator ==(Vecd left, Vecd right)
		{
			if (left.size != right.size)
				return false;

			for (int i = 0; i < left.size; i++)
			{
				if (left.elements[i] != right.elements[i])
					return false;
			}

			return true;
		}

		public static bool operator !=(Vecd left, Vecd right)
		{
			if (left.size != right.size)
				return true;

			for (int i = 0; i < left.size; i++)
			{
				if (left.elements[i] != right.elements[i])
					return true;
			}

			return false;
		}

		public static Vecd operator +(Vecd left, Vecd right)
		{
			if (left.size != right.size)
				throw new ArgumentException();

			Vecd result = new Vecd(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] + right.elements[i];
			}
			return result;
		}

		public static Vecd operator +(Vecd left, double[] right)
		{
			if (left.size != right.Length)
				throw new ArgumentException();

			Vecd result = new Vecd(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] + right[i];
			}
			return result;
		}

		public static Vecd operator +(Vecd left, double value)
		{
			Vecd result = new Vecd(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] + value;
			}
			return result;
		}

		public void Add(Vecd v)
		{
			if (size != v.size)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] += v.elements[i];
			}
		}

		public void Add(double[] v)
		{
			if (size != v.Length)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] += v[i];
			}
		}

		public void Add(double value)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] += value;
			}
		}

		public static Vecd operator -(Vecd left, Vecd right)
		{
			if (left.size != right.size)
				throw new ArgumentException();

			Vecd result = new Vecd(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] - right.elements[i];
			}
			return result;
		}

		public static Vecd operator -(double[] left, Vecd right)
		{
			if (left.Length != right.size)
				throw new ArgumentException();

			Vecd result = new Vecd(left.Length);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left[i] - right.elements[i];
			}
			return result;
		}

		public static Vecd operator -(Vecd left, double[] right)
		{
			if (left.size != right.Length)
				throw new ArgumentException();

			Vecd result = new Vecd(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] - right[i];
			}
			return result;
		}

		public static Vecd operator -(Vecd left, double value)
		{
			Vecd result = new Vecd(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] - value;
			}
			return result;
		}

		public void Sub(Vecd v)
		{
			if (size != v.size)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] -= v.elements[i];
			}
		}

		public void Sub(double[] v)
		{
			if (size != v.Length)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] -= v[i];
			}
		}

		public void Sub(double value)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] -= value;
			}
		}

		public static Vecd Mul(Vecd v, Matf m)
		{
			if (v.size != m.rows)
				throw new ArgumentException();

			Vecd result = new Vecd(m.cols);

			for (int col = 0; col < m.cols; col++)
			{
				double sum = 0;
				for (int i = 0; i < v.size; i++)
				{
					sum += v.elements[i] * m[i, col];
				}
				result.elements[col] = sum;
			}

			return result;
		}

		#region scale

		public static Vecd operator *(Vecd left, double value)
		{
			Vecd result = new Vecd(left.size);
			for (int i = 0; i < left.size; i++)
			{
				result.elements[i] = left.elements[i] * value;
			}
			return result;
		}

		public void Scale(Vecd v)
		{
			if (size != v.size)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] *= v.elements[i];
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
