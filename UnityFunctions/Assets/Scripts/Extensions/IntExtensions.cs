using System;
using System.Globalization;
using System.Text;

namespace Extensions
{
    internal static class IntExtensions
    {
        internal static DateTime FromUnixTimestamp(this long stamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return dtDateTime.AddSeconds(stamp);
        }
        internal static string PrefixZero(this int i, int length)
        {
            return i.ToString().PadLeft(length, '0');
        }
        internal static string ToLabel(this int num)
        {
            var sign = num >= 0 ? "" : "-";
            var abs = num < 0 ? num * -1 : num;
            if (abs < 999) return sign + abs;
            var numToStr = abs.ToString(CultureInfo.InvariantCulture);
            var j = 0;
            var sb = new StringBuilder();

            for (var i = numToStr.Length - 1; i >= 0; --i)
            {
                var curr = numToStr[i];
                sb.Insert(0, curr);
                ++j;
                if (j % 3 == 0 && i != 0) sb.Insert(0, ' ');
            }
            return sign + sb;
        }

        internal static string ToTime(this Int64 n)
        {
            if (n < 1000) return n + " ms";
            if (n < 60000) return Math.Round(n / 1000.0, 2) + " seconds";
            if (n < 3600000) return Math.Round(n / 60000.0, 2) + " minutes";
            return Math.Round(n / 3600000.0, 2) + " hours";
        }
        internal static int Sign(this int i) { return Math.Sign(i); }
        internal static bool IsOneOf(this int n, int a) { return n == a; }
        internal static bool IsOneOf(this int n, int a, int b) { return n == a || n == b; }
        internal static bool IsOneOf(this int n, int a, int b, int c) { return n == a || n == b || n == c; }
        internal static bool IsOneOf(this int n, int a, int b, int c, int d) { return n == a || n == b || n == c || n == d; }
//        internal static bool IsOneOf(this int n, params int[] args) { return args.Contains(n); }
        internal static int Clamp(this int value, int min, int max)
        {
            if (value < min)
            {
                value = min;
                return value;
            }
            if (value > max)
            {
                value = max;
            }
            return value;
        }
        internal static int Abs(this int n)
        {
            return n >= 0 ? n : n * -1;
        }
        internal static int NoMoreThan(this int a, int b) { return a > b ? b : a; }
        internal static int NoLessThan(this int a, int b) { return a < b ? b : a; }


    }

}