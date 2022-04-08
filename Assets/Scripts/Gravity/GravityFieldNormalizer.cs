using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFieldNormalizer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 gravityNormal;
    void Start()
    {
        if (gravityNormal.magnitude < 1) { gravityNormal = -transform.up * 9.8f; }
    }
    private void OnTriggerEnter(Collider other)
    {
        ArtifiacalGravinty isArtifical = other.GetComponent<ArtifiacalGravinty>();
        if (isArtifical)
        {
            isArtifical.ChangeGravityVector(gravityNormal);
            return;
        }
        if (other.tag == "Player")
        {
            Physics.gravity = gravityNormal;
            return;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
