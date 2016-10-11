using Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class LineSegmentDiskCollision : BaseMainScript
    {
        private Transform _disk;
        private Transform _collision;
        private Transform _lineEnd1;
        private Transform _lineEnd2;
        private const float DiskRadius = 0.2f;


        void Start ()
	    {
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
                fun.meshes.CreateSphere(new DtSphere {radius = 0.01, name = "collision"})
                    .SetStandardShaderTransparentColor(1,0,0,0.9).transform;

            _lineEnd1 = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.03, name = "line1"})
                    .SetStandardShaderTransparentColor(0,1,0,0.9).transform;
            _lineEnd1.position += Vector3.forward*-0.5f;
            _lineEnd2 = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.03, name = "line2"})
                    .SetStandardShaderTransparentColor(0,0,1,0.9).transform;
            _lineEnd2.position += Vector3.forward*0.5f;
	    }
        void Update()
        {
            var diskNormal = _disk.up;
            var diskCenter = _disk.position;

            var l1 = _lineEnd1.position;
            var l2 = _lineEnd2.position;

            // test code STARTS here -----------------------------------------------
            Vector3 collision;
            var hasCollision = 
                fun.intersection.BetweenLineSegmentAndDisk(
                    ref l1, ref l2, 
                    ref diskNormal, ref diskCenter, DiskRadius, 
                    out collision);
            // test code ENDS here -------------------------------------------------

            Debug.DrawLine(l1,l2,hasCollision?Color.green:Color.red,0,false);
            _collision.position = hasCollision ? collision : new Vector3(0,999,0);
            SetColorOnChanged(hasCollision, rgba(0,1,0,0.7), rgba(0.5,0.5,0.5,0.7), _disk);
        }
    }
}