using Main;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class TriangleDiskCollision : BaseMainScript
    {
        private Transform _disk;
        private const float DiskRadius = 0.2f;


        void Start ()
	    {
	        const float pointSize = 0.025f;
	        CreateTriangle(pointSize);

            _disk =
                fun.meshes.CreateCone(
                    new DtCone
                    {
                        name = "Disk",
                        bottomRadius = DiskRadius,
                        height = 0.001f,
                        topRadius = 0.001f
                    })
                    .transform;
	    }

        void Update()
        {
            Vector3 t1, t2, t3, triangleNormal;
            SetTriangle(out t1, out t2, out t3, out triangleNormal);

            var diskNormal = _disk.up;
            var diskCenter = _disk.position;

            // test code STARTS here -----------------------------------------------
            var hasCollision = fun.intersection.BetweenTriangleAndDisk(ref t1, ref t2, ref t3, ref diskNormal, ref diskCenter, DiskRadius);
            // test code ENDS here -------------------------------------------------
            
            SetColorOnChanged(hasCollision, rgba(0,1,0,0.7), rgba(0.5,0.5,0.5,0.7), _disk, _a, _b, _c);
        }

    }
}