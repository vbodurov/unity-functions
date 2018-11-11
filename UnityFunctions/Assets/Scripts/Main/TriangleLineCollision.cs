using Extensions;
using Main;
using Unianio;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class TriangleLineCollision : BaseMainScript
    {
        private Transform _t, _line1, _line2;


        void Start ()
	    {
	        const float pointSize = 0.025f;
	        _t = CreateTriangle(pointSize).transform;

            _line1 = fun.meshes.CreateSphere(new DtSphere {radius = pointSize}).SetStandardShaderTransparentColor(0,1,0,0.9).transform;
            _line2 = fun.meshes.CreateSphere(new DtSphere {radius = pointSize}).SetStandardShaderTransparentColor(0,1,0,0.9).transform;

            _line1.position += Vector3.forward*-0.3f;
            _line2.position += Vector3.forward*0.3f;
	    }

        void Update()
        {
            Vector3 tp1,tp2,tp3;
            SetTriangle(out tp1 , out tp2, out tp3);
            var line1 = _line1.position;
            var line2 = _line2.position;

            // test code STARTS here -----------------------------------------------
            var hasIntersection =
                fun.intersection.BetweenTriangleAndLineSegment(in tp1, in tp2, in tp3, in line1, in line2);
            // test code ENDS here -------------------------------------------------

            Debug.DrawLine(line1, line2, hasIntersection ? Color.green : Color.red, 0, true);

            SetColorOnChanged(hasIntersection, Color.green, Color.grey, _a, _b, _c, _line1, _line2);
            SetColorOnChanged(hasIntersection, new Color(0,0,1,0.5f), new Color(0.7f,0.8f,1f,0.5f), _t.transform);
        }

        
    }
}