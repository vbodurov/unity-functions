using Extensions;
using Unianio;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class DiskSphereCollision : BaseMainScript
    {
        private const float DiskRadius = 0.3f;
        private const float SphereRadius = 0.8f;
        private Transform _disk;
        private Transform _sphere;
        private Transform _collision;

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
            _sphere =
                fun.meshes.CreateSphere(
                    new DtSphere
                    {
                        name = "Sphere",
                        radius = SphereRadius
                    })
                    .transform;
            _sphere.position += Vector3.forward*0.9f;

            _collision = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.025,name = "collision"})
                    .SetStandardShaderTransparentColor(1,0,0,0.9).transform;
	    }

        void Update()
        {
            var diskCenter = _disk.position;
            var diskPlaneNormal = _disk.up;
            var sphereCenter = _sphere.position;
            // test code STARTS here -----------------------------------------------
            Vector3 collision;
            var hasCollision = 
                fun.intersection.BetweenDiskAndSphere(
                    ref diskPlaneNormal, ref diskCenter, DiskRadius, ref sphereCenter, SphereRadius, out collision);
            // test code ENDS here -------------------------------------------------

            _collision.position = hasCollision ? collision : new Vector3(0,999,0);

            SetColorOnChanged(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5,0.5,0.5,0.5), _disk);
            SetColorOnChanged(hasCollision, rgba(0, 0, 1, 0.5), rgba(0.5,0.5,0.5,0.5), _sphere);
        }
    }
}