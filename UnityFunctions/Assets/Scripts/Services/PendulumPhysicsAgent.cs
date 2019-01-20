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
        Vector3 Compute(in Vector3 currRigidPosition, ApplyConstrain constrain = null);
    }
    internal class PendulumPhysicsAgent : IPendulumPhysicsAgent
    {
        Vector3 force, acc, vel, dynamicPos;
        float bStiffness, bMass, bDamping;

        internal PendulumPhysicsAgent(double stiffness = 0.1, double mass = 0.9, double damping = 0.75)
        {
            bStiffness = (float)stiffness;
            bMass = (float)mass;
            bDamping = (float)damping;
        }

        
        Vector3 IPendulumPhysicsAgent.Compute(in Vector3 targetPos, ApplyConstrain constrain)
        {
            force.x = (targetPos.x - dynamicPos.x) * bStiffness;
            acc.x = force.x / bMass;
            vel.x += acc.x * (1 - bDamping);

            force.y = (targetPos.y - dynamicPos.y) * bStiffness;
            // force.y -= bGravity / 10; // Add some gravity
            acc.y = force.y / bMass;
            vel.y += acc.y * (1 - bDamping);

            force.z = (targetPos.z - dynamicPos.z) * bStiffness;
            acc.z = force.z / bMass;
            vel.z += acc.z * (1 - bDamping);

            var moveBy = vel + force;

            var candidate = dynamicPos + moveBy;

            if (constrain != null && constrain(ref dynamicPos, ref vel, in force))
            {
                candidate = dynamicPos + vel + force;
            }

            dynamicPos = candidate;

            return dynamicPos;
        }
    }
}