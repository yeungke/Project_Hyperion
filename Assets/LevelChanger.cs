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

    public void SetLevel(string level)
    {
        _level = level;
    }

    public void FadeToLevel(SpawnLocations spawn)
    {
        PlayerPrefs.SetInt("Spawn", (int) spawn);
        _animator.SetTrigger("Fade_out");
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(_level);
    }

    private void Awake()
    {
        GameObject player = GameObject.Find("Player");

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
        if (player != null && PlayerPrefs.HasKey("Spawn"))
        {
            bool spawnPointFound = false;

            SpawnLocations spawn = (SpawnLocations)PlayerPrefs.GetInt("Spawn");
            SpawnLocations targetSpawn;

            switch (spawn)
            {
                case SpawnLocations.RIGHT:
                    targetSpawn = SpawnLocations.LEFT;
                    break;
                case SpawnLocations.LEFT:
                    targetSpawn = SpawnLocations.RIGHT;
                    break;
                case SpawnLocations.UP:
                    targetSpawn = SpawnLocations.DOWN;
                    break;
                case SpawnLocations.DOWN:
                    targetSpawn = SpawnLocations.UP;
                    break;
                case SpawnLocations.HIDDEN:
                    targetSpawn = SpawnLocations.HIDDEN;
                    break;
                default:
                    targetSpawn = SpawnLocations.DEFAULT;
                    break;
            }

            //rename eventually
            LevelTransitionTEST[] spawns = (LevelTransitionTEST[]) GameObject.FindObjectsOfType<LevelTransitionTEST>();
            LevelTransitionTEST defaultSpawn = null;
            foreach (var spawnPoint in spawns)
            {
                if (spawnPoint.GetSpawn() == targetSpawn)
                {
                    player.gameObject.transform.position = spawnPoint.transform.position;
                    spawnPointFound = true;
                    break;
                }
                else if (spawnPoint.GetSpawn() == SpawnLocations.DEFAULT)
                {
                    defaultSpawn = spawnPoint;
                }
            }
            if (!spawnPointFound && defaultSpawn != null)
            {
                player.gameObject.transform.position = defaultSpawn.transform.position;
                        
            }
        }
    }
}
