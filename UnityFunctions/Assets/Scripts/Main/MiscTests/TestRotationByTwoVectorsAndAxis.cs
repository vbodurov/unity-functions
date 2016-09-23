using Extensions;
using Unianio.Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main.MiscTests
{
    public class TestRotationByTwoVectorsAndAxis : BaseMainScript
    {
        private const float seconds = 2.5f;
        private readonly Vector3 _from = new Vector3(1,0,0);
        private readonly Vector3 _to = new Vector3(0,1,0);
        private readonly Vector3 _axis = new Vector3(1,1,0).normalized;
        private readonly TimeRange _time = new TimeRange();
        private AngleAxisData _rotation;

        void Start ()
	    {


            _time.SetTime(seconds);
            _rotation = new AngleAxisData (_from, _to, _axis);
            Debug.Log(_rotation.Angle);
	    }

        void FixedUpdate()
        {
            var x = _time.Progress() % 1;
dbg.log(x);
            var ini = _from;

            // test code STARTS here -----------------------------------------------
            var curr = _rotation.Slerp(x)*ini;
            
            // test code ENDS here -------------------------------------------------

            Debug.DrawLine(Vector3.zero, curr, Color.green, seconds/2f);

            Debug.DrawLine(Vector3.zero, _axis, Color.black, 0);
            Debug.DrawLine(Vector3.zero, _rotation.Axis, Color.white, 0);
            Debug.DrawLine(Vector3.zero, _from, Color.red, 0);
            Debug.DrawLine(Vector3.zero, _to, Color.blue, 0);

        }
    }
}