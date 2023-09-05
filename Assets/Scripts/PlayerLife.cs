using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [SerializeField]private AudioSource deathSoundEffect;

    private void Start()
{
    rb = GetComponent<Rigidbody2D>();
    deathSoundEffect = GetComponent<AudioSource>(); // Assign the AudioSource if not done in the Inspector
    deathSoundEffect.clip.LoadAudioData(); // Preload the audio clip
}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            deathSoundEffect.Play();
            RestartLevel(); // Restart the level immediately
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
