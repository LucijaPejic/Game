using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private Animator anim;
    private bool levelCompleted = false;
    private Text cherriesText; // Reference to the cherries text element


    // Start is called before the first frame update
    void Start()
    {
        finishSound = GetComponent<AudioSource>();
        cherriesText = FindObjectOfType<ItemCollector>().cherriesText;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            // Get the total number of cherries from the ItemCollector
            int totalCherries = FindObjectOfType<ItemCollector>().GetTotalCherries();

            // Check if all cherries are collected before finishing the level
            if (AreAllCherriesCollected())
            {
                finishSound.Play();
                levelCompleted = true;
                Invoke("CompleteLevel", 0.5f);
            }
            else
            {
                // Display the message to collect all cherries
                cherriesText.text = "Collect all cherries to pass the level!";
            }
        }
    }
    private bool AreAllCherriesCollected()
    {
        // Get all instances of the ItemCollector script in the scene
        ItemCollector[] collectors = FindObjectsOfType<ItemCollector>();

        // Iterate through the collectors and check if all cherries are collected
        foreach (ItemCollector collector in collectors)
        {
            if (collector.GetCollectedCherries() < collector.GetTotalCherries())
            {
                return false; // At least one collector hasn't collected all cherries
            }
        }

        return true; // All collectors have collected all cherries
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}