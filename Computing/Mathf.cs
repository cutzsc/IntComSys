namespace IntComSys.Computing
{
	public static class Mathf
	{
		public static float NextSingle(float min = 0, float max = 1)
		{
			return (float)Math.random.NextDouble() * (max - min) + min;
		}

		public static float Sigmoid(float x)
		{
			return (float)(1 / (1 + System.Math.Exp(-x)));
		}

		public static float SigmoidDerivative(float x)
		{
			return (x * (1 - x));
		}

		public static float Sqrt(float x)
		{
			return (float)System.Math.Sqrt(x);
		}
	}
}
