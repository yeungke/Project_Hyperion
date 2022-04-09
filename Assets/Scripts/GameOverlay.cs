using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverlay : View
{
    [SerializeField] private Button _pauseButton;

    [SerializeField] private Text _lifeText;


    public override void Initialize()
    {
        _pauseButton.onClick.AddListener(() => ViewManager.Show<PauseMenu>());
        _pauseButton.onClick.AddListener(() => GameManager.Pause());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _lifeText.text = "Life: " + GameManager.LifeUIUpdate().ToString(); ;
    }
}
