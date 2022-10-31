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


    [SerializeField] private Button _continueBtn;
    public override void Initialize()
    {
        _startButton.onClick.AddListener(() => GameManager.Play());

        // old
        //_startButton.onClick.AddListener(() => LevelManager.LoadLevel(_startLevel));
        _startButton.onClick.AddListener(() => GameManager.ClearSave());
        _startButton.onClick.AddListener(() => SceneManager.LoadSceneAsync(_startLevel));
        _startButton.onClick.AddListener(() => _viewManager.Show<GameOverlay>());
        _continueBtn.onClick.AddListener(() => GameManager.Play());
        _continueBtn.onClick.AddListener(() => GameManager.Reload());
    }


    private void Awake()
    {
        int saveScene = PlayerPrefs.GetInt("Save_SCENE", 0);

        if (saveScene == 0)
        {
            _continueBtn.enabled = false;
            Text text = _continueBtn.gameObject.GetComponentInChildren<Text>();
            Color textColor = text.color;
            textColor.a = 0f;
            text.color = textColor;

            Color tmpColor = _continueBtn.image.color;
            tmpColor.a = 0f;
            _continueBtn.image.color = tmpColor;

        }
        else
        {
            _continueBtn.enabled = true;
            Text text = _continueBtn.gameObject.GetComponentInChildren<Text>();
            Color textColor = text.color;
            textColor.a = 1f;
            text.color = textColor;

            Color tmpColor = _continueBtn.image.color;
            tmpColor.a = 1f;
            _continueBtn.image.color = tmpColor;
        }
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
