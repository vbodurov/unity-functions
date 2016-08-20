﻿using Extensions;
using UnityEngine;
using UnityFunctions;

public class RayTriangleCollision : BaseMovableTriangle
{
    private Transform _origin, _intersection;
    private bool _isInsideTri;
	void Start ()
	{
	    const float pointSize = 0.025f;
	    CreateTriangle(pointSize);
	    
        _origin = 
            fun.meshes.CreatePointyCone(new DtCone {height = pointSize,bottomRadius = pointSize,topRadius = 0.001f})
                .SetStandardShaderTransparentColor(1,0,1,0.5).transform;
	    _origin.position += Vector3.forward*-0.5f;

	    _intersection =
	        fun.meshes.CreateSphere(new DtSphere {radius = pointSize, name = "intersection"})
	            .SetStandardShaderTransparentColor(1, 0, 0, 0.9).transform;
	}

    void Update ()
	{
	    Vector3 a, b, c, planeNormal;
        SetTriangle(out a, out b, out c, out planeNormal);

        var rayOr = _origin.position;
        var rayFw = _origin.forward;

        // test code STARTS here -----------------------------------------------
        Vector3 p;
        var hasOnPlane = fun.intersection.BetweenPlaneAndRay(ref planeNormal, ref a, ref rayFw, ref rayOr, out p);
        var isInsideTri = fun.intersection.BetweenTriangleAndRay(ref a, ref b, ref c, ref rayFw, ref rayOr);
        // test code ENDS here -------------------------------------------------

        if (hasOnPlane)
        {
            Debug.DrawLine(rayOr,p,Color.green,0,true);
        }
        else
	    {
	        p = new Vector3(0,999,0);
            Debug.DrawLine(rayOr,rayOr+rayFw*0.2f,Color.black,0,true);
	    }

        if (_isInsideTri != isInsideTri)
        {
            _intersection.gameObject.SetStandardShaderTransparentColor(isInsideTri ? Color.red : Color.gray);
            _isInsideTri = isInsideTri;
        }

        _intersection.position = p;
        
	}
}