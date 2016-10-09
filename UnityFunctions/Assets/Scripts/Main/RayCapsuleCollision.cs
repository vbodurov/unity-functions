using Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class RayCapsuleCollision : BaseMainScript
    {
        private const float CapsuleRadius = 0.1f;
        private const float CapsuleHeight = 0.8f;
        private Transform _origin;
        private Transform _capsule;

        void Start ()
        {
            const float pointSize = 0.025f;	    
            _origin = 
                fun.meshes.CreatePointyCone(new DtCone {height = pointSize*2,bottomRadius = pointSize*2,topRadius = 0.001f})
                    .SetStandardShaderTransparentColor(1,0,1,0.5).transform;
            _origin.position += Vector3.forward*-0.5f;

            _capsule = fun.meshes.CreateCapsule(new DtCapsule {radius = CapsuleRadius, height = CapsuleHeight, name = "capsule"}).transform;
            _capsule.position += Vector3.forward*0.5f;
        }

        void Update ()
        {
            var c1p1 = _capsule.position - _capsule.up*(CapsuleHeight/2);
            var c1p2 = _capsule.position + _capsule.up*(CapsuleHeight/2);
            var rayOr = _origin.position;
            var rayFw = _origin.forward;

            // test code STARTS here -----------------------------------------------
            Vector3 collision;
            var hasCollision = 
                fun.intersection.BetweenRayAndCapsule(
                    ref rayFw, ref rayOr, ref c1p1, ref c1p2, CapsuleRadius, out collision);
            // test code ENDS here -------------------------------------------------
            Debug.DrawLine(rayOr, rayOr+rayFw*3, Color.gray, 0, false);
            if (hasCollision)
            {
                Debug.DrawLine(rayOr, collision, Color.red, 0, false);
            }
            
            

            SetColorOnChanged(hasCollision,rgba(0,1,0,0.5),rgba(0.5,0.5,0.5,0.5), _capsule);
        
        }

        
    }
}