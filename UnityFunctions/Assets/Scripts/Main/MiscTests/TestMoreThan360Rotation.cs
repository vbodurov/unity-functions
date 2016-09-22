using Extensions;
using Unianio.Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main.MiscTests
{
    public struct AngleAxisData
    {
        public float Angle;
        public Vector3 Axis;
        public Quaternion Slerp(Quaternion start, double progress)
        {
            return start*Quaternion.AngleAxis(Angle*(float) progress, Axis);
        }
        public Quaternion Slerp(double progress)
        {
            return Quaternion.AngleAxis(Angle*(float) progress, Axis);
        }
    }
    public class TestMoreThan360Rotation : BaseMainScript
    {
        private Transform _capsule;

        private readonly TimeRange _time = new TimeRange();
        private AngleAxisData _rotation;

        void Start ()
	    {
            _capsule =
                fun.meshes.CreateCapsule(
                    new DtCapsule
                    {
                        name = "capsule",
                        radius = 0.1,
                        height = 1
                    })
                    .SetStandardShaderTransparentColor(0,1,0,0.6)
                    .transform;
            _capsule.LookAt(Vector3.one*10);

            _time.SetTime(3);
            _rotation = new AngleAxisData {Angle = 720, Axis = Vector3.up};

	    }

        void Update()
        {
            var x = _time.Progress().Clamp01();
            var ini = Vector3.one.normalized;

            // test code STARTS here -----------------------------------------------
            ini = _rotation.Slerp(x)*ini;
            _capsule.LookAt(ini*100);
            // test code ENDS here -------------------------------------------------


        }
    }
}