using UnityEngine;

namespace AsteroidBelt.Enemies_scripts.Enemy_Behaviours
{
    public class Asteroid : EnemyHitHandler
    {
        [Header("Movements")]
        public float movementSpeed = 4f;
        private Vector2 travelDirection;
        private Transform _transform;

        [Header("Properties and stats")]
        private float randomScale;

        [Header("Visual touches")]
        [SerializeField] private GameObject model;
        [SerializeField] private Mesh[] possibleMeshes;
        [SerializeField] private float rotationSpeed;
        Quaternion currentRotation;

        void Start()
        {
            _transform = transform;

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

            _transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            travelDirection = -_transform.up;
        }

        void Update()
        {
            Vector2 previousPos = transform.localPosition;
            Vector2 direction = transform.localPosition;
            direction += travelDirection * movementSpeed * Time.deltaTime;
            _transform.localPosition = direction;
            Vector2 newPos = _transform.localPosition;

            checkDeath(health, randomScale);

            model.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }
}
