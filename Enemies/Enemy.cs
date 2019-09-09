using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shooter.Utils;

namespace Shooter
{
				class Enemy
				{
								public float health;
								public float maxHealth;
								public float damage;
								public Vector2 pos;
								public Directions direction;
								public float movementSpeed;
								public float maxMovementSpeed;
								public float increaseMovementSpeed;
								public Vector2 shootingPos;
								public float attackSpeed;
								private float timeToShoot;
								public float maxAttackSpeed;
								public float cooldownReduction;

								public float timeSinceDamaged = -0.5f;

								public Vector2 size;

								public string[,] shape;

								public bool isDead = false;

								public Enemy(float health, float damage, Vector2 pos, Vector2 shootingPos, float movementSpeed, float maxMovementSpeed, float increaseMovementSpeed, float initialAttackSpeed, float maxAttackSpeed, float cooldownReduction, Vector2 size, string[,] shape)
								{
												this.health = health;
												this.maxHealth = health;
												this.damage = damage;
												this.movementSpeed = movementSpeed;
												this.maxMovementSpeed = maxMovementSpeed;
												this.increaseMovementSpeed = increaseMovementSpeed;
												this.shootingPos = shootingPos;
												this.size = size;
												this.shape = shape;
												this.pos = pos;
												this.direction = Directions.LEFT;
												this.attackSpeed = initialAttackSpeed;
												this.timeToShoot = this.attackSpeed;
												this.maxAttackSpeed = maxAttackSpeed;
												this.cooldownReduction = cooldownReduction;
								}

								public void Draw()
								{
												for (int x = 0; x < shape.GetLength(1); x++)
												{
																for (int y = 0; y < shape.GetLength(0); y++)
																{
																				if (!shape[y, x].Equals(" "))
																								Renderer.Put(shape[y, x], this.pos.Add(x, y), GetHealthColor());
																}
												}
												if (timeSinceDamaged < 1.5f)
												{
																Vector2 p = this.pos.Add(0, -1);
																if (this.pos.GetYInt() == 0)
																				p = p.Add(0, this.size.GetXInt());
																Renderer.Put(health.ToString(), p);
												}
								}

								public void Update()
								{
												timeSinceDamaged += Shooter.deltaTime;

												if (direction == Directions.LEFT)
												{
																this.pos = this.pos.Add(-this.movementSpeed * Shooter.deltaTime, 0);
																if (this.pos.GetXInt() <= 0)
																{
																				this.direction = Directions.RIGHT;
																				Buff();
																}
												}
												else if (direction == Directions.RIGHT)
												{
																this.pos = this.pos.Add(this.movementSpeed * Shooter.deltaTime, 0);
																if (this.pos.GetXInt() >= Renderer.WINDOW_WIDTH - this.size.GetXInt())
																{
																				this.direction = Directions.LEFT;
																				Buff();
																}
												}

												this.timeToShoot -= Shooter.deltaTime;
												if (this.timeToShoot <= 0f)
												{
																this.timeToShoot = this.attackSpeed;

																Projectile projectile;
																if (this.damage > 15f)
																				projectile = new Projectile(this.damage, 10f, this.pos + this.shootingPos, Directions.DOWN, "■");
																else
																				projectile = new Projectile(this.damage, 10f, this.pos + this.shootingPos, Directions.DOWN, "V");
																Shooter.AddProjectile(projectile);
												}
								}
								public void Damage(float damage)
								{
												this.health -= damage;
												this.timeSinceDamaged = 0f;
												if (this.health <= 0f)
																this.isDead = true;
								}

								private void Buff()
								{
												this.movementSpeed += this.increaseMovementSpeed;
												if (this.movementSpeed > this.maxMovementSpeed)
																this.movementSpeed = this.maxMovementSpeed;

												this.attackSpeed -= this.cooldownReduction;
												if (this.attackSpeed < this.maxAttackSpeed)
																this.attackSpeed = this.maxAttackSpeed;
								}

								public ConsoleColor GetHealthColor()
								{
												if (this.health / this.maxHealth > 0.8)
																return ConsoleColor.Green;
												else if (this.health / this.maxHealth > 0.5)
																return ConsoleColor.Yellow;
												else if (this.health / this.maxHealth > 0.1)
																return ConsoleColor.DarkYellow;

												return ConsoleColor.Red;
								}

								public bool HasBeenHitBy(Projectile projectile)
								{
												if (projectile.direction.Equals(Directions.DOWN))
																return false;
												int pX = projectile.pos.GetXInt();
												int pY = projectile.pos.GetYInt();
												int eX = this.pos.GetXInt();
												int eY = this.pos.GetYInt();
												if (pX >= eX && pX < eX + this.size.GetXInt() && pY >= eY && pY < eY + this.size.GetYInt())
												{
																timeSinceDamaged = 0;
																return true;
												}
												return false;
								}
				}
}
