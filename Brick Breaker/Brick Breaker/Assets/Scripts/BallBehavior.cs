using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction; // Direction of the ball
    [SerializeField] private AudioClip wallHitSound;
    [SerializeField] private AudioClip paddleHitSound;
    private AudioSource _audioSource;

    void Start()
    {
        // Generate a random start direction
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(0f, 1f); // Ensure Y is always positive

        direction = new Vector2(randomX, randomY).normalized; // Normalize to keep the speed constant

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

        // Bounce off bricks
        if (collision.CompareTag("Brick"))
        {
            // Calculate X and Y distances between the ball and the brick
            float xDistance = Mathf.Abs(transform.position.x - collision.transform.position.x);
            float yDistance = Mathf.Abs(transform.position.y - collision.transform.position.y);
            GameBehavior.Instance.IncreaseScore(10);
            

            // Compare distances and bounce accordingly
            if (xDistance > yDistance)
            {
                // Bounce on X axis
                direction.x *= -1;
            }
            else
            {
                // Bounce on Y axis
                direction.y *= -1;
            }

            // Play wall hit sound if a brick was hit
            if (_audioSource != null && wallHitSound != null)
            {
                _audioSource.PlayOneShot(wallHitSound);
            }

            // Normalize the direction to maintain the speed after bounce
            direction = direction.normalized;
        }
    }
}
