using Extensions;
using UnityEngine;

namespace UnityFunctions
{
    public abstract class BaseMovableTriangle : MonoBehaviour
    {
        protected Transform _a, _b,_c;
        protected Mesh _mesh;

        protected void CreateTriangle(double pointSize)
        {
            var dt = new DtTrianglePlane();
	        fun.meshes.CreateTwoSidedTrianglePlane(dt).SetStandardShaderTransparentColor(0,0,1,0.5);

	        _a = fun.meshes.CreateSphere(new DtSphere {radius = pointSize}).SetStandardShaderTransparentColor(0,1,0,0.9).transform;
            _b = fun.meshes.CreateSphere(new DtSphere {radius = pointSize}).SetStandardShaderTransparentColor(0,1,0,0.9).transform;
            _c = fun.meshes.CreateSphere(new DtSphere {radius = pointSize}).SetStandardShaderTransparentColor(0,1,0,0.9).transform;
            _mesh = dt.mesh;

            _a.position = _mesh.vertices[0];
            _b.position = _mesh.vertices[1];
            _c.position = _mesh.vertices[2];
        }

        protected void SetTriangle(out Vector3 a, out Vector3 b, out Vector3 c)
        {
            a = _a.position;
            b = _b.position;
            c = _c.position;

	        _mesh.vertices = new [] {a,b,c};
        }

        protected void SetTriangle(out Vector3 a, out Vector3 b, out Vector3 c, out Vector3 planeNormal)
        {
            SetTriangle(out a, out b, out c);
            planeNormal = fun.point.GetNormal(a, b, c);
        }
    }
}