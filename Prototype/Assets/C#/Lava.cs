using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Player")){
            StartCoroutine(ReloadLevel(0.2f));
        }

        if(col.CompareTag("Moveable")){
            StartCoroutine(CreateNewCrate(col.gameObject));
        }
    }

    private IEnumerator ReloadLevel(float wait){
        
        yield return new WaitForSeconds(wait);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator CreateNewCrate(GameObject col)
    {
        if (col == null)
    {
        yield break; // Exit the coroutine if col is null.
    }

    SpriteRenderer spriteRenderer = col.GetComponent<SpriteRenderer>();
    if (spriteRenderer != null)
    {
        spriteRenderer.enabled = false;
    }

    BoxCollider2D boxCollider = col.GetComponent<BoxCollider2D>();
    if (boxCollider != null)
    {
        boxCollider.enabled = false;
    }

    yield return new WaitForSeconds(0.2f);

    // Check if the object still exists and has a Respawn component.
    Respawn respawnComponent = col.GetComponent<Respawn>();
    if (respawnComponent != null)
    {
        respawnComponent.RespawnCrate();
 
    }
    }
}
