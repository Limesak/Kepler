using UnityEngine;
using AsteroidBelt.Player_Scripts;

namespace AsteroidBelt
{
	public class bossShot : MonoBehaviour {
		public int power;

		private void OnTriggerEnter(Collider other){
			if(other.gameObject.layer.Equals(6)){
				other.transform.parent.gameObject.GetComponent<Player>().TakeDamage(power);
			}
		}
	}
}
