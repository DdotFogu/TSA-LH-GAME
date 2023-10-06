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

    void Start(){
        telekinesisIcon.GetComponent<SpriteRenderer>().enabled = false;
        normalGravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
    }

    void Update(){
        if(dragging && GameObject.Find("Player").GetComponent<AbilityController>().telekenisis == true){
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }

        if(Physics2D.Linecast(GameObject.Find("Player").transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), groundLayer)){
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            GameObject.Find("Player").GetComponent<Movement>().enabled = true;
            dragging = false;
        }
    }

    private void OnMouseDown(){
        if(!Physics2D.Linecast(GameObject.Find("Player").transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), groundLayer)){
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            dragging = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            GameObject.Find("Player").GetComponent<Movement>().enabled = false;
            GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0, GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    private void OnMouseUp(){
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        GameObject.Find("Player").GetComponent<Movement>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = normalGravity;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        dragging = false;
    }

    private void OnMouseOver(){
        if(!Physics2D.Linecast(GameObject.Find("Player").transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), groundLayer)){
            UpdateTelekinesisIcon();
        }
    }

    private void OnMouseExit() {
        telekinesisIcon.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void UpdateTelekinesisIcon()
    {
        if(GameObject.Find("Player").GetComponent<AbilityController>().telekenisis == true){
            Vector2 trueHeight = gameObject.GetComponent<SpriteRenderer>().bounds.extents;
            telekinesisIcon.transform.position = gameObject.transform.position + Vector3.up * (trueHeight.y + 1f);
            telekinesisIcon.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
