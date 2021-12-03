using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldenAsteroidsSpawn : MonoBehaviour {

    public GameObject thisgoldenrock;
    private float seconds = 8f;
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
            thisgoldenrock,
            new Vector3(Random.Range(-16, 16), spawn.position.y, 0),
            Quaternion.identity
            );
            nextSpawn = seconds;
            seconds = Random.Range(2f, 5f);
        }
    }
}
