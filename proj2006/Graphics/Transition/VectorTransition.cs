using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using project2006.Util.Math;
using Microsoft.Xna.Framework;


namespace project2006.Graphics.Transition
{
    internal class VectorTransition : Transition
    {
        internal Vector2 Start;
        internal Vector2 End;
        internal Vector2 Current;

        internal VectorTransition(int startTime, int endTime, Vector2 startValue, Vector2 endValue)
        {
            StartTime = startTime;
            EndTime = endTime;
            Start = startValue;
            End = endValue;
            Current = new Vector2(Start.X, Start.Y);
            TweenType = Util.Math.TweenType.linear;
        }

        internal override void Update(int time)
        {
            if (!enable)
            {
                return;
            }
            float val = (time - StartTime) / length;
            val = (float)Tween.Current(val, TweenType);//这里继续用val，但意义不同了
            Current.X = (float)(Start.X + val * (End.X - Start.X));
            Current.Y = (float)(Start.Y + val * (End.Y - Start.Y));
        }
    }
}
