using System;
using System.Collections.Generic;
using UnityEngine;
using UnityFunctions;

namespace Extensions
{
    internal static class Vector3Extensions
    {
        private static readonly float RTD = (float)(180 / Math.PI);
        private static readonly Vector3 unitX = new Vector3(1, 0, 0);
        private static readonly Vector3 unitY = new Vector3(0, 1, 0);

        internal static Vector3 RotateTowardsCanOvershoot(this Vector3 current, ref Vector3 target, double degrees)
        {
            Vector3 normal;
            fun.vector.GetNormal(ref current, ref target, out normal);
            Vector3 result;
            fun.rotate.Vector(ref current, ref normal, degrees, out result);
            return result;
        }
        internal static Vector3 ToUnit(this Vector3 vector, Vector3 dirIfZero)
        {
            var mag = fun.vector.Magnitude(ref vector);
            return mag < 0.00001 ? dirIfZero : vector/mag;
        }
        internal static Vector3 ToUnit(this Vector3 vector, Vector3 dirIfZero, out float magnitude)
        {
            magnitude = fun.vector.Magnitude(ref vector);
            return magnitude < 0.00001 ? dirIfZero : vector/magnitude;
        }
        internal static Vector3 ToUnit(this Vector3 vector)
        {
            var magnitude = fun.vector.Magnitude(ref vector);
            return magnitude < 0.00001 ? Vector3.zero : vector/magnitude;
        }
        internal static Vector3 ToUnit(this Vector3 vector, out float magnitude)
        {
            magnitude = fun.vector.Magnitude(ref vector);
            return magnitude < 0.00001 ? Vector3.zero : vector/magnitude;
        }
        internal static Vector3 ToUnit(this Vector3 vector, ref Vector3 dirIfZero)
        {
            var mag = fun.vector.Magnitude(ref vector);
            return mag < 0.00001 ? dirIfZero : vector/mag;
        }
        internal static Vector3 ToUnit(this Vector3 vector, ref Vector3 dirIfZero, out float magnitude)
        {
            magnitude = fun.vector.Magnitude(ref vector);
            return magnitude < 0.00001 ? dirIfZero : vector/magnitude;
        }

        internal static Vector3 ProjectOnPlaneAndNormalize(this Vector3 vector, ref Vector3 planeNormal)
        {
            fun.vector.ProjectOnPlane(ref vector, ref planeNormal, out vector);
            var mag = fun.vector.Magnitude(ref vector);
            if ((double) mag > 9.99999974737875E-06) return vector / mag;
            return Vector3.zero;
        }
        internal static Vector3 HorizontalUnit(this Vector3 v3)
        {
            v3.y = 0;
            // normalize
            var mag = (float)Math.Sqrt((float) ((double) v3.x * (double) v3.x + (double) v3.z * (double) v3.z));
            if ((double) mag > 9.99999974737875E-06)
                return v3/mag;
            return Vector3.zero;
        }
        internal static Vector3 HorizontalUnit(this Vector3 v3, Vector3 ifZero)
        {
            v3.y = 0;
            // normalize
            var mag = (float)Math.Sqrt((float) ((double) v3.x * (double) v3.x + (double) v3.z * (double) v3.z));
            if ((double) mag > 9.99999974737875E-06)
                return v3/mag;
            return ifZero;
        }
        internal static Vector3 HorizontalUnit(this Vector3 v3, Vector3 ifZero, out float mag)
        {
            v3.y = 0;
            // normalize
            mag = (float)Math.Sqrt((float) ((double) v3.x * (double) v3.x + (double) v3.z * (double) v3.z));
            if ((double) mag > 9.99999974737875E-06)
                return v3/mag;
            return ifZero;
        }
        internal static Vector3 HorizontalUnit(this Vector3 v3, out float mag)
        {
            v3.y = 0;
            // normalize
            mag = (float)Math.Sqrt((float) ((double) v3.x * (double) v3.x + (double) v3.z * (double) v3.z));
            if ((double) mag > 9.99999974737875E-06)
                return v3/mag;
            return Vector3.zero;
        }

        internal static Vector2 As2d(this Vector3 v3, Vector3 normalizedX, Vector3 normalizedY)
        {
            return new Vector2(fun.dot.Product(ref v3, ref normalizedX),fun.dot.Product(ref v3, ref normalizedY));
        }
        internal static Vector2 As2d(this Vector3 v3, ref Vector3 normalizedX, ref Vector3 normalizedY)
        {
            return new Vector2(fun.dot.Product(ref v3, ref normalizedX),fun.dot.Product(ref v3, ref normalizedY));
        }
        internal static void As2d(this Vector3 v3, ref Vector3 normalizedX, ref Vector3 normalizedY, out Vector2 result)
        {
            result = new Vector2(fun.dot.Product(ref v3, ref normalizedX),fun.dot.Product(ref v3, ref normalizedY));
        }

        internal static Vector2 As2d(this Vector3 v3, Vector3 origin, Vector3 normalizedX, Vector3 normalizedY)
        {
            v3 = v3 - origin;
            return new Vector2(fun.dot.Product(ref v3, ref normalizedX),fun.dot.Product(ref v3, ref normalizedY));
        }
        internal static Vector2 As2d(this Vector3 v3, ref Vector3 origin, ref Vector3 normalizedX, ref Vector3 normalizedY)
        {
            v3 = v3 - origin;
            return new Vector2(fun.dot.Product(ref v3, ref normalizedX),fun.dot.Product(ref v3, ref normalizedY));
        }
        internal static void As2d(this Vector3 v3, ref Vector3 origin, ref Vector3 normalizedX, ref Vector3 normalizedY, out Vector2 result)
        {
            v3 = v3 - origin;
            result = new Vector2(fun.dot.Product(ref v3, ref normalizedX),fun.dot.Product(ref v3, ref normalizedY));
        }

        internal static Vector3 MidWayTo(this Vector3 a, Vector3 b)
        {
            return fun.point.MoveRel(a,b,0.5);
        }
        internal static Vector3 MoveTowards(this Vector3 current, Vector3 target, double maxDistanceDelta)
        {
            var vector3 = target - current;
            var magnitude = vector3.magnitude;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if ((double) magnitude <= maxDistanceDelta || (double) magnitude == 0.0)
                return target;
            return current + vector3 / magnitude * (float)maxDistanceDelta;
        }
        internal static Vector3 MoveTowards(this Vector3 current, ref Vector3 target, double maxDistanceDelta)
        {
            var vector3 = target - current;
            var magnitude = vector3.magnitude;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if ((double) magnitude <= maxDistanceDelta || (double) magnitude == 0.0)
                return target;
            return current + vector3 / magnitude * (float)maxDistanceDelta;
        }
        internal static Vector3 RotateTowards(this Vector3 current, Vector3 target, float maxDegreesDelta, float maxMagnitudeDelta)
        {
            return Vector3.RotateTowards(current, target, maxDegreesDelta*fun.DTR, maxMagnitudeDelta);
        }
        internal static Vector3 RotateTowards(this Vector3 current, ref Vector3 target, float maxDegreesDelta, float maxMagnitudeDelta)
        {
            return Vector3.RotateTowards(current, target, maxDegreesDelta*fun.DTR, maxMagnitudeDelta);
        }
        internal static Vector3 RotateTowards(this Vector3 current, Vector3 target, float maxDegreesDelta)
        {
            return Vector3.RotateTowards(current, target, maxDegreesDelta*fun.DTR, 0.0f);
        }
        internal static Vector3 RotateTowards(this Vector3 current, ref Vector3 target, float maxDegreesDelta)
        {
            return Vector3.RotateTowards(current, target, maxDegreesDelta*fun.DTR, 0.0f);
        }
        internal static bool IsNan(this Vector3 v)
        {
            return float.IsNaN(v.x);
        }

        internal static bool IsEqual(this Vector3 a, Vector3 b)
        {
            return 
                a.x.IsEqual(b.x) && 
                a.y.IsEqual(b.y) && 
                a.z.IsEqual(b.z);
        }
        internal static bool IsEqual(this Vector3 a, ref Vector3 b)
        {
            return 
                a.x.IsEqual(b.x) && 
                a.y.IsEqual(b.y) && 
                a.z.IsEqual(b.z);
        }
        internal static bool IsEqual(this Vector3 a, Vector3 b, double delta)
        {
            return 
                a.x.IsEqual(b.x, delta) && 
                a.y.IsEqual(b.y, delta) && 
                a.z.IsEqual(b.z, delta);
        }
        internal static bool IsEqual(this Vector3 a, ref Vector3 b, double delta)
        {
            return 
                a.x.IsEqual(b.x, delta) && 
                a.y.IsEqual(b.y, delta) && 
                a.z.IsEqual(b.z, delta);
        }

        internal static bool Equals(this Vector3 a, ref Vector3 b)
        {
            if (a.x.Equals(b.x) && a.y.Equals(b.y))
                return a.z.Equals(b.z);
            return false;
        }
        

        
        internal static string s(this Vector3 v)
        {
            return v.x.s() + "," + v.y.s() + "," + v.z.s();
        }
//        internal static string s(this Vector3 v, int digits)
//        {
//            return Math.Round(v.x, digits).s() + "," + Math.Round(v.y, digits).s() + "," + Math.Round(v.z, digits).s();
//        }
        internal static Vector2 ToV2(this Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }
        internal static Vector3 MakeSureIsOverTerrain(this Vector3 p)
        {
            var minY = Terrain.activeTerrain.SampleHeight(p);
            return minY > p.y ? new Vector3(p.x, minY+0.2f, p.z) : p;
        }
        internal static Vector3 OverTerrain(this Vector3 p, double height)
        {
            return new Vector3(p.x, Terrain.activeTerrain.SampleHeight(p) + (float)height, p.z);
        }
        internal static Vector3 OverTerrainNoLowerThan(this Vector3 p, double height, double noLowerThan)
        {
            return new Vector3(p.x, Mathf.Max(Terrain.activeTerrain.SampleHeight(p) + (float)height, (float)noLowerThan), p.z);
        }
        internal static float HeightOverTerrain(this Vector3 p)
        {
            return p.y - Terrain.activeTerrain.SampleHeight(p);
        }
        internal static Vector3 EnsureNotUnderTerrain(this Vector3 p)
        {
            var h = p.HeightOverTerrain();
            if (h < 0)
            {
                p = p.PlusY(h.Abs() + 0.1f);
            }
            return p;
        }
        internal static Vector3 Round(this Vector3 p)
        {
            return new Vector3(Mathf.Round(p.x), Mathf.Round(p.y), Mathf.Round(p.z));
        }
        internal static Vector3 EnsureNotUnderTerrain(this Vector3 p, float minHeightOverTerrain)
        {
            var h = p.HeightOverTerrain();
            if (h < minHeightOverTerrain)
            {
                p = p.PlusY(h.Abs() + minHeightOverTerrain);
            }
            return p;
        }
        internal static Vector3 ProgressTowards(this Vector3 from, Vector3 to, float ratio)
        {
            return new Vector3
            {
                x = from.x + (to.x - from.x) * ratio,
                y = from.y + (to.y - from.y) * ratio,
                z = from.z + (to.z - from.z) * ratio
            };
        }

        internal static bool IsSameDirectionAs(this Vector3 v1, ref Vector3 v2)
        {
            return (Math.Cos(fun.angle.BetweenVectorsUnSignedInRadians(ref v1, ref v2)) * v1.magnitude * v2.magnitude) > 0;
        }

        internal static Vector3 RotateAbout(this Vector3 point, Vector3 pivot, float x, float y, float z)
        {
            return (Quaternion.Euler(x, y, z) * (point - pivot)) + pivot;
        }

        internal static Vector3 RotateAbout(this Vector3 point, Vector3 pivot, Vector3 axis, float degrees)
        {
            return (Quaternion.AngleAxis(degrees, axis) * (point - pivot)) + pivot;
        }


        internal static Vector3 RotateAbout(this Vector3 point, Vector3 axis, float degrees)
        {
            return (Quaternion.AngleAxis(degrees, axis) * point);
        }

        internal static Vector3 PlusX(this Vector3 v, double n)
        {
            v.x += (float)n;
            return v;
        }
        internal static Vector3 PlusY(this Vector3 v, double n)
        {
            v.y += (float)n;
            return v;
        }
        internal static Vector3 PlusZ(this Vector3 v, double n)
        {
            v.x += (float)n;
            return v;
        }
        internal static Vector3 PlusXYZ(this Vector3 v, double x, double y, double z)
        {
            v.x += (float)x;
            v.y += (float)y;
            v.z += (float)z;
            return v;
        }
        internal static Vector3 WithLength(this Vector3 v, double length)
        {
            return v.normalized * (float)length;
        }
        internal static Vector3 WithX(this Vector3 v, double n)
        {
            v.x = (float)n;
            return v;
        }
        internal static Vector3 WithY(this Vector3 v, double n)
        {
            v.y = (float)n;
            return v;
        }
        internal static Vector3 WithZ(this Vector3 v, double n)
        {
            v.z = (float)n;
            return v;
        }
        internal static Vector3 Clone(this Vector3 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }
        internal static Vector3 RotateToWithMaxStep(this Vector3 from, Vector3 to, float stepDegrees)
        {
            if(stepDegrees < 0.00001) return from;
            var fromVec = from.normalized;
            var toVec = to.normalized;
            Vector3 normal;
            Cross(ref fromVec, ref toVec, out normal);
            var absDegrees = fun.angle.BetweenVectorsUnSignedInRadians(ref fromVec, ref toVec)*fun.RTD;
            if (absDegrees <= stepDegrees) return to;
            return Vector3.Slerp(fromVec, toVec, stepDegrees/absDegrees);
        }
        private static Vector3 Cross(ref Vector3 lhs, ref Vector3 rhs)
        {
            return new Vector3((lhs.y * rhs.z - lhs.z * rhs.y), (lhs.z * rhs.x - lhs.x * rhs.z), (lhs.x * rhs.y - lhs.y * rhs.x));
        }
        private static void Cross(ref Vector3 lhs, ref Vector3 rhs, out Vector3 r)
        {
            r = new Vector3((lhs.y * rhs.z - lhs.z * rhs.y), (lhs.z * rhs.x - lhs.x * rhs.z), (lhs.x * rhs.y - lhs.y * rhs.x));
        }
        private static float AngleSigned(ref Vector3 v1, ref Vector3 v2, ref Vector3 n)
        {
            var x = Cross(ref v1, ref v2);
            return (float)Math.Atan2(Dot(ref n, ref x), Dot(ref v1, ref v2)) * RTD;
        }
        private static float Dot(ref Vector3 lhs, ref Vector3 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }
        internal static Vector3 SlerpAbsoluteTo(this Vector3 from, ref Vector3 to, float maxDegreesTurn)
        {
            var fromLen = from.magnitude;
            var toLen = to.magnitude;
            var fromNorm = from.normalized;
            var toNorm = to.normalized;
            Vector3 normal;
            fun.vector.GetNormal(ref fromNorm, ref toNorm, out normal);
            var angle = fun.angle.BetweenVectorsSigned(ref fromNorm, ref toNorm, ref normal);
            var absAngle = Mathf.Abs(angle);
            var turnRatio = angle.IsZero() ? 1f : (maxDegreesTurn / absAngle).Clamp01();
            if (absAngle > maxDegreesTurn)
            {
                angle = maxDegreesTurn * Math.Sign(angle);
            }
            var rot = Quaternion.AngleAxis(angle, normal);
            var resultNorm = rot * fromNorm;
            return resultNorm * (fromLen + (toLen - fromLen) * turnRatio);
        }
        internal static Vector3 SlerpAbsoluteTo(this Vector3 from, Vector3 to, float maxDegreesTurn)
        {
            return from.SlerpAbsoluteTo(ref to, maxDegreesTurn);
        }
        internal static Vector3 LerpAbsoluteTo(this Vector3 from, Vector3 to, float distance)
        {
            if (distance.IsEqual(0))
            {
                return from;
            }

            var preDistance = fun.distance.Between(ref from, ref to);

            if (preDistance.IsEqual(0))
            {
                return to;
            }

            var amount = distance / preDistance;
            return new Vector3
            {
                x = from.x + (to.x - from.x) * amount,
                y = from.y + (to.y - from.y) * amount,
                z = from.z + (to.z - from.z) * amount
            };
        }
        internal static Vector3 ApproachByMaxStep(this Vector3 from, Vector3 to, float step)
        {
            if (step.IsEqual(0))
            {
                return from;
            }

            var preDistance = Vector3.Distance(from, to);

            if (preDistance.IsEqual(0))
            {
                return to;
            }

            if (step > preDistance)
            {
                step = preDistance;
            }

            var amount = step / preDistance;
            return new Vector3
            {
                x = from.x + (to.x - from.x) * amount,
                y = from.y + (to.y - from.y) * amount,
                z = from.z + (to.z - from.z) * amount
            };
        }
        internal static Vector3 HalfWayTo(this Vector3 from, Vector3 to)
        {
            return fun.point.MoveRel(ref from, ref to, 0.5);
        }
        internal static Vector3 HalfWayTo(this Vector3 from, ref Vector3 to)
        {
            return fun.point.MoveRel(ref from, ref to, 0.5);
        }
        internal static float LenXZ(this Vector3 v)
        {
            return Mathf.Sqrt((float) ((double) v.x * (double) v.x + (double) v.z * (double) v.z));
      
        }
        internal static float MaxDim(this Vector3 v)
        {
            return Mathf.Max(Mathf.Max(v.x, v.y), v.z);
        }
        
        internal static float LengthSquared(this Vector3 v)
        {
            return (((v.x * v.x) + (v.y * v.y)) + (v.z * v.z));
        }
        internal static float DistanceTo(this Vector3 a, Vector3 b)
        {
            var vectorX = a.x - b.x;
            var vectorY = a.y - b.y;
            var vectorZ = a.z - b.z;
            return (float)Math.Sqrt(((vectorX * vectorX) + (vectorY * vectorY)) + (vectorZ * vectorZ));
        }
        internal static float DistanceTo(this Vector3 a, ref Vector3 b)
        {
            var vectorX = a.x - b.x;
            var vectorY = a.y - b.y;
            var vectorZ = a.z - b.z;
            return (float)Math.Sqrt(((vectorX * vectorX) + (vectorY * vectorY)) + (vectorZ * vectorZ));
        }
        internal static float Distance2dTo(this Vector3 a, Vector3 b)
        {
            var vectorX = a.x - b.x;
            var vectorZ = a.z - b.z;
            return (float)Math.Sqrt((vectorX * vectorX) + (vectorZ * vectorZ));
        }
        internal static float Distance2dTo(this Vector3 a, ref Vector3 b)
        {
            var vectorX = a.x - b.x;
            var vectorZ = a.z - b.z;
            return (float)Math.Sqrt((vectorX * vectorX) + (vectorZ * vectorZ));
        }
        
 

        internal static Quaternion CalculateQuaternionTo(this Vector3 src, Vector3 dest)
        {
            src.Normalize();
            dest.Normalize();

            var d = Vector3.Dot(src, dest);

            if (d >= 1f)
            {
                return Quaternion.identity;
            }
            if (d < -0.9999999f)
            {
                var ux = unitX;
                Vector3 axis;
                Cross(ref ux, ref src, out axis);

                if (axis.LengthSquared().IsZero())
                {
                    var uy = unitY;
                    Cross(ref uy, ref src, out axis);
                }

                axis.Normalize();
                Quaternion q = Quaternion.AngleAxis(180, axis);
                return q;
            }
            else
            {
                var s = (float)Math.Sqrt((1 + d) * 2);
                var invS = 1 / s;
                Vector3 c;
                Cross(ref src, ref dest, out c);
                var v = invS*c;

                var q = new Quaternion(v.x, v.y, v.z, 0.5f * s);

                Normalize(ref q);

                return q;
            }
        }
        private static void Normalize(ref Quaternion q)
        {
            var num2 = (((q.x * q.x) + (q.y * q.y)) + (q.z * q.z)) + (q.w * q.w);
            var num = 1f / ((float)Math.Sqrt(num2));
            q.x *= num;
            q.y *= num;
            q.z *= num;
            q.w *= num;
        }
        internal static Vector3 ProjectedOnPlane(this Vector3 point, Vector3 planeNormal, Vector3 planePoint)
        {
            //FirstT calculate the distance from the point to the plane:
            var distance = SignedDistancePlanePoint(ref planeNormal, ref planePoint, ref point);

            //Reverse the sign of the distance
            distance *= -1;

            //Give a translation vector
            Vector3 translationVector = SetVectorLength(ref planeNormal, distance);

            //Translate the point to form a projection
            return point + translationVector;
        }
        internal static float SignedDistanceToPlane(this Vector3 point, ref Vector3 planeNormal, ref Vector3 planePoint)
        {
            return Vector3.Dot(planeNormal, point - planePoint);
        }
        internal static float SignedDistanceToPlane(this Vector3 point, Vector3 planeNormal, Vector3 planePoint)
        {
            return Vector3.Dot(planeNormal, point - planePoint);
        }
        internal static Vector3 GetRealUp(this Vector3 forward, Vector3 aproximateUp)
        {
            Vector3 realUp;
            fun.vector.GetRealUp(ref forward, ref aproximateUp, out realUp);
            return realUp;
        }
        internal static Vector3 GetRealUp(this Vector3 forward, ref Vector3 aproximateUp)
        {
            Vector3 realUp;
            fun.vector.GetRealUp(ref forward, ref aproximateUp, out realUp);
            return realUp;
        }
        internal static void GetRealUp(this Vector3 forward, ref Vector3 originalUp, ref Vector3 originalFw, out Vector3 realUp)
        {
            fun.vector.GetRealUp(ref forward, ref originalUp, ref originalFw, out realUp);
        }
        internal static Vector3 GetRealUp(this Vector3 forward, Vector3 originalUp, Vector3 originalFw)
        {
            Vector3 realUp;
            fun.vector.GetRealUp(ref forward, ref originalUp, ref originalFw, out realUp);
            return realUp;
        }
        internal static Vector3 GetRealUp(this Vector3 forward, ref Vector3 originalUp, ref Vector3 originalFw)
        {
            Vector3 realUp;
            fun.vector.GetRealUp(ref forward, ref originalUp, ref originalFw, out realUp);
            return realUp;
        }
        private static float SignedDistancePlanePoint(ref Vector3 planeNormal, ref Vector3 planePoint, ref Vector3 point)
        {
            return Vector3.Dot(planeNormal, point - planePoint);
        }
        private static Vector3 SetVectorLength(ref Vector3 vector, float size)
        {
            return Vector3.Normalize(vector) * size;
        }

        internal static IList<Vector3> AddTo(this Vector3 v, IList<Vector3> list)
        {
            list.Add(v);
            return list;
        }
    }
}