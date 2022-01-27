using UnityEngine;

namespace AsteroidBelt.Enemies_scripts.Enemy_Behaviours
{
    public class Minion : MonoBehaviour {

        private float speed = 8f;
        bool left = false;
        bool right = false;
        public Rigidbody2D rb;
        private float lifetime = 6f;
	
        // Update is called once per frame
        void Update () {
            if (left)
            {
                rb.velocity = transform.right * speed;
            }

            else if (right)
            {
                rb.velocity = transform.right * -speed;
            }

            lifetime -= Time.deltaTime;
            if (lifetime < 0)
            {
                Destroy(gameObject);
            }
        }

        public void checkLeft()
        {
            left = true;
        }

        public void checkRight()
        {
            right = true;
        }
    }
}
