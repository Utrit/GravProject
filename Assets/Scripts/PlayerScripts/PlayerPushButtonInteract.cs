using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushButtonInteract : MonoBehaviour
{
    private Transform _neck;
    private Transform _head;
    private Ray _lookRay;
    private RaycastHit _lookHit;
    private Rigidbody _carrydObject;
    private PlayerContext _playerContext;
    void Start()
    {
        _playerContext = GetComponent<PlayerContext>();
        _neck = _playerContext.Neck;
        _head = _playerContext.Head;
    }

    void Update()
    {
        TickMouse();
    }

    private void TickMouse()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (Input.GetButtonDown("Submit"))
            {
                    PushingButton();
            }
        }
    }

    private void PushingButton()
    {
        _lookRay = new Ray(_head.position, _head.forward);
        if (Physics.Raycast(_lookRay, out _lookHit, 3))
        {
            PushButton button = _lookHit.collider.gameObject.GetComponent<PushButton>();
            if (button)
            {
                button.PushDown();
            }
        }
    }
}
