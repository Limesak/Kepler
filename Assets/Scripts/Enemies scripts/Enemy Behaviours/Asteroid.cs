using AsteroidBelt.Player_Scripts;
using UnityEngine;

namespace AsteroidBelt.Enemies_scripts.Enemy_Behaviours
{
    public class Asteroid : EnemyHitHandler
    {
        [Header("Movements")]
        public float movementSpeed = 4f;
        private Vector2 travelDirection;

        [Header("Properties and stats")]
        private float randomScale;
        private int damage = 1;

        [Header("Visual touches")]
        [SerializeField] private GameObject model;
        [SerializeField] private Mesh[] possibleMeshes;
        [SerializeField] private float rotationSpeed;
        Quaternion currentRotation;

        void Start()
        {
            model.GetComponent<MeshFilter>().mesh = possibleMeshes[(Random.Range(0, possibleMeshes.Length))];

            randomScale = Random.Range(3f, 9f);
            Vector3 baseRotation = new Vector3(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
            currentRotation.eulerAngles = baseRotation;
            model.transform.rotation = currentRotation;

            if (randomScale <= 4f)
            {
                health = 2;
            }

            else if (randomScale > 4f && randomScale < 6f)
            {
                health = 4;
            }

            else if (randomScale >= 6f)
            {
                health = 6;
            }

            transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            travelDirection = -transform.up;
        }

        void Update()
        {
            Vector2 previousPos = transform.localPosition;
            Vector2 direction = transform.localPosition;
            direction += travelDirection * movementSpeed * Time.deltaTime;
            transform.localPosition = direction;
            Vector2 newPos = transform.localPosition;

            checkDeath(health, randomScale);

            model.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
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
