using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : BasicLogicElement
{
    float _pushTime;
    Vector3 _downButtonPos;
    Vector3 _upButtonPos;
    Transform _button;
    // Start is called before the first frame update
    void Start()
    {
        _button = transform.GetChild(0).GetChild(0);
        _upButtonPos = _button.position;
        _downButtonPos = _button.position - _button.up * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_state && Time.timeSinceLevelLoad - _pushTime > 1)
        {
            PushUp();
        }
        _button.position = Vector3.Slerp(_button.position, _state ? _downButtonPos : _upButtonPos, 1f * Time.deltaTime);
    }
    public void PushDown()
    {
        Debug.Log("PushDown");
        _state = true;
        _pushTime = Time.timeSinceLevelLoad;
    }
    private void PushUp()
    {
        _state = false;
    }
}
