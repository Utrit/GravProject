using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AddSceneToBuild : ScriptableObject
{
        [MenuItem("Scenes/AddToBuild")]
        static void AddToBuild()
        {
            const string path = "Assets/ScenesToBuild";
            List<EditorBuildSettingsScene> sceneToBuild = new List<EditorBuildSettingsScene>();
            FileInfo[] scenes = new DirectoryInfo(path).GetFiles("*.unity");
            foreach (FileInfo scene in scenes)
            {
                sceneToBuild.Add(new EditorBuildSettingsScene(path+"/"+scene.Name,true));
                Debug.Log($"AddedScene - {path + "/" + scene.Name}");
            }
            EditorBuildSettings.scenes = sceneToBuild.ToArray();
            Debug.Log($"ScenesAdded - {sceneToBuild.Count}");
        }
}
