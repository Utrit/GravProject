using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    [SerializeField] private Transform _neck;
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _legs;
    [SerializeField] private Transform _menuButtons;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private PlayerMenu _playerMenu;
    public Transform Head => _head;
    public Transform Neck => _neck;
    public Transform Legs => _legs;
    public Transform MenuButtons => _menuButtons;   
    public SceneLoader SceneLoader => _sceneLoader;
    public PlayerMenu PlayerMenu => _playerMenu;
}
