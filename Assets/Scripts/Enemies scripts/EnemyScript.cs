﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : EnemyHitHandler 
{
    [Header("Movements")]
    public float speed;
    private Vector2 travelDirection;

    [Header("Stats")]
    private int damage = 1;

    private void Start()
    {
        travelDirection = -transform.up;
    }

    // Update is called once per frame
    void Update() {
        Vector2 previousPos = transform.localPosition;
        Vector2 direction = transform.localPosition;
        direction += travelDirection * speed * Time.deltaTime;
        transform.localPosition = direction;
        Vector2 newPos = transform.localPosition;

        checkDeath();
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
