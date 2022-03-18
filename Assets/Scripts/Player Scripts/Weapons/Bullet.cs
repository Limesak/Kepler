using System;
//using AsteroidBelt.Enemies_scripts;
using UnityEngine;
using AsteroidBelt.Enemies_scripts.Enemy_Behaviours;

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
            int mask = 1 << 3 | 1 << 7;

            RaycastHit hit;

            if(Physics.Linecast(previousPos, newPos, out hit, mask)){

                switch (hit.transform.gameObject.layer){
                    case 3:
                        OnPlayerFireHit?.Invoke();
                        if(hit.transform.gameObject.tag.Equals("Boss") && hit.transform.parent.gameObject.GetComponent<SquadLeader>()){
                            DistributeDamage(hit.transform.parent.gameObject);
                        }
                        else{
                            DistributeDamage(hit.transform.gameObject);
                        }
                        break;
                    case 7:
                        OnPlayerFireHit?.Invoke();
                        Destroy(gameObject);
                        break;
                }
            }
        }

        public virtual void DistributeDamage(GameObject target){
            target.GetComponent<AsteroidBelt.Enemies_scripts.EnemyHitHandler>().lastHitByOtherEnemy = false;
            target.GetComponent<AsteroidBelt.Enemies_scripts.EnemyHitHandler>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
