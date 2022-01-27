using AsteroidBelt.Player_Scripts.Weapons;
using UnityEngine;

namespace AsteroidBelt.Observers.Sounds
{
    public class HitAudio : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Awake(){
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable(){
            Bullet.OnPlayerFireHit += PlayHitAudio;
        }

        private void OnDisable(){
            Bullet.OnPlayerFireHit -= PlayHitAudio;
        }

        private void PlayHitAudio(){
            audioSource.Play();
        }
    }
}
