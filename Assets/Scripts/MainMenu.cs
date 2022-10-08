using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : View
{
    [SerializeField] private Button _startButton;
    [SerializeField] private string _startLevel;
    [SerializeField] private ViewManager _viewManager;
    public override void Initialize()
    {
        _startButton.onClick.AddListener(() => GameManager.Play());

        // old
        //_startButton.onClick.AddListener(() => LevelManager.LoadLevel(_startLevel));
        _startButton.onClick.AddListener(() => SceneManager.LoadSceneAsync(_startLevel));
        _startButton.onClick.AddListener(() => _viewManager.Show<GameOverlay>());
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
