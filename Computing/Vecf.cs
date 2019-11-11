using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntComSys.Computing
{
	public partial struct Vecf
	{
		public readonly int size;

		float[] elements;

		public float this[int index]
		{
			get { return elements[index]; }
			set { elements[index] = value; }
		}

		public Vecf(int size)
		{
			this.size = size;
			elements = new float[size];
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
			Vecf v = (Vecf)obj;
			if (v == null ||
				size != v.size)
				return false;

			return true;
		}

		public float[] ToArray()
		{
			float[] result = new float[size];
			for (int i = 0; i < size; i++)
			{
				result[i] = elements[i];
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

		public void Map(Func<float, float> f)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] = f(elements[i]);
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

		#region set

		public void Set(float[] v)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] = v[i];
			}
		}

		public void Set(int start, float[] v)
		{
			for (int i = start; i < size; i++)
			{
				elements[i] = v[i];
			}
		}

		public void Set(int start, int length, float[] v)
		{
			for (int i = start; i < length; i++)
			{
				elements[i] = v[i];
			}
		}

		public void Set(Vecf v)
		{
			for (int i = 0; i < size; i++)
			{
				elements[i] = v.elements[i];
			}
		}

		public void Set(int start, Vecf v)
		{
			for (int i = start; i < size; i++)
			{
				elements[i] = v.elements[i];
			}
		}

		public void Set(int start, int length, Vecf v)
		{
			for (int i = start; i < length; i++)
			{
				elements[i] = v.elements[i];
			}
		}

		#endregion
	}
}
