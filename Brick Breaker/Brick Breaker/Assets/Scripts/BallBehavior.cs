using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction = new Vector2(1, 1); // Initial movement direction

    void Start()
    {
        direction = direction.normalized; // Ensure direction is a unit vector
    }

    void Update()
    {
        // Ball Movement
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Bounce off walls and paddle
        if (collision.CompareTag("Wall"))
        {
            direction.x *= -1; // Reverse Ball X
        }
        if (collision.CompareTag("TopWall"))
        {
            direction.y *= -1; // Reverse Ball Y
        }
        if (collision.CompareTag("Paddle"))
        {
            direction.y *= -1; // Reverse Ball Y direction when hitting the paddle

            // Adjust ball direction based on where it hit the paddle
            float hitPoint = transform.position.x - collision.transform.position.x;
            direction.x += hitPoint * 0.2f; // Change X direction slightly
            direction = direction.normalized; // Normalize to maintain speed
        }
    }
}
