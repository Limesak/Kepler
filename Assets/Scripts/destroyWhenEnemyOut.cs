﻿using UnityEngine;

namespace AsteroidBelt
{
    public class destroyWhenEnemyOut : MonoBehaviour {

        // Update is called once per frame
        void Update () {
		
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Coin" || other.gameObject.tag == "Asteroid" || other.gameObject.tag == "goldenAsteroid")
            {
                Destroy(other.gameObject);
            }
        }
    }
}