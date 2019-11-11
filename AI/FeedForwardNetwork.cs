using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntComSys.Computing;

namespace IntComSys.AI
{
	public class FeedForwardNetwork : NeuralNetwork<float>
	{
		Vecf[] outputs;
		Vecf[] gradients;
		Matf[] weights;
		Matf[] deltaWeights;

		public override float[] LastGuess => outputs[outputs.Length - 1].ToArray();

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
				outputs[i][dimension[i]] = 1;
				// Количество градиентов соответствует количество нейронов на i-ом слое, кроме bias
				// Так как bias не имеет входных связей
				gradients[i] = new Vecf(dimension[i]);
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
			outputs[0].Set(0, dimension[0], inputs);

			for (int i = 0; i < outputs.Length - 1; i++)
			{
				// Расчитываем выходные значения для первого после входного слоя
				outputs[i + 1].Set(0, dimension[i + 1], Vecf.Mul(outputs[i], weights[i]));
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
			// Расчитываем градиенты для O слоя
			// Запоминаем выходные значения в вектор градиентов
			gradients[gradients.Length - 1] = outputs[outputs.Length - 1];
			// Для выходных значений применяем производную функцию активации
			gradients[gradients.Length - 1].Map(Mathf.SigmoidDerivative);
			// Умножаем получившееся значения на разницу между искомым и нужным результатами
			gradients[gradients.Length - 1].Scale(targets - outputs[outputs.Length - 1]);

			// Запоминаем старые веса
			Matf oldDeltaWeights = deltaWeights[deltaWeights.Length - 1];
			// Умножаем каждый из весов на momentum rate
			oldDeltaWeights.Scale(alpha);
			// Расчитываем новые веса eta * output * gradient
			// eta - learning rate, output - значение нейрона от которого идет связь, gradient - градиент нейрона к которому идет связь
			deltaWeights[deltaWeights.Length - 1] = Matf.Mul(outputs[outputs.Length - 2], gradients[gradients.Length - 1]);
			deltaWeights[deltaWeights.Length - 1].Scale(eta);
			// Прибавляем momentum rate к learning rate
			deltaWeights[deltaWeights.Length - 1].Add(oldDeltaWeights);
			// Обновляем веса
			weights[weights.Length - 1].Add(deltaWeights[deltaWeights.Length - 1]);

			for (int i = weights.Length - 2; i >= 0; i--)
			{
				// Расчитываем градиенты для H слоя
				// Запоминаем выходные значения в вектор градиентов
				gradients[i] = outputs[i];
				// Для выходных значений применяем производную функцию активации
				gradients[i].Map(Mathf.SigmoidDerivative);
				// Умножаем вес нейрона на градиент нейрона с которым связан этот нейрон, и умножаем получившееся значение на пред. действие
				gradients[i].Scale(Vecf.Mul(gradients[i], Matf.Transpose(weights[i])));
				// Пересчитываем силы связей
				// Запоминаем старые веса
				oldDeltaWeights = deltaWeights[i];
				// Умножаем каждый из весов на momentum rate
				oldDeltaWeights.Scale(alpha);
				// Расчитываем новые веса eta * output * gradient
				// eta - learning rate, output - значение нейрона от которого идет связь, gradient - градиент нейрона к которому идет связь
				deltaWeights[i] = Matf.Mul(outputs[i], gradients[i]);
				deltaWeights[i].Scale(eta);
				// Прибавляем momentum rate к learning rate
				deltaWeights[i].Add(oldDeltaWeights);
				// Обновляем веса
				weights[i].Add(deltaWeights[i]);
			}
		}
	}
}
