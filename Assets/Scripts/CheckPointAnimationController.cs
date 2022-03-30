using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointAnimationController : MonoBehaviour
{
    private Animator anim;

    private void OnEnable()
    {
        CheckPointManagement.PlayerLeavesCheckPoint += CheckPointAnimationStart;
    }
    private void OnDisable()
    {
        CheckPointManagement.PlayerLeavesCheckPoint -= CheckPointAnimationStart;
    }
 
    /// <summary>
    /// This Function is triggered by unity event on CheckPointManagement.cs
    /// </summary>
    public void CheckPointAnimationStart(int cpNumber)
    {
        anim = GetComponent<Animator>();
        if (!anim)
            Debug.LogError("I need an animator here!");
        else
        {
            if(cpNumber == 1)
                anim.SetBool(Helper.AnimationParameters.checkPoint1Reached, true);
            else if(cpNumber == 2)
                anim.SetBool(Helper.AnimationParameters.checkPoint2Reached, true);
            else if(cpNumber ==3)
                anim.SetBool(Helper.AnimationParameters.checkPoint3Reached, true);

        }
    } // CheckPointAnimationStart

    /// <summary>
    /// This function is called by animation event in animation clip
    /// </summary>
    //public void SetBoolFalse()
    //{
    //    anim.SetBool(Helper.AnimationParameters.checkPointReached, false);
    //} // SetBoolFalse
} // Class
