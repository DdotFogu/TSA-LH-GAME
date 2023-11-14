using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisController : MonoBehaviour
{
    public bool dragging = false;
    public GameObject telekinesisIcon;
    private Vector3 offset;
    private float normalGravity;
    public LayerMask groundLayer;
    public Animator playerAni;

    // Cached references to components
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private AbilityController abilityController;
    private Movement playerMovement;

    void Start()
    {
        telekinesisIcon.GetComponent<SpriteRenderer>().enabled = false;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        abilityController = GameObject.Find("Player").GetComponent<AbilityController>();
        playerMovement = GameObject.Find("Player").GetComponent<Movement>();
        normalGravity = rb.gravityScale;
    }

    void Update()
    {
        if (dragging && abilityController.telekenisis == true)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }

        if (Physics2D.Linecast(playerMovement.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), groundLayer))
        {
            boxCollider.isTrigger = false;
            playerMovement.enabled = true;
            dragging = false;
        }
    }

    private void OnMouseDown()
    {
        if (playerMovement.isGrounded() && !Physics2D.Linecast(playerMovement.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), groundLayer) && abilityController.telekenisis == true)
        {
            normalGravity = rb.gravityScale;
            playerAni.SetBool("Telekensis", true);
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            boxCollider.isTrigger = true;
            dragging = true;
            rb.gravityScale = 0;
            playerMovement.enabled = false;
            playerMovement.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerMovement.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    private void OnMouseUp()
    {
        playerAni.SetBool("Telekensis", false);
        boxCollider.isTrigger = false;
        playerMovement.enabled = true;
        rb.gravityScale = normalGravity;
        rb.velocity = new Vector2(0, 0);
        dragging = false;
    }

    private void OnMouseOver()
    {
        if (!Physics2D.Linecast(playerMovement.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), groundLayer))
        {
            UpdateTelekinesisIcon();
        }
    }

    private void OnMouseExit()
    {
        telekinesisIcon.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void UpdateTelekinesisIcon()
    {
        if (abilityController.telekenisis == true && playerMovement.isGrounded())
        {
            Vector2 trueHeight = GetComponent<SpriteRenderer>().bounds.extents;
            telekinesisIcon.transform.position = transform.position + Vector3.up * (trueHeight.y + 1f);
            telekinesisIcon.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            telekinesisIcon.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "MetalPressurePlate"){
            return;
        }
        if (col.tag == "PressurePlate" && col.gameObject.GetComponent<PressurePlate>().door != null)
        {
            col.gameObject.GetComponent<PressurePlate>().door.GetComponent<LevelHandler>().strenght--;
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Moveable") || col.gameObject.CompareTag("Nonmoveable") || col.gameObject.CompareTag("Slime")){
            if(!dragging){
                FindObjectOfType<AudioManager>().Play("BoxThud");
            }
        }
    }
}
