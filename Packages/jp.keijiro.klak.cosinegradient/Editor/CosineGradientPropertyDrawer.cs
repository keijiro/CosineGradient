using UnityEditor;
using UnityEngine;

namespace Klak.Chromatics {

//
// Custom property drawer for CosineGradient
//

[CustomPropertyDrawer(typeof(CosineGradient))]
public class CosineGradientPropertyDrawer : PropertyDrawer
{
    #region Private members

    CosineGradientGraphDrawer _graph;

    static class Style
    {
        public static GUIContent Randomize = new GUIContent("Randomize");
    }

    Vector4 ReadFloat4AsVector4(SerializedProperty prop)
      => new Vector4(prop.FindPropertyRelative("x").floatValue,
                     prop.FindPropertyRelative("y").floatValue,
                     prop.FindPropertyRelative("z").floatValue,
                     prop.FindPropertyRelative("w").floatValue);

    void Randomize(SerializedProperty prop)
    {
        prop.serializedObject.Update();
        prop.FindPropertyRelative("R.x").floatValue = Random.value;
        prop.FindPropertyRelative("R.y").floatValue = Random.value;
        prop.FindPropertyRelative("R.z").floatValue = Random.value * 2;
        prop.FindPropertyRelative("R.w").floatValue = Random.value;
        prop.FindPropertyRelative("G.x").floatValue = Random.value;
        prop.FindPropertyRelative("G.y").floatValue = Random.value;
        prop.FindPropertyRelative("G.z").floatValue = Random.value * 2;
        prop.FindPropertyRelative("G.w").floatValue = Random.value;
        prop.FindPropertyRelative("B.x").floatValue = Random.value;
        prop.FindPropertyRelative("B.y").floatValue = Random.value;
        prop.FindPropertyRelative("B.z").floatValue = Random.value * 2;
        prop.FindPropertyRelative("B.w").floatValue = Random.value;
        prop.serializedObject.ApplyModifiedProperties();
    }

    #endregion

    #region PropertyDrawer implementation

    public override float
      GetPropertyHeight(SerializedProperty prop, GUIContent label)
      => EditorGUIUtility.singleLineHeight * 5 +
         EditorGUIUtility.standardVerticalSpacing * 4;

    public override void
      OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
    {
        // Entry
        EditorGUI.BeginProperty(rect, label, prop);

        // Graph drawer object lazy initialization
        if (_graph == null) _graph = new CosineGradientGraphDrawer();

        // -- Data retrieval

        // RGB component properties
        var rprop = prop.FindPropertyRelative("R");
        var gprop = prop.FindPropertyRelative("G");
        var bprop = prop.FindPropertyRelative("B");

        // RGB component values
        var rvalue = ReadFloat4AsVector4(rprop);
        var gvalue = ReadFloat4AsVector4(gprop);
        var bvalue = ReadFloat4AsVector4(bprop);

        // Gradient reconstruction
        var gradient = new CosineGradient
          { R = rvalue, G = gvalue, B = bvalue };

        // -- GUI

        // Basic constants
        var labelWidth = EditorGUIUtility.labelWidth;
        var lineHeight = EditorGUIUtility.singleLineHeight;
        var lineSpace = EditorGUIUtility.standardVerticalSpacing;

        // Draw the prefix label.
        var labelID = GUIUtility.GetControlID(FocusType.Passive);
        rect.height = lineHeight;
        EditorGUI.PrefixLabel(rect, labelID, label);

        // Calculate the graph area.
        rect.x += labelWidth;
        rect.width -= labelWidth;
        rect.height = lineHeight * 2 + lineSpace;
        _graph.SetRect(rect);

        // Right-click menu
        var ev = Event.current;
        if (ev.type == EventType.MouseDown && ev.button == 1 &&
            rect.Contains(ev.mousePosition))
        {
            var menu = new GenericMenu();
            menu.AddItem(Style.Randomize, false, () => Randomize(prop));
            menu.ShowAsContext();
            ev.Use();
        }

        // Graph: Background
        _graph.DrawPreview(gradient);

        // Graph: Horizontal line
        var lineColor = Color.white * 0.4f;
        _graph.DrawLine(0, 0.5f, 1, 0.5f, lineColor);

        // Graph: Vertical lines
        _graph.DrawLine(0.25f, 0, 0.25f, 1, lineColor);
        _graph.DrawLine(0.50f, 0, 0.50f, 1, lineColor);
        _graph.DrawLine(0.75f, 0, 0.75f, 1, lineColor);

        // Graph: R/G/B curves
        _graph.DrawGradientCurve(rvalue, Color.red);
        _graph.DrawGradientCurve(gvalue, Color.green);
        _graph.DrawGradientCurve(bvalue, Color.blue);

        // Calculate the field edit area.
        rect.height = lineHeight;
        rect.y += lineHeight * 2 + lineSpace * 2;

        // Minimize the label width.
        EditorGUIUtility.labelWidth = 12;

        // Cancel indentation.
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Red
        EditorGUI.PropertyField(rect, rprop, GUIContent.none);
        rect.y += lineHeight + lineSpace;

        // Green
        EditorGUI.PropertyField(rect, gprop, GUIContent.none);
        rect.y += lineHeight + lineSpace;

        // Blue
        EditorGUI.PropertyField(rect, bprop, GUIContent.none);

        // Recover the original label width and indent level.
        EditorGUIUtility.labelWidth = labelWidth;
        EditorGUI.indentLevel = indent;

        // Exit
        EditorGUI.EndProperty();
    }

    #endregion
}

} // namespace Klak.Chromatics
