using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particules : MonoBehaviour {

    private float lifespan = 1.5f;

	// Update is called once per frame
	void Update () {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(gameObject);
        }
	}
}
