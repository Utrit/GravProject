using System.Collections;
using UnityEngine;
public abstract class BasicLogicElement : MonoBehaviour
{
    protected bool _state;
    public bool State => _state;
}