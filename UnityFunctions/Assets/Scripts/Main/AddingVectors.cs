using Extensions;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    /// <summary>
    /// IMPORTANT: to manipulate cones you have to have Editor switched to Global
    /// </summary>
    public class AddingVectors : BaseMainScript
    {
        private const float arrowHeight = 0.1f;
        private const float arrowConeBase = 0.05f;
        private const float sphereRadius = 0.03f;

        private Transform _aFrom, _aTo, _bFrom, _bTo, _cFrom, _cTo;

        void Start ()
        {
            var sphe = new DtSphere {radius = sphereRadius};
            var cone = new DtCone {height = arrowHeight, topRadius = 0.001, bottomRadius = arrowConeBase, localPos = new Vector3(0,-arrowHeight, 0) };

            _aFrom = fun.meshes.CreateSphere(sphe).SetName("A_From").SetStandardShaderTransparentColor(0, 1, 0, 1).transform;
            _aTo = fun.meshes.CreateCone(cone).SetName("A_To").SetStandardShaderTransparentColor(0, 1, 0, 1).transform;
            _bFrom = fun.meshes.CreateSphere(sphe).SetName("B_From").SetStandardShaderTransparentColor(0, 0, 1, 1).transform;
            _bTo = fun.meshes.CreateCone(cone).SetName("B_To").SetStandardShaderTransparentColor(0, 0, 1, 1).transform;

            _cFrom = fun.meshes.CreateSphere(new DtSphere {radius = sphereRadius/3f}).SetName("C_From").SetStandardShaderTransparentColor(1, 0, 1, 1).transform;
            _cTo = fun.meshes.CreateCone(new DtCone {height = arrowHeight/3f, topRadius = 0.001, bottomRadius = arrowConeBase/3f, localPos = new Vector3(0,-arrowHeight/3f, 0) }).SetName("C_To").SetStandardShaderTransparentColor(1, 0, 1, 1).transform;

            _aFrom.position = V3(0, 0, 0);
            _bFrom.position = V3(1, 0, 0);
            _aTo.position = V3(0.4, 0, 0);
            _bTo.position = V3(0.6, 0, 0);
        }
        void Update()
        {
            var vecA = _aTo.position - _aFrom.position;
            var vecB = _bTo.position - _bFrom.position;
            
            _aTo.up = vecA.normalized;
            _bTo.up = vecB.normalized;
            Debug.DrawLine(_aFrom.position, _aTo.position, new Color(0, 1, 0, 1),0,true);
            Debug.DrawLine(_bFrom.position, _bTo.position, new Color(0, 0, 1, 1),0,true);

            var vec_A_plus_B = vecA + vecB; // B relative to A
            var cFrom = Vector3.Lerp(_aTo.position, _bTo.position, 0.5f);
            var cTo = cFrom + vec_A_plus_B;
            Debug.DrawLine(cFrom, cTo, new Color(1, 0, 1, 1),0,true);
            _cFrom.position = cFrom;
            _cTo.position = cTo;
            _cTo.up = vec_A_plus_B.normalized;
        }
    }
}