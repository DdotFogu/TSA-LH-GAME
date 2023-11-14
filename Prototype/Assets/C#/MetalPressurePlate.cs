using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPressurePlate : MonoBehaviour
{
    public GameObject door;
    public GameObject wall;
    public GameObject elevator;
    public GameObject progress;
    public Animator ani;
    public bool isOn = false;
    public int touchingObjectsCount = 0;

    public void TurnOn(Collider2D col)
    {
        FindObjectOfType<AudioManager>().Play("PressurePlate");
        if (elevator != null && col.CompareTag("MetalBox"))
        {
            isOn = true;
            Elevator elevatorComponent = elevator.GetComponent<Elevator>();
            if (elevatorComponent != null)
            {
                elevatorComponent.strenght++;
            }
        }

        if (door != null && col.CompareTag("MetalBox"))
        {
            isOn = true;
            LevelHandler levelHandler = door.GetComponent<LevelHandler>();
            if (levelHandler != null)
            {
                if (progress != null)
                {
                    progress.GetComponent<ProgressBar>().progression += 0.33f;
                }
                levelHandler.strenght++;
            }
        }

        if (wall != null && col.CompareTag("MetalBox"))
        {
            isOn = true;
            WallHandler wallComponent = wall.GetComponent<WallHandler>();
            if (wallComponent != null)
            {
                wallComponent.strenght++;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        
        if (touchingObjectsCount == 0)
        {
            ani.SetBool("Pressure", true);
            TurnOn(col);
        }

        if (elevator != null || door != null || wall != null)
        {
            if (col.CompareTag("MetalBox"))
            {
                touchingObjectsCount++;
            }
        }
    }

    public void TurnOff(Collider2D col)
    {
        FindObjectOfType<AudioManager>().Play("PressurePlate");
        if (elevator != null && col.CompareTag("MetalBox"))
        {
            elevator.GetComponent<Elevator>().strenght--;
        }

        if (door != null && col.CompareTag("MetalBox"))
        {
            LevelHandler levelHandler = door.GetComponent<LevelHandler>();
            if (levelHandler != null)
            {
                if (progress != null)
                {
                    progress.GetComponent<ProgressBar>().progression -= 0.33f;
                }
                levelHandler.strenght--;
                isOn = false;
            }
        }

        if (wall != null && col.CompareTag("MetalBox"))
        {
            wall.GetComponent<WallHandler>().strenght--;
            isOn = false;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (elevator != null || door != null || wall != null)
        {
            if (col.CompareTag("MetalBox"))
            {
                touchingObjectsCount--;

                if (touchingObjectsCount == 0)
                {
                    ani.SetBool("Pressure", false);
                    TurnOff(col);
                }
            }
        }
    }
}
