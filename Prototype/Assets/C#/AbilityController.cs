using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public bool telekenisis;
    public bool stasis;
    public bool invertGravity;
    public bool littleBuddy;

    [Header("Book")]
    public bool hasBook = true;

    public void DisableAll(){
        telekenisis = false;
        stasis = false;
        invertGravity = false;
        littleBuddy = false;
    }

    public void EnableAll(){
        telekenisis = true;
        stasis = true;
        invertGravity = true;
        littleBuddy = true;
    }
}
