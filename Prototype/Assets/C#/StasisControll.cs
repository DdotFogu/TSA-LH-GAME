using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisControll : MonoBehaviour
{
    [SerializeField] private GameObject stasisIcon;
    [SerializeField] private bool lockState;
    [SerializeField] private Camera cam;
    public Vector2 lastVelocity;
    private Vector2 mousePos;
    private const float iconOffsetY = 0.5f;
    public LayerMask groundLayer;

    private void Start() {
        stasisIcon.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void Update() {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if(lockState == false){
            SetVelocity();
        }
        else{
            gameObject.GetComponent<Rigidbody2D>().velocity = lastVelocity;
        }
        StateController();
    }

    private void OnMouseOver() {
        if(GameObject.Find("Player").GetComponent<AbilityController>().stasis == true && !Physics2D.Linecast(GameObject.Find("Player").transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), groundLayer)){
            if(lockState == false){
            UpdateStasisIcon();
            }

            if(Input.GetKeyDown(KeyCode.Q)){
                if(lockState == false){
                lockState = true;
                }
            else{
                lockState = false;
            }
        }
        }
    }

    private void SetVelocity(){
        lastVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
    }

    private void StateController(){
        if(GameObject.Find("Player").GetComponent<AbilityController>().stasis == true){
            if(lockState == true){
                gameObject.tag = "Nonmoveable";
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                stasisIcon.GetComponent<SpriteRenderer>().enabled = true;
                stasisIcon.transform.position = gameObject.transform.position;
            }
            else
            {
                gameObject.tag = "Moveable";
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    private void OnMouseExit() {
        stasisIcon.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void UpdateStasisIcon()
    {
        if(GameObject.Find("Player").GetComponent<AbilityController>().stasis == true){
            Vector2 trueHeight = gameObject.GetComponent<SpriteRenderer>().bounds.extents;
            stasisIcon.transform.position = gameObject.transform.position + Vector3.up * (trueHeight.y + iconOffsetY);
            stasisIcon.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
