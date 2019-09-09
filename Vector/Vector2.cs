using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter
{
    class Vector2
    {
        public float x;
        public float y;

        public Vector2()
        {
            this.x = 0;
            this.y = 0;
        }

        public Vector2(Vector2 pos)
        {
            this.x = pos.x;
            this.y = pos.y;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public int GetXInt()
        {
            return (int)this.x;
        }
        public int GetYInt()
        {
            return (int)this.y;
        }
        public Vector2 GetInt()
        {
            return new Vector2((int)this.x, (int)this.y);
        }

        public Vector2 Add(float x, float y)
        {
            return new Vector2(this.x + x, this.y + y);
        }

        public Vector2 Left()
        {
            return new Vector2(x - 1, y);
        }
        public Vector2 Right()
        {
            return new Vector2(x + 1, y);
        }

        public override string ToString()
        {
            return string.Format("Vector2[X: {0}, Y: {1}]", x, y);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2)
            {
                Vector2 v = (Vector2)obj;
                if (this.x == v.x && this.y == v.y)
                    return true;
            }
            return false;
        }

								public bool EqualsInt(Vector2 v)
        {
            return this.GetXInt() == v.GetXInt() && this.GetYInt() == v.GetYInt();
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x * v2.x, v1.y * v2.y);
        }
    }
}
