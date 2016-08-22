using System;
using Extensions;
using Main;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class RaySphereCollision : BaseMainScript
    {
        private const float SphereRadius = 0.3f;

        private Transform _origin;
        private Transform _sphere;

        void Start ()
        {
            const float pointSize = 0.025f;	    
            _origin = 
                fun.meshes.CreatePointyCone(new DtCone {height = pointSize*2,bottomRadius = pointSize*2,topRadius = 0.001f})
                    .SetStandardShaderTransparentColor(1,0,1,0.5).transform;
            _origin.position += Vector3.forward*-0.5f;

            _sphere = fun.meshes.CreateSphere(new DtSphere {radius = SphereRadius}).transform;
            _sphere.position += Vector3.forward*0.5f;

//            _intersection =
//                fun.meshes.CreateSphere(new DtSphere {radius = pointSize, name = "intersection"})
//                    .SetStandardShaderTransparentColor(1, 0, 0, 0.9).transform;
        }

        void Update ()
        {
            var sphereCenter = _sphere.position;
            var rayOr = _origin.position;
            var rayFw = _origin.forward;
            

            // test code STARTS here -----------------------------------------------
            Vector3 collision;
            var hasCollision = BetweenRayAndSphere(ref rayFw, ref rayOr, ref sphereCenter, SphereRadius, out collision);
            // test code ENDS here -------------------------------------------------
            if (hasCollision)
            {
                Debug.DrawLine(rayOr, collision, Color.red, 0, false);
            }

            SetColorOnChanged(hasCollision,rgba(0,1,0,0.5),rgba(0.5,0.5,0.5,0.5), _sphere);
        
        }

        internal static bool BetweenRayAndSphere(
            ref Vector3 rayFw, ref Vector3 rayOr, 
            ref Vector3 sphereCenter, float sphereRadius,
            out Vector3 collision)
        {
            var radiusSquared = sphereRadius*sphereRadius;
            var rayToSphere = sphereCenter - rayOr; 
            var tca = fun.dot.Product(ref rayToSphere, ref rayFw); 
            var d2 = fun.dot.Product(ref rayToSphere, ref rayToSphere) - tca * tca;
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

            collision = rayOr + rayFw*t;
 
            return true;
        }
    }
}