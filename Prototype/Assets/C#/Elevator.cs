using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public bool goDown = true;
    public float strenght;
    public float quota;
    public float speed;

    private void Update(){
        State();
    }

    private void State(){
        if(strenght >= quota && goDown == true){
            this.GetComponent<Rigidbody2D>().gravityScale = 1 * speed;
        }
        else if(strenght < quota && goDown == true){
            this.GetComponent<Rigidbody2D>().gravityScale = -1 * speed;
        }

        if(strenght >= quota && goDown == false){
            this.GetComponent<Rigidbody2D>().gravityScale = -1 * speed;
        }
        else if(strenght < quota && goDown == false){
            this.GetComponent<Rigidbody2D>().gravityScale = 1 * speed;
        }
    }

}
