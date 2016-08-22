using System;
using Unianio.Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class CapsuleCapsuleCollision : BaseMovableTriangle
    {
//        private const float CapsuleRadius1 = 0.12f;
//        private const float CapsuleRadius2 = 0.2f;
//        private const float CapsuleHeight1 = 0.8f;
//        private const float CapsuleHeight2 = 0.6f;
        private const float CapsuleRadius1 = 0.14f;
        private const float CapsuleRadius2 = 0.06f;
        private const float CapsuleHeight1 = 0.4f;
        private const float CapsuleHeight2 = 0.8f;
        private Transform[] _cyl1,_cyl2;

        void Start ()
	    {
            _cyl1 = CreateCapsule(CapsuleRadius1,CapsuleHeight1);
            _cyl2 = CreateCapsule(CapsuleRadius2,CapsuleHeight2);
            _cyl2[0].position += Vector3.forward*0.5f;

            _cyl1[0].localPosition = new Vector3(-0.044f, 0.042f, 0.713f);
            _cyl1[0].localRotation = Quaternion.Euler(new Vector3(95.07898f,0,0));
	    }

        void Update()
        {
            var radius1 = CapsuleRadius1;
            var radius2 = CapsuleRadius2;
            var height1 = CapsuleHeight1;
            var height2 = CapsuleHeight2;
            var c1p1 = _cyl1[0].position - _cyl1[0].up*(height1/2 - radius1);
            var c1p2 = _cyl1[0].position + _cyl1[0].up*(height1/2 - radius1);
            var c2p1 = _cyl2[0].position - _cyl2[0].up*(height2/2 - radius2);
            var c2p2 = _cyl2[0].position + _cyl2[0].up*(height2/2 - radius2);
            

//            Debug.DrawLine(c1p1, c1p2, Color.black, 0, false);
//            Debug.DrawLine(c2p1, c2p2, Color.black, 0, false);

            
            // test code STARTS here -----------------------------------------------
            var hasCollision = 
                fun.intersection.BetweenCapsules(
                    ref c1p1, ref c1p2, radius1, 
                    ref c2p1, ref c2p2, radius2);


            



//                fun.intersection.BetweenCapsules(
//                    c1p1, c1p2, _capsuleRadius1,
//                    c2p1, c2p2, _capsuleRadius2);
            // test code ENDS here -------------------------------------------------

            SetColorOnChanged(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5,0.5,0.5,0.5), _cyl1);
            SetColorOnChanged(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5,0.5,0.5,0.5), _cyl2);
//            SetColor(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5,0.5,0.5,0.5), _cyl1);
//            SetColor(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5,0.5,0.5,0.5), _cyl2);
        }

        private Transform[] CreateCapsule(double radius, double height)
        {
            var hMin2Rad = (float)(height - radius*2);
            var cyl = 
                fun.meshes.CreateCone(
                    new DtCone
                    {
                        bottomRadius = radius,
                        height = hMin2Rad,
                        topRadius = radius,
                        localPos = new Vector3(0,-hMin2Rad/2f,0)
                    })
                    .transform;

            // lower end
            var sp1Go = 
                fun.meshes.CreateHalfSphere(
                    new DtSphere
                    {
                        radius = radius
                    })
                    .transform;
            sp1Go.SetParent(cyl);
            sp1Go.localPosition = Vector3.up*-hMin2Rad/2f;
            sp1Go.localRotation = Quaternion.Euler(180,0,0);
            sp1Go.hideFlags = HideFlags.HideInHierarchy | HideFlags.NotEditable;

            // upper end
            var sp2Go = 
                fun.meshes.CreateHalfSphere(
                    new DtSphere
                    {
                        radius = radius
                    })
                    .transform;
            sp2Go.SetParent(cyl);
            sp2Go.localPosition = Vector3.up*hMin2Rad/2f;
            sp2Go.hideFlags = HideFlags.HideInHierarchy | HideFlags.NotEditable;
            return new [] { cyl, sp1Go, sp2Go };
        }

        


    }
}