using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] private bool gravityState;
    [SerializeField] private Camera cam;
    [SerializeField] private float normalGravity;
    private Vector2 mousePos;
    private const float iconOffsetY = 0.5f;

    private void Update() {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if(GameObject.Find("Player").GetComponent<AbilityController>().invertGravity == true){
            StateController();
        }
    }

    private void OnMouseOver() {
        if(GameObject.Find("Player").GetComponent<AbilityController>().invertGravity == true){
                if(Input.GetKeyDown(KeyCode.R) && gameObject.tag == "Moveable"){
                if(gravityState == false){
                    gravityState = true;
                }
                else{
                    gravityState = false;
                }
            }
        }
    }

    private void StateController(){
        if(gravityState == true && gameObject.tag != "Nonmoveable"){
           gameObject.GetComponent<Rigidbody2D>().gravityScale = -normalGravity;
        }
        else if(gameObject.tag == "Moveable")
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = normalGravity;
        }
    }
}
