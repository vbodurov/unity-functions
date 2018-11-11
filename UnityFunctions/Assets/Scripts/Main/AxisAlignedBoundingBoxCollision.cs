using Extensions;
using Unianio;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class AxisAlignedBoundingBoxCollision : BaseMainScript
    {
        private Transform _box1, _box2;

        void Start ()
        {


            _box1 = 
                fun.meshes.CreateBox(new DtBox {side = 1, name = "box_1"})
                    .transform;
            _box2 =
                fun.meshes.CreateBox(new DtBox { side = 1, name = "box_2" })
                    .transform;
            _box2.position += Vector3.forward*0.5f;

        }
        void Update()
        {

            var pos1 = _box1.position;
            var size1 = _box1.localScale;
            var pos2 = _box2.position;
            var size2 = _box2.localScale;



            // test code STARTS here -----------------------------------------------
            var hasCollision = fun.intersection.BetweenAxisAlignedBoxes(in pos1, in size1, in pos2, in size2);
            // test code ENDS here -------------------------------------------------


            SetColorOnChanged(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5,0.5,0.5,0.5), _box1);
            SetColorOnChanged(hasCollision, rgba(0, 1, 0, 0.5), rgba(0.5,0.5,0.5,0.5), _box2);
        }

        

        


    }
}