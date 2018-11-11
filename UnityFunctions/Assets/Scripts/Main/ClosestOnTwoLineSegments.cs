using Extensions;
using Unianio;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class ClosestOnTwoLineSegments : BaseMainScript
    {
        private Transform _collisionA;
        private Transform _collisionB;
        private Transform _lineEndA1;
        private Transform _lineEndA2;
        private Transform _lineEndB1;
        private Transform _lineEndB2;


        void Start ()
	    {

            _lineEndA1 = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.03, name = "lineA1"})
                    .SetStandardShaderTransparentColor(0,1,0,0.9).transform;
            _lineEndA1.position = Vector3.forward*-0.5f;
            _lineEndA2 = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.03, name = "lineA2"})
                    .SetStandardShaderTransparentColor(0,1,0,0.9).transform;
            _lineEndA2.position = Vector3.forward*0.5f;

            _lineEndB1 = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.03, name = "lineB1"})
                    .SetStandardShaderTransparentColor(0,0,1,0.9).transform;
            _lineEndB1.position = Vector3.forward*-0.5f + Vector3.right*0.2f;
            _lineEndB2 = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.03, name = "lineB2"})
                    .SetStandardShaderTransparentColor(0,0,1,0.9).transform;
            _lineEndB2.position = Vector3.forward*0.5f + Vector3.right*0.2f;


            _collisionA = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.05, name = "collisionA"})
                    .SetStandardShaderTransparentColor(1,0,0,0.3).transform;
            _collisionB = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.05, name = "collisionB"})
                    .SetStandardShaderTransparentColor(1,1,0,0.3).transform;

            _collisionA.position = _collisionB.position = Vector3.one*1000;
	    }
        void Update()
        {
            var lA1 = _lineEndA1.position;
            var lA2 = _lineEndA2.position;

            var lB1 = _lineEndB1.position;
            var lB2 = _lineEndB2.position;

            // test code STARTS here -----------------------------------------------
            Vector3 cpA,cpB;
            fun.point.ClosestOnTwoLineSegments(in lA1, in lA2, in lB1, in lB2, out cpA, out cpB);
            // test code ENDS here -------------------------------------------------
            Debug.DrawLine(lA1,lA2,Color.green,0,false);
            Debug.DrawLine(lB1,lB2,Color.blue,0,false);

            Debug.DrawLine(cpA,cpB,Color.black,0,false);
            _collisionA.position = cpA;
            _collisionB.position = cpB; 
        }
    }
}