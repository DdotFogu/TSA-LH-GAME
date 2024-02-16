using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LitlleBuddy : MonoBehaviour
{
    [SerializeField] private GameObject buddyPrefab;
    [SerializeField] private GameObject vircam;
    public bool abilityOn;
    public GameObject Sprite;
    [SerializeField] private KeyCode buddyKey;
    public Animator playerAni;
    public AbilityController ab;

    [Header("Ground Raycast")]
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundBoxSize;

    private bool firstActivation = false;

    private void Start() {
        abilityOn = false;
    }
    
    private void Update() {
        bool groundDetected = Physics2D.BoxCast(buddyPrefab.transform.position, groundBoxSize, 0, -transform.up, groundDistance, groundLayer);

        if(abilityOn){
            gameObject.GetComponent<Movement>().enabled = false;
        }
        else{
            gameObject.GetComponent<Movement>().enabled = true;
        }
        
        if(!abilityOn){
            if(Sprite.GetComponent<SpriteRenderer>().enabled == true){
                playerAni.SetBool("AnimalOn", false);
            }
            if(groundDetected){
                buddyPrefab.GetComponent<BuddyMovement>().buddyAni.SetInteger("AnimalState", 1);
            }
        }
        else{
            if(Sprite.GetComponent<SpriteRenderer>().enabled == true){
                playerAni.SetBool("AnimalOn", true);
            }
        }

        if(GameObject.Find("Player").GetComponent<AbilityController>().littleBuddy == true){
            HandelCamera();
            if(Input.GetKeyDown(buddyKey)){
                if(abilityOn == false){
                    abilityOn = true;
                    gameObject.GetComponent<Movement>().enabled = false;
                    buddyPrefab.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    ActiveBuddy();

                    if(firstActivation == false){
                        firstActivation = true;
                    }
                }
                else{
                    FindObjectOfType<AudioManager>().Play("Ability");
                    abilityOn = false;
                    gameObject.GetComponent<Movement>().enabled = true;
                    buddyPrefab.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
            }
        }

        if(Input.GetKeyDown(buddyKey)){
            HandlePowers();
        }
    }

    public void HandlePowers(){
        if(abilityOn == true){
            ab.DisableAll();
            ab.littleBuddy = true;
        }
        else{
            playerAni.SetInteger("AnimalState", 0);
            ab.EnableAll();
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
        FindObjectOfType<AudioManager>().Play("Ability");
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Movement>().enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(buddyPrefab.transform.position - transform.up * groundDistance, groundBoxSize);
    }
}

