using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawningBoss : MonoBehaviour {

    public GameObject bossLeader;
    public Transform spawn;

    public void SpawnTheBoss()
    {
        Instantiate(bossLeader, transform.position, transform.rotation);
    } 
}
