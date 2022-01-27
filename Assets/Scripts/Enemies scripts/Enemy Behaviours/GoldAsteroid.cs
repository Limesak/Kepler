using AsteroidBelt.Player_Scripts;
using UnityEngine;

namespace AsteroidBelt.Enemies_scripts.Enemy_Behaviours
{
    public class GoldAsteroid : EnemyHitHandler 
    {
        [Header("Movements")]
        public float movementSpeed = 4f;
        private Vector2 travelDirection;

        [Header("Properties and stats")]
        private float randomScale;
        private int damage = 1;
        private int startingHealth;

        [Header("Visual touches")]
        [SerializeField] private GameObject model;
        [SerializeField] private float rotationSpeed;
        Quaternion currentRotation;

        void Start () {
            randomScale = Random.Range(0.8f, 3f);
            Vector3 baseRotation = new Vector3(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));        
            currentRotation.eulerAngles = baseRotation;
            model.transform.rotation = currentRotation;

            if (randomScale <= 1f)
            {
                health = 2;
            }

            else if (randomScale > 1f && randomScale <= 2f)
            {
                health = 4;
            }

            else if (randomScale > 2f && randomScale <= 3f)
            {
                health = 6;
            }

            startingHealth = health;
            transform.localScale = new Vector3(randomScale, randomScale, 1);
            travelDirection = -transform.up;
        }
	
        void Update () {
            Vector2 previousPos = transform.localPosition;
            Vector2 direction = transform.localPosition;
            direction += travelDirection * movementSpeed * Time.deltaTime;
            transform.localPosition = direction;
            Vector2 newPos = transform.localPosition;

            checkDeath(startingHealth, randomScale);

            model.transform.Rotate( 0f, 0f, rotationSpeed * Time.deltaTime);
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
}
