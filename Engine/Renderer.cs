using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter.Engine
{
				class Renderer
				{
								private static Pixel[] windowBuffer;
								private static Pixel[] drawnBuffer;
								public const int WINDOW_WIDTH = 48;
								public const int WINDOW_HEIGHT = 32;

								public static void Init()
								{
												windowBuffer = new Pixel[WINDOW_WIDTH * WINDOW_HEIGHT];
												drawnBuffer = new Pixel[WINDOW_WIDTH * WINDOW_HEIGHT];
												for (int i = 0; i < windowBuffer.Length; i++)
												{
																windowBuffer[i] = new Pixel();
																drawnBuffer[i] = new Pixel();
												}
								}

								public static void Put(string text, Vector2 pos, ConsoleColor color = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
								{
												for (int i = 0; i < text.Length; i++)
												{
																Put(text[i], Utils.GetMatrixPosition(Utils.GetArrayPosition(pos, WINDOW_WIDTH) + i, WINDOW_WIDTH), color, backgroundColor);
												}
								}

								public static void Put(char text, Vector2 pos, ConsoleColor color = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
								{
												if (pos.x < 0 || pos.x >= WINDOW_WIDTH || pos.y < 0 || pos.y >= WINDOW_HEIGHT)
																return;

												int a = Utils.GetArrayPosition(pos, WINDOW_WIDTH);
												windowBuffer[a].Set(text, color, backgroundColor);
								}

								public static void Draw()
								{
												for (int i = 0; i < windowBuffer.Length; i++)
												{
																if (windowBuffer[i].GetCharacter().Equals(' ') && windowBuffer[i].Equals(drawnBuffer[i]))
																				continue;
																int x = Utils.GetMatrixPosition(i, WINDOW_WIDTH).GetXInt();
																int y = Utils.GetMatrixPosition(i, WINDOW_WIDTH).GetYInt();
																Console.SetCursorPosition(x, y);
																Console.ForegroundColor = windowBuffer[i].GetColor();
																Console.BackgroundColor = windowBuffer[i].GetBackgroundColor();
																Console.Write(windowBuffer[i].GetCharacter());
																drawnBuffer[i].Set(windowBuffer[i].GetCharacter(), windowBuffer[i].GetColor(), windowBuffer[i].GetBackgroundColor());

																windowBuffer[i].Set();
												}
								}

								public class Pixel
								{
												char c;
												ConsoleColor color;
												ConsoleColor backgroundColor;

												public Pixel()
												{
																this.c = ' ';
																this.color = ConsoleColor.White;
																this.backgroundColor = ConsoleColor.Black;
												}
												public Pixel(char c, ConsoleColor color, ConsoleColor backgroundColor)
												{
																this.c = c;
																this.color = color;
																this.backgroundColor = backgroundColor;
												}

												/// <summary>
												/// Sets the pixel to the passed arguments, passing no arguments will reset the Pixel to nothing, black background and white text
												/// </summary>
												/// <param name="c"></param>
												/// <param name="color"></param>
												/// <param name="backgroundColor"></param>
												public void Set(char c = ' ', ConsoleColor color = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
												{
																this.c = c;
																this.color = color;
																this.backgroundColor = backgroundColor;
												}

												public char GetCharacter()
												{
																return this.c;
												}
												public ConsoleColor GetColor()
												{
																return this.color;
												}
												public ConsoleColor GetBackgroundColor()
												{
																return this.backgroundColor;
												}

												public override string ToString()
												{
																return String.Format("Pixel[char: {0}, color: {1}, bColor: {2}]", c, color, backgroundColor);
												}

												public override bool Equals(object obj)
												{
																if (obj is Pixel)
																{
																				Pixel p = (Pixel)obj;
																				return 
																								this.c.Equals(p.c) && 
																								this.color.Equals(p.color) && 
																								this.backgroundColor.Equals(p.backgroundColor);
																}
																return false;
												}
								}
				}
}
