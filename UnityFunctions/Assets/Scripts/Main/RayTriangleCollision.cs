using Extensions;
using Main;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class RayTriangleCollision : BaseMainScript
    {
        private Transform _origin;
        private Transform _intersection;
        void Start ()
        {
            const float pointSize = 0.025f;
            CreateTriangle(pointSize);
	    
            _origin = 
                fun.meshes.CreatePointyCone(new DtCone {height = pointSize*2,bottomRadius = pointSize*2,topRadius = 0.001f})
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
            var intersectsPlane = fun.intersection.BetweenPlaneAndRay(ref planeNormal, ref a, ref rayFw, ref rayOr, out p);
            var isInsideTri = intersectsPlane && fun.intersection.BetweenTriangleAndRay(ref a, ref b, ref c, ref rayFw, ref rayOr);
            // test code ENDS here -------------------------------------------------

            if (intersectsPlane)
            {
                Debug.DrawLine(rayOr,p,Color.green,0,true);
            }
            else
            {
                p = new Vector3(0,999,0);
                Debug.DrawLine(rayOr,rayOr+rayFw*0.2f,Color.black,0,true);
            }

            SetColorOnChanged(isInsideTri,Color.red, Color.gray, _intersection,_origin);

            _intersection.position = p;
        
        }
    }
}