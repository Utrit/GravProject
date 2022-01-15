using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTube : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<BasicLogicElement> _logicInputs;
    [SerializeField] GameObject _spawningObject;
    private GameObject _spawnedObject;
    private bool _currectLogicState;
    private bool _prevLogicState;
    private Vector3 _spawningPoint;
    void Start()
    {
        _spawningPoint = transform.GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
        if (_currectLogicState & !_prevLogicState)
        {
            _prevLogicState = _currectLogicState;
            if (_spawnedObject)
            {
                _spawnedObject.transform.position = _spawningPoint;
            }
            else
            {
                _spawnedObject = Instantiate(_spawningObject, _spawningPoint, Quaternion.identity);
            }
        }
        if (!_currectLogicState & _prevLogicState)
        {
            _prevLogicState = false;
        }
    }

    private void CheckInputs()
    {
        _currectLogicState = true;
        foreach(BasicLogicElement logicInput in _logicInputs)
        {
            _currectLogicState = _currectLogicState & logicInput.State;
        }
    }
}
