using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadLeader : MonoBehaviour {

    private int health = 30;
    private float protection = 0f;
    public GameObject main;
    public GameObject player;
    public GameObject minion;
    public GameObject shot;
    public Transform leftspawn;
    public Transform rightspawn;
    private float delayMinions = 10f;
    private int randomSpawn;
    private int randomSide;
    private float delayshots = 4f;
    private int doIshoot;


	// Use this for initialization
	void Start () {
		if (main == null)
        {
            main = GameObject.FindWithTag("Main");
            player = GameObject.FindWithTag("Player");
        }
	}
	
	// Update is called once per frame
	void Update () {
        delayMinions -= Time.deltaTime;
        if (delayMinions <= 0f)
        {
            randomSpawn = Random.Range(1, 3);
            if (randomSpawn == 1)
            {
                randomSide = Random.Range(1, 3);
                if (randomSide == 1)
                {
                    GameObject sbire = Instantiate(minion, leftspawn.position, leftspawn.rotation);
                    sbire.GetComponent<Minion>().checkLeft();                    
                }
                else if (randomSide == 2)
                {
                    GameObject sbire = Instantiate(minion, rightspawn.position, leftspawn.rotation);
                    sbire.GetComponent<Minion>().checkRight();
                }
            }
            delayMinions = 3f;
        }

        delayshots -= Time.deltaTime;
        if (delayshots <= 0)
        {
            doIshoot = Random.Range(1, 3);
            if (doIshoot == 1)
            {
                Instantiate(shot, transform.position, transform.rotation);
            }
            delayshots = 3f;
        }

        protection -= Time.deltaTime;

        if (transform.position.y > 4)
        { 
            transform.Translate(Vector3.down * 1.3f * Time.deltaTime, Space.World);
        }

        if (health == 0)
        {
            main.GetComponent<Main>().UpdatePhase();
            main.GetComponent<Main>().resetKills();
            player.GetComponent<Player>().addBossReward();
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" && protection <= 0)
        {
            health--;
            protection = 0.7f;
        }

        if(other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
        }
    }
}
