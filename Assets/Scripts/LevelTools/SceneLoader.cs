using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
[RequireComponent(typeof(Canvas))]
public class SceneLoader : MonoBehaviour
{
    Image _blackOut;
    AsyncOperation sceneLoading;
    void Start()
    {
        _blackOut = GetComponentInChildren<Image>();
        _blackOut.DOColor(new Color(0, 0, 0, 0), 1);
    }

    void Update()
    {
    }
    public void LoadScene(string sceneName)
    {
        Physics.gravity = -Vector3.up * 9.8f;
        _blackOut.raycastTarget = true;
        sceneLoading = SceneManager.LoadSceneAsync(sceneName);
        sceneLoading.allowSceneActivation = false;
        _blackOut.DOColor(Color.black, 1)
            .onComplete=(() => sceneLoading.allowSceneActivation = true);
    }
}
