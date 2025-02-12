using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 8f; // speed paddle moves
    private float screenLimit = 5.14f; // boundary for paddle

    void Update()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.position += new Vector3(move, 0, 0);

        // Keep paddle within the screen boundaries
        float clampedX = Mathf.Clamp(transform.position.x, -screenLimit, screenLimit);
        transform.position = new Vector3(clampedX, transform.position.y, 0);
    }
}

