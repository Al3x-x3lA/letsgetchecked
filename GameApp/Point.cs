﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp
{
    public struct Point
    {
        public int X;
        public int Y;

        public Point (int x, int y)
        {
            X = x;
            Y = y;
        }


        public override string ToString()
        {
            return $"[{X};{Y}]";
        }


        public override bool Equals(object point)
        {
            return point is Point && ((Point)point).X == X && ((Point)point).Y == Y;
        }


        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }


        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

    }
}
