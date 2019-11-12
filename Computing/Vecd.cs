using System;

namespace IntComSys.Computing
{
	public partial struct Vecd
	{
		public readonly int size;

		public readonly double[] elements;

		public Vecd(int size)
		{
			this.size = size;
			elements = new double[size];
		}

		public override int GetHashCode()
		{
			return elements.GetHashCode();
		}

		public override string ToString()
		{
			return $"V({size})";
		}

		public override bool Equals(object obj)
		{
			Vecd v = (Vecd)obj;
			if (v == null ||
				size != v.size)
				return false;

			return true;
		}

		public double[] ToArray()
		{
			double[] result = new double[size];
			for (int i = 0; i < size; i++)
			{
				result[i] = elements[i];
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

		public void Map(Func<double, double> f)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] = f(elements[i]);
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

		#region set

		public void Set(double[] v)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] = v[i];
			}
		}

		public void Set(int start, double[] v)
		{
			for (int i = start; i < size; i++)
			{
				elements[i] = v[i];
			}
		}

		public void Set(int start, int length, double[] v)
		{
			for (int i = start; i < length; i++)
			{
				elements[i] = v[i];
			}
		}

		public void Set(Vecd v)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] = v.elements[i];
			}
		}

		public void Set(int start, Vecd v)
		{
			for (int i = start; i < size; i++)
			{
				elements[i] = v.elements[i];
			}
		}

		public void Set(int start, int length, Vecd v)
		{
			for (int i = start; i < length; i++)
			{
				elements[i] = v.elements[i];
			}
		}

		#endregion
	}
}
