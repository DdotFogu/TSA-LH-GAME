using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject door;

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player") || col.collider.CompareTag("LittleBuddy"))
        {
            door.GetComponent<LevelHandler>().num += 1;
        }
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player") || col.collider.CompareTag("LittleBuddy"))
        {
            door.GetComponent<LevelHandler>().num -= 1;
        }
    }
}
