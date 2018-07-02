using Extensions;
using Unianio;
using UnityEngine;
using UnityFunctions;

namespace Main
{
    public class CapsuleCapsuleResolution : BaseMainScript
    {
//        private const float CapsuleRadius1 = 0.12f;
//        private const float CapsuleRadius2 = 0.2f;
//        private const float CapsuleHeight1 = 0.8f;
//        private const float CapsuleHeight2 = 0.6f;
        
        private const float StaticHeight = 0.4f;
        private const float StaticRadius = 0.14f;

        private const float DynamicHeight = 0.8f;
        private const float DynamicRadius = 0.06f;
        private Transform _static,_dynamicPrev,_dynamicNext;
        private GameObject _dynPrev;
        private GameObject _dynNext;

        void Start ()
        {
            var statCap = fun.meshes.CreateCapsule(
                    new DtCapsule {radius = StaticRadius, height = StaticHeight, name = "static_cap"});
            _dynPrev = fun.meshes.CreateCapsule(
                    new DtCapsule {radius = DynamicRadius, height = DynamicHeight, name = "dynamic_prev_raw"});
            _dynNext = fun.meshes.CreateCapsule(
                    new DtCapsule {radius = DynamicRadius, height = DynamicHeight, name = "dynamic_next_raw"});
            _dynPrev.hideFlags = HideFlags.HideInHierarchy;
            _dynNext.hideFlags = HideFlags.HideInHierarchy;


            var dynPrevWrapper = new GameObject("dynamic_prev");
            dynPrevWrapper.transform.position = _dynPrev.transform.position + (-_dynPrev.transform.up)*(DynamicHeight/2+DynamicRadius);
            dynPrevWrapper.transform.rotation = Quaternion.LookRotation(_dynPrev.transform.up, _dynPrev.transform.forward);
            dynPrevWrapper.hideFlags = HideFlags.HideInHierarchy;
            _dynPrev.transform.SetParent(dynPrevWrapper.transform);

            var dynNextWrapper = new GameObject("dynamic_next");
            dynNextWrapper.transform.position = _dynNext.transform.position + (-_dynNext.transform.up)*(DynamicHeight/2+DynamicRadius);
            dynNextWrapper.transform.rotation = Quaternion.LookRotation(_dynNext.transform.up, _dynNext.transform.forward);  
            _dynNext.transform.SetParent(dynNextWrapper.transform);


            _static = statCap.transform;
            _dynamicPrev = dynPrevWrapper.transform;
            _dynamicPrev.position += Vector3.forward*-0.5f + Vector3.up*0.5f + Vector3.right*0.3f;
            _dynamicPrev.rotation = Quaternion.Euler(0,0,180);
            _dynamicNext = dynNextWrapper.transform;
            _dynamicNext.position = _dynamicPrev.position;
            _dynamicNext.rotation = _dynamicPrev.rotation;

        }
        void Update()
        {
            var statP0 = _static.position - _static.up*(StaticHeight/2);
            var statP1 = _static.position + _static.up*(StaticHeight/2);

            var prevP0 = _dynamicPrev.position + _dynamicPrev.forward*(DynamicRadius);
            var prevP1 = _dynamicPrev.position + _dynamicPrev.forward*(DynamicRadius + DynamicHeight);

            var nextP0 = _dynamicNext.position + _dynamicNext.forward*(DynamicRadius);
            var nextP1 = _dynamicNext.position + _dynamicNext.forward*(DynamicRadius + DynamicHeight);
            var nextUp = _dynamicNext.up;

            // test code STARTS here -----------------------------------------------
            Vector3 resolvedPos;
            Quaternion resolvedRot;
            var hasResolution = 
                fun.resolution.BetweenCapsules(
                    ref statP0, ref statP1, StaticRadius, 
                    ref prevP0, ref prevP1, ref nextP0, ref nextP1, ref nextUp, DynamicRadius,
                    out resolvedPos, out resolvedRot);
            // test code ENDS here -------------------------------------------------
            
            _dynamicPrev.position = resolvedPos;
            _dynamicPrev.rotation = resolvedRot;

            SetColorOnChanged(hasResolution, rgba(1, 0, 0, 0.5), rgba(0.5,0.0,0.0,0.2), _static);
            SetColorOnChanged(hasResolution, rgba(0, 1, 0, 0.5), rgba(0.0,0.5,0.0,0.2), _dynPrev.transform);
            SetColorOnChanged(hasResolution, rgba(0, 0, 1, 0.5), rgba(0.0,0.0,0.5,0.2), _dynNext.transform);
        }

        

        


    }
}