using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DetectBallsCollected : MonoBehaviour
{
    private int ballCount;
    private float timer = 3f;
    private bool ballsCollided = false;
    private bool isBallNumberSent = false;
    private SceneManagement sceneManagement;
    
    public TextMeshPro textBallCount;

    private void Start()
    {
        sceneManagement = GameObject.FindGameObjectWithTag(Helper.Tags.sceneManagement).GetComponent<SceneManagement>();
    }

    private void FixedUpdate()
    {
        textBallCount.text = ballCount.ToString();

        //if balls collided wait 3 seconds to get number of collected balls.
        if (ballsCollided && timer <= 5)
            timer += Time.fixedDeltaTime;

        if (timer > 5 && !isBallNumberSent)
        {
            isBallNumberSent = true;
            sceneManagement.BallsCollectedIncrement(ballCount);
        }
    } // FixedUpdate


    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag(Helper.Tags.ball))
        {
            Debug.Log("Ball collided");
            ballsCollided = true;
            ballCount++;
        }
    } // OnCollisionEnter

} // OnCollisionEnter
