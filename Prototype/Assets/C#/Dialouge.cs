using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialouge : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject txt;
    public string[] lines;
    public float textspeed;
    public bool active;
    public bool hidetxt;
    public Animator ani;

    private int index;

    void Start()
    {
        active = false;
        textComponent.text = string.Empty;
        
        Image imageComponent = gameObject.GetComponent<Image>();
        if (imageComponent != null)
        {
            imageComponent.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (index < lines.Length)
            {
                if (textComponent.text == lines[index] && active)
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }
    }

    public void StartDialogue()
    {
        FindObjectOfType<AudioManager>().Play("DialougeStart");

        if (ani != null)
        {
            ani.SetBool("Active", true);
        }

        active = true;
        index = 0;

        Image imageComponent = gameObject.GetComponent<Image>();
        if (imageComponent != null)
        {
            imageComponent.enabled = true;
        }

        // Check if DialougeTxt GameObject exists in the scene
        GameObject dialougeTxt = GameObject.Find("DialougeTxt");
        if (dialougeTxt != null)
        {
            dialougeTxt.GetComponent<TextMeshProUGUI>().text = null;
        }

        StartCoroutine(TypeLine());
    }

    IEnumerator EndDialouge()
    {
        yield return new WaitForSeconds(1f);

        Image imageComponent = gameObject.GetComponent<Image>();
        if (imageComponent != null)
        {
            imageComponent.enabled = false;
        }
        active = false;
    }

    IEnumerator TypeLine()
    {
        if (index < lines.Length)
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textspeed);
            }
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            FindObjectOfType<AudioManager>().Play("DialougeNext");
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            if (ani != null)
            {
                ani.SetBool("Active", false);
            }
            StartCoroutine(EndDialouge());
        }
    }
}
