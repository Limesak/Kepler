using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldAsteroid : MonoBehaviour {

    public float movementSpeed = 4f;
    private float randomScale;
    public Rigidbody2D rb;
    private int health;
    public GameObject goldPoint;
    public GameObject particles;

    // Use this for initialization
    void Start () {
        randomScale = Random.Range(0.8f, 3f);

        if (randomScale <= 1f)
        {
            health = 2;
        }

        else if (randomScale > 1f && randomScale <= 2f)
        {
            health = 4;
        }

        else if (randomScale > 2f && randomScale <= 3f)
        {
            health = 6;
        }


        transform.localScale = new Vector3(randomScale, randomScale, 1);
        rb.velocity = transform.up * -movementSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            Instantiate(particles, transform.position, transform.rotation);
            if (randomScale <= 1f)
            {
                Instantiate(goldPoint, transform.position + (Vector3) Random.insideUnitCircle*randomScale, transform.rotation);
                Instantiate(goldPoint, transform.position + (Vector3) Random.insideUnitCircle*randomScale, transform.rotation);
            }
            if (randomScale > 1f && randomScale <= 2f)
            {
                Instantiate(goldPoint, transform.position + (Vector3)Random.insideUnitCircle * randomScale, transform.rotation);
                Instantiate(goldPoint, transform.position + (Vector3)Random.insideUnitCircle * randomScale, transform.rotation);
                Instantiate(goldPoint, transform.position + (Vector3)Random.insideUnitCircle * randomScale, transform.rotation);
                Instantiate(goldPoint, transform.position + (Vector3)Random.insideUnitCircle * randomScale, transform.rotation);
            }
            if (randomScale > 2f && randomScale <= 3f)
            {
                Instantiate(goldPoint, transform.position + (Vector3)Random.insideUnitCircle * randomScale, transform.rotation);
                Instantiate(goldPoint, transform.position + (Vector3)Random.insideUnitCircle * randomScale, transform.rotation);
                Instantiate(goldPoint, transform.position + (Vector3)Random.insideUnitCircle * randomScale, transform.rotation);
                Instantiate(goldPoint, transform.position + (Vector3)Random.insideUnitCircle * randomScale, transform.rotation);
                Instantiate(goldPoint, transform.position + (Vector3)Random.insideUnitCircle * randomScale, transform.rotation);
                Instantiate(goldPoint, transform.position + (Vector3)Random.insideUnitCircle * randomScale, transform.rotation);
            }

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
