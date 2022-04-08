using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridBaker : ScriptableObject
{
    [MenuItem("Scenes/BakeLevel")]
    static void Bake()
    {
        OptimizeMesh(Selection.transforms[0].gameObject);
    }
    static void OptimizeMesh(GameObject gameObject)
    {
        MeshFilter[] meshFilters   = gameObject.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combines = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combines[i].mesh = meshFilters[i].sharedMesh;
            combines[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.sharedMesh = new Mesh();
        meshFilter.sharedMesh.CombineMeshes(combines);
        meshFilter.sharedMesh.Optimize();
        gameObject.GetComponent<MeshCollider>().sharedMesh = meshFilter.sharedMesh;
        gameObject.SetActive(true);
    }
    [MenuItem("Scenes/BakeLevel", true)]
    static bool ValidateSelection()
    {
        return Selection.transforms.Length == 1;
    }
}
