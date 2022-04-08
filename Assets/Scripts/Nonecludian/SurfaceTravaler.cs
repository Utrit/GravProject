using System.Collections;
using UnityEngine;


public class SurfaceTravaler
{
    private int _sideTravel;
    private Collider _colliderLink;

    public int Side => _sideTravel;
    public Transform TransformLink => _colliderLink.transform;
    public Collider ColliderLink => _colliderLink;
    public Rigidbody RigidbodyLink => _colliderLink.attachedRigidbody;
    public SurfaceTravaler(int sideTravel, Collider colliderLink)
    {
        _sideTravel = sideTravel;
        _colliderLink = colliderLink;
    }
}