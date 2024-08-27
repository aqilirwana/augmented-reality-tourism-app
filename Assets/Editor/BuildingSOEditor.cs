using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildingSO))]
public class BuildingSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BuildingSO buildingSO = (BuildingSO)target;

        buildingSO.mainImage = (Sprite)EditorGUILayout.ObjectField("Main Image", buildingSO.mainImage, typeof(Sprite), false);
        buildingSO.name = EditorGUILayout.TextField("Name", buildingSO.name);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Attributes", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Attribute"))
        {
            buildingSO.attributes.Add(new BuildingAttribute());
        }

        for (int i = 0; i < buildingSO.attributes.Count; i++)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            buildingSO.attributes[i].attributeName = EditorGUILayout.TextField("Attribute Name", buildingSO.attributes[i].attributeName);
            buildingSO.attributes[i].attributeDetails = EditorGUILayout.TextField("Attribute Details", buildingSO.attributes[i].attributeDetails);

            if (GUILayout.Button("Remove Attribute"))
            {
                buildingSO.attributes.RemoveAt(i);  
            }
            EditorGUILayout.EndVertical();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(buildingSO);
        }
    }
}

