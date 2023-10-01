using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] float _windforce = 0f;
    
    private void OnTriggerStay2D(Collider2D other) {
        var hitobj = other.gameObject;
        if(hitobj != null){
            var rb = hitobj.GetComponent<Rigidbody2D>();
            var dir = transform.up;
            rb.AddForce(dir * _windforce);
        }
    }
}
