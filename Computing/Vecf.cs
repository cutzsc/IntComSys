using System;

namespace IntComSys.Computing
{
	public class Vecf : Vec<float>
	{
		public Vecf(int size)
			: base(size) { }

		public override Vec<float> Copy()
		{
			Vecf result = new Vecf(size);
			for (int i = 0; i < size; i++)
			{
				result.elements[i] = elements[i];
			}
			return result;
		}

		public override Vec<float> SubVec(int start)
		{
			return SubVec(start, size - start);
		}

		public override Vec<float> SubVec(int start, int length)
		{
			Vecf result = new Vecf(length);
			for (int i = 0; i < length; i++)
			{
				result.elements[i] = elements[start + i];
			}
			return result;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Vecf v = (Vecf)obj;
			if (v == null)
				return false;
			return this == v;
		}

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
			if (left.size != right.size)
				throw new ArgumentException();

			Vecf result = new Vecf(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] + right.elements[i];
			}
			return result;
		}

		public static Vecf operator +(Vecf left, float[] right)
		{
			if (left.size != right.Length)
				throw new ArgumentException();

			Vecf result = new Vecf(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] + right[i];
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
			if (size != v.size)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] += v.elements[i];
			}
		}

		public void Add(float[] arr)
		{
			if (size != arr.Length)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] += arr[i];
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
			if (left.size != right.size)
				throw new ArgumentException();

			Vecf result = new Vecf(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] - right.elements[i];
			}
			return result;
		}

		public static Vecf operator -(float[] left, Vecf right)
		{
			if (left.Length != right.size)
				throw new ArgumentException();

			Vecf result = new Vecf(left.Length);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left[i] - right.elements[i];
			}
			return result;
		}

		public static Vecf operator -(Vecf left, float[] right)
		{
			if (left.size != right.Length)
				throw new ArgumentException();

			Vecf result = new Vecf(left.size);
			for (int i = 0; i < result.size; i++)
			{
				result.elements[i] = left.elements[i] - right[i];
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
			if (size != v.size)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] -= v.elements[i];
			}
		}

		public void Sub(float[] arr)
		{
			if (size != arr.Length)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] -= arr[i];
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
			if (v.size != m.rows)
				throw new ArgumentException();

			Vecf result = new Vecf(m.cols);
			for (int x = 0; x < m.cols; x++)
			{
				float sum = 0;
				for (int y = 0, offset = 0; y < m.rows; y++, offset += m.cols)
				{
					sum += v.elements[y] * m.elements[offset + x];
				}
				result.elements[x] = sum;
			}
			return result;
		}

		#region Scale

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
			if (size != v.size)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] *= v.elements[i];
			}
		}

		public void Scale(float[] arr)
		{
			if (size != arr.Length)
				throw new ArgumentException();

			for (int i = 0; i < size; i++)
			{
				elements[i] *= arr[i];
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
