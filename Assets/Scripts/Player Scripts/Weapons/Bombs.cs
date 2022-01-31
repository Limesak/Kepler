using System.Collections;
using UnityEngine;
using System;

namespace AsteroidBelt.Player_Scripts.Weapons
{
    public class Bombs : Bullet
    {
        [SerializeField] private float timeBeforeExplosion;
        [SerializeField] private GameObject blast;
        

        void Awake(){
            StartCoroutine(TravelAndExplode());
        }

        private IEnumerator TravelAndExplode(){
            yield return new WaitForSeconds(timeBeforeExplosion);
            Explode();
        }

        public override void DistributeDamage(GameObject target)
        {
            base.DistributeDamage(target);
            Explode();
        }

        private void Explode(){
            OnBombExplosion?.Invoke();
            var newBlast = Instantiate(blast, transform.position, Quaternion.identity);
            newBlast.GetComponent<Blast>().blastDamage = damage;
            Destroy(gameObject);
        }

        public static event Action OnBombExplosion;
    }
}
