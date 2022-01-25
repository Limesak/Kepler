using UnityEngine;
using System;

public class BombPickUp : MonoBehaviour
{
    public int bombPickedUp;
    private float lifeSpan = 5f;

    void Update(){
        lifeSpan -= Time.deltaTime;
        if(lifeSpan <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.layer.Equals(6)){
            OnBombPickUp(bombPickedUp);
            Destroy(gameObject);
        }
    }

    public static event Action<int> OnBombPickUp;
}
