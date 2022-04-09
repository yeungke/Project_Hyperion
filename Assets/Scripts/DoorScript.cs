using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private int _enemyCount = 0;
    [SerializeField] private bool _fullOpen = true;
    [SerializeField] private bool _opened = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OpenDoor()
    {
        if (!_fullOpen)
        {
            GameObject lower = this.transform.Find("BottomHalf").gameObject;
            if (lower.GetComponent<BoxCollider2D>() != null)
            {
                lower.GetComponent<BoxCollider2D>().enabled = false;
                lower.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetNumEnemies() <= _enemyCount && !_opened)
        {
            OpenDoor();
        }
    }
}
