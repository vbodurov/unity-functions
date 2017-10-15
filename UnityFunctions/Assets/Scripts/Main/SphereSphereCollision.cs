using System;
using Extensions;
using Main;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class SphereSphereCollision : BaseMainScript
    {
        private const float Sphere1Radius = 0.3f;
        private const float Sphere2Radius = 0.9f;

        private Transform _sphere1, _sphere2;

        void Start ()
        {
            _sphere1 = fun.meshes.CreateSphere(new DtSphere { radius = Sphere1Radius }).transform;
            _sphere2 = fun.meshes.CreateSphere(new DtSphere { radius = Sphere2Radius }).transform;
            _sphere2.position += Vector3.forward * 0.75f;
        }

        void Update ()
        {
            var sc1 = _sphere1.position;
            var sc2 = _sphere2.position;
            

            // test code STARTS here -----------------------------------------------
            Vector3 circleCenter;
            float circleRadius;
            var hasCollision = 
                fun.intersection.BetweenSpheres(
                    ref sc1, Sphere1Radius, ref sc2, Sphere2Radius, out circleCenter, out circleRadius);
            // test code ENDS here -------------------------------------------------

            if (hasCollision)
            {
                var axis = (sc1 - sc2).normalized;
                Vector3 normX, normY;
                fun.vector.ComputeRandomXYAxesForPlane(ref axis, out normX, out normY);

                Vector3 prev = Vector3.zero;
                for (var a = 0; a < 361; a += 10)
                {
                    var curr = circleCenter + normX.RotateAbout(axis, a) * circleRadius;
                    if(a > 0)
                    {
                        Debug.DrawLine(curr,prev,Color.black,0,false);
                    }
                    prev = curr;
                }
            }

            SetColorOnChanged(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5, 0.5, 0.5, 0.5), _sphere1);
            SetColorOnChanged(hasCollision, rgba(0, 0, 1, 0.5), rgba(0.5, 0.5, 0.5, 0.5), _sphere2);

        }

        
    }
}