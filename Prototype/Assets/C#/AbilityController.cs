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
    private bool trueTele;
    public bool stasis;
    public Image stasisIcon;
    private bool trueStasis;
    public bool invertGravity;
    public Image GravityIcon;
    private bool trueGravity;
    public bool littleBuddy;
    public Image BuddyIcon;
    public bool trueBuddy;

    [Header("Conditions")]
    public bool carryingMetal;

    [Header("Book")]
    public bool hasBook = true;

    void Start(){
        trueTele = telekenisis;
        trueStasis = stasis;
        trueGravity = invertGravity;
        trueBuddy = littleBuddy;
    }
    public void DisableAll(){
        telekenisis = false;
        stasis = false;
        invertGravity = false;
        littleBuddy = false;
    }

    public void EnableAll(){
        if(trueTele){
            telekenisis = true;
        }
        if(trueStasis){
            stasis = true;
        }
        if(trueGravity){
            invertGravity = true;
        }
        if(trueBuddy){
            littleBuddy = true;
        }
    }

    private void Update(){
        if(!telekenisis && telekenisisIcon != null){
            telekenisisIcon.color = transparent;
        }
        else if(telekenisisIcon != null){
            telekenisisIcon.color = full;
        }
        if(!stasis && stasisIcon != null){
            stasisIcon.color = transparent;
        }
        else if(stasisIcon != null){
            stasisIcon.color = full;
        }
        if(!invertGravity && GravityIcon != null){
            GravityIcon.color = transparent;
        }
        else if(GravityIcon != null){
            GravityIcon.color = full;
        }
        if(!littleBuddy && BuddyIcon != null){
            BuddyIcon.color = transparent;
        }
        else if(BuddyIcon != null){
            BuddyIcon.color = full;
        }
    }
}
