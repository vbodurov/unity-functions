using Extensions;
using Main;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class TriangleDiskCollision : BaseMainScript
    {
        private Transform _disk;
        private Transform _collision;
        private const float DiskRadius = 0.2f;


        void Start ()
	    {
	        CreateTriangle(0.025f);

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

            _collision = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.03, name = "collision"})
                    .SetStandardShaderTransparentColor(1,0,0,0.9).transform;
	    }

        void Update()
        {
            Vector3 t1, t2, t3, triangleNormal;
            SetTriangle(out t1, out t2, out t3, out triangleNormal);

            var diskNormal = _disk.up;
            var diskCenter = _disk.position;

            // test code STARTS here -----------------------------------------------
            Vector3 collision;
            var hasCollision = 
                fun.intersection.BetweenTriangleAndDisk(
                    ref t1, ref t2, ref t3, 
                    ref diskNormal, ref diskCenter, DiskRadius, 
                    out collision);
            // test code ENDS here -------------------------------------------------

            _collision.position = hasCollision ? collision : new Vector3(0,999,0);
            SetColorOnChanged(hasCollision, rgba(0,1,0,0.7), rgba(0.5,0.5,0.5,0.7), _disk, _a, _b, _c);
        }

    }
}