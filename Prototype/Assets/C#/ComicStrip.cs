using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicStrip : MonoBehaviour
{
    [SerializeField] private Animator transition;

    void Start()
    {
        StartCoroutine(NextLevel(5));
    }

    IEnumerator NextLevel(float wait){
        yield return new WaitForSeconds(wait);

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
