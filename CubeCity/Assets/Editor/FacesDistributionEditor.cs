using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DistributionItem))]
public class DistributionItemDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), label);
        
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var amountRect = new Rect(position.x, position.y, position.width, position.height);
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);

        EditorGUI.EndProperty();
    }
}