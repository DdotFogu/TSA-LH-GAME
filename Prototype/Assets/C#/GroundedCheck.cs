using UnityEngine;

public class GroundedCheck2D : MonoBehaviour
{
    public float raycastDistance = 0.1f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer);

        if (isGrounded)
        {
            HandleGrounded();
        }
    }

    void HandleGrounded()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - Vector3.up * raycastDistance);
    }
}
