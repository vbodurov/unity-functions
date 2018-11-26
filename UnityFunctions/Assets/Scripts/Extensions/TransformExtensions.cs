using Unianio;
using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static Transform SetPosition(this Transform transform, in Vector3 pos)
        {
            transform.position = pos;
            return transform;
        }
        public static Transform SetPosition(this Transform transform, double x, double y, double z)
        {
            transform.position = new Vector3((float)x, (float)y, (float)z);
            return transform;
        }
        public static Transform SetScale(this Transform transform, double scale)
        {
            transform.localScale = new Vector3((float)scale, (float)scale, (float)scale);
            return transform;
        }
        public static Transform SetHideFlags(this Transform transform, HideFlags flags)
        {
            transform.hideFlags = flags;
            return transform;
        }
        public static Transform SetColor(this Transform transform, uint color)
        {
            transform.GetComponentInChildren<Renderer>().material.color = fun.color.Parse(color);
            return transform;
        }
    }
}