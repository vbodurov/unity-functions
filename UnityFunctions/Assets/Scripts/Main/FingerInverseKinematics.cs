using Extensions;
using Unianio;
using UnityEngine;
using UnityFunctions;
using Utils;

namespace Main
{
    public class FingerInverseKinematics : BaseMainScript
    {
        Transform _origin,_target,_join1,_join2,_join3;
        Vector3[] _points;
        float[] _lengths;
        int _i;

        const double len1 = 0.30;	    
        const double len2 = 0.20;
        const double len3 = 0.10;


        void Start ()
        {
            _lengths = new[] { (float)len1, (float)len2, (float)len3 };
            _points = new[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };

            _origin = 
                fun.meshes.CreatePointyCone(new DtCone {height = 0.05,bottomRadius = 0.05,topRadius = 0.001f})
                    .SetStandardShaderTransparentColor(1,0,1,0.5).transform;
            _origin.position += Vector3.forward*-0.5f;

            _join1 = CreateJoin(1, len1, 1, 0, 0);
            _join2 = CreateJoin(2, len2, 0, 1, 0);
            _join3 = CreateJoin(3, len3, 0, 0, 1);

            _join1.position = _origin.position;
            _join2.position = _join1.position + _origin.forward * (float)len1;
            _join3.position = _join2.position + _origin.forward * (float)len2;

            _target = fun.meshes.CreateBox(new DtBox {side = 0.05}).transform;
            //_target.position += Vector3.forward*0.5f;
            _i = 0;
        }

        void Update ()
        {
            ++_i;
            var tarPo = _target.position;
            var oriPo = _origin.position;
            var oriUp = _origin.up;
            var oriFw = _origin.forward;

//            if(_i > 1) return;
            // test code STARTS here -----------------------------------------------
            Vector3 j0, j1, norm;
            fun.inverseKinematics.Finger(oriPo, oriFw, oriUp, tarPo, _points,_lengths, out j0, out j1, out norm);
            // test code ENDS here -------------------------------------------------

            var toJ0 = (j0 - oriPo).normalized;
            var toJ1 = (j1 - j0).normalized;
            var toTarg = (tarPo - j1).normalized;

            _join1.rotation = Quaternion.LookRotation(toJ0, norm);

            _join2.rotation = Quaternion.LookRotation(toJ1, toJ1.GetRealUp(_join1.rotation * Vector3.up, _join1.rotation * Vector3.forward));
            _join2.position = j0;

            _join3.rotation = Quaternion.LookRotation(toTarg, toTarg.GetRealUp(_join2.rotation * Vector3.up, _join2.rotation * Vector3.forward));
            _join3.position = j1;
        }

        private Transform CreateJoin(int id, double len, double red, double green, double blue)
        {
            var join = 
                fun.meshes.CreatePointyCone(new DtCone { height = len, bottomRadius = 0.02, topRadius = 0.001f, relNoseLen = 2 })
                .SetStandardShaderTransparentColor(red,green,blue,0.5).transform;
            join.gameObject.hideFlags = HideFlags.HideInHierarchy;
            var wrapper = new GameObject(join.name+"_wrapper");
            join.SetParent(wrapper.transform);
            join.transform.localRotation = Quaternion.Euler(90,0,0);
            join.transform.localPosition = new Vector3(0,0,0);
            return wrapper.transform;
        }

        
    }
}