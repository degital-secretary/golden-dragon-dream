using System.IO;
using UnityEditor;
using UnityEngine;

public class SceneDuplicator : EditorWindow
{
    private string newSceneName = "";
    private bool addToBuildSettings = true;

    [MenuItem("Tools/Scene Duplicator")]
    public static void OpenWindow()
    {
        GetWindow<SceneDuplicator>("Scene Duplicator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Duplicate selected .unity scene", EditorStyles.boldLabel);

        var selected = Selection.activeObject;
        string selectedPath = selected ? AssetDatabase.GetAssetPath(selected) : "";

        EditorGUILayout.LabelField("Selected:", string.IsNullOrEmpty(selectedPath) ? "<none>" : selectedPath);

        EditorGUILayout.Space();

        newSceneName = EditorGUILayout.TextField("New scene name (no extension, leave empty to auto)", newSceneName);
        addToBuildSettings = EditorGUILayout.Toggle("Add to Build Settings", addToBuildSettings);

        EditorGUILayout.Space();

        GUI.enabled = !string.IsNullOrEmpty(selectedPath) && selectedPath.EndsWith(".unity");
        if (GUILayout.Button("Duplicate Scene"))
        {
            DuplicateScene(selectedPath, newSceneName, addToBuildSettings);
        }
        GUI.enabled = true;
    }

    private static void DuplicateScene(string srcPath, string newName, bool addToBuild)
    {
        string dir = Path.GetDirectoryName(srcPath).Replace("\\", "/");
        string srcFileName = Path.GetFileNameWithoutExtension(srcPath);

        // Determine base destination name
        string baseName = string.IsNullOrEmpty(newName) ? (srcFileName + "_Copy") : newName;
        string destPath = Path.Combine(dir, baseName + ".unity").Replace("\\", "/");

        // If destination exists, auto-increment: base, base1, base2, ...
        if (AssetDatabase.LoadAssetAtPath<Object>(destPath) != null)
        {
            int i = 1;
            string tryPath;
            do
            {
                tryPath = Path.Combine(dir, $"{baseName}{i}.unity").Replace("\\", "/");
                i++;
            } while (AssetDatabase.LoadAssetAtPath<Object>(tryPath) != null);

            destPath = tryPath;
        }

        bool ok = AssetDatabase.CopyAsset(srcPath, destPath);
        if (!ok)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to copy asset from:\n{srcPath}\n\nto:\n{destPath}", "OK");
            return;
        }

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Success", $"Scene duplicated to:\n{destPath}", "OK");

        if (addToBuild)
        {
            var list = new System.Collections.Generic.List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
            // Avoid adding duplicate entries for same path
            bool already = list.Exists(s => s.path == destPath);
            if (!already)
            {
                list.Add(new EditorBuildSettingsScene(destPath, true));
                EditorBuildSettings.scenes = list.ToArray();
                EditorUtility.DisplayDialog("Build Settings", "Added duplicated scene to Build Settings.", "OK");
            }
        }
    }
}
