using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUi;

    [Header("Transition")]
    public Animator transition;
    public float wait;

    void Start(){
        Resume();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){
                Resume();
            } else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause(){
        FindObjectOfType<AudioManager>().Play("Pause");
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenuScreen(){
        Debug.Log("should work");
        StartCoroutine(LoadMenu());
    }

    public void ReloadScene(){
        StartCoroutine(Reload());
    }
    
    public IEnumerator LoadMenu(){
        Resume();
        
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(wait);

        SceneManager.LoadScene(0);
    }

    public IEnumerator Reload(){
        Resume();

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(wait);


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
