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
        if (Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - Vector3.up * raycastDistance);
    }
}
