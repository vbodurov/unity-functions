using Main;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class TriangleTriangleCollision : BaseMainScript
    {
        private Transform _t1p1,_t1p2,_t1p3;
        private Transform _t2p1,_t2p2,_t2p3;
        private Mesh _mesh1, _mesh2;
        private Transform _t1, _t2;

        void Start ()
	    {
	        const float pointSize = 0.025f;
	        _t1 = CreateTriangle(pointSize, out _t1p1, out _t1p2, out _t1p3, out _mesh1).transform;
            _t2 = CreateTriangle(pointSize, out _t2p1, out _t2p2, out _t2p3, out _mesh2).transform;

            _t2p1.position += Vector3.forward*0.5f;
            _t2p2.position += Vector3.forward*0.5f;
            _t2p3.position += Vector3.forward*0.5f;
	    }

        void Update()
        {
            Vector3 t1p1,t1p2,t1p3,planeNormal1;
            SetTriangle(_t1p1, _t1p2, _t1p3, _mesh1, out t1p1 , out t1p2, out t1p3, out planeNormal1);
            Vector3 t2p1,t2p2,t2p3,planeNormal2;
            SetTriangle(_t2p1, _t2p2, _t2p3, _mesh2, out t2p1 , out t2p2, out t2p3, out planeNormal2);

            // test code STARTS here -----------------------------------------------
            var hasIntersection =
                fun.triangle.Overlap(
                    _t1p1.position, _t1p2.position, _t1p3.position,
                    _t2p1.position, _t2p2.position, _t2p3.position);
            // test code ENDS here -------------------------------------------------

            SetColorOnChanged(hasIntersection, Color.green, Color.grey, _t1p1, _t1p2, _t1p3, _t2p1, _t2p2, _t2p3);
            SetColorOnChanged(hasIntersection, new Color(0,0,1,0.5f), new Color(0.7f,0.8f,1f,0.5f), _t1,_t2);
        }
    }
}