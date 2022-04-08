using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Collider))]
public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private string _sceneNameToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<SceneLoader>().LoadScene(_sceneNameToLoad);
        }
    }
}
