using UnityEngine;
using UnityEditor;

class LookAtMainCamera : ScriptableObject
{
    [MenuItem("Aling/Aling to surface _g")]
    static void Look()
    {
        var camera = SceneView.lastActiveSceneView.camera;
        int layerMask = 1 << 9;
        layerMask = layerMask + (1 << 10);
        RaycastHit rayHitInfo;
        if (camera)
        {
            foreach (Transform transform in Selection.transforms)
            {
                Undo.RegisterCompleteObjectUndo(Selection.transforms, " aling to surface");
                Ray rayToObj = new Ray(camera.transform.position, transform.position - camera.transform.position);
                if (Physics.Raycast(rayToObj,out rayHitInfo, 1000, layerMask))
                {
                    transform.rotation = Quaternion.FromToRotation(transform.up, rayHitInfo.normal) * transform.rotation;
                    Ray rayToSurface = new Ray(transform.position + transform.up * 3, -transform.up);
                    if(Physics.Raycast(rayToSurface,out rayHitInfo, 1000, layerMask))
                    {
                        transform.position = rayHitInfo.point+transform.up*transform.localScale.y;
                    }
                }
            }
        }
    }

    [MenuItem("Aling/Aling to surface _g", true)]
    static bool ValidateSelection()
    {
        return Selection.transforms.Length != 0;
    }
}