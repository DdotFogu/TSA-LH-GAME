using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHandler : MonoBehaviour
{
    public float strenght; 
    public float quota;

    private void Update(){
        State();
    }

    public void State(){
        if(strenght >= quota){
            BoxCollider2D wallCollider = this.GetComponent<BoxCollider2D>();
            SpriteRenderer wallRenderer = this.GetComponent<SpriteRenderer>(); // Use GetComponent to access the SpriteRenderer

            if (wallCollider != null)
            {
                wallCollider.enabled = false;
            }

            if (wallRenderer != null)
            {
                wallRenderer.enabled = false;
            }
        }

        if(strenght < quota){
            BoxCollider2D wallCollider = this.GetComponent<BoxCollider2D>();
            SpriteRenderer wallRenderer = this.GetComponent<SpriteRenderer>(); // Use GetComponent to access the SpriteRenderer

            if (wallCollider != null)
            {
                wallCollider.enabled = true;
            }

            if (wallRenderer != null)
            {
                wallRenderer.enabled = true;
            }
        }
    }
}
