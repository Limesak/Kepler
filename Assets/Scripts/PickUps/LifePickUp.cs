﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickUp : MonoBehaviour {

    public int healthRegained;
    private float lifeSpan = 5f;

    void Update(){
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.layer.Equals(6)){
            other.transform.parent.GetComponent<Player>().GainHealth(healthRegained);
            Destroy(gameObject);
        }
    }
}