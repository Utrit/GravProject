using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : BasicLogicElement
{
    private Vector3 _initPosition;
    private Vector3 _pressedPosition;
    private Vector3 _targetPosition;
    private Transform _animationObject;
    // Start is called before the first frame update
    void Start()
    {
        _animationObject = transform.GetChild(0);
        _initPosition = _animationObject.position;
        _targetPosition = _initPosition;
        _pressedPosition = _animationObject.position - _animationObject.up*0.15f;
    }
    private void OnTriggerStay(Collider other)
    {
        _targetPosition = _pressedPosition;
        _state = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _targetPosition = _initPosition;
        _state = false;
    }
    // Update is called once per frame
    void Update()
    {
        _animationObject.position = Vector3.Slerp(_animationObject.position, _targetPosition, 5f * Time.deltaTime);
    }
}
