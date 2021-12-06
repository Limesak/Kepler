using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitHandler : MonoBehaviour
{
    [Header("Hit handling")]
    public int health;
    [SerializeField] private GameObject particles;
    [SerializeField] private bool canDrop;
    [SerializeField] private bool dropsPoints;
    [SerializeField] private bool isBoss;
    [SerializeField] private GameObject itemDrop;
    public int scoreToGive;

    public void checkDeath(int startingHealth, float randomScale){
        if(health <= 0){
            KillThis(startingHealth, randomScale);
        }
    }

    public void TakeDamage(int receivedDamage){
        health -= receivedDamage;
    }

    public void KillThis(int startingHealth, float randomScale){
        if(canDrop && !dropsPoints){
            int i = Random.Range(1, 4);
            if(i == 2){
                DropThis(startingHealth, randomScale);
            }
        }
        else if(canDrop && dropsPoints){
            DropThis(startingHealth, randomScale);
        }
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void DropThis(int startingHealth, float randomScale){
        if(dropsPoints){
            for(int i = 0; i < (startingHealth * 3); i++){
                Instantiate(itemDrop, transform.position + (Vector3)Random.insideUnitCircle * randomScale, transform.rotation);
            }
        }
        else{
                Instantiate(itemDrop, transform.position, Quaternion.identity);
        }
    }
}