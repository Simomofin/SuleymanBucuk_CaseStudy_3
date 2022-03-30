using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When game has started, this script disables
/// UI animated "Tap To Start" images and texts
/// </summary>
public class AnimationDisabler : MonoBehaviour
{
    private SceneManagement sceneManagement;

    private void Start()
    {
        sceneManagement = GameObject.FindGameObjectWithTag(Helper.Tags.sceneManagement).GetComponent<SceneManagement>();
    }
    void FixedUpdate()
    {
        if(sceneManagement.GameHasStarted)
        {
            gameObject.SetActive(false);
        }
    } // FixedUpdate
} // Class
