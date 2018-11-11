using System;
using Extensions;
using UnityEngine;

namespace Utils
{
    // to test functions: http://cubic-bezier.com/#.26,1.66,.77,.3
    internal static class BezierFunc
    {
        const double epsilon = 0.000000001;
        private static void GetCubicCoefficients(double a, double b, double c, double d, out double c0, out double c1, out double c2, out double c3)
        {
            c0 = -a + 3 * b - 3 * c + d;
            c1 = 3 * a - 6 * b + 3 * c;
            c2 = -3 * a + 3 * b;
            c3 = a;
        }
        private static int GetQuadraticRoots(double a, double b, double c, out double r0, out double r1)
        {
            r0 = r1 = 0;

            if (Math.Abs(a - 0) < epsilon)
            {
                if (Math.Abs(b - 0) < epsilon) return 0;
                r0 = -c / b;
                return 1;
            }

            var q = b * b - 4 * a * c;
            var signQ =
                        (q > 0) ? 1
                        : q < 0 ? -1
                        : 0;

            if (signQ < 0)
            {
                return 0;
            }
            if (Math.Abs(signQ - 0) < epsilon)
            {
                r0 = -b / (2 * a);
                return 1;
            }
            var n = -b / (2 * a);
            r0 = n;
            r1 = n;
            var tmp = Math.Sqrt(q) / (2 * a);
            r0 -= tmp;
            r1 += tmp;
            return 2;
        }
        private static int GetCubicRoots(double a, double b, double c, double d, out double r0, out double r1, out double r2)
        {
            r1 = r2 = 0;
            if (Math.Abs(a - 0) < epsilon) return GetQuadraticRoots(b, c, d, out r0, out r1);

            b /= a;
            c /= a;
            d /= a;

            var q = (b * b - 3 * c) / 9.0;
            var qCubed = q * q * q;
            var r = (2 * b * b * b - 9 * b * c + 27 * d) / 54.0;

            var diff = qCubed - r * r;
            if (diff >= 0)
            {
                if (Math.Abs(q - 0) < epsilon)
                {
                    r0 = 0.0;
                    return 1;
                }

                var theta = Math.Acos(r / Math.Sqrt(qCubed));
                var qSqrt = Math.Sqrt(q); // won't change

                r0 = -2 * qSqrt * Math.Cos(theta / 3.0) - b / 3.0;
                r1 = -2 * qSqrt * Math.Cos((theta + 2 * Math.PI) / 3.0) - b / 3.0;
                r2 = -2 * qSqrt * Math.Cos((theta + 4 * Math.PI) / 3.0) - b / 3.0;

                return 3;
            }
            var tmp = Math.Pow(Math.Sqrt(-diff) + Math.Abs(r), 1 / 3.0);
            var rSign = (r > 0) ? 1 : r < 0 ? -1 : 0;
            r0 = -rSign * (tmp + q / tmp) - b / 3.0;
            return 1;
        }
        internal static double GetY(double x, double bx, double by, double cx, double cy)
        {
            return GetY(x, 0, 0, bx, by, cx, cy, 1, 1);
        }
        private static double GetSingleValue(double t, double a, double b, double c, double d)
        {
            return (t * t * (d - a) + 3 * (1 - t) * (t * (c - a) + (1 - t) * (b - a))) * t + a;
        }
        internal static double GetY(double x, double ax, double ay, double bx, double by, double cx, double cy, double dx, double dy)
        {
            if (ax < dx)
            {
                if (x <= ax + epsilon) return ay;
                if (x >= dx - epsilon) return dy;
            }
            else
            {
                if (x >= ax + epsilon) return ay;
                if (x <= dx - epsilon) return dy;
            }
            double c0, c1, c2, c3;
            GetCubicCoefficients(ax, bx, cx, dx, out c0, out c1, out c2, out c3);

            double r0, r1, r2;
            // x(t) = a*t^3 + b*t^2 + c*t + d
            var rootsLength = GetCubicRoots(c0, c1, c2, c3 - x, out r0, out r1, out r2);
            var time = Double.NaN;
            if (rootsLength == 0)
                time = 0;
            else if (rootsLength == 1)
                time = r0;
            else
            {


                for (var i = 0; i < rootsLength; ++i)
                {
                    var root = i == 0 ? r0 : i == 1 ? r1 : r2;
                    if (0 <= root && root <= 1)
                    {
                        time = root;
                        break;
                    }
                }
            }

            return Double.IsNaN(time) ? Double.NaN : GetSingleValue(time, ay, by, cy, dy);
        }

        internal static Vector2 GetPointQuadratic2D(double progress, Vector2 start, Vector2 control, Vector2 end)
        {
            // https://en.wikipedia.org/wiki/B%C3%A9zier_curve#Quadratic_B.C3.A9zier_curves
            // quadratic bezier formula is
            // [x,gety] = 
            //      (1 - t)^2 * P0 + 
            //      2 * (1 - t) * t * P1 + 
            //      t^2 * P2
            // where 0 <= t <= 1

            float t = (float)progress.Clamp01();
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector2 p = uu * start; //first term

            p += (2 * u * t) * control; //second term

            p += tt * end; //third term

            return p;

        }


        // start = start
        // control = control 1
        // end = control 2
        // p3 = end
        internal static Vector2 GetPointCubic2D(double progress, Vector2 start, Vector2 control1, Vector2 control2, Vector2 end)
        {
            // https://en.wikipedia.org/wiki/B%C3%A9zier_curve#Cubic_B.C3.A9zier_curves
            // cubic bezier formula is
            // [x,gety] = 
            //      (1 - t)^3 * P0 + 
            //      3 * (1 - t)^2 * t * P1 + 
            //      3 * (1 - t) * t^2 * P2 +
            //      t^3 * P3
            // where 0 <= t <= 1

            float t = (float)progress.Clamp01();
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector2 p = uuu * start; //first term

            p += (3 * uu * t) * control1; //second term
            p += (3 * u * tt) * control2; //third term
            p += ttt * end; //fourth term

            return p;

        }
        // start = start
        // control = control
        // end = end
        internal static Vector2 GetPointQuadratic2D(double progress, ref Vector2 start, ref Vector2 control, ref Vector2 end)
        {
            // https://en.wikipedia.org/wiki/B%C3%A9zier_curve#Quadratic_B.C3.A9zier_curves
            // quadratic bezier formula is
            // [x,gety] = 
            //      (1 - t)^2 * P0 + 
            //      2 * (1 - t) * t * P1 + 
            //      t^2 * P2
            // where 0 <= t <= 1

            float t = (float)progress.Clamp01();
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector2 p = uu * start; //first term

            p += (2 * u * t) * control; //second term

            p += tt * end; //third term

            return p;

        }

        internal static Vector2 GetPointCubic2D(double progress, in Vector2 start, in Vector2 control1, in Vector2 control2, in Vector2 end)
        {
            // cubic bezier formula is
            // [x,gety] = 
            //      (1 - t)^3 * P0 + 
            //      3 * (1 - t)^2 * t * P1 + 
            //      3 * (1 - t) * t^2 * P2 +
            //      t^3 * P3
            // where 0 <= t <= 1

            float t = (float)progress.Clamp01();
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector2 p = uuu * start; //first term

            p += (3 * uu * t) * control1; //second term
            p += (3 * u * tt) * control2; //third term
            p += ttt * end; //fourth term

            return p;

        }
        // start = start
        // control = control
        // end = end
        internal static Vector3 GetPointQuadratic(double progress, Vector3 start, Vector3 control, Vector3 end)
        {
            // https://en.wikipedia.org/wiki/B%C3%A9zier_curve#Quadratic_B.C3.A9zier_curves
            // quadratic bezier formula is
            // [x,gety] = 
            //      (1 - t)^2 * P0 + 
            //      2 * (1 - t) * t * P1 + 
            //      t^2 * P2
            // where 0 <= t <= 1

            float t = (float)progress.Clamp01();
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 p = uu * start; //first term

            p += (2 * u * t) * control; //second term

            p += tt * end; //third term

            return p;

        }
        internal static Vector3 GetPointCubic(double progress, Vector3 start, Vector3 control1, Vector3 control2, Vector3 end)
        {
            // cubic bezier formula is
            // [x,gety] = 
            //      (1 - t)^3 * P0 + 
            //      3 * (1 - t)^2 * t * P1 + 
            //      3 * (1 - t) * t^2 * P2 +
            //      t^3 * P3
            // where 0 <= t <= 1

            float t = (float)progress.Clamp01();
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * start; //first term

            p += (3 * uu * t) * control1; //second term
            p += (3 * u * tt) * control2; //third term
            p += ttt * end; //fourth term

            return p;

        }

        // start = start
        // control = control
        // end = end
        internal static Vector3 GetPointQuadratic(double progress, in Vector3 start, in Vector3 control, in Vector3 end)
        {
            // https://en.wikipedia.org/wiki/B%C3%A9zier_curve#Quadratic_B.C3.A9zier_curves
            // quadratic bezier formula is
            // [x,gety] = 
            //      (1 - t)^2 * P0 + 
            //      2 * (1 - t) * t * P1 + 
            //      t^2 * P2
            // where 0 <= t <= 1

            float t = (float)progress.Clamp01();
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 p = uu * start; //first term

            p += (2 * u * t) * control; //second term

            p += tt * end; //third term

            return p;

        }

        internal static Vector3 GetPointCubic(double progress, in Vector3 start, in Vector3 control1, in Vector3 control2, in Vector3 end)
        {
            // cubic bezier formula is
            // [x,gety] = 
            //      (1 - t)^3 * P0 + 
            //      3 * (1 - t)^2 * t * P1 + 
            //      3 * (1 - t) * t^2 * P2 +
            //      t^3 * P3
            // where 0 <= t <= 1

            float t = (float)progress.Clamp01();
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * start; //first term

            p += (3 * uu * t) * control1; //second term
            p += (3 * u * tt) * control2; //third term
            p += ttt * end; //fourth term

            return p;

        }

        internal static float GetLengthCubic(Vector3 start, Vector3 control1, Vector3 control2, Vector3 end)
        {
            return GetLengthCubic(in start, in control1, in control2, in end, 16);
        }
        internal static float GetLengthCubic(Vector3 start, Vector3 control1, Vector3 control2, Vector3 end, int segments)
        {
            return GetLengthCubic(in start, in control1, in control2, in end, 16);
        }
        internal static float GetLengthCubic(in Vector3 start, in Vector3 control1, in Vector3 control2, in Vector3 end)
        {
            return GetLengthCubic(in start, in control1, in control2, in end, 16);
        }
        internal static float GetLengthCubic(in Vector3 start, in Vector3 control1, in Vector3 control2, in Vector3 end, int segments)
        {
            if (segments < 1 || segments > 2048)
                throw new ArgumentException("Segments must be between 1 and 2048");

            var distance = 0f;
            Vector3 prev = new Vector3(0, 0, 0);
            for (var i = 0; i <= segments; ++i)
            {
                var x = i / segments;

                var curr = GetPointCubic(x, in start, in control1, in control2, in end);

                if (i > 0)
                {
                    distance += DistanceBetween(in curr, in prev);
                }

                prev = curr;
            }

            return distance;
        }


        internal static float GetLengthQuadratic(Vector3 start, Vector3 control, Vector3 end)
        {
            return GetLengthQuadratic(in start, in control, in end, 16);
        }
        internal static float GetLengthQuadratic(Vector3 start, Vector3 control, Vector3 end, int segments)
        {
            return GetLengthQuadratic(in start, in control, in end, 16);
        }
        internal static float GetLengthQuadratic(in Vector3 start, in Vector3 control, in Vector3 end)
        {
            return GetLengthQuadratic(in start, in control, in end, 16);
        }
        internal static float GetLengthQuadratic(in Vector3 start, in Vector3 control, in Vector3 end, int segments)
        {
            if (segments < 1 || segments > 2048)
                throw new ArgumentException("Segments must be between 1 and 2048");

            var distance = 0f;
            Vector3 prev = new Vector3(0, 0, 0);
            for (var i = 0; i <= segments; ++i)
            {
                var x = i / segments;

                var curr = GetPointQuadratic(x, in start, in control, in end);

                if (i > 0)
                {
                    distance += DistanceBetween(in curr, in prev);
                }

                prev = curr;
            }

            return distance;
        }

        private static float DistanceBetween(in Vector3 a, in Vector3 b)
        {
            var vectorX = a.x - b.x;
            var vectorY = a.y - b.y;
            var vectorZ = a.z - b.z;
            return (float)Math.Sqrt((((double)vectorX * (double)vectorX) + ((double)vectorY * (double)vectorY)) + ((double)vectorZ * (double)vectorZ));
        }

        internal static Vector3 GetFirstDerivativeQuadratic(double progress, in Vector3 start, in Vector3 control, in Vector3 end)
        {
            var t = Mathf.Clamp01((float)progress);
            var n = 1f - t;
            return 2f * n * (control - start) + 2f * t * (end - control);
        }
        internal static Vector3 GetFirstDerivativeCubic(double progress, in Vector3 start, in Vector3 control1, in Vector3 control2, in Vector3 end)
        {
            var t = Mathf.Clamp01((float)progress);
            var n = 1f - t;
            return 3f * n * n * (control1 - start) + 6f * n * t * (control2 - control1) + 3f * t * t * (end - control2);
        }
    }

}