using AsteroidBelt.Interfaces;
using UnityEngine;

namespace AsteroidBelt.PickUps
{
    public class CollectGold : CollectMe
    {

        public float movementSpeed = 4f;
        public int scoreToGive;
        public Rigidbody rb;

        void Update()
        {
            if (transform.position.y < -13) RemoveFromGame();
        }

        void Start()
        {
            rb.velocity = transform.up * -movementSpeed;
        }

        public override void CollectThis()
        {
            Main.Instance.currentScore += scoreToGive;
        }

        void RemoveFromGame()
        {
            Destroy(gameObject);
        }
    }
}
