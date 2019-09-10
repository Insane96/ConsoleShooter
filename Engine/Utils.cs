using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter.Engine
{
    class Utils
				{
								/// <summary>
								/// Basic directions list
								/// </summary>
								public enum Directions
        {
            UP,
            RIGHT,
            DOWN,
            LEFT
        }

								/// <summary>
								/// Returns the position in an array given a matrix x and y position and matrix width
								/// </summary>
								/// <param name="x"></param>
								/// <param name="y"></param>
								/// <param name="width"></param>
								/// <returns></returns>
								public static int GetArrayPosition(int x, int y, int width = Renderer.WINDOW_WIDTH)
								{
												return (y * width) + x;
								}

								/// <summary>
								/// Returns the position in an array given a matrix x and y position as Vector2 and matrix width
								/// </summary>
								/// <param name="pos"></param>
								/// <param name="width"></param>
								/// <returns></returns>
								public static int GetArrayPosition(Vector2 pos, int width = Renderer.WINDOW_WIDTH)
								{
												return GetArrayPosition(pos.GetXInt(), pos.GetYInt(), width);
								}

								/// <summary>
								/// Returns the x and y position on a matrix given the array position and matrix width
								/// </summary>
								/// <param name="l"></param>
								/// <param name="width"></param>
								/// <returns></returns>
								public static Vector2 GetMatrixPosition(int l, int width = Renderer.WINDOW_WIDTH)
								{
												int y = (int)(l / width);
												int x = l % width;
												return new Vector2(x, y);
								}
				}
}
