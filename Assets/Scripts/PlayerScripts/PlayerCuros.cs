using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCuros : MonoBehaviour
{
    [SerializeField] private Mesh _cursorMesh;
    [SerializeField] private Material _cursorMaterial;
    [SerializeField] private int _cursorLayer;
    private PlayerContext _playerContext;
    void Start()
    {
        _playerContext = GetComponent<PlayerContext>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(_playerContext.Head.transform.position,_playerContext.Head.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject.layer == _cursorLayer)
            {
                MaterialPropertyBlock mat = new MaterialPropertyBlock();
                Graphics.DrawMesh(_cursorMesh, hit.point + hit.normal * 0.1f, Quaternion.FromToRotation(Vector3.up, hit.normal), _cursorMaterial, 1, Camera.main, 0, mat, false);
            }
        }
    }
}
