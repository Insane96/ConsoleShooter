using ConsoleEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter
{
    class Player
    {
        public float health;
        public float maxHealth;
        public float movementSpeed;
        public Vector2 pos;
        public float shootTimeCooldown;
        public float shootSpeed;
        public bool isDead = false;

        public Vector2 shootingPos;

        public Vector2 size;

        public string[,] shape;

        public Player(float health, float movementSpeed, float shootSpeed, Vector2 shootingPos, Vector2 size, string[,] shape)
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

        public void Draw()
        {
            for (int x = 0; x < shape.GetLength(1); x++)
            {
                for (int y = 0; y < shape.GetLength(0); y++)
                {
                    if (!shape[y, x].Equals(" "))
                        Renderer.Put(shape[y, x], this.pos.Add(x, y), ConsoleColor.Cyan);
                }
            }
            Renderer.Put("Health: " + this.health + "/" + this.maxHealth, new Vector2(0, Console.WindowHeight - 1));
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

        public void Update()
        {
            if (shootTimeCooldown > 0)
                shootTimeCooldown -= Time.deltaTime;
        }

        public bool HasBeenHitBy(Projectile projectile)
        {
            if (projectile.direction.Equals(Utils.Directions.UP))
                return false;
            int pX = projectile.pos.GetXInt();
            int pY = projectile.pos.GetYInt();
            int eX = this.pos.GetXInt();
            int eY = this.pos.GetYInt();
            if (pX >= eX && pX < eX + this.size.GetXInt() && pY >= eY && pY < eY + this.size.GetYInt())
            {
                return true;
            }
            return false;
        }

        public void Shoot()
        {
            if (shootTimeCooldown <= 0)
            {
                Projectile projectile = new Projectile(2f, 15f, this.pos + this.shootingPos, Utils.Directions.UP, this.shape[this.shootingPos.GetYInt(), this.shootingPos.GetXInt()], this);
                Shooter.AddProjectile(projectile);
                shootTimeCooldown = shootSpeed;
            }
        }
    }
}
