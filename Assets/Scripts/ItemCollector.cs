using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherriesCollected = 0;
    private int totalCherries = 0;
    [SerializeField] public Text cherriesText;
    [SerializeField] private AudioSource collectionSoundEffect;

    private bool displayedMessage = false; // Add a flag to track if message has been displayed

    private void Start()
    {
        // Count total cherries in the scene at the start
        totalCherries = GameObject.FindGameObjectsWithTag("Cherry").Length;

        UpdateCherriesText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            cherriesCollected++;

            UpdateCherriesText();

            // Check if all cherries are collected and show message once
            if (cherriesCollected == totalCherries && !displayedMessage)
            {
                displayedMessage = true;
                ShowMessage("All cherries collected! Proceed to the finish.");
            }
        }
    }

    private void UpdateCherriesText()
    {
        cherriesText.text = "Cherries: " + cherriesCollected + "/" + totalCherries;
    }

    // Method to display the message
    private void ShowMessage(string message)
    {
        // Assuming you have a UI Text element to display the message
        cherriesText.text = message;
    }

    // Add a public method to get the number of collected cherries
    public int GetCollectedCherries()
    {
        return cherriesCollected;
    }

    // Add a public method to get the total number of cherries
    public int GetTotalCherries()
    {
        return totalCherries;
    }
}