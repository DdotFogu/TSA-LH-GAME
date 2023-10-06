using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialougeTrigger : MonoBehaviour
{
    public string[] txtLines;
    public KeyCode interactKey;
    public GameObject interactIcon;

    [Header("RayCast")]
    [SerializeField] float distance;
    [SerializeField] LayerMask Layer;
    [SerializeField] Vector2 boxSize;


    void Start(){
        interactIcon.SetActive(false);
    }

    public void Update(){
        if(Physics2D.BoxCast(transform.position, boxSize, 0, transform.right, distance, Layer) && GameObject.Find("DialogueBox").GetComponent<Dialouge>().active == false){
            interactIcon.SetActive(true);
        }
        else{
            interactIcon.SetActive(false);
        }

        if(Input.GetKeyDown(interactKey) && Physics2D.BoxCast(transform.position, boxSize, 0, transform.right, distance, Layer) && GameObject.Find("DialogueBox").GetComponent<Dialouge>().active == false){
            GameObject.Find("DialogueBox").GetComponent<Dialouge>().lines = txtLines;
            GameObject.Find("DialogueBox").GetComponent<Dialouge>().StartDialogue();
        }
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position - transform.up * distance, boxSize);
    }
}
