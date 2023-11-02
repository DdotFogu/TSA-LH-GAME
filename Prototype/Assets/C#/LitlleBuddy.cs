using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LitlleBuddy : MonoBehaviour
{
    [SerializeField] private GameObject buddyPrefab;
    [SerializeField] private GameObject vircam;
    public bool abilityOn;
    [SerializeField] private KeyCode buddyKey;
    public Animator playerAni;
    public AbilityController ab;

    private bool firstActivation = false;

    private void Start() {
        abilityOn = false;
    }
    
    private void Update() {

        if(abilityOn){
            gameObject.GetComponent<Movement>().enabled = false;
        }

        if(GameObject.Find("Player").GetComponent<AbilityController>().littleBuddy == true){
            HandelCamera();
            if(Input.GetKeyDown(buddyKey)){
                if(abilityOn == false){
                    abilityOn = true;
                    gameObject.GetComponent<Movement>().enabled = false;
                    ActiveBuddy();

                    if(firstActivation == false){
                        firstActivation = true;
                    }
                }
                else{
                    abilityOn = false;
                    buddyPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    gameObject.GetComponent<Movement>().enabled = true;
                }
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
        if(abilityOn){
            vcam.Follow = buddyPrefab.transform;
        }
        else{
            vcam.Follow = gameObject.transform;
        }
    }

    private void ActiveBuddy(){
        buddyPrefab.SetActive(true);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Movement>().enabled = false;
    }
}
