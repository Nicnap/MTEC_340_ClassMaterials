using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction = new Vector2(1, 1); // Initial movement direction
    [SerializeField] private AudioClip wallHitSound;
    [SerializeField] private AudioClip paddleHitSound;
    private AudioSource _audioSource;

    void Start()
    {
        direction = direction.normalized; // Ensure direction is a unit vector
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 1.0f; // Full volume


        if (_audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on the Ball. Sound effects won't play.");
        }
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
            if (_audioSource != null && wallHitSound != null)
            {
                _audioSource.PlayOneShot(wallHitSound);
            }
            direction.x *= -1; // Reverse Ball X
        }
        if (collision.CompareTag("TopWall"))
        {
            _audioSource.PlayOneShot(wallHitSound);
            direction.y *= -1; // Reverse Ball Y
        }
        if (collision.CompareTag("Paddle"))
        {
            if (_audioSource != null && paddleHitSound != null)
            {
                _audioSource.PlayOneShot(paddleHitSound);
            }
            direction.y *= -1; // Reverse Ball Y direction when hitting the paddle

            // Adjust ball direction based on where it hit the paddle
            float hitPoint = transform.position.x - collision.transform.position.x;
            direction.x += hitPoint * 0.2f; // Change X direction slightly
            direction = direction.normalized; // Normalize to maintain speed
        }
    }
}
