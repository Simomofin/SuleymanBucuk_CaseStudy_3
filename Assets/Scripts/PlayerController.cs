using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Movement Variables
    private bool firstMove = false;
    private float _firstFingerPositionX;
    private float _newMoveX;
    private float _lastFingerPositionX;
    private float _currentFingerPositionX;
    private float _moveX;
    private float _lastMoveX;

    #endregion

    //private Touch touch;
    private Rigidbody rb;
    //private float slideAmontOnXAxis;
    private Vector3 rbVelocity;
    private bool checkPointReached = false;
    private SceneManagement sceneManagement;
    private Vector3 movePosition;
    
    public float PlayerSpeed { get; set; }
    public float SwipeSpeed { get; set; }

    private void OnEnable()
    {
        CheckPointManagement.PlayerReachedCheckPoint += PlayerReachedCheckPoint;
        CheckPointManagement.PlayerLeavesCheckPoint += PlayerLeaveCheckPoint;
    }

    private void OnDisable()
    {
        CheckPointManagement.PlayerReachedCheckPoint -= PlayerReachedCheckPoint;
        CheckPointManagement.PlayerLeavesCheckPoint -= PlayerLeaveCheckPoint;
    }

    private void Awake()
    {
        SetPlayerAndSwipeSpeed();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogError("Set me a Rigidbody !");

        sceneManagement = GameObject.FindGameObjectWithTag(Helper.Tags.sceneManagement).GetComponent<SceneManagement>();
    } // Start

    private void Update()
    {
        if(sceneManagement.GameHasStarted && !checkPointReached)
        {
            Movement();
        }
    } // Update
    void FixedUpdate()
    {        
        if(sceneManagement.GameHasStarted && !checkPointReached)
        {
            movePosition = new Vector3(transform.position.x + (_moveX * SwipeSpeed * Time.fixedDeltaTime), transform.position.y, transform.position.z + (PlayerSpeed * Time.fixedDeltaTime));
            // Player should stay on the platform so clamp x pos
            if (movePosition.x > 1.73f)
                movePosition = new Vector3(1.73f, movePosition.y, movePosition.z);
            else if(movePosition.x < -1.73f)
                movePosition = new Vector3(-1.73f, movePosition.y, movePosition.z);

            rb.MovePosition(movePosition);
            

            #region Working With Mobile Platform Touches Codes (Commented Out)

            //if (Input.touchCount > 0)
            //{
            //    touch = Input.GetTouch(0);
            //    if (touch.phase == TouchPhase.Moved)
            //        slideAmontOnXAxis = touch.deltaPosition.x * Time.fixedDeltaTime * swipeSpeed;
            //}
            //else
            //    slideAmontOnXAxis = 0;

            //rbVelocity = new Vector3(swipeSpeed * slideAmontOnXAxis, 0, forwardSpeed);

            //ApplyForceTillReachMaxSpeed(rb, rbVelocity, 10);

            //playerSpeed = rb.velocity.magnitude;
            #endregion
        }
    } // FixedUpdate

    #region Touch Style Force Apply (Commented Out)
    /// <summary>
    /// This function applies force to the rigidbody until it reaches a predetermined speed
    /// When it reaches the desired speed, keeps it at constant speed.
    /// Later Summary: After commented touch codes, this function no need to be used
    /// </summary>
    /// <param name="rigidbody"></param>
    /// <param name="velocity"></param>
    /// <param name="force"></param>
    /// <param name="mode"></param>
    public void ApplyForceTillReachMaxSpeed(Rigidbody rigidbody, Vector3 velocity, float force, ForceMode mode = ForceMode.Force)
    {
        if (force == 0 || velocity.magnitude == 0)
            return;
        velocity = velocity + velocity.normalized * 0.2f * rigidbody.drag;

        force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);
        if (rigidbody.velocity.magnitude == 0)
        {
            rigidbody.AddForce(velocity * force, mode);
        }
        else
        {
            var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude);
            rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
        }
    } // ApplyForceTillReachMaxSpeed

    #endregion

    public void PlayerReachedCheckPoint(int cpNumber)
    {              
        checkPointReached = true;
        //rb.isKinematic = true;
    } // PlayerReachedCheckPoint

    public void PlayerLeaveCheckPoint(int cpNumber)
    {
        if (cpNumber == 3)
            return;
        //Debug.Log("Player leaves checkpoint");
        checkPointReached = false;        
    } // PlayerLeaveCheckPoint

    private void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstMove = true;
            _firstFingerPositionX = Input.mousePosition.x;            
        }
        else if (Input.GetMouseButton(0))
        {            
            if (firstMove)
            {                
                _lastFingerPositionX = _firstFingerPositionX;                
                firstMove = false;
            }
            else
            {                
                _newMoveX = Input.mousePosition.x - _lastFingerPositionX; // movement on x axis on screen, moves player on x axis straight
                _moveX = Mathf.Lerp(_moveX, _newMoveX, Time.fixedDeltaTime);
                _moveX = Mathf.Clamp(_moveX, -1, 1);
                _lastFingerPositionX = Input.mousePosition.x;             
            }            
        } 
        else
        {
            //Reset values, if you dont, rb moves in last direction      
            _moveX = 0;
            _firstFingerPositionX = 0;
            _lastFingerPositionX = 0;

            
        }
    } // Movement

    private void SetPlayerAndSwipeSpeed()
    {
        //Get Player Speed
        if (PlayerPrefs.HasKey(Helper.PlayerPrefs.playerSpeed))
        {
            PlayerSpeed = PlayerPrefs.GetFloat(Helper.PlayerPrefs.playerSpeed);
            Debug.Log("Player Speed: " + PlayerPrefs.GetFloat(Helper.PlayerPrefs.playerSpeed));
        }
        else
        {
            PlayerPrefs.SetFloat(Helper.PlayerPrefs.playerSpeed, 5f);
            PlayerSpeed = 5f; // set the speed to 5 when game runs for the first time
            Debug.Log("Player Speed at the very beginning: " + PlayerPrefs.GetFloat(Helper.PlayerPrefs.playerSpeed));
        }

        // Get Swipe Speed
        if (PlayerPrefs.HasKey(Helper.PlayerPrefs.swipeSpeed))
        {
            SwipeSpeed = PlayerPrefs.GetFloat(Helper.PlayerPrefs.swipeSpeed);
            Debug.Log("Swipe Speed: " + PlayerPrefs.GetFloat(Helper.PlayerPrefs.swipeSpeed));
        }
        else
        {
            PlayerPrefs.SetFloat(Helper.PlayerPrefs.swipeSpeed, 3f);
            SwipeSpeed = 3f; // set the swipe speed to 3 when game runs for the first time
            Debug.Log("Player Speed at the very beginning: " + PlayerPrefs.GetFloat(Helper.PlayerPrefs.swipeSpeed));
        }
    } // SetPlayerAndSwipeSpeed

} // Class
