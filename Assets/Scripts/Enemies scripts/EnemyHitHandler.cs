using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitHandler : MonoBehaviour
{
    [Header("Hit handling")]
    public int health;
    [SerializeField] private GameObject particles;
    [SerializeField] private bool canDrop;
    [SerializeField] private bool isBoss;
    [SerializeField] private GameObject itemDrop;
    public int scoreToGive;

    public void checkDeath(){
        if(health <= 0){
            KillThis();
        }
    }

    public void TakeDamage(int receivedDamage){
        health -= receivedDamage;
    }

    public void KillThis(){
        if(canDrop){
            int i = Random.Range(1, 4);
            if(i == 2){
                DropThis();
            }
        }
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void DropThis(){
        Instantiate(itemDrop, transform.position, Quaternion.identity);
    }
}