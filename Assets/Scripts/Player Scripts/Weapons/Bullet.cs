using System;
using AsteroidBelt.Enemies_scripts;
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
            previousPos = transform.localPosition;
            Vector2 direction = transform.localPosition;
            direction += travelDirection * bulletSpeed * Time.deltaTime;
            transform.localPosition = direction;
            newPos = transform.localPosition;

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
            target.GetComponent<EnemyHitHandler>().SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }
    }
}
