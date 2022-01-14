using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewController : MonoBehaviour
{
    
    [SerializeField] private float _mouseSensivity = 2f;
    [SerializeField] private bool _verticalyInvert = true;
    private Transform _neck;
    private Transform _Head;
    private float _HeadElevation = 0;

    void Start()
    {
        _neck = transform.GetChild(0);
        _Head = _neck.GetChild(0);
    }

    void Update()
    {
        MoveCameraView();
        LockCursor();
    }

    private void LockCursor()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Pressed");
            Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked);
            Debug.Log(Cursor.lockState);
        }
    }

    private void MoveCameraView()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseDX = Input.GetAxis("Mouse X") * _mouseSensivity;
            float mouseDY = Input.GetAxis("Mouse Y") * _mouseSensivity * (_verticalyInvert ? -1:1);
            _neck.Rotate(new Vector3(0, mouseDX, 0));
            if (_HeadElevation + mouseDY > -90 && _HeadElevation + mouseDY < 90)
            {
                _Head.Rotate(new Vector3(mouseDY, 0, 0));
                _HeadElevation += mouseDY;
            }
        }
    }

}
