using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator _animator;
    [SerializeField] private string _level;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel()
    {
        _animator.SetTrigger("Fade_out");
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(_level);
    }
}
