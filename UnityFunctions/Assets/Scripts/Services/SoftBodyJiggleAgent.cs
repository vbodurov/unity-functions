using System;
using Extensions;
using Unianio;
using UnityEngine;
using static Unianio.fun;

namespace Services
{
    internal interface ISoftBodyJiggleAgent
    {
        (Vector3 position, Quaternion rotation) Compute();
        SoftBodyConfig Config { get; }
    }

    internal class SoftBodyConfig
    {
        internal double Stiffness { get; set; } = 0.10;
        internal double Mass { get; set; } = 0.90;
        internal double Damping { get; set; } = 0.75;
        internal double MaxDegrees { get; set; } = 50;
        internal double MaxStretch { get; set; } = 2;
        internal double MaxSqueeze { get; set; } = 1;
        internal double RelTargetAt { get; set; } = 0.07;
        internal bool SqueezeAndStretch { get; set; } = false;
        internal Transform Bone { get; set; }

    }
    internal class SoftBodyJiggleAgent : ISoftBodyJiggleAgent
    {
        readonly IPendulumPhysicsAgent _ppa;
        readonly SoftBodyConfig _cfg;
        readonly Vector3 _relStaticTarget, _relIniPos, _relIniFw, _relIniUp;
        internal SoftBodyJiggleAgent(SoftBodyConfig config)
        {
            _ppa = new PendulumPhysicsAgent(config.Stiffness, config.Mass, config.Damping);
            _cfg = config ?? new SoftBodyConfig();
            _relStaticTarget =
                (_cfg.Bone.position + _cfg.Bone.forward * (float) _cfg.RelTargetAt)
                .AsLocalPoint(_cfg.Bone.parent);
            _relIniPos = _cfg.Bone.position.AsLocalPoint(_cfg.Bone.parent);
            _relIniFw = _cfg.Bone.forward.AsLocalDir(_cfg.Bone.parent);
            _relIniUp = _cfg.Bone.up.AsLocalDir(_cfg.Bone.parent);
        }
        SoftBodyConfig ISoftBodyJiggleAgent.Config => _cfg;
        (Vector3 position, Quaternion rotation) ISoftBodyJiggleAgent.Compute()
        {
            var staticTarget = 
                _relStaticTarget.AsWorldPoint(_cfg.Bone.parent);

            var iniFw = _relIniFw.AsWorldDir(_cfg.Bone.parent);
            
            var staticSource =
                _relIniPos.AsWorldPoint(_cfg.Bone.parent);

            var dynamicTarget = 
                _ppa.Compute(in staticTarget,
                    (ref Vector3 dynamicPos, ref Vector3 velocity, in Vector3 force) =>
                    {
                        var candidate = dynamicPos + velocity + force;
                        var curFw = staticSource.DirTo(in candidate, out var dist);
                        var degrees = angle.BetweenVectorsUnSignedInDegrees(in iniFw, in curFw);
                        var x = degrees / _cfg.MaxDegrees;
                        var y = bezier(x, 0.70, 0.00, 1.00, 0.00, 0.80, 1.00, 0.97, 1.00);
                        if (y < 0.001)
                        {
                            return false;
                        }
                        velocity = velocity * (1f - y);
                        if (degrees > 30)
                        {
                            dynamicPos = staticSource + iniFw.RotateTowards(curFw, _cfg.MaxDegrees) * dist;
                        }

                        return true;
                    });

//Debug.DrawLine(_cfg.Bone.position, staticTarget, Color.black, 0, false);

            var fw = (dynamicTarget - staticSource).ToUnit(out var length);
            var rotation = 
                Quaternion.LookRotation(fw, _relIniUp.AsWorldDir(_cfg.Bone.parent));

            var xx = (length / _cfg.RelTargetAt).FromRangeTo01(0,2);

            var yy = bezier(xx, 0.00, -1.00, 0.00, -0.50, 1.00, 0.50, 1.00, 1.00);

            var span = yy < 0 ? _cfg.MaxSqueeze : _cfg.MaxStretch;

            return (
                position: staticSource + _relIniFw*(yy*(float)span), 
                rotation: rotation
                );
        }
    }
}