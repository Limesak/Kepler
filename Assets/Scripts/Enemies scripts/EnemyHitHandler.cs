using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitHandler : MonoBehaviour
{
    [Header("Health and score")]
    public int health;
    public int scoreToGive;
    [SerializeField] private GameObject particles;

    [Header("Attributes")]
    [SerializeField] private bool progessKillCount;
    [SerializeField] private bool canDrop;
    [SerializeField] private bool dropsPoints;
    [SerializeField] private GameObject itemDrop;
    [SerializeField] private bool isBoss;
    public AudioClip soundWhenTouched;

    Main main;

    private void Awake(){
        main = Main.Instance;
    }

    public void checkDeath(int startingHealth, float randomScale){
        if(health <= 0){
            KillThis(startingHealth, randomScale);
        }
    }

    public void TakeDamage(int receivedDamage){
        health -= receivedDamage;
    }

    public void KillThis(int startingHealth, float randomScale){
        main.currentScore += scoreToGive;

        if(canDrop && !dropsPoints){
            int i = Random.Range(1, 4);
            if(i == 2){
                DropThis(startingHealth, randomScale);
            }
        }
        else if(canDrop && dropsPoints){
            DropThis(startingHealth, randomScale);
        }

        if(progessKillCount){
            main.AddOneKill();
        }

        if(isBoss){
            main.UpdatePhase();
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