using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidBelt.Enemies_scripts.Enemy_Behaviours
{
    public class WeakSpots : EnemyHitHandler
    {
        public SquadLeader bossScript;
        private bool destroyed;
        [SerializeField] private ParticleSystem partSystem;
        [SerializeField] private int weakSpotHealth;

        public override void TakeDamage(int receivedDamage){
            Debug.Log("weak spot shot");
            if(!destroyed){
                weakSpotHealth -= receivedDamage;
            }
        }
        
        private void Update(){
            if(weakSpotHealth <= 0 && !destroyed){
                DestroyWeakSpot();
            }

            var emission = partSystem.emission;

            emission.enabled = destroyed;
        }

        private void DestroyWeakSpot(){
            bossScript.weakPointsRemaining--;
            destroyed = true;
        }
    }
}