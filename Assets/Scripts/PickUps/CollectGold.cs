using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidBelt
{
    public class CollectGold : CollectMe 
    {

        public float movementSpeed = 4f;
        public int scoreToGive;
        public Rigidbody rb;

        void Update(){
            if(transform.position.y < -13) RemoveFromGame();
        }

        void Start(){
            rb.velocity = transform.up * -movementSpeed;
        }

        public override void CollectThis(){
            Main.Instance.currentScore += scoreToGive;
        }

        void RemoveFromGame(){
            Destroy(gameObject);
        }
    }
}
