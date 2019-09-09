using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Shooter
{
				class Shooter
				{
								public static Random random = new Random();


								//Projectiles List
								static List<Projectile> projectiles = new List<Projectile>();

								public static bool gameOver = false;

								static Stopwatch sw = new Stopwatch();

								public static float deltaTime;

								public static void AddProjectile(Projectile p)
								{
												projectiles.Add(p);
								}

								static void Main(string[] args)
								{
												Console.SetWindowSize(Renderer.WINDOW_WIDTH, Renderer.WINDOW_HEIGHT);
												Console.CursorVisible = false;

												Renderer.Init();

												float fps = 0f;
												float fpsDisplayTime = 1f;

												//Player
												Player player = new Player(
																health: 100f,
																movementSpeed: 1f,
																shootSpeed: 0.25f,
																shootingPos: new Vector2(1, 0),
																size: new Vector2(3, 2),
																shape: new string[2, 3]
																{
																				{" ","■"," "},
																				{"█","█","█"},
																}
												);

												//Enemies List
												List<Enemy> enemies = new List<Enemy>();
												enemies.Add(new Enemy(
																health: 100f,
																damage: 10f,
																pos: new Vector2(Renderer.WINDOW_WIDTH / 2, Utils.GetRandomFloat(random, 10, 15)),
																shootingPos: new Vector2(1, 1),
																movementSpeed: 4f,
																maxMovementSpeed: 8f,
																increaseMovementSpeed: 1f,
																initialAttackSpeed: 2.5f,
																maxAttackSpeed: 0.4f,
																cooldownReduction: 0.25f,
																size: new Vector2(3, 2),
																shape: new string[2, 3]
																{
																				{"█","█","█"},
																				{" ","■"," "},
																}
												));
												enemies.Add(new Enemy(
																health: 300f,
																damage: 30f,
																pos: new Vector2(Renderer.WINDOW_WIDTH / 2, Utils.GetRandomFloat(random, 0, 5)),
																shootingPos: new Vector2(2, 1),
																movementSpeed: 2f,
																maxMovementSpeed: 3f,
																increaseMovementSpeed: 0.5f,
																initialAttackSpeed: 1.25f,
																maxAttackSpeed: 0.75f,
																cooldownReduction: 0.25f,
																new Vector2(5, 3),
																shape: new string[3, 5]
																{
																				{"█","█","█","█", "█"},
																				{"█"," ","■"," ", "█"},
																				{"█"," "," "," ", "█"},
																}
												));

												ConsoleKeyInfo keyPressed = new ConsoleKeyInfo();

												/*Console.Beep(329, 200);
												Console.Beep(329, 300);
												Console.Beep(329, 300);
												Console.Beep(294, 200);
												Console.Beep(329, 400);
												Console.Beep(392, 500);
												Console.Beep(196, 400);*/

												do
												{
																//Stopwatch
																sw.Start();


																//Get current Key input
																if (Console.KeyAvailable)
																				keyPressed = Console.ReadKey(true);
																else keyPressed = new ConsoleKeyInfo();
																Utils.ClearKeyBuffer();

																if (!gameOver)
																{
																				//Input
																				if (keyPressed.Key.Equals(ConsoleKey.RightArrow))
																								player.Move(Utils.Directions.RIGHT);
																				if (keyPressed.Key.Equals(ConsoleKey.LeftArrow))
																								player.Move(Utils.Directions.LEFT);
																				if (keyPressed.Key.Equals(ConsoleKey.Spacebar))
																								player.Shoot();

																				//Update
																				player.Update();
																				foreach (Enemy enemy in enemies)
																				{
																								enemy.Update();
																				}

																				foreach (Projectile projectile in projectiles)
																				{
																								projectile.Update();
																								if (player.HasBeenHitBy(projectile))
																								{
																												projectile.isDead = true;
																												player.Damage(projectile.damage);
																								}
																				}

																				List<Enemy> deadEntities = new List<Enemy>();
																				List<Projectile> deadProjectiles = new List<Projectile>();
																				foreach (Enemy enemy in enemies)
																				{
																								foreach (Projectile projectile in projectiles)
																								{
																												if (enemy.HasBeenHitBy(projectile))
																												{
																																enemy.Damage(projectile.damage);
																																projectile.isDead = true;
																												}

																												if (projectile.isDead)
																																deadProjectiles.Add(projectile);
																								}

																								if (enemy.isDead)
																												deadEntities.Add(enemy);
																				}

																				foreach (Enemy deadEntity in deadEntities)
																				{
																								enemies.Remove(deadEntity);
																				}
																				foreach (Projectile deadProjectile in deadProjectiles)
																				{
																								projectiles.Remove(deadProjectile);
																				}

																				if (player.isDead)
																								gameOver = true;
																}

																//Draw
																player.Draw();

																foreach (Enemy enemy in enemies)
																{
																				enemy.Draw();
																}

																foreach (Projectile projectile in projectiles)
																{
																				projectile.Draw();
																}

																if (gameOver)
																				Renderer.Put("Game Over", new Vector2(Renderer.WINDOW_WIDTH / 2 - 4, Renderer.WINDOW_HEIGHT / 2), ConsoleColor.Red);

																if (1f / deltaTime >= 10f)
																				Renderer.Put("FPS: " + Math.Round(fps, 0), new Vector2(Renderer.WINDOW_WIDTH / 2, Renderer.WINDOW_HEIGHT - 1));
																else
																				Renderer.Put("FPS: " + Math.Round(fps, 1), new Vector2(Renderer.WINDOW_WIDTH / 2, Renderer.WINDOW_HEIGHT - 1));


																Renderer.Draw();

																sw.Stop();
																deltaTime = sw.ElapsedMilliseconds / 1000f;
																sw.Reset();

																fpsDisplayTime -= deltaTime;
																if (fpsDisplayTime <= 0f)
																{
																				fpsDisplayTime = 1f;
																				fps = (1f / deltaTime);
																}

												} while (!keyPressed.Key.Equals(ConsoleKey.Escape));
								}
				}
}
