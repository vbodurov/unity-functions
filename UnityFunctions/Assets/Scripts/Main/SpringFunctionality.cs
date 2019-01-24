using Extensions;
using Services;
using Unianio;
using UnityEngine;
using UnityEngine.UI;
using UnityFunctions;

namespace Main
{
    public class SpringFunctionality : BaseMainScript
    {
        IPendulumPhysicsAgent _ppa;
        Transform _soft, _rigid;
        Text _log;

        void Start()
        {
//            fun.setTimeScale(0.2);

            _soft = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform
                .SetScale(1.2).SetColor(0xFF0000FF).SetHideFlags(HideFlags.HideInHierarchy);

            _rigid = GameObject.CreatePrimitive(PrimitiveType.Cube).transform
                .SetScale(1).SetColor(0x0000FFFF).SetPosition(-2.5, 0, 0);

            _ppa = new PendulumPhysicsAgent();

            _log = GameObject.Find("LogText").GetComponent<Text>();
        }
        void Update()
        {
            fun.frame();
            var p = V3(5, 0, 0).AsWorldPoint(_rigid);
            Debug.DrawLine(p, _rigid.position, Color.yellow, 0, false);
            _soft.position = _ppa.Compute(p);

        }
    }
}