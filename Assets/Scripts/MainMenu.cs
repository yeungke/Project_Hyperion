using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : View
{
    [SerializeField] private Button _startButton;
    public override void Initialize()
    {
        _startButton.onClick.AddListener(() => LevelManager.LoadLevel("TestLevel"));
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
