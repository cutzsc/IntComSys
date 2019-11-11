using System;

namespace Computing
{
	public static class Math
	{
		public static readonly Random random;

		static Math()
		{
			random = new Random();
		}

		public static double NextDouble(double min = 0, double max = 1)
		{
			return random.NextDouble() * (max - min) + min;
		}

		public static double Sigmoid(double x)
		{
			return 1 / (1 + System.Math.Exp(-x));
		}

		public static double SigmoidDerivative(double x)
		{
			return x * (1 - x);
		}
	}
}
