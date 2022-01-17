using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonecludianSurface : MonoBehaviour
{
    [SerializeField] private GameObject _otherSurface;
    private Camera _otherCamera;
    private RenderTexture _surfaceTexture;
    private MeshRenderer _meshRenderer;
    void Start()
    {
        
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _otherCamera = _otherSurface.GetComponentInChildren<Camera>();
        _surfaceTexture = new RenderTexture(Screen.width, Screen.height, 24);
        _otherCamera.targetTexture = _surfaceTexture;
        _meshRenderer.material.mainTexture = _otherCamera.targetTexture;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        Vector3 LocalCoordsPosition = transform.InverseTransformPoint(Camera.main.transform.position);
        LocalCoordsPosition = new Vector3(-LocalCoordsPosition.x, LocalCoordsPosition.y, -LocalCoordsPosition.z);
        _otherCamera.transform.localPosition = LocalCoordsPosition;

        Quaternion differenceRotation = transform.rotation * Quaternion.Inverse(_otherSurface.transform.rotation * Quaternion.Euler(0, 180, 0));
        _otherCamera.transform.rotation = differenceRotation * Camera.main.transform.rotation;

        //_otherCamera.nearClipPlane = LocalCoordsPosition.magnitude;
        ObliqueNearClip();
    }
    void ObliqueNearClip()
    {
        Transform clipPlane = _otherSurface.transform;
        int direction = System.Math.Sign(Vector3.Dot(clipPlane.forward, clipPlane.position-_otherCamera.transform.position));

        Vector3 camSpacePos = _otherCamera.worldToCameraMatrix.MultiplyPoint3x4(clipPlane.position);
        Vector3 camSpaceNormal = _otherCamera.worldToCameraMatrix.MultiplyVector(clipPlane.forward) * direction;
        float camDistance = -Vector3.Dot(camSpacePos, camSpaceNormal);
        Vector4 clipPlaneCameraSpace = new Vector4(camSpaceNormal.x, camSpaceNormal.y, camSpaceNormal.z, camDistance);

        _otherCamera.projectionMatrix = Camera.main.CalculateObliqueMatrix(clipPlaneCameraSpace);
    }
}
