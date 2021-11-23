using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossShot : MonoBehaviour {

    public float speed = 10f;
    public Rigidbody2D rb;
    private float lifetime = 3f;

	// Use this for initialization
	void Start () {
        rb.velocity = transform.up * -speed;
	}
	
	// Update is called once per frame
	void Update () {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
	}
}
