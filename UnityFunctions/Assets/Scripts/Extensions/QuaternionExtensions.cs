using System;
using Unianio;
using UnityEngine;
using static Unianio.fun;

namespace Extensions
{
    internal static class QuaternionExtensions
    {
        internal static string s(this Quaternion q)
        {
            return q.x.s() + "," + q.y.s() + "," + q.z.s() + "," + q.w.s();
        }
        internal static Quaternion AsHorizontal(this Quaternion q)
        {
            var fwDir = q * Vector3.forward;
            var upDir = q * Vector3.up;
            var v3Up = Vector3.up;
            var isUpUp = fun.vector.PointSameDirection(ref upDir, ref v3Up);
            upDir = isUpUp ? Vector3.up : Vector3.down;
            return Quaternion.LookRotation(fwDir.ToHorzUnit(), upDir);
        }
        internal static Quaternion AsLocalRot(this Quaternion worldRotation, Transform parent)
        {
            return parent == null ? worldRotation : Quaternion.Inverse(parent.rotation) * worldRotation;
        }
        internal static Quaternion AsLocalRot(this Quaternion worldRotation, Quaternion parentWorld)
        {
            return Quaternion.Inverse(parentWorld) * worldRotation;
        }
        internal static Quaternion AsWorldRot(this Quaternion localRotation, Transform parent)
        {
            return parent == null ? localRotation : parent.rotation * localRotation;
        }
        internal static Quaternion AsWorldRot(this Quaternion localRotation, Quaternion parentWorld)
        {
            return parentWorld * localRotation;
        }
        internal static Quaternion RotateTowards(this Quaternion from, Quaternion to, double maxDegreesDelta)
        {
            var angleDegrees = fun.angle.Between(ref from, ref to);
            if (Math.Abs(angleDegrees) < 0.00001 || maxDegreesDelta >= 180)
                return to;
            float t = (float)Math.Min(1f, maxDegreesDelta / angleDegrees);
            return Quaternion.Slerp(from, to, t);
        }
        internal static Quaternion RotateTowards(this Quaternion from, ref Quaternion to, double maxDegreesDelta)
        {
            var angleDegrees = fun.angle.Between(ref from, ref to);
            if (Math.Abs(angleDegrees) < 0.00001 || maxDegreesDelta >= 180)
                return to;
            float t = (float)Math.Min(1f, maxDegreesDelta / angleDegrees);
            return Quaternion.Slerp(from, to, t);
        }
        internal static void RotateTowards(this Quaternion from, ref Quaternion to, double maxDegreesDelta, out Quaternion output)
        {
            var angleDegrees = fun.angle.Between(ref from, ref to);
            if (Math.Abs(angleDegrees) < 0.00001 || maxDegreesDelta >= 180)
            {
                output = to;
                return;
            }
            float t = (float)Math.Min(1f, maxDegreesDelta / angleDegrees);
            output = Quaternion.Slerp(from, to, t);
        }

        private const double TwiseRadiansToDegrees = 2.0 * 57.2957801818848;
        internal static float DegreesTo(this Quaternion from, ref Quaternion to)
        {
            return (float)((double)Math.Acos(Math.Min(Math.Abs(dot(ref from, ref to)), 1f)) * TwiseRadiansToDegrees);
        }
        internal static float RadiansTo(this Quaternion from, ref Quaternion to)
        {
            return (float)((double)Math.Acos(Math.Min(Math.Abs(dot(ref from, ref to)), 1f)) * 2);
        }
        internal static float DegreesTo(this Quaternion from, Quaternion to)
        {
            return (float)((double)Math.Acos(Math.Min(Math.Abs(dot(ref from, ref to)), 1f)) * TwiseRadiansToDegrees);
        }
        internal static float RadiansTo(this Quaternion from, Quaternion to)
        {
            return (float)((double)Math.Acos(Math.Min(Math.Abs(dot(ref from, ref to)), 1f)) * 2);
        }
        internal static bool IsEqual(this Quaternion a, Quaternion b)
        {
            return
                ((double)a.x).IsEqual((double)b.x) &&
                ((double)a.y).IsEqual((double)b.y) &&
                ((double)a.z).IsEqual((double)b.z) &&
                ((double)a.w).IsEqual((double)b.w);
        }
        internal static bool IsEqual(this Quaternion a, ref Quaternion b)
        {
            return
                ((double)a.x).IsEqual((double)b.x) &&
                ((double)a.y).IsEqual((double)b.y) &&
                ((double)a.z).IsEqual((double)b.z) &&
                ((double)a.w).IsEqual((double)b.w);
        }
        internal static bool IsEqual(this Quaternion a, Quaternion b, double delta)
        {
            return
                ((double)a.x).IsEqual((double)b.x, delta) &&
                ((double)a.y).IsEqual((double)b.y, delta) &&
                ((double)a.z).IsEqual((double)b.z, delta) &&
                ((double)a.w).IsEqual((double)b.w, delta);
        }
        internal static bool IsEqual(this Quaternion a, ref Quaternion b, double delta)
        {
            return
                ((double)a.x).IsEqual((double)b.x, delta) &&
                ((double)a.y).IsEqual((double)b.y, delta) &&
                ((double)a.z).IsEqual((double)b.z, delta) &&
                ((double)a.w).IsEqual((double)b.w, delta);
        }
        internal static bool Equals(this Quaternion a, ref Quaternion b)
        {
            if (a.x.Equals(b.x) && a.y.Equals(b.y) && a.z.Equals(b.z))
                return a.w.Equals(b.w);
            return false;
        }
        internal static void Normalize(this Quaternion q)
        {
            var num2 = (((q.x * q.x) + (q.y * q.y)) + (q.z * q.z)) + (q.w * q.w);
            var num = 1f / ((float)Math.Sqrt(num2));
            q.x *= num;
            q.y *= num;
            q.z *= num;
            q.w *= num;
        }
        /*
roll  = atan2(2*gety*w - 2*x*z, 1 - 2*gety*gety - 2*z*z)
pitch = atan2(2*x*w - 2*gety*z, 1 - 2*x*x - 2*z*z)
yaw   =  asin(2*x*gety + 2*z*w)
         * 
roll  = Mathf.Atan2(2*gety*w - 2*x*z, 1 - 2*gety*gety - 2*z*z);
pitch = Mathf.Atan2(2*x*w - 2*gety*z, 1 - 2*x*x - 2*z*z);
yaw   =  Mathf.Asin(2*x*gety + 2*z*w);
 */

        internal static float GetYaw(this Quaternion q)
        {
            return q.eulerAngles.y;
        }
        internal static float GetPitch(this Quaternion q)
        {
            return q.eulerAngles.x;
        }
        internal static float GetRoll(this Quaternion q)
        {
            return q.eulerAngles.z;
        }
        internal static void GetYawPitchRoll(this Quaternion q, out float yaw, out float pitch, out float roll)
        {
            yaw = q.eulerAngles.y;
            pitch = q.eulerAngles.x;
            roll = q.eulerAngles.z;
        }
    }

}