using Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class CapsuleSphereCollision : BaseMainScript
    {
        private const float CapsuleRadius = 0.15f;
        private const float CapsuleHeight = 0.8f;
        private const float SphereRadius = 0.3f;
        private Transform _capsule;
        private Transform _sphere;
        private Transform _collision;

        void Start ()
	    {
            _capsule = fun.meshes.CreateCapsule(
                        new DtCapsule {radius = CapsuleRadius, height = CapsuleHeight, name = "capsule"})
                        .transform;
            _sphere = fun.meshes.CreateSphere(new DtSphere {radius = SphereRadius}).transform;
            _sphere.position += Vector3.forward*0.5f;
            _collision = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.03,name = "collision"})
                    .SetStandardShaderTransparentColor(1,0,0,0.9).transform;
	    }

        void Update()
        {

            var c1p1 = _capsule.position - _capsule.up*(CapsuleHeight/2);
            var c1p2 = _capsule.position + _capsule.up*(CapsuleHeight/2);
            var sp = _sphere.position;

            
            // test code STARTS here -----------------------------------------------
            Vector3 collision;
            var hasCollision = 
                fun.intersection.BetweenCapsuleAndSphere(
                    ref c1p1, ref c1p2, CapsuleRadius, ref sp, SphereRadius, out collision);
            // test code ENDS here -------------------------------------------------

            _collision.position = hasCollision ? collision : new Vector3(0,999,0);

            SetColorOnChanged(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5,0.5,0.5,0.5), _capsule);
            SetColorOnChanged(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5,0.5,0.5,0.5), _sphere);
        }

        

        


    }
}