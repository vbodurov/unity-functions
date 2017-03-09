using Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class LineSegmentConeCollision : BaseMainScript
    {
        private Transform _cone, _collision, _lineEnd1, _lineEnd2;
        private const float ConeRadius = 0.3f;
        private const float ConeHeight = 0.8f;

        void Start ()
	    {
            _lineEnd1 = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.03, name = "line1"})
                    .SetStandardShaderTransparentColor(0,1,0,0.9).transform;
            _lineEnd1.position += Vector3.forward*-0.5f + Vector3.up*0.5f;
            _lineEnd2 = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.03, name = "line2"})
                    .SetStandardShaderTransparentColor(0,0,1,0.9).transform;
            _lineEnd2.position += Vector3.forward*0.5f + Vector3.up*0.5f;


            _cone =
                fun.meshes.CreateCone(
                    new DtCone
                    {
                        name = "Cone",
                        bottomRadius = ConeRadius,
                        height = ConeHeight,
                        topRadius = 0.001f
                    })
                    .transform;

            _collision = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.025,name = "collision"})
                    .SetStandardShaderTransparentColor(1,0,0,0.9).transform;


//_lineEnd1.position  = V3(-0.03,0.5,-0.029);
//_lineEnd2.position  = V3(0,1.187,0.016);
_lineEnd1.position  = V3(-0.03,0.762,-0.248);
_lineEnd2.position  = V3(0,0.134,0.016);
	    }
        void Update()
        {
//if(_test) return;
//_test = true;
            var coneBase = _cone.position;
            var coneUp = _cone.up;
            var l1 = _lineEnd1.position;
            var l2 = _lineEnd2.position;
            // test code STARTS here -----------------------------------------------
            
            Vector3 collision;
            var hasCollision = 
                fun.intersection.BetweenLineSegmentAndCone(
                    ref l1, ref l2, ref coneBase, ref coneUp, ConeRadius, ConeHeight, out collision);
            // test code ENDS here -------------------------------------------------
            if (!hasCollision) collision = Vector3.one*9999;

            _collision.position = collision;

            Debug.DrawLine(l1,l2,hasCollision?Color.green:Color.red,0,false);

            SetColorOnChanged(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5,0.5,0.5,0.5), _cone);
        }

//private bool _test;
    }
}