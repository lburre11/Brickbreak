using UnityEngine;

public class balls : MonoBehaviour
{

    public float minY = -5.5f;
    public float maxVelocity = 15f;
    private Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
        {
            transform.position = new Vector3(Random.Range(-3.58f, 3.58f), Random.Range(0.0f, 3.7f), 0.0f);
            rb.linearVelocity = Vector2.zero;
        }

        if (rb.linearVelocity.magnitude > maxVelocity)
        {
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxVelocity);
        }
    }
}
