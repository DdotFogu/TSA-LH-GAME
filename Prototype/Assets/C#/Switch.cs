using UnityEngine;

public class Switch : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public bool switchState = false; 
    public GameObject interactIcon;
    public GameObject door;
    public GameObject wall;
    public GameObject elevator;
    public Animator ani;

    private bool canInteract = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("LittleBuddy"))
        {
            interactIcon.SetActive(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("LittleBuddy"))
        {
            interactIcon.SetActive(false);
            canInteract = false;
        }
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(interactKey))
        {
            FindObjectOfType<AudioManager>().Play("LeverFlick");

            switchState = !switchState;

            if(switchState){
                ani.SetBool("On", true);
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

            if(!switchState){
                ani.SetBool("On", false);
                if(door != null){
                    door.GetComponent<LevelHandler>().strenght--;
                }

                if (wall != null)
                {
                    wall.GetComponent<WallHandler>().strenght--;
                }   

                if(elevator != null){
                    elevator.GetComponent<Elevator>().strenght--;
                }
            }
        }
    }
}
