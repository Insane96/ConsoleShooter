using ConsoleEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter
{
    class Player : GameObject
    {
        public float health;
        public float maxHealth;
        public float movementSpeed;
        public float shootTimeCooldown;
        public float shootSpeed;
        public bool isDead = false;

        public Vector2 shootingPos;

        public Player(string name, float health, float movementSpeed, float shootSpeed, Vector2 shootingPos, Vector2 size, string[] shape) : base(name, new Vector2(Renderer.GetWindowWidth() / 2, 27), size, shape, true)
        {
            pos = new Vector2(Renderer.GetWindowWidth() / 2, 27);
            this.health = health;
            this.maxHealth = health;
            this.movementSpeed = movementSpeed;
            this.shootSpeed = shootSpeed;
            this.shootingPos = shootingPos;
            this.size = size;
            this.shape = shape;
        }

        public override void Update()
        {
            if (this.isDead)
                return;
            if (shootTimeCooldown > 0)
                shootTimeCooldown -= Time.deltaTime;

            //Input
            if (Input.IsKeyPressed(ConsoleKey.RightArrow))
                this.Move(Utils.Directions.RIGHT);
            if (Input.IsKeyPressed(ConsoleKey.LeftArrow))
                this.Move(Utils.Directions.LEFT);
            if (Input.IsKeyPressed(ConsoleKey.Spacebar))
                this.Shoot();
        }

        public override void Draw()
        {
            Renderer.Put("Health: " + this.health + "/" + this.maxHealth, new Vector2(0, Console.WindowHeight - 1));

            if (isDead)
                return;

            for (int x = 0; x < shape.Length; x++)
            {
                for (int y = 0; y < shape[x].Length; y++)
                {
                    if (!shape[x][y].Equals(" "))
                        Renderer.Put(shape[x][y], this.pos.Add(y, x), ConsoleColor.Cyan);
                }
            }
        }

        public void Damage(float damage)
        {
            this.health -= damage;
            if (this.health <= 0f)
            {
                this.health = 0f;
                this.isDead = true;
            }
        }

        public void Move(Utils.Directions direction)
        {
            switch (direction)
            {
                case Utils.Directions.RIGHT:
                    if (this.pos.Add(movementSpeed, 0).GetXInt() < Renderer.GetWindowWidth() - this.size.GetYInt())
                        this.pos = this.pos.Add(movementSpeed, 0);
                    break;
                case Utils.Directions.LEFT:
                    if (this.pos.Add(-movementSpeed, 0).GetXInt() >= 0)
                        this.pos = this.pos.Add(-movementSpeed, 0);
                    break;
                default:
                    break;
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Projectile projectile)
            {
                if (projectile.direction == Utils.Directions.DOWN)
                {
                    this.Damage(projectile.damage);
                    Engine.RemoveGameObjectByName(projectile.GetName());
                }
            }
        }

        public void Shoot()
        {
            if (shootTimeCooldown <= 0)
            {
                for (int i = -1; i <= 1; i++)
                {
                    Vector2 shootPos = this.pos + this.shootingPos;
                    shootPos = shootPos.Add(i, 0);
                    Projectile projectile = new Projectile("projectile" + Rand.GetRandomInt(0, int.MaxValue), 1f, 15f, shootPos, Vector2.One, Utils.Directions.UP, new string[] { "A" });
                    Shooter.AddProjectile(projectile);
                    Engine.AddGameObject(projectile);
                }
                shootTimeCooldown = shootSpeed;
            }
        }
    }
}
