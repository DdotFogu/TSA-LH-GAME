using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialougeTiggerOnStart : MonoBehaviour
{
    public string[] txtLines;
    public GameObject DialogueBox;

    void Start()
    {
        DialogueBox.GetComponent<Dialouge>().lines = txtLines;
        DialogueBox.GetComponent<Dialouge>().StartDialogue();
    }
}
