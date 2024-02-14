using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnStart : MonoBehaviour
{
    void OnEnable()
    {
        Debug.Log("Loading End Screen");
        SceneManager.LoadScene("DemoEnd", LoadSceneMode.Additive);
    }
}
