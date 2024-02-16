using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject door;
    public GameObject wall;
    public GameObject elevator;
    public GameObject interactIcon;
    public KeyCode interactKey;
    public Animator ani;
    private bool flipped = false;

    public void OnTriggerStay2D(Collider2D col){
        if(col.CompareTag("Player") && Input.GetKey(interactKey) && flipped == false || col.CompareTag("LittleBuddy") && Input.GetKey(interactKey) && flipped == false){
            FindObjectOfType<AudioManager>().Play("LeverFlick");
            flipped = true;
            ani.SetTrigger("Trigger");
            if(door != null){
                door.GetComponent<LevelHandler>().strenght++;
            }

            if (wall != null)
            {
                wall.GetComponent<WallHandler>().strenght++;
            }

            if(elevator != null){
                elevator.GetComponent<Elevator>().strenght++;
            }
        
        }
        if(col.CompareTag("Player") && flipped == false || col.CompareTag("LittleBuddy") && flipped == false){
            interactIcon.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D col){
        if(col.CompareTag("Player") || col.CompareTag("LittleBuddy")){
            interactIcon.SetActive(false);
        }
    }
}
