using UnityEngine;

namespace UnityFunctions
{
    internal struct AngleAxisData
    {
        internal AngleAxisData(double angle, Vector3 axis)
        {
            Angle = (float)angle;
            Axis = axis;
        }
        /// <summary>
        /// if axis vector does not lie on the plane between the 2 vectors it will be projected there
        /// </summary>
        internal AngleAxisData(Vector3 fromVector, Vector3 toVector, Vector3 axis)
        {
            fromVector.Normalize();
            toVector.Normalize();
            axis.Normalize();
            var planeNormal = (fromVector - toVector).normalized;
            fun.vector.ProjectOnPlane(ref axis, ref planeNormal, out axis);
            var q1 = Quaternion.LookRotation(fromVector, axis);
            var q2 = Quaternion.LookRotation(toVector, axis);
            var sign = fun.angle.BetweenVectorsSigned(ref fromVector, ref toVector, ref axis) < 0 ? -1 : 1;
            Angle = fun.angle.Between(ref q1, ref q2)*sign;
            Axis = axis;
        }
        internal float Angle;
        internal Vector3 Axis;
        internal Quaternion Slerp(Quaternion start, double progress)
        {
            return start*Quaternion.AngleAxis(Angle*(float) progress, Axis);
        }
        internal Quaternion Slerp(double progress)
        {
            return Quaternion.AngleAxis(Angle*(float) progress, Axis);
        }
    }
}