using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject door;
    public KeyCode interactKey;
    private bool flipped = false;

    public void OnTriggerStay2D(Collider2D col){
        if(col.CompareTag("Player") && Input.GetKey(interactKey) && flipped == false){
            flipped = true;
            door.GetComponent<LevelHandler>().num++;
        }
    }
}
