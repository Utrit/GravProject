using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float _SpeedAcceleration = 2500;
    [SerializeField] private float _AirSpeedAcceleration = 100;
    [SerializeField] private float _JumpPower = 100;
    [SerializeField] private float _MaxSpeed = 25;
    [SerializeField] [Range(0,1)] private float _Friction = 0.1f;
    private Transform _Neck;
    private Transform _Head;
    private Transform _Legs;
    private Ray _GroundRay;
    private RaycastHit _Groundhit;
    private Rigidbody _RigidBody;
    private Vector3 _MoveDirection;
    private bool _grounded;
    void Start()
    {
        _Legs = transform.GetChild(1);
        _Neck = transform.GetChild(0);
        _Head = _Neck.GetChild(0);
        _RigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        TickMovement();
    }
    void OnCollisionStay(Collision collision)
    {
        _GroundRay = new Ray(_Legs.position, -_Legs.up);
        _grounded = Physics.Raycast(_GroundRay, out _Groundhit, 0.5f);
    }
    private void OnCollisionExit(Collision collision)
    {
        _grounded = false;
    }
    public void TickMovement()
    {
        _MoveDirection = Vector3.zero;

        if (Input.anyKey)
        {

            _MoveDirection += _Neck.forward * Input.GetAxis("ForwardBackward");
            _MoveDirection += _Neck.right * Input.GetAxis("RightLeft");
            _MoveDirection = _MoveDirection * (_grounded ? _SpeedAcceleration:_AirSpeedAcceleration) * Time.deltaTime;
            if (_grounded) _RigidBody.AddForce(_Groundhit.normal * _JumpPower * Input.GetAxis("Jump"));
        }
        _RigidBody.AddForce(_MoveDirection);
        Vector3 VerticalSpeed = Vector3.Dot(_RigidBody.velocity, Physics.gravity.normalized) * Physics.gravity.normalized;
        Vector3 PlaneSpeed = _RigidBody.velocity - VerticalSpeed;
        if (_grounded) { PlaneSpeed = Vector3.ClampMagnitude(PlaneSpeed, _MaxSpeed) * (1 - _Friction); }
        _RigidBody.velocity = PlaneSpeed + VerticalSpeed;
    }
}
