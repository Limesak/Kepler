using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 2f;
    public int damage;
    private Vector2 travelDirection;
    Vector2 previousPos;
    Vector2 newPos;

    public static event Action OnPlayerFireHit;

    private void Start()
    {
        travelDirection = transform.up;
    }

    private void Update(){         
        // Détruit l'objet si sort de l'écran
        if (transform.position.y >= 11)
        {
            Destroy(gameObject);
        }

        DetectCollision();
    }

    private void FixedUpdate(){
        previousPos = transform.localPosition;
        Vector2 direction = transform.localPosition;
        direction += travelDirection * bulletSpeed * Time.deltaTime;
        transform.localPosition = direction;
        newPos = transform.localPosition;
    }

    private void DetectCollision(){
        int mask = 1 << 3;

        RaycastHit[] hits = Physics.RaycastAll(new Ray(previousPos, (newPos - previousPos).normalized), (newPos - previousPos).magnitude, mask);
        for(int i = 0; i < hits.Length; i++){
            RaycastHit hit = hits[i];
            OnPlayerFireHit?.Invoke();
            DistributeDamage(hit.transform.gameObject);
        }
    }

    private void DistributeDamage(GameObject target){
        target.GetComponent<EnemyHitHandler>().SendMessage("TakeDamage", damage);
        Destroy(gameObject);
    }
}
