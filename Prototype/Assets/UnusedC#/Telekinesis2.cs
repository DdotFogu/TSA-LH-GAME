using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject mouse1Icon;
     [SerializeField] LayerMask groundLayer;
    private Vector2 mousePos;
    private Collider2D hitCollider;
    private bool movingObject;
    private const float iconOffsetY = 0.5f;
    private const float gravityScaleNormal = 15f;
    private const float rotateForce = .3f;
    private float velocityMultiplier = 10f;

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        HandleTelekinesis();
    }

    private void OnMouseDown(){
        
    }

    private void HandleTelekinesis()
    {
        hitCollider = Physics2D.OverlapCircle(mousePos, 0f);
        if (hitCollider != null && hitCollider.gameObject.CompareTag("Moveable"))
        {
            UpdateMouseIconPosition(hitCollider);
            if (Input.GetMouseButton(0) && CheckForColliders(hitCollider) == true)
            {
                GetComponent<Movement>().enabled = false;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                RotateObject(hitCollider);
                DragObject(hitCollider);
                
                if(CheckForColliders(hitCollider) == false){
                    ReleaseObject(hitCollider);
                }
            }
            else if(movingObject == true)
            {
                GetComponent<Movement>().enabled = true;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                ReleaseObject(hitCollider);
            }
        }
        else
        {
            DisableMouseIcon();
        }
    }

    private bool CheckForColliders(Collider2D hit){
        Collider2D col = Physics2D.OverlapCircle(hit.transform.position, 0f, groundLayer);
        if(col != null){
            return false;
        }
        else{
            return true;
        }
    }

    private void UpdateMouseIconPosition(Collider2D hit)
    {
        Vector2 trueHeight = hit.GetComponent<SpriteRenderer>().bounds.extents;
        mouse1Icon.transform.position = hit.gameObject.transform.position + Vector3.up * (trueHeight.y + iconOffsetY);
        mouse1Icon.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void DragObject(Collider2D hit)
    {
        movingObject = true;
        mouse1Icon.GetComponent<SpriteRenderer>().enabled = false;
        hit.GetComponent<Rigidbody2D>().gravityScale = 0;
        hit.gameObject.transform.position = mousePos;
    }

    private void RotateObject(Collider2D hit){
        if(Input.GetKey(KeyCode.D)){
            hit.gameObject.transform.Rotate(0, 0, -rotateForce);
        }
        if(Input.GetKey(KeyCode.A)){
            hit.gameObject.transform.Rotate(0, 0, rotateForce);
        }
    }

    private void ReleaseObject(Collider2D hit)
    {
        float mX = Input.GetAxis("Mouse X");
        float mY = Input.GetAxis("Mouse Y");
        hit.GetComponent<Rigidbody2D>().velocity = new Vector2(mX * velocityMultiplier, mY * velocityMultiplier);
        movingObject = false;
        hit.GetComponent<Rigidbody2D>().gravityScale = gravityScaleNormal;
    }

    private void DisableMouseIcon()
    {
        mouse1Icon.GetComponent<SpriteRenderer>().enabled = false;
    }
}