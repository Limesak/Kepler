using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            other.transform.parent.GetComponent<Player>().PickUpBombs(bombPickedUp);
            Destroy(gameObject);
        }
    }
}
