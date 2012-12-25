using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using project2006.Util.Math;


namespace project2006.Graphics.Transition
{
    internal class FloatTransition : Transition
    {
        internal float Start;
        internal float End;
        internal float Current;

        internal FloatTransition(int startTime, int endTime, float startValue, float endValue)
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
            Current = (float)(Start + Tween.Current(val, TweenType) * (End - Start));
        }
    }
}
