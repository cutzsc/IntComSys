using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntComSys.Computing;

namespace IntComSys.AI
{
	public class FeedForwardNetwork : NeuralNetwork<float, Vecf, Matf>
	{
		public Vecf[] gradients;
		public Matf[] deltaWeights;

		public override float[] LastGuess => outputs[layersCount - 1].ToArray();

		public FeedForwardNetwork(params int[] dimension)
			: base(dimension)
		{
			// Определить количество слоев, для выходных значений
			outputs = new Vecf[dimension.Length];
			// Количество солев кроме первого, так как в Input Layer не нужны градиенты для gradient descent
			gradients = new Vecf[dimension.Length - 1];
			// Количество связей на слоях кроме последнего
			weights = new Matf[dimension.Length - 1];
			// Количество дельта связей на слоях кроме последнего
			deltaWeights = new Matf[dimension.Length - 1];
		}

		public override void Bind(float minWeight, float maxWeight)
		{
			for (int i = 0; i < dimension.Length - 1; i++)
			{
				// Установить количество выходных значений (нейронов) для i-го слоя, +1 нейрон для bias
				outputs[i] = new Vecf(dimension[i] + 1);
				// Установка значения для bias
				outputs[i].elements[dimension[i]] = 1;
				// Количество градиентов соответствует количество нейронов на i-ом слое, кроме bias
				// Так как bias не имеет входных связей
				gradients[i] = new Vecf(dimension[i + 1]);
				// Установка количества связей для i-го слоя
				// Матрица будет иметь dimension[i]+1 строк (кол-во нейронов +1 для bias) и dimension[i+1] столбцов (кол-во нейронов на следующем слое)
				weights[i] = new Matf(dimension[i] + 1, dimension[i + 1]);
				// Метод Map вызовит для каждого элемента матрицы функцию возвращающую случайно значение в пределах [minWeight, maxWeight)
				weights[i].Map(() => Mathf.NextSingle(minWeight, maxWeight));
				// Принцип такой же как и с weights, только все значения устанавливаются в 0
				deltaWeights[i] = new Matf(dimension[i] + 1, dimension[i + 1]);
			}
			// Установка количества нейронов для последнего слоя, без bias
			outputs[dimension.Length - 1] = new Vecf(dimension[dimension.Length - 1]);
		}

		public override void FeedForward(float[] inputs)
		{
			// Количество входных нейронов должно соответсвовать количеству входных значений
			if (dimension[0] != inputs.Length)
				throw new ArgumentException();

			// Установить начальные значения для входного слоя
			for (int i = 0; i < dimension[0]; i++)
			{
				outputs[0].elements[i] = inputs[i];
			}

			for (int i = 0; i < outputs.Length - 1; i++)
			{
				// Расчитываем выходные значения для первого после входного слоя
				Vecf res = Vecf.ParallelMul(outputs[i], weights[i]);
				for (int j = 0; j < res.size; j++)
				{
					outputs[i + 1].elements[j] = res.elements[j];
				}
				// Применяем функцию активации для каждого выходного значения
				outputs[i + 1].Map(0, dimension[i + 1], Mathf.Sigmoid);
			}
		}

		public override float[] Predict(float[] inputs)
		{
			FeedForward(inputs);
			return outputs[outputs.Length - 1].ToArray();
		}

		public void BackPropagation(float[] targets, float eta, float alpha)
		{
			// Расчитываем градиенты для выходного слоя
			// Запоминаем выходные значения в вектор градиентов
			gradients[layersCount - 2].Set(outputs[layersCount - 1]);
			// Для выходных значений применяем производную функцию активации
			gradients[layersCount - 2].Map(Mathf.SigmoidDerivative);
			// Умножаем получившееся значения на разницу между искомым и нужным результатами
			gradients[layersCount - 2].Scale(targets - outputs[layersCount - 1]);

			// Пересчитываем силы связей
			// Находим momentum
			Matf momentum = Matf.Scale(deltaWeights[layersCount - 2], alpha);
			// Расчитываем новые веса, eta * output * gradient
			// eta - learning rate, output - значение нейрона от которого идет связь, gradient - градиент нейрона к которому идет связь
			deltaWeights[layersCount - 2] = Matf.ParallelMul(outputs[layersCount - 2], gradients[layersCount - 2]);
			deltaWeights[layersCount - 2].Scale(eta);
			// Прибавляем momentum rate к learning rate
			deltaWeights[deltaWeights.Length - 1].Add(momentum);
			// Обновляем веса
			weights[layersCount - 2].Add(deltaWeights[layersCount - 2]);

			for (int i = layersCount - 3; i >= 0; i--)
			{
				// Расчитываем градиенты для H слоя
				// Запоминаем выходные значения в вектор градиентов
				gradients[i].Set(outputs[i + 1]);
				// Для выходных значений применяем производную функцию активации
				gradients[i].Map(Mathf.SigmoidDerivative);
				// Применяем cost function
				gradients[i].Scale(Vecf.ParallelMul(gradients[i + 1], (Matf)weights[i + 1].SubMat(0, weights[i + 1].rows - 1, 0, weights[i + 1].cols).Transpose()));

				// Пересчитываем силы связей
				// Находим momentum rate
				momentum = Matf.Scale(deltaWeights[i], alpha);
				// Расчитываем новые веса eta * output * gradient
				// eta - learning rate, output - значение нейрона от которого идет связь, gradient - градиент нейрона к которому идет связь
				deltaWeights[i] = Matf.ParallelMul(outputs[i], gradients[i]);
				deltaWeights[i].Scale(eta);
				// Прибавляем momentum rate к learning rate
				deltaWeights[i].Add(momentum);
				// Обновляем веса
				weights[i].Add(deltaWeights[i]);
			}
		}

		public override float[] GetArrayOutputs(int layer)
		{
			if (layer < 0 || layer >= layersCount)
				throw new ArgumentException();

			return outputs[layer].ToArray();
		}

		public override float[,] GetArrayWeights(int toLayer)
		{
			if (toLayer < 0 || toLayer >= layersCount)
				throw new ArgumentException();

			return weights[toLayer].ToArray();
		}

		public override Vecf GetVectorOutputs(int layer)
		{
			if (layer < 0 || layer >= layersCount)
				throw new ArgumentException();

			return new Vecf(0);
		}

		public override Matf GetMatrixWeights(int toLayer)
		{
			if (toLayer < 0 || toLayer >= layersCount)
				throw new ArgumentException();

			return new Matf(0, 0);
		}
	}
}
