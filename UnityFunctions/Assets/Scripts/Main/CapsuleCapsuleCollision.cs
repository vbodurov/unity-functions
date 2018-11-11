using Extensions;
using Unianio;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class CapsuleCapsuleCollision : BaseMainScript
    {
//        private const float CapsuleRadius1 = 0.12f;
//        private const float CapsuleRadius2 = 0.2f;
//        private const float CapsuleHeight1 = 0.8f;
//        private const float CapsuleHeight2 = 0.6f;
        private const float CapsuleRadius1 = 0.20f;
        private const float CapsuleRadius2 = 0.06f;
        private const float CapsuleHeight1 = 0.50f;
        private const float CapsuleHeight2 = 0.8f;
        private Transform _capsule1,_capsule2,_collision;

        void Start ()
        {


            _capsule1 = 
                fun.meshes.CreateCapsule(
                    new DtCapsule {radius = CapsuleRadius1, height = CapsuleHeight1, name = "capsule_1"})
                    .transform;
            _capsule2 = 
                fun.meshes.CreateCapsule(
                    new DtCapsule {radius = CapsuleRadius2, height = CapsuleHeight2, name = "capsule_2"})
                    .transform;
            _capsule2.position += Vector3.forward*0.5f;
            _collision = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.03,name = "collision"})
                    .SetStandardShaderTransparentColor(1,0,0,0.9).transform;

        }
        void Update()
        {
            var radius1 = CapsuleRadius1;
            var radius2 = CapsuleRadius2;
            var height1 = CapsuleHeight1;
            var height2 = CapsuleHeight2;
            var c1p1 = _capsule1.position - _capsule1.up*(height1/2);
            var c1p2 = _capsule1.position + _capsule1.up*(height1/2);
            var c2p1 = _capsule2.position - _capsule2.up*(height2/2);
            var c2p2 = _capsule2.position + _capsule2.up*(height2/2);

            
            // test code STARTS here -----------------------------------------------
            Vector3 collision;
            var hasCollision = 
                fun.intersection.BetweenCapsules(
                    in c1p1, in c1p2, radius1,
                    in c2p1, in c2p2, radius2, out collision);
            // test code ENDS here -------------------------------------------------

            _collision.position = hasCollision ? collision : new Vector3(0,999,0);

            SetColorOnChanged(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5,0.5,0.5,0.5), _capsule1);
            SetColorOnChanged(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5,0.5,0.5,0.5), _capsule2);
        }

        

        


    }
}