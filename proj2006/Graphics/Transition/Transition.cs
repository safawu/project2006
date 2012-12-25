using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using project2006.Util.Math;

namespace project2006.Graphics.Transition
{
    internal abstract class Transition:IComparable<Transition>
    {
        protected int startTime;
        protected int endTime;
        protected float length;
        protected bool enable;//该transition有效

        internal TransitionType TransitionType;
        internal TweenType TweenType;
        internal int StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
                length = endTime - startTime;
                enable = length > 0;
            }
        }

        internal int EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
                length = endTime - startTime;
                enable = length > 0;
            }
        }

        internal abstract void Update(int time);

        public int CompareTo(Transition t) 
        {
            return startTime.CompareTo(t.startTime);
        }
    }
}
