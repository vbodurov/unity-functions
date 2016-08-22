using UnityEngine;

namespace Extensions
{
    internal static class GameObjectExtensions
    {
        internal static GameObject SetStandardShaderTransparentColor(this GameObject go, Color color)
        {
            return go.SetStandardShaderTransparentColor(color.r,color.g,color.b,color.a);
        }
        internal static GameObject SetStandardShaderTransparentColor(this GameObject go, double r, double g, double b, double a)
        {
            var renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.SetStandardShaderRenderingModeTransparent();
                renderer.material.color = new Color((float)r,(float)g,(float)b,(float)a);
            }
            return go;
        }
    }
}