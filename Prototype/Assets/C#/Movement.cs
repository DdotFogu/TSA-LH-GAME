using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private float horizontalMovement;
    [SerializeField] float jumpForce;
    [SerializeField] KeyCode jumpKey;
    Rigidbody2D rb;

    [Header("Raycast")]
    [SerializeField] float distance;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector2 boxSize;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate() {
        MovementHandling();
    }
    
    public bool isGrounded(){
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, distance, groundLayer)){
            return true;
        }
        else{
            return false;
        }
    }
    private void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position - transform.up * distance, boxSize);
    }

    void MovementHandling(){
        rb.velocity = new Vector2(horizontalMovement * moveSpeed * Time.deltaTime, rb.velocity.y);
        if(Input.GetKey(jumpKey) && isGrounded()){
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
