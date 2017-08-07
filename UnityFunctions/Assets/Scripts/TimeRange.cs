using Extensions;
using UnityEngine;

namespace UnityFunctions
{
    internal class TimeRange
    {

        private float _interval;
        internal TimeRange() : this(0, 0) { }
        internal TimeRange(double from, double to)
        {
            From = (float)from;
            To = (float)to;
            _interval = To - From;
        }
        public float From { get; set; }
        public float To { get; set; }
        internal bool IsCyclical { get; set; }
        internal TimeRange SetAsCyclical()
        {
            IsCyclical = true;
            return this;
        }
        internal TimeRange SetTime(double secondsAfterNow)
        {
            _interval = (float)secondsAfterNow;
            From = Time.time;
            To = From + (float)secondsAfterNow;
            return this;
        }
        internal TimeRange SetZero()
        {
            To = From = 0f;
            _interval = 0;
            return this;
        }
        internal float Progress()
        {
            var now = Time.time;
            if (IsCyclical && To < now)
            {
                var all = To - From;
                if (all.IsZero())
                {
                    return now < From ? 0 : 1;
                }
                var curr = now - From;
                return curr / all;
            }
            return Progress(now);
        }
        internal float Progress(float value)
        {
            return (float)ProgressByValue(From, To, value);
        }
        private static double ProgressByValue(double from, double to, double value)
        {
            var all = to - from;
            if (all < 0.000001 && all > -0.000001)
            {
                return value < from ? 0 : 1;
            }
            var curr = value - from;
            return (curr / all);
        }
        internal bool IsFinished()
        {
            if (IsCyclical) return false; // cyclicals are never finished
            return To <= Time.time;
        }
        internal bool IsFinishedReset()
        {
            if (IsCyclical) return false; // cyclicals are never finished
            var finished = To <= Time.time;
            if (finished)
            {
                Reset();
            }
            return finished;
        }
        internal TimeRange Reset()
        {
            SetTime(_interval);
            return this;
        }
        public override string ToString() { return "{type:'TimeRange',from:" + From + ",to:" + To + "}"; }
    }
}