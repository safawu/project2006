using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using project2006.Util.Math;


namespace project2006.Graphics.Transition
{
    internal class IntTransition : Transition
    {
        internal int Start;
        internal int End;
        internal int Current;

        internal IntTransition(int startTime, int endTime, int startValue, int endValue)
        {
            StartTime = startTime;
            EndTime = endTime;
            Start = startValue;
            End = endValue;
            TweenType = Util.Math.TweenType.linear;
        }

        internal override void Update(int time)
        {
            if (!enable)
            {
                return;
            }
            float val = (time - StartTime) / length;
            Current = (int)(Start + Tween.Current(val, TweenType) * (End - Start));
        }
    }
}
