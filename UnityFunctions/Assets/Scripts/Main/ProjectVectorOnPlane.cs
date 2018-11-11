using Extensions;
using Main;
using Unianio;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class ProjectVectorOnPlane : BaseMainScript {
        private Transform _point, _projection;

        void Start ()
        {
            const float pointSize = 0.025f;
            var dt = new DtTrianglePlane();
	        fun.meshes.CreateTwoSidedTrianglePlane(dt).SetStandardShaderTransparentColor(0,0,1,0.5);
            _mesh = dt.mesh;
            _b = fun.meshes.CreateSphere(new DtSphere {radius = pointSize, name = "Tri_ver_b"}).SetStandardShaderTransparentColor(0,1,0,0.9).transform;
            _c = fun.meshes.CreateSphere(new DtSphere {radius = pointSize, name = "Tri_ver_c"}).SetStandardShaderTransparentColor(0,1,0,0.9).transform;


            _b.position = Vector3.forward*0.5f;
            _c.position = Vector3.right*0.5f;

	    
            _point = 
                fun.meshes.CreateSphere(new DtSphere {radius = pointSize})
                    .SetStandardShaderTransparentColor(1,1,1,0.9).transform;
            _projection = 
                fun.meshes.CreateSphere(new DtSphere {radius = pointSize})
                    .SetStandardShaderTransparentColor(1,0,0,0.9).transform;

            _point.position = new Vector3(0.5f,0.5f,0.5f);
        }
	
        void Update ()
        {
            var a = Vector3.zero;
            var b = _b.position;
            var c = _c.position;
            Vector3 planeNormal;
            fun.point.GetNormal(in c, in b, in a, out planeNormal);

	        _mesh.vertices = new [] {b,c,a};

            var vector = _point.position;

            // test code STARTS here -----------------------------------------------
            Vector3 projection;
            fun.vector.ProjectOnPlane(in vector, in planeNormal, out projection);
            _projection.position = projection;
            var isAbove = fun.vector.IsAbovePlane(in vector, in planeNormal);
            // test code ENDS here -------------------------------------------------
//
            SetColorOnChanged(isAbove,new Color(0,0,1,0.9f),new Color(1,0,0,0.9f),_projection);
            Debug.DrawLine(Vector3.zero, projection, isAbove ? Color.blue : Color.red, 0, true);
            Debug.DrawLine(vector,projection, Color.black, 0, true);
            Debug.DrawLine(Vector3.zero,planeNormal*0.1f, Color.green, 0, true);
        }
    }
}
