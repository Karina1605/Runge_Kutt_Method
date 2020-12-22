using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Vector
    {
        public double x1, x2;
        public Vector (double x1, double x2)
        {
            this.x1 = x1;
            this.x2 = x2;
        }
        public static Vector operator *(double a, Vector vector)
        {
            return new Vector(vector.x1 * a, vector.x2 * a);
        }
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x1 + b.x1, a.x2 + b.x2);
        }
        public static Vector operator /(Vector v, double a)
        {
            return new Vector(v.x1 / a, v.x2 / a);
        }
        public static Vector operator - (Vector a, Vector b)
        {
            return new Vector(a.x1 - b.x1, a.x2 - b.x2);
        }
    }
}
