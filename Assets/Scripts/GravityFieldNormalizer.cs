using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFieldNormalizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Physics.gravity = -Vector3.up * 9.8f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
