using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialouge : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject txt;
    public GameObject continueTxt;
    public string[] lines;
    public float textspeed;
    public bool active;

    private int index;


    // Start is called before the first frame update
    void Start()
    {
        active = false;
        textComponent.text = string.Empty;
        gameObject.GetComponent<Image>().enabled = false;
        txt.SetActive(false);
        continueTxt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(textComponent.text == lines[index]){
                NextLine();
            }
            else{
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue(){
        active = true;
        index = 0;
        continueTxt.SetActive(true);
        gameObject.GetComponent<Image>().enabled = true;
        txt.SetActive(true);
        GameObject.Find("DialougeTxt").GetComponent<TextMeshProUGUI>().text = null;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach(char c in lines[index].ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(textspeed);
        }
    }

    void NextLine(){
        if(index < lines.Length - 1){
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else{
            gameObject.GetComponent<Image>().enabled = false;
            active = false;
            continueTxt.SetActive(false);
            txt.SetActive(false);
        }
    }
}
