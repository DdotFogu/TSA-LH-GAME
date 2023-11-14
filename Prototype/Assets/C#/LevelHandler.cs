using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    public bool open;
    public int strenght;
    public int qouta;
    private Animator ani;
    public Animator transition;
    public KeyCode interactKey;
    public GameObject interactIcon;


    void Start(){
        ani = gameObject.GetComponent<Animator>();
        interactIcon.SetActive(false);
    }

    void Update(){
        if(strenght < 0){
            strenght = 0;
        }

        if(open == true){
            ani.SetBool("OpenState", true);
        }
        else{
            ani.SetBool("OpenState", false);
        }

        if(strenght == qouta){
            OpenDoor();
        }
        else{
            CloseDoor();
        }
    }

    public void OpenDoor(){
        open = true;
    }

    public void CloseDoor(){
        open = false;
    }

    void OnTriggerStay2D(Collider2D col){
        if(open == true && col.tag == "Player" && Input.GetKey(interactKey)){
            StartCoroutine(NextLevel());
        }
        if(col.CompareTag("Player") && open == true){
            interactIcon.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D col){
        if(col.CompareTag("Player") && open == true){
            interactIcon.SetActive(false);
        }
    }


    IEnumerator NextLevel(){
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScenceIndex = currentSceneIndex + 1;
        if (nextScenceIndex == SceneManager.sceneCountInBuildSettings){
            nextScenceIndex = 0;
        }
        SceneManager.LoadScene(nextScenceIndex);
    }
}


