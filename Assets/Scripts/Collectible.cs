using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public float movementSpeed = 4f;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.up * -movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
