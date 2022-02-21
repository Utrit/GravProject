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
    void Start()
    {
        _neck = transform.GetChild(0);
        _head = _neck.GetChild(0);
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
