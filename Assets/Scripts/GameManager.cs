using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SceneManagement sceneManagement;
    public static GameManager sharedInstance;
       
    public float percentageofBallsToPassLevel; //this indicates that if enough ball is collected to pass this level - balssCollected / totalBallsSpawned    

    public int LevelNumber{ get; private set;}

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (sharedInstance != null)
            Destroy(gameObject);
        else
            sharedInstance = this;

        //PlayerPrefs.DeleteAll();        
       
        CheckCurrentLevel();
    } // Awake
        
    private void CheckCurrentLevel()
    {
        if (PlayerPrefs.HasKey(Helper.PlayerPrefs.currentLevel))
        {
            //Debug.Log("Level playerpref is called before, now checking current level");
            LevelNumber = PlayerPrefs.GetInt(Helper.PlayerPrefs.currentLevel);
            //Debug.Log(LevelNumber);
        }
        else
        {
            //Debug.Log("Level playerpref is called for the first time");
            PlayerPrefs.SetInt(Helper.PlayerPrefs.currentLevel, 1);
            LevelNumber = (PlayerPrefs.GetInt(Helper.PlayerPrefs.currentLevel));
            //Debug.Log(LevelNumber);
        }
    } // CheckCurrentLevel

    /// <summary>
    /// This function increase current level number by 1,
    /// This is called from a button press event in UI->Game won panel
    /// </summary>
    public void IncreaseLevelNumber()
    {
        IncreaseDifficulty(); //When level passed, game gets harder

        //Debug.Log("Current Level" + LevelNumber);
        LevelNumber++;
        PlayerPrefs.SetInt(Helper.PlayerPrefs.currentLevel, LevelNumber);
        //Debug.Log("Next Level" + PlayerPrefs.GetInt(Helper.PlayerPrefs.currentLevel));        
    } // IncreaseLevelNumber      

    public void IncreaseDifficulty()
    {
        // *- get values of howManyBallSpawner, player speed and percentage of ball collected from prefabs
        // +- set howManyBallSpawner will be spawned, increase as the level increase
        // *- set players max speed (forward speed)
        // *- set percentage of ball collected, increase as the level increase
        // *- save these values to playerprefs
        
        //increase spawner count as the level passed
        int _spawnerCount = PlayerPrefs.GetInt(Helper.PlayerPrefs.ballSpawnerCount);
        _spawnerCount += 1;  // inscrease spawner count by 1 on every level passed       
        PlayerPrefs.SetInt(Helper.PlayerPrefs.ballSpawnerCount, _spawnerCount);
        //Debug.Log("HowManySpawnerCount :" + PlayerPrefs.GetInt(Helper.PlayerPrefs.ballSpawnerCount));

        //increase PlayerSpeed as the level passed;
        float _playerSpeed = PlayerPrefs.GetFloat(Helper.PlayerPrefs.playerSpeed);
        _playerSpeed += 0.05f; //increase playerspeed by 0.05 on every level passed
        _playerSpeed = _playerSpeed > 15 ? 15 : _playerSpeed; //Don't let player speed to be more than 15.
        PlayerPrefs.SetFloat(Helper.PlayerPrefs.playerSpeed, _playerSpeed);
        //Debug.Log("Player Speed: " + _playerSpeed);

        //increase SwipeSpeed as the level passed
        float _swipeSpeed = PlayerPrefs.GetFloat(Helper.PlayerPrefs.swipeSpeed);
        _swipeSpeed += 0.02f; // increase swipe speed by 0.02 on every level passed
        _swipeSpeed = _swipeSpeed > 6 ? 6 : _swipeSpeed; // Don't let swipe speed to be more than 6
        PlayerPrefs.SetFloat(Helper.PlayerPrefs.swipeSpeed, _swipeSpeed);
        //Debug.Log("Swipe Speed: " + _swipeSpeed);

        //increase percentage of collected balls to win
        percentageofBallsToPassLevel = PlayerPrefs.GetFloat(Helper.PlayerPrefs.ballPercentageToWin);
        percentageofBallsToPassLevel += 0.005f; // increase win percentage %0.5 by every level passed
        percentageofBallsToPassLevel = percentageofBallsToPassLevel > 70 ? 70 : percentageofBallsToPassLevel; //max percentage to win is %70
        PlayerPrefs.SetFloat(Helper.PlayerPrefs.ballPercentageToWin, percentageofBallsToPassLevel);
        Debug.Log("Percentage to win: " + percentageofBallsToPassLevel);
    }

    public void LoadNextScene()
    {
        int rand = Random.Range(0, 11); // Randomize between any scene on built settings, we have 10 on this case

        string newSceneName = SceneUtility.GetScenePathByBuildIndex(rand);
        //Debug.Log(newSceneName);
        //This controls if LoadScene is not loaded again
        if (newSceneName != "Assets/Scenes/LoadingScene_RunThisFirst.unity")
            SceneManager.LoadScene(rand);
        else
        {
            Debug.Log("LoadScene selected, i will select a new scene to load");
            LoadNextScene();
        }
    } // LoadNextScene

} // Class
