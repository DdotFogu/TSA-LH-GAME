using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject interactIcon;
    public KeyCode interactKey;

    [Header("RayCast")]
    [SerializeField] float distance;
    [SerializeField] LayerMask Layer;
    [SerializeField] Vector2 boxSize;

    void Start(){
        interactIcon.SetActive(false);
    }

    public void Update(){
        if(Physics2D.BoxCast(transform.position, boxSize, 0, transform.right, distance, Layer) && GameObject.Find("Player").GetComponent<AbilityController>().hasBook == false){
            interactIcon.SetActive(true);
        }
        else{
            interactIcon.SetActive(false);
        }

        if(Input.GetKeyDown(interactKey) && Physics2D.BoxCast(transform.position, boxSize, 0, transform.right, distance, Layer) && GameObject.Find("Player").GetComponent<AbilityController>().hasBook == false){
            GameObject.Find("Sprite").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Sprite(NoBook)").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Player").GetComponent<AbilityController>().hasBook = true;
            GameObject.Find("Player").GetComponent<Movement>().ani = GameObject.Find("Sprite").GetComponent<Animator>();
            GameObject.Find("Player").GetComponent<LitlleBuddy>().playerAni = GameObject.Find("Sprite").GetComponent<Animator>();
            GameObject.Find("Player").GetComponent<AbilityController>().littleBuddy = true;
        }
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position - transform.up * distance, boxSize);
    }
}