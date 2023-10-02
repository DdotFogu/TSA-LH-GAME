using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private float normalGravity;
    private Vector2 mousePos;
    private bool gravityState;
    private const float iconOffsetY = 0.5f;

    private void Update() {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Start(){
        normalGravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
    }

    private void OnMouseOver() {
        if(GameObject.Find("Player").GetComponent<AbilityController>().invertGravity == true){
                if(Input.GetKeyDown(KeyCode.R) && gameObject.tag == "Moveable"){
                if(gravityState == false){
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = -normalGravity;
                    gravityState = true;
                }
                else{
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = normalGravity;
                    gravityState = false;
                }
            }
        }
    }
}
