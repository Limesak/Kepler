using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float movementSpeed = 4f;
    private float randomScale;
    public Rigidbody2D rb;
    private int health;
    public GameObject particles;

    // mouvement vers le bas
    void Start()
    {
        randomScale = Random.Range(3f, 9f);

        if (randomScale <= 4f)
        {
            health = 2;
        }

        else if (randomScale > 4f && randomScale < 6f)
        {
            health = 4;
        }

        else if (randomScale >= 6f)
        {
            health = 6;
        }


        transform.localScale = new Vector3(randomScale, randomScale, 1);
        rb.velocity = transform.up * -movementSpeed;
    }

    void Update()
    {
        if (health <= 0)
        {
            Instantiate(particles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

// Destroy bullet on collision and applies damage
void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health--;
            Destroy(other.gameObject);
        }
    }
}
