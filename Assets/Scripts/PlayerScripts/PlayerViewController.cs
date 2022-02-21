using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewController : MonoBehaviour
{
    
    [SerializeField] private float _mouseSensivity = 2f;
    [SerializeField] private bool _verticalyInvert = true;
    private Transform _neck;
    private Transform _head;
    private float _headElevation = 0;

    void Start()
    {
        _neck = transform.GetChild(0);
        _head = _neck.GetChild(0);
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
            if (_headElevation + mouseDY > -90 && _headElevation + mouseDY < 90)
            {
                _head.Rotate(new Vector3(mouseDY, 0, 0));
                _headElevation += mouseDY;
            }
        }
    }

}
