using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneLoader loader;
    [SerializeField] private GameObject buttonReference;
    [SerializeField] private Transform buttonsFolder;
    private Dictionary<string, string> levelNames = new Dictionary<string, string>()
    {
        {"Assets/ScenesToBuild/0MainMenu.unity",    "Меню"},
        {"Assets/ScenesToBuild/LevelEditor.unity",  "Редактор"},
        {"Assets/ScenesToBuild/SampleScene.unity",  "ПСцена"},
        {"Assets/ScenesToBuild/Test.unity",         "Тест"},
    };
    void Start()
    {
        for(int i= 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = SceneUtility.GetScenePathByBuildIndex(i);
            GameObject button = Instantiate(buttonReference,buttonsFolder);
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(0, i * rectTransform.sizeDelta.y, 0);
            if (levelNames.ContainsKey(sceneName))
            {
                button.GetComponentInChildren<Text>().text = levelNames[sceneName];
            }
            else
            {
                button.GetComponentInChildren<Text>().text = sceneName;
                Debug.LogError($"{sceneName} - dosen't exist name ref!");
            }
            button.GetComponentInChildren<Button>().onClick.AddListener(()=>LoadScene(sceneName));
        }
    }

    
    void Update()
    {
        
    }
    public void LoadScene(string sceneName)
    {
        loader.LoadScene(sceneName);
    }
}
