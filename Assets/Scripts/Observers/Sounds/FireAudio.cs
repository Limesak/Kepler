using UnityEngine;

public class FireAudio : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable(){
        Player.OnFireShot += PlayShotAudio;
    }

    private void OnDisable(){
        Player.OnFireShot -= PlayShotAudio;
    }

    private void PlayShotAudio(){
        audioSource.Play();
    }
}
