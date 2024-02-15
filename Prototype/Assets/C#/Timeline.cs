using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Timeline : MonoBehaviour
{

    public PlayableDirector timeline;
    private bool active = false;
    public AbilityController ab;
    
    void Start(){
        timeline = GetComponent<PlayableDirector>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !active){
            Debug.Log("Trigger");
            active = true;
            GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            ab.littleBuddy = true;
            ab.trueBuddy = true;
            timeline.Play();
        }
    }
}
