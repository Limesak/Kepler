using AsteroidBelt.Player_Scripts;
using UnityEngine;
using System.Collections.Generic;

namespace AsteroidBelt.Enemies_scripts.Enemy_Behaviours
{
    public class EnemyScript : EnemyHitHandler
    {
        [Header("Movements")]
        public float speed;
        private Vector2 travelDirection;
        [SerializeField] private float viewDistance;
        [SerializeField] private int numOfRays;
        [SerializeField] private float angle;
        private List<Vector2> directionsToCheck = new List<Vector2>();

        [Header("Damage")]
        private int damage = 1;

        private Transform _transform;
        private GameObject targetPlayer;

        private void Start()
        {
            targetPlayer = main.player;
            _transform = transform;
            travelDirection = -_transform.up;
        }

        void Update()
        {
            DrawRays();
            FindClearPath();
            _transform.localPosition += ((Vector3)FindClearPath() + (Vector3)travelDirection).normalized * speed * Time.deltaTime;

            checkDeath(health, 0f);
        }

        private Vector2 FindClearPath()
        {
            Vector3 Bestdir = -_transform.up;
            float furthestClearPath = 0;
            RaycastHit hit;
            int mask = 1 << 3;

            for (int i = 0; i < directionsToCheck.Count; i++)
            {
                Vector2 dir = _transform.TransformDirection(directionsToCheck[i]);
                if(Physics.Raycast( _transform.position, dir, out hit, viewDistance, mask))
                {
                    if(hit.distance > furthestClearPath)
                    {
                        Bestdir = dir;
                        furthestClearPath = hit.distance;
                    }
                }
                else
                {
                    return dir;
                }
            }

            return Bestdir;
        }

        private void DrawRays(){
            for(int i = 0; i < numOfRays; i++)
            {
                var rotation = _transform.rotation;
                var rotationMod = Quaternion.AngleAxis(i / ((float)numOfRays - 1) * angle * 2 - angle, -_transform.forward);
                var direction = rotation * rotationMod * -_transform.up * 3;
                directionsToCheck.Add(direction);
            }
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
