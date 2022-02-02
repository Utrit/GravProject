using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonecludianSurface : MonoBehaviour
{
    [SerializeField] private GameObject _otherSurface;
    private Camera _otherCamera;
    private RenderTexture _surfaceTexture;
    private MeshRenderer _meshRenderer;

    public GameObject OtherSurface => _otherSurface;

    void Start()
    {
        
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _otherCamera = _otherSurface.GetComponentInChildren<Camera>();
        _surfaceTexture = new RenderTexture(Screen.width, Screen.height, 24);
        _otherCamera.targetTexture = _surfaceTexture;
        _meshRenderer.material.mainTexture = _otherCamera.targetTexture;
    }

    void LateUpdate()
    {
        var localToWorldCamMatrix = Camera.main.transform.localToWorldMatrix;
        localToWorldCamMatrix = _otherSurface.transform.localToWorldMatrix * transform.worldToLocalMatrix * localToWorldCamMatrix;
        //float scaleDiff = (_otherSurface.transform.localScale-transform.localScale).magnitude;
        Vector3 renderPosition = localToWorldCamMatrix.GetColumn(3);
        //renderPosition *=scaleDiff;
        Quaternion renderRotation = localToWorldCamMatrix.rotation;

        _otherCamera.transform.SetPositionAndRotation(renderPosition, renderRotation);

        ObliqueNearClip();
    }
    void ObliqueNearClip()
    {
        Transform clipPlane = _otherSurface.transform;
        int direction = System.Math.Sign(Vector3.Dot(clipPlane.forward, clipPlane.position-_otherCamera.transform.position));

        Vector3 camSpacePos = _otherCamera.worldToCameraMatrix.MultiplyPoint3x4(clipPlane.position);
        Vector3 camSpaceNormal = _otherCamera.worldToCameraMatrix.MultiplyVector(clipPlane.forward) * direction;
        float camDistance = -Vector3.Dot(camSpacePos, camSpaceNormal);
        Vector4 clipPlaneCameraSpace = new Vector4(camSpaceNormal.x, camSpaceNormal.y, camSpaceNormal.z, camDistance+0.25f);

        _otherCamera.projectionMatrix = Camera.main.CalculateObliqueMatrix(clipPlaneCameraSpace);
    }
}
