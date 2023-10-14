using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LitlleBuddy : MonoBehaviour
{
    [SerializeField] private GameObject buddyPrefab;
    [SerializeField] private GameObject vircam;
    public bool abilityOn;
    [SerializeField] private KeyCode resetKey;
    [SerializeField] private KeyCode buddyKey;
    [SerializeField] private Vector2 buddyPos;
    public GameObject buddyIcon;
    public Animator playerAni;
    public AbilityController ab;

    private void Start() {
        abilityOn = false;
        buddyPrefab.SetActive(false);
        buddyIcon.SetActive(false);
    }
    
    private void Update() {
        if(GameObject.Find("Player").GetComponent<AbilityController>().littleBuddy == true){
            HandelCamera();
            if(Input.GetKeyDown(buddyKey)){
                if(abilityOn == false){
                    abilityOn = true;
                    gameObject.GetComponent<Movement>().enabled = false;
                    ActiveBuddy();
                }
                else{
                    abilityOn = false;
                    buddyIcon.SetActive(true);
                    buddyIcon.transform.position = buddyPrefab.transform.position;
                    gameObject.GetComponent<Movement>().enabled = true;
                    buddyPrefab.SetActive(false);
                }
            }

            if(abilityOn == true){
                buddyPos = buddyPrefab.transform.position;
            }

            if(Input.GetKeyDown(resetKey)){
                buddyPrefab.transform.position = gameObject.transform.position;
            }
        }

        if(Input.GetKeyDown(buddyKey)){
            HandlePowers();
        }
    }

    public void HandlePowers(){
        if(abilityOn == true){
            ab.telekenisis = false;
            ab.stasis = false;
            ab.invertGravity = false;
        }
        else{
            playerAni.SetInteger("AnimalState", 0);
            ab.telekenisis = true;
            ab.stasis = true;
            ab.invertGravity = true;
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
        buddyIcon.SetActive(false);
        buddyPrefab.SetActive(true);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if(buddyPos == new Vector2(0, 0)){
            buddyPrefab.transform.position = gameObject.transform.position;
        }
        else{
            buddyPrefab.transform.position = buddyPos;   
        }
        gameObject.GetComponent<Movement>().enabled = false;
    }
}
