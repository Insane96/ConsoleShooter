using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter
{
    class Utils
				{

								public enum Directions
        {
            UP,
            RIGHT,
            DOWN,
            LEFT
        }

        public static void ClearKeyBuffer()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }
        
        public static float GetRandomFloat(Random random, float min, float max)
        {
            return (float) random.NextDouble() * (max - min) + min;
								}

								public static int GetArrayPosition(int x, int y, int width)
								{
												return ((y - 1) * width) + x;
								}
								public static int GetArrayPosition(Vector2 pos, int width = Renderer.WINDOW_WIDTH)
								{
												return (pos.GetYInt() * width) + pos.GetXInt();
								}

								public static Vector2 GetMatrixPosition(int l, int width = Renderer.WINDOW_WIDTH)
								{
												int y = (int)(l / width);
												int x = l % width;
												return new Vector2(x, y);
								}
				}
}
