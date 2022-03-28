using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidBelt.Enemies_scripts.Enemy_Behaviours
{
    public class WeakSpots : EnemyHitHandler
    {
        public SquadLeader bossScript;
        private bool destroyed;
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
        }

        private void DestroyWeakSpot(){
            bossScript.weakPointsRemaining--;
            destroyed = true;
        }
    }
}