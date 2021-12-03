using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    private int health = 2;
    public Rigidbody2D rb;
    public float UFOspeed;
    private int randomBonus;
    public GameObject bonus;

    private void Start()
    {
        rb.velocity = transform.up * -UFOspeed;
        randomBonus = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update() {
        if (health == 0)
        {
            GameObject.FindGameObjectWithTag("Main").GetComponent<Main>().AddOneKill();
            if (randomBonus == 4)
            {
                Instantiate(bonus, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health--;
            Destroy(other.gameObject);
        }
    }
}
