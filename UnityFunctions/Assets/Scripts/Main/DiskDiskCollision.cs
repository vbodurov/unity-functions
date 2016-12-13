using Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class DiskDiskCollision : BaseMainScript
    {
        private const float Disk1Radius = 0.3f;
        private const float Disk2Radius = 0.8f;
        private Transform _disk1;
        private Transform _disk2;
        private Transform _collision;

        void Start ()
	    {
            _disk1 =
                fun.meshes.CreateCone(
                    new DtCone
                    {
                        name = "Disk1",
                        bottomRadius = Disk1Radius,
                        height = 0.001f,
                        topRadius = 0.001f
                    })
                    .transform;
            _disk2 =
                fun.meshes.CreateCone(
                    new DtCone
                    {
                        name = "Disk2",
                        bottomRadius = Disk2Radius,
                        height = 0.001f,
                        topRadius = 0.001f
                    })
                    .transform;
            _disk2.position += Vector3.forward*0.9f;

            _collision = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.025,name = "collision"})
                    .SetStandardShaderTransparentColor(1,0,0,0.9).transform;
	    }

        void Update()
        {
            var disk1Center = _disk1.position;
            var disk1Up = _disk1.up;
            var disk2Center = _disk2.position;
            var disk2Up = _disk2.up;
            // test code STARTS here -----------------------------------------------
            Vector3 collision;
            var hasCollision = 
                fun.intersection.BetweenDiskAndDisk(
                    ref disk1Up, ref disk1Center, Disk1Radius, ref disk2Up, ref disk2Center, Disk2Radius, out collision);
            // test code ENDS here -------------------------------------------------

            _collision.position = hasCollision ? collision : new Vector3(0,999,0);

            SetColorOnChanged(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5,0.5,0.5,0.5), _disk1);
            SetColorOnChanged(hasCollision, rgba(0, 0, 1, 0.5), rgba(0.5,0.5,0.5,0.5), _disk2);
        }

        
    }
}