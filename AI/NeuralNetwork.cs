using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntComSys.Computing;

namespace IntComSys.AI
{
	public abstract class NeuralNetwork<T, V, M>
		where T : struct
		where V : Vec<T>
		where M : Mat<T>
	{
		public readonly int[] dimension;
		public readonly int layersCount;

		protected V[] outputs;
		protected M[] weights;

		public abstract T[] LastGuess { get; }

		public NeuralNetwork(params int[] dimension)
		{
			layersCount = dimension.Length;
			this.dimension = new int[layersCount];

			for (int i = 0; i < layersCount; i++)
			{
				this.dimension[i] = dimension[i];
			}
		}

		public abstract void Bind(T minWeight, T maxWeight);

		public abstract void FeedForward(T[] input);
		public abstract T[] Predict(T[] input);

		public abstract T[] GetArrayOutputs(int layer);
		public abstract T[,] GetArrayWeights(int toLayer);
		public abstract V GetVectorOutputs(int layer);
		public abstract M GetMatrixWeights(int toLayer);
	}
}
