using System;
using Unianio;
using UnityEngine;
using UnityFunctions;

namespace Extensions
{
    internal static class ColliderExtensions
    {
        internal static Vector3 ClosestToSurfacePoint(this Collider collider, Vector3 point, bool throwIfFallback = true)
        {
            var sc = collider as SphereCollider;
            if(sc != null)
            {
                var closest = sc.ClosestPoint(point);
                if(fun.distanceSquared.Between(in closest, in point) < 0.001)
                {
                    var toPointDir = (point - sc.transform.position).ToUnit(Vector3.forward);
                    closest = sc.ClosestPoint(sc.transform.position + toPointDir*(sc.radius+0.001f));
                }
                return closest;
            }
            var cc = collider as CapsuleCollider;
            if(cc != null)
            {
                var closest = cc.ClosestPoint(point);
                if(fun.distanceSquared.Between(in closest, in point) < 0.001)
                {
                    var dir = cc.direction == 0 ? new Vector3(1,0,0) : cc.direction == 1 ? new Vector3(0,1,0) : new Vector3(0,0,1);

                    var vec = (cc.transform.rotation * dir) * (cc.height/2f - cc.radius);
                    var c1 = cc.transform.position + vec;
                    var c2 = cc.transform.position - vec;
                    Vector3 drop;
                    fun.point.ClosestOnLineSegment(in point, in c1, in c2, out drop);
                    var toPointDir = (point - drop).ToUnit(Vector3.forward);
                    closest = cc.ClosestPoint(drop + toPointDir*(cc.radius+0.001f));
                }
                return closest;
            }
            if(throwIfFallback) throw new ArgumentException("Unsupported collider type "+collider);
            return collider.ClosestPoint(point);
        }

        internal static void ClosestToSurfacePointAndNormal(this Collider collider, ref Vector3 point, out Vector3 onSurface, out Vector3 normal, out bool isOnSurfaceOrInside)
        {
            isOnSurfaceOrInside = false;
            var sc = collider as SphereCollider;
            if(sc != null)
            {
                onSurface = sc.ClosestPoint(point);
                normal = (point - sc.transform.position).ToUnit(Vector3.forward);
                if(fun.distanceSquared.Between(in onSurface, in point) < 0.001)
                {
                    onSurface = sc.ClosestPoint(sc.transform.position + normal*(sc.radius+0.001f));
                    isOnSurfaceOrInside = true;
                }
                return;
            }
            var cc = collider as CapsuleCollider;
            if(cc != null)
            {
                var pos = cc.transform.position;
                onSurface = cc.ClosestPoint(point);
                var dir = cc.direction == 0 ? new Vector3(1,0,0) : cc.direction == 1 ? new Vector3(0,1,0) : new Vector3(0,0,1);
                var vec = (cc.transform.rotation * dir) * (cc.height/2f - cc.radius);
                var c1 = pos + vec;
                var c2 = pos - vec;
                Vector3 drop;
                fun.point.ClosestOnLineSegment(in point, in c1, in c2, out drop);
                normal = (point - drop).ToUnit(Vector3.forward);
                if(fun.distanceSquared.Between(in onSurface, in point) < 0.001)
                {
                    onSurface = cc.ClosestPoint(drop + normal*(cc.radius+0.001f));
                    isOnSurfaceOrInside = true;
                }
                return;
            }
            throw new ArgumentException("Unsupported collider type "+collider);
        }


        internal static bool IsPointOnSurfaceOrInside(this Collider collider, Vector3 point)
        {
            var closest = collider.ClosestPoint(point);
            return fun.distanceSquared.Between(in closest, in point) < 0.0001;
        }
    }
}