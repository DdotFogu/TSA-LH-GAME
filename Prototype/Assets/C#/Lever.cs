using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject door;
    public GameObject interactIcon;
    public KeyCode interactKey;
    private bool flipped = false;


    public void Start(){
        interactIcon.SetActive(false);
    }

    

    public void OnTriggerStay2D(Collider2D col){
        if(col.CompareTag("Player") && Input.GetKey(interactKey) && flipped == false){
            flipped = true;
            door.GetComponent<LevelHandler>().num++;
        }
        if(col.CompareTag("Player") && flipped == false){
            interactIcon.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D col){
        if(col.CompareTag("Player")){
            interactIcon.SetActive(false);
        }
    }
}
