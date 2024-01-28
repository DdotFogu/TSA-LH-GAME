using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour
{
    public Color transparent;
    public Color full;

    [Header("Magic")]
    public bool telekenisis;
    public Image telekenisisIcon;
    public bool stasis;
    public Image stasisIcon;
    public bool invertGravity;
    public Image GravityIcon;
    public bool littleBuddy;
    public Image BuddyIcon;

    [Header("Conditions")]
    public bool carryingMetal;

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

    private void Update(){
        if(!telekenisis){
            telekenisisIcon.color = transparent;
        }
        else{
            telekenisisIcon.color = full;
        }
        if(!stasis){
            stasisIcon.color = transparent;
        }
        else{
            stasisIcon.color = full;
        }
        if(!invertGravity){
            GravityIcon.color = transparent;
        }
        else{
            GravityIcon.color = full;
        }
        if(!littleBuddy){
            BuddyIcon.color = transparent;
        }
        else{
            BuddyIcon.color = full;
        }
    }
}
