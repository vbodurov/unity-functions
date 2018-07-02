using System;
using System.Globalization;
using System.Text;
using System.Threading;
using UnityEngine;
using static Unianio.fun;

namespace Extensions
{
    internal static class SingleExtensions
    {
        internal const double Epsilon = 0.000001;

        internal static Vector3 ToVector3(this float[] arr)
        {
            if (arr == null || arr.Length == 0) return Vector3.zero;
            if (arr.Length == 1) return new Vector3(arr[0], 0, 0);
            if (arr.Length == 2) return new Vector3(arr[0], arr[1], 0);
            return new Vector3(arr[0], arr[1], arr[2]);
        }
        internal static float ToNow(this float time)
        {
            return Time.time - time;
        }
        /*
        var xMin = -0.5;
        var xMax  = 2.0; 
        */
        internal static float FromRangeTo01(this float xOfRange, double xMin, double xMax)
        {
            var diff = xMax - xMin;
            return (float)(Math.Abs(diff) < 0.0000001 ? 1.0 : (xOfRange - xMin) / diff);
        }
        internal static float From01ToRange(this float x01, double xMin, double xMax)
        {
            return (float)((xMax - xMin) * x01 + xMin);
        }
        internal static float FromM11ToRange(this float x01, double xMin, double xMax)
        {
            return (float)((xMax - xMin) * x01 * 0.5 + xMin + (xMax - xMin) * 0.5);
        }
        internal static float From01To10(this float x)
        {
            return 1f - x;
        }
        internal static float FromMin11To01(this float x)
        {
            return x * 0.5f + 0.5f;
        }
        internal static float From01ToMin11(this float x)
        {
            return 2 * x - 1f;
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
            return ((double)n).IsEqual(m, Epsilon);
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
        internal static float ApplyFunc(this float n, Func<double, double> fn)
        {
            if (fn == null) return n; // no function is considered linear identity function
            return (float)fn(n);
        }
        internal static float AbsApplyFunc(this float n, Func<double, double> fn)
        {
            if (fn == null) return n; // no function is considered linear identity function
            return (float)fn(Abs(n)) * Sign(n);
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

        internal static float WithFunc(this float n, Func<double, double> func)
        {
            return (float)func(n);
        }
        internal static float Of01Range(this float progress, double min, double max)
        {
            return (float)(min + progress * (max - min));
        }
        internal static float OfM11Range(this float progress, double min, double max)
        {
            progress = 0.5f * progress + 0.5f;
            return (float)(min + progress * (max - min));
        }
        internal static bool IsBetween(this float n, double a, double b)
        {
            return n >= min(a, b) && n <= max(a, b);
        }
        internal static float Abs(this float n)
        {
            return n >= 0 ? n : n * -1;
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
        internal static float GoTowards(this float from, double to, double step)
        {
            var diff = to - from;
            if (diff.Abs() <= step) return (float)to;
            var sign = diff < 0 ? -1 : 1;
            return from + (float)step * sign;
        }
        internal static float GoTowardsWithin360(this float from, double to, double step)
        {
            var diff = to - from;
            if (diff.Abs() >= 180)
            {
                if (to.Abs() >= from.Abs())
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
        internal static float ApproachByRatio(this float from, double to, double ratio)
        {
            var distance = to - from;
            if (distance.IsEqual(0, 0.0001f))
            {
                return (float)to;
            }
            return (float)(distance * ratio) + from;
        }
        internal static float ClampMin11(this float value)
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
        internal static float IfConditionTransform(this float x, Func<float, bool> condition, Func<float, float> transform)
        {
            if (condition(x))
            {
                return transform(x);
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
            if (a < 0)
            {
                return b < a ? b : a;
            }
            return b > a ? b : a;
        }
        internal static Color ToColor01(this float n, Color if0, Color if1)
        {
            var x = n.Clamp01();
            return new Color(
                        lerp(if0.r, if1.r, x),
                        lerp(if0.g, if1.g, x),
                        lerp(if0.b, if1.b, x),
                        lerp(if0.a, if1.a, x)
                    );
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
            if (decimalParts.Length == 1) decimalParts = new string[] { exponentParts[0], "0" };

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
