using UnityEngine;

public class GroundedCheck2D : MonoBehaviour
{
    public float raycastDistance = 0.1f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool isGrounded = IsGrounded();

        if (isGrounded)
        {
            HandleGrounded();
        }
    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, raycastDistance, groundLayer);
        
        if (hit.collider != null)
        {
            // Check if the hit GameObject has a BoxCollider2D and it's a trigger.
            BoxCollider2D boxCollider = hit.collider.GetComponent<BoxCollider2D>();
            if (boxCollider != null && !boxCollider.isTrigger && boxCollider.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0f)
            {
                return true;
            }
        }

        return false;
    }

    private void HandleGrounded()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 position = transform.position;
        Gizmos.DrawLine(position, position - Vector2.up * raycastDistance);
    }
}
