using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTube : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<BasicLogicElement> _logicInputsSpawn;
    [SerializeField] List<BasicLogicElement> _logicInputsDestroy;
    [SerializeField] GameObject _spawningObject;
    private GameObject _spawnedObject;
    private bool _currectLogicState;
    private bool _prevLogicState;
    private Vector3 _spawningPoint;
    private ColorChanger _colorChanger;
    void Start()
    {
        _spawningPoint = transform.GetChild(0).position;
        _colorChanger = GetComponent<ColorChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
        Spawn();
    }

    private void CheckInputs()
    {
        _currectLogicState = true;
        foreach(BasicLogicElement logicInput in _logicInputsSpawn)
        {
            _currectLogicState = _currectLogicState & logicInput.State;
        }
    }

    private void Spawn()
    {
        if (_currectLogicState & !_prevLogicState)
        {
            _prevLogicState = _currectLogicState;
            _colorChanger.TurnOn();
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
}
