using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour {

    public float movementSpeed = 4f;
    public int scoreToGive;
    public Rigidbody rb;
    Main main;

    void Awake(){
        main = Main.Instance;
    }

    void Update(){
        if(transform.position.y < -13) RemoveFromGame();
    }

    void Start(){
        rb.velocity = transform.up * -movementSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer.Equals(6)){
            main.currentScore += scoreToGive;
            Destroy(gameObject);
        }
    }

    void RemoveFromGame(){
        Destroy(gameObject);
    }
}
