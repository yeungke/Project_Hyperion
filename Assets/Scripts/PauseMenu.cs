using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : View
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _upgradeMenuButton;
    [SerializeField] private ViewManager _viewManager;

    [SerializeField] private UpgradeListView _upgradeListViewPrefab;

    public override void Initialize()
    {
        _resumeButton.onClick.AddListener(() => GameManager.Play());
        _resumeButton.onClick.AddListener(() => _viewManager.Show<GameOverlay>());

        //_upgradeMenuButton.onClick.AddListener(() => _viewManager.Show<UpgradeListView>());
        _upgradeMenuButton.onClick.AddListener(() => UpgradeListShow());
        _upgradeMenuButton.onClick.AddListener(() => _viewManager.HideView());
        // need to change to work with local 
        //_mainMenuButton.onClick.AddListener(() => SceneManager.LoadSceneAsync(0)); // final
        _mainMenuButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("JordanTestGlobal")); // debug with test levels


        //_mainMenuButton.onClick.AddListener(() => LevelManager.ReturnToMenu());
        //_mainMenuButton.onClick.AddListener(() => ViewManager.Show<MainMenu>());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void UpgradeListShow()
    {
        List<Upgrade> upgrades = UpgradeManager.instance.GetUpgrades();
        UpgradeListView upgradeListView = (UpgradeListView)Instantiate(_upgradeListViewPrefab);
        upgradeListView.Prime(upgrades);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
