using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("No AudioSource found on Brick! Please add one in the Inspector.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (breakSound != null && _audioSource != null)
            {
                _audioSource.PlayOneShot(breakSound);
            }
            else
            {
                Debug.LogError("Break sound or AudioSource is missing!");
            }

            // Delay destruction to allow sound to play
            GetComponent<Collider2D>().enabled = false; // Disable collider to prevent multiple triggers
            GetComponent<SpriteRenderer>().enabled = false; // Hide brick
            Destroy(gameObject, breakSound.length); // Destroy after sound finishes
        }
    }
}
