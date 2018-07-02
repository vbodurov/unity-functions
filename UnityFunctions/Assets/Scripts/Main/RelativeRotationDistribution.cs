using Extensions;
using Unianio;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class RelativeRotationDistribution : BaseMainScript
    {
        private Transform _arm,_wrist,_finger1,_finger2,_finger3, _control;
        private Quaternion _origLocRotFin1,_origLocRotFin2,_origLocRotFin3;

        void Start ()
        {
            _arm = CreateJoin(9,0,0,1);
            _arm.position = Vector3.zero;
            _wrist = CreateJoin(2,0,1,0);
            _wrist.position = V3(0,0,1);
            _wrist.SetParent(_arm);
            _finger1 = CreateJoin(2,1,0,0);
            _finger1.position = V3(0,0,1.2);
            _finger1.rotation = Quaternion.LookRotation(Vector3.Slerp(Vector3.forward, Vector3.right, 0.5f), Vector3.up);
            _finger1.SetParent(_wrist);
            _finger2 = CreateJoin(2,1,0,0);
            _finger2.position = V3(0,0,1.2);
            _finger2.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            _finger2.SetParent(_wrist);
            _finger3 = CreateJoin(2,1,0,0);
            _finger3.position = V3(0,0,1.2);
            _finger3.rotation = Quaternion.LookRotation(Vector3.Slerp(Vector3.forward, Vector3.left, 0.5f), Vector3.up);
            _finger3.SetParent(_wrist);

            _origLocRotFin1 = _finger1.localRotation;
            _origLocRotFin2 = _finger2.localRotation;
            _origLocRotFin3 = _finger3.localRotation;

            _control = 
                fun.meshes.CreateBox(new DtBox {side = 0.1})
                .SetStandardShaderTransparentColor(1,1,1,0.5).transform;
            _control.position = _finger2.position;
            _control.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            _control.SetParent(_arm);
        }

        void Update ()
        {
            var controlFw = _control.forward;
            var controlUp = _control.up;
            
            var targetRot = Quaternion.LookRotation(controlFw, controlUp);
            var halfRot = Quaternion.Slerp(_arm.rotation, targetRot, 0.5f);
            _wrist.rotation = halfRot;

            var restOfRot =  
                (Quaternion.Inverse(_wrist.rotation) * Quaternion.LookRotation(controlFw, controlUp));
            
            _finger1.localRotation = restOfRot * _origLocRotFin1;
            _finger2.localRotation = restOfRot * _origLocRotFin2;
            _finger3.localRotation = restOfRot * _origLocRotFin3;
//            _finger1.localRotation = _origLocRotFin1 * relRot;
//            _finger2.localRotation = _origLocRotFin2 * relRot;
//            _finger3.localRotation = _origLocRotFin3 * relRot;

        }

        private Transform CreateJoin(double noseLen, double red, double green, double blue)
        {
            var join =
                fun.meshes.CreatePointyCone(new DtCone {height = 0.1,bottomRadius = 0.1,topRadius = 0.001f, relNoseLen = noseLen})
                    .SetStandardShaderTransparentColor(red,green,blue,0.75).transform;
            join.hideFlags = HideFlags.HideInHierarchy | HideFlags.NotEditable;
            return join;
        }

        
    }
}