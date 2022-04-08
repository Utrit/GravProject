using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceTelepor : MonoBehaviour
{
    [SerializeField] private bool _affectGravity;
    private GameObject _otherSurface;
    private float depth;
    private float prevDepth;
    private List<SurfaceTravaler> _objectsTravel;
    void Start()
    {
        _otherSurface = GetComponent<NonecludianSurface>().OtherSurface;
        _objectsTravel = new List<SurfaceTravaler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (SurfaceTravaler travaler in _objectsTravel)
        {
            float depth = Vector3.Dot(transform.forward, transform.position - travaler.TransformLink.position);
            if (depth * travaler.Side < 0.35f)
            {
                if (travaler.RigidbodyLink)
                {
                    Vector3 localVelocity = transform.InverseTransformDirection(travaler.RigidbodyLink.velocity);
                    localVelocity = _otherSurface.transform.TransformDirection(localVelocity);
                    travaler.RigidbodyLink.velocity = localVelocity;
                    if (_affectGravity)
                    {
                        Vector3 localGravity;
                        ArtifiacalGravinty isArtifical = travaler.TransformLink.GetComponent<ArtifiacalGravinty>();
                        if (isArtifical)
                        {
                            localGravity = transform.InverseTransformDirection(isArtifical.GetGravity());
                            localGravity = _otherSurface.transform.TransformDirection(localGravity);
                            isArtifical.ChangeGravityVector(localGravity);
                        }
                        if(travaler.TransformLink.tag=="Player")
                        {
                            localGravity = transform.InverseTransformDirection(Physics.gravity);
                            localGravity = _otherSurface.transform.TransformDirection(localGravity);
                            Physics.gravity = localGravity;
                        }
                    }
                }
                //float scaleDiff = (_otherSurface.transform.localScale - transform.localScale).magnitude;
                Vector3 localCoords = transform.InverseTransformPoint(travaler.TransformLink.position - transform.forward * 0.36f * travaler.Side);
                localCoords = new Vector3(localCoords.x, localCoords.y, -localCoords.z);
                travaler.TransformLink.position = _otherSurface.transform.TransformPoint(localCoords);
                //travaler.TransformLink.localScale /= scaleDiff; 


                Quaternion differenceRotation = _otherSurface.transform.rotation * Quaternion.Inverse(transform.rotation);
                travaler.TransformLink.rotation = differenceRotation * travaler.TransformLink.rotation;

                _objectsTravel.RemoveAll(x => x.TransformLink == travaler.TransformLink);
                
            }
            Debug.Log(depth);
        }
    }
    private void OnTriggerStay(Collider other)
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        int sideEnter = System.Math.Sign(Vector3.Dot(transform.position - other.transform.position, transform.forward));
        _objectsTravel.Add(new SurfaceTravaler(sideEnter, other));
    }
    private void OnTriggerExit(Collider other)
    {
        _objectsTravel.RemoveAll(x => x.TransformLink == other.transform);
    }
}
