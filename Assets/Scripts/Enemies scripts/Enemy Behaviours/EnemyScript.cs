using AsteroidBelt.Player_Scripts;
using UnityEngine;

namespace AsteroidBelt.Enemies_scripts.Enemy_Behaviours
{
    public class EnemyScript : EnemyHitHandler
    {
        [Header("Movements")]
        public float speed;
        private Vector2 travelDirection;

        [Header("Damage")]
        private int damage = 1;

        private void Start()
        {
            travelDirection = -transform.up;
        }

        void Update()
        {
            Vector2 previousPos = transform.localPosition;
            Vector2 direction = transform.localPosition;
            direction += travelDirection * speed * Time.deltaTime;
            transform.localPosition = direction;
            Vector2 newPos = transform.localPosition;

            checkDeath(health, 0f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer.Equals(6))
            {
                DistributeDamage(other.gameObject);
            }
        }

        private void DistributeDamage(GameObject target)
        {
            target.transform.parent.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
