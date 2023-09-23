using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LitlleBuddy : MonoBehaviour
{
    [SerializeField] private GameObject buddyPrefab;
    [SerializeField] private GameObject vircam;
    [SerializeField] private bool abilityOn;
    [SerializeField] private KeyCode buddyKey;
    private void Start() {
        abilityOn = false;
        buddyPrefab.SetActive(false);
    }
    private void Update() {
        HandelCamera();
        if(Input.GetKeyDown(buddyKey)){
            if(abilityOn == false){
                abilityOn = true;
                ActiveBuddy();
            }
            else{
                abilityOn = false;
                gameObject.GetComponent<Movement>().enabled = true;
                buddyPrefab.SetActive(false);
            }
        }
    }
    public void HandelCamera(){
        var vcam = vircam.GetComponent<CinemachineVirtualCamera>();
        if(buddyPrefab.activeInHierarchy){
            vcam.Follow = buddyPrefab.transform;
        }
        else{
            vcam.Follow = gameObject.transform;
        }
    }

    private void ActiveBuddy(){
        buddyPrefab.SetActive(true);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        buddyPrefab.transform.position = gameObject.transform.position;
        gameObject.GetComponent<Movement>().enabled = false;
    }
}
