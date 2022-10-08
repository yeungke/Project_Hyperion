using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverlay : View
{
    [SerializeField] private Button _pauseButton;

    [SerializeField] private Text _lifeText;

    [SerializeField] private ViewManager _viewManager;


    public override void Initialize()
    {
        _pauseButton.onClick.AddListener(() => _viewManager.Show<PauseMenu>());
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

        if (Input.GetButtonDown("Cancel"))
        {
            _viewManager.Show<PauseMenu>();
            GameManager.Pause();
        }
    }
}
