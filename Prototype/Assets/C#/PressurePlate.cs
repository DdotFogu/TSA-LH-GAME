using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject door;
    public GameObject wall;
    public GameObject elevator;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (elevator != null){
            if (col.CompareTag("Player") || col.CompareTag("LittleBuddy") || col.CompareTag("Moveable") || col.CompareTag("MetalBox"))
            {
                elevator.GetComponent<Elevator>().strenght++;
            }
        }

        if (door != null)
        {
            if (col.CompareTag("Player") || col.CompareTag("LittleBuddy") || col.CompareTag("Moveable") || col.CompareTag("MetalBox"))
            {
                LevelHandler levelHandler = door.GetComponent<LevelHandler>();
                if (levelHandler != null)
                {
                    levelHandler.num++;
                }
            }
        }
        
        if (wall != null)
        {
            if (col.CompareTag("Player") || col.CompareTag("LittleBuddy") || col.CompareTag("Moveable") || col.CompareTag("MetalBox"))
            {
                wall.GetComponent<WallHandler>().strenght++;
            }
        }
    }


    public void OnTriggerExit2D(Collider2D col)
    {
        if (elevator != null){
            if (col.CompareTag("Player") || col.CompareTag("LittleBuddy") || col.CompareTag("Moveable") || col.CompareTag("MetalBox"))
            {
                elevator.GetComponent<Elevator>().strenght--;
            }
        }

        if (door != null)
        {
            if (col.CompareTag("Player") || col.CompareTag("LittleBuddy") || col.CompareTag("Moveable") || col.CompareTag("MetalBox"))
            {
                LevelHandler levelHandler = door.GetComponent<LevelHandler>();
                if (levelHandler != null)
                {
                    levelHandler.num--;
                }
            }
        }

        if (wall != null)
        {
            if (col.CompareTag("Player") || col.CompareTag("LittleBuddy") || col.CompareTag("Moveable") || col.CompareTag("MetalBox"))
            {
                wall.GetComponent<WallHandler>().strenght--;
            }
        }
    }
}
