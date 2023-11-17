using UnityEngine;

public class Movement : MonoBehaviour
{
    private bool inair = false;
    [SerializeField] float moveSpeed;
    private float horizontalMovement;
    public float lastHorizontalMovement;
    [SerializeField] float jumpForce;
    [SerializeField] KeyCode jumpKey;
    Rigidbody2D rb;
    public GameObject pivot;
    public Animator ani;

    [Header("Raycast")]
    [SerializeField] float distance;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector2 boxSize;

    [Header("Coyote Timer")]
    [SerializeField] float coyoteTime = 0.1f;
    private float coyoteTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (horizontalMovement != 0)
        {
            lastHorizontalMovement = horizontalMovement;
        }

        if (this.GetComponent<Rigidbody2D>().velocity.y > 7)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 7);
        }
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        Animation();

        if (isGrounded())
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        MovementHandling();
    }

    void Animation()
    {
        if (horizontalMovement == 1)
        {
            pivot.transform.localScale = new Vector2(-1, 1);
        }
        if (horizontalMovement == -1)
        {
            pivot.transform.localScale = new Vector2(1, 1);
        }
        
            
        if (horizontalMovement == 0)
        {
            ani.SetBool("Walking", false);
        }
        else
        {
            ani.SetBool("Walking", true);
        }
        if (Input.GetKeyDown(jumpKey) && (isGrounded() || coyoteTimer > 0))
        {
            ani.SetTrigger("Jump");
            coyoteTimer = 0;
        }
        if (GetComponent<Rigidbody2D>().velocity.y < 0 && inair)
        {
            ani.SetBool("Falling", true);
        }
        if (inair && Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, distance, groundLayer))
        {
            FindObjectOfType<AudioManager>().Play("Land");
        }
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, distance, groundLayer))
        {
            ani.SetBool("Falling", false);
        }
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, distance, groundLayer))
        {
            inair = false;
            return true;
        }
        else
        {
            inair = true;
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * distance, boxSize);
    }

    void MovementHandling()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed * Time.deltaTime, rb.velocity.y);
        if (Input.GetKeyDown(jumpKey) && isGrounded())
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
