using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Animator playerAni;
    private float normalGravity;
    private Vector2 mousePos;
    private bool gravityState;
    private const float iconOffsetY = 0.5f;
    public LayerMask groundLayer;

    private void Update() {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Start(){
        normalGravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
    }

    private void OnMouseOver() {
        if(GameObject.Find("Player").GetComponent<AbilityController>().invertGravity == true && !Physics2D.Linecast(GameObject.Find("Player").transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), groundLayer)){
                if(Input.GetKeyDown(KeyCode.R) && gameObject.tag == "Moveable"){
                if(gravityState == false){
                    StartCoroutine(Animation());
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = -normalGravity;
                    gravityState = true;
                }
                else{
                    StartCoroutine(Animation());
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = normalGravity;
                    gravityState = false;
                }
            }
        }
    }

    private IEnumerator Animation(){
        GameObject.Find("Player").GetComponent<Movement>().enabled = false;
        GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0, GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.y);
        playerAni.SetTrigger("Gravity");

        yield return new WaitForSeconds(0.5f);

        GameObject.Find("Player").GetComponent<Movement>().enabled = true;
    }
}
