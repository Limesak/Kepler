using System.Collections;
using AsteroidBelt.Enemies_scripts;
using UnityEngine;

namespace AsteroidBelt.Player_Scripts.Weapons
{
    public class Blast : MonoBehaviour
    {
        [HideInInspector] public int blastDamage;
        [SerializeField] private float expensionTime;
        [SerializeField] private float expensionSpeed;
        [SerializeField] private bool canDamage;
        public GameObject radius;
        float startTime;

        void Start(){
            StartCoroutine(ExpensionCoroutine());
            canDamage = true;
            startTime = Time.time;
        }

        void Update(){
            float blastTimer = (Time.time - startTime) / expensionSpeed;
            Vector3 blastRange = new Vector3( 20, 20, 20);
            radius.transform.localScale = Vector3.Lerp(radius.transform.localScale, blastRange, blastTimer);
        }

        private void OnTriggerEnter(Collider other){
            if(other.gameObject.layer.Equals(3)){
                DistributeDamage(other.gameObject);
            }
        }

        private void DistributeDamage(GameObject target){
            if(canDamage){
                target.transform.parent.gameObject.GetComponent<EnemyHitHandler>().TakeDamage(blastDamage);
            }
        }

        private IEnumerator ExpensionCoroutine(){
            yield return new WaitForSeconds(expensionTime);
            canDamage = false;
            StartCoroutine(StopExplosion());

            IEnumerator StopExplosion(){
                yield return new WaitForSeconds(0.3f);
                Destroy(gameObject);
            }
        }
    }
}
