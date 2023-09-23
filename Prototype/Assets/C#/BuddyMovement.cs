using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BuddyMovement : MonoBehaviour
{
    [SerializeField] private string buddyState;

    [Header("Movement")]
    [SerializeField] float moveSpeed;
    private float horizontalMovement;

    [Header("GroundRaycast")]
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector2 groundBoxSize;

    [Header("WallRaycast")]
    [SerializeField] Vector2 wallDistance;
    [SerializeField] Vector2 wallBoxSize;
    [SerializeField] Vector2 wall2Distance;
    [SerializeField] Vector2 wall2BoxSize;

    private void FixedUpdate() {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        HandelMovement();
        StateSetter();
    }
    public void HandelMovement(){
        if(buddyState == "Fox"){
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMovement * moveSpeed * Time.deltaTime, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        if(buddyState == "Frog"){
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            if(Physics2D.BoxCast(transform.position, wallBoxSize, 0, transform.right, wallDistance.x, groundLayer)){
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -horizontalMovement * moveSpeed * Time.deltaTime);
            }
            if(Physics2D.BoxCast(transform.position, wall2BoxSize, 0, transform.right, wall2Distance.x, groundLayer)){
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, horizontalMovement * moveSpeed * Time.deltaTime);
            }

            if(Input.GetKey(KeyCode.Space)){
                if(Physics2D.BoxCast(transform.position, wallBoxSize, 0, transform.right, wallDistance.x, groundLayer)){
                   gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(10, gameObject.GetComponent<Rigidbody2D>().velocity.y); 
                }
                if(Physics2D.BoxCast(transform.position, wall2BoxSize, 0, transform.right, wall2Distance.x, groundLayer)){
                   gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, gameObject.GetComponent<Rigidbody2D>().velocity.y); 
                }
            }
        }
        if(buddyState == "Owl"){
            if(gameObject.GetComponent<Rigidbody2D>().velocity.y > 1){
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            }
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.05f;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMovement * moveSpeed * Time.deltaTime, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    public void StateSetter(){
        if(Physics2D.BoxCast(transform.position, groundBoxSize, 0, -transform.up, groundDistance, groundLayer)){
            buddyState = "Fox";
        }
        else{
            buddyState = "Owl";
        }
        if(Physics2D.BoxCast(transform.position, wallBoxSize, 0, transform.right, wallDistance.x, groundLayer) || Physics2D.BoxCast(transform.position, wall2BoxSize, 0, transform.right, wall2Distance.x, groundLayer)){
            buddyState = "Frog";
        }
    }
    private void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position - transform.up * groundDistance, groundBoxSize);
        Gizmos.DrawWireCube(transform.position - transform.right * wallDistance.x, wallBoxSize);
        Gizmos.DrawWireCube(transform.position - transform.right * wall2Distance.x, wall2BoxSize);
    }
}
