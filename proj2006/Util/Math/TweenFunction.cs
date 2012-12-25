using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace project2006.Util.Math
{
    internal enum TweenType
    {
        easeInQuad,
        easeOutQuad,
        easeInCubic,
        easeOutCubic,
        easeInSine,
        easeOutSine,
        elastic,
        spring,
        wobble,
        linear
    }

    internal static class Tween
    {
        internal static double Current(float val, TweenType type)
        {
            switch (type)
            {
                case TweenType.easeInQuad: 
                    return System.Math.Pow(val, 2);
                case TweenType.easeOutQuad:
                    return -(System.Math.Pow((val-1), 2) -1);
                case TweenType.easeInCubic:
                    return System.Math.Pow(val, 3);
                case TweenType.easeOutCubic: 
                    return (System.Math.Pow((val-1), 3) +1);
                case TweenType.easeInSine:
                    return -System.Math.Cos(val* (System.Math.PI/2)) + 1;
                case TweenType.easeOutSine:
                    return System.Math.Sin(val * (System.Math.PI/2));
                case TweenType.elastic:
                    return -1 * System.Math.Pow(4, -8 * val) * System.Math.Sin((val * 6 - 1) * (2 * System.Math.PI) / 2) + 1;
                case TweenType.spring:
                    return 1 - (System.Math.Cos(val * 4.5 * System.Math.PI) * System.Math.Exp(-val* 6));
                case TweenType.wobble:
                    return (-System.Math.Cos(val* System.Math.PI * (9 * val)) / 2) + 0.5;
                case TweenType.linear:
                default:
                    return val;
            }
        }
    }
}
