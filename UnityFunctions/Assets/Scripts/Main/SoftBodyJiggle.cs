using Extensions;
using Services;
using Unianio;
using UnityEngine;
using UnityEngine.UI;
using UnityFunctions;

namespace Main
{
    public class SoftBodyJiggle : BaseMainScript
    {
        ISoftBodyJiggleAgent _sja;
        Transform _bone, _rigid;
        Text _log;

        void Start()
        {
            //            fun.setTimeScale(0.2);

//            Application.targetFrameRate = 90;
//            QualitySettings.vSyncCount = 0;


            var cone = fun.meshes.CreatePointyCone(
                    new DtCone
                    {
                        height = 0.5,
                        topRadius = 0.001,
                        bottomRadius = 0.5,
                        relNoseLen = 5
                    }).transform
                .SetColor(0xFF0000FF)
//                .SetHideFlags(HideFlags.HideInHierarchy)
                ;

            _bone = new GameObject("Soft").transform.SetPosition(-3, 0, 0);
            cone.SetParent(_bone);
            cone.localRotation = Quaternion.LookRotation(v3.bk, v3.up);
            cone.localPosition = V3(0, 0, 3);
            _bone.rotation = Quaternion.LookRotation(v3.rt, v3.up);

            _rigid = GameObject.CreatePrimitive(PrimitiveType.Cube).transform
                .SetColor(0x0000FFFF).SetPosition(-4, 0, 0);

            _bone.SetParent(_rigid);

            _sja = new SoftBodyJiggleAgent(new SoftBodyConfig
            {
                Bone = _bone,
                RelTargetAt = 3,
                MaxDegrees = 50
            });

            _log = GameObject.Find("LogText").GetComponent<Text>();
        }
        void Update()
        {
            fun.frame();

            var r = _sja.Compute();

            _bone.position = r.position;
            _bone.rotation = r.rotation;
            
        }
    }
}