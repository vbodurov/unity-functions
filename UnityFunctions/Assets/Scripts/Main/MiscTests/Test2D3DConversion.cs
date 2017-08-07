using Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main.MiscTests
{
    public class Test2D3DConversion : BaseMainScript
    {
        private Transform _point1, _point2, _point3;

        // Drag the white ball in Scene view
        void Start ()
        {
            const float pointSize = 0.025f;
            CreateTriangle(pointSize);
	    
            _point1 = 
                fun.meshes.CreateSphere(new DtSphere {radius = pointSize})
                    .SetStandardShaderTransparentColor(1,0,0,0.9).transform;
            _point2 = 
                fun.meshes.CreateSphere(new DtSphere {radius = pointSize})
                    .SetStandardShaderTransparentColor(0,1,0,0.9).transform;
            _point3 = 
                fun.meshes.CreateSphere(new DtSphere {radius = pointSize})
                    .SetStandardShaderTransparentColor(0,0,1,0.9).transform;


            _point1.position += Vector3.forward*0.5f;
            _point2.position += Vector3.forward*0.5f+Vector3.up*0.2f;
            _point3.position += Vector3.forward*0.5f+Vector3.up*0.2f+Vector3.right*0.2f;
        }
	
        // Update is called once per frame
        void Update ()
        {
            Vector3 a, b, c, planeNormal;
            SetTriangle(out a, out b, out c, out planeNormal);

            var p1 = _point1.position;
            var p2 = _point2.position;
            var p3 = _point3.position;

            // test code STARTS here -----------------------------------------------
            Vector3 xAxis2d,yAxis2d;
            fun.vector.ComputeRandomXYAxesForPlane(ref planeNormal, out xAxis2d, out yAxis2d);

            // we take point "a" to be the origin 
            var p1in2d = p1.As2d(ref a, ref xAxis2d, ref yAxis2d);
            var p2in2d = p2.As2d(ref a, ref xAxis2d, ref yAxis2d);
            var p3in2d = p3.As2d(ref a, ref xAxis2d, ref yAxis2d);

            var p1Proj = p1in2d.As3d(ref a, ref xAxis2d, ref yAxis2d);
            var p2Proj = p2in2d.As3d(ref a, ref xAxis2d, ref yAxis2d);
            var p3Proj = p3in2d.As3d(ref a, ref xAxis2d, ref yAxis2d);


            // test code ENDS here -------------------------------------------------

            Debug.DrawLine(p1in2d,p2in2d,Color.black,0,false);
            Debug.DrawLine(p2in2d,p3in2d,Color.black,0,false);
            Debug.DrawLine(p3in2d,p1in2d,Color.black,0,false);

            Debug.DrawLine(p1,p1Proj,Color.red,0,false);
            Debug.DrawLine(p2,p2Proj,Color.green,0,false);
            Debug.DrawLine(p3,p3Proj,Color.blue,0,false);
        }
    }
}