using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter.Engine
{
				class Input
				{

								private static ConsoleKeyInfo keyPressed = new ConsoleKeyInfo();

								/// <summary>
								/// Removes all the stacked key inputs every frame, instead of reading them later. Mostly useful on low FPS
								/// </summary>
								public static void ClearKeyBuffer()
								{
												while (Console.KeyAvailable)
																Console.ReadKey(true);
								}

								public static void UpdateKeyPress()
								{
												//Get current Key input
												if (Console.KeyAvailable)
																keyPressed = Console.ReadKey(true);
												else
																keyPressed = new ConsoleKeyInfo();

												ClearKeyBuffer();
								}

								public static bool IsKeyPressed(ConsoleKey consoleKey)
								{
												return keyPressed.Key.Equals(consoleKey);
								}
				}
}
