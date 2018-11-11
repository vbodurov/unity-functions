using Extensions;
using Unianio;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class HueSaturationLuminance : BaseMainScript
    {
        private Transform _sphere;

        void Start ()
	    {
            _sphere = 
                fun.meshes.CreateSphere(new DtSphere {radius = 0.1, name = "hsl"})
                    .SetStandardShaderTransparentColor(1,0,0,1).transform;
            _sphere.position = V3(0, 1, 0.5);

        }

        void Update()
        {
            // X = Hue
            // Y = Saturation
            // Z = Luminance
            var p = _sphere.position;
            var c = fun.color.FromHueSaturationLuminance(
                p.x.Clamp01(),
                p.y.Clamp01(),
                p.z.Clamp01());
            _sphere.GetComponent<Renderer>().material.color = c;
                

            if(Time.frameCount%200==0) Debug.Log(c + " => H:"+ p.x.Clamp01()+" S:"+ p.y.Clamp01()+" L:"+ p.z.Clamp01());
        }
    }
}