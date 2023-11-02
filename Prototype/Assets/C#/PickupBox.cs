using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBox : MonoBehaviour
{
    [SerializeField] private float xDistance;
    [SerializeField] private KeyCode pickupKey;
    [SerializeField] private bool pickedUp = false;
    public GameObject interactIcon;

    [Header("Raycast")]
    [SerializeField] float distance;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Vector2 boxSize;
    
    [Header("WallRaycast")]
    [SerializeField] LayerMask wallLayer;

    [SerializeField] Vector2 rDistance;
    [SerializeField] Vector2 rBoxSize;

    [SerializeField] Vector2 lDistance;
    [SerializeField] Vector2 lBoxSize;

    void Start(){
        interactIcon.SetActive(false);
    }

    private void Update(){
        StateHandler();

        xDistance = GameObject.Find("Player").transform.position.x - transform.position.x;

        if(GameObject.Find("Player").GetComponent<LitlleBuddy>().abilityOn == false){
            
            if(pickedUp == true && Input.GetKeyDown(pickupKey)){
                if(GameObject.Find("Player").GetComponent<Movement>().lastHorizontalMovement < 0 && !Physics2D.BoxCast(GameObject.Find("Player").transform.position, lBoxSize, 0, transform.right, lDistance.x, wallLayer)){
                    pickedUp = false;
                    GameObject.Find("Player").GetComponent<AbilityController>().EnableAll();
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    transform.position = new Vector2(GameObject.Find("Player").transform.position.x - 1, GameObject.Find("Player").transform.position.y);
                }
                else if(GameObject.Find("Player").GetComponent<Movement>().lastHorizontalMovement > 0 && !Physics2D.BoxCast(GameObject.Find("Player").transform.position, rBoxSize, 0, transform.right, rDistance.x, wallLayer)){
                    pickedUp = false;
                    GameObject.Find("Player").GetComponent<AbilityController>().EnableAll();
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    transform.position = new Vector2(GameObject.Find("Player").transform.position.x + 1, GameObject.Find("Player").transform.position.y);
                }
            }

            if(GameObject.Find("Player").GetComponent<AbilityController>().carryingMetal == false && Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, distance, playerLayer) && Input.GetKeyDown(pickupKey) && pickedUp == false){
            
                if(xDistance < 0 && GameObject.Find("Player").GetComponent<Movement>().lastHorizontalMovement > 0){
                    Debug.Log("Right");
                    pickedUp = true;
                }

                if(xDistance > 0 && GameObject.Find("Player").GetComponent<Movement>().lastHorizontalMovement < 0){
                    Debug.Log("Left");
                    pickedUp = true;
                }
            }

            if(xDistance > 0 && GameObject.Find("Player").GetComponent<Movement>().lastHorizontalMovement < 0 && Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, distance, playerLayer)){
                interactIcon.SetActive(true);
            }
            else if(xDistance < 0 && GameObject.Find("Player").GetComponent<Movement>().lastHorizontalMovement > 0 && Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, distance, playerLayer)){
                interactIcon.SetActive(true);
            }
            else{
                interactIcon.SetActive(false);
            }
        }
        else{
            interactIcon.SetActive(false);
        }
    }


    private void StateHandler(){
        if(pickedUp){
            GameObject.Find("Player").GetComponent<AbilityController>().DisableAll();
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<BoxCollider2D>().isTrigger = true;
            GameObject.Find("Player").GetComponent<AbilityController>().carryingMetal = true;
            transform.position = new Vector2(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y + 1f);
        }
        else{
            GetComponent<BoxCollider2D>().isTrigger = false;
            GameObject.Find("Player").GetComponent<AbilityController>().carryingMetal = false;
        }
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position - transform.up * distance, boxSize);

        if(pickedUp){
            Gizmos.DrawWireCube(GameObject.Find("Player").transform.position - transform.right * lDistance.x, lBoxSize);
            Gizmos.DrawWireCube(GameObject.Find("Player").transform.position - transform.right * rDistance.x, rBoxSize);
        }
    }
}
