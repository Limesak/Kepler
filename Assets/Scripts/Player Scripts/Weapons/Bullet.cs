using System;
//using AsteroidBelt.Enemies_scripts;
using UnityEngine;

namespace AsteroidBelt.Player_Scripts.Weapons
{
    public class Bullet : MonoBehaviour
    {
        public float bulletSpeed = 2f;
        public int damage;
        private Vector2 travelDirection;
        Vector2 previousPos;
        Vector2 newPos;

        public static event Action OnPlayerFireHit;

        private void Start(){
            travelDirection = transform.up;
        }

        public void Update(){
            // Détruit l'objet si sort de l'écran
            if (transform.position.y >= 11)
            {
                Destroy(gameObject);
            }

            DetectCollision();
        }

        public void FixedUpdate(){
            previousPos = transform.position;
            Vector2 direction = transform.position;
            direction += travelDirection * bulletSpeed * Time.deltaTime;
            transform.position = direction;
            newPos = transform.position;

            DetectCollision();
        }

        private void LateUpdate(){
            DetectCollision();
        }

        private void DetectCollision(){
            int mask = 1 << 3;

            RaycastHit hit;

            if(Physics.Linecast(previousPos, newPos, out hit, mask)){
                OnPlayerFireHit?.Invoke();
                DistributeDamage(hit.transform.gameObject);
            }
        }

        public virtual void DistributeDamage(GameObject target){
            target.GetComponent<AsteroidBelt.Enemies_scripts.EnemyHitHandler>().lastHitByOtherEnemy = false;
            target.GetComponent<AsteroidBelt.Enemies_scripts.EnemyHitHandler>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
