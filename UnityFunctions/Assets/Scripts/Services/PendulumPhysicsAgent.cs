using Extensions;
using Unianio;
using UnityEngine;
using static Unianio.fun;

namespace Services
{
    internal interface IPendulumPhysicsAgent
    {
        Vector3 Compute(in Vector3 currRigidPosition);
    }
    internal class PendulumPhysicsAgent : IPendulumPhysicsAgent
    {
        readonly float _drag, _catchUp;
        Vector3 _prevSoftPosition, _force;

        internal PendulumPhysicsAgent(double drag = 0.25, double catchUp = 1)
        {
            _drag = (float)drag;
            _catchUp = (float)catchUp;
            _prevSoftPosition = v3.nan;
        }
        Vector3 IPendulumPhysicsAgent.Compute(in Vector3 currRigidPosition)
        {
            var nextSoftPosition = currRigidPosition;
            if (!float.IsNaN(_prevSoftPosition.x))
            {
                nextSoftPosition = lerp(in _prevSoftPosition, in currRigidPosition, fun.smoothDeltaTime / _drag);

                var moveBy = currRigidPosition - _prevSoftPosition;

                _force = lerp(in _force, in moveBy, fun.smoothDeltaTime * _catchUp);

            }
            return _prevSoftPosition = nextSoftPosition + _force;
        }
    }
}