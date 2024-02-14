using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float strenght = 1;
    public GameObject Partical;
    public Animator fanAni;
    [SerializeField] float _windforce = 0f;
    
    private void OnTriggerStay2D(Collider2D other) {
        var hitobj = other.gameObject;
        if(hitobj != null && hitobj.GetComponent<Rigidbody2D>() != null && strenght >= 1){
            fanAni.enabled = true;
            Partical.SetActive(true);
            var rb = hitobj.GetComponent<Rigidbody2D>();
            var dir = transform.up;
            rb.AddForce(dir * _windforce);
        }
        if(hitobj != null && hitobj.GetComponent<Rigidbody2D>() != null && strenght <= 0){
            fanAni.enabled = false;
            Partical.SetActive(false);
            var rb = hitobj.GetComponent<Rigidbody2D>();
            var dir = transform.up;
            if(rb.velocity.x > 0){
                rb.AddForce(dir * -_windforce);
            }

            if(rb.velocity.x < 0){
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }
}
