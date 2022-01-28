using AsteroidBelt.Interfaces;
using UnityEngine;

namespace AsteroidBelt.Enemies_scripts
{
    public class EnemyHitHandler : MonoBehaviour
    {
        [Header("Health and score")]
        public int health;
        public int scoreToGive;
        [SerializeField] private GameObject particles;

        [Header("Attributes")]
        [SerializeField] private bool progessKillCount;
        [SerializeField] private bool canDrop;
        [SerializeField] private bool dropsPoints;
        [SerializeField] private GameObject goldPoints;
        [SerializeField] private GameObject[] itemDrop;
        [SerializeField] private bool isBoss;
        public AudioClip soundWhenTouched;

        Main main;

        private void Awake(){
            main = Main.Instance;
        }

        private void FixedUpdate(){
            if(transform.position.y < -25){
                RemoveFromGame();
            }
        }

        public void checkDeath(int startingHealth, float randomScale){
            if(health <= 0){
                KillThis(startingHealth, randomScale);
            }
        }

        public void TakeDamage(int receivedDamage){
            health -= receivedDamage;
        }

        public void KillThis(int startingHealth, float randomScale){
            main.currentScore += scoreToGive;

            if(canDrop && !dropsPoints){
                int i = Random.Range(1, 4);
                if(i == 2){
                    DropPickUp();
                }
            }

            if(canDrop && dropsPoints){
                DropPoints(startingHealth, randomScale);
            }

            if(progessKillCount){
                main.AddOneKill();
            }

            if(isBoss){
                main.UpdatePhase();
            }

            Instantiate(particles, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        public void DropPickUp(){
            if(main.playerHasBombs){
                int i = Random.Range(0, itemDrop.Length);
                Instantiate(itemDrop[i], transform.position, Quaternion.identity);
            }
            else{
                Instantiate(itemDrop[0], transform.position, Quaternion.identity);
            }
        }

        public void DropPoints(int startingHealth, float randomScale){
            for(int i = 0; i < (startingHealth * 3); i++){
                Instantiate(goldPoints, transform.position + (Vector3)Random.insideUnitCircle * randomScale, transform.rotation);
            }
        }

        private void RemoveFromGame(){
            Destroy(gameObject);
        }
    }
}