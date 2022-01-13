using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewController : MonoBehaviour
{
    
    [SerializeField] private float _MouseSensivity = 2f;
    [SerializeField] private bool _VerticalyInvert = true;
    private Vector2 _LastMousePos;
    private Transform _Neck;
    private Transform _Head;
    private float _HeadElevation = 0;

    void Start()
    {
        _LastMousePos = new Vector2(0, 0);
        _Neck = transform.GetChild(0);
        _Head = _Neck.GetChild(0);
    }

    void Update()
    {
        MoveCameraView();
        LockCursor();
    }

    public void LockCursor()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("Pressed");
            Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked);
            Debug.Log(Cursor.lockState);
        }
    }

    public void MoveCameraView()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float MouseDX = Input.GetAxis("Mouse X") * _MouseSensivity;
            float MouseDY = Input.GetAxis("Mouse Y") * _MouseSensivity * (_VerticalyInvert ? -1:1);
            _Neck.Rotate(new Vector3(0, MouseDX, 0));
            if (_HeadElevation + MouseDY > -90 && _HeadElevation + MouseDY < 90)
            {
                _Head.Rotate(new Vector3(MouseDY, 0, 0));
                _HeadElevation += MouseDY;
            }
        }
    }

}
