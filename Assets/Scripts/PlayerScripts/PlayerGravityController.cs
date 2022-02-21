using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerCarryController))]
public class PlayerGravityController : MonoBehaviour
{
    [SerializeField] private float _gravityAlingSpeed = 360;
    [SerializeField] private int _gravitySurfaceLayer = 9;
    private Transform _neck;
    private Transform _Head;
    private Ray _lookRay;
    private RaycastHit _lookHit;
    private Quaternion _TargetRotation;
    ArtifiacalGravinty selfGravity;
    PlayerCarryController carryInfo;

    void Start()
    {
        _neck = transform.GetChild(0);
        _Head = _neck.GetChild(0);
        _gravitySurfaceLayer = (1 << _gravitySurfaceLayer) + (1 << 10);
        carryInfo = GetComponent<PlayerCarryController>();
    }

    void Update()
    {
        TickMouse();
        AlingCurGravity();
    }

    private void TickMouse()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ChangeGravity(1);
                Debug.DrawRay(_Head.position, _Head.forward * 10, Color.red, 10f);
                Debug.DrawRay(_lookHit.point, _lookHit.normal * 10, Color.green, 10f);
            }
            if (Input.GetButtonDown("Fire2"))
            {
                ChangeGravity(-1);
                Debug.DrawRay(_Head.position, _Head.forward * 10, Color.red, 10f);
                Debug.DrawRay(_lookHit.point, _lookHit.normal * 10, Color.yellow, 10f);
            }
        }
        
        //transform.rotation = Quaternion.Slerp(transform.rotation, _TargetRotation, 0.01f);
        //transform.Rotate(Vector3.Cross(Physics.gravity, transform.up), Vector3.Dot(transform.forward,Physics.gravity)*Time.deltaTime*10);
    }

    private void AlingCurGravity()
    {
        _TargetRotation = Quaternion.FromToRotation(transform.up, -Physics.gravity) * transform.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _TargetRotation, _gravityAlingSpeed * Time.deltaTime);
    }

    private void ChangeGravity(int direction)
    {
        _lookRay = new Ray(_Head.position, _Head.forward);
        if (Physics.Raycast(_lookRay, out _lookHit, 1000, _gravitySurfaceLayer))
        {
            if (_lookHit.collider.gameObject.layer == 10) return;
            if (carryInfo.isCarryng())
            {
                carryInfo.CarryObject.GetComponent<ArtifiacalGravinty>().ChangeGravityVector(direction * -_lookHit.normal.normalized * 9.8f);
                carryInfo.DropObject();
            }
            else
            {
                Physics.gravity = direction * -_lookHit.normal.normalized * 9.8f;
            }
        }
    }

}
