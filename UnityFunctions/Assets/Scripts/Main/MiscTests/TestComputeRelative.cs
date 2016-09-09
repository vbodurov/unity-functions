using Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main.MiscTests
{
    public class TestComputeRelative : BaseMainScript
    {
        const float CapsuleHeight = 2f;
        const float CapsuleRadius = 0.5f;
        private Transform _capsule;

        void Start ()
	    {
            _capsule =
                fun.meshes.CreateCapsule(
                    new DtCapsule
                    {
                        name = "capsule",
                        radius = CapsuleRadius,
                        height = CapsuleHeight
                    })
                    .SetStandardShaderTransparentColor(0,1,0,0.6)
                    .transform;

            _capsule.position += Vector3.forward*0.9f;
            _capsule.LookAt(Vector3.one*10);
	    }

        void Update()
        {
            _capsule.RotateAround(Vector3.zero, Vector3.up, 90*Time.smoothDeltaTime);

            var moveUp = (CapsuleHeight/2) + CapsuleRadius;

            var p1Wor1 = _capsule.TransformPoint(Vector3.up*moveUp);
            var p2Wor1 = _capsule.TransformPoint(Vector3.up*moveUp + Vector3.forward*(CapsuleRadius*2));


            // test code STARTS here -----------------------------------------------
            var p1Loc = fun.point.ToLocal(p1Wor1, _capsule.rotation, _capsule.position);
            var p2Loc = fun.point.ToLocal(p2Wor1, _capsule.rotation, _capsule.position);

            var p1Wor2 = fun.point.ToWorld(p1Loc+Vector3.right*0.02f, _capsule.rotation, _capsule.position);
            var p2Wor2 = fun.point.ToWorld(p2Loc+Vector3.right*0.02f, _capsule.rotation, _capsule.position);

            // test code ENDS here -------------------------------------------------


            Debug.DrawLine(p1Wor1, p2Wor1, Color.red, 0, false);
            Debug.DrawLine(p1Wor2, p2Wor2, Color.black, 0, false);
        }
    }
}