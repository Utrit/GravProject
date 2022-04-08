using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float _speedAcceleration = 2500;
    [SerializeField] private float _airSpeedAcceleration = 100;
    [SerializeField] private float _jumpPower = 100;
    [SerializeField] private float _maxSpeed = 25;
    [SerializeField] [Range(0,1)] private float _friction = 0.1f;
    private PlayerContext _playerContext;
    private Transform _neck;
    private Transform _legs;
    private Ray _groundRay;
    private RaycastHit _groundHit;
    private Rigidbody _rigidBody;
    private Vector3 _moveDirection;
    private bool _grounded;
    void Start()
    {

        _playerContext = GetComponent<PlayerContext>();
        _neck = _playerContext.Neck;
        _legs = _playerContext.Legs;
        _rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        TickMovement();
    }
    void OnCollisionStay(Collision collision)
    {
        _groundRay = new Ray(_legs.position, -_legs.up);
        _grounded = Physics.Raycast(_groundRay, out _groundHit, 0.5f);
    }
    private void OnCollisionExit(Collision collision)
    {
        _grounded = false;
    }
    private void TickMovement()
    {
        _moveDirection = Vector3.zero;

        if (Input.anyKey)
        {

            _moveDirection += _neck.forward * Input.GetAxis("ForwardBackward");
            _moveDirection += _neck.right * Input.GetAxis("RightLeft");
            _moveDirection = _moveDirection.normalized * (_grounded ? _speedAcceleration : _airSpeedAcceleration) * Time.deltaTime;
            if (_grounded) _rigidBody.AddForce(_groundHit.normal * _jumpPower * Input.GetAxis("Jump"));
        }
        _rigidBody.AddForce(_moveDirection);
        Vector3 verticalSpeed = Vector3.Dot(_rigidBody.velocity, Physics.gravity.normalized) * Physics.gravity.normalized;
        Vector3 planeSpeed = _rigidBody.velocity - verticalSpeed;
        if (_grounded) { planeSpeed = Vector3.ClampMagnitude(planeSpeed, _maxSpeed) * (1 - _friction); }
        _rigidBody.velocity = planeSpeed + verticalSpeed;
    }
}
