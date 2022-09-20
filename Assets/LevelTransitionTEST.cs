using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionTEST : MonoBehaviour
{
    private bool _triggered = false;
    [SerializeField] private GameObject _LevelChange;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Triggered");

        LevelChanger lc = _LevelChange.GetComponent<LevelChanger>();

        lc.FadeToLevel();
    }
}
