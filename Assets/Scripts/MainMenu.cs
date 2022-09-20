using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : View
{
    [SerializeField] private Button _startButton;
    [SerializeField] private string _startLevel;
    public override void Initialize()
    {
        _startButton.onClick.AddListener(() => GameManager.Play());
        _startButton.onClick.AddListener(() => LevelManager.LoadLevel(_startLevel));
        _startButton.onClick.AddListener(() => ViewManager.Show<GameOverlay>());
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
