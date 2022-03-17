using AsteroidBelt.Player_Scripts;
using UnityEngine;
using System.Collections.Generic;

namespace AsteroidBelt.Enemies_scripts.Enemy_Behaviours
{
    public class EnemyScript : EnemyHitHandler
    {
        [Header("Movements")]
        public float maxSpeed;
        private Vector2 travelDirection;
        [SerializeField] private float viewDistance;
        [SerializeField] private int numOfRays;
        [SerializeField] private float angle;
        private List<Vector2> directionsToCheck = new List<Vector2>();

        //[Header("Damage")]

        private Transform _transform;
        private Vector2 velocity;
        private GameObject targetPlayer;

        private void Start()
        {
            targetPlayer = main.player;
            _transform = transform;
            travelDirection = -_transform.up;
            velocity = travelDirection * 5f;
        }

        private void Update()
        {
            Vector2 acceleration = Vector2.zero;

            if(WillCollide())
            {
                Vector2 clearPath = FindClearPath();
                Vector2 steerForce = Steering(clearPath) * 15f;
                acceleration += steerForce;
            }

            velocity += acceleration * Time.deltaTime;
            float speed = velocity.magnitude;
            Vector2 dir = velocity / speed;
            speed = Mathf.Clamp(speed, 4f, maxSpeed);
            velocity = dir * speed;

            _transform.position += (Vector3)velocity * Time.deltaTime;
            _transform.up = -dir;

            checkDeath(health, 0f);
        }

        private bool WillCollide()
        {
            int mask = 1 << 3;
            RaycastHit hit;
            if(Physics.SphereCast(_transform.position, 5f, -_transform.up, out hit, viewDistance, mask))
            {
                Debug.Log("will hit");
                return true;
            }
            else
            {
                return false;
            }
        }

        private Vector2 FindClearPath()
        {
            DrawRays();
            int mask = 1 << 3;

            for (int i = 0; i < directionsToCheck.Count; i++)
            {
                Vector2 dir = _transform.TransformDirection(directionsToCheck[i]);
                Ray ray = new Ray(_transform.position, dir);
                if(!Physics.SphereCast(ray, 5f, viewDistance, mask))
                {
                    return dir;
                }
            }
            return -_transform.up + (Vector3)travelDirection;
        }

        private void DrawRays()
        {   
            var subAngle = angle/(numOfRays * 2);

            for (int i = 0; i <= numOfRays / 2; i++)
            {
                var currentAngle = subAngle * i;
                var direction = Quaternion.AngleAxis(currentAngle, -_transform.forward) * -_transform.up;
                var directionBis = Quaternion.AngleAxis(currentAngle, _transform.forward) * -_transform.up;

                directionsToCheck.Add(direction);
                directionsToCheck.Add(directionBis);
            }
        }

        Vector2 Steering(Vector2 vector)
        {
            Vector2 v = vector.normalized * maxSpeed - velocity;
            return Vector2.ClampMagnitude(v, 3f);
        }
    }
}
