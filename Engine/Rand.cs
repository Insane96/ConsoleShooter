using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter.Engine
{
				class Rand
				{
								/// <summary>
								/// Random instance used to generate random numbers
								/// </summary>
								public static Random random = new Random();

								/// <summary>
								/// Returns a random int number between min (inclusive) and max (exclusive). If max is smaller than min then min is returned
								/// </summary>
								/// <param name="min"></param>
								/// <param name="max"></param>
								/// <returns></returns>
								public static int GetRandomInt(int min, int max)
								{
												return max > min ? random.Next(min, max) : min;
								}

								/// <summary>
								/// Returns a random float number between min (inclusive) and max (exclusive). If max is smaller than min then min is returned
								/// </summary>
								/// <param name="min"></param>
								/// <param name="max"></param>
								/// <returns></returns>
								public static float GetRandomFloat(float min, float max)
								{
												return max > min ? (float)random.NextDouble() * (max - min) + min : min;
								}

								/// <summary>
								/// Returns a random double number between min (inclusive) and max (exclusive). If max is smaller than min then min is returned
								/// </summary>
								/// <param name="min"></param>
								/// <param name="max"></param>
								/// <returns></returns>
								public static double GetRandomDouble(float min, float max)
								{
												return max > min ? random.NextDouble() * (max - min) + min : min;
								}
				}
}
