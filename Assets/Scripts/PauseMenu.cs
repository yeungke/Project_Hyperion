using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : View
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    public override void Initialize()
    {
        _resumeButton.onClick.AddListener(() => GameManager.Play());
        _resumeButton.onClick.AddListener(() => ViewManager.Show<GameOverlay>());
        _mainMenuButton.onClick.AddListener(() => LevelManager.ReturnToMenu());
        _mainMenuButton.onClick.AddListener(() => ViewManager.Show<MainMenu>());
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
