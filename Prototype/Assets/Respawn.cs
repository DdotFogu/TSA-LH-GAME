using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector2 orginalPos;

    private void Start(){
        orginalPos = gameObject.transform.position;
    }

    public void RespawnCrate(){
        gameObject.GetComponent<TelekinesisController>().dragging = false;
        gameObject.transform.position = orginalPos;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
