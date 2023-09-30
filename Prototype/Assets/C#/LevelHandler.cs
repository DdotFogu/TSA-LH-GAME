using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    public bool open;
    public int num;
    [SerializeField] private int qouta;
    private Animator ani;
    public KeyCode interactKey;

    void Start(){
        ani = gameObject.GetComponent<Animator>();
    }

    void Update(){
        if(open == true){
            ani.SetBool("OpenState", true);
        }
        else{
            ani.SetBool("OpenState", false);
        }

        if(num == qouta){
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
            NextLevel();
        }
    }


    void NextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScenceIndex = currentSceneIndex + 1;
        if (nextScenceIndex == SceneManager.sceneCountInBuildSettings){
            nextScenceIndex = 0;
        }
        SceneManager.LoadScene(nextScenceIndex);
    }
}


