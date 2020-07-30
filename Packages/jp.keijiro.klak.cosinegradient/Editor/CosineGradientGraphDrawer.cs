using UnityEditor;
using UnityEngine;

namespace Klak.Chromatics {

//
// Internal class for drawing gradient graphs
//

sealed class CosineGradientGraphDrawer
{
    #region Internal members

    // Preview shader (exists in the resources directory)
    const string PreviewShaderName =
      "Hidden/Klak/Chromatics/CosineGradient/Preview";

    // Number of vertices in curve
    const int CurveResolution = 96;

    // Preview shader material
    static Material _material;

    // Draw area rectangle
    Rect _rect;

    // Vertex buffers
    Vector3[] _lineVertices = new Vector3[2];
    Vector3[] _curveVertices = new Vector3[CurveResolution];

    // Transform a point into the draw area rectangle.
    Vector3 PointInRect(float x, float y)
      => new Vector3(Mathf.Lerp(_rect.x, _rect.xMax, x),
                     Mathf.Lerp(_rect.yMax, _rect.y, y), 0);

    #endregion

    #region Public methods

    public void SetRect(Rect rect)
    {
        // Shrink slightly
        rect.x += 3;
        rect.y ++;
        rect.width -= 4;
        rect.height -= 2;
        _rect = rect;
    }

    public void DrawLine(float x1, float y1, float x2, float y2, Color color)
    {
        _lineVertices[0] = PointInRect(x1, y1);
        _lineVertices[1] = PointInRect(x2, y2);

        Handles.color = color;
        Handles.DrawAAPolyLine(2, _lineVertices);
    }


    public void DrawGradientCurve(Vector4 coeffs, Color color)
    {
        for (var i = 0; i < CurveResolution; i++)
        {
            var x = (float)i / (CurveResolution - 1);
            var theta = (coeffs.z * x + coeffs.w) * Mathf.PI * 2;
            var y = coeffs.x + coeffs.y * Mathf.Cos(theta);
            _curveVertices[i] = PointInRect(x, Mathf.Clamp01(y));
        }

        Handles.color = color;
        Handles.DrawAAPolyLine(2, CurveResolution, _curveVertices);
    }

    public void DrawPreview(CosineGradient grad)
    {
        if (_material == null)
        {
            _material = new Material(Shader.Find(PreviewShaderName));
            _material.hideFlags = HideFlags.DontSave;
        }

        _material.SetMatrix("_Gradient", grad);

        EditorGUI.DrawPreviewTexture
          (_rect, EditorGUIUtility.whiteTexture, _material);
    }

    #endregion
}

}
