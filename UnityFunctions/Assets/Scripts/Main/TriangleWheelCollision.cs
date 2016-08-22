using Main;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class TriangleWheelCollision : BaseMovableTriangle
    {
        private Transform _wheel;
        private float _wheelRadius;


        void Start ()
	    {
	        const float pointSize = 0.025f;
	        CreateTriangle(pointSize);
            _wheelRadius = 0.2f;

            _wheel =
                fun.meshes.CreateCone(
                    new DtCone
                    {
                        bottomRadius = _wheelRadius,
                        height = 0.001f,
                        topRadius = 0.001f
                    })
                    .transform;
	    }

        void Update()
        {
            Vector3 t1, t2, t3, triangleNormal;
            SetTriangle(out t1, out t2, out t3, out triangleNormal);

            var wheelNormal = _wheel.up;
            var wheelPos = _wheel.position;

            // test code STARTS here -----------------------------------------------
            var hasCollision = fun.intersection.BetweenTriangleAndWheel(ref t1, ref t2, ref t3, ref wheelNormal, ref wheelPos, _wheelRadius);
            // test code ENDS here -------------------------------------------------
            
            SetColorOnChanged(hasCollision, Color.green, Color.gray, _wheel, _a, _b, _c);
        }

    }
}