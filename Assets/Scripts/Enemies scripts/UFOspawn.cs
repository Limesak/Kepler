using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOspawn : MonoBehaviour {

    public GameObject thisUFO;
    private float seconds = 3f;
    float nextSpawn;
    public Transform spawn;

    // Use this for initialization
    void Start()
    {
        nextSpawn = seconds;
    }

    // Update is called once per frame
    void Update()
    {
        nextSpawn -= Time.deltaTime;
        if (nextSpawn <= 0)
        {
            GameObject.Instantiate(
            thisUFO,
            new Vector3(Random.Range(-16, 16), spawn.position.y, 0),
            Quaternion.identity
            );
            nextSpawn = seconds;
            seconds = Random.Range(0.5f, 5);
        }
    }
}
