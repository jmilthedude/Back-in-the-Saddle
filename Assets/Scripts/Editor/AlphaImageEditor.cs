using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(AlphaImage))]
public class AlphaImageEditor : ImageEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        SerializedProperty alphaHitTestThreshold = serializedObject.FindProperty("alphaHitThreshold");
        EditorGUILayout.PropertyField(alphaHitTestThreshold);
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}