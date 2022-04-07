using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 _leftDoorClosePos;
    private Vector3 _rightDoorClosePos;
    private Vector3 _leftDoorOpenPos;
    private Vector3 _rightDoorOpenPos;
    private Transform _leftDoor;
    private Transform _rightDoor;
    private bool _open;
    [SerializeField] private List<BasicLogicElement> _logicInputs;
    void Start()
    {
        _leftDoor = transform.GetChild(0);
        _rightDoor = transform.GetChild(1);
        _leftDoorClosePos = _leftDoor.position;
        _rightDoorClosePos = _rightDoor.position;
        _leftDoorOpenPos = _leftDoor.position - _leftDoor.up * 1.5f;
        _rightDoorOpenPos = _rightDoor.position - _rightDoor.up * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
        if (_open)
        {
            _leftDoor.position = Vector3.Slerp(_leftDoor.position, _leftDoorOpenPos, 1f * Time.deltaTime);
            _rightDoor.position = Vector3.Slerp(_rightDoor.position, _rightDoorOpenPos, 1f * Time.deltaTime);
        }
        else
        {
            _leftDoor.position = Vector3.Slerp(_leftDoor.position, _leftDoorClosePos, 3f * Time.deltaTime);
            _rightDoor.position = Vector3.Slerp(_rightDoor.position, _rightDoorClosePos, 3f * Time.deltaTime);
        }
    }
    private void CheckInputs()
    {
        _open = true;
        foreach(BasicLogicElement plate in _logicInputs)
        {
            _open = _open & plate.State;
        }
    }
}
