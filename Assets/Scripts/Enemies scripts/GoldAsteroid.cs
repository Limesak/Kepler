using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldAsteroid : EnemyHitHandler 
{
    [Header("Movements")]
    public float movementSpeed = 4f;
    private Vector2 travelDirection;

    [Header("Properties and stats")]
    private float randomScale;
    private int damage = 1;
    private int startingHealth;

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

        startingHealth = health;
        transform.localScale = new Vector3(randomScale, randomScale, 1);
        travelDirection = -transform.up;
    }
	
	void Update () {
        Vector2 previousPos = transform.localPosition;
        Vector2 direction = transform.localPosition;
        direction += travelDirection * movementSpeed * Time.deltaTime;
        transform.localPosition = direction;
        Vector2 newPos = transform.localPosition;

        checkDeath(startingHealth, randomScale);
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.layer.Equals(6)){
            DistributeDamage(other.gameObject);
        }
    }

    private void DistributeDamage(GameObject target){
        target.transform.parent.gameObject.GetComponent<Player>().TakeDamage(damage);
    }
}
