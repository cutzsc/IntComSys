using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntComSys.Computing;

namespace IntComSys.AI
{
	public abstract class NeuralNetwork<T>
	{
		public readonly int[] dimension;

		public abstract T[] LastGuess { get; }

		public NeuralNetwork(params int[] dimension)
		{
			// Запомнить размерность нейронной сети
			this.dimension = new int[dimension.Length];
			for (int i = 0; i < dimension.Length; i++)
			{
				this.dimension[i] = dimension[i];
			}
		}

		public abstract void Bind(T minWeight, T maxWeight);
		public abstract T[] Predict(T[] input);
		public abstract void FeedForward(T[] input);
	}
}
