using UnityEngine;

public class Switch : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public bool switchState = false; 
    public GameObject interactIcon;
    public GameObject door;
    public GameObject wall;
    public GameObject elevator;

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
            switchState = !switchState;

            if(switchState){
                if(door != null){
                    door.GetComponent<LevelHandler>().num++;
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
                if(door != null){
                    door.GetComponent<LevelHandler>().num--;
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
