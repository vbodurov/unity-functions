using System;
using Extensions;
using Main;
using Unianio;
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
        }

        void Update ()
        {
            var sphereCenter = _sphere.position;
            var rayOr = _origin.position;
            var rayFw = _origin.forward;
            

            // test code STARTS here -----------------------------------------------
            Vector3 collision;
            var hasCollision = 
                fun.intersection.BetweenRayAndSphere(
                    ref rayFw, ref rayOr, ref sphereCenter, SphereRadius, out collision);
            // test code ENDS here -------------------------------------------------
            if (hasCollision)
            {
                Debug.DrawLine(rayOr, collision, Color.red, 0, false);
            }

            SetColorOnChanged(hasCollision,rgba(0,1,0,0.5),rgba(0.5,0.5,0.5,0.5), _sphere);
        
        }

        
    }
}