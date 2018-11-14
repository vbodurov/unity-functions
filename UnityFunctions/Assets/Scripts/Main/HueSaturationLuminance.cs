using Extensions;
using Unianio;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class HueSaturationLuminance : BaseMainScript
    {
        Material _sphere;
        Text _hueVal, _satVal, _lumVal;
        Slider _hueSld, _satSld, _lumSld;
        float _hue, _sat, _lum;

        void Start ()
	    {
	        _sphere = fun.meshes.CreateSphere(new DtSphere {radius = 2, name = "hsl"}).GetComponent<Renderer>().material;
            _hueVal = GameObject.Find("HueValue").GetComponent<Text>();
	        _satVal = GameObject.Find("SatValue").GetComponent<Text>();
	        _lumVal = GameObject.Find("LumValue").GetComponent<Text>();
	        _hueSld = GameObject.Find("HueSlider").GetComponent<Slider>();
            _satSld = GameObject.Find("SatSlider").GetComponent<Slider>();
            _lumSld = GameObject.Find("LumSlider").GetComponent<Slider>();
	        _hueSld.value = _hue = 0.0f;
            _satSld.value = _sat = 1.0f;
            _lumSld.value = _lum = 0.5f;

        }

        void Update()
        {
            _hue = _hueSld.value;
            _sat = _satSld.value;
            _lum = _lumSld.value;
            _hueVal.text = _hue.ToString("F2");
            _satVal.text = _sat.ToString("F2");
            _lumVal.text = _lum.ToString("F2");
            _sphere.color = fun.color.FromHueSaturationLuminance(_hue.Round(2), _sat.Round(2), _lum.Round(2));
        }
    }
}