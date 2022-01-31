using AsteroidBelt.Player_Scripts;
using UnityEngine;
using System.Collections.Generic;

namespace AsteroidBelt.Enemies_scripts.Enemy_Behaviours
{
    public class EnemyScript : EnemyHitHandler
    {
        public int multiplier = 3;

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
            _transform.up = -FindClearPath();
            //_transform.localPosition += (Vector3)FindClearPath().normalized * speed * Time.deltaTime;
            _transform.localPosition += -_transform.up.normalized * speed * Time.deltaTime;

            checkDeath(health, 0f);
        }

        private Vector2 FindClearPath()
        {
            Vector3 Bestdir = travelDirection;
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

        private void DrawRays()
        {
            //directionsToCheck.Add(-_transform.up);            

            var subAngle = angle/(numOfRays * 2);

            for (int i = 0; i <= numOfRays / 2; i++)
            {
                var currentAngle = subAngle * i;
                //var rotationMod = Quaternion.AngleAxis(currentAngle, -_transform.forward);
                //var rotationModBis = Quaternion.AngleAxis(currentAngle, _transform.forward);

                var rotationVector = currentAngle * _transform.forward;
                var rotation = Quaternion.Euler(rotationVector);

                var rotationVectorBis = currentAngle * -_transform.forward;
                var rotationBis = Quaternion.Euler(rotationVectorBis);

                //var direction = rotationMod.normalized * -_transform.up * multiplier;
                //var directionBis = rotationModBis.normalized * -_transform.up * multiplier;

                var direction = rotation * -_transform.up;
                var directionBis = rotationBis * -_transform.up;

                //print(direction);
                //print(rotation);

                directionsToCheck.Add(direction);
                directionsToCheck.Add(directionBis);
            }

            /*
            for(int i = 0; i < numOfRays; i++)
            {
                var rotation = _transform.rotation;
                var rotationMod = Quaternion.AngleAxis(i / ((float)numOfRays - 1) * angle * 2 - angle, -_transform.forward);
                var direction = rotation * rotationMod * -_transform.up;
                directionsToCheck.Add(direction);
            }
            */

            /*foreach(Vector2 directionToCheck in directionsToCheck){
                print(directionToCheck);
            }*/
        }

        private void OnDrawGizmos(){
            Gizmos.color = Color.yellow;
            for (int i = 0; i < directionsToCheck.Count; i++)
            {
                Gizmos.DrawRay( _transform.position, directionsToCheck[i] * viewDistance);                
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
