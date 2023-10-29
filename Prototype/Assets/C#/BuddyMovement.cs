using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BuddyMovement : MonoBehaviour
{
    [SerializeField] private string buddyState;
    public Animator playerAni;
    private Animator ani;
    private bool abilityState;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private float horizontalMovement;

    [Header("Ground Raycast")]
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundBoxSize;

    [Header("Wall Raycast")]
    [SerializeField] private Vector2 wallDistance;
    [SerializeField] private Vector2 wallBoxSize;
    [SerializeField] private Vector2 wall2Distance;
    [SerializeField] private Vector2 wall2BoxSize;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        abilityState = GameObject.Find("Player").GetComponent<LitlleBuddy>().abilityOn;
    }

    private void FixedUpdate()
    {
        if (abilityState)
        {
            GameObject.Find("Player").GetComponent<Movement>().enabled = false;
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            HandleMovement();
            StateSetter();
        }
    }

    private void HandleMovement()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        if (buddyState == "Fox")
        {
            rb.gravityScale = 1;
            rb.velocity = new Vector2(horizontalMovement * moveSpeed * Time.deltaTime, rb.velocity.y);
        }
        else if (buddyState == "Frog")
        {
            rb.gravityScale = 0;

            bool wallDetected = Physics2D.BoxCast(transform.position, wallBoxSize, 0, transform.right, wallDistance.x, groundLayer);
            bool wall2Detected = Physics2D.BoxCast(transform.position, wall2BoxSize, 0, transform.right, wall2Distance.x, groundLayer);

            if (!wall2Detected)
            {
                if (wallDetected)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -horizontalMovement * moveSpeed * Time.deltaTime);
                }
            }

            if (!wallDetected)
            {
                if (wall2Detected)
                {
                    rb.velocity = new Vector2(rb.velocity.x, horizontalMovement * moveSpeed * Time.deltaTime);
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (wallDetected)
                {
                    rb.velocity = new Vector2(10, rb.velocity.y);
                }
                if (wall2Detected)
                {
                    rb.velocity = new Vector2(-10, rb.velocity.y);
                }
            }
        }
        else if (buddyState == "Owl")
        {
            if (rb.velocity.y > 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            rb.gravityScale = 0.08f;
            rb.velocity = new Vector2(horizontalMovement * moveSpeed * Time.deltaTime, rb.velocity.y);
        }
    }

    private void StateSetter()
    {
        bool groundDetected = Physics2D.BoxCast(transform.position, groundBoxSize, 0, -transform.up, groundDistance, groundLayer);

        if (groundDetected)
        {
            playerAni.SetInteger("AnimalState", 1);
            buddyState = "Fox";
        }
        else
        {
            playerAni.SetInteger("AnimalState", 3);
            buddyState = "Owl";
        }

        bool wallDetected = Physics2D.BoxCast(transform.position, wallBoxSize, 0, transform.right, wallDistance.x, groundLayer);
        bool wall2Detected = Physics2D.BoxCast(transform.position, wall2BoxSize, 0, transform.right, wall2Distance.x, groundLayer);

        if (wallDetected || wall2Detected)
        {
            playerAni.SetInteger("AnimalState", 2);
            buddyState = "Frog";
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundDistance, groundBoxSize);
        Gizmos.DrawWireCube(transform.position - transform.right * wallDistance.x, wallBoxSize);
        Gizmos.DrawWireCube(transform.position - transform.right * wall2Distance.x, wall2BoxSize);
    }
}
