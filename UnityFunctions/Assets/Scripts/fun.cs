using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using Extensions;
using Utils;

namespace Unianio
{
    internal static class fun
    {
        internal const float RTD = (float)(180 / Math.PI);
        internal const float DTR = (float)(Math.PI / 180);

        internal static float framesPerSec = 90f;
        internal static float smoothDeltaTime = 1 / 90f;
        private static int _smoothDeltaTimeFrame = 0;
        internal static void frame()
        {
            if (_smoothDeltaTimeFrame < 20) ++_smoothDeltaTimeFrame;
            smoothDeltaTime = statistics.Average(smoothDeltaTime, Time.deltaTime, _smoothDeltaTimeFrame);
            framesPerSec = 1 / smoothDeltaTime;
        }
        internal static float framesIf90Fps(double f)
        {
            return (framesPerSec * (float)f) / 90f;
        }
        internal static int numFramesIf90Fps(double f)
        {
            return (int)Math.Round((framesPerSec * f) / 90.0);
        }
        internal static int abs(int n) { return n < 0 ? n * -1 : n; }
        internal static float abs(float n) { return n < 0 ? n * -1 : n; }
        internal static float abs(double n) { return n < 0 ? (float)n * -1 : (float)n; }
        internal static int sign(double n) { return n < 0 ? -1 : 1; }
        internal static int min(int a, int b) { return a > b ? b : a; }
        internal static int max(int a, int b) { return a < b ? b : a; }
        internal static float min(double a, double b) { return a > b ? (float)b : (float)a; }
        internal static float max(double a, double b) { return a < b ? (float)b : (float)a; }
        internal static float sqrt(double n) { return (float)Math.Sqrt(n); }
        internal static float sin(double n) { return (float)Math.Sin(n); }
        internal static float cos(double n) { return (float)Math.Cos(n); }
        internal static float atan2(double a, double b) { return (float)Math.Atan2(a, b); }
        internal static float pow(double n, double p) { return (float)Math.Pow(n, p); }
        internal static float tan(double n) { return (float)Math.Tan(n); }
        internal static Vector3 lerp(Vector3 a, Vector3 b, double t)
        {
            t = t.Clamp01();
            return new Vector3(a.x + (b.x - a.x) * (float)t, a.y + (b.y - a.y) * (float)t, a.z + (b.z - a.z) * (float)t);
        }
        internal static Vector3 lerpUnclamped(Vector3 a, Vector3 b, double t)
        {
            return new Vector3(a.x + (b.x - a.x) * (float)t, a.y + (b.y - a.y) * (float)t, a.z + (b.z - a.z) * (float)t);
        }

        internal static Vector2 lerp2d(in Vector2 a, in Vector2 b, double t)
        {
            t = t.Clamp01();
            return new Vector2(a.x + (b.x - a.x) * (float)t, a.y + (b.y - a.y) * (float)t);
        }
        internal static Vector2 lerp2d(Vector2 a, Vector2 b, double t)
        {
            t = t.Clamp01();
            return new Vector2(a.x + (b.x - a.x) * (float)t, a.y + (b.y - a.y) * (float)t);
        }
        internal static Vector3 lerp(in Vector3 a, in Vector3 b, double t)
        {
            t = t.Clamp01();
            return new Vector3(a.x + (b.x - a.x) * (float)t, a.y + (b.y - a.y) * (float)t, a.z + (b.z - a.z) * (float)t);
        }
        internal static Vector3 lerpUnclamped(in Vector3 a, in Vector3 b, double t)
        {
            return new Vector3(a.x + (b.x - a.x) * (float)t, a.y + (b.y - a.y) * (float)t, a.z + (b.z - a.z) * (float)t);
        }
        internal static float lerp(double a, double b, double t)
        {
            return (float)(a + (b - a) * t.Clamp01());
        }
        internal static float lerpUnclamped(double a, double b, double t)
        {
            return (float)(a + (b - a) * t);
        }
        internal static float lerp(float a, float b, float t)
        {
            return (a + (b - a) * t.Clamp01());
        }
        internal static float lerpUnclamped(float a, float b, float t)
        {
            return (a + (b - a) * t);
        }
        internal static float uniFun(double x, Func<double, double> f0,
            double f1StartX, Func<double, double> f1)
        {
            double y;
            if (x < f1StartX) y = f0(x);
            else y = f1(x);
            return (float)Math.Round(y);
        }
        internal static Transform[] getGameObjectSequence(string name)
        {
            var list = new List<Transform>();
            var go = GameObject.Find(name);
            if (go != null) list.Add(go.transform);
            var i = 0;
            var numNotFound = 0;
            while (true)
            {
                go = GameObject.Find(name + " (" + i + ")");
                if (go != null)
                {
                    numNotFound = 0;
                    list.Add(go.transform);
                }
                else
                {
                    ++numNotFound;
                }
                if (numNotFound > 3) break;
                ++i;
            }
            return list.ToArray();
        }
        internal static float uniFun(double x, Func<double, double> f0,
            double f1StartX, Func<double, double> f1,
            double f2StartX, Func<double, double> f2)
        {
            double y;
            if (x < f1StartX) y = f0(x);
            else if (x < f2StartX) y = f1(x);
            else y = f2(x);
            return (float)Math.Round(y);
        }
        internal static float uniFun(double x, Func<double, double> f0,
            double f1StartX, Func<double, double> f1,
            double f2StartX, Func<double, double> f2,
            double f3StartX, Func<double, double> f3)
        {
            double y;
            if (x < f1StartX) y = f0(x);
            else if (x < f2StartX) y = f1(x);
            else if (x < f3StartX) y = f2(x);
            else y = f3(x);
            return (float)Math.Round(y);
        }
        internal static float uniFun(double x, Func<double, double> f0,
            double f1StartX, Func<double, double> f1,
            double f2StartX, Func<double, double> f2,
            double f3StartX, Func<double, double> f3,
            double f4StartX, Func<double, double> f4)
        {
            double y;
            if (x < f1StartX) y = f0(x);
            else if (x < f2StartX) y = f1(x);
            else if (x < f3StartX) y = f2(x);
            else if (x < f4StartX) y = f3(x);
            else y = f4(x);
            return (float)Math.Round(y);
        }
        internal static float uniFun(double x, Func<double, double> f0,
            double f1StartX, Func<double, double> f1,
            double f2StartX, Func<double, double> f2,
            double f3StartX, Func<double, double> f3,
            double f4StartX, Func<double, double> f4,
            double f5StartX, Func<double, double> f5)
        {
            double y;
            if (x < f1StartX) y = f0(x);
            else if (x < f2StartX) y = f1(x);
            else if (x < f3StartX) y = f2(x);
            else if (x < f4StartX) y = f3(x);
            else if (x < f5StartX) y = f4(x);
            else y = f5(x);
            return (float)Math.Round(y);
        }
        internal static float uniFun01(double x, Func<double, double> f0,
            double f1StartX, Func<double, double> f1)
        {
            double y;
            if (x < f1StartX) y = f0(x.FromRangeTo01(0, f1StartX));
            else y = f1(x.FromRangeTo01(f1StartX, 1));
            return (float)Math.Round(y);
        }
        internal static float uniFun01(double x, Func<double, double> f0,
           double f1StartX, Func<double, double> f1,
           double f2StartX, Func<double, double> f2)
        {
            double y;
            if (x < f1StartX) y = f0(x.FromRangeTo01(0, f1StartX));
            else if (x < f2StartX) y = f1(x.FromRangeTo01(f1StartX, f2StartX));
            else y = f2(x.FromRangeTo01(f2StartX, 1));
            return (float)Math.Round(y);
        }
        internal static float uniFun01(double x, Func<double, double> f0,
           double f1StartX, Func<double, double> f1,
           double f2StartX, Func<double, double> f2,
           double f3StartX, Func<double, double> f3)
        {
            double y;
            if (x < f1StartX) y = f0(x.FromRangeTo01(0, f1StartX));
            else if (x < f2StartX) y = f1(x.FromRangeTo01(f1StartX, f2StartX));
            else if (x < f3StartX) y = f2(x.FromRangeTo01(f2StartX, f3StartX));
            else y = f3(x.FromRangeTo01(f3StartX, 1));
            return (float)Math.Round(y);
        }
        internal static float uniFun01(double x, Func<double, double> f0,
           double f1StartX, Func<double, double> f1,
           double f2StartX, Func<double, double> f2,
           double f3StartX, Func<double, double> f3,
           double f4StartX, Func<double, double> f4)
        {
            double y;
            if (x < f1StartX) y = f0(x.FromRangeTo01(0, f1StartX));
            else if (x < f2StartX) y = f1(x.FromRangeTo01(f1StartX, f2StartX));
            else if (x < f3StartX) y = f2(x.FromRangeTo01(f2StartX, f3StartX));
            else if (x < f4StartX) y = f3(x.FromRangeTo01(f3StartX, f4StartX));
            else y = f4(x.FromRangeTo01(f4StartX, 1));
            return (float)Math.Round(y);
        }
        internal static float uniFun01(double x, Func<double, double> f0,
           double f1StartX, Func<double, double> f1,
           double f2StartX, Func<double, double> f2,
           double f3StartX, Func<double, double> f3,
           double f4StartX, Func<double, double> f4,
           double f5StartX, Func<double, double> f5)
        {
            double y;
            if (x < f1StartX) y = f0(x.FromRangeTo01(0, f1StartX));
            else if (x < f2StartX) y = f1(x.FromRangeTo01(f1StartX, f2StartX));
            else if (x < f3StartX) y = f2(x.FromRangeTo01(f2StartX, f3StartX));
            else if (x < f4StartX) y = f3(x.FromRangeTo01(f3StartX, f4StartX));
            else if (x < f5StartX) y = f4(x.FromRangeTo01(f4StartX, f5StartX));
            else y = f5(x.FromRangeTo01(f5StartX, 1));
            return (float)Math.Round(y);
        }






        internal const string HolderSuffix = "_Holder";
        internal const string HandleSuffix = "_Handle";
        internal const float PI = (float)Math.PI;
        internal const float TwoPI = (float)Math.PI * 2;
        internal const float HalfPI = (float)(Math.PI * 0.5);
        internal const float DegInRadians90 = (float)(Math.PI * 0.5);
        internal const float DegInRadians180 = (float)(Math.PI);
        internal static readonly float[] DotProductByDegree = { 1f, 0.9998477f, 0.9993908f, 0.9986295f, 0.9975641f, 0.9961947f, 0.9945219f, 0.9925461f, 0.9902681f, 0.9876884f, 0.9848077f, 0.9816272f, 0.9781476f, 0.9743701f, 0.9702957f, 0.9659258f, 0.9612617f, 0.9563048f, 0.9510565f, 0.9455186f, 0.9396926f, 0.9335804f, 0.9271839f, 0.9205049f, 0.9135454f, 0.9063078f, 0.8987941f, 0.8910065f, 0.8829476f, 0.8746197f, 0.8660254f, 0.8571673f, 0.8480481f, 0.8386706f, 0.8290376f, 0.8191521f, 0.809017f, 0.7986355f, 0.7880108f, 0.7771459f, 0.7660445f, 0.7547096f, 0.7431449f, 0.7313538f, 0.7193398f, 0.7071067f, 0.6946584f, 0.6819984f, 0.6691306f, 0.656059f, 0.6427876f, 0.6293204f, 0.6156615f, 0.601815f, 0.5877852f, 0.5735765f, 0.5591929f, 0.5446391f, 0.5299193f, 0.5150381f, 0.5f, 0.4848096f, 0.4694716f, 0.4539905f, 0.4383711f, 0.4226182f, 0.4067366f, 0.3907312f, 0.3746066f, 0.3583679f, 0.3420201f, 0.3255681f, 0.309017f, 0.2923717f, 0.2756374f, 0.258819f, 0.2419218f, 0.224951f, 0.2079117f, 0.1908091f, 0.1736482f, 0.1564345f, 0.1391731f, 0.1218693f, 0.1045284f, 0.08715588f, 0.06975645f, 0.05233604f, 0.03489941f, 0.01745242f, 0f, -0.01745248f, -0.03489947f, -0.05233586f, -0.06975651f, -0.0871557f, -0.1045285f, -0.1218693f, -0.139173f, -0.1564344f, -0.1736481f, -0.1908089f, -0.2079116f, -0.224951f, -0.2419218f, -0.258819f, -0.2756375f, -0.2923716f, -0.3090171f, -0.3255682f, -0.3420202f, -0.3583679f, -0.3746065f, -0.3907312f, -0.4067366f, -0.4226184f, -0.4383712f, -0.4539905f, -0.4694716f, -0.4848095f, -0.5000001f, -0.515038f, -0.5299193f, -0.5446391f, -0.5591928f, -0.5735766f, -0.5877852f, -0.601815f, -0.6156615f, -0.6293204f, -0.6427877f, -0.656059f, -0.6691307f, -0.6819984f, -0.6946582f, -0.7071067f, -0.7193398f, -0.7313538f, -0.7431448f, -0.7547096f, -0.7660444f, -0.777146f, -0.7880107f, -0.7986356f, -0.8090171f, -0.819152f, -0.8290374f, -0.8386706f, -0.8480481f, -0.8571672f, -0.8660253f, -0.8746197f, -0.8829476f, -0.8910065f, -0.8987941f, -0.9063078f, -0.9135456f, -0.9205048f, -0.9271837f, -0.9335804f, -0.9396925f, -0.9455187f, -0.9510566f, -0.9563048f, -0.9612616f, -0.9659257f, -0.9702957f, -0.9743701f, -0.9781476f, -0.9816272f, -0.9848078f, -0.9876882f, -0.9902682f, -0.9925461f, -0.9945219f, -0.9961947f, -0.9975641f, -0.9986296f, -0.9993908f, -0.9998477f, -1f };
        internal static readonly Color HandleColor = new Color(0.75f, 0.0f, 0.75f, 1f);

        internal static bool angleOfDotProductIsLessThan(double dotProduct, double degreesAngle)
        {
            var degrees = (int)Math.Round(degreesAngle);
            if (degrees < 0) degrees *= -1;
            if (degrees > 360) degrees = degrees % 360;
            if (degrees > 180) degrees = 180 - (degrees % 180);
            return dotProduct > DotProductByDegree[degrees];
        }
        internal static bool angleOfDotProductIsMoreThan(double dotProduct, double degreesAngle)
        {
            var degrees = (int)Math.Round(degreesAngle);
            if (degrees < 0) degrees *= -1;
            if (degrees > 360) degrees = degrees % 360;
            if (degrees > 180) degrees = 180 - (degrees % 180);
            return dotProduct < DotProductByDegree[degrees];
        }
        internal static bool angleIsLessThan(in Vector3 dir1, in Vector3 dir2, double degreesAngle)
        {
            var degrees = (int)Math.Round(degreesAngle);
            if (degrees < 0) degrees *= -1;
            if (degrees > 360) degrees = degrees % 360;
            if (degrees > 180) degrees = 180 - (degrees % 180);
            return dot(in dir1, in dir2) > DotProductByDegree[degrees];
        }
        internal static bool angleIsMoreThan(in Vector3 dir1, in Vector3 dir2, double degreesAngle)
        {
            var degrees = (int)Math.Round(degreesAngle);
            if (degrees < 0) degrees *= -1;
            if (degrees > 360) degrees = degrees % 360;
            if (degrees > 180) degrees = 180 - (degrees % 180);
            return dot(in dir1, in dir2) < DotProductByDegree[degrees];
        }
        internal static bool angleIsLessThan(Vector3 dir1, Vector3 dir2, double degreesAngle)
        {
            var degrees = (int)Math.Round(degreesAngle);
            if (degrees < 0) degrees *= -1;
            if (degrees > 360) degrees = degrees % 360;
            if (degrees > 180) degrees = 180 - (degrees % 180);
            return dot(in dir1, in dir2) > DotProductByDegree[degrees];
        }
        internal static bool angleIsMoreThan(Vector3 dir1, Vector3 dir2, double degreesAngle)
        {
            var degrees = (int)Math.Round(degreesAngle);
            if (degrees < 0) degrees *= -1;
            if (degrees > 360) degrees = degrees % 360;
            if (degrees > 180) degrees = 180 - (degrees % 180);
            return dot(in dir1, in dir2) < DotProductByDegree[degrees];
        }
        internal static T onesIn<T>(int numberTimes, T state) where T : class
        {
            if (randomBool(1.0 / max(numberTimes, 1))) return state;
            return null;
        }
        internal static readonly Color colorRed = new Color(1f, 0.0f, 0.0f, 1f);
        internal static readonly Color colorLightRed = new Color(1f, 0.5f, 0.5f, 1f);
        internal static readonly Color colorOrange = new Color(1f, 0.5f, 0.0f, 1f);
        internal static readonly Color colorYellow = new Color(1f, 0.9215686f, 0.01568628f, 1f);
        internal static readonly Color colorLightYellow = new Color(1f, 0.9f, 0.5f, 1f);
        internal static readonly Color colorGreen = new Color(0.0f, 1f, 0.0f, 1f);
        internal static readonly Color colorDarkGreen = new Color(0.0f, 0.5f, 0.0f, 1f);
        internal static readonly Color colorCyan = new Color(0.0f, 1f, 1f, 1f);
        internal static readonly Color colorBlue = new Color(0.0f, 0.0f, 1f, 1f);
        internal static readonly Color colorLightGreen = new Color(0.2f, 0.8f, 0.2f, 1f);
        internal static readonly Color colorMagenta = new Color(1f, 0.0f, 1f, 1f);
        internal static readonly Color colorWhite = new Color(1f, 1f, 1f, 1f);
        internal static readonly Color colorBlack = new Color(0.0f, 0.0f, 0.0f, 1f);
        internal static readonly Color colorGrey = new Color(0.5f, 0.5f, 0.5f, 1f);
        internal static readonly Color colorBrown = new Color(0.6f, 0.3f, 0.0f, 1f);
        internal static readonly Color colorPink = new Color(1.00f, 0.75f, 0.80f, 1f);

        internal static Color rgba(double r, double g, double b, double a) { return new Color((float)r, (float)g, (float)b, (float)a); }
        internal static Vector3 V3(double n) { return new Vector3((float)n, (float)n, (float)n); }
        internal static Vector3 V3(double x, double y, double z) { return new Vector3((float)x, (float)y, (float)z); }
        internal static Vector2 V2(double n) { return new Vector2((float)n, (float)n); }
        internal static Vector2 V2(double x, double y) { return new Vector2((float)x, (float)y); }
        internal static float max(double a, double b, double c) { return max(max(a, b), c); }
        internal static float max(double a, double b, double c, double d) { return max(max(max(a, b), c), d); }
        internal static float max(double a, double b, double c, double d, double e) { return max(max(max(max(a, b), c), d), e); }
        internal static float max(double a, double b, double c, double d, double e, double f) { return max(max(max(max(max(a, b), c), d), e), f); }
        internal static float min(double a, double b, double c) { return min(min(a, b), c); }
        internal static float min(double a, double b, double c, double d) { return min(min(min(a, b), c), d); }
        internal static float min(double a, double b, double c, double d, double e) { return min(min(min(min(a, b), c), d), e); }
        internal static float min(double a, double b, double c, double d, double e, double f) { return min(min(min(min(min(a, b), c), d), e), f); }
        internal static float maxOfTheAbs(double a, double b)
        {
            return (float)(abs(a) > abs(b) ? a : b);
        }

        internal static float degByOppAdj(double opposite, double adjucent) { return (float)Math.Atan2(opposite, adjucent) * RTD; }
//        internal static float noise(double x) { return NoiseGenerator.Generate(x); }
        internal static float exp(double n) { return (float)(Math.Exp(n)); }
        internal static float avg(double a, double b) { return lerp(a, b, 0.5); }
        internal static float avg(double lastAverage, double current, int count)
        {
            return (count <= 1.0f) ? (float)current : ((float)lastAverage * (count - 1.0f) + (float)current) / count;
        }
        internal static float avg(double lastAverage, double current, int count, int countLimit)
        {
            if (count > countLimit) count = countLimit;
            return avg(lastAverage, current, count);
        }
        internal static float valueOverTime01(double iniTime, double duration, Func<double, double> func)
        {
            duration = duration.Abs();
            if (duration < 0.0001) duration = 0.0001;
            var x = ((Time.time - iniTime) / duration).Clamp01();
            return (float)func(x);
        }
        internal static float valueOverTimeEndless(double iniTime, double duration, Func<double, double> func)
        {
            duration = duration.Abs();
            if (duration < 0.0001) duration = 0.0001;
            var x = ((Time.time - iniTime) / duration);
            return (float)func(x);
        }
        

        internal static double increaseChanceForExtremes1(double x) { return bezier(x, 0.25, 0.00, 0.75, 1.00); }
        internal static double increaseChanceForExtremes2(double x) { return bezier(x, 0.50, 0.00, 0.50, 1.00); }
        internal static double increaseChanceForExtremes3(double x) { return bezier(x, 0.75, 0.00, 0.25, 1.00); }
        internal static double increaseChanceForExtremes4(double x) { return bezier(x, 0.90, 0.00, 0.10, 1.00); }
        internal static double funFastSlowFast1(double x) { return bezier(x, 0.30, 0.70, 0.70, 0.30); }
        internal static double funFastSlowFast2(double x) { return bezier(x, 0.20, 0.80, 0.80, 0.20); }
        internal static double funFastSlowFast3(double x) { return bezier(x, 0.10, 0.90, 0.90, 0.10); }
        internal static double funSlowFastSlow1(double x) { return bezier(x, 0.70, 0.30, 0.30, 0.70); }
        internal static double funSlowFastSlow2(double x) { return bezier(x, 0.80, 0.20, 0.20, 0.80); }
        internal static double funSlowFastSlow3(double x) { return bezier(x, 0.90, 0.10, 0.10, 0.90); }
        internal static double funSlowToFast1(double x) { return pow(x, 2); }
        internal static double funSlowToFast2(double x) { return pow(x, 3); }
        internal static double funSlowToFast3(double x) { return pow(x, 4); }
        internal static double funFastToSlow1(double x) { return (1.0 - Math.Pow(1.0 - x, 2)); }
        internal static double funFastToSlow2(double x) { return (1.0 - Math.Pow(1.0 - x, 3)); }
        internal static double funFastToSlow3(double x) { return (1.0 - Math.Pow(1.0 - x, 4)); }
        internal static double funFlatTopSine1M1(double x, double flatness) // flatness = 1 ~ 1000
        {
            return sqrt((1.0 + flatness * flatness) / (1.0 + flatness * flatness * cos(x * PI) * cos(x * PI))) * cos(x * PI);
        }
        internal static double funFlatTopSineM11(double x, double flatness) // flatness = 1 ~ 1000
        {
            return sqrt((1.0 + flatness * flatness) / (1.0 + flatness * flatness * cos(x * PI - PI) * cos(x * PI - PI))) * cos(x * PI - PI);
        }
        internal static double funFlatTopSine01(double x, double flatness) // flatness = 1 ~ 1000
        {
            return 0.5 - sqrt((1.0 + flatness * flatness) / (1.0 + flatness * flatness * cos(x * PI) * cos(x * PI))) * cos(x * PI) * 0.5;
        }
        internal static double funFlatTopSine10(double x, double flatness) // flatness = 1 ~ 1000
        {
            return 0.5 + sqrt((1.0 + flatness * flatness) / (1.0 + flatness * flatness * cos(x * PI) * cos(x * PI))) * cos(x * PI) * 0.5;
        }

        internal static double funcToRange(double x01, double minNum, double maxNum, Func<double, double> func01)
        {
            var x = func01(x01);
            return x.From01ToRange(minNum, maxNum);
        }
        internal static TValue ifElseAssign<TValue>(bool ifTrue, TValue value, ref TValue thenAssign, ref TValue otherwiseAssign)
        {
            if (ifTrue) thenAssign = value;
            else otherwiseAssign = value;
            return value;
        }
        internal static void ifElseDo(bool ifTrue, Action thenDo, Action otherwiseDo)
        {
            if (ifTrue) thenDo();
            else otherwiseDo();
        }
        internal static void ifElseDo(bool ifTrue, Action thenDo, bool ifElse, Action elseDo, Action otherwiseDo)
        {
            if (ifTrue) thenDo();
            else if (ifElse) elseDo();
            else otherwiseDo();
        }
        internal static TOutput ifElse<TOutput>(bool ifTrue, TOutput then, TOutput otherwise)
        {
            return ifTrue ? then : otherwise;
        }
        internal static TOutput ifElse<TInput, TOutput>(TInput current,
            TInput ifIs, TOutput then, TOutput otherwise) where TInput : struct
        {
            return current.Equals(ifIs) ? then : otherwise;
        }
        internal static TOutput ifElse<TInput, TOutput>(TInput current,
            TInput ifIs1, TOutput then1,
            TInput ifIs2, TOutput then2, TOutput otherwise) where TInput : struct
        {
            return current.Equals(ifIs1) ? then1 : current.Equals(ifIs2) ? then2 : otherwise;
        }
        internal static TOutput ifElse<TInput, TOutput>(TInput current,
            TInput ifIs1, TOutput then1,
            TInput ifIs2, TOutput then2,
            TInput ifIs3, TOutput then3, TOutput otherwise) where TInput : struct
        {
            return current.Equals(ifIs1) ? then1 : current.Equals(ifIs2) ? then2 : current.Equals(ifIs3) ? then3 : otherwise;
        }
        internal static TOutput ifElse<TInput, TOutput>(TInput current,
            TInput ifIs1, TOutput then1,
            TInput ifIs2, TOutput then2,
            TInput ifIs3, TOutput then3,
            TInput ifIs4, TOutput then4, TOutput otherwise) where TInput : struct
        {
            return current.Equals(ifIs1) ? then1 : current.Equals(ifIs2) ? then2 : current.Equals(ifIs3) ? then3 : current.Equals(ifIs4) ? then4 : otherwise;
        }
        internal static TOutput ifElse<TInput, TOutput>(TInput current,
            TInput ifIs1, TOutput then1,
            TInput ifIs2, TOutput then2,
            TInput ifIs3, TOutput then3,
            TInput ifIs4, TOutput then4,
            TInput ifIs5, TOutput then5, TOutput otherwise) where TInput : struct
        {
            return current.Equals(ifIs1) ? then1 : current.Equals(ifIs2) ? then2 : current.Equals(ifIs3) ? then3 : current.Equals(ifIs4) ? then4 : current.Equals(ifIs5) ? then5 : otherwise;
        }
        /// <summary>
        /// Note: the dot product must be of unit vectors
        /// </summary>
        /// <returns>degrees of that dot product</returns>
        internal static float dpToDeg(double dotProduct)
        {
            return (float)(Math.Acos(dotProduct.Clamp01()) * RTD);
        }
        /// <summary>
        /// Note: the dot product must be of unit vectors
        /// </summary>
        /// <returns>radians of that dot product</returns>
        internal static float dpToRad(double dotProduct)
        {
            return (float)Math.Acos(dotProduct.Clamp01());
        }

        internal static Color lerp(Color a, Color b, double t)
        {
            t = t.Clamp01();
            return new Color(a.r + (b.r - a.r) * (float)t, a.g + (b.g - a.g) * (float)t, a.b + (b.b - a.b) * (float)t, a.a + (b.a - a.a) * (float)t);
        }
        internal static void lerp(in Vector3 a, in Vector3 b, double t, out Vector3 r)
        {
            t = t.Clamp01();
            r = new Vector3(a.x + (b.x - a.x) * (float)t, a.y + (b.y - a.y) * (float)t, a.z + (b.z - a.z) * (float)t);
        }
        internal static Vector3 lerpOnBezierControlRelToMiddle(Vector3 start, Vector3 controlRelToMiddle, Vector3 end, double t)
        {
            return BezierFunc.GetPointQuadratic(t, start, lerp(in start, in end, 0.5) + controlRelToMiddle, end);
        }
        /*
         toX0 ------
                    --------- toX1
                    --------- fromX1
         fromX0 ----
         */
        internal static float lerpPlane(double fromX0, double toX0, double fromX1, double toX1, double x01, double fromTo01)
        {
            return lerp(lerp(fromX0, toX0, x01), lerp(fromX1, toX1, x01), fromTo01);
        }
        internal static float vDist(Vector3 a, Vector3 b)
        {
            var vectorX = a.x - b.x;
            var vectorY = a.y - b.y;
            var vectorZ = a.z - b.z;
            return (float)Math.Sqrt((((double)vectorX * (double)vectorX) + ((double)vectorY * (double)vectorY)) + ((double)vectorZ * (double)vectorZ));
        }
        internal static float vDist(Transform a, Transform b)
        {
            return vDist(a.position, b.position);
        }
        internal static float vDist(in Vector3 a, in Vector3 b)
        {
            var vectorX = a.x - b.x;
            var vectorY = a.y - b.y;
            var vectorZ = a.z - b.z;
            return (float)Math.Sqrt((((double)vectorX * (double)vectorX) + ((double)vectorY * (double)vectorY)) + ((double)vectorZ * (double)vectorZ));
        }
        internal static float vDistBy(Vector3 a, Vector3 b, double by)
        {
            var vectorX = a.x - b.x;
            var vectorY = a.y - b.y;
            var vectorZ = a.z - b.z;
            return (float)(by * Math.Sqrt((((double)vectorX * (double)vectorX) + ((double)vectorY * (double)vectorY)) + ((double)vectorZ * (double)vectorZ)));
        }
        internal static float vDistSqr(Vector3 a, Vector3 b)
        {
            var vectorX = a.x - b.x;
            var vectorY = a.y - b.y;
            var vectorZ = a.z - b.z;
            return (float)((((double)vectorX * (double)vectorX) + ((double)vectorY * (double)vectorY)) + ((double)vectorZ * (double)vectorZ));
        }
        internal static float vDistSqr(in Vector3 a, in Vector3 b)
        {
            var vectorX = a.x - b.x;
            var vectorY = a.y - b.y;
            var vectorZ = a.z - b.z;
            return (float)((((double)vectorX * (double)vectorX) + ((double)vectorY * (double)vectorY)) + ((double)vectorZ * (double)vectorZ));
        }
        internal static float vHorzDist(Vector3 a, Vector3 b)
        {
            var vectorX = a.x - b.x;
            var vectorZ = a.z - b.z;
            return (float)Math.Sqrt(((double)vectorX * (double)vectorX) + ((double)vectorZ * (double)vectorZ));
        }
        internal static float vHorzDist(in Vector3 a, in Vector3 b)
        {
            var vectorX = a.x - b.x;
            var vectorZ = a.z - b.z;
            return (float)Math.Sqrt(((double)vectorX * (double)vectorX) + ((double)vectorZ * (double)vectorZ));
        }
        internal static float vHorzDistSqr(Vector3 a, Vector3 b)
        {
            var vectorX = a.x - b.x;
            var vectorZ = a.z - b.z;
            return (float)(((double)vectorX * (double)vectorX) + ((double)vectorZ * (double)vectorZ));
        }
        internal static float vHorzDistSqr(in Vector3 a, in Vector3 b)
        {
            var vectorX = a.x - b.x;
            var vectorZ = a.z - b.z;
            return (float)(((double)vectorX * (double)vectorX) + ((double)vectorZ * (double)vectorZ));
        }
        internal static double dotAsDouble(Vector3 lhs, Vector3 rhs)
        {
            return ((double)lhs.x * (double)rhs.x + (double)lhs.y * (double)rhs.y + (double)lhs.z * (double)rhs.z);
        }
        internal static double dotAsDouble(in Vector3 lhs, in Vector3 rhs)
        {
            return ((double)lhs.x * (double)rhs.x + (double)lhs.y * (double)rhs.y + (double)lhs.z * (double)rhs.z);
        }
        internal static float dot2D(double lhsX, double lhsY, double rhsX, double rhsY)
        {
            return (float)(lhsX * rhsX + lhsY * rhsY);
        }
        internal static float dot2D(in Vector2 lhs, in Vector2 rhs)
        {
            return (float)((double)lhs.x * (double)rhs.x + (double)lhs.y * (double)rhs.y);
        }
        internal static float dot2D(Vector2 lhs, Vector2 rhs)
        {
            return (float)((double)lhs.x * (double)rhs.x + (double)lhs.y * (double)rhs.y);
        }
        internal static float dot(Vector3 lhs, Vector3 rhs)
        {
            return (float)((double)lhs.x * (double)rhs.x + (double)lhs.y * (double)rhs.y + (double)lhs.z * (double)rhs.z);
        }
        internal static float dot(in Vector3 lhs, in Vector3 rhs)
        {
            return (float)((double)lhs.x * (double)rhs.x + (double)lhs.y * (double)rhs.y + (double)lhs.z * (double)rhs.z);
        }
        internal static float dotHorz(Vector3 lhs, Vector3 rhs)
        {
            return (float)((double)lhs.x * (double)rhs.x + (double)lhs.z * (double)rhs.z);
        }
        internal static float dotHorz(in Vector3 lhs, in Vector3 rhs)
        {
            return (float)((double)lhs.x * (double)rhs.x + (double)lhs.z * (double)rhs.z);
        }
        internal static float dot(Quaternion a, Quaternion b)
        {
            return (float)((double)a.x * (double)b.x + (double)a.y * (double)b.y + (double)a.z * (double)b.z + (double)a.w * (double)b.w);
        }
        internal static float dot(in Quaternion a, in Quaternion b)
        {
            return (float)((double)a.x * (double)b.x + (double)a.y * (double)b.y + (double)a.z * (double)b.z + (double)a.w * (double)b.w);
        }
        internal static Vector3 centroidUnit(Vector3 a, Vector3 b, Vector3 c)
        {
            Vector3 cen;
            fun.triangle.GetCentroid(in a, in b, in c, out cen);
            return cen.normalized;
        }

        internal static Vector3 up_fw(double degrees) { return Vector3.SlerpUnclamped(Vector3.up, Vector3.forward, (float)(degrees / 90.0)); }
        internal static Vector3 up_rt(double degrees) { return Vector3.SlerpUnclamped(Vector3.up, Vector3.right, (float)(degrees / 90.0)); }
        internal static Vector3 up_lt(double degrees) { return Vector3.SlerpUnclamped(Vector3.up, Vector3.left, (float)(degrees / 90.0)); }
        internal static Vector3 up_bk(double degrees) { return Vector3.SlerpUnclamped(Vector3.up, Vector3.back, (float)(degrees / 90.0)); }
        internal static Vector3 dn_rt(double degrees) { return Vector3.SlerpUnclamped(Vector3.down, Vector3.right, (float)(degrees / 90.0)); }
        internal static Vector3 dn_lt(double degrees) { return Vector3.SlerpUnclamped(Vector3.down, Vector3.left, (float)(degrees / 90.0)); }
        internal static Vector3 fw_rt(double degrees) { return Vector3.SlerpUnclamped(Vector3.forward, Vector3.right, (float)(degrees / 90.0)); }
        internal static Vector3 fw_lt(double degrees) { return Vector3.SlerpUnclamped(Vector3.forward, Vector3.left, (float)(degrees / 90.0)); }
        internal static Vector3 fw_up(double degrees) { return Vector3.SlerpUnclamped(Vector3.forward, Vector3.up, (float)(degrees / 90.0)); }
        internal static Vector3 fw_dn(double degrees) { return Vector3.SlerpUnclamped(Vector3.forward, Vector3.down, (float)(degrees / 90.0)); }
        internal static Vector3 bk_rt(double degrees) { return Vector3.SlerpUnclamped(Vector3.back, Vector3.right, (float)(degrees / 90.0)); }
        internal static Vector3 bl_lt(double degrees) { return Vector3.SlerpUnclamped(Vector3.back, Vector3.left, (float)(degrees / 90.0)); }
        internal static Vector3 bk_up(double degrees) { return Vector3.SlerpUnclamped(Vector3.back, Vector3.up, (float)(degrees / 90.0)); }
        internal static Vector3 bk_dn(double degrees) { return Vector3.SlerpUnclamped(Vector3.back, Vector3.down, (float)(degrees / 90.0)); }

        internal static Vector3 slerp(Vector3 a, double t, Vector3 b)
        {
            return Vector3.SlerpUnclamped(a, b, (float)t);
        }
        internal static Vector3 slerp(Vector3 a, Vector3 b, double t)
        {
            return Vector3.SlerpUnclamped(a, b, (float)t);
        }
        internal static Vector3 slerp(Vector3 a, double ab, Vector3 b, double abc, Vector3 c)
        {
            return Vector3.SlerpUnclamped(Vector3.SlerpUnclamped(a, b, (float)ab), c, (float)abc);
        }
        internal static Vector3 slerp(in Vector3 a, in Vector3 b, double t)
        {
            return Vector3.SlerpUnclamped(a, b, (float)t);
        }
        internal static Vector3 slerp(in Vector3 a, double t, in Vector3 b)
        {
            return Vector3.SlerpUnclamped(a, b, (float)t);
        }
        internal static Quaternion slerp(in Quaternion a, in Quaternion b, double t)
        {
            return Quaternion.SlerpUnclamped(a, b, (float)t);
        }
        internal static Quaternion slerp(in Quaternion a, double t, in Quaternion b)
        {
            return Quaternion.SlerpUnclamped(a, b, (float)t);
        }
        internal static Quaternion slerp(Quaternion a, Quaternion b, double t)
        {
            return Quaternion.SlerpUnclamped(a, b, (float)t);
        }
        internal static Quaternion slerp(Quaternion a, double t, Quaternion b)
        {
            return Quaternion.SlerpUnclamped(a, b, (float)t);
        }
        internal static void slerp(in Vector3 a, in Vector3 b, double t, out Vector3 d)
        {
            d = Vector3.SlerpUnclamped(a, b, (float)t);
        }
        internal static Vector3 slerp(in Vector3 a, double ab, in Vector3 b, double abc, in Vector3 c)
        {
            return Vector3.SlerpUnclamped(Vector3.SlerpUnclamped(a, b, (float)ab), c, (float)abc);
        }
        internal static Quaternion slerp2(in Quaternion a, in Quaternion b, double bStart, in Quaternion c, double t)
        {
            if (bStart < 0 || bStart > 1) throw new InvalidEnumArgumentException("Limit must be between 0 and 1 it was = " + bStart);
            if (t <= bStart) return slerp(in a, in b, t.FromRangeTo01(0, bStart));
            return slerp(in b, in c, t.FromRangeTo01(bStart, 1));
        }
        internal static Quaternion lookAt(Vector3 fwDir, Vector3 upDir) => Quaternion.LookRotation(fwDir, upDir);
        internal static Quaternion lookAt(Vector3 target, Vector3 source, Vector3 upDir) => Quaternion.LookRotation((target - source).normalized, upDir);
        internal static Quaternion lookAtHorz(Vector3 target, Vector3 source) => Quaternion.LookRotation((target - source).ToHorzUnit(), v3.up);
        internal static int ceil(double n)
        {
            return (int)Math.Ceiling(n);
        }
        internal static int floor(double n)
        {
            return (int)Math.Floor(n);
        }

        internal static Vector3 rotRel(double latitudeDegrees, double longetudeDegrees)
        {
            latitudeDegrees = latitudeDegrees % 180.0;
            longetudeDegrees = longetudeDegrees % 90.0;
            var dir = Quaternion.AngleAxis((float)-latitudeDegrees, v3.up) * Vector3.forward;
            return dir.RotateTowards(longetudeDegrees > 0 ? v3.up : v3.down, longetudeDegrees);
        }
        internal static Vector3 rotAbs(double latitudeDegrees, double longetudeDegrees, Transform m)
        {
            latitudeDegrees = latitudeDegrees % 180.0;
            longetudeDegrees = longetudeDegrees % 90.0;
            var dir = Quaternion.AngleAxis((float)-latitudeDegrees, m.up) * m.forward;
            return dir.RotateTowards(longetudeDegrees > 0 ? m.up : -m.up, longetudeDegrees);
        }

        internal static Vector3 vecNorm(Vector3 lhs, Vector3 rhs)
        {
            Vector3 norm;
            fun.vector.GetNormal(in lhs, in rhs, out norm);
            return norm;
        }
        internal static Vector3 vecNorm(in Vector3 lhs, in Vector3 rhs)
        {
            Vector3 norm;
            fun.vector.GetNormal(in lhs, in rhs, out norm);
            return norm;
        }
        internal static void vecNorm(in Vector3 lhs, in Vector3 rhs, out Vector3 norm)
        {
            fun.vector.GetNormal(in lhs, in rhs, out norm);
        }


        internal static Vector3 pntNorm(Vector3 a, Vector3 b, Vector3 c)
        {
            Vector3 norm;
            fun.point.GetNormal(in a, in b, in c, out norm);
            return norm;
        }
        internal static Vector3 pntNorm(in Vector3 a, in Vector3 b, in Vector3 c)
        {
            Vector3 norm;
            fun.point.GetNormal(in a, in b, in c, out norm);
            return norm;
        }
        internal static void pntNorm(in Vector3 a, in Vector3 b, in Vector3 c, out Vector3 norm)
        {
            fun.point.GetNormal(in a, in b, in c, out norm);
        }

        internal static Vector3 pntNormSameSideAs(Vector3 a, Vector3 b, Vector3 c, Vector3 sideDir)
        {
            Vector3 norm;
            fun.point.GetNormal(in a, in b, in c, out norm);
            fun.vector.EnsurePointSameDirAs(in norm, in sideDir, out norm);
            return norm;
        }
        internal static Vector3 pntNormSameSideAs(in Vector3 a, in Vector3 b, in Vector3 c, in Vector3 sideDir)
        {
            Vector3 norm;
            fun.point.GetNormal(in a, in b, in c, out norm);
            fun.vector.EnsurePointSameDirAs(in norm, in sideDir, out norm);
            return norm;
        }
        internal static void pntNormSameSideAs(in Vector3 a, in Vector3 b, in Vector3 c, in Vector3 sideDir, out Vector3 norm)
        {
            fun.point.GetNormal(in a, in b, in c, out norm);
            fun.vector.EnsurePointSameDirAs(in norm, in sideDir, out norm);
        }



        internal static bool propability(double p)
        {
            return fun.random.Bool(p);
        }
        /// <summary>
        /// random between min and max
        /// </summary>
        /// <param name="min">minimum random value</param>
        /// <param name="max">maximum random value</param>
        /// <returns></returns>
        internal static float rnd(double min, double max)
        {
            return fun.random.Between(min, max);
        }
        internal static float rnd(double min, double max, Func<double, double> probability)
        {
            return fun.random.Between(min, max, probability);
        }
        internal static void execRnd(Action a, Action b)
        {
            if (number01 < 0.5) a();
            else b();
        }
        internal static void execRnd(Action a, Action b, Action c)
        {
            var p = number01;
            if (p < 0.333) a();
            else if (p < 0.666) b();
            else c();
        }
        internal static void execRnd(double probabilityA, Action a, double probabilityB, Action b, Action c)
        {
            var p = number01;
            if (p < probabilityA) a();
            else if (p < probabilityB) b();
            else c();
        }
        internal static int rndInt(int min, int max)
        {
            return fun.random.Between(min, max);
        }
        internal static T rndOf<T>(params T[] arr)
        {
            return fun.random.Of(arr);
        }
        internal static T rndOf<T>(T a, T b)
        {
            var n = fun.random.number01;
            return n < 0.5 ? a : b;
        }
        internal static T rndOf<T>(T a, T b, T c)
        {
            var n = fun.random.number01;
            return n <= 0.33333 ? a : n <= 0.66666 ? b : c;
        }
        internal static float number01 { get { return fun.random.number01; } }
        internal static float numberM11 { get { return fun.random.number01.From01ToMin11(); } }
        internal static bool randomBool(double probability)
        {
            return fun.random.Bool(probability);
        }
        internal static float bezier(double x, double bx, double by, double cx, double cy)
        {
            return (float)BezierFunc.GetY(x, bx, by, cx, cy);
        }
        internal static float bezier(double x, double ax, double ay, double bx, double by, double cx, double cy, double dx, double dy)
        {
            return (float)BezierFunc.GetY(x, ax, ay, bx, by, cx, cy, dx, dy);
        }
        internal static float bezier2parts(double x,
            double ax1, double ay1, double bx1, double by1, double cx1, double cy1,
            double ax2, double ay2, double bx2, double by2, double cx2, double cy2,
            double dx2, double dy2)
        {
            if (x <= ax2)
            {
                return (float)BezierFunc.GetY(x, ax1, ay1, bx1, by1, cx1, cy1, ax2, ay2);
            }
            return (float)BezierFunc.GetY(x, ax2, ay2, bx2, by2, cx2, cy2, dx2, dy2);
        }
        internal static float bezier3parts(double x,
            double ax1, double ay1, double bx1, double by1, double cx1, double cy1,
            double ax2, double ay2, double bx2, double by2, double cx2, double cy2,
            double ax3, double ay3, double bx3, double by3, double cx3, double cy3,
            double dx3, double dy3)
        {
            if (x <= ax2)
            {
                return (float)BezierFunc.GetY(x, ax1, ay1, bx1, by1, cx1, cy1, ax2, ay2);
            }
            if (x <= ax3)
            {
                return (float)BezierFunc.GetY(x, ax2, ay2, bx2, by2, cx2, cy2, ax3, ay3);
            }
            return (float)BezierFunc.GetY(x, ax3, ay3, bx3, by3, cx3, cy3, dx3, dy3);
        }
        internal static float bezier4parts(double x,
            double ax1, double ay1, double bx1, double by1, double cx1, double cy1,
            double ax2, double ay2, double bx2, double by2, double cx2, double cy2,
            double ax3, double ay3, double bx3, double by3, double cx3, double cy3,
            double ax4, double ay4, double bx4, double by4, double cx4, double cy4,
            double dx4, double dy4)
        {

            if (x <= ax2)
            {
                return (float)BezierFunc.GetY(x, ax1, ay1, bx1, by1, cx1, cy1, ax2, ay2);
            }
            if (x <= ax3)
            {
                return (float)BezierFunc.GetY(x, ax2, ay2, bx2, by2, cx2, cy2, ax3, ay3);
            }
            if (x <= ax4)
            {
                return (float)BezierFunc.GetY(x, ax3, ay3, bx3, by3, cx3, cy3, ax4, ay4);
            }
            return (float)BezierFunc.GetY(x, ax4, ay4, bx4, by4, cx4, cy4, dx4, dy4);
        }
        internal static int round(double d)
        {
            return (int)Math.Round(d);
        }
        internal static float round(double d, int places)
        {
            return (float)Math.Round(d, places);
        }


        internal static bool isEvenFrame => Time.frameCount % 2 == 0;
        internal static bool isFrame(int frame) { return Time.frameCount % frame == 0; }
        internal static float range01(double progress, double min, double max)
        {
            return (float)(min + progress * (max - min));
        }
        internal static float range11(double progress, double min, double max)
        {
            progress = 0.5f * progress + 0.5f;
            return (float)(min + progress * (max - min));
        }

        internal static float clamp(double n, double min, double max)
        {
            return (float)(n < min ? min : n > max ? max : n);
        }
        internal static float clamp01(double n)
        {
            return (float)(n < 0 ? 0 : n > 1 ? 1 : n);
        }
        internal static float clampMin11(double n)
        {
            return (float)(n < -1 ? -1 : n > 1 ? 1 : n);
        }
        internal static float ratio01ByRange(double value, double from, double to, bool clamp)
        {
            var all = to - from;
            if (all.IsZero())
            {
                return value < from ? 0 : 1;
            }
            var curr = value - from;
            var ratioRaw = curr / all;
            return (float)(clamp ? ratioRaw.Clamp01() : ratioRaw);
        }
        internal static float ratioMin11ByRange(double value, double from, double to, bool clamp)
        {
            var all = to - from;
            if (all.IsZero())
            {
                return value < from ? 0 : 1;
            }
            var curr = value - from;
            var ratioRaw = curr / all;
            return (float)(clamp ? ratioRaw.From01ToMin11().ClampMin11() : ratioRaw.From01ToMin11());
        }

        #region angle
        internal static class angle
        {
            private const double TwiseRadiansToDegrees = 2.0 * 57.2957801818848;

            /// <summary>
            /// Angle between rotations in degrees
            /// </summary>
            internal static float Between(Quaternion a, Quaternion b)
            {
                return (float)((double)Math.Acos(Math.Min(Math.Abs(dot(in a, in b)), 1f)) * TwiseRadiansToDegrees);
            }
            /// <summary>
            /// Angle between rotations in degrees
            /// </summary>
            internal static float Between(in Quaternion a, in Quaternion b)
            {
                return (float)((double)Math.Acos(Math.Min(Math.Abs(dot(in a, in b)), 1f)) * TwiseRadiansToDegrees);
            }
            internal static float BetweenVectorsUnSignedInRadians(in Vector3 from, in Vector3 to)
            {
                return Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f));
            }
            internal static float BetweenVectorsUnSignedInDegrees(in Vector3 from, in Vector3 to)
            {
                return Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f)) * RTD;
            }
            internal static float BetweenVectorsSigned2D(in Vector2 lhs, in Vector2 rhs)
            {
                var perpDot = lhs.x * rhs.y - lhs.y * rhs.x;

                return -RTD * (float)Math.Atan2(perpDot, dot2D(in lhs, in rhs));
            }
            internal static float BetweenVectorsSigned2D(Vector2 lhs, Vector2 rhs)
            {
                var perpDot = lhs.x * rhs.y - lhs.y * rhs.x;

                return -RTD * (float)Math.Atan2(perpDot, dot2D(in lhs, in rhs));
            }
            internal static float BetweenVectorsSigned2D(double v1x, double v1y, double v2x, double v2y)
            {
                var perpDot = v1x * v2y - v1y * v2x;

                return -RTD * (float)Math.Atan2(perpDot, dot2D(v1x, v1y, v2x, v2y));
            }
            internal static float ShortestBetweenVectorsSigned(in Vector3 lhs, in Vector3 rhs)
            {
                Vector3 normal;
                fun.vector.GetNormal(in lhs, in rhs, out normal);
                var x = cross.Product(in lhs, in rhs);
                return (float)Math.Atan2(dot(in normal, in x), dot(in lhs, in rhs)) * RTD;
            }
            internal static float BetweenPointsSignedIgnoreY(Vector3 lhsPoint, Vector3 centerPoint, Vector3 rhsPoint)
            {
                var lhs = lhsPoint - centerPoint;
                var rhs = rhsPoint - centerPoint;
                return BetweenVectorsSigned2D(lhs.x, lhs.z, rhs.x, rhs.z);
            }
            internal static float BetweenPointsSignedIgnoreY(in Vector3 lhsPoint, in Vector3 centerPoint, in Vector3 rhsPoint)
            {
                var lhs = lhsPoint - centerPoint;
                var rhs = rhsPoint - centerPoint;
                return BetweenVectorsSigned2D(lhs.x, lhs.z, rhs.x, rhs.z);
            }

            /// <summary>
            /// angle in degrees, left to right angle will be positive, right to left negative
            /// </summary>
            internal static float BetweenVectorsSignedInDegrees(in Vector3 lhs, in Vector3 rhs, in Vector3 normal)
            {
                var x = fun.cross.Product(in lhs, in rhs);
                return (float)Math.Atan2(fun.dot(in normal, in x), fun.dot(in lhs, in rhs)) * RTD;
            }
            /// <summary>
            /// angle in degrees, left to right angle will be positive, right to left negative
            /// </summary>
            internal static float BetweenVectorsSignedInDegrees(Vector3 lhs, Vector3 rhs, Vector3 normal)
            {
                var x = fun.cross.Product(in lhs, in rhs);
                return (float)Math.Atan2(fun.dot(in normal, in x), fun.dot(in lhs, in rhs)) * RTD;
            }
            internal static float BetweenPointsSignedInDegrees(in Vector3 lhsPoint, in Vector3 centerPoint, in Vector3 rhsPoint)
            {
                Vector3 normal;
                fun.point.GetNormal(in lhsPoint, in centerPoint, in rhsPoint, out normal);
                var lhs = lhsPoint - centerPoint;
                var rhs = rhsPoint - centerPoint;
                return BetweenVectorsSignedInDegrees(in lhs, in rhs, in normal);
            }
            internal static float BetweenPointsSignedInDegrees(Vector3 lhsPoint, Vector3 centerPoint, Vector3 rhsPoint)
            {
                Vector3 normal;
                fun.point.GetNormal(in lhsPoint, in centerPoint, in rhsPoint, out normal);
                var lhs = lhsPoint - centerPoint;
                var rhs = rhsPoint - centerPoint;
                return BetweenVectorsSignedInDegrees(in lhs, in rhs, in normal);
            }
            internal static float BetweenPointsSignedInDegrees(in Vector3 lhsPoint, in Vector3 centerPoint, in Vector3 rhsPoint, in Vector3 normal)
            {
                var lhs = lhsPoint - centerPoint;
                var rhs = rhsPoint - centerPoint;
                return BetweenVectorsSignedInDegrees(in lhs, in rhs, in normal);
            }
            internal static float BetweenPointsSignedInDegrees(Vector3 lhsPoint, Vector3 centerPoint, Vector3 rhsPoint, Vector3 normal)
            {
                var lhs = lhsPoint - centerPoint;
                var rhs = rhsPoint - centerPoint;
                return BetweenVectorsSignedInDegrees(in lhs, in rhs, in normal);
            }
            /// <summary>
            /// signed degrees difference for each axis
            /// </summary>
            internal static void DifferenceForEachAxis(in Quaternion original, in Quaternion changed, out Vector3 diffEuler)
            {
                var aFw = original * Vector3.forward;
                var aUp = original * Vector3.up;
                var aRt = original * Vector3.right;
                var bFw = changed * Vector3.forward;
                var bUp = changed * Vector3.up;
                var bRt = changed * Vector3.right;
                Vector3 bFwProj, bUpProj, bRtProj;

                vector.ProjectOnPlane(in bRt, in aUp, out bRtProj);
                vector.ProjectOnPlane(in bUp, in aFw, out bUpProj);
                vector.ProjectOnPlane(in bFw, in aRt, out bFwProj);
                var x = 0f;
                var y = 0f;
                var z = 0f;
                if (bRtProj.sqrMagnitude > 0.000001)
                {
                    bRtProj.Normalize();
                    x = BetweenVectorsSignedInDegrees(in bRtProj, in aRt, in aUp);
                }
                if (bUpProj.sqrMagnitude > 0.000001)
                {
                    bUpProj.Normalize();
                    y = BetweenVectorsSignedInDegrees(in bUpProj, in aUp, in aFw);
                }
                if (bFwProj.sqrMagnitude > 0.000001)
                {
                    bFwProj.Normalize();
                    z = BetweenVectorsSignedInDegrees(in bFwProj, in aFw, in aRt);
                }
                diffEuler = new Vector3(x, y, z);
            }
            /// <summary>
            /// returns angle in degrees
            /// </summary>
            internal static float BetweenVectorsProjectedOnPlane(in Vector3 vec1, in Vector3 vec2, in Vector3 planeNormal)
            {
                Vector3 proj1, proj2;
                fun.vector.ProjectOnPlane(in vec1, in planeNormal, out proj1);
                fun.vector.ProjectOnPlane(in vec2, in planeNormal, out proj2);
                proj1.Normalize();
                proj2.Normalize();
                return fun.angle.BetweenVectorsUnSignedInDegrees(in proj1, in proj2);
            }
        }

        #endregion
        #region circle2d
        internal static class circle2d
        {
            internal static bool Overlap(
                in Vector3 circleCenter1, double circleRadius1,
                in Vector3 circleCenter2, double circleRadius2)
            {
                return distance.BetweenIgnoreY(in circleCenter1, in circleCenter2) <= (circleRadius1 + circleRadius2);
            }

            internal static void Join(
                in Vector3 circleCenter1, double circleRadius1,
                in Vector3 circleCenter2, double circleRadius2,
                out Vector3 combinedCirCen, out float combinedCirRad)
            {
                var dist = distance.BetweenIgnoreY(in circleCenter1, in circleCenter2);
                // circle 2 is inside circle 1
                if ((dist + circleRadius2) <= circleRadius1)
                {
                    combinedCirRad = (float)circleRadius1;
                    combinedCirCen = circleCenter1;
                    return;
                }
                // circle 1 is inside circle 2
                if ((dist + circleRadius1) <= circleRadius2)
                {
                    combinedCirRad = (float)circleRadius2;
                    combinedCirCen = circleCenter2;
                    return;
                }

                combinedCirRad = (float)((circleRadius1 + circleRadius2 + dist) / 2.0);
                var circ1to2 = (circleCenter2 - circleCenter1).ToHorzUnit();
                combinedCirCen = circleCenter1 + circ1to2 * (float)-circleRadius1 + circ1to2 * combinedCirRad;
            }

            internal static bool JoinIfOverlap(
                in Vector3 circleCenter1, double circleRadius1,
                in Vector3 circleCenter2, double circleRadius2,
                out Vector3 combinedCirCen, out float combinedCirRad)
            {
                var dist = distance.BetweenIgnoreY(in circleCenter1, in circleCenter2);
                // they don't overlap
                if (dist > (circleRadius1 + circleRadius2))
                {
                    combinedCirCen = Vector3.zero;
                    combinedCirRad = 0;
                    return false;
                }
                // circle 2 is inside circle 1
                if ((dist + circleRadius2) <= circleRadius1)
                {
                    combinedCirRad = (float)circleRadius1;
                    combinedCirCen = circleCenter1;
                    return true;
                }
                // circle 1 is inside circle 2
                if ((dist + circleRadius1) <= circleRadius2)
                {
                    combinedCirRad = (float)circleRadius2;
                    combinedCirCen = circleCenter2;
                    return true;
                }

                combinedCirRad = (float)((circleRadius1 + circleRadius2 + dist) / 2.0);
                var circ1to2 = (circleCenter2 - circleCenter1).ToHorzUnit();
                combinedCirCen = circleCenter1 + circ1to2 * (float)-circleRadius1 + circ1to2 * combinedCirRad;
                return true;
            }
        }

        #endregion
        #region bezierFunc
        internal static class bezierFunc
        {
            internal static float GetY(double x, double bx, double by, double cx, double cy)
            {
                return (float)BezierFunc.GetY(x, bx, by, cx, cy);
            }
            internal static float GetY(double x, double ax, double ay, double bx, double by, double cx, double cy, double dx, double dy)
            {
                return (float)BezierFunc.GetY(x, ax, ay, bx, by, cx, cy, dx, dy);
            }
            internal static Vector3 GetPoint2D(double x, Vector2 start, Vector2 control1, Vector2 control2, Vector2 end)
            {
                return BezierFunc.GetPointCubic2D(x, start, control1, control2, end);
            }
            internal static Vector3 GetPoint(double x, Vector3 start, Vector3 control1, Vector3 control2, Vector3 end)
            {
                return BezierFunc.GetPointCubic(x, start, control1, control2, end);
            }
            internal static Vector3 GetPoint(double x, in Vector3 start, in Vector3 control1, in Vector3 control2, in Vector3 end)
            {
                return BezierFunc.GetPointCubic(x, in start, in control1, in control2, in end);
            }

            internal static Vector3 GetPoint2D(double x, Vector2 start, Vector2 control, Vector2 end)
            {
                return BezierFunc.GetPointQuadratic2D(x, start, control, end);
            }
            internal static Vector3 GetPoint(double x, Vector3 start, Vector3 control, Vector3 end)
            {
                return BezierFunc.GetPointQuadratic(x, start, control, end);
            }
            internal static Vector3 GetPoint(double x, in Vector3 start, in Vector3 control, in Vector3 end)
            {
                return BezierFunc.GetPointQuadratic(x, in start, in control, in end);
            }
        }

        #endregion
        #region color
        internal static class color
        {
            internal static Color Parse(uint color)
            {
                byte r = (byte)(color >> 24);
                byte g = (byte)(color >> 16);
                byte b = (byte)(color >> 8);
                byte a = (byte)(color >> 0);
                return new Color(r / (float)0xFF, g / (float)0xFF, b / (float)0xFF, a / (float)0xFF);
            }
            internal static Color Rainbow(double x01)
            {
                return FromHueSaturationLuminance(x01.Clamp01().From01ToRange(0, 0.9999), 1, 0.5);
            }
            internal static Color FromHueSaturationLuminance(double hue, double saturation, double luminance)
            {
                float v;
                float r, g, b;
                // default to gray
                r = (float)luminance;
                g = (float)luminance;
                b = (float)luminance;
                v = (float)((luminance <= 0.5) ? (luminance * (1.0 + saturation)) : (luminance + saturation - luminance * saturation));
                if (v > 0)
                {
                    float m;
                    float sv;
                    int sextant;
                    float fract, vsf, mid1, mid2;
                    m = (float)luminance + (float)luminance - v;
                    sv = (v - m) / v;
                    hue *= 6.0f;
                    sextant = (int)hue;
                    fract = (float)hue - sextant;
                    vsf = v * sv * fract;
                    mid1 = m + vsf;
                    mid2 = v - vsf;
                    switch (sextant)
                    {
                        case 0:
                            r = v;
                            g = mid1;
                            b = m;
                            break;
                        case 1:
                            r = mid2;
                            g = v;
                            b = m;
                            break;
                        case 2:
                            r = m;
                            g = v;
                            b = mid1;
                            break;
                        case 3:
                            r = m;
                            g = mid2;
                            b = v;
                            break;
                        case 4:
                            r = mid1;
                            g = m;
                            b = v;
                            break;
                        case 5:
                            r = v;
                            g = m;
                            b = mid2;
                            break;
                    }
                }
                return new Color(r, g, b);
            }
        }
        #endregion
        #region cross

        internal static class cross
        {
            internal static Vector3 Product(in Vector3 leftVector, in Vector3 rightVector)
            {
                return new Vector3(leftVector.y * rightVector.z - leftVector.z * rightVector.y, leftVector.z * rightVector.x - leftVector.x * rightVector.z, leftVector.x * rightVector.y - leftVector.y * rightVector.x);
            }
            internal static Vector3 Product(Vector3 leftVector, Vector3 rightVector)
            {
                return new Vector3(leftVector.y * rightVector.z - leftVector.z * rightVector.y, leftVector.z * rightVector.x - leftVector.x * rightVector.z, leftVector.x * rightVector.y - leftVector.y * rightVector.x);
            }
            internal static void Product(in Vector3 leftVector, in Vector3 rightVector, out Vector3 result)
            {
                result = new Vector3(leftVector.y * rightVector.z - leftVector.z * rightVector.y, leftVector.z * rightVector.x - leftVector.x * rightVector.z, leftVector.x * rightVector.y - leftVector.y * rightVector.x);
            }
        }
        #endregion
        #region distance
        internal static class distance
        {
            internal static float Between(Vector2 a, Vector2 b)
            {
                var vectorX = a.x - b.x;
                var vectorY = a.y - b.y;
                return (float)Math.Sqrt((((double)vectorX * (double)vectorX) + ((double)vectorY * (double)vectorY)));
            }
            internal static float Between(in Vector2 a, in Vector2 b)
            {
                var vectorX = a.x - b.x;
                var vectorY = a.y - b.y;
                return (float)Math.Sqrt((((double)vectorX * (double)vectorX) + ((double)vectorY * (double)vectorY)));
            }
            internal static float Between(in Vector2 a, float bx, float by)
            {
                var vector = new Vector2(a.x - bx, a.y - by);
                return (float)Math.Sqrt((((double)vector.x * (double)vector.x) + ((double)vector.y * (double)vector.y)));
            }
            internal static float Between(in Vector3 a, in Vector3 b)
            {
                var vectorX = a.x - b.x;
                var vectorY = a.y - b.y;
                var vectorZ = a.z - b.z;
                return (float)Math.Sqrt((((double)vectorX * (double)vectorX) + ((double)vectorY * (double)vectorY)) + ((double)vectorZ * (double)vectorZ));
            }
            internal static float Between(Vector3 a, Vector3 b)
            {
                var vectorX = a.x - b.x;
                var vectorY = a.y - b.y;
                var vectorZ = a.z - b.z;
                return (float)Math.Sqrt((((double)vectorX * (double)vectorX) + ((double)vectorY * (double)vectorY)) + ((double)vectorZ * (double)vectorZ));
            }
            internal static float BetweenIgnoreY(Vector3 a, Vector3 b)
            {
                var vectorX = a.x - b.x;
                var vectorZ = a.z - b.z;
                return (float)Math.Sqrt(((double)vectorX * (double)vectorX) + ((double)vectorZ * (double)vectorZ));
            }
            internal static float BetweenIgnoreY(in Vector3 a, in Vector3 b)
            {
                var vectorX = a.x - b.x;
                var vectorZ = a.z - b.z;
                return (float)Math.Sqrt(((double)vectorX * (double)vectorX) + ((double)vectorZ * (double)vectorZ));
            }
            internal static float Between(float ax, float ay, float bx, float by)
            {
                var vx = ax - bx;
                var vy = ay - by;
                return (float)Math.Sqrt(((vx * vx) + (vy * vy)));
            }
            internal static float Between(float ax, float ay, float az, float bx, float by, float bz)
            {
                var vx = ax - bx;
                var vy = ay - by;
                var vz = az - bz;
                return (float)Math.Sqrt((((double)vx * (double)vx) + ((double)vy * (double)vy)) + ((double)vz * (double)vz));
            }

            internal static float FromPointToPlane(in Vector3 point, in Vector3 planeNormal, in Vector3 planePoint)
            {
                var vectorToPlane = point - planePoint;
                var distance = dot(in planeNormal, in vectorToPlane);
                return abs(distance);
            }
            internal static float FromPointToPlane(Vector3 point, Vector3 planeNormal, Vector3 planePoint)
            {
                var vectorToPlane = point - planePoint;
                var distance = dot(in planeNormal, in vectorToPlane);
                return abs(distance);
            }
        }

        #endregion
        #region distanceSquared
        internal static class distanceSquared
        {
            internal static float Between(Vector2 a, Vector2 b)
            {
                var vectorX = (double)(a.x - b.x);
                var vectorY = (double)(a.y - b.y);
                return (float)((vectorX * vectorX) + (vectorY * vectorY));
            }
            internal static float Between(in Vector2 a, in Vector2 b)
            {
                var vectorX = (double)(a.x - b.x);
                var vectorY = (double)(a.y - b.y);
                return (float)((vectorX * vectorX) + (vectorY * vectorY));
            }
            internal static float Between(in Vector2 a, float bx, float by)
            {
                var vector = new Vector2(a.x - bx, a.y - by);
                return (float)(((double)vector.x * (double)vector.x) + ((double)vector.y * (double)vector.y));
            }
            internal static float Between(in Vector3 a, in Vector3 b)
            {
                var vectorX = (double)(a.x - b.x);
                var vectorY = (double)(a.y - b.y);
                var vectorZ = (double)(a.z - b.z);
                return (float)(((vectorX * vectorX) + (vectorY * vectorY)) + (vectorZ * vectorZ));
            }
            internal static double BetweenAsDouble(in Vector3 a, in Vector3 b)
            {
                var vectorX = (double)(a.x - b.x);
                var vectorY = (double)(a.y - b.y);
                var vectorZ = (double)(a.z - b.z);
                return (((vectorX * vectorX) + (vectorY * vectorY)) + (vectorZ * vectorZ));
            }
            internal static float Between(Vector3 a, Vector3 b)
            {
                var vectorX = (double)(a.x - b.x);
                var vectorY = (double)(a.y - b.y);
                var vectorZ = (double)(a.z - b.z);
                return (float)(((vectorX * vectorX) + (vectorY * vectorY)) + (vectorZ * vectorZ));
            }
            internal static float BetweenIgnoreY(Vector3 a, Vector3 b)
            {
                var vectorX = (double)(a.x - b.x);
                var vectorZ = (double)(a.z - b.z);
                return (float)((vectorX * vectorX) + (vectorZ * vectorZ));
            }
            internal static float BetweenIgnoreY(in Vector3 a, in Vector3 b)
            {
                var vectorX = (double)(a.x - b.x);
                var vectorZ = (double)(a.z - b.z);
                return (float)((vectorX * vectorX) + (vectorZ * vectorZ));
            }
            internal static float Between(float ax, float ay, float bx, float by)
            {
                var vx = ax - bx;
                var vy = ay - by;
                return (vx * vx) + (vy * vy);
            }
            internal static float Between(float ax, float ay, float az, float bx, float by, float bz)
            {
                var vx = ax - bx;
                var vy = ay - by;
                var vz = az - bz;
                return (float)((((double)vx * (double)vx) + ((double)vy * (double)vy)) + ((double)vz * (double)vz));
            }
        }

        #endregion
        #region ellipse
        internal static class ellipse
        {
            /// <summary>
            /// 0 degrees is horizontal, 90 degrees is vertical
            /// RadiusByAngle(10,20,0) == 10
            /// RadiusByAngle(10,20,90) == 20
            /// alse
            /// RadiusByAngle(200, 50, 20)==RadiusByAngle(200, 50, -20)==RadiusByAngle(200, 50, 180-20)==RadiusByAngle(200, 50, 180+20)==120.5023                       
            /// </summary>
            internal static float RadiusByAngle(double horzRadius, double vertRadius, double degrees)
            {
                var angleRadians = DTR * degrees;
                return (float)((horzRadius * vertRadius) / Math.Sqrt(horzRadius * horzRadius * Math.Pow(Math.Sin(angleRadians), 2) + vertRadius * vertRadius * Math.Pow(Math.Cos(angleRadians), 2)));
            }
        }
        #endregion
        #region resolution
        internal static class resolution
        {
            /// <param name="staticP0">static capsule lower center</param>
            /// <param name="staticP1">static capsule upper center</param>
            /// <param name="staticRadius">static capsule radius</param>
            /// <param name="prevP0">previous dynamic capsule lower center</param>
            /// <param name="prevP1">previous dynamic capsule upper center</param>
            /// <param name="nextP0">next dynamic capsule lower center</param>
            /// <param name="nextP1">next dynamic capsule upper center</param>
            /// <param name="nextUp">next dynamic capsule up vector</param>
            /// <param name="dynamicRadius">dynamic capsule radius</param>
            /// <param name="resolvedPos">resolved dynamic capsule lowest tip (not lowerst center!)</param>
            /// <param name="resolvedRot"></param>
            /// <returns></returns>
            internal static bool BetweenCapsules(
                in Vector3 staticP0, in Vector3 staticP1, double staticRadius,
                in Vector3 prevP0, in Vector3 prevP1,
                in Vector3 nextP0, in Vector3 nextP1, in Vector3 nextUp, double dynamicRadius,
                out Vector3 resolvedPos, out Quaternion resolvedRot)
            {
                var staRad = (float)staticRadius;
                var dynRad = (float)dynamicRadius;
                var minDist = staRad + dynRad;

                var hasNext = intersection.BetweenCapsules(in staticP0, in staticP1, staRad, in nextP0, in nextP1, dynRad);

                var prevDir = (prevP1 - prevP0).normalized;
                var prevRadVec = prevDir * dynRad;
                var prevTip = prevP1 + prevRadVec;
                var prevPos = prevP0 - prevRadVec;

                var nextDir = (nextP1 - nextP0).normalized;
                var nextRadVec = nextDir * dynRad;
                var nextTip = nextP1 + nextRadVec;
                var nextPos = nextP0 - nextRadVec;

                var t1 = nextTip;
                var t2 = nextPos;
                var t3 = prevTip;
                Vector3 triCol;
                var hasTri = intersection.BetweenTriangleAndLineSegment(in t1, in t2, in t3, in staticP0, in staticP1, out triCol);

                if (!hasTri)
                {
                    t1 = nextPos;
                    t2 = prevPos;
                    t3 = prevTip;
                    hasTri = intersection.BetweenTriangleAndLineSegment(in t1, in t2, in t3, in staticP0, in staticP1, out triCol);
                }

                if (!hasNext && !hasTri)
                {
                    // there is no collision so prev can become next
                    resolvedPos = nextPos;
                    resolvedRot = Quaternion.LookRotation(nextDir, nextUp);
                    return false;
                }

                Vector3 onStat, onPrev;
                point.ClosestOnTwoLineSegments(in staticP0, in staticP1, in prevPos, in prevTip, out onStat, out onPrev);

                resolvedPos = nextPos;
                var statDir = (staticP1 - staticP0).normalized;

                Vector3 target;
                if (point.IsAbovePlane(in onPrev, in statDir, in staticP1))
                {
                    target = prevP1 + (onPrev - prevP1).normalized * minDist;
                    resolvedRot = Quaternion.LookRotation((target - resolvedPos).normalized, nextUp);
                    return true;
                }
                var minStatDir = -statDir;
                if (point.IsAbovePlane(in onPrev, in minStatDir, in staticP0))
                {
                    target = prevP0 + (onPrev - prevP0).normalized * minDist;
                    resolvedRot = Quaternion.LookRotation((target - resolvedPos).normalized, nextUp);
                    return true;
                }
                var backDir = (onPrev - onStat).normalized;
                var radians = angle.BetweenVectorsUnSignedInRadians(in backDir, in statDir);
                target = onStat + backDir * abs(minDist / sin(radians));
                resolvedRot = Quaternion.LookRotation((target - resolvedPos).normalized, nextUp);
                return true;
            }
        }

        #endregion
        #region intersection
        internal static class intersection
        {
            /// <summary>
            /// intersection between two axis alligned bounding boxes
            /// </summary>
            /// <param name="center1">center of the first box</param>
            /// <param name="size1">width of each dimension of the first bounding box</param>
            /// <param name="center2">center of the second box</param>
            /// <param name="size2">width of each dimension of the second bounding box</param>
            /// <returns>return true if they overlap</returns>
            internal static bool BetweenAxisAlignedBoxes(in Vector3 center1, in Vector3 size1, in Vector3 center2, in Vector3 size2)
            {
                var hasOverlapOnX = Math.Abs(center1.x - center2.x) <= ((size1.x / 2f) + (size2.x / 2f));
                var hasOverlapOnY = Math.Abs(center1.y - center2.y) <= ((size1.y / 2f) + (size2.y / 2f));
                var hasOverlapOnZ = Math.Abs(center1.z - center2.z) <= ((size1.z / 2f) + (size2.z / 2f));
                return hasOverlapOnX && hasOverlapOnY && hasOverlapOnZ;
            }


            /// <summary>
            /// intersection between two spheres is a circle
            /// </summary>
            /// <param name="sphere1Center">sphere 1 center</param>
            /// <param name="sphere1Radius">sphere 1 radius</param>
            /// <param name="sphere2Center">sphere 2 center</param>
            /// <param name="sphere2Radius">sphere 2 radius</param>
            /// <param name="intersectCircleCenter">intersection circle center</param>
            /// <param name="intersectCircleRadius">intersection circle radius</param>
            /// <returns>return true if the 2 spheres intersect their surfaces, if one is inside the other returns false</returns>
            internal static bool BetweenSpheres(in Vector3 sphere1Center, double sphere1Radius, in Vector3 sphere2Center, double sphere2Radius, out Vector3 intersectCircleCenter, out float intersectCircleRadius)
            {
                var r1 = (float)sphere1Radius;
                var r2 = (float)sphere2Radius;
                var d = distance.Between(in sphere1Center, in sphere2Center);
                // Two separate spheres
                if (d > (r1 + r2))
                {
                    intersectCircleCenter = lerp(in sphere1Center, in sphere2Center, sphere1Radius / sphere2Radius);
                    intersectCircleRadius = 0;
                    return false;
                }
                // Outer tangency
                if (d.IsEqual(r1 + r2))
                {
                    intersectCircleCenter = sphere1Center + (sphere2Center - sphere1Center).normalized * r2;
                    intersectCircleRadius = 0;
                    return true;
                }
                // Inner tangency
                if (d.IsEqual(abs(r1 - r2)))
                {
                    intersectCircleCenter =
                        r1 > r2
                        ? sphere1Center + (sphere2Center - sphere1Center).ToUnit(Vector3.forward) * r1
                        : sphere2Center + (sphere1Center - sphere2Center).ToUnit(Vector3.forward) * r2;
                    intersectCircleRadius = 0;
                    return true;
                }
                // One sphere inside the other
                if (d < abs(r1 - r2))
                {
                    intersectCircleCenter = lerp(in sphere1Center, in sphere2Center, 0.5);
                    intersectCircleRadius = 0;
                    return false;
                }
                var d1 = (d * d - r2 * r2 + r1 * r1) / (2 * d);
                var ratio = d1 / r1;
                var cr = sqrt(1f - ratio * ratio) * r1;
                intersectCircleRadius = cr;
                var dir = (sphere2Center - sphere1Center).ToUnit(Vector3.forward);
                intersectCircleCenter = sphere1Center + dir * d1;
                return true;
            }

            /// <summary>
            /// Check if disk and plane cross each other
            /// </summary>
            /// <param name="planeNormal">the normal of the plane</param>
            /// <param name="planePoint">any point on the plane</param>
            /// <param name="sphereCenter">the center of the sphere</param>
            /// <param name="sphereRadius">sphere radius</param>
            /// <param name="collision">collision point</param>
            /// <returns></returns>
            internal static bool BetweenPlaneAndSphere(in Vector3 planeNormal, in Vector3 planePoint, in Vector3 sphereCenter, float sphereRadius, out Vector3 collision)
            {
                Vector3 sphereCenterProj;
                point.ProjectOnPlane(in sphereCenter, in planeNormal, in planePoint, out sphereCenterProj);
                var h = distance.Between(in sphereCenter, in sphereCenterProj);
                if (h > sphereRadius)
                {
                    // sphere does not intersect the plane
                    collision = Vector3.zero;
                    return false;
                }
                collision = sphereCenterProj;
                return true;
            }


            /// <summary>
            /// Check if disk and plane cross each other
            /// </summary>
            /// <param name="p1">point on the plane 1</param>
            /// <param name="p2">point on the plane 2</param>
            /// <param name="p3">point on the plane 3</param>
            /// <param name="sphereCenter">the center of the sphere</param>
            /// <param name="sphereRadius">sphere radius</param>
            /// <param name="intersectPoint">collision point</param>
            /// <returns></returns>
            internal static bool BetweenPlaneAndSphere(in Vector3 p1, in Vector3 p2, in Vector3 p3, in Vector3 sphereCenter, float sphereRadius, out Vector3 intersectPoint)
            {
                Vector3 planeNormal;
                point.GetNormal(in p1, in p2, in p3, out planeNormal);
                return BetweenPlaneAndSphere(in planeNormal, in p1, in sphereCenter, sphereRadius, out intersectPoint);
            }

            /// <summary>
            /// Check if disk and sphere cross each other
            /// </summary>
            /// <param name="diskPlaneNormal">the normal of the disk 1 plane</param>
            /// <param name="diskCenter">the center of the disk</param>
            /// <param name="diskRadius">disk radius</param>
            /// <param name="sphereCenter">the center of the sphere</param>
            /// <param name="sphereRadius">sphere radius</param>
            /// <param name="collision">collision point</param>
            /// <returns></returns>
            internal static bool BetweenDiskAndSphere(in Vector3 diskPlaneNormal, in Vector3 diskCenter, float diskRadius, in Vector3 sphereCenter, float sphereRadius, out Vector3 collision)
            {
                Vector3 sphereCenterProj;
                point.ProjectOnPlane(in sphereCenter, in diskPlaneNormal, in diskCenter, out sphereCenterProj);
                var h = distance.Between(in sphereCenter, in sphereCenterProj);
                if (h > sphereRadius)
                {
                    // sphere does not intersect disk plane
                    collision = Vector3.zero;
                    return false;
                }
                var projCirCenDist = distance.Between(in sphereCenterProj, in diskCenter);
                if (projCirCenDist > (diskRadius + sphereRadius))
                {
                    // maximum extent sphere circle interection with disk plane cannot reach disk plane
                    collision = Vector3.zero;
                    return false;
                }

                var projCirRad = (float)Math.Sqrt(sphereRadius * sphereRadius - h * h);
                if (projCirCenDist > (diskRadius + projCirRad))
                {
                    // real extent sphere circle interection with disk plane cannot reach disk plane
                    collision = Vector3.zero;
                    return false;
                }
                var overlap = diskRadius + projCirRad - projCirCenDist;
                point.MoveAbs(in sphereCenterProj, in diskCenter, projCirRad - overlap * 0.5f, out collision);
                return true;
            }

            /// <summary>
            /// Check if two disks cross each other
            /// </summary>
            /// <param name="disk1PlaneNormal">the normal of the disk 1 plane</param>
            /// <param name="disk1Center">the center of disk 1</param> 
            /// <param name="disk1Radius">the radius of disk 1</param>
            /// <param name="disk2PlaneNormal">the normal of the disk 2 plane</param>
            /// <param name="disk2Center">the center of disk 2</param> 
            /// <param name="disk2Radius">the radius of disk 2</param>
            /// <param name="collision">returns the collision point</param>
            /// <returns></returns>
            internal static bool BetweenDiskAndDisk(
                in Vector3 disk1PlaneNormal, in Vector3 disk1Center, double disk1Radius,
                in Vector3 disk2PlaneNormal, in Vector3 disk2Center, double disk2Radius,
                out Vector3 collision)
            {
                var max = (float)(disk1Radius + disk2Radius);
                var distBetweenCenters = distance.Between(in disk1Center, in disk2Center);
                // the centers are too far and the disks could not overlap
                if (distBetweenCenters > max)
                {
                    collision = Vector3.zero;
                    return false;
                }

                var dpOfDir = dot(in disk1PlaneNormal, in disk2PlaneNormal);
                // if disks point up same or opposite direction
                if (dpOfDir > 0.99999 || dpOfDir < -0.99999)
                {

                    var centerToCenter = disk2Center - disk1Center;
                    if (centerToCenter.sqrMagnitude < 0.0001f)
                    {
                        collision = disk1Center;
                        return true;
                    }
                    centerToCenter = centerToCenter.normalized;
                    var dpOf2 = dot(in disk1PlaneNormal, in centerToCenter);
                    // if the vector between centers and the up vectors are orthogonal (90 degrees) or dot product = 0
                    if (dpOf2 < 0.000001f && dpOf2 > -0.000001f)
                    {
                        // check they are far enough to touch
                        collision = disk1Center + (disk2Center - disk1Center).normalized * (((float)disk1Radius / max) * distBetweenCenters);
                        return true;
                    }
                    collision = Vector3.zero;
                    return false;
                }

                Vector3 normal;
                BetweenPlanes(in disk1PlaneNormal, in disk1Center, in disk2PlaneNormal, in disk2Center, out collision, out normal);

                var collisionPlusNorm = collision + normal;

                Vector3 norm1X, norm1Y;
                vector.ComputeRandomXYAxesForPlane(in disk1PlaneNormal, out norm1X, out norm1Y);
                var a = (collision - disk1Center).As2d(in norm1X, in norm1Y);
                var b = (collisionPlusNorm - disk1Center).As2d(in norm1X, in norm1Y);
                Vector2 projection1;
                var has1 = IsLineGettingCloserToOriginThan(in a, in b, (float)disk1Radius, out projection1);
                if (!has1)
                {
                    collision = Vector3.zero;
                    return false;
                }
                Vector3 norm2X, norm2Y;
                vector.ComputeRandomXYAxesForPlane(in disk2PlaneNormal, out norm2X, out norm2Y);
                a = (collision - disk2Center).As2d(in norm2X, in norm2Y);
                b = (collisionPlusNorm - disk2Center).As2d(in norm2X, in norm2Y);
                Vector2 projection2;
                var has2 = IsLineGettingCloserToOriginThan(in a, in b, (float)disk2Radius, out projection2);
                if (!has2)
                {
                    collision = Vector3.zero;
                    return false;
                }

                Vector3 perp1, perp2;
                projection1.As3d(in disk1Center, in norm1X, in norm1Y, out perp1);
                projection2.As3d(in disk2Center, in norm2X, in norm2Y, out perp2);
                //Debug.DrawLine(perp1,disk1Center,Color.yellow,0,false);
                //Debug.DrawLine(perp2,disk2Center,Color.white,0,false);
                //Debug.DrawLine(collision,collision+normal,Color.blue,0,false);
                //Debug.DrawLine(collision,collision-normal,Color.blue,0,false);
                var perp1to2 = perp2 - perp1;
                if (perp1to2.sqrMagnitude < 0.00001f)
                {
                    collision = perp1;
                    return true;
                }
                // in 2d relative to radius from center to perpendicular
                var relCenToPerp1 = projection1.magnitude / disk1Radius;
                var relCenToPerp2 = projection2.magnitude / disk2Radius;

                var absPerpToEdge1 = sqrt(1f - relCenToPerp1 * relCenToPerp1) * disk1Radius;
                var absPerpToEdge2 = sqrt(1f - relCenToPerp2 * relCenToPerp2) * disk2Radius;

                if (perp1to2.magnitude <= (absPerpToEdge1 + absPerpToEdge2))
                {
                    collision = point.Lerp(in perp1, in perp2, disk1Radius / max);
                    return true;
                }

                collision = Vector3.zero;
                return false;
            }


            /// <summary>
            /// The points of the capsule are the centers of the spheres at the end of capsules
            /// </summary>
            /// <param name="csb">capsule sphere below center</param>
            /// <param name="csa">capsule sphere above center</param>
            /// <param name="capsuleRadius">capsule radius</param>
            /// <param name="diskCenter">The center point of the disk</param>
            /// <param name="diskNormal">disk up normal</param>
            /// <param name="diskRadius">disk radius</param>
            /// <returns></returns>
            internal static bool BetweenCapsuleAndDisk(
                in Vector3 csb, in Vector3 csa, double capsuleRadius,
                in Vector3 diskNormal, in Vector3 diskCenter, double diskRadius)
            {
                // see if disk center is within the capsule cylinder
                Vector3 diskCenterOnAxis;
                var diskCenIsWithinCylPl =
                    point.TryProjectOnLineSegment(in diskCenter, in csb, in csa, out diskCenterOnAxis);
                if (diskCenIsWithinCylPl &&
                    distance.Between(in diskCenterOnAxis, in diskCenter) <= capsuleRadius)
                {
                    return true;
                }
                var capsuleUp = (csa - csb).normalized;
                var maxDistance = capsuleRadius + diskRadius;

                // middle part
                Vector3 diskCenOnCapPl;
                point.ProjectOnPlane(in diskCenter, in capsuleUp, in csb, out diskCenOnCapPl);
                var capToDisk = (diskCenOnCapPl - csb).normalized * (float)capsuleRadius;
                var csaShifted = csa + capToDisk;
                var csbShifted = csb + capToDisk;
                Vector3 c1;
                if (BetweenPlaneAndLineSegment(in diskNormal, in diskCenter, in csaShifted, in csbShifted, out c1))
                {
                    if (distance.Between(in c1, in diskCenter) <= diskRadius)
                    {
                        return true;
                    }
                }


                // test collision with below sphere
                Vector3 capEndOnDiskPl;
                point.ProjectOnPlane(in csb, in diskNormal, in diskCenter, out capEndOnDiskPl);
                var diskToSph = capEndOnDiskPl - diskCenter;
                var diskToSphMag = vector.Magnitude(in diskToSph);
                if (diskToSphMag < 0.00001f)
                {
                    if (distance.Between(in csb, in diskCenter) < maxDistance)
                    {
                        return true;
                    }
                }
                else
                {
                    diskToSph = diskToSph / diskToSphMag;//normalize
                    Vector3 c2;
                    if (BetweenRayAndSphere(in diskToSph, in diskCenter, in csb, capsuleRadius, out c2))
                    {
                        if (distance.Between(in c2, in diskCenter) <= diskRadius)
                        {
                            return true;
                        }
                    }
                }
                // test collision with above sphere
                point.ProjectOnPlane(in csa, in diskNormal, in diskCenter, out capEndOnDiskPl);
                diskToSph = capEndOnDiskPl - diskCenter;
                diskToSphMag = vector.Magnitude(in diskToSph);
                if (diskToSphMag < 0.00001f)
                {
                    if (distance.Between(in csa, in diskCenter) < maxDistance)
                    {
                        return true;
                    }
                }
                else
                {
                    diskToSph = diskToSph / diskToSphMag;//normalize
                    Vector3 c3;
                    if (BetweenRayAndSphere(in diskToSph, in diskCenter, in csa, capsuleRadius, out c3))
                    {
                        if (distance.Between(in c3, in diskCenter) <= diskRadius)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            /// <summary>
            /// The points of the capsule are the centers of the spheres at the end of capsules
            /// </summary>
            /// <param name="csb">capsule sphere below center</param>
            /// <param name="csa">capsule sphere above center</param>
            /// <param name="capsuleRadius">capsule radius</param>
            /// <param name="diskCenter">The center point of the disk</param>
            /// <param name="diskNormal">disk up normal</param>
            /// <param name="diskRadius">disk radius</param>
            /// <param name="collision">the point best describing the collision point - might not be precise!</param>
            /// <returns></returns>
            internal static bool BetweenCapsuleAndDisk(
                in Vector3 csb, in Vector3 csa, float capsuleRadius,
                in Vector3 diskNormal, in Vector3 diskCenter, float diskRadius, out Vector3 collision)
            {
                // see if disk center is within the capsule cylinder
                Vector3 diskCenterOnAxis;
                var diskCenIsWithinCylPl =
                    point.TryProjectOnLineSegment(in diskCenter, in csb, in csa, out diskCenterOnAxis);
                if (diskCenIsWithinCylPl &&
                    distance.Between(in diskCenterOnAxis, in diskCenter) <= capsuleRadius)
                {
                    collision = diskCenter;
                    return true;
                }
                var capsuleUp = (csa - csb).normalized;
                var maxDistance = capsuleRadius + diskRadius;

                // middle part
                Vector3 diskCenOnCapPl;
                point.ProjectOnPlane(in diskCenter, in capsuleUp, in csb, out diskCenOnCapPl);
                var capToDisk = (diskCenOnCapPl - csb).normalized * capsuleRadius;
                var csaShifted = csa + capToDisk;
                var csbShifted = csb + capToDisk;
                if (BetweenPlaneAndLineSegment(in diskNormal, in diskCenter, in csaShifted, in csbShifted, out collision))
                {
                    if (distance.Between(in collision, in diskCenter) <= diskRadius)
                    {
                        return true;
                    }
                }


                // test collision with below sphere
                Vector3 capEndOnDiskPl;
                point.ProjectOnPlane(in csb, in diskNormal, in diskCenter, out capEndOnDiskPl);
                var diskToSph = capEndOnDiskPl - diskCenter;
                var diskToSphMag = vector.Magnitude(in diskToSph);
                if (diskToSphMag < 0.00001f)
                {
                    if (distance.Between(in csb, in diskCenter) < maxDistance)
                    {
                        var diff = (csb - diskCenter);
                        var diffMag = vector.Magnitude(in diff);
                        collision = (diffMag < 0.00001) ? diskCenter : diskCenter + (diff / diffMag) * min(diffMag / 2, diskRadius);
                        return true;
                    }
                }
                else
                {
                    diskToSph = diskToSph / diskToSphMag;//normalize
                    if (BetweenRayAndSphere(in diskToSph, in diskCenter, in csb, capsuleRadius, out collision))
                    {
                        if (distance.Between(in collision, in diskCenter) <= diskRadius)
                        {
                            Vector3 collisionOnAxis;
                            if (point.TryProjectOnLineSegment(in collision, in csb, in csa, out collisionOnAxis))
                            {
                                Vector3 diskOnPlane;
                                point.ProjectOnPlane(in diskCenter, in capsuleUp, in collisionOnAxis, out diskOnPlane);
                                collision = (diskOnPlane - collisionOnAxis).normalized * capsuleRadius + collisionOnAxis;
                            }
                            return true;
                        }
                    }
                }
                // test collision with above sphere
                point.ProjectOnPlane(in csa, in diskNormal, in diskCenter, out capEndOnDiskPl);
                diskToSph = capEndOnDiskPl - diskCenter;
                diskToSphMag = vector.Magnitude(in diskToSph);
                if (diskToSphMag < 0.00001f)
                {
                    if (distance.Between(in csa, in diskCenter) < maxDistance)
                    {
                        var diff = (csa - diskCenter);
                        var diffMag = vector.Magnitude(in diff);
                        collision = diffMag < 0.00001 ? diskCenter : diskCenter + (diff / diffMag) * min(diffMag / 2, diskRadius);
                        return true;
                    }
                }
                else
                {
                    diskToSph = diskToSph / diskToSphMag;//normalize
                    if (BetweenRayAndSphere(in diskToSph, in diskCenter, in csa, capsuleRadius, out collision))
                    {
                        if (distance.Between(in collision, in diskCenter) <= diskRadius)
                        {
                            Vector3 collisionOnAxis;
                            if (point.TryProjectOnLineSegment(in collision, in csb, in csa, out collisionOnAxis))
                            {
                                Vector3 diskOnPlane;
                                point.ProjectOnPlane(in diskCenter, in capsuleUp, in collisionOnAxis, out diskOnPlane);
                                collision = (diskOnPlane - collisionOnAxis).normalized * capsuleRadius + collisionOnAxis;
                            }
                            return true;
                        }
                    }
                }

                collision = Vector3.zero;
                return false;
            }


            internal static bool BetweenRayAndSphere(
                in Vector3 rayFw, in Vector3 rayOr,
                in Vector3 sphereCenter, double sphereRadius,
                out Vector3 collision)
            {
                var radiusSquared = sphereRadius * sphereRadius;
                var rayToSphere = sphereCenter - rayOr;
                var tca = fun.dot(in rayToSphere, in rayFw);
                var d2 = fun.dot(in rayToSphere, in rayToSphere) - tca * tca;
                if (d2 > radiusSquared)
                {
                    collision = Vector3.zero;
                    return false;
                }
                var thc = (float)Math.Sqrt(radiusSquared - d2);
                var t0 = tca - thc;
                var t1 = tca + thc;

                if (t0 > t1)
                {
                    var temp = t0;
                    t0 = t1;
                    t1 = temp;
                }

                if (t0 < 0)
                {
                    t0 = t1; // if t0 is negative, let's use t1 instead 
                    if (t0 < 0)
                    {
                        collision = Vector3.zero;
                        return false; // both t0 and t1 are negative 
                    }
                }

                var t = t0;

                collision = rayOr + rayFw * t;

                return true;
            }
            internal static bool BetweenRayAndCapsule(
                in Vector3 rayFw, in Vector3 rayOr,
                in Vector3 cpu, in Vector3 cpd, float capsuleRadius,
                out Vector3 collision)
            {
                var capDir = (cpu - cpd);
                if (capDir.sqrMagnitude < 0.00001f)
                {
                    // the two capsule ends are so close so it is esencially a sphere
                    return BetweenRayAndSphere(in rayFw, in rayOr, in cpu, capsuleRadius, out collision);
                }
                capDir.Normalize();
                Vector3 pl1, pl2;
                point.ClosestOnTwoLinesByPointAndDirection(in rayOr, in rayFw, in cpd, in capDir, out pl1, out pl2);
                if (!point.IsOnSegment(in cpu, in pl2, in cpd))
                {
                    if (BetweenRayAndSphere(in rayFw, in rayOr, in cpu, capsuleRadius, out collision)) return true;
                    if (BetweenRayAndSphere(in rayFw, in rayOr, in cpd, capsuleRadius, out collision)) return true;
                    return false;
                }

                var d = distance.Between(in pl1, in pl2);
                var hasCollision = d <= capsuleRadius;
                if (hasCollision)
                {
                    var n = Math.Sqrt(capsuleRadius * capsuleRadius - d * d);
                    point.MoveAbs(in pl1, in rayOr, (float)n, out collision);
                    return true;
                }
                collision = Vector3.zero;
                return false;
            }

            /// <summary>
            /// The points of the capsule are the centers of the spheres at the end of capsules
            /// </summary>
            /// <param name="csb">capsule sphere below center</param>
            /// <param name="csa">capsule sphere above center</param>
            /// <param name="capsuleRadius">capsule radius</param>
            /// <param name="sphereCenter">sphere center point</param>
            /// <param name="sphereRadius">sphere radius</param>
            /// <returns></returns>
            internal static bool BetweenCapsuleAndSphere(
                in Vector3 csb, in Vector3 csa, double capsuleRadius,
                in Vector3 sphereCenter, double sphereRadius)
            {
                var maxDistance = capsuleRadius + sphereRadius;
                // is in the middle
                Vector3 proj;
                if (point.TryProjectOnLineSegment(in sphereCenter, in csb, in csa, out proj))
                {
                    return distance.Between(in sphereCenter, in proj) <= maxDistance;
                }
                var aboveVec = (csa - csb).normalized;
                // is above
                if (point.IsAbovePlane(in sphereCenter, in aboveVec, in csa))
                {
                    return distance.Between(in sphereCenter, in csa) <= maxDistance;
                }
                var belowVec = -aboveVec;
                // is below
                if (point.IsAbovePlane(in sphereCenter, in belowVec, in csb))
                {
                    return distance.Between(in sphereCenter, in csb) <= maxDistance;
                }
                return false;
            }

            /// <summary>
            /// The points of the capsule are the centers of the spheres at the end of capsules
            /// </summary>
            /// <param name="csb">capsule sphere below center</param>
            /// <param name="csa">capsule sphere above center</param>
            /// <param name="capsuleRadius">capsule radius</param>
            /// <param name="sphereCenter">sphere center point</param>
            /// <param name="sphereRadius">sphere radius</param>
            /// <param name="collision">the point of collision</param>
            /// <returns></returns>
            internal static bool BetweenCapsuleAndSphere(
                in Vector3 csb, in Vector3 csa, double capsuleRadius,
                in Vector3 sphereCenter, double sphereRadius, out Vector3 collision)
            {
                var maxDistance = capsuleRadius + sphereRadius;
                // is in the middle
                Vector3 proj;
                if (point.TryProjectOnLineSegment(in sphereCenter, in csb, in csa, out proj))
                {
                    return HasOverlapOfTwoSpheres(
                        in sphereCenter, in proj, maxDistance, sphereRadius, out collision);
                }
                var aboveVec = (csa - csb).normalized;
                // is above
                if (point.IsAbovePlane(in sphereCenter, in aboveVec, in csa))
                {
                    return HasOverlapOfTwoSpheres(
                        in sphereCenter, in csa, maxDistance, sphereRadius, out collision);
                }
                var belowVec = -aboveVec;
                // is below
                if (point.IsAbovePlane(in sphereCenter, in belowVec, in csb))
                {
                    return HasOverlapOfTwoSpheres(
                        in sphereCenter, in csb, maxDistance, sphereRadius, out collision);
                }
                collision = Vector3.zero;
                return false;
            }

            private static bool HasOverlapOfTwoSpheres(
                in Vector3 sphereCenter1, in Vector3 sphereCenter2,
                double sumOfRadii, double sphereRadius1, out Vector3 collision)
            {
                var d = distance.Between(in sphereCenter1, in sphereCenter2);
                var has = d <= sumOfRadii;
                if (has)
                {
                    var ratio = sphereRadius1 / sumOfRadii;
                    point.TryMoveAbs(in sphereCenter1, in sphereCenter2, d * ratio, out collision);
                    return true;
                }
                collision = Vector3.zero;
                return false;
            }


            /// <summary>
            /// The points are the centers of the spheres at the end of capsules
            /// </summary>
            /// <param name="c1sb">capsule 1 sphere below center</param>
            /// <param name="c1sa">capsule 1 sphere above center</param>
            /// <param name="radius1">radius of capsule 1 sphere</param>
            /// <param name="c2sb">capsule 2 sphere below center</param>
            /// <param name="c2sa">capsule 2 sphere above center</param>
            /// <param name="radius2">radius of capsule 2 sphere</param>
            /// <returns>true if there is overlap false otherwise</returns>
            internal static bool BetweenCapsules(
                in Vector3 c1sb, in Vector3 c1sa, double radius1,
                in Vector3 c2sb, in Vector3 c2sa, double radius2)
            {
                Vector3 closest1, closest2;
                point.ClosestOnTwoLineSegments(in c1sb, in c1sa, in c2sb, in c2sa, out closest1, out closest2);
                var dist = distance.Between(in closest1, in closest2);
                if (dist < (radius1 + radius2))
                {
                    return true;
                }
                return false;
            }

            /// <summary>
            /// The points are the centers of the spheres at the end of capsules
            /// </summary>
            /// <param name="c1sb">capsule 1 sphere below center</param>
            /// <param name="c1sa">capsule 1 sphere above center</param>
            /// <param name="radius1">radius of capsule 1 sphere</param>
            /// <param name="c2sb">capsule 2 sphere below center</param>
            /// <param name="c2sa">capsule 2 sphere above center</param>
            /// <param name="radius2">radius of capsule 2 sphere</param>
            /// <param name="collision">The point of collision</param>
            /// <returns>true if there is overlap false otherwise</returns>
            internal static bool BetweenCapsules(
                in Vector3 c1sb, in Vector3 c1sa, double radius1,
                in Vector3 c2sb, in Vector3 c2sa, double radius2, out Vector3 collision)
            {
                Vector3 closest1, closest2;
                point.ClosestOnTwoLineSegments(in c1sb, in c1sa, in c2sb, in c2sa, out closest1, out closest2);
                var dist = distance.Between(in closest1, in closest2);
                if (dist < (radius1 + radius2))
                {
                    if (dist < 0.000001)
                    {
                        collision = closest1;
                        return true;
                    }

                    collision = closest1 + (closest2 - closest1).normalized * (float)radius1;
                    return true;
                }
                collision = Vector3.zero;
                return false;
            }

            internal static bool BetweenSpheres(
                in Vector3 sphereCenter1, double radius1,
                in Vector3 sphereCenter2, double radius2)
            {
                var sum = radius1 + radius2;
                if (sum < 0.0001)
                {
                    return sphereCenter1.IsEqual(sphereCenter2, 0.0001);
                }
                var dist = fun.distance.Between(in sphereCenter1, in sphereCenter2);
                return dist <= (radius1 + radius2);
            }

            internal static bool BetweenSpheres(
                in Vector3 sphereCenter1, double radius1,
                in Vector3 sphereCenter2, double radius2, out Vector3 middle)
            {
                var sum = radius1 + radius2;
                if (sum < 0.0001)
                {
                    middle = sphereCenter1;
                    return sphereCenter1.IsEqual(sphereCenter2, 0.0001);
                }
                var dist = fun.distance.Between(in sphereCenter1, in sphereCenter2);
                middle = sphereCenter1.MoveTowards(sphereCenter2, (radius1 / sum) * dist);
                return dist <= (radius1 + radius2);
            }



            private static bool IsLineGettingCloserToOriginThan(in Vector2 a, in Vector2 b, float maxDist, out Vector2 projection)
            {
                var k = ((b.y - a.y) * -a.x - (b.x - a.x) * -a.y) / ((b.y - a.y) * (b.y - a.y) + (b.x - a.x) * (b.x - a.x));
                projection = new Vector2(-k * (b.y - a.y), k * (b.x - a.x));
                return projection.magnitude <= maxDist;
            }






            internal static bool BetweenLines(in Vector3 ray1Origin, in Vector3 ray1Dir, in Vector3 ray2Origin, in Vector3 ray2Dir, out Vector3 intersection)
            {
                var lineVec3 = ray2Origin - ray1Origin;
                var crossVec1and2 = cross.Product(in ray1Dir, in ray2Dir);
                var crossVec3and2 = cross.Product(in lineVec3, in ray2Dir);

                var planarFactor = dot(in lineVec3, in crossVec1and2);

                //is coplanar, and not parrallel
                if (abs(planarFactor) < 0.0001f && crossVec1and2.sqrMagnitude > 0.0001f)
                {
                    var s = dot(in crossVec3and2, in crossVec1and2) / crossVec1and2.sqrMagnitude;
                    intersection = ray1Origin + (ray1Dir * s);
                    return true;
                }
                intersection = Vector3.zero;
                return false;
            }

            internal static bool BetweenTriangleAndLineSegment(
                in Vector3 p1, in Vector3 p2, in Vector3 p3,
                in Vector3 line1, in Vector3 line2)
            {
                var diff = line1 - line2;
                var rayForward = diff.normalized;
                var rayOrigin = line2;
                var rayCollides = BetweenTriangleAndRay(in p1, in p2, in p3, in rayForward, in rayOrigin);
                if (rayCollides)
                {
                    Vector3 planeNormal;
                    point.GetNormal(in p1, in p2, in p3, out planeNormal);
                    Vector3 collision;
                    if (BetweenPlaneAndRay(in planeNormal, in p1, in rayForward, in rayOrigin, out collision))
                    {
                        return (collision - rayOrigin).sqrMagnitude <= diff.sqrMagnitude;
                    }
                }
                return false;
            }

            internal static bool BetweenTriangleAndLineSegment(
                in Vector3 p1, in Vector3 p2, in Vector3 p3,
                in Vector3 line1, in Vector3 line2, out Vector3 collision)
            {
                var diff = line1 - line2;
                var rayForward = diff.normalized;
                var rayOrigin = line2;
                var rayCollides = BetweenTriangleAndRay(in p1, in p2, in p3, in rayForward, in rayOrigin);
                if (rayCollides)
                {
                    Vector3 planeNormal;
                    point.GetNormal(in p1, in p2, in p3, out planeNormal);
                    if (BetweenPlaneAndRay(in planeNormal, in p1, in rayForward, in rayOrigin, out collision))
                    {
                        return (collision - rayOrigin).sqrMagnitude <= diff.sqrMagnitude;
                    }
                }
                collision = Vector3.zero;
                return false;
            }

            internal static bool BetweenTriangleAndRay(in Vector3 p1, in Vector3 p2, in Vector3 p3, in Vector3 rayForward, in Vector3 rayOrigin)
            {
                //Find vectors for two edges sharing vertex/point p1
                var e1 = p2 - p1;
                var e2 = p3 - p1;

                // calculating determinant 
                var p = cross.Product(in rayForward, in e2);

                //Calculate determinat
                var det = dot(in e1, in p);

                //if determinant is near zero, ray lies in plane of triangle otherwise not
                if (det > -0.000001 && det < 0.000001) { return false; }
                var invDet = 1.0f / det;

                //calculate distance from p1 to ray origin
                var t = rayOrigin - p1;

                //Calculate u parameter
                var u = dot(in t, in p) * invDet;

                //Check for ray hit
                if (u < 0 || u > 1) { return false; }

                //Prepare to test v parameter
                var q = cross.Product(in t, in e1);

                //Calculate v parameter
                var v = dot(in rayForward, in q) * invDet;

                //Check for ray hit
                if (v < 0 || u + v > 1) { return false; }

                if ((dot(in e2, in q) * invDet) > 0.000001)
                {
                    //ray does intersect
                    return true;
                }

                // No hit at all
                return false;
            }

            internal static bool BetweenPlaneAndLine(
                in Vector3 planeNormal, in Vector3 planePoint,
                in Vector3 lineA, in Vector3 lineB, out Vector3 intersection)
            {
                planeNormal.Normalize();// that changes state but plane normal should always be normal
                var rayOrigin = lineA;
                var rayNormal = lineB - lineA;
                // if line points are the same
                if (rayNormal.sqrMagnitude < 0.00001)
                {
                    point.ProjectOnPlane(in lineA, in planeNormal, in planePoint, out intersection);
                    return true;
                }
                rayNormal.Normalize();
                var planeDistance = -dot(in planeNormal, in planePoint);
                var a = dot(in rayNormal, in planeNormal);
                var num = -dot(in rayOrigin, in planeNormal) - planeDistance;
                // if line is parallel to the plane
                if (a < 0.000001 && a > -0.000001)
                {
                    intersection = Vector3.zero;
                    return false;
                }
                var distanceToCollision = num / a;
                intersection = lineA + rayNormal * distanceToCollision;
                return true;
            }

            internal static bool BetweenPlaneAndRay(
                in Vector3 planeNormal, in Vector3 planePoint,
                in Vector3 rayNormal, in Vector3 rayOrigin, out float distanceToCollision)
            {
                var norm = planeNormal.normalized;
                var planeDistance = -dot(in norm, in planePoint);
                var a = dot(in rayNormal, in norm);
                var num = -dot(in rayOrigin, in norm) - planeDistance;
                if (a < 0.000001f && a > -0.000001f)
                {
                    distanceToCollision = 0.0f;
                    return false;
                }
                distanceToCollision = num / a;
                return distanceToCollision > 0.0;
            }

            internal static bool BetweenPlaneAndRay(
                in Vector3 planeNormal, in Vector3 planePoint,
                in Vector3 rayNormal, in Vector3 rayOrigin,
                out Vector3 collisionPoint)
            {
                var norm = planeNormal.normalized;
                var planeDistance = -dot(in norm, in planePoint);
                var a = dot(in rayNormal, in norm);
                var num = -dot(in rayOrigin, in norm) - planeDistance;
                if (a < 0.000001f && a > -0.000001f)
                {
                    collisionPoint = Vector3.zero;
                    return false;
                }
                var distanceToCollision = num / a;
                if (distanceToCollision > 0.0)
                {
                    collisionPoint = rayOrigin + rayNormal * distanceToCollision;
                    return true;
                }
                collisionPoint = Vector3.zero;
                return false;
            }

            internal static bool BetweenPlaneAndRay(
                in Vector3 planeNormal, in Vector3 planePoint,
                in Vector3 rayNormal, in Vector3 rayOrigin,
                out float distanceToCollision, out Vector3 collisionPoint)
            {
                var norm = planeNormal.normalized;
                var planeDistance = -dot(in norm, in planePoint);
                var a = dot(in rayNormal, in norm);
                var num = -dot(in rayOrigin, in norm) - planeDistance;
                if (a < 0.000001f && a > -0.000001f)
                {
                    collisionPoint = Vector3.zero;
                    distanceToCollision = 0;
                    return false;
                }
                distanceToCollision = num / a;
                if (distanceToCollision > 0.0)
                {
                    collisionPoint = rayOrigin + rayNormal * distanceToCollision;
                    return true;
                }
                distanceToCollision = 0;
                collisionPoint = Vector3.zero;
                return false;
            }

            internal static bool BetweenPlaneAndLineSegment(
                in Vector3 planeNormal, in Vector3 planePoint,
                in Vector3 line1, in Vector3 line2, out Vector3 collisionPoint)
            {
                var distanceToCollision = 0f;
                return
                    BetweenPlaneAndLineSegment(in planeNormal, in planePoint,
                        in line1, in line2,
                        out distanceToCollision, out collisionPoint);
            }
            internal static bool BetweenPlaneAndLineSegment(
                in Vector3 planeNormal, in Vector3 planePoint,
                in Vector3 line1, in Vector3 line2, out float distanceToCollision, out Vector3 collisionPoint)
            {
                var rayNormal = (line2 - line1).normalized;
                var norm = planeNormal.normalized;
                var planeDistance = -dot(in norm, in planePoint);
                var a = dot(in rayNormal, in norm);
                var num = -dot(in line1, in norm) - planeDistance;
                if (a < 0.000001f && a > -0.000001f)
                {
                    collisionPoint = Vector3.zero;
                    distanceToCollision = 0;
                    return false;
                }
                distanceToCollision = num / a;
                if (distanceToCollision > 0.0000001 || distanceToCollision < -0.0000001)
                {
                    collisionPoint = line1 + rayNormal * distanceToCollision;
                    if (!point.IsOnSegment(in line1, in collisionPoint, in line2))
                    {
                        collisionPoint = Vector3.zero;
                        distanceToCollision = 0;
                        return false;
                    }
                    distanceToCollision = abs(distanceToCollision);
                    return true;
                }
                distanceToCollision = 0;
                collisionPoint = Vector3.zero;
                return false;
            }

            internal static int BetweenLineAndUnitCircle(
                Vector2 point1, Vector2 point2,
                out Vector2 intersection1, out Vector2 intersection2)
            {
                float t;

                var dx = point2.x - point1.x;
                var dy = point2.y - point1.y;

                var a = dx * dx + dy * dy;
                var b = 2 * (dx * point1.x + dy * point1.y);
                var c = point1.x * point1.x + point1.y * point1.y - 1;

                var determinate = b * b - 4 * a * c;
                if ((a <= 0.0000001) || (determinate < -0.0000001))
                {
                    // No real solutions.
                    intersection1 = new Vector2(float.NaN, float.NaN);
                    intersection2 = new Vector2(float.NaN, float.NaN);
                    return 0;
                }
                if (determinate < 0.0000001 && determinate > -0.0000001)
                {
                    // One solution.
                    t = -b / (2 * a);
                    intersection1 = new Vector2(point1.x + t * dx, point1.y + t * dy);
                    intersection2 = new Vector2(float.NaN, float.NaN);
                    return 1;
                }

                // Two solutions.
                t = (float)((-b + Math.Sqrt(determinate)) / (2 * a));
                intersection1 = new Vector2(point1.x + t * dx, point1.y + t * dy);
                t = (float)((-b - Math.Sqrt(determinate)) / (2 * a));
                intersection2 = new Vector2(point1.x + t * dx, point1.y + t * dy);
                return 2;
            }
            internal static int BetweenLineAndCircle2d(
                Vector2 circleCenter, float circleRadius,
                Vector2 point1, Vector2 point2,
                out Vector2 intersection1, out Vector2 intersection2)
            {
                float t;

                var dx = point2.x - point1.x;
                var dy = point2.y - point1.y;

                var a = dx * dx + dy * dy;
                var b = 2 * (dx * (point1.x - circleCenter.x) + dy * (point1.y - circleCenter.y));
                var c = (point1.x - circleCenter.x) * (point1.x - circleCenter.x) + (point1.y - circleCenter.y) * (point1.y - circleCenter.y) - circleRadius * circleRadius;

                var determinate = b * b - 4 * a * c;
                if ((a <= 0.0000001) || (determinate < -0.0000001))
                {
                    // No real solutions.
                    intersection1 = new Vector2(float.NaN, float.NaN);
                    intersection2 = new Vector2(float.NaN, float.NaN);
                    return 0;
                }
                if (determinate < 0.0000001 && determinate > -0.0000001)
                {
                    // One solution.
                    t = -b / (2 * a);
                    intersection1 = new Vector2(point1.x + t * dx, point1.y + t * dy);
                    intersection2 = new Vector2(float.NaN, float.NaN);
                    return 1;
                }

                // Two solutions.
                t = (float)((-b + Math.Sqrt(determinate)) / (2 * a));
                intersection1 = new Vector2(point1.x + t * dx, point1.y + t * dy);
                t = (float)((-b - Math.Sqrt(determinate)) / (2 * a));
                intersection2 = new Vector2(point1.x + t * dx, point1.y + t * dy);
                return 2;
            }

            internal static int BetweenLineAndCircle2d(
                in Vector2 circleCenter, double circleRadius,
                in Vector2 point1, in Vector2 point2,
                out Vector2 intersection1, out Vector2 intersection2)
            {
                float t;

                var dx = point2.x - point1.x;
                var dy = point2.y - point1.y;

                var a = dx * dx + dy * dy;
                var b = 2 * (dx * (point1.x - circleCenter.x) + dy * (point1.y - circleCenter.y));
                var c = (point1.x - circleCenter.x) * (point1.x - circleCenter.x) + (point1.y - circleCenter.y) * (point1.y - circleCenter.y) - circleRadius * circleRadius;

                var determinate = b * b - 4 * a * c;
                if ((a <= 0.0000001) || (determinate < -0.0000001))
                {
                    // No real solutions.
                    intersection1 = new Vector2(float.NaN, float.NaN);
                    intersection2 = new Vector2(float.NaN, float.NaN);
                    return 0;
                }
                if (determinate < 0.0000001 && determinate > -0.0000001)
                {
                    // One solution.
                    t = -b / (2 * a);
                    intersection1 = new Vector2(point1.x + t * dx, point1.y + t * dy);
                    intersection2 = new Vector2(float.NaN, float.NaN);
                    return 1;
                }

                // Two solutions.
                t = (float)((-b + Math.Sqrt(determinate)) / (2 * a));
                intersection1 = new Vector2(point1.x + t * dx, point1.y + t * dy);
                t = (float)((-b - Math.Sqrt(determinate)) / (2 * a));
                intersection2 = new Vector2(point1.x + t * dx, point1.y + t * dy);
                return 2;
            }

            internal static bool BetweenLinesIgnoreY(
                   in Vector3 line1point1, in Vector3 line1point2,
                   in Vector3 line2point1, in Vector3 line2point2,
                   out Vector3? intersection)
            {
                var a1 = line1point2.z - line1point1.z;
                var b1 = line1point1.x - line1point2.x;
                var c1 = a1 * line1point1.x + b1 * line1point1.z;

                var a2 = line2point2.z - line2point1.z;
                var b2 = line2point1.x - line2point2.x;
                var c2 = a2 * line2point1.x + b2 * line2point1.z;

                var det = a1 * b2 - a2 * b1;
                const float epsilon = 0.00001f;
                if (det < epsilon && det > -epsilon)
                {
                    // Lines are parallel
                    intersection = null;
                    return false;
                }
                var x = (b2 * c1 - b1 * c2) / det;
                var z = (a1 * c2 - a2 * c1) / det;


                intersection = new Vector3(x, (line1point1.y + line1point2.y) / 2f, z);

                var maxX1 = Math.Max(line1point1.x, line1point2.x);
                var minX1 = Math.Min(line1point1.x, line1point2.x);
                var maxZ1 = Math.Max(line1point1.z, line1point2.z);
                var minZ1 = Math.Min(line1point1.z, line1point2.z);
                var maxX2 = Math.Max(line2point1.x, line2point2.x);
                var minX2 = Math.Min(line2point1.x, line2point2.x);
                var maxZ2 = Math.Max(line2point1.z, line2point2.z);
                var minZ2 = Math.Min(line2point1.z, line2point2.z);

                return x >= minX1 && x <= maxX1 &&
                       z >= minZ1 && z <= maxZ1 &&
                       x >= minX2 && x <= maxX2 &&
                       z >= minZ2 && z <= maxZ2;
            }

            internal static bool BetweenPlanes(in Vector3 plane1Normal, in Vector3 plane2Normal, out Vector3 intersectionNormal)
            {
                intersectionNormal = cross.Product(in plane1Normal, in plane2Normal);
                intersectionNormal = intersectionNormal.normalized;
                return true;
            }
            internal static bool BetweenPlanes(
                in Vector3 plane1Normal, in Vector3 plane1Position,
                in Vector3 plane2Normal, in Vector3 plane2Position,
                out Vector3 intersectionPoint, out Vector3 intersectionNormal)
            {
                intersectionPoint = Vector3.zero;

                //We can get the direction of the line of intersection of the two planes by calculating the 
                //cross product of the normals of the two planes. Note that this is just a direction and the line
                //is not fixed in space yet. We need a point for that to go with the line vector.
                cross.Product(in plane1Normal, in plane2Normal, out intersectionNormal);
                intersectionNormal.Normalize();


                //Next is to calculate a point on the line to fix it's position in space. This is done by finding a vector from
                //the plane2 location, moving parallel to it's plane, and intersecting plane1. To prevent rounding
                //errors, this vector also has to be perpendicular to lineDirection. To get this vector, calculate
                //the cross product of the normal of plane2 and the lineDirection.		
                Vector3 ldir = cross.Product(in plane2Normal, in intersectionNormal);

                var denominator = dot(in plane1Normal, in ldir);
                //Prevent divide by zero
                if (abs(denominator) > 0.00001f)
                {

                    var plane1ToPlane2 = plane1Position - plane2Position;
                    var t = dot(in plane1Normal, in plane1ToPlane2) / denominator;
                    intersectionPoint = plane2Position + t * ldir;
                    intersectionNormal = intersectionNormal.normalized;
                    return true;
                }
                return false;
            }

            internal static bool BetweenPlanes(Vector3 plane1Normal, Vector3 plane2Normal, out Vector3 intersectionNormal)
            {
                var zero = Vector3.zero;
                Vector3 intersectionPoint;
                return BetweenPlanes(plane1Normal, zero, plane1Normal, zero, out intersectionPoint, out intersectionNormal);
            }

            internal static bool BetweenPlanes(
                Vector3 plane1Normal, Vector3 plane1Position,
                Vector3 plane2Normal, Vector3 plane2Position,
                out Vector3 intersectionPoint, out Vector3 intersectionNormal)
            {
                intersectionPoint = Vector3.zero;

                //We can get the direction of the line of intersection of the two planes by calculating the 
                //cross product of the normals of the two planes. Note that this is just a direction and the line
                //is not fixed in space yet. We need a point for that to go with the line vector.
                intersectionNormal = Vector3.Cross(plane1Normal, plane2Normal);

                //Next is to calculate a point on the line to fix it's position in space. This is done by finding a vector from
                //the plane2 location, moving parallel to it's plane, and intersecting plane1. To prevent rounding
                //errors, this vector also has to be perpendicular to lineDirection. To get this vector, calculate
                //the cross product of the normal of plane2 and the lineDirection.		
                Vector3 ldir = Vector3.Cross(plane2Normal, intersectionNormal);

                float denominator = Vector3.Dot(plane1Normal, ldir);

                //Prevent divide by zero
                if (Mathf.Abs(denominator) > 0.001f)
                {

                    Vector3 plane1ToPlane2 = plane1Position - plane2Position;
                    float t = Vector3.Dot(plane1Normal, plane1ToPlane2) / denominator;
                    intersectionPoint = plane2Position + t * ldir;

                    return true;
                }
                return false;
            }

            internal static float BetweenRayAndSphere(
                in Vector3 rayDirection,
                in Vector3 rayOrigin,
                in Vector3 sphereCenter,
                double radius)
            {
                var xDiff = sphereCenter.x - rayOrigin.x;
                var yDiff = sphereCenter.y - rayOrigin.y;
                var zDiff = sphereCenter.z - rayOrigin.z;
                var sumOfSquares = ((xDiff * xDiff) + (yDiff * yDiff)) + (zDiff * zDiff);
                var squareOfRadius = radius * radius;
                if (sumOfSquares <= squareOfRadius)
                {
                    return 0f;
                }
                var num = ((xDiff * rayDirection.x) + (yDiff * rayDirection.y)) + (zDiff * rayDirection.z);
                if (num < 0f)
                {
                    return Single.NaN;
                }
                var sqDiff = sumOfSquares - (num * num);
                if (sqDiff > squareOfRadius)
                {
                    return Single.NaN;
                }
                return num - (float)Math.Sqrt(squareOfRadius - sqDiff);
            }

            internal static bool TryBetweenRayAndSphere(
                in Vector3 rayDirection,
                in Vector3 rayOrigin,
                in Vector3 sphereCenter,
                double radius)
            {
                var xDiff = sphereCenter.x - rayOrigin.x;
                var yDiff = sphereCenter.y - rayOrigin.y;
                var zDiff = sphereCenter.z - rayOrigin.z;
                var sumOfSquares = ((xDiff * xDiff) + (yDiff * yDiff)) + (zDiff * zDiff);
                var squareOfRadius = radius * radius;
                if (sumOfSquares <= squareOfRadius)
                {
                    return true;
                }
                var num = ((xDiff * rayDirection.x) + (yDiff * rayDirection.y)) + (zDiff * rayDirection.z);
                if (num < 0f)
                {
                    return false;
                }
                var sqDiff = sumOfSquares - (num * num);
                if (sqDiff > squareOfRadius)
                {
                    return false;
                }
                return true;
            }

            internal static bool BetweenRayAndDisk(
                in Vector3 rayDirection,
                in Vector3 rayOrigin,
                in Vector3 diskNormal,
                in Vector3 diskCenter,
                double diskRadius,
                out Vector3 crossPoint)
            {
                Vector3 intersectPlane;
                var hasIntersection =
                    BetweenPlaneAndRay(
                        in diskNormal, in diskCenter,
                        in rayDirection, in rayOrigin, out intersectPlane);

                // if ray lies in the disk plane, then turn it into 2d problem
                if (abs(dot(in diskNormal, in rayDirection)) < 0.0001f)
                {
                    Vector3 x2d, y2d;
                    vector.ComputeRandomXYAxesForPlane(in diskNormal, out x2d, out y2d);

                    var line1 = rayOrigin.As2d(in diskCenter, in x2d, in y2d);
                    var line2 = (rayOrigin + rayDirection).As2d(in diskCenter, in x2d, in y2d);

                    var cirCen = Vector2.zero;
                    Vector2 int1, int2;

                    var found = BetweenLineAndCircle2d(in cirCen, diskRadius, in line1, in line2, out int1, out int2) > 0;

                    if (found)
                    {
                        var ds1 = distanceSquared.Between(in int1, in line1);
                        var ds2 = distanceSquared.Between(in int2, in line1);
                        crossPoint = (ds1 > ds2 ? int1 : int2).As3d(in diskCenter, in x2d, in y2d);
                    }
                    else
                    {
                        crossPoint = Vector3.zero;
                    }

                    return found;
                }

                if (!hasIntersection)
                {
                    crossPoint = Vector3.zero;
                    return false;
                }

                // the ray is intersecting disk plane in a point outside the disk
                if (distance.Between(in intersectPlane, in diskCenter) > diskRadius)
                {
                    crossPoint = Vector3.zero;
                    return false;
                }

                crossPoint = intersectPlane;
                return true;
            }

            // https://searchcode.com/codesearch/view/16225010/
            internal static bool BetweenLineSegmentAndCone(
                in Vector3 lineStart, in Vector3 lineEnd,
                in Vector3 coneBase, in Vector3 coneUp, double coneRadius, double coneHeight,
                out Vector3 collision)
            {
                const float epsilon = 0.000001f;
                if (coneHeight < epsilon)
                {
                    if (coneRadius < epsilon)
                    {
                        Vector3 coneBaseOnLine;
                        point.ProjectOnLine(in coneBase, in lineEnd, in lineStart, out coneBaseOnLine);

                        var hasCollision = distance.Between(in coneBaseOnLine, in coneBase) < epsilon;
                        collision = hasCollision ? coneBaseOnLine : Vector3.zero;
                        return hasCollision;
                    }

                    return BetweenLineSegmentAndDisk(in lineEnd, in lineStart, in coneUp, in coneBase, coneRadius, out collision);
                }
                var coneNor = coneUp.normalized;
                var coneTop = coneBase + coneNor * (float)coneHeight;
                if (coneRadius < epsilon)
                {
                    Vector3 closest1, closest2;
                    point.ClosestOnTwoLineSegments(in lineEnd, in lineStart, in coneBase, in coneTop, out closest1, out closest2);

                    var hasCollision = distance.Between(in closest1, in closest2) < epsilon;
                    collision = hasCollision ? closest1 : Vector3.zero;
                    return hasCollision;
                }

                var coneRadiusSqr = coneRadius * coneRadius;
                var coneHeightSqr = coneHeight * coneHeight;

                var fac = coneRadiusSqr / coneHeightSqr;

                var rayOr = lineStart;
                var rayVec = lineEnd - lineStart;
                var rayMag = vector.Magnitude(in rayVec);
                var rayFw = rayMag < 0.000001 ? Vector3.up : rayVec / rayMag;


                // cylinder part
                var yA = (fac) * rayFw.y * rayFw.y;
                var yB = (2 * fac * rayOr.y * rayFw.y) - (2 * coneRadiusSqr / coneHeight) * rayFw.y;
                var yC = (fac * rayOr.y * rayOr.y) - ((2 * coneRadiusSqr / coneHeight) * rayOr.y) + coneRadiusSqr;

                var A = (rayFw.x * rayFw.x) + (rayFw.z * rayFw.z) - yA;
                var B = (2 * rayOr.x * rayFw.x) + (2 * rayOr.z * rayFw.z) - yB;
                var C = (rayOr.x * rayOr.x) + (rayOr.z * rayOr.z) - yC;

                var d = (B * B) - (4 * A * C);

                var t = new float[] { 0.0f, 0.0f };
                var t_near = float.MaxValue;

                var near = Vector3.zero;
                var tempNear = Vector3.zero;

                if (d >= 0)
                {
                    t[0] = (float)((-B - Math.Sqrt(d)) / (2 * A));
                    t[1] = (float)((-B + Math.Sqrt(d)) / (2 * A));

                    for (var i = 0; i < 2; ++i)
                    {
                        if (t[i] > epsilon)
                        {
                            // So it doesn't cast shadows on itself
                            // find intersection
                            tempNear = rayOr + t[i] * rayFw;
                            if (tempNear.y <= coneHeight && tempNear.y >= 0) // valid intersection point
                            {

                                if (t[i] < t_near)
                                {
                                    t_near = t[i];
                                    near = tempNear;
                                    if (point.IsOnSegment(in lineEnd, in near, in lineStart))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                // base
                var capCen = new Vector3(0, 0, 0);
                var capNor = new Vector3(0, -1, 0);

                Vector3 col;
                var hit = BetweenRayAndCircle(in rayFw, in rayOr, in capCen, coneRadius, in capNor, out col);
                //if(hit) Debug.DrawLine(Vector3.one*999,col, Color.magenta);
                var t_cap = distance.Between(in rayOr, in col);

                if (hit)
                {
                    if (t_cap > t_near)
                    {
                        t_near = t_cap;
                        near = tempNear;
                    }
                    else
                    {
                        t_near = t_cap;
                        near = col;
                    }
                }
                //Debug.Log("t_near "+t_near+"|"+near);
                if (t_near < float.MaxValue)
                {
                    //Debug.DrawLine(Vector3.one*999,near, Color.magenta,9999,false);
                    if (point.IsOnSegment(in lineEnd, in near, in lineStart))
                    {
                        collision = near;
                        return true;
                    }
                }

                if (BetweenPointAndCone(in lineStart, in coneBase, in coneNor, coneRadius, coneHeight) &&
                    BetweenPointAndCone(in lineEnd, in coneBase, in coneNor, coneRadius, coneHeight))
                {
                    collision = lineStart;
                    return true;
                }

                collision = Vector3.zero;
                return false;
            }

            private static bool BetweenPointAndCone(in Vector3 testPoint, in Vector3 coneBase, in Vector3 coneNor, double coneRadius, double coneHeight)
            {
                var coneTop = coneBase + coneNor * (float)coneHeight;
                Vector3 testPointProj;
                point.ProjectOnLine(in testPoint, in coneBase, in coneTop, out testPointProj);

                var ratio = (1f - (distance.Between(in testPointProj, in coneBase) / (float)coneHeight)).Clamp01();
                var currRadius = coneRadius * ratio;

                return distance.Between(in testPoint, in testPointProj) <= currRadius;
            }

            // https://searchcode.com/file/16224990/src/utils.cpp
            internal static bool BetweenRayAndCircle(
                in Vector3 rayDirection, in Vector3 rayOrigin,
                in Vector3 circleCenter, double circleRadius, in Vector3 circleNormal, out Vector3 collision)
            {
                var denom = dot(in rayDirection, in circleNormal);
                var v1 = circleCenter - rayOrigin;
                var num = dot(in v1, in circleNormal);

                const float epsilon = 0.00001f;

                if ((denom <= -epsilon || denom >= epsilon) && (num <= -epsilon || num >= epsilon))
                {
                    var t = num / denom;
                    if (t > epsilon)
                    {
                        // find intersection
                        var near = rayOrigin + t * rayDirection;
                        if ((near.x * near.x) + (near.z * near.z) <= (circleRadius * circleRadius))
                        {
                            collision = near;
                            return true;
                        }

                    }
                }
                collision = Vector3.zero;
                return false;
            }


            internal static bool BetweenLineSegmentAndDisk(
                in Vector3 lineEnd1,
                in Vector3 lineEnd2,
                in Vector3 diskNormal,
                in Vector3 diskCenter,
                double diskRadius,
                out Vector3 crossPoint)
            {
                Vector3 rayDiskIntersection;
                var rayDirection = (lineEnd1 - lineEnd2);
                // if 2 ends of a line are the same point, then check if that point lies on the disk
                if (rayDirection.sqrMagnitude < 0.00001)
                {
                    Vector3 projection;
                    point.ProjectOnPlane(in lineEnd1, in diskNormal, in diskCenter, out projection);
                    if (distanceSquared.Between(in projection, in lineEnd1) < 0.0001)
                    {
                        crossPoint = lineEnd1;
                        return true;
                    }
                    crossPoint = Vector3.zero;
                    return false;
                }
                var hasIntersection =
                    BetweenRayAndDisk(
                        in rayDirection,
                        in lineEnd2,
                        in diskNormal,
                        in diskCenter,
                        diskRadius,
                        out rayDiskIntersection);
                if (!hasIntersection)
                {
                    crossPoint = Vector3.zero;
                    return false;
                }
                // the ray is intersecting disk within that line segment
                if (point.IsOnSegment(in lineEnd1, in rayDiskIntersection, in lineEnd2))
                {
                    crossPoint = rayDiskIntersection;
                    return true;
                }
                crossPoint = Vector3.zero;
                return false;
            }

            internal static bool BetweenLineSegmentAndDisk(
                in Vector3 lineEnd1,
                in Vector3 lineEnd2,
                in Vector3 diskNormal,
                in Vector3 diskCenter,
                double diskRadius)
            {
                Vector3 rayDiskIntersection;
                var rayDirection = (lineEnd1 - lineEnd2);
                // if 2 ends of a line are the same point, then check if that point lies on the disk
                if (rayDirection.sqrMagnitude < 0.00001)
                {
                    Vector3 projection;
                    point.ProjectOnPlane(in lineEnd1, in diskNormal, in diskCenter, out projection);
                    if (distanceSquared.Between(in projection, in lineEnd1) < 0.0001)
                    {
                        return true;
                    }
                    return false;
                }
                var hasIntersection =
                    BetweenRayAndDisk(
                        in rayDirection,
                        in lineEnd2,
                        in diskNormal,
                        in diskCenter,
                        diskRadius,
                        out rayDiskIntersection);
                if (!hasIntersection)
                {
                    return false;
                }
                // the ray is intersecting disk within that line segment
                if (point.IsOnSegment(in lineEnd1, in rayDiskIntersection, in lineEnd2))
                {
                    return true;
                }
                return false;
            }

            internal static bool BetweenLineSegmentAndPlane(
                in Vector3 lineEnd1,
                in Vector3 lineEnd2,
                in Vector3 planeNormal,
                in Vector3 planePoint,
                out Vector3 crossPoint)
            {
                Vector3 rayDiskIntersection;
                var rayDirection = (lineEnd1 - lineEnd2);
                // if 2 ends of a line are the same point, then check if that point lies on the disk
                if (rayDirection.sqrMagnitude < 0.00001)
                {
                    Vector3 projection;
                    point.ProjectOnPlane(in lineEnd1, in planeNormal, in planePoint, out projection);
                    if (distanceSquared.Between(in projection, in lineEnd1) < 0.0001)
                    {
                        crossPoint = lineEnd1;
                        return true;
                    }
                    crossPoint = Vector3.zero;
                    return false;
                }
                var hasIntersection =
                    BetweenPlaneAndRay(
                        in planeNormal,
                        in planePoint,
                        in rayDirection,
                        in lineEnd2,
                        out rayDiskIntersection);
                if (!hasIntersection)
                {
                    crossPoint = Vector3.zero;
                    return false;
                }
                // the ray is intersecting disk within that line segment
                if (point.IsOnSegment(in lineEnd1, in rayDiskIntersection, in lineEnd2))
                {
                    crossPoint = rayDiskIntersection;
                    return true;
                }
                crossPoint = Vector3.zero;
                return false;
            }


            internal static bool Between2DLines(Vector2 line1p1, Vector2 line1p2, Vector2 line2p1, Vector2 line2p2)
            {
                return Between2DLines(in line1p1, in line1p2, in line2p1, in line2p2);
            }
            internal static bool Between2DLines(in Vector2 line1p1, in Vector2 line1p2, in Vector2 line2p1, in Vector2 line2p2)
            {
                // Find the four orientations needed for general and
                // special cases
                var o1 = Orientation(in line1p1, in line1p2, in line2p1);
                var o2 = Orientation(in line1p1, in line1p2, in line2p2);
                var o3 = Orientation(in line2p1, in line2p2, in line1p1);
                var o4 = Orientation(in line2p1, in line2p2, in line1p2);

                // General case
                if (o1 != o2 && o3 != o4)
                    return true;

                // Special Cases : colinear
                if (o1 == 0 && point.IsOn2DSegment(in line1p1, in line2p1, in line1p2)) return true;

                if (o2 == 0 && point.IsOn2DSegment(in line1p1, in line2p2, in line1p2)) return true;

                if (o3 == 0 && point.IsOn2DSegment(in line2p1, in line1p1, in line2p2)) return true;

                if (o4 == 0 && point.IsOn2DSegment(in line2p1, in line1p2, in line2p2)) return true;

                return false; // Doesn't fall in any of the above cases
            }
            internal static bool Between2DLines(Vector2 lineA1, Vector2 lineA2, Vector2 lineB1, Vector2 lineB2, out Vector2 intersect)
            {
                var a1 = (double)lineA2.y - (double)lineA1.y;
                var b1 = (double)lineA1.x - (double)lineA2.x;
                var c1 = a1 * (double)lineA1.x + b1 * (double)lineA1.y;

                var a2 = (double)lineB2.y - (double)lineB1.y;
                var b2 = (double)lineB1.x - (double)lineB2.x;
                var c2 = a2 * (double)lineB1.x + b2 * (double)lineB1.y;

                var det = a1 * b2 - a2 * b1;
                // if lines are parallel
                if (det < 0.00001f && det > -0.00001f)
                {
                    intersect = Vector2.zero;
                    return false;
                }
                var x = (b2 * c1 - b1 * c2) / det;
                var y = (a1 * c2 - a2 * c1) / det;
                intersect = new Vector2((float)x, (float)y);

                return
                    intersect.x >= Mathf.Min(lineA1.x, lineA2.x) &&
                    intersect.x <= Mathf.Max(lineA1.x, lineA2.x) &&
                    intersect.y >= Mathf.Min(lineA1.y, lineA2.y) &&
                    intersect.y <= Mathf.Max(lineA1.y, lineA2.y) &&
                    intersect.x >= Mathf.Min(lineB1.x, lineB2.x) &&
                    intersect.x <= Mathf.Max(lineB1.x, lineB2.x) &&
                    intersect.y >= Mathf.Min(lineB1.y, lineB2.y) &&
                    intersect.y <= Mathf.Max(lineB1.y, lineB2.y);
            }
            internal static bool Between2DLines(in Vector2 lineA1, in Vector2 lineA2, in Vector2 lineB1, in Vector2 lineB2, out Vector2 intersect)
            {
                var a1 = (double)lineA2.y - (double)lineA1.y;
                var b1 = (double)lineA1.x - (double)lineA2.x;
                var c1 = a1 * (double)lineA1.x + b1 * (double)lineA1.y;

                var a2 = (double)lineB2.y - (double)lineB1.y;
                var b2 = (double)lineB1.x - (double)lineB2.x;
                var c2 = a2 * lineB1.x + b2 * lineB1.y;
                var det = a1 * b2 - a2 * b1;
                // if lines are parallel
                if (det < 0.00001 && det > -0.00001)
                {
                    intersect = Vector2.zero;
                    return false;
                }
                var x = (b2 * c1 - b1 * c2) / det;
                var y = (a1 * c2 - a2 * c1) / det;
                intersect = new Vector2((float)x, (float)y);
                return
                    intersect.x >= Mathf.Min(lineA1.x, lineA2.x) &&
                    intersect.x <= Mathf.Max(lineA1.x, lineA2.x) &&
                    intersect.y >= Mathf.Min(lineA1.y, lineA2.y) &&
                    intersect.y <= Mathf.Max(lineA1.y, lineA2.y) &&
                    intersect.x >= Mathf.Min(lineB1.x, lineB2.x) &&
                    intersect.x <= Mathf.Max(lineB1.x, lineB2.x) &&
                    intersect.y >= Mathf.Min(lineB1.y, lineB2.y) &&
                    intersect.y <= Mathf.Max(lineB1.y, lineB2.y);
            }


            // To find orientation of ordered triplet (p, q, r).
            // The function returns following values
            // 0 --> p, q and r are colinear
            // 1 --> Clockwise
            // 2 --> Counterclockwise
            private static int Orientation(in Vector2 p, in Vector2 q, in Vector2 r)
            {
                // See http://www.geeksforgeeks.org/orientation-3-ordered-points/
                // for details of below formula.
                var val = ((double)q.y - (double)p.y) * ((double)r.x - (double)q.x) - ((double)q.x - (double)p.x) * ((double)r.y - (double)q.y);

                if (val < 0.000001 && val > -0.000001) return 0;  // colinear

                return (val > 0) ? 1 : 2; // clock or counterclock wise
            }

            internal static bool BetweenTriangleAndSphere(
                in Vector3 t1, in Vector3 t2, in Vector3 t3,
                in Vector3 sphereCenter, double sphereRadius,
                out Vector3 collision)
            {
                Vector3 triangleNormal;
                point.GetNormal(in t1, in t2, in t3, out triangleNormal);

                Vector3 proj;
                point.ProjectOnPlane(in sphereCenter, in triangleNormal, in t1, out proj);
                var x2d = (t2 - t1).normalized;
                Vector3 y2d;
                vector.GetNormal(in x2d, in triangleNormal, out y2d);

                var dist = distance.Between(in sphereCenter, in proj);
                var ratio = dist / sphereRadius;
                if (ratio <= 1.0)
                {

                    var t1in2d = Vector2.zero;
                    var t2in2d = (t2 - t1).As2d(in x2d, in y2d);
                    var t3in2d = (t3 - t1).As2d(in x2d, in y2d);
                    var spin2d = (sphereCenter - t1).As2d(in x2d, in y2d);
                    var radius2d = (float)Math.Sqrt(1.0 - ratio * ratio) * sphereRadius;
                    if (HasCircleTriangleCollision2D(in spin2d, radius2d, in t1in2d, in t2in2d, in t3in2d))
                    {
                        Vector2 int2d;
                        GetCircleTriangle2DIntersectionPoint(in spin2d, radius2d, in t1in2d, in t2in2d, in t3in2d, out int2d);
                        //for (var a = 0; a < 360; a += 5)
                        //{
                        //Debug.DrawLine(
                        //    spin2d+(Vector2)((Vector3)(Vector2.right*radius2d)).RotateAbout(Vector3.forward, a), 
                        //    spin2d+(Vector2)((Vector3)(Vector2.right*radius2d)).RotateAbout(Vector3.forward, a+5), 
                        //    Color.red,0, false);
                        //}
                        //Debug.DrawLine(t1in2d,t2in2d,Color.black,0, false);
                        //Debug.DrawLine(t2in2d,t3in2d,Color.black,0, false);
                        //Debug.DrawLine(t3in2d,t1in2d,Color.black,0, false);
                        //Debug.DrawLine(Vector2.one*100,int2d,Color.magenta,0, false);
                        int2d.As3d(in t1, in x2d, in y2d, out collision);
                        return true;
                    }
                }
                collision = Vector3.zero;
                return false;
            }

            internal static bool BetweenTriangleAndDisk(
                in Vector3 t1, in Vector3 t2, in Vector3 t3,
                in Vector3 diskNormal, in Vector3 diskCenter, float diskRadius,
                out Vector3 intersect)
            {
                Vector3 triangleNormal;
                point.GetNormal(in t1, in t2, in t3, out triangleNormal);
                diskNormal.Normalize(); // ensure disk normal is unit vector
                // they are parallel
                if (abs(dot(in triangleNormal, in diskNormal)) > 0.999999f)
                {
                    var diskCenterPlus = diskCenter + diskNormal;
                    Vector3 proj;
                    point.ProjectOnLine(in t1, in diskCenter, in diskCenterPlus, out proj);
                    // they lie on the same plane
                    if (distanceSquared.Between(in proj, in diskCenter) < 0.0001)
                    {
                        Vector3 x2d, y2d;
                        vector.ComputeRandomXYAxesForPlane(in diskNormal, out x2d, out y2d);

                        var t1in2d = Vector2.zero;
                        var t2in2d = (t2 - t1).As2d(in x2d, in y2d);
                        var t3in2d = (t3 - t1).As2d(in x2d, in y2d);
                        var dcin2d = (diskCenter - t1).As2d(in x2d, in y2d);

                        if (HasCircleTriangleCollision2D(in dcin2d, diskRadius, in t1in2d, in t2in2d, in t3in2d))
                        {
                            Vector2 int2d;
                            GetCircleTriangle2DIntersectionPoint(in dcin2d, diskRadius, in t1in2d, in t2in2d, in t3in2d, out int2d);
                            intersect = int2d.As3d(in t1, in x2d, in y2d);
                            return true;
                        }
                    }
                }
                Vector3 intPoint, intNorm;
                var linesIntersect = BetweenPlanes(in triangleNormal, in t1, in diskNormal, in diskCenter, out intPoint, out intNorm);
                if (linesIntersect)
                {
                    var distToInt = distance.Between(in intPoint, in diskCenter);
                    var ratio = distToInt / diskRadius;
                    if (ratio <= 1.0)
                    {
                        var side = (float)Math.Sqrt(1.0 - ratio * ratio) * diskRadius;
                        var xPos = diskCenter + (intPoint - diskCenter);
                        var w1 = xPos + intNorm * side;
                        var w2 = xPos + intNorm * -side;

                        var x2d = intNorm;
                        Vector3 y2d;
                        vector.GetNormal(in intNorm, in triangleNormal, out y2d);

                        var t1in2d = Vector2.zero;
                        var t2in2d = (t2 - t1).As2d(in x2d, in y2d);
                        var t3in2d = (t3 - t1).As2d(in x2d, in y2d);
                        var w1in2d = (w1 - t1).As2d(in x2d, in y2d);
                        var w2in2d = (w2 - t1).As2d(in x2d, in y2d);
                        //Debug.DrawLine(Vector3.one*100, w1in2d, Color.red, 0, false);
                        //Debug.DrawLine(Vector3.one*100, w2in2d, Color.black, 0, false);

                        Vector2 int2d;
                        if (Between2DTriangleAndLineSegment(
                            in t1in2d, in t2in2d, in t3in2d,
                            in w1in2d, in w2in2d, out int2d))
                        {
                            intersect = int2d.As3d(in t1, in x2d, in y2d);
                            return true;
                        }
                    }
                }
                intersect = Vector3.zero;
                return false;
            }
            internal static bool Between2DTriangleAndLineSegment(
                in Vector2 t1, in Vector2 t2, in Vector2 t3,
                in Vector2 line1, in Vector2 line2,
                out Vector2 intersect)
            {
                Vector2 curr;
                var c = Vector2.zero;
                var b1 = false;
                var b2 = false;
                var b3 = false;
                if (Between2DLines(in t1, in t2, in line1, in line2, out curr))
                {
                    b1 = true;
                    c = curr;
                }
                if (Between2DLines(in t2, in t3, in line1, in line2, out curr))
                {
                    b2 = true;
                    c = b1 ? point.Middle2D(c, curr) : curr;
                }
                if (Between2DLines(in t3, in t1, in line1, in line2, out curr))
                {
                    b3 = true;
                    c = b1 || b2 ? point.Middle2D(c, curr) : curr;
                }
                if (b1 || b2 || b3)
                {
                    intersect = c;
                    return true;
                }

                if (triangle.IsPointInside(in line1, in t1, in t2, in t3) ||
                    triangle.IsPointInside(in line2, in t1, in t2, in t3))
                {
                    Vector2 wcin2d;
                    point.Middle2D(in line1, in line2, out wcin2d);
                    intersect = wcin2d;
                    return true;
                }
                intersect = Vector2.zero;
                return false;
            }
            private static bool GetCircleTriangle2DIntersectionPoint(
                in Vector2 circleCenter, double radius,
                in Vector2 t1, in Vector2 t2, in Vector2 t3, out Vector2 intersect)
            {
                intersect = Vector2.zero;
                var n1 = GetLineCircleIntersectionPoint(
                        false, in circleCenter, radius, in t1, in t2, ref intersect);
                var n2 = GetLineCircleIntersectionPoint(
                        n1 > 0, in circleCenter, radius, in t2, in t3, ref intersect);
                var n3 = GetLineCircleIntersectionPoint(
                        n1 + n2 > 0, in circleCenter, radius, in t3, in t1, ref intersect);
                var hasIntersection = (n1 + n2 + n3) > 0;
                if (hasIntersection)
                {
                    return true;
                }

                // if the circle is inside the triangle
                if (distanceSquared.Between(in t1, in circleCenter) > radius * radius)
                {
                    intersect = circleCenter;
                    return false;
                }
                // if the triangle is inside the circle
                Vector2 centroid;
                triangle.GetCentroid2D(in t1, in t2, in t3, out centroid);
                intersect = centroid;
                return false;
            }
            private static int GetLineCircleIntersectionPoint(
                bool hasPrevious,
                in Vector2 circleCenter, double curcleRadius,
                in Vector2 point1, in Vector2 point2, ref Vector2 output)
            {
                Vector2 intersection1, intersection2;
                var num =
                    BetweenLineSegmentAndCircle2D(
                        in circleCenter, curcleRadius,
                        in point1, in point2,
                        out intersection1, out intersection2);
                var current = Vector2.zero;
                if (num == 1) current = intersection1;
                else if (num == 2) point.Middle2D(in intersection1, in intersection2, out current);
                if (num > 0)
                {
                    if (hasPrevious) point.Middle2D(in output, in current, out output);
                    else output = current;
                }
                return num;
            }
            internal static int BetweenLineSegmentAndCircle2D(
                in Vector2 circleCenter, double circleRadius,
                in Vector2 point1, in Vector2 point2,
                out Vector2 intersection1, out Vector2 intersection2)
            {
                // test to see if line is inside the circle
                var dsq = distanceSquared.Between(in circleCenter, in point1);
                var circleRadiusSqr = circleRadius * circleRadius;
                if (dsq < circleRadiusSqr)
                {
                    dsq = distanceSquared.Between(in circleCenter, in point2);
                    if (dsq < circleRadiusSqr)
                    {
                        intersection1 = Vector2.zero;
                        intersection2 = Vector2.zero;
                        return 0;
                    }
                }

                var dx = point2.x - point1.x;
                var dy = point2.y - point1.y;

                var a = dx * dx + dy * dy;
                var b = 2 * (dx * (point1.x - circleCenter.x) + dy * (point1.y - circleCenter.y));
                var c = (point1.x - circleCenter.x) * (point1.x - circleCenter.x) + (point1.y - circleCenter.y) * (point1.y - circleCenter.y) - circleRadius * circleRadius;

                var determinate = b * b - 4 * a * c;
                if ((a <= 0.0000001) || (determinate < -0.0000001))
                {
                    // No real solutions.
                    intersection1 = Vector2.zero;
                    intersection2 = Vector2.zero;
                    return 0;
                }
                float t;
                if (determinate < 0.0000001 && determinate > -0.0000001)
                {
                    // One solution.
                    t = -b / (2 * a);
                    intersection1 = new Vector2(point1.x + t * dx, point1.y + t * dy);
                    point.EnforceWithin(ref intersection1, in point1, in point2);
                    intersection2 = Vector2.zero;
                    return 1;
                }

                // Two solutions.
                t = (float)((-b + Math.Sqrt(determinate)) / (2 * a));
                intersection1 = new Vector2(point1.x + t * dx, point1.y + t * dy);
                point.EnforceWithin(ref intersection1, in point1, in point2);
                t = (float)((-b - Math.Sqrt(determinate)) / (2 * a));
                intersection2 = new Vector2(point1.x + t * dx, point1.y + t * dy);
                point.EnforceWithin(ref intersection2, in point1, in point2);
                return 2;
            }
            private static bool HasCircleTriangleCollision2D(in Vector2 centre, double radius, in Vector2 t1, in Vector2 t2, in Vector2 t3)
            {
                //
                // TEST 1: Vertex within circle
                //
                var c1x = centre.x - t1.x;
                var c1y = centre.y - t1.y;

                var radiusSqr = radius * radius;
                var c1sqr = c1x * c1x + c1y * c1y - radiusSqr;

                if (c1sqr <= 0) return true;

                var c2x = centre.x - t2.x;
                var c2y = centre.y - t2.y;
                var c2sqr = c2x * c2x + c2y * c2y - radiusSqr;

                if (c2sqr <= 0) return true;

                var c3x = centre.x - t3.x;
                var c3y = centre.y - t3.y;
                var c3sqr = c3x * c3x + c3y * c3y - radiusSqr;

                if (c3sqr <= 0) return true;

                //
                // TEST 2: Circle centre within triangle
                //

                //
                // Calculate edges
                //
                if (fun.triangle.IsPointInside(in centre, in t1, in t2, in t3)) return true;


                //
                // TEST 3: Circle intersects edge
                //
                var e1x = t2.x - t1.x;
                var e1y = t2.y - t1.y;

                var e2x = t3.x - t2.x;
                var e2y = t3.y - t2.y;

                var e3x = t1.x - t3.x;
                var e3y = t1.y - t3.y;

                var k = c1x * e1x + c1y * e1y;

                if (k > 0)
                {
                    var len = e1x * e1x + e1y * e1y;     // squared len

                    if (k < len)
                    {
                        if ((c1sqr * len) <= k * k)
                            return true;
                    }
                }

                // Second edge
                k = c2x * e2x + c2y * e2y;

                if (k > 0)
                {
                    var len = e2x * e2x + e2y * e2y;

                    if (k < len)
                    {
                        if ((c2sqr * len) <= k * k)
                            return true;
                    }
                }

                // Third edge
                k = c3x * e3x + c3y * e3y;

                if (k > 0)
                {
                    var len = e3x * e3x + e3y * e3y;

                    if (k < len)
                    {
                        if ((c3sqr * len) <= k * k)
                            return true;
                    }
                }
                // We're done, no intersection
                return false;
            }
            /// <summary>
            /// note the collision point returned is not precise, but is good enough for my needs
            /// </summary>
            internal static bool BetweenTriangles(
                in Vector3 t1p1, in Vector3 t1p2, in Vector3 t1p3,
                in Vector3 t2p1, in Vector3 t2p2, in Vector3 t2p3,
                out Vector3 collision)
            {
                if (triangle.Overlap(
                    in t1p1, in t1p2, in t1p3,
                    in t2p1, in t2p2, in t2p3))
                {
                    Vector3 normalT1, normalT2;
                    point.GetNormal(in t1p1, in t1p2, in t1p3, out normalT1);
                    point.GetNormal(in t2p1, in t2p2, in t2p3, out normalT2);

                    // then are on the same plane
                    if (abs(dot(in normalT1, in normalT2)) > 0.999999f)
                    {
                        Vector3 cen1, cen2;
                        triangle.GetCentroid(in t1p1, in t1p2, in t1p3, out cen1);
                        triangle.GetCentroid(in t2p1, in t2p2, in t2p3, out cen2);

                        point.Middle(in cen1, in cen2, out collision);

                        return true;
                    }

                    Vector3 intPoint, intNormal;
                    BetweenPlanes(in normalT1, in t1p1, in normalT2, in t2p1, out intPoint, out intNormal);

                    var l1 = (intPoint + intNormal * 999);
                    var l2 = (intPoint - intNormal * 999);

                    Vector3 x2d, y2d;
                    vector.ComputeRandomXYAxesForPlane(in normalT1, out x2d, out y2d);
                    var t12d = Vector2.zero;
                    var t22d = (t1p2 - t1p1).As2d(in x2d, in y2d);
                    var t32d = (t1p3 - t1p1).As2d(in x2d, in y2d);
                    var w12d = (l1 - t1p1).As2d(in x2d, in y2d);
                    var w22d = (l2 - t1p1).As2d(in x2d, in y2d);

                    Vector2 int2d;
                    Between2DTriangleAndLineSegment(in t12d, in t22d, in t32d, in w12d, in w22d, out int2d);
                    var p1 = int2d.As3d(in t1p1, in x2d, in y2d);

                    vector.ComputeRandomXYAxesForPlane(in normalT2, out x2d, out y2d);
                    t22d = (t2p2 - t2p1).As2d(in x2d, in y2d);
                    t32d = (t2p3 - t2p1).As2d(in x2d, in y2d);
                    w12d = (l1 - t2p1).As2d(in x2d, in y2d);
                    w22d = (l2 - t2p1).As2d(in x2d, in y2d);

                    Between2DTriangleAndLineSegment(in t12d, in t22d, in t32d, in w12d, in w22d, out int2d);
                    var p2 = int2d.As3d(in t2p1, in x2d, in y2d);
                    //Debug.DrawLine(Vector3.one*100, p1, Color.red, 0, false);
                    //Debug.DrawLine(Vector3.one*100, p2, Color.black, 0, false);
                    point.Middle(in p1, in p2, out collision);
                    return true;
                }

                collision = Vector3.zero;
                return false;
            }

            internal static bool BetweenCircles2D(in Vector2 p0, float radius0, in Vector2 p1, float radius1, out Vector2 cross0, out Vector2 cross1)
            {
                var d = distance.Between(in p0, in p1);
                var radiusesSum = radius0 + radius1;
                const float delta = 0.00001f;
                var radiusesDiff = abs(radius0 - radius1);
                if (d > radiusesSum || d < (radiusesDiff + delta))
                {
                    cross0 = cross1 = Vector2.zero;
                    return false;
                }
                var a = (radius0 * radius0 - radius1 * radius1 + d * d) / (2 * d);
                var h = sqrt(radius0 * radius0 - a * a);
                var p2 = ((p1 - p0) * (a / d)) + p0;
                var x3 = p2.x + h * (p1.y - p0.y) / d;
                var y3 = p2.y - h * (p1.x - p0.x) / d;
                var x4 = p2.x - h * (p1.y - p0.y) / d;
                var y4 = p2.y + h * (p1.x - p0.x) / d;

                cross0 = new Vector2(x3, y3);
                cross1 = new Vector2(x4, y4);
                return true;
            }
        }

        #endregion
        // http://wiki.unity3d.com/index.php/ProceduralPrimitives
        #region meshes
        internal static class meshes
        {
            /*private static GameObject CreateGameObject(string prefix, DtBase data, out Mesh mesh)
            {
                var gameObject = new GameObject(data.name ?? prefix + "_" + ShortGuid.New().ToString("X"));
                var renderer = gameObject.AddComponent<MeshRenderer>();
                renderer.material = GlobalFactory.Default.Get<IResourceManager>().LoadMaterial("materials/" + materials.DynamicMesh);
                //renderer.material.SetTexture("_DetailAlbedoMap", Resources.Load<Texture>(""));
                var meshFilter = gameObject.AddComponent<MeshFilter>();

                mesh = meshFilter.mesh;
                mesh.Clear();
                mesh.name = prefix;
                data.mesh = mesh;
                if (data.set != null) data.set(gameObject.transform);
                return gameObject;
            }*/
            private static GameObject CreateGameObject(string prefix, DtBase data, out Mesh mesh)
            {
                var gameObject = new GameObject(data.name ?? prefix + "_" + Guid.NewGuid().ToString("N"));
                var renderer = gameObject.AddComponent<MeshRenderer>();
                //                renderer.material = GlobalFactory.Default.Get<IResourceManager>().LoadMaterial("materials/" + materials.DynamicMesh);
                renderer.material = new Material(Shader.Find("Standard"));
                var meshFilter = gameObject.AddComponent<MeshFilter>();

                mesh = meshFilter.mesh;
                mesh.Clear();
                mesh.name = prefix;
                data.mesh = mesh;
                if (data.set != null) data.set(gameObject.transform);
                return gameObject;
            }
            #region Box
            internal static GameObject CreateBox()
            {
                return CreateBox(new DtBox());
            }
            internal static GameObject CreateBox(DtBox dt)
            {
                Mesh m;
                var gameObject = CreateGameObject("Box", dt, out m);

                var x = (float)dt.x;
                var y = (float)dt.y;
                var z = (float)dt.z;

                #region Vertices
                var p0 = new Vector3(-x * .5f, -y * .5f, z * .5f);
                var p1 = new Vector3(x * .5f, -y * .5f, z * .5f);
                var p2 = new Vector3(x * .5f, -y * .5f, -z * .5f);
                var p3 = new Vector3(-x * .5f, -y * .5f, -z * .5f);

                var p4 = new Vector3(-x * .5f, y * .5f, z * .5f);
                var p5 = new Vector3(x * .5f, y * .5f, z * .5f);
                var p6 = new Vector3(x * .5f, y * .5f, -z * .5f);
                var p7 = new Vector3(-x * .5f, y * .5f, -z * .5f);

                var vertices = new[]
                {
			        // Bottom
			        p0, p1, p2, p3,
			
			        // Left
			        p7, p4, p0, p3,
			
			        // Front
			        p4, p5, p1, p0,
			
			        // Back
			        p6, p7, p3, p2,
			
			        // Right
			        p5, p6, p2, p1,
			
			        // Top
			        p7, p6, p5, p4
                };
                #endregion

                #region normals
                var up = Vector3.up;
                var down = Vector3.down;
                var front = Vector3.forward;
                var back = Vector3.back;
                var left = Vector3.left;
                var right = Vector3.right;

                var normals = new[]
                {
			        // Bottom
			        down, down, down, down,
			
			        // Left
			        left, left, left, left,
			
			        // Front
			        front, front, front, front,
			
			        // Back
			        back, back, back, back,
			
			        // Right
			        right, right, right, right,
			
			        // Top
			        up, up, up, up
                };
                #endregion

                #region UVs
                var p00 = new Vector2(0f, 0f);
                var p10 = new Vector2(1f, 0f);
                var p01 = new Vector2(0f, 1f);
                var p11 = new Vector2(1f, 1f);

                var uvs = new[]
                {
			        // Bottom
			        p11, p01, p00, p10,
			
			        // Left
			        p11, p01, p00, p10,
			
			        // Front
			        p11, p01, p00, p10,
			
			        // Back
			        p11, p01, p00, p10,
			
			        // Right
			        p11, p01, p00, p10,
			
			        // Top
			        p11, p01, p00, p10,
                };
                #endregion

                #region Triangles
                var triangles = new[]
                {
			        // Bottom
			        3, 1, 0,
                    3, 2, 1,			
			
			        // Left
			        3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
                    3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
			
			        // Front
			        3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
                    3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
			
			        // Back
			        3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
                    3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
			
			        // Right
			        3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
                    3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
			
			        // Top
			        3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
                    3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,

                };
                #endregion

                m.vertices = vertices;
                m.normals = normals;
                m.uv = uvs;
                m.triangles = triangles;

                m.RecalculateBounds();
                ;
                return gameObject;
            }
            #endregion

            #region Triangle Plane
            internal static GameObject CreateTwoSidedTrianglePlane()
            {
                return CreateTwoSidedTrianglePlane(new DtTrianglePlane());
            }
            internal static GameObject CreateTwoSidedTrianglePlane(DtTrianglePlane dt)
            {
                Mesh m;
                var gameObject = CreateGameObject("TwoSidedTrianglePlane", dt, out m);

                var length = (float)dt.length;
                var width = (float)dt.width;

                #region Vertices		
                var vertices = new Vector3[4];
                vertices[0] = new Vector3(length * -0.5f, width * -0.5f, 0);
                vertices[1] = new Vector3(length * 0.5f, width * -0.5f, 0);
                vertices[2] = new Vector3(length * -0.5f, width * 0.5f, 0);
                #endregion

                #region Normales
                var normals = new Vector3[vertices.Length];
                normals[0] = Vector3.forward;
                normals[1] = Vector3.forward;
                normals[2] = Vector3.forward;
                #endregion

                #region UVs		
                Vector2[] uvs = new Vector2[vertices.Length];
                uvs[0] = new Vector2(0, 0);
                uvs[1] = new Vector2(1, 0);
                uvs[2] = new Vector2(0, 1);
                #endregion

                #region Triangles
                var triangles = new int[6];
                int t = 0;

                triangles[t++] = 0;
                triangles[t++] = 1;
                triangles[t++] = 2;

                triangles[t++] = 2;
                triangles[t++] = 1;
                triangles[t++] = 0;
                #endregion

                m.vertices = vertices;
                m.normals = normals;
                m.uv = uvs;
                m.triangles = triangles;

                m.RecalculateBounds();
                ;
                return gameObject;
            }
            #endregion

            #region Square Plane
            internal static GameObject CreateTwoSidedSquarePlane()
            {
                return CreateTwoSidedSquarePlane(new DtSquarePlane());
            }
            internal static GameObject CreateTwoSidedSquarePlane(DtSquarePlane dt)
            {
                Mesh m;
                var gameObject = CreateGameObject("TwoSidedSquarePlane", dt, out m);

                var length = (float)dt.length;
                var width = (float)dt.width;

                #region Vertices		
                var vertices = new Vector3[4];
                vertices[0] = new Vector3(length * -0.5f, width * -0.5f, 0);
                vertices[1] = new Vector3(length * 0.5f, width * -0.5f, 0);
                vertices[2] = new Vector3(length * -0.5f, width * 0.5f, 0);
                vertices[3] = new Vector3(length * 0.5f, width * 0.5f, 0);
                #endregion

                #region Normales
                var normals = new Vector3[vertices.Length];
                normals[0] = Vector3.forward;
                normals[1] = Vector3.forward;
                normals[2] = Vector3.forward;
                normals[3] = Vector3.forward;
                #endregion

                #region UVs		
                Vector2[] uvs = new Vector2[vertices.Length];
                uvs[0] = new Vector2(0, 0);
                uvs[1] = new Vector2(1, 0);
                uvs[2] = new Vector2(0, 1);
                uvs[3] = new Vector2(1, 1);
                #endregion

                #region Triangles
                var triangles = new int[12];
                int t = 0;

                triangles[t++] = 0;
                triangles[t++] = 1;
                triangles[t++] = 2;

                triangles[t++] = 3;
                triangles[t++] = 2;
                triangles[t++] = 1;

                triangles[t++] = 1;
                triangles[t++] = 2;
                triangles[t++] = 3;

                triangles[t++] = 2;
                triangles[t++] = 1;
                triangles[t++] = 0;
                #endregion

                m.vertices = vertices;
                m.normals = normals;
                m.uv = uvs;
                m.triangles = triangles;

                m.RecalculateBounds();
                ;
                return gameObject;
            }
            internal static GameObject CreateSquarePlane()
            {
                return CreateSquarePlane(new DtSquarePlane());
            }
            internal static GameObject CreateSquarePlane(DtSquarePlane dt)
            {
                Mesh m;
                var gameObject = CreateGameObject("SquarePlane", dt, out m);

                var length = (float)dt.length;
                var width = (float)dt.width;

                #region Vertices		
                var vertices = new Vector3[4];
                vertices[0] = new Vector3(length * -0.5f, width * -0.5f, 0);
                vertices[1] = new Vector3(length * 0.5f, width * -0.5f, 0);
                vertices[2] = new Vector3(length * -0.5f, width * 0.5f, 0);
                vertices[3] = new Vector3(length * 0.5f, width * 0.5f, 0);
                #endregion

                #region Normales
                var normals = new Vector3[vertices.Length];
                normals[0] = Vector3.forward;
                normals[1] = Vector3.forward;
                normals[2] = Vector3.forward;
                normals[3] = Vector3.forward;
                #endregion

                #region UVs		
                Vector2[] uvs = new Vector2[vertices.Length];
                uvs[0] = new Vector2(0, 0);
                uvs[1] = new Vector2(1, 0);
                uvs[2] = new Vector2(0, 1);
                uvs[3] = new Vector2(1, 1);
                #endregion

                #region Triangles
                var triangles = new int[6];
                int t = 0;

                triangles[t++] = 0;
                triangles[t++] = 1;
                triangles[t++] = 2;

                triangles[t++] = 3;
                triangles[t++] = 2;
                triangles[t++] = 1;
                #endregion

                m.vertices = vertices;
                m.normals = normals;
                m.uv = uvs;
                m.triangles = triangles;

                m.RecalculateBounds();
                ;
                return gameObject;
            }
            #endregion

            #region Cone
            internal static GameObject CreateCone()
            {
                return CreateCone(new DtCone());
            }
            internal static GameObject CreateCone(DtCone dt)
            {
                Mesh m;
                var gameObject = CreateGameObject("Cone", dt, out m);

                var height = (float)dt.height;
                var bottomRadius = (float)dt.bottomRadius;
                var topRadius = (float)dt.topRadius;
                var numSides = dt.numSides;
                var numHeightSeg = 1;

                var nbVerticesCap = numSides + 1;
                #region Vertices

                // bottom + top + sides
                Vector3[] vertices = new Vector3[nbVerticesCap + nbVerticesCap + numSides * numHeightSeg * 2 + 2];
                int vert = 0;
                float _2pi = Mathf.PI * 2f;

                // Bottom cap
                vertices[vert++] = new Vector3(0f, 0f, 0f) + dt.localPos;
                while (vert <= numSides)
                {
                    float rad = (float)vert / numSides * _2pi;
                    vertices[vert] = new Vector3(Mathf.Cos(rad) * bottomRadius, 0f, Mathf.Sin(rad) * bottomRadius) + dt.localPos;
                    vert++;
                }

                // Top cap
                vertices[vert++] = new Vector3(0f, height, 0f) + dt.localPos;
                while (vert <= numSides * 2 + 1)
                {
                    float rad = (float)(vert - numSides - 1) / numSides * _2pi;
                    vertices[vert] = new Vector3(Mathf.Cos(rad) * topRadius, height, Mathf.Sin(rad) * topRadius) + dt.localPos;
                    vert++;
                }


                // Sides
                int v = 0;
                while (vert <= vertices.Length - 4)
                {
                    float rad = (float)v / numSides * _2pi;
                    vertices[vert] = new Vector3(Mathf.Cos(rad) * topRadius, height, Mathf.Sin(rad) * topRadius) + dt.localPos;
                    vertices[vert + 1] = new Vector3(Mathf.Cos(rad) * bottomRadius, 0, Mathf.Sin(rad) * bottomRadius) + dt.localPos;
                    vert += 2;
                    v++;
                }
                vertices[vert] = vertices[numSides * 2 + 2];
                vertices[vert + 1] = vertices[numSides * 2 + 3];
                #endregion

                #region Normales

                // bottom + top + sides
                Vector3[] normales = new Vector3[vertices.Length];
                vert = 0;

                // Bottom cap
                while (vert <= numSides)
                {
                    normales[vert++] = Vector3.down;
                }

                // Top cap
                while (vert <= numSides * 2 + 1)
                {
                    normales[vert++] = Vector3.up;
                }

                // Sides
                v = 0;
                while (vert <= vertices.Length - 4)
                {
                    float rad = (float)v / numSides * _2pi;
                    float cos = Mathf.Cos(rad);
                    float sin = Mathf.Sin(rad);

                    normales[vert] = new Vector3(cos, 0f, sin);
                    normales[vert + 1] = normales[vert];

                    vert += 2;
                    v++;
                }
                normales[vert] = normales[numSides * 2 + 2];
                normales[vert + 1] = normales[numSides * 2 + 3];
                #endregion

                #region UVs
                Vector2[] uvs = new Vector2[vertices.Length];

                // Bottom cap
                int u = 0;
                uvs[u++] = new Vector2(0.5f, 0.5f);
                while (u <= numSides)
                {
                    float rad = (float)u / numSides * _2pi;
                    uvs[u] = new Vector2(Mathf.Cos(rad) * .5f + .5f, Mathf.Sin(rad) * .5f + .5f);
                    u++;
                }

                // Top cap
                uvs[u++] = new Vector2(0.5f, 0.5f);
                while (u <= numSides * 2 + 1)
                {
                    float rad = (float)u / numSides * _2pi;
                    uvs[u] = new Vector2(Mathf.Cos(rad) * .5f + .5f, Mathf.Sin(rad) * .5f + .5f);
                    u++;
                }

                // Sides
                int u_sides = 0;
                while (u <= uvs.Length - 4)
                {
                    float t = (float)u_sides / numSides;
                    uvs[u] = new Vector3(t, 1f);
                    uvs[u + 1] = new Vector3(t, 0f);
                    u += 2;
                    u_sides++;
                }
                uvs[u] = new Vector2(1f, 1f);
                uvs[u + 1] = new Vector2(1f, 0f);
                #endregion

                #region Triangles
                int nbTriangles = numSides + numSides + numSides * 2;
                int[] triangles = new int[nbTriangles * 3 + 3];

                // Bottom cap
                int tri = 0;
                int i = 0;
                while (tri < numSides - 1)
                {
                    triangles[i] = 0;
                    triangles[i + 1] = tri + 1;
                    triangles[i + 2] = tri + 2;
                    tri++;
                    i += 3;
                }
                triangles[i] = 0;
                triangles[i + 1] = tri + 1;
                triangles[i + 2] = 1;
                tri++;
                i += 3;

                // Top cap
                //tri++;
                while (tri < numSides * 2)
                {
                    triangles[i] = tri + 2;
                    triangles[i + 1] = tri + 1;
                    triangles[i + 2] = nbVerticesCap;
                    tri++;
                    i += 3;
                }

                triangles[i] = nbVerticesCap + 1;
                triangles[i + 1] = tri + 1;
                triangles[i + 2] = nbVerticesCap;
                tri++;
                i += 3;
                tri++;

                // Sides
                while (tri <= nbTriangles)
                {
                    triangles[i] = tri + 2;
                    triangles[i + 1] = tri + 1;
                    triangles[i + 2] = tri + 0;
                    tri++;
                    i += 3;

                    triangles[i] = tri + 1;
                    triangles[i + 1] = tri + 2;
                    triangles[i + 2] = tri + 0;
                    tri++;
                    i += 3;
                }
                #endregion

                m.vertices = vertices;
                m.normals = normales;
                m.uv = uvs;
                m.triangles = triangles;

                m.RecalculateBounds();
                ;
                return gameObject;
            }
            internal static GameObject CreatePointyCone()
            {
                return CreateCone(new DtCone());
            }
            internal static GameObject CreatePointyCone(DtCone dt)
            {
                Mesh m;
                var gameObject = CreateGameObject("PointyCone", dt, out m);

                var height = (float)dt.height;
                var bottomRadius = (float)dt.bottomRadius;
                var topRadius = (float)dt.topRadius;
                var nbSides = dt.numSides;
                var noseLen = (float)dt.relNoseLen;
                var nbHeightSeg = 1;

                int nbVerticesCap = nbSides + 1;
                #region Vertices

                // bottom + top + sides
                Vector3[] vertices = new Vector3[nbVerticesCap + nbVerticesCap + nbSides * nbHeightSeg * 2 + 2];
                int vert = 0;
                float _2pi = Mathf.PI * 2f;

                // Bottom cap
                vertices[vert++] = new Vector3(0f, 0f, 0f);
                while (vert <= nbSides)
                {
                    float rad = (float)vert / nbSides * _2pi;
                    var isXtop = Mathf.Sin(rad) > 0.95f;
                    var nose = isXtop ? noseLen * bottomRadius : 0;
                    var noseShift = isXtop ? 0 : 1;
                    vertices[vert] = new Vector3(Mathf.Cos(rad) * bottomRadius * noseShift, 0f, Mathf.Sin(rad) * bottomRadius + nose);
                    vert++;
                }

                // Top cap
                vertices[vert++] = new Vector3(0f, height, 0f);
                while (vert <= nbSides * 2 + 1)
                {
                    float rad = (float)(vert - nbSides - 1) / nbSides * _2pi;

                    vertices[vert] = new Vector3(Mathf.Cos(rad) * topRadius, height, Mathf.Sin(rad) * topRadius);
                    vert++;
                }


                // Sides
                int v = 0;
                while (vert <= vertices.Length - 4)
                {
                    float rad = (float)v / nbSides * _2pi;

                    var isXtop = Mathf.Sin(rad) > 0.95f;
                    var nose = isXtop ? noseLen * bottomRadius : 0;
                    var noseShift = isXtop ? 0 : 1;
                    vertices[vert] = new Vector3(Mathf.Cos(rad) * topRadius, height, Mathf.Sin(rad) * topRadius);
                    vertices[vert + 1] = new Vector3(Mathf.Cos(rad) * bottomRadius * noseShift, 0, Mathf.Sin(rad) * bottomRadius + nose);
                    vert += 2;
                    v++;
                }
                vertices[vert] = vertices[nbSides * 2 + 2];
                vertices[vert + 1] = vertices[nbSides * 2 + 3];
                #endregion

                #region Normales

                // bottom + top + sides
                Vector3[] normales = new Vector3[vertices.Length];
                vert = 0;

                // Bottom cap
                while (vert <= nbSides)
                {
                    normales[vert++] = Vector3.down;
                }

                // Top cap
                while (vert <= nbSides * 2 + 1)
                {
                    normales[vert++] = Vector3.up;
                }

                // Sides
                v = 0;
                while (vert <= vertices.Length - 4)
                {
                    float rad = (float)v / nbSides * _2pi;
                    float cos = Mathf.Cos(rad);
                    float sin = Mathf.Sin(rad);

                    normales[vert] = new Vector3(cos, 0f, sin);
                    normales[vert + 1] = normales[vert];

                    vert += 2;
                    v++;
                }
                normales[vert] = normales[nbSides * 2 + 2];
                normales[vert + 1] = normales[nbSides * 2 + 3];
                #endregion

                #region UVs
                Vector2[] uvs = new Vector2[vertices.Length];

                // Bottom cap
                int u = 0;
                uvs[u++] = new Vector2(0.5f, 0.5f);
                while (u <= nbSides)
                {
                    float rad = (float)u / nbSides * _2pi;
                    uvs[u] = new Vector2(Mathf.Cos(rad) * .5f + .5f, Mathf.Sin(rad) * .5f + .5f);
                    u++;
                }

                // Top cap
                uvs[u++] = new Vector2(0.5f, 0.5f);
                while (u <= nbSides * 2 + 1)
                {
                    float rad = (float)u / nbSides * _2pi;
                    uvs[u] = new Vector2(Mathf.Cos(rad) * .5f + .5f, Mathf.Sin(rad) * .5f + .5f);
                    u++;
                }

                // Sides
                int u_sides = 0;
                while (u <= uvs.Length - 4)
                {
                    float t = (float)u_sides / nbSides;
                    uvs[u] = new Vector3(t, 1f);
                    uvs[u + 1] = new Vector3(t, 0f);
                    u += 2;
                    u_sides++;
                }
                uvs[u] = new Vector2(1f, 1f);
                uvs[u + 1] = new Vector2(1f, 0f);
                #endregion

                #region Triangles
                int nbTriangles = nbSides + nbSides + nbSides * 2;
                int[] triangles = new int[nbTriangles * 3 + 3];

                // Bottom cap
                int tri = 0;
                int i = 0;
                while (tri < nbSides - 1)
                {
                    triangles[i] = 0;
                    triangles[i + 1] = tri + 1;
                    triangles[i + 2] = tri + 2;
                    tri++;
                    i += 3;
                }
                triangles[i] = 0;
                triangles[i + 1] = tri + 1;
                triangles[i + 2] = 1;
                tri++;
                i += 3;

                // Top cap
                //tri++;
                while (tri < nbSides * 2)
                {
                    triangles[i] = tri + 2;
                    triangles[i + 1] = tri + 1;
                    triangles[i + 2] = nbVerticesCap;
                    tri++;
                    i += 3;
                }

                triangles[i] = nbVerticesCap + 1;
                triangles[i + 1] = tri + 1;
                triangles[i + 2] = nbVerticesCap;
                tri++;
                i += 3;
                tri++;

                // Sides
                while (tri <= nbTriangles)
                {
                    triangles[i] = tri + 2;
                    triangles[i + 1] = tri + 1;
                    triangles[i + 2] = tri + 0;
                    tri++;
                    i += 3;

                    triangles[i] = tri + 1;
                    triangles[i + 1] = tri + 2;
                    triangles[i + 2] = tri + 0;
                    tri++;
                    i += 3;
                }
                #endregion

                m.vertices = vertices;
                m.normals = normales;
                m.uv = uvs;
                m.triangles = triangles;

                m.RecalculateBounds();
                ;
                return gameObject;
            }
            #endregion

            #region Sphere
            internal static GameObject CreateSphere()
            {
                return CreateSphere(new DtSphere());
            }

            internal static GameObject CreateSphere(DtSphere dt)
            {
                Mesh m;
                var gameObject = CreateGameObject("Sphere", dt, out m);

                var radius = (float)dt.radius;
                // Longitude |||
                int nbLong = dt.longitude;
                // Latitude ---
                int nbLat = dt.latitude;

                #region Vertices
                Vector3[] vertices = new Vector3[(nbLong + 1) * nbLat + 2];
                float _pi = Mathf.PI;
                float _2pi = _pi * 2f;

                vertices[0] = Vector3.up * radius;
                for (int lat = 0; lat < nbLat; lat++)
                {
                    float a1 = _pi * (float)(lat + 1) / (nbLat + 1);
                    float sin1 = Mathf.Sin(a1);
                    float cos1 = Mathf.Cos(a1);

                    for (int lon = 0; lon <= nbLong; lon++)
                    {
                        float a2 = _2pi * (float)(lon == nbLong ? 0 : lon) / nbLong;
                        float sin2 = Mathf.Sin(a2);
                        float cos2 = Mathf.Cos(a2);

                        vertices[lon + lat * (nbLong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * radius;
                    }
                }
                vertices[vertices.Length - 1] = Vector3.up * -radius;
                #endregion

                #region Normales		
                Vector3[] normales = new Vector3[vertices.Length];
                for (int n = 0; n < vertices.Length; n++)
                    normales[n] = vertices[n].normalized;
                #endregion

                #region UVs
                Vector2[] uvs = new Vector2[vertices.Length];
                uvs[0] = Vector2.up;
                uvs[uvs.Length - 1] = Vector2.zero;
                for (int lat = 0; lat < nbLat; lat++)
                    for (int lon = 0; lon <= nbLong; lon++)
                        uvs[lon + lat * (nbLong + 1) + 1] = new Vector2((float)lon / nbLong, 1f - (float)(lat + 1) / (nbLat + 1));
                #endregion

                #region Triangles
                int nbFaces = vertices.Length;
                int nbTriangles = nbFaces * 2;
                int nbIndexes = nbTriangles * 3;
                int[] triangles = new int[nbIndexes];

                //Top Cap
                int i = 0;
                for (int lon = 0; lon < nbLong; lon++)
                {
                    triangles[i++] = lon + 2;
                    triangles[i++] = lon + 1;
                    triangles[i++] = 0;
                }

                //Middle
                for (int lat = 0; lat < nbLat - 1; lat++)
                {
                    for (int lon = 0; lon < nbLong; lon++)
                    {
                        int current = lon + lat * (nbLong + 1) + 1;
                        int next = current + nbLong + 1;

                        triangles[i++] = current;
                        triangles[i++] = current + 1;
                        triangles[i++] = next + 1;

                        triangles[i++] = current;
                        triangles[i++] = next + 1;
                        triangles[i++] = next;
                    }
                }

                //Bottom Cap
                for (int lon = 0; lon < nbLong; lon++)
                {
                    triangles[i++] = vertices.Length - 1;
                    triangles[i++] = vertices.Length - (lon + 2) - 1;
                    triangles[i++] = vertices.Length - (lon + 1) - 1;
                }
                #endregion

                m.vertices = vertices;
                m.normals = normales;
                m.uv = uvs;
                m.triangles = triangles;

                m.RecalculateBounds();
                ;

                return gameObject;
            }

            #endregion

            #region Capsule
            internal static GameObject CreateCapsule()
            {
                return CreateCapsule(new DtCapsule());
            }

            internal static GameObject CreateCapsule(DtCapsule dt)
            {
                Mesh m;
                var gameObject = CreateGameObject("Capsule", dt, out m);

                var height = (float)dt.height;
                var radius = (float)dt.radius;
                // Longitude |||
                int nbLong = dt.longitude;
                // Latitude ---
                int nbLat = dt.latitude;


                #region Vertices
                Vector3[] vertices = new Vector3[(nbLong + 1) * nbLat + 2];
                float _pi = Mathf.PI;
                float _2pi = _pi * 2f;

                var upperShift = Vector3.up * (height / 2f);

                vertices[0] = Vector3.up * radius + upperShift + dt.localPos;
                var halfLat = nbLat / 2;
                for (int lat = 0; lat < halfLat; lat++)
                {
                    float a1 = _pi * (float)(lat + 1) / (nbLat + 1);
                    float sin1 = Mathf.Sin(a1);
                    float cos1 = Mathf.Cos(a1);

                    for (int lon = 0; lon <= nbLong; lon++)
                    {
                        float a2 = _2pi * (float)(lon == nbLong ? 0 : lon) / nbLong;
                        float sin2 = Mathf.Sin(a2);
                        float cos2 = Mathf.Cos(a2);
                        vertices[lon + lat * (nbLong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * radius + upperShift + dt.localPos;
                    }
                }

                var lowerShift = Vector3.up * (-height / 2f);

                for (int lat = halfLat; lat < nbLat; lat++)
                {
                    float a1 = _pi * (float)(lat + 1) / (nbLat + 1);
                    float sin1 = Mathf.Sin(a1);
                    float cos1 = Mathf.Cos(a1);

                    for (int lon = 0; lon <= nbLong; lon++)
                    {
                        float a2 = _2pi * (float)(lon == nbLong ? 0 : lon) / nbLong;
                        float sin2 = Mathf.Sin(a2);
                        float cos2 = Mathf.Cos(a2);
                        vertices[lon + lat * (nbLong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * radius + lowerShift + dt.localPos;
                    }
                }
                vertices[vertices.Length - 1] = Vector3.up * -radius + lowerShift + dt.localPos;
                #endregion

                #region Normales		
                Vector3[] normales = new Vector3[vertices.Length];
                for (int n = 0; n < vertices.Length / 2; n++)
                {
                    normales[n] = ((vertices[n] - upperShift).normalized - dt.localPos).normalized;
                }
                for (int n = vertices.Length / 2; n < vertices.Length; n++)
                {
                    normales[n] = ((vertices[n] - lowerShift).normalized - dt.localPos).normalized;
                }
                #endregion

                #region UVs
                Vector2[] uvs = new Vector2[vertices.Length];
                uvs[0] = Vector2.up;
                uvs[uvs.Length - 1] = Vector2.zero;
                for (int lat = 0; lat < nbLat; lat++)
                    for (int lon = 0; lon <= nbLong; lon++)
                        uvs[lon + lat * (nbLong + 1) + 1] = new Vector2((float)lon / nbLong, 1f - (float)(lat + 1) / (nbLat + 1));
                #endregion

                #region Triangles
                int nbFaces = vertices.Length;
                int nbTriangles = nbFaces * 2;
                int nbIndexes = nbTriangles * 3;
                int[] triangles = new int[nbIndexes];

                //Top Cap
                int i = 0;
                for (int lon = 0; lon < nbLong; lon++)
                {
                    triangles[i++] = lon + 2;
                    triangles[i++] = lon + 1;
                    triangles[i++] = 0;
                }

                //Middle
                for (int lat = 0; lat < nbLat - 1; lat++)
                {
                    for (int lon = 0; lon < nbLong; lon++)
                    {
                        int current = lon + lat * (nbLong + 1) + 1;
                        int next = current + nbLong + 1;

                        triangles[i++] = current;
                        triangles[i++] = current + 1;
                        triangles[i++] = next + 1;

                        triangles[i++] = current;
                        triangles[i++] = next + 1;
                        triangles[i++] = next;
                    }
                }

                //Bottom Cap
                for (int lon = 0; lon < nbLong; lon++)
                {
                    triangles[i++] = vertices.Length - 1;
                    triangles[i++] = vertices.Length - (lon + 2) - 1;
                    triangles[i++] = vertices.Length - (lon + 1) - 1;
                }
                #endregion



                m.vertices = vertices;
                m.normals = normales;
                m.uv = uvs;
                m.triangles = triangles;

                m.RecalculateBounds();
                ;
                return gameObject;
            }
            #endregion

        }

        #endregion
        #region point
        internal static class point
        {
            const float epsilon = 0.0000001f;

            internal static bool IsWithinAxisAlignedBoundingBox(Vector3 point, Vector3 bbCenter, Vector3 bbSize)
            {
                var hasOverlapOnX = Math.Abs(point.x - bbCenter.x) <= (bbSize.x / 2f);
                var hasOverlapOnY = Math.Abs(point.y - bbCenter.y) <= (bbSize.y / 2f);
                var hasOverlapOnZ = Math.Abs(point.z - bbCenter.z) <= (bbSize.z / 2f);
                return hasOverlapOnX && hasOverlapOnY && hasOverlapOnZ;
            }

            internal static bool IsWithinAxisAlignedBoundingBox(in Vector3 point, in Vector3 bbCenter, in Vector3 bbSize)
            {
                var hasOverlapOnX = Math.Abs(point.x - bbCenter.x) <= (bbSize.x / 2f);
                var hasOverlapOnY = Math.Abs(point.y - bbCenter.y) <= (bbSize.y / 2f);
                var hasOverlapOnZ = Math.Abs(point.z - bbCenter.z) <= (bbSize.z / 2f);
                return hasOverlapOnX && hasOverlapOnY && hasOverlapOnZ;
            }

            internal static bool IsWithinSphere(Vector3 point, Vector3 sphereCenter, double sphereRadius)
                => IsWithinSphere(in point, in sphereCenter, sphereRadius);

            internal static bool IsWithinSphere(in Vector3 point, in Vector3 sphereCenter, double sphereRadius)
                => distance.Between(in point, in sphereCenter) <= sphereRadius;

            internal static bool IsWithinCapsule(Vector3 point, Vector3 cpu, Vector3 cpd, double capsuleRadius)
                => IsWithinCapsule(in point, in cpu, in cpd, capsuleRadius);
            internal static bool IsWithinCapsule(
                in Vector3 point, in Vector3 cpu, in Vector3 cpd, double capsuleRadius)
            {
                Vector3 closest;
                ProjectOnLineSegmentOrGetClosest(in point, in cpu, in cpd, out closest);

                return distance.Between(in point, in closest) <= capsuleRadius;
            }
            internal static float DistanceToLine(in Vector3 point, in Vector3 line1, in Vector3 line2)
            {
                Vector3 proj;
                ProjectOnLine(in point, in line1, in line2, out proj);
                return distance.Between(in point, in proj);
            }
            internal static void GetClosestBetweenLineAndDisk(
                in Vector3 diskNormal, in Vector3 diskCenter, double diskRadius,
                in Vector3 line1, in Vector3 line2, out Vector3 closestOnDisk, out Vector3 closestOnLine)
            {
                ProjectOnLine(in diskCenter, in line1, in line2, out closestOnLine);
                Vector3 diskCenOnLineProj;
                ProjectOnPlane(in closestOnLine, in diskNormal, in diskCenter, out diskCenOnLineProj);
                var distSqrToCen = distanceSquared.Between(in diskCenOnLineProj, in diskCenter);
                if (distSqrToCen < (diskRadius * diskRadius))
                {
                    closestOnDisk = diskCenOnLineProj;
                    return;
                }
                closestOnDisk = diskCenter.MoveTowards(in diskCenOnLineProj, diskRadius);
            }

            internal static void ToLocal(in Vector3 worldPoint, in Quaternion worldRotation, in Vector3 worldPosition, out Vector3 localPoint)
            {
                localPoint = Quaternion.Inverse(worldRotation) * (worldPoint - worldPosition);
            }
            internal static Vector3 ToLocal(in Vector3 worldPoint, in Quaternion worldRotation, in Vector3 worldPosition)
            {
                return Quaternion.Inverse(worldRotation) * (worldPoint - worldPosition);
            }
            internal static Vector3 ToLocal(Vector3 worldPoint, Quaternion worldRotation, Vector3 worldPosition)
            {
                return Quaternion.Inverse(worldRotation) * (worldPoint - worldPosition);
            }


            internal static void ToWorld(in Vector3 localPoint, in Quaternion worldRotation, in Vector3 worldPosition, out Vector3 worldPoint)
            {
                worldPoint = worldRotation * localPoint + worldPosition;
            }
            internal static Vector3 ToWorld(in Vector3 localPoint, in Quaternion worldRotation, in Vector3 worldPosition)
            {
                return worldRotation * localPoint + worldPosition;
            }
            internal static Vector3 ToWorld(Vector3 localPoint, Quaternion worldRotation, Vector3 worldPosition)
            {
                return worldRotation * localPoint + worldPosition;
            }

            internal static bool IsOn2DSegment(in Vector2 segStart, in Vector2 point, in Vector2 segEnd)
            {
                if (point.x <= max(segStart.x, segEnd.x) + epsilon &&
                    point.x >= min(segStart.x, segEnd.x) - epsilon &&
                    point.y <= max(segStart.y, segEnd.y) + epsilon &&
                    point.y >= min(segStart.y, segEnd.y) - epsilon)
                    return true;

                return false;
            }

            internal static bool IsOnSegment(in Vector3 segStart, in Vector3 point, in Vector3 segEnd)
            {
                if (point.x <= max(segStart.x, segEnd.x) + epsilon &&
                    point.x >= min(segStart.x, segEnd.x) - epsilon &&
                    point.y <= max(segStart.y, segEnd.y) + epsilon &&
                    point.y >= min(segStart.y, segEnd.y) - epsilon &&
                    point.z <= max(segStart.z, segEnd.z) + epsilon &&
                    point.z >= min(segStart.z, segEnd.z) - epsilon)
                    return true;

                return false;
            }

            internal static bool ClosestOnLineSegment(in Vector3 p, in Vector3 line1, in Vector3 line2, out Vector3 closest)
            {
                point.ProjectOnLine(in p, in line1, in line2, out closest);
                if (!IsOnSegment(in line1, in closest, in line2))
                {
                    var d1 = distanceSquared.Between(in line1, in closest);
                    var d2 = distanceSquared.Between(in line2, in closest);
                    closest = d1 < d2 ? line1 : line2;
                    return false;
                }
                return true;
            }
            internal static void ClosestOnTwoLineSegments(
                in Vector3 line1p1, in Vector3 line1p2,
                in Vector3 line2p1, in Vector3 line2p2,
                out Vector3 closestPointLine1, out Vector3 closestPointLine2)
            {
                var dir1 = (line1p2 - line1p1).normalized;
                var dir2 = (line2p2 - line2p1).normalized;

                ClosestOnTwoLinesByPointAndDirection(
                        in line1p1, in dir1, in line2p1, in dir2,
                        out closestPointLine1, out closestPointLine2);

                var isClosest1Within = IsOnSegment(in line1p1, in closestPointLine1, in line1p2);
                var isClosest2Within = IsOnSegment(in line2p1, in closestPointLine2, in line2p2);

                // if the closest points are within finish here
                if (isClosest1Within && isClosest2Within)
                {
                    return;
                }

                // project each end
                Vector3 line1p1Proj;
                ProjectOnLineSegmentOrGetClosest(in line1p1, in line2p1, in line2p2, out line1p1Proj);
                var line1p1DistSqr = distanceSquared.Between(in line1p1, in line1p1Proj);
                var smallestDistSqr = line1p1DistSqr;
                byte smallestIndex = 0;

                Vector3 line1p2Proj;
                ProjectOnLineSegmentOrGetClosest(in line1p2, in line2p1, in line2p2, out line1p2Proj);
                var line1p2DistSqr = distanceSquared.Between(in line1p2, in line1p2Proj);
                if (line1p2DistSqr < smallestDistSqr)
                {
                    smallestDistSqr = line1p2DistSqr;
                    smallestIndex = 1;
                }

                Vector3 line2p1Proj;
                ProjectOnLineSegmentOrGetClosest(in line2p1, in line1p1, in line1p2, out line2p1Proj);
                var line2p1DistSqr = distanceSquared.Between(in line2p1, in line2p1Proj);
                if (line2p1DistSqr < smallestDistSqr)
                {
                    smallestDistSqr = line2p1DistSqr;
                    smallestIndex = 2;
                }

                Vector3 line2p2Proj;
                ProjectOnLineSegmentOrGetClosest(in line2p2, in line1p1, in line1p2, out line2p2Proj);
                var line2p2DistSqr = distanceSquared.Between(in line2p2, in line2p2Proj);
                if (line2p2DistSqr < smallestDistSqr)
                {
                    //                    smallestDistSqr = line2p2DistSqr;
                    smallestIndex = 3;
                }

                switch (smallestIndex)
                {
                    case 0:
                        closestPointLine1 = line1p1;
                        closestPointLine2 = line1p1Proj;
                        break;
                    case 1:
                        closestPointLine1 = line1p2;
                        closestPointLine2 = line1p2Proj;
                        break;
                    case 2:
                        closestPointLine1 = line2p1Proj;
                        closestPointLine2 = line2p1;
                        break;
                    case 3:
                        closestPointLine1 = line2p2Proj;
                        closestPointLine2 = line2p2;
                        break;
                    default:
                        throw new ArgumentException("Invalid Index");
                }
            }
            // if the lines are parallel then any point is closest so we return false
            internal static bool ClosestOnTwoLinesByPointAndDirection(
                in Vector3 line1p1, in Vector3 line1Direction, in Vector3 line2p1, in Vector3 line2Direction,
                out Vector3 closestPointLine1, out Vector3 closestPointLine2)
            {

                var a = dot(in line1Direction, in line1Direction);
                var b = dot(in line1Direction, in line2Direction);
                var e = dot(in line2Direction, in line2Direction);

                var d = a * e - b * b;
                //lines are not parallel
                if (d > 0.00001 || d < -0.00001)
                {

                    var r = line1p1 - line2p1;
                    var c = dot(in line1Direction, in r);
                    var f = dot(in line2Direction, in r);

                    var s = (b * f - c * e) / d;
                    var t = (a * f - c * b) / d;

                    closestPointLine1 = line1p1 + line1Direction * s;
                    closestPointLine2 = line2p1 + line2Direction * t;
                    return true;
                }

                closestPointLine1 = line1p1;
                var line2p2 = line2p1 + line2Direction;
                point.ProjectOnLine(in line1p1, in line2p1, in line2p2, out closestPointLine2);
                return false;
            }

            internal static bool ClosestOnTwoLinesByPoints(
                in Vector3 line1p1, in Vector3 line1p2, in Vector3 line2p1, in Vector3 line2p2,
                out Vector3 closestPointLine1, out Vector3 closestPointLine2)
            {
                var line1Direction = (line1p2 - line1p1).normalized;
                var line2Direction = (line2p2 - line2p1).normalized;

                return ClosestOnTwoLinesByPointAndDirection(
                    in line1p1, in line1Direction,
                    in line2p1, in line2Direction,
                    out closestPointLine1, out closestPointLine2);
            }



            internal static bool IsLeftOfLine2D(in Vector2 point, in Vector2 linePoint1, in Vector2 linePoint2)
            {
                return ((linePoint2.x - linePoint1.x) * (point.y - linePoint1.y) - (linePoint2.y - linePoint1.y) * (point.x - linePoint1.x)) > 0;
            }
            internal static bool IsLeftOfLine2D(Vector2 point, Vector2 linePoint1, Vector2 linePoint2)
            {
                return ((linePoint2.x - linePoint1.x) * (point.y - linePoint1.y) - (linePoint2.y - linePoint1.y) * (point.x - linePoint1.x)) > 0;
            }
            internal static bool IsAbovePlane(in Vector3 point, in Vector3 planeNormal, in Vector3 planePoint)
            {
                var vectorToPlane = (point - planePoint).normalized;
                var distance = -dot(in vectorToPlane, in planeNormal);
                return distance < 0;
            }
            internal static bool IsAbovePlane(Vector3 point, Vector3 planeNormal, Vector3 planePoint)
            {
                var vectorToPlane = (point - planePoint).normalized;
                var distance = -dot(in vectorToPlane, in planeNormal);
                return distance < 0;
            }
            internal static bool IsAbovePlane(in Vector3 point, in Vector3 planeNormal)
            {
                return IsAbovePlane(in point, in planeNormal, in v3.zero);
            }
            internal static bool IsAbovePlane(Vector3 point, Vector3 planeNormal)
            {
                return IsAbovePlane(in point, in planeNormal, in v3.zero);
            }


            internal static bool IsBelowPlane(Vector3 point, Vector3 planeNormal, Vector3 planePoint)
            {
                var vectorToPlane = (point - planePoint).normalized;
                var distance = -dot(in vectorToPlane, in planeNormal);
                return distance > 0;
            }
            internal static bool IsBelowPlane(in Vector3 point, in Vector3 planeNormal, in Vector3 planePoint)
            {
                var vectorToPlane = (point - planePoint).normalized;
                var distance = -dot(in vectorToPlane, in planeNormal);
                return distance > 0;
            }
            internal static bool IsBelowPlane(in Vector3 point, in Vector3 planeNormal)
            {
                return IsBelowPlane(in point, in planeNormal, in v3.zero);
            }
            internal static bool IsBelowPlane(Vector3 point, Vector3 planeNormal)
            {
                return IsBelowPlane(in point, in planeNormal, in v3.zero);
            }


            internal static bool AreOnSameSidesOfPlane(in Vector3 point1, in Vector3 point2, in Vector3 planeNormal, in Vector3 planePoint)
            {
                return IsAbovePlane(in point1, in planeNormal, in planePoint) == IsAbovePlane(in point2, in planeNormal, in planePoint);
            }

            internal static float DistanceToPlane(in Vector3 point, in Vector3 planeNormal, in Vector3 planePoint)
            {
                var vectorToPlane = point - planePoint;
                var distance = dot(in planeNormal, in vectorToPlane);
                return abs(distance);
            }
            internal static float DistanceToPlane(Vector3 point, Vector3 planeNormal, Vector3 planePoint)
            {
                var vectorToPlane = point - planePoint;
                var distance = dot(in planeNormal, in vectorToPlane);
                return abs(distance);
            }

            internal static bool IsFirstCloser(in Vector3 first, in Vector3 second, in Vector3 reference)
            {
                var d1 = distanceSquared.Between(in first, in reference);
                var d2 = distanceSquared.Between(in second, in reference);
                return d1 < d2;
            }
            internal static bool IsFirstCloser(Vector3 first, Vector3 second, Vector3 reference)
            {
                var d1 = distanceSquared.Between(in first, in reference);
                var d2 = distanceSquared.Between(in second, in reference);
                return d1 < d2;
            }
            internal static bool IsFirstCloser2d(in Vector2 first, in Vector2 second, in Vector2 reference)
            {
                var d1 = distanceSquared.Between(in first, in reference);
                var d2 = distanceSquared.Between(in second, in reference);
                return d1 < d2;
            }
            internal static bool IsFirstCloser2d(Vector2 first, Vector2 second, Vector2 reference)
            {
                var d1 = distanceSquared.Between(in first, in reference);
                var d2 = distanceSquared.Between(in second, in reference);
                return d1 < d2;
            }

            internal static Vector3 ProjectOnPlane(in Vector3 point, in Vector3 planeNormal, in Vector3 planePoint)
            {
                var vectorToPlane = point - planePoint;
                var distance = -dot(in planeNormal, in vectorToPlane);
                return point + planeNormal * distance;
            }
            internal static void ProjectOnPlane(in Vector3 point, in Vector3 planeNormal, in Vector3 planePoint, out Vector3 projection)
            {
                var norm = planeNormal.normalized;
                var vectorToPlane = point - planePoint;
                var distance = -dot(in norm, in vectorToPlane);
                projection = point + norm * distance;
            }
            internal static Vector3 ProjectOnPlane(Vector3 point, Vector3 planeNormal, Vector3 planePoint)
            {
                planeNormal = planeNormal.normalized;
                var vectorToPlane = point - planePoint;
                var distance = -dot(in planeNormal, in vectorToPlane);
                return point + planeNormal * distance;
            }
            internal static Vector3 ProjectOnLine(Vector3 point, Vector3 line1, Vector3 line2)
            {
                var pointToLine = point - line1;
                var lineVector = line2 - line1;
                Vector3 onNormal;
                vector.ProjectOnNormal(in pointToLine, in lineVector, out onNormal);
                return onNormal + line1;
            }
            internal static Vector3 ProjectOnLine(in Vector3 point, in Vector3 line1, in Vector3 line2)
            {
                var pointToLine = point - line1;
                var lineVector = line2 - line1;
                Vector3 onNormal;
                vector.ProjectOnNormal(in pointToLine, in lineVector, out onNormal);
                return onNormal + line1;
            }
            internal static void ProjectOnLine(in Vector3 point, in Vector3 line1, in Vector3 line2, out Vector3 projection)
            {
                var pointToLine = point - line1;
                var lineVector = line2 - line1;
                Vector3 onNormal;
                vector.ProjectOnNormal(in pointToLine, in lineVector, out onNormal);
                projection = onNormal + line1;
            }
            internal static bool TryProjectOnLineSegment(in Vector3 point, in Vector3 line1, in Vector3 line2, out Vector3 projection)
            {
                var pointToLine = point - line1;
                var lineVector = line2 - line1;
                Vector3 onNormal;
                vector.ProjectOnNormal(in pointToLine, in lineVector, out onNormal);
                projection = onNormal + line1;
                if (!IsOnSegment(in line1, in projection, in line2))
                {
                    projection = Vector3.zero;
                    return false;
                }
                return true;
            }

            internal static void ProjectOnLineSegmentOrGetClosest(in Vector3 point, in Vector3 line1, in Vector3 line2, out Vector3 projection)
            {
                var pointToLine = point - line1;
                var lineVector = line2 - line1;
                Vector3 onNormal;
                vector.ProjectOnNormal(in pointToLine, in lineVector, out onNormal);
                projection = onNormal + line1;
                if (!IsOnSegment(in line1, in projection, in line2))
                {
                    var d1 = distanceSquared.Between(in line1, in projection);
                    var d2 = distanceSquared.Between(in line2, in projection);
                    projection = d1 < d2 ? line1 : line2;
                }
            }
            internal static void ProjectOnLine2D(in Vector2 point, in Vector2 linePoint1, in Vector2 linePoint2, out Vector2 result)
            {
                var line = linePoint1 - linePoint2;
                var newPoint = point - linePoint2;

                result = ((dot2D(in newPoint, in line) / dot2D(in line, in line)) * line) + linePoint2;
            }
            internal static Vector3 ReflectOfPlane(Vector3 point, Vector3 planeNormal, Vector3 planePoint)
            {
                Vector3 projection;
                ProjectOnPlane(in point, in planeNormal, in planePoint, out projection);
                var distance = fun.distance.Between(in point, in projection);
                Vector3 mirrorPoint;
                fun.point.TryMoveAbs(in point, in projection, distance * 2, out mirrorPoint);
                return mirrorPoint;
            }
            internal static Vector3 ReflectOfPlane(in Vector3 point, in Vector3 planeNormal, in Vector3 planePoint)
            {
                Vector3 projection;
                ProjectOnPlane(in point, in planeNormal, in planePoint, out projection);
                var distance = fun.distance.Between(in point, in projection);
                Vector3 mirrorPoint;
                fun.point.TryMoveAbs(in point, in projection, distance * 2, out mirrorPoint);
                return mirrorPoint;
            }
            internal static void ReflectOfPlane(in Vector3 point, in Vector3 planeNormal, in Vector3 planePoint, out Vector3 mirrorPoint)
            {
                Vector3 projection;
                ProjectOnPlane(in point, in planeNormal, in planePoint, out projection);
                var distance = fun.distance.Between(in point, in projection);
                fun.point.TryMoveAbs(in point, in projection, distance * 2, out mirrorPoint);
            }

            /// <summary>
            /// if you are looking from above the normal: counter clockwise
            /// </summary>
            internal static Vector3 GetNormal(Vector3 p1, Vector3 p2, Vector3 p3)
            {
                var lhs = p1 - p2;
                var rhs = p3 - p2;
                Vector3 r;
                vector.GetNormal(in lhs, in rhs, out r);
                return r;
            }
            internal static Vector3 GetNormalSameSideAs(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 sameSideAs)
            {
                var lhs = p1 - p2;
                var rhs = p3 - p2;
                Vector3 r;
                vector.GetNormal(in lhs, in rhs, out r);
                vector.EnsurePointSameDirAs(in r, in sameSideAs, out r);
                return r;
            }

            /// <summary>
            /// if you are looking from above the normal: counter clockwise
            /// </summary>
            internal static void GetNormal(in Vector3 p1, in Vector3 p2, in Vector3 p3, out Vector3 r)
            {
                var lhs = p1 - p2;
                var rhs = p3 - p2;
                vector.GetNormal(in lhs, in rhs, out r);
            }
            internal static bool TryGetNormal(in Vector3 p1, in Vector3 p2, in Vector3 p3, out Vector3 r)
            {
                var lhs = p1 - p2;
                var rhs = p3 - p2;
                return vector.TryGetNormal(in lhs, in rhs, out r);
            }
            internal static void GetNormalSameSideAs(in Vector3 p1, in Vector3 p2, in Vector3 p3, in Vector3 sameSideAs, out Vector3 normal)
            {
                var lhs = p1 - p2;
                var rhs = p3 - p2;
                vector.GetNormal(in lhs, in rhs, out normal);
                vector.EnsurePointSameDirAs(in normal, in sameSideAs, out normal);
            }
            internal static Vector2 Lerp2D(Vector2 from, Vector2 to, double ratio)
            {
                var r = (float)ratio;
                if (r.IsEqual(0))
                {
                    return from;
                }
                return new Vector2
                {
                    x = from.x + (to.x - from.x) * r,
                    y = from.y + (to.y - from.y) * r
                };
            }
            internal static void Lerp2D(in Vector2 from, in Vector2 to, double ratio, out Vector2 result)
            {
                var r = (float)ratio;
                if (r.IsEqual(0))
                {
                    result = from;
                    return;
                }
                result = new Vector2
                {
                    x = from.x + (to.x - from.x) * r,
                    y = from.y + (to.y - from.y) * r
                };
            }
            internal static Vector3 Lerp(in Vector3 from, in Vector3 to, double ratio)
            {
                var r = (float)ratio;
                if (r.IsEqual(0))
                {
                    return from;
                }
                return new Vector3(from.x + (to.x - from.x) * r, from.y + (to.y - from.y) * r, from.z + (to.z - from.z) * r);
            }
            internal static void Lerp(in Vector3 from, in Vector3 to, double ratio, out Vector3 result)
            {
                var r = (float)ratio;
                if (r.IsEqual(0))
                {
                    result = from;
                    return;
                }
                result = new Vector3(from.x + (to.x - from.x) * r, from.y + (to.y - from.y) * r, from.z + (to.z - from.z) * r);
            }
            internal static Vector3 Lerp(Vector3 from, Vector3 to, double ratio)
            {
                var r = (float)ratio;
                if (r.IsEqual(0))
                {
                    return from;
                }
                return new Vector3(from.x + (to.x - from.x) * r, from.y + (to.y - from.y) * r, from.z + (to.z - from.z) * r);
            }

            internal static Vector3 Lerp(Vector3 from, Vector3 to, double ratio,
                Func<float, float> xFunc, Func<float, float> yFunc, Func<float, float> zFunc)
            {
                var r = (float)ratio;
                if (r.IsEqual(0))
                {
                    return from;
                }
                if (xFunc == null) xFunc = n => n;
                if (yFunc == null) yFunc = n => n;
                if (zFunc == null) zFunc = n => n;
                return new Vector3(
                    from.x + (to.x - from.x) * xFunc(r),
                    from.y + (to.y - from.y) * yFunc(r),
                    from.z + (to.z - from.z) * zFunc(r)
                );
            }

            internal static bool TryMoveRel2D(in Vector2 from, in Vector2 to, double ratio, out Vector2 result)
            {
                var r = (float)ratio;
                if (r.IsEqual(0))
                {
                    result = from;
                    return false;
                }
                result = new Vector2
                {
                    x = from.x + (to.x - from.x) * r,
                    y = from.y + (to.y - from.y) * r
                };
                return true;
            }
            internal static bool TryMoveRel(in Vector3 from, in Vector3 to, double ratio, out Vector3 result)
            {
                var r = (float)ratio;
                if (r.IsEqual(0))
                {
                    result = from;
                    return false;
                }
                result = new Vector3
                {
                    x = from.x + (to.x - from.x) * r,
                    y = from.y + (to.y - from.y) * r,
                    z = from.z + (to.z - from.z) * r
                };
                return true;
            }
            internal static bool TryMoveAbs(float fromX, float fromY, float toX, float toY, double distance, out float x, out float y)
            {
                if (distance.IsEqual(0))
                {
                    x = fromX;
                    y = fromY;
                    return false;
                }

                var preDistance = fun.distance.Between(fromX, fromY, toX, toY);

                if (preDistance.IsEqual(0))
                {
                    x = toX;
                    y = toY;
                    return false;
                }

                var amount = (float)distance / preDistance;
                x = fromX + (toX - fromX) * amount;
                y = fromY + (toY - fromY) * amount;
                return true;
            }
            internal static bool TryMoveAbs2D(in Vector2 from, in Vector2 to, double distance, out Vector2 result)
            {
                if (distance.IsEqual(0))
                {
                    result = from;
                    return false;
                }

                var preDistance = fun.distance.Between(in from, in to);

                if (preDistance.IsEqual(0))
                {
                    result = to;
                    return false;
                }

                var amount = (float)distance / preDistance;
                result = new Vector2
                {
                    x = from.x + (to.x - from.x) * amount,
                    y = from.y + (to.y - from.y) * amount
                };
                return true;
            }
            internal static bool TryMoveAbs(in Vector3 from, in Vector3 to, double distance, out Vector3 result)
            {
                if (distance.IsEqual(0))
                {
                    result = from;
                    return false;
                }

                var dir = (to - from);
                var mag = vector.Magnitude(in dir);
                if (mag < 0.00001)
                {
                    result = to;
                    return false;
                }
                dir = dir / mag;

                result = from + dir * (float)distance;
                return true;
            }
            internal static Vector2 MoveAbs2D(Vector2 from, Vector2 to, double distance)
            {
                if (distance.IsEqual(0))
                {
                    return from;
                }

                var dir = (to - from);
                var mag = vector.Magnitude2D(in dir);
                if (mag < 0.00001) return to;

                dir = dir / mag;

                return from + dir * (float)distance;
            }
            internal static Vector3 MoveAbs(Vector3 from, Vector3 to, double distance)
            {
                if (distance.IsEqual(0))
                {
                    return from;
                }

                var dir = (to - from);
                var mag = vector.Magnitude(in dir);
                if (mag < 0.00001)
                {
                    return to;
                }
                dir = dir / mag;

                return from + dir * (float)distance;
            }
            internal static Vector3 MoveAbs(in Vector3 from, in Vector3 to, double distance)
            {
                if (distance.IsEqual(0))
                {
                    return from;
                }

                var dir = (to - from);
                var mag = vector.Magnitude(in dir);
                if (mag < 0.00001)
                {
                    return to;
                }
                dir = dir / mag;

                return from + dir * (float)distance;
            }
            internal static bool MoveAbs(in Vector3 from, in Vector3 to, double distance, out Vector3 result)
            {
                if (distance.IsEqual(0))
                {
                    result = from;
                    return false;
                }

                var dir = (to - from);
                var mag = vector.Magnitude(in dir);
                if (mag < 0.00001)
                {
                    result = to;
                    return false;
                }
                dir = dir / mag;

                result = from + dir * (float)distance;
                return true;
            }

            internal static Vector2 Middle2D(Vector2 a, Vector2 b)
            {
                return new Vector2((b.x - a.x) * 0.5f + a.x, (b.y - a.y) * 0.5f + a.y);
            }
            internal static void Middle2D(in Vector2 a, in Vector2 b, out Vector2 output)
            {
                output = new Vector2((b.x - a.x) * 0.5f + a.x, (b.y - a.y) * 0.5f + a.y);
            }
            internal static Vector3 Middle(Vector3 a, Vector3 b)
            {
                return new Vector3((b.x - a.x) * 0.5f + a.x, (b.y - a.y) * 0.5f + a.y, (b.z - a.z) * 0.5f + a.z);
            }
            internal static void Middle(in Vector3 a, in Vector3 b, out Vector3 output)
            {
                output = new Vector3((b.x - a.x) * 0.5f + a.x, (b.y - a.y) * 0.5f + a.y, (b.z - a.z) * 0.5f + a.z);
            }

            internal static void EnforceWithin(ref Vector2 current, in Vector2 start, in Vector2 end)
            {
                var isWithin =
                    current.x <= max(start.x, end.x) + epsilon && current.x >= min(start.x, end.x) - epsilon &&
                    current.y <= max(start.y, end.y) + epsilon && current.y >= min(start.y, end.y) - epsilon;

                if (!isWithin)
                {
                    var dSq1 = distanceSquared.Between(in current, in start);
                    var dSq2 = distanceSquared.Between(in current, in end);

                    current = dSq1 < dSq2 ? start : end;
                }
            }

            internal static void GetPlaneNormalOfTheSideOf(in Vector3 planeNormal, in Vector3 planeCenter, in Vector3 point, out Vector3 planeNormalOfTheSideOfPoint)
            {
                var toPoint = (point - planeCenter).normalized;
                planeNormal.Normalize();

                var dp = dot(in toPoint, in planeNormal);
                if (dp > 0)
                {
                    planeNormalOfTheSideOfPoint = planeNormal;
                }
                else
                {
                    planeNormalOfTheSideOfPoint = -planeNormal;
                }
            }


            internal static void LiftAboveTowards(in Vector3 point, in Vector3 planeNormal, in Vector3 planePoint, in Vector3 towardsPoint, double liftAmount, out Vector3 pointLifted)
            {
                var normal = IsAbovePlane(in towardsPoint, in planeNormal, in planePoint) ? planeNormal : -planeNormal;
                pointLifted = point + normal * (float)liftAmount;
            }

            internal static bool IsBetweenTwo(in Vector3 point, in Vector3 limitA, in Vector3 limitB)
            {
                var vec = limitA - limitB;
                var normMag = vec.magnitude;
                if (normMag < 0.00001)
                {
                    return false;
                }
                var normB = vec / normMag;
                var normA = -normB;

                return IsAbovePlane(in point, in normB, in limitB) && IsAbovePlane(in point, in normA, in limitA);
            }

            internal static Vector3 MidAwayFromAxis(in Vector3 point1, in Vector3 point2, double awayMeters, in Vector3 axis)
            {
                return MidAwayFromAxis(in point1, in point2, awayMeters, in axis, in v3.zero);
            }

            internal static Vector3 MidAwayFromAxis(in Vector3 point1, in Vector3 point2, double awayMeters, in Vector3 axis, in Vector3 axisPoint)
            {
                Vector3 mid;
                Lerp(in point1, in point2, 0.5, out mid);

                Vector3 proj;
                ProjectOnLine(in mid, in axisPoint, in axis, out proj);

                var dir = (mid - proj).normalized;

                return mid + dir * (float)awayMeters;
            }

            internal static bool IsFirstCloserToLine(in Vector3 first, in Vector3 second, in Vector3 linePoint1, in Vector3 linePoint2)
            {
                Vector3 firstOnLine, secondOnLine;
                ProjectOnLine(in first, in linePoint1, in linePoint2, out firstOnLine);
                ProjectOnLine(in second, in linePoint1, in linePoint2, out secondOnLine);

                return distanceSquared.Between(in first, in firstOnLine) <
                       distanceSquared.Between(in second, in secondOnLine);
            }

            internal static void GetClosestToPoint(in Vector3 targetPoint, in Vector3 p1, in Vector3 p2, out Vector3 closestToTarget)
            {
                var dSq1 = fun.distanceSquared.Between(in targetPoint, in p1);
                var dSq2 = fun.distanceSquared.Between(in targetPoint, in p2);
                closestToTarget = dSq1 <= dSq2 ? p1 : p2;
            }
        }

        #endregion
        #region polygon
        internal static class polygon
        {
            internal static bool IsPointWithin(Vector2 point, Vector2[] points)
            {
                var result = false;
                int curr;
                var count = points.Length;
                var prevPoint = points[points.Length - 1];
                for (curr = 0; curr < count; curr++)
                {
                    var currPoint = points[curr];

                    if (((currPoint.y > point.y) != (prevPoint.y > point.y)) &&
                         (point.x < (prevPoint.x - currPoint.x) * (point.y - currPoint.y) / (prevPoint.y - currPoint.y) + currPoint.x))
                        result = !result;

                    prevPoint = currPoint;
                }
                return result;
            }
            internal static bool IsPointWithinHorzPoly(Vector3 point, Vector3[] points)
            {
                var result = false;
                int curr;
                var count = points.Length;
                var prevPoint = points[points.Length - 1];
                for (curr = 0; curr < count; curr++)
                {
                    var currPoint = points[curr];

                    if (((currPoint.z > point.z) != (prevPoint.z > point.z)) &&
                        (point.x < (prevPoint.x - currPoint.x) * (point.z - currPoint.z) / (prevPoint.z - currPoint.z) + currPoint.x))
                        result = !result;

                    prevPoint = currPoint;
                }
                return result;
            }
        }
        #endregion
        #region random
        internal static class random
        {
            private static readonly System.Random _rnd = new System.Random((int)(DateTime.UtcNow.Ticks % 1000000000));
            internal static float Between(double min, double max)
            {
                return Between((float)min, (float)max);
            }
            // probabilityDistribFunc => 0; would mean non altered probability, func [0-1]
            internal static float Between(double min, double max, Func<double, double> probabilityDistribFunc)
            {
                return Between((float)min, (float)max, probabilityDistribFunc);
            }
            internal static int Between(int min, int max)
            {
                return _rnd.Next(min, max + 1);
            }
            internal static float Between(float min, float max)
            {
                var n = (float)_rnd.NextDouble();
                return (max - min) * n + min;
            }
            internal static float number01 => (float)_rnd.NextDouble();

            // probabilityDistribFunc => 0; would mean non altered probability, func [0-1]
            internal static float Between(float min, float max, Func<double, double> probabilityDistribFunc)
            {
                var nd = _rnd.NextDouble();
                var n = (float)probabilityDistribFunc(nd);
                var range = (max - min);
                var candidate = range * n + min;
                return candidate.Clamp(min, max);
            }
            internal static T Of<T>(T a, T b)
            {
                return Bool(0.5) ? a : b;
            }
            internal static T Of<T>(T a, T b, T c)
            {
                var n = number01;
                return n <= 0.33333 ? a : n <= 0.66666 ? b : c;
            }
            internal static T Of<T>(IList<T> arr)
            {
                return arr[Index(arr.Count)];
            }
            internal static T Of<T>(Func<double, double> probabilityDistribFunc, IList<T> arr)
            {
                return arr[Index(arr.Count, probabilityDistribFunc)];
            }
            internal static T Of<T>(params T[] arr)
            {
                return arr[Index(arr.Length)];
            }
            internal static T Of<T>(Func<double, double> probabilityDistribFunc, params T[] arr)
            {
                return arr[Index(arr.Length)];
            }
            internal static T OfExcept<T>(T[] arr, T except)
            {
                if (arr.Length == 1) return arr[0];
                T curr;
                var i = 0;
                do
                {
                    curr = Of(arr);
                    if (++i > 16) break;// check to prevent endless loop
                }
                while (curr.Equals(except));
                return curr;
            }
            internal static int IndexExcept(int numberAll, int exceptIndex)
            {
                if (numberAll <= 0) return 0;
                if (exceptIndex < 0 || exceptIndex >= numberAll) return Between(0, numberAll - 1);
                var i = Between(0, numberAll - 2);
                return (i >= exceptIndex) ? i + 1 : i;
            }
            internal static T Between<T>(IList<T> arr, int exceptIndex)
            {
                return arr[Index(arr.Count, exceptIndex)];
            }
            internal static int Index(int count)
            {
                return _rnd.Next(0, count);
            }
            internal static int Index(int count, int exceptIndex)
            {
                if (exceptIndex < 0 || exceptIndex >= count) return Index(count);
                var i = Index(count - 1);
                if (i >= exceptIndex) ++i;
                return i;
            }
            internal static int Index(int count, Func<double, double> probabilityDistribFunc)
            {
                var nd = _rnd.NextDouble();
                var n = (float)probabilityDistribFunc(nd);
                var index = (int)Math.Round(count * n);
                return index.Clamp(0, count - 1);
            }
            internal static bool Bool(double probability)
            {
                if (probability < 0.000001) return false;
                if (probability > 0.999999) return true;
                var n = (float)_rnd.NextDouble();
                return n <= probability;
            }
            internal static Vector2 V2(float x1, float y1, float x2, float y2)
            {
                return new Vector2(Between(x1, x2), Between(y1, y2));
            }

            internal static Vector3 PointOnPlane(Vector3 position, Vector3 normal, float radius)
            {
                Vector3 randomPoint;

                do
                {
                    randomPoint = Vector3.Cross(UnityEngine.Random.insideUnitSphere, normal);
                } while (randomPoint == Vector3.zero);

                randomPoint.Normalize();
                randomPoint *= radius;
                randomPoint += position;

                return randomPoint;
            }

            internal static void PointOnPlane(in Vector3 position, in Vector3 normal, float radius, out Vector3 result)
            {
                Vector3 randomPoint;

                do
                {
                    randomPoint = Vector3.Cross(UnityEngine.Random.insideUnitSphere, normal);
                } while (randomPoint == Vector3.zero);

                randomPoint.Normalize();
                randomPoint *= radius;
                randomPoint += position;

                result = randomPoint;
            }
            internal static bool TrySelectIndexNotInBit(int len, ref long mask, out int index)
            {
                if (len >= 64) throw new ArgumentException("The length of mask must be less than 32");
                var numChecked = 0;
                for (var i = 0; i < len; ++i)
                {
                    if ((mask & (1 << i)) > 0) ++numChecked;
                }
                var tempIndex = Between(0, len - numChecked);
                var count = -1;
                for (var i = 0; i < len; ++i)
                {
                    var curr = (long)1 << i;
                    if ((curr & mask) == 0) ++count;
                    if (count == tempIndex)
                    {
                        mask |= curr;
                        index = i;
                        return true;
                    }
                }
                index = 0;
                return false;
            }
        }
        #endregion
        #region rotate
        internal static class rotate
        {
            internal static void AngleAndAxisToQuaternion(float degrees, in Vector3 axis, out Quaternion quaternion)
            {
                var radians = degrees * DTR;
                var s = (float)Math.Sin(radians / 2f);
                var x = axis.x * s;
                var y = axis.y * s;
                var z = axis.z * s;
                var w = (float)Math.Cos(radians / 2);
                quaternion = new Quaternion(x, y, z, w);
            }
            /// <summary>
            /// If looking from axis up towards down the positive angle results in rotation clockwise
            /// </summary>
            internal static void Vector(in Vector3 vector, in Vector3 aboutAxis, double degrees, out Vector3 result)
            {
                //var rotation = Quaternion.AngleAxis(degrees, aboutAxis);
                Quaternion rotation;
                AngleAndAxisToQuaternion((float)degrees, in aboutAxis, out rotation);

                var num1 = rotation.x * 2f;
                var num2 = rotation.y * 2f;
                var num3 = rotation.z * 2f;
                var num4 = rotation.x * num1;
                var num5 = rotation.y * num2;
                var num6 = rotation.z * num3;
                var num7 = rotation.x * num2;
                var num8 = rotation.x * num3;
                var num9 = rotation.y * num3;
                var num10 = rotation.w * num1;
                var num11 = rotation.w * num2;
                var num12 = rotation.w * num3;
                Vector3 vector3;
                vector3.x = (float)((1.0 - ((double)num5 + (double)num6)) * (double)vector.x + ((double)num7 - (double)num12) * (double)vector.y + ((double)num8 + (double)num11) * (double)vector.z);
                vector3.y = (float)(((double)num7 + (double)num12) * (double)vector.x + (1.0 - ((double)num4 + (double)num6)) * (double)vector.y + ((double)num9 - (double)num10) * (double)vector.z);
                vector3.z = (float)(((double)num8 - (double)num11) * (double)vector.x + ((double)num9 + (double)num10) * (double)vector.y + (1.0 - ((double)num4 + (double)num5)) * (double)vector.z);
                result = vector3;
            }

            internal static void Point2dAbout(in Vector2 pointToRotate, in Vector2 privotPoint, double degrees, out Vector2 result)
            {
                var s = sin(degrees * DTR);
                var c = cos(degrees * DTR);

                var px = pointToRotate.x;
                var py = pointToRotate.y;

                // translate point back to origin:
                px -= privotPoint.x;
                py -= privotPoint.y;

                // rotate point
                var xnew = px * c - py * s;
                var ynew = px * s + py * c;

                // translate point back:
                px = xnew + privotPoint.x;
                py = ynew + privotPoint.y;
                result = new Vector2(px, py);
            }
            internal static Vector2 Point2dAbout(in Vector2 pointToRotate, in Vector2 privotPoint, double degrees)
            {
                var s = sin(degrees * DTR);
                var c = cos(degrees * DTR);

                var px = pointToRotate.x;
                var py = pointToRotate.y;

                // translate point back to origin:
                px -= privotPoint.x;
                py -= privotPoint.y;

                // rotate point
                var xnew = px * c - py * s;
                var ynew = px * s + py * c;

                // translate point back:
                px = xnew + privotPoint.x;
                py = ynew + privotPoint.y;
                return new Vector2(px, py);
            }
            internal static void PointAbout(in Vector3 rotatePoint, in Vector3 pivot, in Vector3 aboutAxis, double degrees, out Vector3 result)
            {
                var rotation = Quaternion.AngleAxis((float)degrees, aboutAxis);

                var point = rotatePoint - pivot;

                var num1 = rotation.x * 2f;
                var num2 = rotation.y * 2f;
                var num3 = rotation.z * 2f;
                var num4 = rotation.x * num1;
                var num5 = rotation.y * num2;
                var num6 = rotation.z * num3;
                var num7 = rotation.x * num2;
                var num8 = rotation.x * num3;
                var num9 = rotation.y * num3;
                var num10 = rotation.w * num1;
                var num11 = rotation.w * num2;
                var num12 = rotation.w * num3;
                Vector3 vector3;
                vector3.x = (float)((1.0 - ((double)num5 + (double)num6)) * (double)point.x + ((double)num7 - (double)num12) * (double)point.y + ((double)num8 + (double)num11) * (double)point.z);
                vector3.y = (float)(((double)num7 + (double)num12) * (double)point.x + (1.0 - ((double)num4 + (double)num6)) * (double)point.y + ((double)num9 - (double)num10) * (double)point.z);
                vector3.z = (float)(((double)num8 - (double)num11) * (double)point.x + ((double)num9 + (double)num10) * (double)point.y + (1.0 - ((double)num4 + (double)num5)) * (double)point.z);
                result = vector3 + pivot;
            }

            internal static Vector3 PointAbout(in Vector3 rotatePoint, in Vector3 pivot, in Vector3 aboutAxis, double degrees)
            {
                var rotation = Quaternion.AngleAxis((float)degrees, aboutAxis);

                var point = rotatePoint - pivot;

                var num1 = rotation.x * 2f;
                var num2 = rotation.y * 2f;
                var num3 = rotation.z * 2f;
                var num4 = rotation.x * num1;
                var num5 = rotation.y * num2;
                var num6 = rotation.z * num3;
                var num7 = rotation.x * num2;
                var num8 = rotation.x * num3;
                var num9 = rotation.y * num3;
                var num10 = rotation.w * num1;
                var num11 = rotation.w * num2;
                var num12 = rotation.w * num3;
                Vector3 vector3;
                vector3.x = (float)((1.0 - ((double)num5 + (double)num6)) * (double)point.x + ((double)num7 - (double)num12) * (double)point.y + ((double)num8 + (double)num11) * (double)point.z);
                vector3.y = (float)(((double)num7 + (double)num12) * (double)point.x + (1.0 - ((double)num4 + (double)num6)) * (double)point.y + ((double)num9 - (double)num10) * (double)point.z);
                vector3.z = (float)(((double)num8 - (double)num11) * (double)point.x + ((double)num9 + (double)num10) * (double)point.y + (1.0 - ((double)num4 + (double)num5)) * (double)point.z);
                return vector3 + pivot;
            }

            internal static void Vector(in Vector3 vector, in Vector3 aboutAxis, in Quaternion rotation, out Vector3 result)
            {
                var num1 = rotation.x * 2f;
                var num2 = rotation.y * 2f;
                var num3 = rotation.z * 2f;
                var num4 = rotation.x * num1;
                var num5 = rotation.y * num2;
                var num6 = rotation.z * num3;
                var num7 = rotation.x * num2;
                var num8 = rotation.x * num3;
                var num9 = rotation.y * num3;
                var num10 = rotation.w * num1;
                var num11 = rotation.w * num2;
                var num12 = rotation.w * num3;
                Vector3 vector3;
                vector3.x = (float)((1.0 - ((double)num5 + (double)num6)) * (double)vector.x + ((double)num7 - (double)num12) * (double)vector.y + ((double)num8 + (double)num11) * (double)vector.z);
                vector3.y = (float)(((double)num7 + (double)num12) * (double)vector.x + (1.0 - ((double)num4 + (double)num6)) * (double)vector.y + ((double)num9 - (double)num10) * (double)vector.z);
                vector3.z = (float)(((double)num8 - (double)num11) * (double)vector.x + ((double)num9 + (double)num10) * (double)vector.y + (1.0 - ((double)num4 + (double)num5)) * (double)vector.z);
                result = vector3;
            }

            internal static void PointAbout(in Vector3 rotatePoint, in Vector3 pivot, in Vector3 aboutAxis, in Quaternion rotation, out Vector3 result)
            {
                var point = rotatePoint - pivot;

                var num1 = rotation.x * 2f;
                var num2 = rotation.y * 2f;
                var num3 = rotation.z * 2f;
                var num4 = rotation.x * num1;
                var num5 = rotation.y * num2;
                var num6 = rotation.z * num3;
                var num7 = rotation.x * num2;
                var num8 = rotation.x * num3;
                var num9 = rotation.y * num3;
                var num10 = rotation.w * num1;
                var num11 = rotation.w * num2;
                var num12 = rotation.w * num3;
                Vector3 vector3;
                vector3.x = (float)((1.0 - ((double)num5 + (double)num6)) * (double)point.x + ((double)num7 - (double)num12) * (double)point.y + ((double)num8 + (double)num11) * (double)point.z);
                vector3.y = (float)(((double)num7 + (double)num12) * (double)point.x + (1.0 - ((double)num4 + (double)num6)) * (double)point.y + ((double)num9 - (double)num10) * (double)point.z);
                vector3.z = (float)(((double)num8 - (double)num11) * (double)point.x + ((double)num9 + (double)num10) * (double)point.y + (1.0 - ((double)num4 + (double)num5)) * (double)point.z);
                result = vector3 + pivot;
            }
        }
        #endregion
        #region statistics
        internal static class statistics
        {
            private const float epsilon = 0.00001f;


            internal static Vector3 LerpAverageV2(Vector2 lastAverage, Vector2 current, int count)
            {
                if (count <= 1) return current;
                return Vector2.Lerp(lastAverage, current, 1 / (float)count);
            }
            internal static Vector3 LerpAverageV2(in Vector2 lastAverage, in Vector2 current, int count)
            {
                if (count <= 1) return current;
                return Vector2.Lerp(lastAverage, current, 1 / (float)count);
            }
            internal static void LerpAverageV2(in Vector2 lastAverage, in Vector2 current, int count, out Vector2 currentAverage)
            {
                if (count <= 1)
                {
                    currentAverage = current;
                    return;
                }
                currentAverage = Vector2.Lerp(lastAverage, current, 1 / (float)count);
            }

            internal static Vector3 LerpAverage(Vector3 lastAverage, Vector3 current, int count)
            {
                if (count <= 1) return current;
                return Vector3.Lerp(lastAverage, current, 1 / (float)count);
            }
            internal static Vector3 LerpAverage(in Vector3 lastAverage, in Vector3 current, int count)
            {
                if (count <= 1) return current;
                return Vector3.Lerp(lastAverage, current, 1 / (float)count);
            }
            internal static void LerpAverage(in Vector3 lastAverage, in Vector3 current, int count, out Vector3 currentAverage)
            {
                if (count <= 1)
                {
                    currentAverage = current;
                    return;
                }
                currentAverage = Vector3.Lerp(lastAverage, current, 1 / (float)count);
            }


            internal static Vector3 SlerpAverage(Vector3 lastAverage, Vector3 current, int count)
            {
                if (count <= 1) return current;
                return Vector3.Slerp(lastAverage, current, 1 / (float)count);
            }
            internal static Vector3 SlerpAverage(in Vector3 lastAverage, in Vector3 current, int count)
            {
                if (count <= 1) return current;
                return Vector3.Slerp(lastAverage, current, 1 / (float)count);
            }
            internal static void SlerpAverage(in Vector3 lastAverage, in Vector3 current, int count, out Vector3 currentAverage)
            {
                if (count <= 1)
                {
                    currentAverage = current;
                    return;
                }
                currentAverage = Vector3.Slerp(lastAverage, current, 1 / (float)count);
            }
            internal static Quaternion SlerpAverage(Quaternion lastAverage, Quaternion current, int count)
            {
                if (count <= 1) return current;
                return Quaternion.Slerp(lastAverage, current, 1 / (float)count);
            }


            internal static Quaternion SlerpAverage(in Quaternion lastAverage, in Quaternion current, int count)
            {
                if (count <= 1) return current;
                return Quaternion.Slerp(lastAverage, current, 1 / (float)count);
            }


            internal static float Average(double lastAverage, double current, int count)
            {
                return (count <= 1.0f) ? (float)current : ((float)lastAverage * (count - 1.0f) + (float)current) / count;
            }
            internal static float Average(float lastAverage, float current, int count)
            {
                return (count <= 1.0f) ? current : (lastAverage * (count - 1.0f) + current) / count;
            }
            internal static float PopulationVariance(double sumOfSquared, double sum, int count)
            {
                if (count <= 0) return 0;
                return ((float)sumOfSquared - (((float)sum * (float)sum) / count)) / count;
            }
            internal static float PopulationStandardDeviation(double sumOfSquared, double sum, int count)
            {
                return (count <= 1)
                        ? 0.0f
                        : (float)Math.Sqrt(PopulationVariance(sumOfSquared, sum, count));
            }
        }
        #endregion
        #region triangle
        internal static class triangle
        {
            /* different tasks for any type of triangle 
                   ^
                  /C\  
               a /   \ b
                /B___A\
                   c 
            */

            /* "we know sides:a,b,c what is angle C"
                   ^
                  /?\  
               a /   \ b
                /_____\
                   c 
                C = arccos((a*a + b*b - c*c) / 2*a*b)
            */
            internal static float GetDegreesBySides(double a, double b, double c)
            {
                return (float)Math.Acos((a * a + b * b - c * c) / (2 * a * b)) * RTD; // http://mathworld.wolfram.com/LawofCosines.html
            }
            /* "we know sides:a,b AND angle:C what is side c"
                   ^
                  /C\  
               a /   \ b
                /_____\
                   ? 
                c	=	sqrt(a*a + b*b-2*a*b*cos(C))
            */
            internal static float GetBaseByTwoSidesAndAngleBetween(double a, double b, double degreesC)
            {
                return (float)Math.Sqrt(a * a + b * b - 2 * a * b * Math.Cos(degreesC * DTR)); // http://mathworld.wolfram.com/LawofCosines.html
            }
            /* "we know sides:a,b AND angle:A what is angle C"
                   ^
                  /?\  
               a /   \ b
                /____A\
                   c 
                C	=	180 - A - arcsin((b * sin(A)) / a)
            */
            internal static float GetAngleDegreesByTwoSidesAndSideAngle(double a, double b, double degreesA)
            {
                return 180f - (float)degreesA - RTD * (float)Math.Asin((b * Math.Sin(degreesA * DTR)) / a); // https://www.mathsisfun.com/algebra/trig-sine-law.html
            }
            internal static Vector3 GetCentroid(Vector3 a, Vector3 b, Vector3 c)
            {
                return new Vector3((a.x + b.x + c.x) / 3f, (a.y + b.y + c.y) / 3f, (a.z + b.z + c.z) / 3f);
            }
            internal static Vector3 GetCentroid(in Vector3 a, in Vector3 b, in Vector3 c)
            {
                return new Vector3((a.x + b.x + c.x) / 3f, (a.y + b.y + c.y) / 3f, (a.z + b.z + c.z) / 3f);
            }
            internal static void GetCentroid(in Vector3 a, in Vector3 b, in Vector3 c, out Vector3 output)
            {
                output = new Vector3((a.x + b.x + c.x) / 3f, (a.y + b.y + c.y) / 3f, (a.z + b.z + c.z) / 3f);
            }
            internal static Vector2 GetCentroid2D(Vector2 a, Vector2 b, Vector2 c)
            {
                return new Vector2((a.x + b.x + c.x) / 3f, (a.y + b.y + c.y) / 3f);
            }
            internal static Vector2 GetCentroid2D(in Vector2 a, in Vector2 b, in Vector2 c)
            {
                return new Vector2((a.x + b.x + c.x) / 3f, (a.y + b.y + c.y) / 3f);
            }
            internal static void GetCentroid2D(in Vector2 a, in Vector2 b, in Vector2 c, out Vector2 centroid)
            {
                centroid = new Vector2((a.x + b.x + c.x) / 3f, (a.y + b.y + c.y) / 3f);
            }
            /*
               ^
              /|\  
           a / |h\ b
            /__|__\
               c
            height starts between sides a and b and falls info side c
            */
            internal static float GetHeight(double a, double b, double c)
            {
                if (abs(c) < 0.000001) return (float)((a + b) / 2);
                var s = (a + b + c) / 2f;
                var n = s * (s - a) * (s - b) * (s - c);
                if (n < 0) return 0;
                return (float)((2 * Math.Sqrt(n)) / c);
            }
            /*
               ^
              /|\  
           a / |h\ b
            /__|__\
             ac+bc = c

            ac+bc = c
            ac^2 + h^2 = a^2
            bc^2 + h^2 = b^2
            */
            internal static void GetBaseSubSides(double a, double b, double c, out float ac, out float bc)
            {
                var h = GetHeight(a, b, c);
                ac = sqrt(a * a - h * h);
                bc = sqrt(b * b - h * h);
            }
            /*
               ^
              /|\  
           a / |h\ b
            /__|__\
             ac+bc = c

            ac+bc = c
            ac^2 + h^2 = a^2
            bc^2 + h^2 = b^2
            */
            internal static float GetBaseSubSideAc(double a, double b, double c)
            {
                var h = GetHeight(a, b, c);
                return sqrt(a * a - h * h);
            }

            /*
               ^
              /D\  
             / |h\
            /__|__\
            baseLen

            D = 180 - 2*arctan(2h/baseLen)*radians_to_degrees
            */
            internal static float GetInIsoscelesDegreesByBaseAndHeight(double baseLen, double height)
            {
                return 180f - (float)(2 * Math.Atan((2 * height) / baseLen) * RTD);
            }
            internal static bool IsPointInside(Vector2 point, Vector2 t1, Vector2 t2, Vector2 t3)
            {
                var b1 = Sign3(in point, in t1, in t2) < 0.0f;
                var b2 = Sign3(in point, in t2, in t3) < 0.0f;
                var b3 = Sign3(in point, in t3, in t1) < 0.0f;

                return (b1 == b2) && (b2 == b3);
            }
            internal static bool IsPointInside(in Vector2 point, in Vector2 t1, in Vector2 t2, in Vector2 t3)
            {
                var b1 = Sign3(in point, in t1, in t2) < 0.0f;
                var b2 = Sign3(in point, in t2, in t3) < 0.0f;
                var b3 = Sign3(in point, in t3, in t1) < 0.0f;

                return (b1 == b2) && (b2 == b3);
            }
            private static float Sign3(in Vector2 p1, in Vector2 p2, in Vector2 p3)
            {
                return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
            }
            internal static bool Overlap(Vector3 t1p1, Vector3 t1p2, Vector3 t1p3, Vector3 t2p1, Vector3 t2p2, Vector3 t2p3)
            {
                return Overlap(in t1p1, in t1p2, in t1p3, in t2p1, in t2p2, in t2p3);
            }
            internal static bool Overlap(in Vector3 t1p1, in Vector3 t1p2, in Vector3 t1p3, in Vector3 t2p1, in Vector3 t2p2, in Vector3 t2p3)
            {
                Vector2 isect1 = Vector2.zero, isect2 = Vector2.zero;

                // compute plane equation of triangle(v0,v1,v2) 
                var e1 = t1p2 - t1p1;
                var e2 = t1p3 - t1p1;
                var n1 = cross.Product(in e1, in e2);
                var d1 = -dot(in n1, in t1p1);
                // plane equation 1: N1.X+d1=0 */

                // put u0,u1,u2 into plane equation 1 to compute signed distances to the plane
                var du0 = dot(in n1, in t2p1) + d1;
                var du1 = dot(in n1, in t2p2) + d1;
                var du2 = dot(in n1, in t2p3) + d1;

                // coplanarity robustness check 
                if (abs(du0) < Mathf.Epsilon) { du0 = 0.0f; }
                if (abs(du1) < Mathf.Epsilon) { du1 = 0.0f; }
                if (abs(du2) < Mathf.Epsilon) { du2 = 0.0f; }

                var du0du1 = du0 * du1;
                var du0du2 = du0 * du2;

                // same sign on all of them + not equal 0 ? 
                if (du0du1 > 0.0f && du0du2 > 0.0f)
                {
                    // no intersection occurs
                    return false;
                }

                // compute plane of triangle (u0,u1,u2)
                e1 = t2p2 - t2p1;
                e2 = t2p3 - t2p1;
                var n2 = cross.Product(in e1, in e2);
                var d2 = -dot(in n2, in t2p1);

                // plane equation 2: N2.X+d2=0 
                // put v0,v1,v2 into plane equation 2
                var dv0 = dot(in n2, in t1p1) + d2;
                var dv1 = dot(in n2, in t1p2) + d2;
                var dv2 = dot(in n2, in t1p3) + d2;

                if (abs(dv0) < Mathf.Epsilon) { dv0 = 0.0f; }
                if (abs(dv1) < Mathf.Epsilon) { dv1 = 0.0f; }
                if (abs(dv2) < Mathf.Epsilon) { dv2 = 0.0f; }


                var dv0dv1 = dv0 * dv1;
                var dv0dv2 = dv0 * dv2;

                // same sign on all of them + not equal 0 ? 
                if (dv0dv1 > 0.0f && dv0dv2 > 0.0f)
                {
                    // no intersection occurs
                    return false;
                }

                // compute direction of intersection line 
                var dd = Vector3.Cross(n1, n2);

                // compute and index to the largest component of D 
                var max = abs(dd.x);
                short index = 0;
                var bb = abs(dd.y);
                var cc = abs(dd.z);
                if (bb > max) { max = bb; index = 1; }
                if (cc > max) { max = cc; index = 2; }

                // this is the simplified projection onto L
                var vp0 = t1p1[index];
                var vp1 = t1p2[index];
                var vp2 = t1p3[index];

                var up0 = t2p1[index];
                var up1 = t2p2[index];
                var up2 = t2p3[index];

                // compute interval for triangle 1 
                float a = 0, b = 0, c = 0, x0 = 0, x1 = 0;
                if (ComputeIntervals(vp0, vp1, vp2, dv0, dv1, dv2, dv0dv1, dv0dv2, ref a, ref b, ref c, ref x0, ref x1))
                {
                    return TriTriCoplanar(in n1, in t1p1, in t1p2, in t1p3, in t2p1, in t2p2, in t2p3);
                }

                // compute interval for triangle 2 
                float d = 0, e = 0, f = 0, y0 = 0, y1 = 0;
                if (ComputeIntervals(up0, up1, up2, du0, du1, du2, du0du1, du0du2, ref d, ref e, ref f, ref y0, ref y1))
                {
                    return TriTriCoplanar(in n1, in t1p1, in t1p2, in t1p3, in t2p1, in t2p2, in t2p3);
                }

                var xx = x0 * x1;
                var yy = y0 * y1;
                var xxyy = xx * yy;

                var tmp = a * xxyy;
                isect1.x = tmp + b * x1 * yy;
                isect1.y = tmp + c * x0 * yy;

                tmp = d * xxyy;
                isect2.x = tmp + e * xx * y1;
                isect2.y = tmp + f * xx * y0;

                Sort(ref isect1);
                Sort(ref isect2);

                return !(isect1.y < isect2.x || isect2.y < isect1.x);
            }
            private static bool ComputeIntervals(float VV0, float VV1, float VV2,
                               float D0, float D1, float D2, float D0D1, float D0D2,
                               ref float A, ref float B, ref float C, ref float X0, ref float X1)
            {
                if (D0D1 > 0.0f)
                {
                    // here we know that D0D2<=0.0 
                    // that is D0, D1 are on the same side, D2 on the other or on the plane 
                    A = VV2; B = (VV0 - VV2) * D2; C = (VV1 - VV2) * D2; X0 = D2 - D0; X1 = D2 - D1;
                }
                else if (D0D2 > 0.0f)
                {
                    // here we know that d0d1<=0.0 
                    A = VV1; B = (VV0 - VV1) * D1; C = (VV2 - VV1) * D1; X0 = D1 - D0; X1 = D1 - D2;
                }
                else if (D1 * D2 > 0.0f || D0 > 0.000001f || D0 < -0.000001f)
                {
                    // here we know that d0d1<=0.0 or that D0!=0.0 
                    A = VV0; B = (VV1 - VV0) * D0; C = (VV2 - VV0) * D0; X0 = D0 - D1; X1 = D0 - D2;
                }
                else if (D1 > 0.000001f || D1 < -0.000001f)
                {
                    A = VV1; B = (VV0 - VV1) * D1; C = (VV2 - VV1) * D1; X0 = D1 - D0; X1 = D1 - D2;
                }
                else if (D2 > 0.000001f || D2 < -0.000001f)
                {
                    A = VV2; B = (VV0 - VV2) * D2; C = (VV1 - VV2) * D2; X0 = D2 - D0; X1 = D2 - D1;
                }
                else
                {
                    return true;
                }

                return false;
            }
            private static bool TriTriCoplanar(in Vector3 N, in Vector3 v0, in Vector3 v1, in Vector3 v2, in Vector3 u0, in Vector3 u1, in Vector3 u2)
            {
                var A = new float[3];
                short i0, i1;

                // first project onto an axis-aligned plane, that maximizes the area
                // of the triangles, compute indices: i0,i1. 
                A[0] = abs(N[0]);
                A[1] = abs(N[1]);
                A[2] = abs(N[2]);
                if (A[0] > A[1])
                {
                    if (A[0] > A[2])
                    {
                        // A[0] is greatest
                        i0 = 1;
                        i1 = 2;
                    }
                    else
                    {
                        // A[2] is greatest
                        i0 = 0;
                        i1 = 1;
                    }
                }
                else
                {
                    if (A[2] > A[1])
                    {
                        // A[2] is greatest 
                        i0 = 0;
                        i1 = 1;
                    }
                    else
                    {
                        // A[1] is greatest 
                        i0 = 0;
                        i1 = 2;
                    }
                }

                // test all edges of triangle 1 against the edges of triangle 2 
                if (EdgeAgainstTriEdges(in v0, in v1, in u0, in u1, in u2, i0, i1)) { return true; }
                if (EdgeAgainstTriEdges(in v1, in v2, in u0, in u1, in u2, i0, i1)) { return true; }
                if (EdgeAgainstTriEdges(in v2, in v0, in u0, in u1, in u2, i0, i1)) { return true; }

                // finally, test if tri1 is totally contained in tri2 or vice versa 
                if (PointInTri(in v0, in u0, in u1, in u2, i0, i1)) { return true; }
                if (PointInTri(in u0, in v0, in v1, in v2, i0, i1)) { return true; }

                return false;
            }
            private static bool EdgeAgainstTriEdges(in Vector3 v0, in Vector3 v1, in Vector3 u0, in Vector3 u1, in Vector3 u2, short i0, short i1)
            {
                // test edge u0,u1 against v0,v1
                if (EdgeEdgeTest(in v0, in v1, in u0, in u1, i0, i1)) { return true; }

                // test edge u1,u2 against v0,v1 
                if (EdgeEdgeTest(in v0, in v1, in u1, in u2, i0, i1)) { return true; }

                // test edge u2,u1 against v0,v1 
                if (EdgeEdgeTest(in v0, in v1, in u2, in u0, i0, i1)) { return true; }

                return false;
            }
            private static bool EdgeEdgeTest(in Vector3 v0, in Vector3 v1, in Vector3 u0, in Vector3 u1, int i0, int i1)
            {
                var Ax = v1[i0] - v0[i0];
                var Ay = v1[i1] - v0[i1];

                var Bx = u0[i0] - u1[i0];
                var By = u0[i1] - u1[i1];
                var Cx = v0[i0] - u0[i0];
                var Cy = v0[i1] - u0[i1];
                var f = Ay * Bx - Ax * By;
                var d = By * Cx - Bx * Cy;
                if ((f > 0 && d >= 0 && d <= f) || (f < 0 && d <= 0 && d >= f))
                {
                    var e = Ax * Cy - Ay * Cx;
                    if (f > 0)
                    {
                        if (e >= 0 && e <= f) { return true; }
                    }
                    else
                    {
                        if (e <= 0 && e >= f) { return true; }
                    }
                }

                return false;
            }
            private static bool PointInTri(in Vector3 v0, in Vector3 u0, in Vector3 u1, in Vector3 u2, short i0, short i1)
            {
                // is T1 completly inside T2?
                // check if v0 is inside tri(u0,u1,u2)
                var a = u1[i1] - u0[i1];
                var b = -(u1[i0] - u0[i0]);
                var c = -a * u0[i0] - b * u0[i1];
                var d0 = a * v0[i0] + b * v0[i1] + c;

                a = u2[i1] - u1[i1];
                b = -(u2[i0] - u1[i0]);
                c = -a * u1[i0] - b * u1[i1];
                var d1 = a * v0[i0] + b * v0[i1] + c;

                a = u0[i1] - u2[i1];
                b = -(u0[i0] - u2[i0]);
                c = -a * u2[i0] - b * u2[i1];
                var d2 = a * v0[i0] + b * v0[i1] + c;

                if (d0 * d1 > 0.0f)
                {
                    if (d0 * d2 > 0.0f) { return true; }
                }

                return false;
            }
            private static void Sort(ref Vector2 v)
            {
                if (v.x > v.y)
                {
                    var temp = v.x;
                    v.x = v.y;
                    v.y = temp;
                }
            }
        }
        #endregion
        #region invserseKinematics
        internal static class inverseKinematics
        {
            public static void Finger(
                Vector3 oriPo, Vector3 oriFw, Vector3 oriUp, Vector3 tarPo,
                Vector3[] points, // 4 points (including target)
                float[] lengths, // 3 floats
                out Vector3 join1, out Vector3 join2, out Vector3 chainUpDir)
            {
                if (points == null || points.Length != 4) throw new ArgumentException("Expected 4 points");
                if (lengths == null || lengths.Length != 3) throw new ArgumentException("Expected 3 lengths");

                float distToTarg;
                var toTargDir = (tarPo - oriPo).ToUnit(out distToTarg);
                var lenAll = (float)(lengths[0] + lengths[1] + lengths[2]);
                if (distToTarg > lenAll)
                {
                    tarPo = oriPo + toTargDir * lenAll;
                    distToTarg = lenAll;
                }
                var relDist01 = distToTarg / lenAll;
                GetPlacementDir(in oriFw, in oriUp, in toTargDir, out chainUpDir);

                var iniDir = chainUpDir.RotateTowards(toTargDir, relDist01.Clamp01().From01ToRange(30, 45));

                //dbg.DrawLine(points.hc(), oriPo, oriPo+ chainUpDir, Color.green);
                //dbg.DrawLine(points.hc()+1,oriPo, oriPo+ iniDir, Color.magenta);
                points[0] = oriPo;
                points[1] = oriPo + iniDir * lengths[0];
                points[2] = points[1] + iniDir * lengths[1];
                points[3] = points[2] + iniDir * lengths[2];

                FABRIK(tarPo, points, lengths, 4, (int)relDist01.From01ToRange(3, 50));

                join1 = points[1];
                join2 = points[2];
            }
            static void GetPlacementDir(in Vector3 oriFw, in Vector3 oriUp, in Vector3 toTargDir, out Vector3 placementDir)
            {
                /*Vector3 oriRt;
                fun.vector.GetNormal(in oriFw, in oriUp, out oriRt);
                var dp01 = dot(in oriUp, in toTargDir).Abs();
                var rhsDir = slerp(in oriUp, in oriFw, dp01);
                Vector3 planeNorm;
                fun.vector.GetNormal(in toTargDir, in rhsDir, out planeNorm);
                fun.vector.EnsurePointSameDirAs(in planeNorm, in oriRt, out planeNorm);
                fun.vector.GetNormal(in planeNorm, in toTargDir, out placementDir);*/
                Vector3 oriRt;
                fun.vector.GetNormal(in oriFw, in oriUp, out oriRt);
                Vector3 toTargOnRtDir;
                fun.vector.ProjectOnPlane(in toTargDir, in oriRt, out toTargOnRtDir);
                fun.vector.GetNormal(in oriRt, in toTargOnRtDir, out placementDir);
            }

            // fun.invserseKinematics.ThreeJoinOnVertPlane(in oriPo, in oriFw, in oriUp, in tarPo, len1, len2, out j0, out j1);
            internal static void ThreeJoinOnVertPlane(
                Vector3 oriPo, Vector3 oriFw, Vector3 oriUp, Vector3 tarPo,
                double len0, double len1, out Vector3 join1, out Vector3 join2)
            {
                float distToTarg;
                var toTargDir = (tarPo - oriPo).ToUnit(out distToTarg);

                if (dot(in toTargDir, in oriUp).Abs() < 0.999)
                {
                    Vector3 planeNormToBe;
                    vector.GetNormal(in toTargDir, in oriUp, out planeNormToBe);
                    vector.ProjectOnPlane(in oriFw, in planeNormToBe, out oriFw);
                }

                if (point.IsBelowPlane(in tarPo, in oriFw, in oriPo))
                {
                    oriUp *= -1;
                    oriFw *= -1;
                }

                var lenAll = (float)(len0 + len1 + len0);
                if (distToTarg >= lenAll)
                {
                    join1 = oriPo + toTargDir * (float)len0;
                    join2 = join1 + toTargDir * (float)len1;
                    return;
                }

                Vector3 realUp;
                vector.GetRealUp(in toTargDir, in oriUp, in oriFw, out realUp);

                var halfRemaining = ((distToTarg - len1) / 2f);

                var deg = Math.Acos(halfRemaining / len0) * RTD;

                var toJ0 = toTargDir.RotateTowardsCanOvershoot(in realUp, deg);
                join1 = oriPo + toJ0 * (float)len0;

                var toJ1 = (-toTargDir).RotateTowardsCanOvershoot(in realUp, deg);
                join2 = tarPo + toJ1 * (float)len0;
            }

            internal static void TwoJoinsOnVertPlane(Vector3 oriPo, Vector3 oriFw, Vector3 oriUp, Vector3 tarPo, double len1, double len2, out Vector3 join)
            {
                float distToTarg;
                var toTargDir = (tarPo - oriPo).ToUnit(out distToTarg);
                var lenAll = (float)(len1 + len2);
                if (distToTarg >= lenAll)
                {
                    join = oriPo + toTargDir * (float)len1;
                    return;
                }
                Vector3 realUp;
                toTargDir.GetRealUp(in oriUp, in oriFw, out realUp);
                Vector3 planeNor;
                vector.GetNormal(in toTargDir, in realUp, out planeNor);

                var degNearLen1 = triangle.GetDegreesBySides(len1, distToTarg, len2);
                var toJoin = toTargDir.RotateTowardsCanOvershoot(in realUp, degNearLen1);
                join = oriPo + toJoin * (float)len1;
            }
            internal static bool ValidateEllipseConstraint(
                in Vector3 originalDir, in Vector3 normal, in Vector3 currentDir,
                double maxDegreesTowardsNormal, double maxDegreesAwayFromNormal,
                out float maxAllowed)
            {
                if (maxDegreesTowardsNormal < 0 || maxDegreesAwayFromNormal < 0)
                    throw new ArgumentException($"Constraint limits cannot be negative maxDegreesTowardsNormal={maxDegreesTowardsNormal} maxDegreesAwayFromNormal={maxDegreesAwayFromNormal}");
                var currentDeg = angle.BetweenVectorsUnSignedInDegrees(in originalDir, in currentDir);
                if (currentDeg < 0.0001)
                {
                    maxAllowed = min(maxDegreesTowardsNormal, maxDegreesAwayFromNormal);
                    return true;
                }
                vector.ProjectOnPlane(in currentDir, in originalDir, out var currProj);
                currProj.Normalize();
                var realNormal = originalDir.GetRealUp(in normal);
                var ellipse01 = abs(angle.BetweenVectorsUnSignedInDegrees(in currProj, in realNormal) / 180f).Clamp01();
                maxAllowed = ellipse01.From01ToRange(maxDegreesTowardsNormal, maxDegreesAwayFromNormal);
                return maxAllowed >= currentDeg;
            }

            /*

            points = "Thigh", "Shin", "Foot"
            initialLimit = 1 repetitions = 1
            IN:2(Foot) FW: 1(Shin)| BK: 1(Shin) 2(Foot)
            initialLimit = 1 repetitions = 2
                IN:2(Foot) FW: 1(Shin)| BK: 1(Shin) 2(Foot)
                IN:2(Foot) FW: 1(Shin)| BK: 1(Shin) 2(Foot)

            points = "AbdomenLower", "AbdomenUpper", "ChestLower", "ChestUpper", "NeckLower"
            initialLimit = 1 repetitions = 1
                IN:4(NeckLower) FW:3(ChestUpper)| BK:3(ChestUpper) 4(NeckLower)
                IN:4(NeckLower) FW:3(ChestUpper) 2(ChestLower)| BK:2(ChestLower) 3(ChestUpper) 4(NeckLower)
                IN:4(NeckLower) FW:3(ChestUpper) 2(ChestLower) 1(AbdomenUpper)| BK:1(AbdomenUpper) 2(ChestLower) 3(ChestUpper) 4(NeckLower)             

            initialLimit = 3 repetitions = 1
                IN:4(NeckLower) FW:3(ChestUpper) 2(ChestLower) 1(AbdomenUpper)| BK:1(AbdomenUpper) 2(ChestLower) 3(ChestUpper) 4(NeckLower)

            initialLimit = 3 repetitions = 2
                IN:4(NeckLower) FW:3(ChestUpper) 2(ChestLower) 1(AbdomenUpper)| BK:1(AbdomenUpper) 2(ChestLower) 3(ChestUpper) 4(NeckLower)
                IN:4(NeckLower) FW:3(ChestUpper) 2(ChestLower) 1(AbdomenUpper)| BK:1(AbdomenUpper) 2(ChestLower) 3(ChestUpper) 4(NeckLower)
            */
            internal static void FABRIK(
                Vector3 handlePos,
                Vector3[] points,
                float[] lengths,
                int initialLimit = 1,
                int repetitions = 1)
            {
                var lastIndex = points.Length - 1;

                for (var j = 0; j < repetitions; ++j)
                {
                    var limitIndex = (int)max(lastIndex - initialLimit, 1);
                    while (limitIndex >= 1)
                    {
                        points[lastIndex] = handlePos;
                        // forward (neck) to (spine0)
                        for (var i = lastIndex; i > limitIndex; --i)
                        {
                            var len = lengths[i - 1];

                            var dir = (points[i - 1] - points[i]).normalized;
                            points[i - 1] = points[i] + dir * len;
                        }
                        // and backward (spine0) to J4 (neck)
                        for (var i = limitIndex - 1; i < lastIndex; ++i)
                        {
                            var len = lengths[i];

                            var dir = (points[i + 1] - points[i]).normalized;
                            points[i + 1] = points[i] + dir * len;
                        }

                        if (distance.Between(points[lastIndex], handlePos) < 0.0005)
                        {
                            return;
                        }

                        limitIndex--;
                    }
                }
            }

            internal static void Legacy_FABRIK(
                Vector3 handlePos,
                Vector3[] points,
                float[] lengths,
                int numberIterations,
                Action<int, int, Vector3[], float[]> onAfterAssignForward = null,
                Action<int, int, Vector3[], float[]> onAfterAssignBack = null)
            {
                var lastIndex = points.Length - 1;
                for (var n = 0; n < numberIterations; ++n)
                {
                    points[points.Length - 1] = handlePos;
                    // forward J4 (head) to J0 (spine1)
                    for (var i = lastIndex; i > 1; --i)
                    {
                        var len = lengths[i - 1];

                        var dir = (points[i - 1] - points[i]).normalized;
                        points[i - 1] = points[i] + dir * len;

                        onAfterAssignForward?.Invoke(i, n, points, lengths);
                    }
                    // and backward (J0 (spine1) to J4 (head))
                    for (var i = 0; i < lastIndex; ++i)
                    {
                        var len = lengths[i];

                        var dir = (points[i + 1] - points[i]).normalized;
                        points[i + 1] = points[i] + dir * len;

                        onAfterAssignBack?.Invoke(i, n, points, lengths);
                    }
                }
            }
        }
        #endregion
        #region vector
        internal static class vector
        {
            internal static void RestrictWithinPlane(in Vector3 dir, in Vector3 planeDir, out Vector3 restrictedDir)
            {
                if (IsBelowPlane(in dir, in planeDir))
                {
                    ProjectOnPlane(in dir, in planeDir, out restrictedDir);
                    restrictedDir.Normalize();
                }
                else
                {
                    restrictedDir = dir;
                }
            }
            internal static double RestrictMaxAngleFrom(in Vector3 dir, in Vector3 axisDir, double maxDegrees, out Vector3 restrictedDir)
            {
                var deg = fun.angle.BetweenVectorsUnSignedInDegrees(in dir, in axisDir);
                restrictedDir = deg > maxDegrees ? axisDir.RotateTowards(dir, maxDegrees) : dir;
                return deg;
            }
            internal static void ComputeLinkedJoinsUp(in Vector3 frontJoinFw, in Vector3 frontJoinUp, in Vector3 backJoinFw, out Vector3 backJoinUp)
            {
                var dpWithUp = dot(in frontJoinUp, in backJoinFw).ClampMin11();
                var handUpRef =
                    dpWithUp < 0
                    ? Vector3.Slerp(frontJoinUp, frontJoinFw, dpWithUp * -1)
                    : Vector3.Slerp(frontJoinUp, -frontJoinFw, dpWithUp);
                ProjectOnPlane(in handUpRef, in backJoinFw, out backJoinUp);
                backJoinUp.Normalize();
                backJoinUp = backJoinFw.GetRealUp(backJoinUp);
            }

            internal static Vector3 Slerp(Vector3 a, double t, Vector3 b)
            {
                return Vector3.SlerpUnclamped(a, b, (float)t);
            }
            internal static Vector3 Slerp(Vector3 a, Vector3 b, double t)
            {
                return Vector3.SlerpUnclamped(a, b, (float)t);
            }
            internal static Vector3 Slerp(Vector3 a, double ab, Vector3 b, double abc, Vector3 c)
            {
                return Vector3.SlerpUnclamped(Vector3.SlerpUnclamped(a, b, (float)ab), c, (float)abc);
            }
            internal static Vector3 Slerp(in Vector3 a, in Vector3 b, double t)
            {
                return Vector3.SlerpUnclamped(a, b, (float)t);
            }
            internal static void Slerp(in Vector3 a, in Vector3 b, double t, out Vector3 d)
            {
                d = Vector3.SlerpUnclamped(a, b, (float)t);
            }
            internal static Vector3 Slerp(in Vector3 a, double ab, in Vector3 b, double abc, in Vector3 c)
            {
                return Vector3.SlerpUnclamped(Vector3.SlerpUnclamped(a, b, (float)ab), c, (float)abc);
            }
            internal static float LengthOnNormal(in Vector3 vectorToPlane, in Vector3 planeNormal)
            {
                var distance = dot(in planeNormal, in vectorToPlane);
                return abs(distance);
            }
            internal static void ToLocal(in Vector3 worldVector, in Quaternion worldRotation, out Vector3 localVector)
            {
                localVector = Quaternion.Inverse(worldRotation) * worldVector;
            }
            internal static Vector3 ToLocal(in Vector3 worldVector, in Quaternion worldRotation)
            {
                return Quaternion.Inverse(worldRotation) * worldVector;
            }
            internal static Vector3 ToLocal(Vector3 worldVector, Quaternion worldRotation)
            {
                return Quaternion.Inverse(worldRotation) * worldVector;
            }


            internal static void ToWorld(in Vector3 localVector, in Quaternion worldRotation, out Vector3 worldPoint)
            {
                worldPoint = worldRotation * localVector;
            }
            internal static Vector3 ToWorld(in Vector3 localVector, in Quaternion worldRotation)
            {
                return worldRotation * localVector;
            }
            internal static Vector3 ToWorld(Vector3 localVector, Quaternion worldRotation)
            {
                return worldRotation * localVector;
            }


            internal static bool IsAbovePlane(in Vector3 vectorToPlane, in Vector3 planeNormal)
            {
                var distance = -dot(in vectorToPlane, in planeNormal);
                return distance < 0;
            }
            internal static bool IsAbovePlane(Vector3 vectorToPlane, Vector3 planeNormal)
            {
                var distance = -dot(in vectorToPlane, in planeNormal);
                return distance < 0;
            }
            internal static bool IsBelowPlane(in Vector3 vectorToPlane, in Vector3 planeNormal)
            {
                var distance = -dot(in vectorToPlane, in planeNormal);
                return distance >= 0;
            }
            internal static bool IsBelowPlane(Vector3 vectorToPlane, Vector3 planeNormal)
            {
                var distance = -dot(in vectorToPlane, in planeNormal);
                return distance >= 0;
            }
            internal static float ProjectionLength(in Vector3 ofVector, in Vector3 ontoVector)
            {
                var unitOntoVector = ontoVector.normalized;
                return dot(in ofVector, in unitOntoVector);
            }
            internal static float ProjectionLength(Vector3 ofVector, Vector3 ontoVector)
            {
                var unitOntoVector = ontoVector.normalized;
                return dot(in ofVector, in unitOntoVector);
            }
            internal static Vector3 ProjectOnNormal(Vector3 vector, Vector3 onNormal)
            {
                var dotProduct = (float)((double)onNormal.x * (double)onNormal.x + (double)onNormal.y * (double)onNormal.y + (double)onNormal.z * (double)onNormal.z); ;
                if ((double)dotProduct < (double)Mathf.Epsilon)
                    return Vector3.zero;
                return onNormal * Vector3.Dot(vector, onNormal) / (float)dotProduct;
            }
            internal static Vector3 ProjectOnNormal(in Vector3 vector, in Vector3 onNormal)
            {
                var dotProduct = (float)((double)onNormal.x * (double)onNormal.x + (double)onNormal.y * (double)onNormal.y + (double)onNormal.z * (double)onNormal.z); ;
                if ((double)dotProduct < (double)Mathf.Epsilon)
                    return Vector3.zero;
                return onNormal * Vector3.Dot(vector, onNormal) / (float)dotProduct;
            }
            internal static void ProjectOnNormal(in Vector3 vector, in Vector3 onNormal, out Vector3 projection)
            {
                var dotProduct = (float)((double)onNormal.x * (double)onNormal.x + (double)onNormal.y * (double)onNormal.y + (double)onNormal.z * (double)onNormal.z); ;
                if ((double)dotProduct < (double)Mathf.Epsilon)
                    projection = Vector3.zero;
                else
                    projection = onNormal * Vector3.Dot(vector, onNormal) / (float)dotProduct;
            }
            internal static Vector3 ProjectOnPlane(in Vector3 vector, in Vector3 planeNormal)
            {
                var normPlaneNormal = planeNormal.normalized;
                var distance = -dot(in normPlaneNormal, in vector);
                return vector + normPlaneNormal * distance;
            }
            internal static void ProjectOnPlane(in Vector3 vector, in Vector3 planeNormal, out Vector3 projection)
            {
                var normPlaneNormal = planeNormal.normalized;
                var distance = -dot(in normPlaneNormal, in vector);
                projection = vector + normPlaneNormal * distance;
            }
            internal static Vector3 ReflectOfPlane(in Vector3 vector, in Vector3 planeNormal)
            {
                Vector3 projection;
                ProjectOnPlane(in vector, in planeNormal, out projection);
                projection.Normalize();
                return Vector3.SlerpUnclamped(vector, projection.normalized, 2);
            }
            internal static void ReflectOfPlane(in Vector3 vector, in Vector3 planeNormal, out Vector3 reflection)
            {
                Vector3 projection;
                ProjectOnPlane(in vector, in planeNormal, out projection);
                projection.Normalize();
                reflection = Vector3.SlerpUnclamped(vector, projection.normalized, 2);
            }
            internal static Vector3 GetNormal(in Vector3 lhs, in Vector3 rhs)
            {
                var normal = new Vector3((lhs.y * rhs.z - lhs.z * rhs.y), (lhs.z * rhs.x - lhs.x * rhs.z), (lhs.x * rhs.y - lhs.y * rhs.x));

                // normalize:
                var len = Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z);
                if (len > 9.99999974737875E-06)
                    normal = normal / (float)len;
                else
                    normal = new Vector3(0, 0, 0);
                return normal;
            }
            internal static Vector3 GetNormalOrDefault(in Vector3 lhs, in Vector3 rhs, in Vector3 normalIfOnLine)
            {
                var normal = new Vector3((lhs.y * rhs.z - lhs.z * rhs.y), (lhs.z * rhs.x - lhs.x * rhs.z), (lhs.x * rhs.y - lhs.y * rhs.x));

                // normalize:
                var len = Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z);
                if (len > 9.99999974737875E-06)
                    normal = normal / (float)len;
                else
                    normal = normalIfOnLine;
                return normal;
            }
            internal static void GetNormalOrDefault(in Vector3 lhs, in Vector3 rhs, in Vector3 normalIfOnLine, out Vector3 normal)
            {
                normal = new Vector3((lhs.y * rhs.z - lhs.z * rhs.y), (lhs.z * rhs.x - lhs.x * rhs.z), (lhs.x * rhs.y - lhs.y * rhs.x));

                // normalize:
                var len = Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z);
                if (len > 9.99999974737875E-06)
                    normal = normal / (float)len;
                else
                    normal = normalIfOnLine;
            }
            internal static void GetNormal(in Vector3 lhs, in Vector3 rhs, out Vector3 normal)
            {
                normal = new Vector3((lhs.y * rhs.z - lhs.z * rhs.y), (lhs.z * rhs.x - lhs.x * rhs.z), (lhs.x * rhs.y - lhs.y * rhs.x));

                // normalize:
                var len = Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z);
                if (len > 9.99999974737875E-06)
                    normal = normal / (float)len;
                else
                    normal = new Vector3(0, 0, 0);
            }
            internal static bool TryGetNormal(in Vector3 lhs, in Vector3 rhs, out Vector3 normal)
            {
                normal = new Vector3((lhs.y * rhs.z - lhs.z * rhs.y), (lhs.z * rhs.x - lhs.x * rhs.z), (lhs.x * rhs.y - lhs.y * rhs.x));

                // normalize:
                var len = Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z);
                if (len <= 9.99999974737875E-06)
                {
                    normal = Vector3.zero; ;
                    return false;
                }
                normal = normal / (float)len;
                return true;
            }
            internal static void GetNormalWithAltRhs(in Vector3 lhs, in Vector3 mainRhs, in Vector3 altRhs, out Vector3 normal)
            {
                var dp = dot(in lhs, in mainRhs).Clamp(-1.0, 1.0);
                var rhs =
                    dp < 0
                    ? Vector3.Slerp(mainRhs, -altRhs, dp * -1)
                    : Vector3.Slerp(mainRhs, altRhs, dp);

                normal = new Vector3((lhs.y * rhs.z - lhs.z * rhs.y), (lhs.z * rhs.x - lhs.x * rhs.z), (lhs.x * rhs.y - lhs.y * rhs.x));

                // normalize:
                var len = Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z);
                if (len > 9.99999974737875E-06)
                    normal = normal / (float)len;
                else
                    normal = new Vector3(0, 0, 0);
            }

            internal static void GetNormal(in Vector3 lhs, in Vector3 rhs, in Vector3 normalIfOnLine, out Vector3 normal)
            {
                normal = new Vector3((lhs.y * rhs.z - lhs.z * rhs.y), (lhs.z * rhs.x - lhs.x * rhs.z), (lhs.x * rhs.y - lhs.y * rhs.x));

                // normalize:
                var len = Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z);
                if (len > 9.99999974737875E-06)
                    normal = normal / (float)len;
                else
                    normal = normalIfOnLine;
            }

            internal static void GetRealUp(in Vector3 forward, in Vector3 rawUp, out Vector3 realUp)
            {
                Vector3 right;
                cross.Product(in rawUp, in forward, out right);
                right.Normalize();
                cross.Product(in forward, in right, out realUp);
                realUp.Normalize();
            }
            internal static void GetRealUp(in Vector3 forward, in Vector3 originalUp, in Vector3 originalFw, out Vector3 realUp)
            {
                var dpWithFw = dot(in forward, in originalFw);
                var originalFw2 = originalFw;
                var originalUp2 = originalUp;
                if (dpWithFw < 0)
                {
                    originalFw2 *= -1;
                    originalUp2 *= -1;
                }

                var dpWithUp = dot(in forward, in originalUp2).Clamp(-1.0, 1.0);
                var rawUp =
                    dpWithUp < 0
                    ? Vector3.Slerp(originalUp2, originalFw2, dpWithUp * -1)
                    : Vector3.Slerp(originalUp2, -originalFw2, dpWithUp);

                Vector3 right;
                cross.Product(in rawUp, in forward, out right);
                right.Normalize();
                cross.Product(in forward, in right, out realUp);
                realUp.Normalize();
            }
            internal static Vector3 GetRealUp(Vector3 forward, Vector3 rawUp)
            {
                Vector3 right, realUp;
                cross.Product(in rawUp, in forward, out right);
                right.Normalize();
                cross.Product(in forward, in right, out realUp);
                return realUp.normalized;
            }

            internal static bool PointSameDirection(in Vector3 a, in Vector3 b)
            {
                return dot(in a, in b) > 0;
            }
            internal static bool PointSameDirection(Vector3 a, Vector3 b)
            {
                return dot(in a, in b) > 0;
            }
            internal static bool PointSameDirection2D(in Vector2 a, in Vector2 b)
            {
                return dot2D(in a, in b) > 0;
            }

            internal static bool PointDifferentDirection(in Vector3 a, in Vector3 b)
            {
                return dot(in a, in b) <= 0;
            }
            internal static bool PointDifferentDirection(Vector3 a, Vector3 b)
            {
                return dot(in a, in b) <= 0;
            }
            internal static bool PointDifferentDirection2D(in Vector2 a, in Vector2 b)
            {
                return dot2D(in a, in b) <= 0;
            }

            internal static void ComputeRandomXYAxesForPlane(in Vector3 planeNormal, out Vector3 normX, out Vector3 normY)
            {
                var fw = Vector3.right;
                cross.Product(in planeNormal, in fw, out normX);
                if (normX.sqrMagnitude < 0.001)
                {
                    var rt = Vector3.forward;
                    cross.Product(in planeNormal, in rt, out normX);
                }
                normX.Normalize();
                GetNormal(in planeNormal, in normX, out normY);
            }

            internal static void EnsurePointSameDirAs(in Vector3 direction, in Vector3 mustBeCodirectionalTo, out Vector3 sameDirection)
            {
                if (PointSameDirection(in direction, in mustBeCodirectionalTo))
                {
                    sameDirection = direction;
                }
                else
                {
                    sameDirection = -direction;
                }
            }
            internal static void EnsurePointDifferentDirThan(in Vector3 direction, in Vector3 mustNotBeCodirectionalTo, out Vector3 sameDirection)
            {
                if (PointSameDirection(in direction, in mustNotBeCodirectionalTo))
                {
                    sameDirection = -direction;
                }
                else
                {
                    sameDirection = direction;
                }
            }
            internal static bool IsCloserToFirst(in Vector3 normalizedTargetDir, in Vector3 normalizedFirst, in Vector3 normalizedSecond)
            {
                var dp1 = dot(in normalizedTargetDir, in normalizedFirst);
                var dp2 = dot(in normalizedTargetDir, in normalizedSecond);
                return dp1 > dp2;
            }
            internal static bool IsWithinCircleFormedBy(in Vector3 normalInQuestion, in Vector3 normalBoundary1, in Vector3 normalBoundary2)
            {
                var normalCenter = Vector3.Slerp(normalBoundary1, normalBoundary2, 0.5f);
                var dpMaxDiff = dot(in normalCenter, in normalBoundary1);
                var dpActDiff = dot(in normalCenter, in normalInQuestion);
                return dpActDiff >= dpMaxDiff;// remember dot product is oposit to angle, higher means closer lower further apart
            }
            internal static float HorzMagnitude(in Vector3 a)
            {
                return (float)Math.Sqrt(((double)a.x * (double)a.x + (double)a.z * (double)a.z));
            }
            internal static float Magnitude(in Vector3 a)
            {
                return (float)Math.Sqrt(((double)a.x * (double)a.x + (double)a.y * (double)a.y + (double)a.z * (double)a.z));
            }
            internal static double MagnitudeAsDouble(in Vector3 a)
            {
                return Math.Sqrt(((double)a.x * (double)a.x + (double)a.y * (double)a.y + (double)a.z * (double)a.z));
            }
            internal static float SqrMagnitude(in Vector3 a)
            {
                return (float)((double)a.x * (double)a.x + (double)a.y * (double)a.y + (double)a.z * (double)a.z);
            }
            internal static float Magnitude2D(in Vector2 a)
            {
                return (float)Math.Sqrt((double)a.x * (double)a.x + (double)a.y * (double)a.y);
            }
            internal static float SqrMagnitude2D(in Vector2 a)
            {
                return (float)((double)a.x * (double)a.x + (double)a.y * (double)a.y);
            }
            internal static bool TryNormalize(in Vector3 vector, out Vector3 normal)
            {
                var mag = Math.Sqrt(((double)vector.x * (double)vector.x + (double)vector.y * (double)vector.y + (double)vector.z * (double)vector.z));
                if (mag < 0.00001)
                {
                    normal = Vector3.zero;
                    return false;
                }
                normal = vector / (float)mag;
                return true;
            }
            internal static bool TryNormalize(in Vector3 vector, out float magnitude, out Vector3 normal)
            {
                var mag = Math.Sqrt(((double)vector.x * (double)vector.x + (double)vector.y * (double)vector.y + (double)vector.z * (double)vector.z));
                magnitude = (float)mag;
                if (mag < 0.000001)
                {
                    normal = Vector3.zero;
                    return false;
                }
                normal = vector / magnitude;
                return true;
            }
            internal static bool TryNormalize(Vector3 vector, out Vector3 normal)
            {
                var mag = Math.Sqrt(((double)vector.x * (double)vector.x + (double)vector.y * (double)vector.y + (double)vector.z * (double)vector.z));
                if (mag < 0.00001)
                {
                    normal = Vector3.zero;
                    return false;
                }
                normal = vector / (float)mag;
                return true;
            }
            internal static bool TryNormalize(Vector3 vector, out float magnitude, out Vector3 normal)
            {
                var mag = Math.Sqrt(((double)vector.x * (double)vector.x + (double)vector.y * (double)vector.y + (double)vector.z * (double)vector.z));
                magnitude = (float)mag;
                if (mag < 0.00001)
                {
                    normal = Vector3.zero;
                    return false;
                }
                normal = vector / magnitude;
                return true;
            }
        }
        #endregion
    }

    internal abstract class DtBase
    {
        internal string name;
        internal Mesh mesh;
        internal Action<Transform> set;
    }
    internal class DtTrianglePlane : DtBase
    {
        internal double length = 1f;
        internal double width = 1f;
    }
    internal class DtSquarePlane : DtBase
    {
        internal double length = 1f;
        internal double width = 1f;
    }
    internal class DtCone : DtBase
    {
        internal double height = 1f;
        internal double bottomRadius = .5f;
        internal double topRadius = .01f;
        internal int numSides = 18;
        internal double relNoseLen = 0.75;
        internal Vector3 localPos = Vector3.zero;
    }
    internal class DtCapsule : DtBase
    {
        internal double height = 3f; // distance from capsule upper sphere center to capsule lower sphere center
        internal double radius = 1f;
        internal int longitude = 24;
        internal int latitude = 16;
        internal Vector3 localPos = Vector3.zero;
    }
    internal class DtBox : DtBase
    {
        internal double x = 1;
        internal double y = 1;
        internal double z = 1;

        internal double side
        {
            get { return (x + y + z) / 3.0; }
            set { x = y = z = value; }
        }
    }

    internal class DtSphere : DtBase
    {
        internal double radius = 1;
        internal int longitude = 24;
        internal int latitude = 16;
    }
}