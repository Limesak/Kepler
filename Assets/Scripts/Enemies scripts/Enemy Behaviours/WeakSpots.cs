using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidBelt.Enemies_scripts.Enemy_Behaviours
{
    public class WeakSpots : EnemyHitHandler
    {
        public SquadLeader bossScript;

        public override void TakeDamage(int receivedDamage){
            bossScript.TakeDamage(receivedDamage);
        }
    }
}
