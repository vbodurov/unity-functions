using Extensions;
using UnityEngine;
using UnityFunctions;

public class RayTriangleCollision : MonoBehaviour
{
    private Mesh _mesh;
    private GameObject _a, _b,_c;
    private GameObject _origin, _next, _intersection;

    // Drag the white ball in Scene view
	void Start ()
	{
	    const float pointSize = 0.025f;
	    var dt = new DtTrianglePlane();
	    fun.meshes.CreateTwoSidedTrianglePlane(dt).SetStandardShaderTransparentColor(0,0,1,0.5);

	    _a = fun.meshes.CreateSphere(new DtSphere {radius = pointSize}).SetStandardShaderTransparentColor(0,1,0,0.9);
        _b = fun.meshes.CreateSphere(new DtSphere {radius = pointSize}).SetStandardShaderTransparentColor(0,1,0,0.9);
        _c = fun.meshes.CreateSphere(new DtSphere {radius = pointSize}).SetStandardShaderTransparentColor(0,1,0,0.9);
        _mesh = dt.mesh;

        _a.transform.position = _mesh.vertices[0];
        _b.transform.position = _mesh.vertices[1];
        _c.transform.position = _mesh.vertices[2];
	    
        _origin = 
            fun.meshes.CreateSphere(new DtSphere {radius = pointSize}).SetStandardShaderTransparentColor(1,1,1,0.9);
        _next = 
            fun.meshes.CreateSphere(new DtSphere {radius = pointSize}).SetStandardShaderTransparentColor(1,0,0,0.9);
	}

    void Update ()
	{
	    var a = _a.transform.position;
        var b = _b.transform.position;
        var c = _c.transform.position;

	    _mesh.vertices = new [] {a,b,c};

	    var norm = fun.point.GetNormal(a, b, c);
//	    _projection.transform.position = fun.point.ProjectOnPlane(_point.transform.position, norm, a);
//	    var isAbove = fun.point.IsAbovePlane(_point.transform.position, norm, a);
//        Debug.DrawLine(_point.transform.position,_projection.transform.position, isAbove ? Color.red : Color.black,0,true);
	}
}