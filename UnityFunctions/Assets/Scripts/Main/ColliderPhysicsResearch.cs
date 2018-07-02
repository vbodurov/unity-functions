using Extensions;
using Unianio;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class ColliderPhysicsResearch : BaseMainScript
    {
        private Transform _test, _cap1, _cap2, _cap3, _sph1;
        const float TestSphereRadius = 0.2f;

        void Start ()
        {
            _cap1 = CreateCapsule("cap1", 0.5, 0.10, 0, 0, 1, Vector3.zero);
            _cap2 = CreateCapsule("cap2", 1.0, 0.25, 0, 1, 0, Vector3.forward*0.5f);
            _cap3 = CreateCapsule("cap3", 0.2, 0.50, 1, 0, 0, Vector3.forward*1.5f);
            _sph1 = CreateSphere("sph1", 0.2, 1, 0, 1, Vector3.right);
            _test = fun.meshes.CreateSphere(new DtSphere {radius = TestSphereRadius, name = name})
                .SetStandardShaderTransparentColor(0,0,0,0.5)
                .transform;
            _test.position = -Vector3.right;
        }
        void Update ()
        {
            if(IsShiftKeyAndNumberDown(1)) RunTest();
        }
        private void RunTest()
        {
            var colliders = Physics.OverlapSphere(_test.position, TestSphereRadius);
            foreach (var c in colliders)
            {
                var closest = c.ClosestToSurfacePoint(_test.position);
                
                Debug.DrawLine(closest, _test.position, Color.magenta, 5, false);
                Debug.Log(c.name+"|"+closest.s()+"|"+fun.distance.Between(_test.position,closest).Round(2));
            }

        }


        private static Transform CreateCapsule(string name, double height, double radius, double r, double g, double b, Vector3 position)
        {
            var cap = fun.meshes.CreateCapsule(new DtCapsule {height = height, radius = radius, name = name})
                .SetStandardShaderTransparentColor(r,g,b,0.5)
                .transform;
            var cc = cap.gameObject.AddComponent<CapsuleCollider>();
            cc.height = (float)(height+radius*2);
            cc.radius = (float)radius;
            
            cap.position = position;
            return cap;
        }
        private static Transform CreateSphere(string name, double radius, double r, double g, double b, Vector3 position)
        {
            var cap = fun.meshes.CreateSphere(new DtSphere {radius = radius, name = name})
                .SetStandardShaderTransparentColor(r,g,b,0.5)
                .transform;
            var cc = cap.gameObject.AddComponent<SphereCollider>();
            cc.radius = (float)radius;
            
            cap.position = position;
            return cap;
        }

    }
}