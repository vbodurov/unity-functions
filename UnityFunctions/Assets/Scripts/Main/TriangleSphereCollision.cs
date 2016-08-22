using System;
using Main;
using Unianio.Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class TriangleSphereCollision : BaseMainScript
    {
        private Transform _sphere;
        private float _sphereRadius;


        void Start ()
	    {
	        const float pointSize = 0.025f;
	        CreateTriangle(pointSize);
            _sphereRadius = 0.2f;

            _sphere = fun.meshes.CreateSphere(new DtSphere {radius = _sphereRadius }).transform;
	    }

        void Update()
        {
            Vector3 t1, t2, t3, triangleNormal;
            SetTriangle(out t1, out t2, out t3, out triangleNormal);
            var spherePos = _sphere.position;

            // test code STARTS here -----------------------------------------------

            var hasCollision = 
                fun.intersection.BetweenTriangleAndSphere(ref t1, ref t2, ref t3, ref spherePos, _sphereRadius);

            // test code ENDS here -------------------------------------------------
            
            SetColorOnChanged(hasCollision, Color.green, Color.gray, _sphere, _a, _b, _c);

        }

        
    }
}