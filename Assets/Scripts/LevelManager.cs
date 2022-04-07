using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    [SerializeField] private Scene _globalScene;
    [SerializeField] private Scene _currentScene;
    [SerializeField] private Scene _prevScene;



    //temp stuff for prototype demo
    private GameObject[] _levelObjects;

    public static void LoadLevel(string s)
    {
        SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive);
    }

    private void Awake()
    {
    }


    private void OnEnable()
    {
        _instance = this;

        _globalScene = SceneManager.GetActiveScene();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene newScene, LoadSceneMode mode)
    {
        _currentScene = newScene;

        if (_prevScene.name != null)
        {
            SceneManager.UnloadSceneAsync(_prevScene);
        }
        /*else*/
        if (_currentScene != _globalScene)
        {
            _prevScene = _currentScene;
        }

        SceneManager.SetActiveScene(_currentScene);


        _levelObjects = _currentScene.GetRootGameObjects();
        foreach (GameObject obj in _levelObjects)
        {
            if (obj.name == "Player")
            {
                GameManager.SetPlayer(obj);
            }
        }
    }


    public static void ReturnToMenu()
    {
        _instance._currentScene = _instance._globalScene;
        if (_instance._prevScene != null)
        {
            SceneManager.UnloadSceneAsync(_instance._prevScene);
        }

        SceneManager.SetActiveScene(_instance._globalScene);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
