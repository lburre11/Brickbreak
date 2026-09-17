using UnityEngine;

public class paddleMovement : MonoBehaviour

{
    public float Speed = 5.0f;
    public KeyCode LeftDirection = KeyCode.A ;
    public KeyCode RightDirection = KeyCode.D;

    private Rigidbody2D rb;
    private float movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        movement = 0.0f;
        
        if (Input.GetKey(LeftDirection))
        {
            movement -= Speed;
        }
        if (Input.GetKey(RightDirection))
        {
            movement += Speed;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement, rb.linearVelocity.y);
    }
}
