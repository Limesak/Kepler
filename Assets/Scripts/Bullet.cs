using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 2f;
    public Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb.velocity = transform.up * bulletSpeed;
    }

    private void Update()
    {
        if (transform.position.y >= 11)
        {
            Destroy(gameObject);
        }
    }
}
