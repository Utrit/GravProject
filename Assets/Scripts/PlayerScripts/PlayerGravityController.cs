using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerCarryController))]
public class PlayerGravityController : MonoBehaviour
{
    [SerializeField] private float _gravityAlingSpeed = 360;
    [SerializeField] private int _gravitySurfaceLayer = 9;
    private PlayerContext _playerContext;
    private Transform _head;
    private Ray _lookRay;
    private RaycastHit _lookHit;
    private Quaternion _TargetRotation;
    private PlayerCarryController _carryInfo;

    void Start()
    {
        _playerContext = GetComponent<PlayerContext>();
        _head = _playerContext.Head;
        _gravitySurfaceLayer = (1 << _gravitySurfaceLayer) + (1 << 10);
        _carryInfo = GetComponent<PlayerCarryController>();
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
                Debug.DrawRay(_head.position, _head.forward * 10, Color.red, 10f);
                Debug.DrawRay(_lookHit.point, _lookHit.normal * 10, Color.green, 10f);
            }
            if (Input.GetButtonDown("Fire2"))
            {
                ChangeGravity(-1);
                Debug.DrawRay(_head.position, _head.forward * 10, Color.red, 10f);
                Debug.DrawRay(_lookHit.point, _lookHit.normal * 10, Color.yellow, 10f);
            }
        }
    }

    private void AlingCurGravity()
    {
        _TargetRotation = Quaternion.FromToRotation(transform.up, -Physics.gravity) * transform.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _TargetRotation, _gravityAlingSpeed * Time.deltaTime);
    }

    private void ChangeGravity(int direction)
    {
        _lookRay = new Ray(_head.position, _head.forward);
        if (Physics.Raycast(_lookRay, out _lookHit, 1000, _gravitySurfaceLayer))
        {
            if (_lookHit.collider.gameObject.layer == 10) return;
            if (_carryInfo.isCarryng())
            {
                _carryInfo.CarryObject.GetComponent<ArtifiacalGravinty>().ChangeGravityVector(direction * -_lookHit.normal.normalized * 9.8f);
                _carryInfo.DropObject();
            }
            else
            {
                Physics.gravity = direction * -_lookHit.normal.normalized * 9.8f;
            }
        }
    }

}
