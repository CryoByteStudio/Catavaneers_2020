using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (var scope = new EditorGUI.DisabledGroupScope(true))
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}

[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
public class ShowOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string text;

        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                text = property.intValue.ToString();
                break;
            case SerializedPropertyType.Boolean:
                text = property.boolValue.ToString();
                break;
            case SerializedPropertyType.Float:
                text = property.floatValue.ToString("0.000");
                break;
            case SerializedPropertyType.String:
                text = property.stringValue;
                break;
            default:
                text = "Not supported...";
                break;
        }

        EditorGUI.LabelField(position, label.text, text);
    }
}

[CustomPropertyDrawer(typeof(BeginReadOnlyGroupAttribute))]
public class BeginReadOnlyGroupDrawer : DecoratorDrawer
{
    public override float GetHeight()
    {
        return 0;
    }

    public override void OnGUI(Rect position)
    {
        EditorGUI.BeginDisabledGroup(true);
    }
}

[CustomPropertyDrawer(typeof(BeginReadOnlyGroupAttribute))]
public class EndReadOnlyGroupDrawer : DecoratorDrawer
{
    public override float GetHeight()
    {
        return 0;
    }

    public override void OnGUI(Rect position)
    {
        EditorGUI.EndDisabledGroup();
    }
}