using UnityEngine;

namespace AsteroidBelt
{
    public abstract class CollectMe : MonoBehaviour
    {
        [HideInInspector] public Player player;
        public string collectibleName;
        public float lifeSpan;

        public void Awake(){
            player = Main.Instance.player.GetComponent<Player>();
        }

        private void Update(){
            lifeSpan -= Time.deltaTime;
            if(lifeSpan <= 0){
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other){
            if(other.gameObject.layer.Equals(6)){
                CollectThis();
                Destroy(gameObject);
            }
        }

        public abstract void CollectThis();
    }
}
