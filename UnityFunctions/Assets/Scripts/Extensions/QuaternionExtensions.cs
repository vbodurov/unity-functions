using UnityEngine;

namespace Extensions
{
    public static class QuaternionExtensions
    {
        public static Quaternion AsLocalRot(this Quaternion worldRotation, Transform parent)
        {
            return Quaternion.Inverse(parent.rotation) * worldRotation;
        }
        public static Quaternion AsLocalRot(this Quaternion worldRotation, Quaternion parentWorld)
        {
            return Quaternion.Inverse(parentWorld) * worldRotation;
        }
        public static Quaternion AsWorldRot(this Quaternion localRotation, Transform parent)
        {
            return Quaternion.Inverse(localRotation) * parent.rotation;
        }
        public static Quaternion AsWorldRot(this Quaternion localRotation, Quaternion parentWorld)
        {
            return Quaternion.Inverse(localRotation) * parentWorld;
        }
    }
}