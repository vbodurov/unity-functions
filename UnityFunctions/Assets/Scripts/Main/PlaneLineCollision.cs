using Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class PlaneLineCollision : BaseMainScript
    {
        private Transform _pointA, _pointB, _projection;

        void Start ()
        {
            const float pointSize = 0.025f;
            var dt = new DtTrianglePlane();
	        fun.meshes.CreateTwoSidedTrianglePlane(dt).SetStandardShaderTransparentColor(0,0,1,0.5);
            _mesh = dt.mesh;
            _a = fun.meshes.CreateSphere(new DtSphere {radius = pointSize, name = "Tri_ver_a"}).SetStandardShaderTransparentColor(0,1,0,0.9).transform;
            _b = fun.meshes.CreateSphere(new DtSphere {radius = pointSize, name = "Tri_ver_b"}).SetStandardShaderTransparentColor(0,1,0,0.9).transform;
            _c = fun.meshes.CreateSphere(new DtSphere {radius = pointSize, name = "Tri_ver_c"}).SetStandardShaderTransparentColor(0,1,0,0.9).transform;


            _b.position = Vector3.forward*0.5f;
            _c.position = Vector3.right*0.5f;

	    
            _pointA = 
                fun.meshes.CreateSphere(new DtSphere {radius = pointSize})
                    .SetStandardShaderTransparentColor(1,1,0,0.9).transform;
            _pointB = 
                fun.meshes.CreateSphere(new DtSphere {radius = pointSize})
                    .SetStandardShaderTransparentColor(1,1,0,0.9).transform;
            _projection = 
                fun.meshes.CreateSphere(new DtSphere {radius = pointSize})
                    .SetStandardShaderTransparentColor(1,0,0,0.9).transform;

            _pointA.position = new Vector3(0.5f,0.5f,0.5f);
            _pointB.position = new Vector3(0.25f,0.25f,0.25f);
        }
	
        void Update ()
        {
            var a = Vector3.zero;
            var b = _b.position;
            var c = _c.position;
            Vector3 planeNormal;
            fun.point.GetNormal(ref c, ref b, ref a, out planeNormal);

	        _mesh.vertices = new [] {b,c,a};

            var line1 = _pointA.position;
            var line2 = _pointB.position;
            // test code STARTS here -----------------------------------------------
            Vector3 collisionPoint;
            var found = fun.intersection.BetweenPlaneAndLine(ref planeNormal, ref a,
                ref line1, ref line2, out collisionPoint);

            // test code ENDS here -------------------------------------------------
            _projection.position = found ? collisionPoint : V3(0, 10000, 0);
            SetColorOnChanged(found,new Color(0,1,0,0.9f),new Color(1,0,0,0.9f),_a,_b,_c);
            Debug.DrawLine(line1,line2,Color.yellow);
            Debug.DrawLine(line1,collisionPoint,Color.yellow);
        }
    }
}