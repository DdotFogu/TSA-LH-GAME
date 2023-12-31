using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float wait;


    public void Start(){
        if(transition != null){
            transition.gameObject.SetActive(true);
        }
    }

    public void PlayGame(){
        StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public IEnumerator LoadNextLevel(int levelIndex){
        if(transition != null){
            transition.SetTrigger("Start");
        }

        yield return new WaitForSeconds(wait);

        SceneManager.LoadScene(levelIndex);
    }


    public void LoadSandBox(){
        StartCoroutine(LoadNextLevel(11));
    }

    public void LoadMainMenu(){
        StartCoroutine(LoadNextLevel(0));
    }

    public void PlaySound(){
        FindObjectOfType<AudioManager>().Play("Select");
    }
}
