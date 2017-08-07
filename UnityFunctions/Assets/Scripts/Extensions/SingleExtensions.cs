using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Extensions
{
    internal static class SingleExtensions
    {
        internal const double Epsilon = 0.000001;
        internal static float FromMin11To01(this float x)
        {
            return x*0.5f + 0.5f;
        }
        internal static float From01ToMin11(this float x)
        {
            return 2*x - 1f;
        }
        internal static int RoundAsInt(this float n, double multiplier)
        {
            return (int)Math.Round(n * multiplier, 0);
        }
        internal static int RoundAsInt(this float n)
        {
            return (int)Math.Round(n, 0);
        }
        internal static float Round(this float n)
        {
            return (float)Math.Round(n, 0);
        }
        internal static float Round(this float n, int digits)
        {
            return (float)Math.Round(n, digits);
        }
        internal static bool IsZero(this float n)
        {
            return n.IsZero(Epsilon);
        }
        internal static bool IsZero(this float n, double epsilon)
        {
            return n < epsilon && n > -epsilon;
        }
        internal static bool IsEqual(this float n, double m)
        {
            return n.IsEqual(m, Epsilon);
        }
        internal static bool IsEqual(this float n, float m, double epsilon)
        {
            return Math.Abs((double)n - (double)m) < epsilon;
        }
        internal static bool IsEqual(this float n, double m, double epsilon)
        {
            return Math.Abs((double)n - m) < epsilon;
        }
        internal static bool IsWithin(this float n, float m, double epsilon)
        {
            return Math.Abs((double)n - (double)m) < epsilon;
        }
        internal static float Project(this float n, Func<float, float> fn)
        {
            if (fn == null) return n; // no function is considered linear identity function
            return fn(n); 
        }
//        internal static float Project(this float n, IBezierGroup bg)
//        {
//            return (float)bg.run(n);
//        }
        internal static float NoMoreThan(this float a, double b) { return a > b ? (float)b : a; }
        internal static float NoLessThan(this float a, double b) { return a < b ? (float)b : a; }
        internal static float EnsureZeroOrNegative(this float n) { return n > 0 ? n * -1 : n; }
        internal static float Clamp(this float value, double min, double max)
        {
            if (value < min)
            {
                value = (float)min;
                return value;
            }
            if (value > max)
            {
                value = (float)max;
            }
            return value;
        }
        internal static float Of01Range(this float progress, double min, double max)
        {
            return (float)(min + progress*(max - min));
        }
        internal static float OfM11Range(this float progress, double min, double max)
        {
            progress = 0.5f * progress + 0.5f;
            return (float)(min + progress*(max - min));
        }
//        internal static float ClampedRatioBetween(this float n, double min, double max)
//        {
//            var range = max - min;
//            if (range.IsZero()) return 0;
//            n = n.Clamp(min, max);
//            return (float)((n - min)/range);
//        }
//        internal static float RatioBetween(this float n, double min, double max)
//        {
//            var range = max - min;
//            if (range.IsZero()) return 0;
//            return (float)((n - min)/range);
//        }
        internal static bool IsBetween(this float n, double min, double max)
        {
            return n >= min && n <= max;
        }
        internal static float Abs(this float n)
        {
            return n >= 0 ? n : n*-1;
        }
        internal static int Sign(this float n)
        {
            return n < 0 ? -1 : 1;
        }
        internal static float Sign(this double n, bool isPositive)
        {
            return (float)n * (isPositive ? 1 : -1);
        }
        internal static float EnsureNotZero(this float n) { return EnsureNotZero(n, 0.00001f); }
        internal static float EnsureNotZero(this float n, float epsillon)
        {
            var abs = n >= 0 ? n : n * -1;
            return abs < epsillon ? epsillon : n;
        }
        internal static float GoTowards(this float from, float to, double step)
        {
            var diff = to - from;
            if (diff.Abs() <= step) return to;
            var sign = Math.Sign(diff);
            return from + (float)step*sign;
        }
        internal static float GoTowardsWithin360(this float from, float to, double step)
        {
            var diff = to - from;
            if(diff.Abs() >= 180)
            {
                if(to.Abs() >= from.Abs())
                {
                    if (to < 0) to += 360;
                    else to -= 360;
                } 
                else
                {
                    if (from < 0) from += 360;
                    else from -= 360;
                }
            }
            return from.GoTowards(to, step);
        }
        internal static float ApproachByRatio(this float from, float to, double ratio)
        {
            var distance = to - from;
            if (distance.IsEqual(0, 0.0001f))
            {
                return to;
            }
            return (float)(distance * ratio) + from;
        }
        internal static float ClampM11(this float value)
        {
            if (value < -1f)
            {
                return -1f;
            }
            if (value > 1f)
            {
                return 1f;
            }
            return value;
        }
        internal static float Clamp01(this float x)
        {
            if (x < 0f)
            {
                return 0f;
            }
            if (x > 1f)
            {
                return 1f;
            }
            return x;
        }
        internal static float Clamp01AndProjectM11(this float x)
        {
            if (x < 0f)
            {
                x = 0f;
            }
            if (x > 1f)
            {
                x = 1f;
            }
            return 2f * x - 1f;
        }
        internal static float EnsureExtreme(this float a, float b)
        {
            if(a < 0)
            {
                return b < a ? b : a;
            }
            return b > a ? b : a;
        }

        internal static string s(this float input)
        {
            string str = input.ToString(CultureInfo.InvariantCulture);

            // if string representation was collapsed from scientific notation, just return it:
            if (str.IndexOf("E", StringComparison.OrdinalIgnoreCase) < 0) return str;

            bool negativeNumber = false;

            if (str[0] == '-')
            {
                str = str.Remove(0, 1);
                negativeNumber = true;
            }

            string sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            char decSeparator = sep.ToCharArray()[0];

            string[] exponentParts = str.Split('E');
            string[] decimalParts = exponentParts[0].Split(decSeparator);

            // fix missing decimal point:
            if (decimalParts.Length==1) decimalParts = new string[]{exponentParts[0],"0"};

            int exponentValue = int.Parse(exponentParts[1]);

            string newNumber = decimalParts[0] + decimalParts[1];

            string result;

            if (exponentValue > 0)
            {
                result = 
                    newNumber + 
                    GetZeros(exponentValue - decimalParts[1].Length);
            }
            else // negative exponent
            {
                result = 
                    "0" + 
                    decSeparator + 
                    GetZeros(exponentValue + decimalParts[0].Length) + 
                    newNumber;

                result = result.TrimEnd('0');
            }

            if (negativeNumber)
                result = "-" + result;

            return result;
        }

        private static string GetZeros(int zeroCount)
        {
            if (zeroCount < 0) 
                zeroCount = Math.Abs(zeroCount);

            var sb = new StringBuilder();

            for (int i = 0; i < zeroCount; i++) sb.Append("0");    

            return sb.ToString();
        }
        
    }
}
