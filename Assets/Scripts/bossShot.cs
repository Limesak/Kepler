using UnityEngine;
using AsteroidBelt.Player_Scripts;

namespace AsteroidBelt
{
	public class bossShot : MonoBehaviour {

		public float speed = 10f;
		public int power;
		Vector3 direction;

		void FixedUpdate(){
			transform.position += direction * speed * Time.fixedDeltaTime;
		}

		public void SetDirection(Vector3 dirOfAtk){
			direction = dirOfAtk;
		}

		private void OnTriggerEnter(Collider other){
			if(other.gameObject.layer.Equals(6)){
				other.transform.parent.gameObject.GetComponent<Player>().TakeDamage(power);
			}
		}
	}
}
