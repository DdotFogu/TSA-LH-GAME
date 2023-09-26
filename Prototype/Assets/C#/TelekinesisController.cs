using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisController : MonoBehaviour
{
    [SerializeField] private GameObject telekinesisIcon;
    [SerializeField] private Camera cam;
    private Vector2 mousePos;
    private const float iconOffsetY = 0.5f;
    public LayerMask Player;
    public LayerMask Box;

    private void Start() {
        telekinesisIcon.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void Update(){
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseOver() {
        if(GameObject.Find("Player").GetComponent<AbilityController>().telekenisis == true){
                UpdateTelekinesisIcon();
                if(Input.GetMouseButton(0)){
                gameObject.tag = "Nonmoveable";
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                GameObject.Find("Player").GetComponent<Movement>().enabled = false;
                GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0, GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.y);
                gameObject.transform.position = mousePos;
            }
            else{
                GameObject.Find("Player").GetComponent<Movement>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                gameObject.tag = "Moveable";
            }
        }
    }

    private void OnMouseExit() {
        telekinesisIcon.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void UpdateTelekinesisIcon()
    {
        Vector2 trueHeight = gameObject.GetComponent<SpriteRenderer>().bounds.extents;
        telekinesisIcon.transform.position = gameObject.transform.position + Vector3.up * (trueHeight.y + iconOffsetY);
        telekinesisIcon.GetComponent<SpriteRenderer>().enabled = true;
    }
}
