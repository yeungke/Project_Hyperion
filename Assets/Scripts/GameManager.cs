using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    //
    [SerializeField] private int _life = 100;

    private void OnEnable()
    {
        _instance = this;
    }

    public static int LifeUIUpdate()
    {
        return _instance._life;
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
