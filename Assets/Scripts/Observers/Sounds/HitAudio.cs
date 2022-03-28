using AsteroidBelt.Player_Scripts.Weapons;
using UnityEngine;
using AsteroidBelt.Enemies_scripts;

namespace AsteroidBelt.Observers.Sounds
{
    public class HitAudio : MonoBehaviour
    {
        private AudioSource audioSource;
        public AudioClip bossDamage;

        private void Awake(){
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable(){
            Bullet.OnPlayerFireHit += PlayHitAudio;
            EnemyHitHandler.OnBossHit += PlayBossHit;

        }

        private void OnDisable(){
            Bullet.OnPlayerFireHit -= PlayHitAudio;
            EnemyHitHandler.OnBossHit -= PlayBossHit;
        }

        private void PlayHitAudio(){
            audioSource.Play();
        }

        private void PlayBossHit(){
            audioSource.PlayOneShot(bossDamage, 0.5f);
        }
    }
}
