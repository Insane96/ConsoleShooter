using ConsoleEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter
{
    class Enemy : GameObject
    {
        public float health;
        public float maxHealth;
        public float damage;
        public Utils.Directions direction;
        public float movementSpeed;
        public float maxMovementSpeed;
        public float increaseMovementSpeed;
        public Vector2 shootingPos;
        public float attackSpeed;
        private float timeToShoot;
        public float maxAttackSpeed;
        public float cooldownReduction;

        private float timeSinceDamaged = -0.5f;

        public Enemy(string name, float health, float damage, Vector2 pos, Vector2 shootingPos, float movementSpeed, float maxMovementSpeed, float increaseMovementSpeed, float initialAttackSpeed, float maxAttackSpeed, float cooldownReduction, Vector2 size, string[] shape) : base(name, pos, size, shape)
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
            this.direction = Utils.Directions.LEFT;
            this.attackSpeed = initialAttackSpeed;
            this.timeToShoot = this.attackSpeed;
            this.maxAttackSpeed = maxAttackSpeed;
            this.cooldownReduction = cooldownReduction;
        }

        public Enemy(string name, EnemySerializable enemy) : base(name, enemy.pos)
        {
            this.health = enemy.health;
            this.maxHealth = this.health;
            this.damage = enemy.damage;
            this.movementSpeed = enemy.movementSpeed;
            this.maxMovementSpeed = enemy.maxMovementSpeed;
            this.increaseMovementSpeed = enemy.increaseMovementSpeed;
            this.shootingPos = enemy.shootingPos;
            this.size = enemy.size;
            this.shape = enemy.shape;
            this.pos = enemy.pos;
            this.direction = Utils.Directions.LEFT;
            this.attackSpeed = enemy.attackSpeed;
            this.timeToShoot = this.attackSpeed;
            this.maxAttackSpeed = enemy.maxAttackSpeed;
            this.cooldownReduction = enemy.cooldownReduction;
        }

        public override void Draw()
        {
            for (int x = 0; x < shape.Length; x++)
            {
                for (int y = 0; y < shape[x].Length; y++)
                {
                    if (!shape[x][y].Equals(" "))
                        Renderer.Put(shape[x][y], this.pos.Add(y, x), GetHealthColor());
                }
            }
            if (timeSinceDamaged < 1.5f)
            {
                Vector2 p = this.pos.Add(0, -1);
                if (this.pos.GetYInt() == 0)
                    p = p.Add(0, this.size.GetXInt() - 1);
                Renderer.Put(health.ToString(), p);
            }
        }

        public override void Update()
        {
            timeSinceDamaged += Time.deltaTime;

            if (direction == Utils.Directions.LEFT)
            {
                this.pos = this.pos.Add(-this.movementSpeed * Time.deltaTime, 0);
                if (this.pos.GetXInt() <= 0)
                {
                    this.direction = Utils.Directions.RIGHT;
                    Buff();
                }
            }
            else if (direction == Utils.Directions.RIGHT)
            {
                this.pos = this.pos.Add(this.movementSpeed * Time.deltaTime, 0);
                if (this.pos.GetXInt() >= Renderer.GetWindowWidth() - this.size.GetXInt())
                {
                    this.direction = Utils.Directions.LEFT;
                    Buff();
                }
            }

            this.timeToShoot -= Time.deltaTime;
            if (this.timeToShoot <= 0f)
            {
                this.timeToShoot = this.attackSpeed;

                Projectile projectile;
                projectile = new Projectile("projectile" + Rand.GetRandomInt(0, int.MaxValue), this.damage, 10f, this.pos + this.shootingPos, Vector2.One, Utils.Directions.DOWN, new string[] { this.shape[this.shootingPos.GetYInt()][this.shootingPos.GetXInt()].ToString() });
                Shooter.AddProjectile(projectile);
                Engine.AddGameObject(projectile);
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Projectile)
            {
                Projectile projectile = (Projectile) other;
                if (projectile.direction == Utils.Directions.UP)
                {
                    this.Damage(projectile.damage);
                    Engine.RemoveGameObjectByName(projectile.GetName());
                    this.timeSinceDamaged = 0;
                }
            }
        }

        public void Damage(float damage)
        {
            this.health -= damage;
            if (this.health <= 0f)
            {
                Engine.RemoveGameObjectByName(this.GetName());
                Shooter.enemies--;
            }
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
    }

    public class EnemySerializable
    {
        public float health;
        public float maxHealth;
        public float damage;
        public Vector2 pos;
        public float movementSpeed;
        public float maxMovementSpeed;
        public float increaseMovementSpeed;
        public Vector2 shootingPos;
        public float attackSpeed;
        public float maxAttackSpeed;
        public float cooldownReduction;

        public Vector2 size;

        public string[] shape;

        public EnemySerializable()
        {
            this.health = 100f;
            this.maxHealth = this.health;
            this.damage = 8f;
            this.movementSpeed = 1f;
            this.maxMovementSpeed = 4f;
            this.increaseMovementSpeed = 0.5f;
            this.shootingPos = new Vector2(1, 1);
            this.size = new Vector2(3, 2);
            this.shape = new string[2]
            {
                    "███",
                    " ■ ",
            };
            this.pos = new Vector2(Renderer.GetWindowWidth() / 2, Rand.GetRandomFloat(10, 15));
            this.attackSpeed = 2.5f;
            this.maxAttackSpeed = 0.4f;
            this.cooldownReduction = 0.5f;
        }

        public EnemySerializable(float health, float damage, Vector2 pos, Vector2 shootingPos, float movementSpeed, float maxMovementSpeed, float increaseMovementSpeed, float initialAttackSpeed, float maxAttackSpeed, float cooldownReduction, Vector2 size, string[] shape)
        {
            this.health = health;
            this.maxHealth = this.health;
            this.damage = damage;
            this.movementSpeed = movementSpeed;
            this.maxMovementSpeed = maxMovementSpeed;
            this.increaseMovementSpeed = increaseMovementSpeed;
            this.shootingPos = shootingPos;
            this.size = size;
            this.shape = shape;
            this.pos = pos;
            this.attackSpeed = initialAttackSpeed;
            this.maxAttackSpeed = maxAttackSpeed;
            this.cooldownReduction = cooldownReduction;
        }

    }
}
