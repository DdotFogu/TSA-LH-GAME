using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private bool inair = false;
    [SerializeField] float moveSpeed;
    private float horizontalMovement;
    [SerializeField] float jumpForce;
    [SerializeField] KeyCode jumpKey;
    Rigidbody2D rb;
    public GameObject pivot;
    public Animator ani;

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
        Animation();
    }

    void FixedUpdate() {
        MovementHandling();
    }

    void Animation(){
        if(horizontalMovement == 1){
            pivot.transform.localScale = new Vector2 (-1, 1);
        }
        if(horizontalMovement == -1){
            pivot.transform.localScale = new Vector2 (1, 1);
        }
        if(horizontalMovement == 0){
            ani.SetBool("Walking", false);
        }
        else{
            ani.SetBool("Walking", true);
        }
        if(Input.GetKeyDown(jumpKey)){

            ani.SetTrigger("Jump");
        }
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, distance, groundLayer)){
            ani.SetTrigger("Land");
        }
        if(GetComponent<Rigidbody2D>().velocity.y < 0 && inair == true){
            ani.SetBool("Falling", true);
        }
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, distance, groundLayer)){
            ani.SetBool("Falling", false);
        }
    }
    
    
    public bool isGrounded(){
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, distance, groundLayer)){
            inair = false;
            return true;
        }
        else{
            inair = true;
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
