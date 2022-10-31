using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : View
{
    [SerializeField] private Button _reloadBtn;
    [SerializeField] private Button _returnToMenuBtn;

    public override void Initialize()
    {
        _returnToMenuBtn.onClick.AddListener(() => SceneManager.LoadSceneAsync("JordanTestGlobal"));

        //_reloadBtn.onClick.AddListener(() => SceneManager.LoadSceneAsync(PlayerPrefs.GetString("")));
        _reloadBtn.onClick.AddListener(() => GameManager.Reload());
    }

    private void OnEnable()
    {
        int saveScene = PlayerPrefs.GetInt("Save_SCENE", 0);

        if (saveScene == 0)
        {
            _reloadBtn.enabled = false;
            Text text = _reloadBtn.gameObject.GetComponentInChildren<Text>();
            Color textColor = text.color;
            textColor.a = 0f;
            text.color = textColor;

            Color tmpColor = _reloadBtn.image.color;
            tmpColor.a = 0f;
            _reloadBtn.image.color = tmpColor;

        }
        else
        {
            _reloadBtn.enabled = true;
            Text text = _reloadBtn.gameObject.GetComponentInChildren<Text>();
            Color textColor = text.color;
            textColor.a = 1f;
            text.color = textColor;

            Color tmpColor = _reloadBtn.image.color;
            tmpColor.a = 1f;
            _reloadBtn.image.color = tmpColor;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {/*
        int saveScene = PlayerPrefs.GetInt("Save_SCENE", 0);

        if (saveScene == 0)
        {
            _reloadBtn.enabled = false;
        }
        else
        {
            _reloadBtn.enabled = true;
        }*/
    }
}
