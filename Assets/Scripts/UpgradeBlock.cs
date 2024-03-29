using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBlock : MonoBehaviour
{
    public Upgrades _upgrade;
    [SerializeField] private GameObject _prompt;
    /*public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collide");
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {*//*
            //Debug.Log("break");
            if (!UpgradeManager.instance.HasUpgrade(_upgrade))
            {
                UpgradeManager.instance.AddUpgrade(_upgrade);

                //temp
                UpgradeManager.instance.EnableUpgrade(_upgrade);
            }
            DestroyObject();*//*
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            if (!_prompt.activeSelf)
            {
                _prompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            if (_prompt.activeSelf)
            {
                _prompt.SetActive(false);
            }
        }
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (_prompt.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("break");
                if (!UpgradeManager.instance.HasUpgrade(_upgrade))
                {
                    UpgradeManager.instance.AddUpgrade(_upgrade);

                    //temp
                    UpgradeManager.instance.EnableUpgrade(_upgrade);
                }
                DestroyObject();
            }
        }
    }
    private void Start()
    {
        if (_upgrade == Upgrades.NULL)
        {
            DestroyObject();
        }
        else if (UpgradeManager.instance.HasUpgrade(_upgrade))
        {
            Debug.Log("Destroy this");
            DestroyObject();
        }
        
    }
}
