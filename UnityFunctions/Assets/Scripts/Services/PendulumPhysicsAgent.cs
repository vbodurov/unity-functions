using System;
using Extensions;
using Unianio;
using UnityEngine;
using static Unianio.fun;

namespace Services
{
    internal delegate bool ApplyConstrain(ref Vector3 dynamicPos, ref Vector3 velocity, in Vector3 force);
    internal interface IPendulumPhysicsAgent
    {
        Vector3 Compute(in Vector3 currRigidPosition, double gravity01 = 0, ApplyConstrain constrain = null);
    }
    internal class PendulumPhysicsAgent : IPendulumPhysicsAgent
    {
        Vector3 _force, _acc, _vel, _dynamicPos;
        readonly float _stiffness, _mass, _damping, _gravity;

        internal PendulumPhysicsAgent(
            double stiffness = 0.1,
            double mass = 0.9,
            double damping = 0.75,
            double gravity = 0.75)
        {
            _stiffness = (float)stiffness;
            _mass = (float)mass;
            _damping = (float)damping;
            _gravity = (float)gravity;
        }


        Vector3 IPendulumPhysicsAgent.Compute(in Vector3 targetPos, double gravity01, ApplyConstrain constrain)
        {
            gravity01 = clamp01(gravity01);
            _force.x = (targetPos.x - _dynamicPos.x) * _stiffness;
            _acc.x = _force.x / _mass;
            _vel.x += _acc.x * (1 - _damping);

            _force.y = (targetPos.y - _dynamicPos.y) * _stiffness;
            _force.y -= (_gravity * (float)gravity01) / 10; // Add some gravity
            _acc.y = _force.y / _mass;
            _vel.y += _acc.y * (1 - _damping);

            _force.z = (targetPos.z - _dynamicPos.z) * _stiffness;
            _acc.z = _force.z / _mass;
            _vel.z += _acc.z * (1 - _damping);

            var moveBy = _vel + _force;

            var candidate = _dynamicPos + moveBy;

            if (constrain != null && constrain(ref _dynamicPos, ref _vel, in _force))
            {
                candidate = _dynamicPos + _vel + _force;
            }

            _dynamicPos = candidate;

            return _dynamicPos;
        }
    }
}