using UnityEngine;

public class FallingFruit : MonoBehaviour
{
    public float fallSpeed = 5f;  // Speed at which the fruit falls
    public float resetHeight = 10f;  // Height from which the fruit will fall
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        // Make the fruit fall
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // If the fruit goes off the screen (below the camera's view), reset it
        if (transform.position.y < -5f)
        {
            ResetFruitPosition();
            //Destroy(gameObject);
        }
    }

    // Reset fruit's position to the top of the screen at a random X position
    public void ResetFruitPosition()
    {
        float randomX = Random.Range(-4f, 4f);  // Randomize position on X-axis
        transform.position = new Vector3(randomX, resetHeight, 0f);  // Reset to top of the screen
    }
}