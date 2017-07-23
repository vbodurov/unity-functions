using UnityEngine;
using UnityFunctions;

namespace Extensions
{
    internal static class Vector2Extensions
    {
        internal static Vector3 As3d(this Vector2 v2, Vector3 origin, Vector3 normalizedX, Vector3 normalizedY)
        {
            return origin + normalizedX*v2.x + normalizedY*v2.y;
        }
        internal static Vector3 As3d(this Vector2 v2, ref Vector3 origin, ref Vector3 normalizedX, ref Vector3 normalizedY)
        {
            return origin + normalizedX*v2.x + normalizedY*v2.y;
        }
        internal static void As3d(this Vector2 v2, ref Vector3 origin, ref Vector3 normalizedX, ref Vector3 normalizedY, out Vector3 result)
        {
            result = origin + normalizedX*v2.x + normalizedY*v2.y;
        }

        

    }
}