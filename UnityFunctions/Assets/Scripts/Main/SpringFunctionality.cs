using Extensions;
using Services;
using Unianio;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class SpringFunctionality : BaseMainScript
    {
        IPendulumPhysicsAgent _ppa;
        Transform _soft, _rigid;
        Text _log;

        void Start()
        {
            _soft = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform
                .SetScale(1.2).SetColor(0xFF0000FF).SetHideFlags(HideFlags.HideInHierarchy);

            _rigid = GameObject.CreatePrimitive(PrimitiveType.Cube).transform
                .SetScale(1).SetColor(0x0000FFFF).SetPosition(-2.5, 0, 0);

            _ppa = new PendulumPhysicsAgent(0.20, 3.0);

            _log = GameObject.Find("LogText").GetComponent<Text>();
        }
        void Update()
        {
            fun.frame();
            _soft.position = _ppa.Compute(V3(5,0,0).AsWorldPoint(_rigid));

        }
    }
}