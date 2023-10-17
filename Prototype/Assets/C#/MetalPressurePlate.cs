using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPressurePlate : MonoBehaviour
{
    public GameObject door;

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("MetalBox"))
        {
            Debug.Log("Metal Box");
            door.GetComponent<LevelHandler>().num++;
        }
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("MetalBox"))
        {
            door.GetComponent<LevelHandler>().num--;
        }
    }
}
