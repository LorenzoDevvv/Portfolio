using UnityEngine;


public class SoundPlayer : MonoBehaviour
{
    public AudioClip soundClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundClip;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Debug.Log("Space pressed");
        //    PlaySound();
        //}
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}
