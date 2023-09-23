using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisController : MonoBehaviour
{
    [SerializeField] private GameObject telekinesisIcon;
    [SerializeField] private Camera cam;
    public bool tele;
    private Vector2 mousePos;
    private const float iconOffsetY = 0.5f;

    private void Start() {
        telekinesisIcon.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void Update(){
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseOver() {
        UpdateTelekinesisIcon();
        if(Input.GetMouseButton(0)){
            gameObject.tag = "Nonmoveable";
            tele = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.transform.position = mousePos;
        }
        else{
            gameObject.tag = "Moveable";
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
