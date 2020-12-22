using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    
    class FindPoints
    {
        double[] _x;
        Vector[] _y;
        double _h;
        int _N;
        Func<double, Vector, Vector> _f;
        Func<double, Vector> _orig;
        Vector GetK1(int pos)
        {
            return (_h * _f(_x[pos], _y[pos]));
        }
        Vector GetK2(int pos)
        {
            return _h * _f(_x[pos] + (_h / 2), _y[pos] + GetK1(pos) / 2);
        }
        Vector GetK3(int pos)
        {
            return _h * _f(_x[pos] + _h, _y[pos] - GetK1(pos) + 2 * GetK2(pos));
        }
        void SetY(int pos)
        {
            _y[pos] = _y[pos - 1] + (GetK1(pos-1)+4*GetK2(pos-1)+GetK3(pos-1)) / 6;
        }
        void Fill_X()
        {
            for (int i = 1; i <= _N; ++i)
                _x[i] = _x[0] + _h * i;
        }
        public void FindY()
        {
            for (int i = 1; i <= _N; ++i)
                SetY(i);
        }
        public FindPoints(double x0, double x1, int N,  Vector y0, Func<double, Vector, Vector> f, Func<double, Vector> F)
        {
            _f = f;
            _orig = F;
            _h = (x1 - x0) / N;
            _N = N;
            _x = new double[N + 1];
            _x[0] = x0;
            Fill_X();
            _y = new Vector[N + 1];
            _y[0] = y0;
        }
        public double[] GetXCoords()
        {
            return _x;
        }
        public Vector GetInacuracy()
        {
            Vector res =new Vector(0,0);
            for (int i = 0; i <= _N; ++i)
            {
                if (Math.Abs(_y[i].x1 - _orig(_x[i]).x1) > res.x1)
                    res.x1 = Math.Abs(_y[i].x1 - _orig(_x[i]).x1);
                if (Math.Abs(_y[i].x2 - _orig(_x[i]).x2) > res.x2)
                    res.x2 = Math.Abs(_y[i].x2 - _orig(_x[i]).x2);
            }
            return res;
        }
        public  Vector[] GetReal()
        {
            Vector[] res = new Vector[_N + 1];
            for (int i = 0; i <= _N; ++i)
                res[i] = _orig(_x[i]);
            return res;
        }
        public Vector[] GetY()
        {
            return _y;
        }
    }
}
