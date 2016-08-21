using Extensions;
using Main;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class ProjectPointOnPlane : BaseMovableTriangle {
        private Transform _point, _projection;

        // Drag the white ball in Scene view
        void Start ()
        {
            const float pointSize = 0.025f;
            CreateTriangle(pointSize);
	    
            _point = 
                fun.meshes.CreateSphere(new DtSphere {radius = pointSize})
                    .SetStandardShaderTransparentColor(1,1,1,0.9).transform;
            _projection = 
                fun.meshes.CreateSphere(new DtSphere {radius = pointSize})
                    .SetStandardShaderTransparentColor(1,0,0,0.9).transform;

            _point.position += Vector3.forward*0.5f;
        }
	
        // Update is called once per frame
        void Update ()
        {
            Vector3 a, b, c, planeNormal;
            SetTriangle(out a, out b, out c, out planeNormal);

            var p = _point.position;

            // test code STARTS here -----------------------------------------------
            _projection.position = fun.point.ProjectOnPlane(p, planeNormal, a);
            var isAbove = fun.point.IsAbovePlane(p, planeNormal, a);
            // test code ENDS here -------------------------------------------------

            SetColorOnChanged(isAbove,new Color(0,1,0,0.9f),new Color(1,0,0,0.9f),_projection);
            Debug.DrawLine(p,_projection.position, isAbove ? Color.red : Color.black, 0, true);
        }
    }
}
