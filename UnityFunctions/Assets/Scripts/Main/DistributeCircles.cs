using Assets.Scripts;
using Extensions;
using Unianio;
using UnityEngine;
using UnityEngine.UI;
using UnityFunctions;

namespace Main
{
    public class DistributeCircles : BaseMainScript
    {
        const float Radius = 1;
        void Start ()
	    {
            GenerateInStages();
        }
        void GenerateInStages()
        {
            var center = v2.zero;
            CreateCircle(center, Radius, Color.red);
            for (var a = 0.0; a <= 360; a += 60)
            {
                var pos = fun.rotate.Point2dAbout(center + (v2.unitX * (Radius * 1)), v2.zero, a);
                CreateCircle(pos, Radius, Color.yellow);

                var dir = (pos - center).normalized;
                
                CreateCircle(fun.rotate.Point2dAbout(pos + dir * Radius, pos, -30), Radius, Color.green);
                CreateCircle(fun.rotate.Point2dAbout(pos + dir * Radius, pos, +30), Radius, Color.green);
            }
        }
        
        void GenerateDirect()
        {
            CreateCircle(v2.zero, Radius, Color.red);
            for (var a = 0.0; a <= 360; a += 60)
            {
                CreateCircle(fun.rotate.Point2dAbout((v2.unitX * (Radius * 1)), v2.zero, a), Radius, Color.yellow);
            }
            for (var a = 0.0; a <= 360; a += 30)
            {
                CreateCircle(fun.rotate.Point2dAbout((v2.unitX * (Radius * 2)), v2.zero, a), Radius, Color.green);
            }
            for (var a = 0.0; a <= 360; a += 20)
            {
                CreateCircle(fun.rotate.Point2dAbout((v2.unitX * (Radius * 3)), v2.zero, a), Radius, Color.magenta);
            }
            for (var a = 0.0; a <= 360; a += 15)
            {
                CreateCircle(fun.rotate.Point2dAbout((v2.unitX * (Radius * 4)), v2.zero, a), Radius, Color.white);
            }
        }


        void CreateCircle(Vector2 center, double radius, Color color)
        {
            Vector3 prev = v3.zero;
            for(var a = 0.0; a <= 360; a += 11.25)
            {
                var curr = V3(center.x + radius, 0, center.y).RotateAbout(V3(center.x, 0, center.y), in v3.unitY, a);

                if(a > 0)
                {
                    Debug.Log("@@"+ prev+"|"+curr);
                    Debug.DrawLine(prev, curr, color, 99999, false);
                }
                prev = curr;
            }
        }
    }
}