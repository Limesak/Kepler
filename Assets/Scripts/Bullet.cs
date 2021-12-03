using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 2f;
    public int damage;
    private Vector2 travelDirection;

    private void Start()
    {
        travelDirection = transform.up;
    }

    private void Update()
    {
        Vector2 previousPos = transform.localPosition;
        Vector2 direction = transform.localPosition;
        direction += travelDirection * bulletSpeed * Time.deltaTime;
        transform.localPosition = direction;
        Vector2 newPos = transform.localPosition;

        DetectCollision(previousPos, newPos);

        if (transform.position.y >= 11)
        {
            Destroy(gameObject);
        }
    }

    private void DetectCollision(Vector2 previousPos, Vector2 newPos){
        int mask = 1 << 3;

        RaycastHit[] hits = Physics.RaycastAll(new Ray(previousPos, (newPos - previousPos).normalized), (newPos - previousPos).magnitude, mask);
        for(int i = 0; i < hits.Length; i++){
            RaycastHit hit = hits[i];
            DistributeDamage(hit.transform.gameObject);
        }
    }

    private void DistributeDamage(GameObject target){

    }
}
