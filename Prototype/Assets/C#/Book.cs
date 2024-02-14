using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject interactIcon;
    public KeyCode interactKey;
    public GameObject Sprite;
    public GameObject SpriteNoBook;
    public GameObject Player;
    public GameObject book;
    public GameObject NoBook;
    public GameObject door;
    public GameObject exclamation;
    public GameObject txt;

    [Header("RayCast")]
    [SerializeField] float distance;
    [SerializeField] LayerMask Layer;
    [SerializeField] Vector2 boxSize;

    void Start()
    {
        exclamation.SetActive(true);
        interactIcon.SetActive(false);
    }

    public void Update()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, transform.right, distance, Layer) && Player.GetComponent<AbilityController>().hasBook == false)
        {
            exclamation.SetActive(false);
            interactIcon.SetActive(true);
        }
        else
        {
            if(!Player.GetComponent<AbilityController>().hasBook){
                exclamation.SetActive(true);
            }
            else{
                exclamation.SetActive(false);
            }
            interactIcon.SetActive(false);
        }

        if (Input.GetKeyDown(interactKey) && Physics2D.BoxCast(transform.position, boxSize, 0, transform.right, distance, Layer) && Player.GetComponent<AbilityController>().hasBook == false)
        {
            FindObjectOfType<AudioManager>().Play("BookGrab");
            txt.SetActive(true);
            Sprite.GetComponent<SpriteRenderer>().enabled = true;
            SpriteNoBook.GetComponent<SpriteRenderer>().enabled = false;
            NoBook.GetComponent<SpriteRenderer>().enabled = true;
            book.GetComponent<SpriteRenderer>().enabled = false;
            Player.GetComponent<AbilityController>().hasBook = true;
            Player.GetComponent<Movement>().ani = Sprite.GetComponent<Animator>();
            Player.GetComponent<LitlleBuddy>().playerAni = Sprite.GetComponent<Animator>();

            LevelHandler levelHandler = door.GetComponent<LevelHandler>();
            if (levelHandler != null)
            {
                levelHandler.strenght++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * distance, boxSize);
    }
}
