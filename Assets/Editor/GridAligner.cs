using UnityEngine;
using UnityEditor;

class GridAligner : ScriptableObject
{
    [SerializeField] static float step = 1.5f;
    [MenuItem("Aling/Aling to grid")]
    static void Look()
    {
            Undo.RegisterCompleteObjectUndo(Selection.transforms, " aling to grid");
            foreach (Transform transform in Selection.transforms)
            {
                transform.position = VectorFloor(transform.position/step)*step;
            }
    }
    static Vector3 VectorFloor(Vector3 vec)
    {
        return new Vector3(
            Mathf.Floor(vec.x),
            Mathf.Floor(vec.y),
            Mathf.Floor(vec.z)
            );
    }
    [MenuItem("Aling/Aling to grid", true)]
    static bool ValidateSelection()
    {
        return Selection.transforms.Length != 0;
    }
}