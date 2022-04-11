using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBlock : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
            DestroyObject();
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
