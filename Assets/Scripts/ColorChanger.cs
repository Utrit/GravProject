using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Material _offEmission;
    [SerializeField] private Material _onEmission;
    private Renderer[] _renderers;
    void Start()
    {
        TurnOff();
    }
    void switchMat(Material matterial)
    {
        _renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in _renderers)
        {
            Material[] mats = renderer.sharedMaterials;
            for (int i = 0; i < mats.Length; i++)
            {
                if (mats[i].IsKeywordEnabled("_EMISSION"))
                {
                    mats[i] = matterial;
                }
            }
            renderer.sharedMaterials = mats;
        }
    }
    public void TurnOn()
    {
        switchMat(_onEmission);
    }
    public void TurnOff()
    {
        switchMat(_offEmission);
    }
}
