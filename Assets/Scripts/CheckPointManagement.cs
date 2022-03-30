using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPointManagement : MonoBehaviour
{
    public ParticleSystem particle;
    public Animator risingPlatformAnimator;

    [SerializeField] private int checkPointNumber; // Set this number according to which checkpoint is this
                                                   //this value is sent to GamaManager class to change UI button color

    public delegate void CheckPointDelegation(int cPointNumber);
    public static event CheckPointDelegation PlayerReachedCheckPoint;
    public static event CheckPointDelegation PlayerLeavesCheckPoint;
    
    private bool isCollided = false;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Helper.Tags.player) && !isCollided)
        {
            //Debug.Log("CheckPoint Reached");
            isCollided = true;
            PlayerReachedCheckPoint?.Invoke(checkPointNumber);

            StartCoroutine(CheckPointCoroutinesFunc());

            particle.gameObject.SetActive(true);
        }
    } // OnTriggerEnter   

    IEnumerator CheckPointCoroutinesFunc()
    {        
        yield return new WaitForSeconds(5f);
        risingPlatformAnimator.SetTrigger(Helper.AnimationParameters.riseThePlatform);
        PlayerLeavesCheckPoint?.Invoke(checkPointNumber);        
    } // CheckPointCoroutinesFunc
} // Class
