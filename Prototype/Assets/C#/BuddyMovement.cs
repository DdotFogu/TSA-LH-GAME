using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BuddyMovement : MonoBehaviour
{
    [SerializeField] private string buddyState;
    public Animator playerAni;
    public Animator buddyAni;
    public GameObject pivot;
    public SpriteRenderer SpriteRen;
    private bool abilityState;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private float horizontalMovement;
    private float verticalMovement;

    [Header("Ground Raycast")]
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundBoxSize;

    [Header("Wall Raycast")]
    [SerializeField] private Vector2 wallDistance;
    [SerializeField] private Vector2 wallBoxSize;
    [SerializeField] private Vector2 wall2Distance;
    [SerializeField] private Vector2 wall2BoxSize;

    private void Update()
    {
        abilityState = GameObject.Find("Player").GetComponent<LitlleBuddy>().abilityOn;

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        if(abilityState){
            if (horizontalMovement == 1 || verticalMovement == 1)
            {
                if(buddyState == "Fox" || buddyState == "Owl"){
                    SpriteRen.flipY = false;
                    pivot.transform.localScale = new Vector2(-1.3624f, 1.3624f);
                }
            }
            if (horizontalMovement == -1 || verticalMovement == -1)
            {
                if(buddyState == "Fox" || buddyState == "Owl"){
                    SpriteRen.flipY = false;
                    pivot.transform.localScale = new Vector2(1.3624f, 1.3624f);
                }
            }
        }

        if(buddyState == "Fox" || buddyState == "Owl"){
            SpriteRen.flipY = false;
        }

        if (horizontalMovement == 0)
        {
            buddyAni.SetBool("Walking", false);
        }
        if(horizontalMovement != 0 && buddyState == "Fox" || horizontalMovement != 0 && buddyState == "Owl")
        {
            buddyAni.SetBool("Walking", true);
        }
        else if(verticalMovement != 0 && buddyState == "Frog"){
            buddyAni.SetBool("Walking", true);
        }

        buddyAni.SetBool("AbilityOn", abilityState);
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

            if(abilityState && wall2Detected){
                if (verticalMovement == 1)
                {
                    if(buddyState == "Frog"){
                        SpriteRen.flipY = false;
                    }
                }
                if (verticalMovement == -1)
                {
                    if(buddyState == "Frog"){
                        SpriteRen.flipY = true;
                    }
                }
            }

            if(abilityState && wallDetected){
                if (verticalMovement == 1)
                {
                    if(buddyState == "Frog"){
                        SpriteRen.flipY = false;
                    }
                }
                if (verticalMovement == -1)
                {
                    if(buddyState == "Frog"){
                        SpriteRen.flipY = true;
                    }
                }
            }

            if (!wall2Detected)
            {
                if (wallDetected)
                {
                    pivot.transform.localScale = new Vector2(1.3624f, 1.3624f);
                    rb.velocity = new Vector2(rb.velocity.x, verticalMovement * moveSpeed * Time.deltaTime);
                }
            }

            if (!wallDetected)
            {
                if (wall2Detected)
                {
                    pivot.transform.localScale = new Vector2(-1.3624f, 1.3624f);
                    rb.velocity = new Vector2(rb.velocity.x, verticalMovement * moveSpeed * Time.deltaTime);
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (wallDetected)
                {
                    rb.velocity = new Vector2(10, 0);
                }
                if (wall2Detected)
                {
                    rb.velocity = new Vector2(-10, 0);
                }
            }

            if(horizontalMovement == 0){
                rb.velocity = new Vector2(0, rb.velocity.y);
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
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        bool groundDetected = Physics2D.BoxCast(transform.position, groundBoxSize, 0, -transform.up, groundDistance, groundLayer);

        if (groundDetected)
        {
            playerAni.SetInteger("AnimalState", 1);
            buddyAni.SetInteger("AnimalState", 1);
            buddyState = "Fox";
        }
        else
        {
            playerAni.SetInteger("AnimalState", 3);
            buddyAni.SetInteger("AnimalState", 3);
            buddyState = "Owl";
        }

        var hit1 = Physics2D.BoxCast(transform.position, wallBoxSize, 0, transform.right, wallDistance.x, groundLayer);
        var hit2 = Physics2D.BoxCast(transform.position, wall2BoxSize, 0, transform.right, wall2Distance.x, groundLayer);

        if ((hit1.collider != null && hit1.collider.CompareTag("Slime")) || (hit2.collider != null && hit2.collider.CompareTag("Slime")))
        {
            Debug.Log("Slime, can't climb");
            if(groundDetected){
                playerAni.SetInteger("AnimalState", 1);
                buddyAni.SetInteger("AnimalState", 1);
                buddyState = "Fox";
            }
            else{
                playerAni.SetInteger("AnimalState", 3);
                buddyAni.SetInteger("AnimalState", 3);
                buddyState = "Owl";
            }
        }
        else if ((hit1.collider != null && (hit1.collider.CompareTag("Ground") || hit1.collider.CompareTag("Moveable"))) || (hit2.collider != null && (hit2.collider.CompareTag("Ground") || hit2.collider.CompareTag("Moveable"))))
        {
            playerAni.SetInteger("AnimalState", 2);
            buddyAni.SetInteger("AnimalState", 2);
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
