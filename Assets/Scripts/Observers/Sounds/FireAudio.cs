using AsteroidBelt.Player_Scripts;
using UnityEngine;

namespace AsteroidBelt.Observers.Sounds
{
    public class FireAudio : MonoBehaviour
    {
        private AudioSource audioSource;
        [SerializeField] private AudioClip simpleShotsClip;
        [SerializeField] private AudioClip dashClip;
        [SerializeField] private AudioClip bombShotClip;

        private void Awake(){
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable(){
            Player.OnFireShot += PlayShotAudio;
            Player.OnDashPerformed += PlayDashAudio;
        }

        private void OnDisable(){
            Player.OnFireShot -= PlayShotAudio;
            Player.OnDashPerformed -= PlayDashAudio;
        }

        private void PlayShotAudio(){
            audioSource.PlayOneShot(simpleShotsClip, 0.2f);
        }

        private void PlayDashAudio(){
            audioSource.PlayOneShot(dashClip);
        }
    }
}
