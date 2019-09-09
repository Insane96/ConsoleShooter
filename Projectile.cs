using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shooter.Utils;

namespace Shooter
{
    class Projectile
    {
        public float damage;
        public float movementSpeed;
        public Vector2 pos;
        public Directions direction;
        public bool isDead = false;
								private string symbol;

        public Projectile(float damage, float movementSpeed, Vector2 pos, Directions direction, string symbol)
        {
            this.damage = damage;
            this.movementSpeed = movementSpeed;
												if (direction.Equals(Directions.UP))
																this.pos = pos.Add(0, -1);
												else if (direction.Equals(Directions.DOWN))
																this.pos = pos.Add(0, 1);
												this.direction = direction;
												this.symbol = symbol;
        }

        public void Update()
        {
            if (direction.Equals(Directions.UP))
                this.pos = this.pos.Add(0, -movementSpeed * Shooter.deltaTime);
            if (direction.Equals(Directions.DOWN))
                this.pos = this.pos.Add(0, movementSpeed * Shooter.deltaTime);

            if (this.pos.GetYInt() < 0 || this.pos.GetYInt() > Console.WindowHeight - 3) this.isDead = true;
        }

        public void Draw()
        {
												Renderer.Put(this.symbol, this.pos, ConsoleColor.Red);
        }
    }
}
