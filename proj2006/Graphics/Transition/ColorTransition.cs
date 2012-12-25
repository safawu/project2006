using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using project2006.Util.Math;
using Microsoft.Xna.Framework;


namespace project2006.Graphics.Transition
{
    internal class ColorTransition : Transition
    {
        internal Color Start;
        internal Color End;
        internal Color Current;

        internal ColorTransition(int startTime, int endTime, Color startValue, Color endValue)
        {
            StartTime = startTime;
            EndTime = endTime;
            Start = startValue;
            End = endValue;
            TweenType = Util.Math.TweenType.linear;
            Current = new Color(Start.R, Start.G, Start.B, Start.A);
        }

        internal override void Update(int time)
        {
            if (!enable)
            {
                return;
            }
            float val = (time - StartTime) / length;
            val = (float)Tween.Current(val, TweenType);//这里继续用val，但意义不同了
            Current.R = (byte)(Start.R + val * (End.R - Start.R));
            Current.G = (byte)(Start.G + val * (End.G - Start.G));
            Current.B = (byte)(Start.B + val * (End.B - Start.B));
            Current.A = (byte)(Start.A + val * (End.A - Start.A));
        }
    }
}
