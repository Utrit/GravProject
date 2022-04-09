using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneLoader _loader;
    [SerializeField] private GameObject _levelButtonPerfab;
    [SerializeField] private Transform _levelButtonsFolder;
    [SerializeField] private Transform _menuButtonsFolder;
    private Dictionary<string, string> _levelNames = new Dictionary<string, string>()
    {
        {"Assets/ScenesToBuild/0MainMenu.unity",    "Меню"},
        {"Assets/ScenesToBuild/LevelEditor.unity",  "Редактор"},
        {"Assets/ScenesToBuild/SampleScene.unity",  "ПСцена"},
        {"Assets/ScenesToBuild/Test.unity",         "Тест"},
        {"Assets/ScenesToBuild/Room00.unity",         "Room00"},
        {"Assets/ScenesToBuild/Room01.unity",         "Room01"},
        {"Assets/ScenesToBuild/Room02.unity",         "Room02"},
    };
    void Start()
    {
        for(int i= 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = SceneUtility.GetScenePathByBuildIndex(i);
            GameObject button = Instantiate(_levelButtonPerfab, _levelButtonsFolder);
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            if (_levelNames.ContainsKey(sceneName))
            {
                button.GetComponentInChildren<Text>().text = _levelNames[sceneName];
            }
            else
            {
                button.GetComponentInChildren<Text>().text = sceneName;
                Debug.LogError($"{sceneName} - dosen't exist name ref!");
            }
            button.GetComponentInChildren<Button>().onClick.AddListener(()=>LoadScene(sceneName));
        }
    }
    private void HideAllButtons()
    {
        _levelButtonsFolder.gameObject.SetActive(false);
        _menuButtonsFolder.gameObject.SetActive(false);
    }
    public void ShowLevelButtons()
    {
        HideAllButtons();
        _levelButtonsFolder.gameObject.SetActive(true);
    }
    public void ShowMenuButtons()
    {
        HideAllButtons();
        _menuButtonsFolder.gameObject.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadScene(string sceneName)
    {
        _loader.LoadScene(sceneName);
    }
}
