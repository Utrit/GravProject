using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMenu : MonoBehaviour
{
    private PlayerContext _playerContext;
    void Start()
    {
        _playerContext = GetComponent<PlayerContext>();
        Debug.Log(_playerContext.SceneLoader);
    }

    public void ShowButtons()
    {
        _playerContext.MenuButtons.gameObject.SetActive(true);
    }
    public void HideButtons()
    {
        _playerContext.MenuButtons.gameObject.SetActive(false);
    }
    public void LoadMainMenu()
    {
        _playerContext.SceneLoader.LoadScene(SceneUtility.GetScenePathByBuildIndex(0));
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
