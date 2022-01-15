using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryController : MonoBehaviour
{
    [SerializeField] private float _gravityAlingSpeed = 360;
    [SerializeField] private int _cubeLayer = 8;
    private Transform _neck;
    private Transform _head;
    private Ray _lookRay;
    private RaycastHit _lookHit;
    private Quaternion _targetRotation;
    private Rigidbody _carrydObject;
    void Start()
    {
        _neck = transform.GetChild(0);
        _head = _neck.GetChild(0);
        _cubeLayer = 1 << _cubeLayer;
    }

    void Update()
    {
        TickMouse();
    }
    private void FixedUpdate()
    {
        if (_carrydObject != null) CarryObjectTick();
    }

    private void CarryObjectTick()
    {
        Vector3 CarryPoint = _head.position + _head.forward * 2.5f;
        Vector3 CarryDirection = CarryPoint - _carrydObject.position;
        _targetRotation = Quaternion.FromToRotation(_carrydObject.transform.up, -Physics.gravity) * _carrydObject.rotation;
        _carrydObject.angularVelocity = Vector3.zero;
        _carrydObject.rotation = Quaternion.RotateTowards(_carrydObject.rotation, _targetRotation, _gravityAlingSpeed * Time.deltaTime);
        _carrydObject.AddForce(CarryDirection*10 - _carrydObject.velocity);
    }
    private void TickMouse()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (Input.GetButtonDown("Submit"))
            {
                if (_carrydObject != null) {
                    _carrydObject.useGravity = true;
                    _carrydObject = null;
                }
                else {
                    CatchObject();
                    Debug.DrawRay(_head.position, _head.forward * 10, Color.red, 10f);
                    Debug.DrawRay(_lookHit.point, _lookHit.normal * 10, Color.green, 10f);
                }
            }
        }
    }

    private void CatchObject()
    {
        _lookRay = new Ray(_head.position, _head.forward);
        if (Physics.Raycast(_lookRay, out _lookHit, 3, _cubeLayer))
        {
            _carrydObject = _lookHit.rigidbody;
            _carrydObject.useGravity = false;
            Debug.Log(_carrydObject.name);
        }
    }
}
