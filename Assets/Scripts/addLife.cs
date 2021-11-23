using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addLife : MonoBehaviour {

    public Rigidbody2D rb;
    private float lifeSpan = 5f;

    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
