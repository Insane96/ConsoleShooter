using ConsoleEngine;
using System;

namespace Shooter
{
    class Projectile : GameObject
    {
        public float damage;
        public float movementSpeed;
        public Utils.Directions direction;
        public bool isDead = false;

        private ConsoleColor color;

        public Projectile(string name, float damage, float movementSpeed, Vector2 pos, Vector2 size, Utils.Directions direction, string[] shape) : base(name, pos, size, shape)
        {
            this.damage = damage;
            this.movementSpeed = movementSpeed;
            this.direction = direction;

            this.color = ConsoleColor.Red;
        }

        public override void Update()
        {
            if (direction.Equals(Utils.Directions.UP))
                this.pos = this.pos.Add(0, -movementSpeed * Time.DeltaTime);
            if (direction.Equals(Utils.Directions.DOWN))
                this.pos = this.pos.Add(0, movementSpeed * Time.DeltaTime);

            if (this.pos.GetYInt() < 0 || this.pos.GetYInt() > Console.WindowHeight - 3)
                Engine.RemoveGameObjectByName(this.GetName());
        }

        public override void Draw()
        {
            for (int x = 0; x < shape.Length; x++)
            {
                for (int y = 0; y < shape[x].Length; y++)
                {
                    if (!shape[x][y].Equals(' '))
                        Renderer.Put(shape[x][y], this.pos.Add(y, x), color);
                }
            }
        }
    }
}
