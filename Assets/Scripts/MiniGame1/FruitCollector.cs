using TMPro;
using UnityEngine;
using UnityEngine.UI;  // For UI Text to show the score

public class FruitCollector : MonoBehaviour
{
    private int score = 0;  // Player's score
    public TMP_Text scoreText;  // UI Text component to display the score

    void Start()
    {
        // Make sure the score text is updated at the beginning
        UpdateScoreUI();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with is tagged as "Fruit"
        if (other.CompareTag("Fruit"))
        {
            // Increase the score when a fruit is collected
            score++;
            UpdateScoreUI();  // Update the score UI

            // Reset the fruit's position or reuse the fruit
            other.GetComponent<FallingFruit>().ResetFruitPosition();
        }
    }

    // Method to update the score UI text
    void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }
}