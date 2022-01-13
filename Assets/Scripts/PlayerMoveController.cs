using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float _SpeedAcceleration = 10;
    [SerializeField] private float _JumpPower = 1;
    [SerializeField] [Range(0,1)] private float _Friction = 0.1f;
    private Transform _Neck;
    private Transform _Head;
    private Transform _Legs;
    private Ray _GroundRay;
    private RaycastHit _Groundhit;
    private Ray _LookRay;
    private RaycastHit _Lookhit;
    private Vector3 _MoveAcceleration;
    private Rigidbody _RigidBody;
    private Quaternion _TargetRotation;
    void Start()
    {
        _Legs = transform.GetChild(1);
        _Neck = transform.GetChild(0);
        _Head = _Neck.GetChild(0);
        _MoveAcceleration = new Vector3();
        _RigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        TickMovement();
    }
    void Update()
    {
        TickMouse();
    }
    public void TickMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            _LookRay = new Ray(_Head.position, _Head.forward);
            Physics.Raycast(_LookRay, out _Lookhit, 1000);
            Physics.gravity = -_Lookhit.normal.normalized*9.8f;
            _TargetRotation = Quaternion.FromToRotation(transform.up, _Lookhit.normal) * transform.rotation;
            Debug.DrawRay(_Head.position, _Head.forward*10,Color.red,10f);
            Debug.DrawRay(_Lookhit.point, _Lookhit.normal*10,Color.green,10f);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, _TargetRotation, 0.1f);
        //transform.Rotate(Vector3.Cross(Physics.gravity, transform.up), Vector3.Dot(transform.forward,Physics.gravity)*Time.deltaTime*10);
    }
    public void TickMovement()
    {
        _GroundRay = new Ray(_Legs.position, -_Legs.up);
        Vector3 _MoveDirection = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            _MoveDirection += _Neck.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _MoveDirection -= _Neck.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _MoveDirection -= _Neck.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _MoveDirection += _Neck.right;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(_GroundRay, out _Groundhit, 0.5f))
        {
            _RigidBody.AddForce(_Groundhit.normal * _JumpPower);
        }
        _MoveAcceleration += _MoveDirection.normalized * _SpeedAcceleration * Time.fixedDeltaTime;
        _MoveAcceleration = _MoveAcceleration * (1 - _Friction);
        _RigidBody.AddForce(_MoveAcceleration);
    }
}
