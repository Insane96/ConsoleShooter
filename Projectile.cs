using ConsoleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter
{
    class Projectile
    {
        public float damage;
        public float movementSpeed;
        public Vector2 pos;
        public Utils.Directions direction;
        public bool isDead = false;
								private string symbol;

        public Projectile(float damage, float movementSpeed, Vector2 pos, Utils.Directions direction, string symbol)
        {
            this.damage = damage;
            this.movementSpeed = movementSpeed;
												if (direction.Equals(Utils.Directions.UP))
																this.pos = pos.Add(0, -1);
												else if (direction.Equals(Utils.Directions.DOWN))
																this.pos = pos.Add(0, 1);
												this.direction = direction;
												this.symbol = symbol;
        }

        public void Update()
        {
            if (direction.Equals(Utils.Directions.UP))
                this.pos = this.pos.Add(0, -movementSpeed * Time.deltaTime);
            if (direction.Equals(Utils.Directions.DOWN))
                this.pos = this.pos.Add(0, movementSpeed * Time.deltaTime);

            if (this.pos.GetYInt() < 0 || this.pos.GetYInt() > Console.WindowHeight - 3) this.isDead = true;
        }

        public void Draw()
        {
												Renderer.Put(this.symbol, this.pos, ConsoleColor.Red);
        }
    }
}
