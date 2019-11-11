namespace IntComSys.Computing
{
	public partial struct Vecf
	{
		public static bool operator ==(Vecf left, Vecf right)
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

		public static bool operator !=(Vecf left, Vecf right)
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

		public static Vecf operator +(Vecf left, Vecf right)
		{
			Vecf result = new Vecf(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] + right.elements[i];
			}
			return result;
		}

		public static Vecf operator +(Vecf left, float value)
		{
			Vecf result = new Vecf(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] + value;
			}
			return result;
		}

		public void Add(Vecf v)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] += v.elements[i];
			}
		}

		public void Add(float value)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] += value;
			}
		}

		public static Vecf operator -(Vecf left, Vecf right)
		{
			Vecf result = new Vecf(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] - right.elements[i];
			}
			return result;
		}

		public static Vecf operator -(float[] left, Vecf right)
		{
			Vecf result = new Vecf(left.Length);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left[i] - right.elements[i];
			}
			return result;
		}

		public static Vecf operator -(Vecf left, float value)
		{
			Vecf result = new Vecf(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] - value;
			}
			return result;
		}

		public void Sub(Vecf v)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] -= v.elements[i];
			}
		}

		public void Sub(float value)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] -= value;
			}
		}

		public static Vecf Mul(Vecf v, Matf m)
		{
			Vecf result = new Vecf(m.cols);

			for (int col = 0; col < m.cols; col++)
			{
				float sum = 0;
				for (int i = 0; i < v.size; i++)
				{
					sum += v.elements[i] * m[i, col];
				}
				result.elements[col] = sum;
			}

			return result;
		}

		#region scale

		public static Vecf operator *(Vecf left, float value)
		{
			Vecf result = new Vecf(left.size);
			for (int i = 0; i < left.size; i++)
			{
				result.elements[i] = left.elements[i] * value;
			}
			return result;
		}

		public void Scale(Vecf v)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] *= v.elements[i];
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
