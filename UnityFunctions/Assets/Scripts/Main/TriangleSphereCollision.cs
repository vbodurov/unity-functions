using System;
using Extensions;
using Main;
using Unianio.Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class TriangleSphereCollision : BaseMainScript
    {
        private static readonly Color green = new Color(0,1,0,0.8f);
        private static readonly Color gray = new Color(0.5f,0.5f,0.5f,0.8f);
        private Transform _sphere;
        private float _sphereRadius;
        private Transform _collision;


        void Start ()
	    {
	        const float pointSize = 0.025f;
	        CreateTriangle(pointSize);
            _sphereRadius = 0.2f;

            _sphere = fun.meshes.CreateSphere(new DtSphere {radius = _sphereRadius,name = "sphere"}).transform;
            _collision = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.03,name = "collision"})
                    .SetStandardShaderTransparentColor(1,0,0,0.9).transform;

	    }

        void Update()
        {
            Vector3 t1, t2, t3, triangleNormal;
            SetTriangle(out t1, out t2, out t3, out triangleNormal);
            var spherePos = _sphere.position;

            // test code STARTS here -----------------------------------------------

            Vector3 collision;
            var hasCollision = 
                fun.intersection.BetweenTriangleAndSphere(
                    ref t1, ref t2, ref t3, ref spherePos, _sphereRadius, out collision);
            
            // test code ENDS here -------------------------------------------------
            _collision.position = hasCollision ? collision : new Vector3(0,999,0);


            SetColorOnChanged(hasCollision, green, gray, _sphere, _a, _b, _c);

        }

        
    }
}