using AsteroidBelt.Player_Scripts;
using AsteroidBelt.Player_Scripts.Weapons;
using UnityEngine;

namespace AsteroidBelt.Observers.Sounds
{
    public class FireAudio : MonoBehaviour
    {
        private AudioSource audioSource;
        [SerializeField] private AudioClip simpleShotsClip;
        [SerializeField] private AudioClip dashClip;
        [SerializeField] private AudioClip bombShotClip;
        [SerializeField] private AudioClip bombExplosionClip;

        private void Awake(){
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable(){
            Player.OnFireShot += PlayShotAudio;
            Player.OnDashPerformed += PlayDashAudio;
            Bombs.OnBombExplosion += PlayExplosionAudio;
        }

        private void OnDisable(){
            Player.OnFireShot -= PlayShotAudio;
            Player.OnDashPerformed -= PlayDashAudio;
            Bombs.OnBombExplosion -= PlayExplosionAudio;
        }

        private void PlayShotAudio(){
            audioSource.PlayOneShot(simpleShotsClip, 0.2f);
        }

        private void PlayDashAudio(){
            audioSource.PlayOneShot(dashClip);
        }

        private void PlayExplosionAudio(){
            audioSource.PlayOneShot(bombExplosionClip);
        }
    }
}
