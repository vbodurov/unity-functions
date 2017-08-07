using Extensions;
using UnityEngine;
using UnityFunctions;
using Utils;

namespace Main
{
    public class TwoJoinsInverseKinematics : BaseMainScript
    {
        private Transform _origin,_target,_join1,_join2;

        const double len1 = 0.30;	    
        const double len2 = 0.20;

        const double capRad = 0.01;

        void Start ()
        {
            
            _origin = 
                fun.meshes.CreatePointyCone(new DtCone {height = 0.05,bottomRadius = 0.05,topRadius = 0.001f})
                    .SetStandardShaderTransparentColor(1,0,1,0.5).transform;
            _origin.position += Vector3.forward*-0.5f;

            _join1 = CreateJoin(1,len1,1,0,0);
            _join2 = CreateJoin(2,len2,0,1,0);

            _join1.position = _origin.position;
            _join2.position = _join1.position + _origin.forward * (float)len1;

            _target = fun.meshes.CreateBox(new DtBox {side = 0.05}).transform;
            _target.position += Vector3.forward*0.5f;
        }

        void Update ()
        {
            var tarPo = _target.position;
            var oriPo = _origin.position;
            var oriUp = _origin.up;
            var oriFw = _origin.forward;

            // test code STARTS here -----------------------------------------------
//            var distToPlane = 
//                fun.point.IsBelowPlane(ref tarPo, ref oriFw, ref oriPo) 
//                ? 0 
//                : fun.point.DistanceToPlane(ref tarPo, ref oriFw, ref oriPo);
//
//            var lenAll = (float)(len1+len2+len1);
//            var relDistToPlane = distToPlane / lenAll;
//            Vector3 oriRt;
//            fun.vector.GetNormal(ref oriFw, ref oriUp, out oriRt);
//            Vector3 tarPoOnPlane;
//            fun.point.ProjectOnPlane(ref tarPo, ref oriRt, ref oriPo, out tarPoOnPlane);
//            tarPo = Vector3.Lerp(tarPoOnPlane, tarPo, (float)BezierFunc.GetY(relDistToPlane, 0.20,0.00,0.00,1.00));

            Vector3 join;
            fun.invserseKinematics.TwoJoinsOnVertPlane(oriPo, oriFw, oriUp, tarPo, len1, len2, out join);
            // test code ENDS here -------------------------------------------------

            var toJ0 = (join - oriPo).normalized;
            var toTarg = (tarPo - join).normalized;

            _join1.rotation = Quaternion.LookRotation(toJ0, toJ0.GetRealUp(oriUp, oriFw));
            _join2.rotation = Quaternion.LookRotation(toTarg, toTarg.GetRealUp(_join1.rotation*Vector3.up,_join1.rotation*Vector3.forward));
            _join2.position = join;
        }

        private Transform CreateJoin(int id, double len, double red, double green, double blue)
        {
            var join = 
                fun.meshes.CreateCapsule(new DtCapsule {height = len-capRad*2, name="join_"+id, radius = capRad})
                .SetStandardShaderTransparentColor(red,green,blue,0.5).transform;
            join.gameObject.hideFlags = HideFlags.HideInHierarchy;
            var wrapper = new GameObject(join.name+"_wrapper");
            join.SetParent(wrapper.transform);
            join.transform.localRotation = Quaternion.Euler(90,0,0);
            join.transform.localPosition = new Vector3(0,0,(float)len/2f);
            return wrapper.transform;
        }

        
    }
}