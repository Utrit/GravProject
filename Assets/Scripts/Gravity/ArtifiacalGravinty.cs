using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArtifiacalGravinty : MonoBehaviour
{
    private Rigidbody rigidBody;
    [SerializeField] private bool _useGravity = true;
    [SerializeField] private bool _useDefalutGravity = false;
    [SerializeField] private Vector3 _gravity = -Vector3.up*9.8f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidBody.AddForce(_useGravity ? (_useDefalutGravity ? Physics.gravity : _gravity) : Vector3.zero, ForceMode.Acceleration);
    }
    public void UseGravity(bool state)
    {
        _useGravity = state;
    }
    public void ChangeGravityVector(Vector3 direction)
    {
        if (Mathf.Abs(direction.magnitude-9.8f)>0.01f) { Debug.LogWarning("Non standart gravity force"); }
        _gravity = direction;
    }
    public Vector3 GetGravity()
    {
        return _gravity;
    }
}
