using UnityEditor;
using UnityEngine;

namespace Klak.Chromatics {

//
// Internal class for drawing gradient graphs
//

sealed class CosineGradientGraphDrawer
{
    #region Public property

    // Draw area rectangle
    public Rect Rect { get; set; }

    #endregion

    #region Internal members

    // Preview shader (exists in the resources directory)
    const string PreviewShaderName =
      "Hidden/Klak/Chromatics/CosineGradient/Preview";

    // Number of vertices in curve
    const int CurveResolution = 96;

    // Preview shader material
    static Material _material;

    // Vertex buffers
    Vector3[] _rectVertices = new Vector3[4];
    Vector3[] _lineVertices = new Vector3[2];
    Vector3[] _curveVertices = new Vector3[CurveResolution];

    // Transform a point into the draw area rectangle.
    Vector3 PointInRect(float x, float y)
      => new Vector3(Mathf.Lerp(Rect.x, Rect.xMax, x),
                     Mathf.Lerp(Rect.yMax, Rect.y, y), 0);

    #endregion

    #region Public methods

    public void DrawLine(float x1, float y1, float x2, float y2, Color color)
    {
        _lineVertices[0] = PointInRect(x1, y1);
        _lineVertices[1] = PointInRect(x2, y2);

        Handles.color = color;
        Handles.DrawAAPolyLine(2, _lineVertices);
    }

    public void DrawRect
      (float x1, float y1, float x2, float y2, float fill, float line)
    {
        _rectVertices[0] = PointInRect(x1, y1);
        _rectVertices[1] = PointInRect(x2, y1);
        _rectVertices[2] = PointInRect(x2, y2);
        _rectVertices[3] = PointInRect(x1, y2);

        Handles.DrawSolidRectangleWithOutline
          (_rectVertices,
           fill < 0 ? Color.clear : Color.white * fill,
           line < 0 ? Color.clear : Color.white * line);
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

        _material.SetVector("_CoeffsA", (Vector3)grad.CoeffsA);
        _material.SetVector("_CoeffsB", (Vector3)grad.CoeffsB);
        _material.SetVector("_CoeffsC", (Vector3)grad.CoeffsC2);
        _material.SetVector("_CoeffsD", (Vector3)grad.CoeffsD2);

        EditorGUI.DrawPreviewTexture
          (Rect, EditorGUIUtility.whiteTexture, _material);
    }

    #endregion
}

}
