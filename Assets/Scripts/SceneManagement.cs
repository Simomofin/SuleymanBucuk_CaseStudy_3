using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public Button checkPoint1Btn, checkPoint2Btn, checkPoint3Btn;
    public GameObject gameWonUI, gameLoseUI;
    public GameObject gameWonParticle1, gameWonParticle2;

    private bool isGameStarted = false;    
    public bool GameHasStarted => isGameStarted;
    public int BallsCollected { get; private set; }
    public int TotalBallsSpawnd { get; private set; }

    [Header("Textures")]
    public Texture[] colourPattern1;
    public Texture[] colourPattern2;
    public Texture[] colourPattern3;
    public Texture[] colourPattern4;
    public Texture[] colourPattern5;
    public Texture[] colourPattern6;
    public Texture[] colourPattern7;
    public Texture[] colourPattern8;
    public Texture[] colourPattern9;
    public Texture[] colourPattern10;

    private Texture[] colourpattern;


    private void OnEnable()
    {
        CheckPointManagement.PlayerReachedCheckPoint += CheckpointUIColorChange;
        CheckPointManagement.PlayerLeavesCheckPoint += CheckLevelFinish;
    }
    private void OnDisable()
    {
        CheckPointManagement.PlayerReachedCheckPoint -= CheckpointUIColorChange;
        CheckPointManagement.PlayerLeavesCheckPoint -= CheckLevelFinish;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetLevelNumberText();
        SetGameObjectTexture();

        // Set timescale zero to wait for touch input
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (!isGameStarted && Input.GetMouseButton(0))
        {
            isGameStarted = true;
            Time.timeScale = 1;
        }
    } // FixedUpdate

    private void SetLevelNumberText()
    {
        currentLevelText.text = GameManager.sharedInstance.LevelNumber.ToString();
        nextLevelText.text = (GameManager.sharedInstance.LevelNumber + 1).ToString();
    } // SetLevelNumberText

    public void CheckpointUIColorChange(int cpNumber)
    {
        Color color = new Color32(190, 16, 14, 255);

        switch (cpNumber)
        {
            case 1:
                checkPoint1Btn.image.color = color;
                break;

            case 2:
                checkPoint2Btn.image.color = color;
                break;

            case 3:
                checkPoint3Btn.image.color = color;
                break;
            default:
                break;
        }
    } // CheckpointUIColorChange

    private void CheckLevelFinish(int cpNumber)
    {
        if (cpNumber == 3)
        {
            if(PlayerPrefs.HasKey(Helper.PlayerPrefs.ballPercentageToWin))
            {
                GameManager.sharedInstance.percentageofBallsToPassLevel = PlayerPrefs.GetFloat(Helper.PlayerPrefs.ballPercentageToWin);
            }
            else
            {
                PlayerPrefs.SetFloat(Helper.PlayerPrefs.ballPercentageToWin, 0.4f); //set the percentage to %40 at the very beginning
                GameManager.sharedInstance.percentageofBallsToPassLevel = PlayerPrefs.GetFloat(Helper.PlayerPrefs.ballPercentageToWin);
                Debug.Log("Percentage to win at the beginning: " + GameManager.sharedInstance.percentageofBallsToPassLevel);
            }

            // -* Check if enough ball collected.
            float collectedBallPercentage = (float)BallsCollected / (float)TotalBallsSpawnd;
            Debug.Log("Collected Ball Percentage: " + collectedBallPercentage);
            if (collectedBallPercentage >= GameManager.sharedInstance.percentageofBallsToPassLevel)
            {
                // -* increase level number
                // -* increase difficulty
                // -* activate game won UI
                Debug.Log("You Won");
                gameWonUI.SetActive(true);
                gameWonParticle1.SetActive(true);
                gameWonParticle2.SetActive(true);
                GameManager.sharedInstance.IncreaseLevelNumber();
            }
            else
            {
                // -* if not, activate gameLoseUI
                gameLoseUI.SetActive(true);
            }
        }
    } // CheckLevelFinish

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

    public void OnReplayButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } // OnReplayButtonClicked

    private void SetGameObjectTexture()
    {
        GetColourPattern();

        //Main platform textures
        GameObject[] mainPlatforms = GameObject.FindGameObjectsWithTag(Helper.Tags.mainPlatform);
        for (int i = 0; i < mainPlatforms.Length; i++)
        {
            mainPlatforms[i].GetComponent<Renderer>().material.SetTexture("_MainTex", colourpattern[0]);
        }
        //Platform side textures
        GameObject[] platformSides = GameObject.FindGameObjectsWithTag(Helper.Tags.platformSide);
        for (int i = 0; i < platformSides.Length; i++)
        {
            platformSides[i].GetComponent<Renderer>().material.SetTexture("_MainTex", colourpattern[1]);
        }
        //Hole Platform1-2-3 textures
        GameObject[] holePlatforms1 = GameObject.FindGameObjectsWithTag(Helper.Tags.holePlatform1);
        for (int i = 0; i < holePlatforms1.Length; i++)
        {
            if (holePlatforms1[i].GetComponent<Renderer>())
                holePlatforms1[i].GetComponent<Renderer>().material.SetTexture("_MainTex", colourpattern[2]);
        }
        GameObject[] holePlatforms2 = GameObject.FindGameObjectsWithTag(Helper.Tags.holePlatform2);
        for (int i = 0; i < holePlatforms2.Length; i++)
        {
            if (holePlatforms2[i].GetComponent<Renderer>())
                holePlatforms2[i].GetComponent<Renderer>().material.SetTexture("_MainTex", colourpattern[2]);
        }
        GameObject[] holePlatforms3 = GameObject.FindGameObjectsWithTag(Helper.Tags.holePlatform3);
        for (int i = 0; i < holePlatforms3.Length; i++)
        {
            if (holePlatforms3[i].GetComponent<Renderer>())
                holePlatforms3[i].GetComponent<Renderer>().material.SetTexture("_MainTex", colourpattern[2]);
        }
        //Balls textures will be assigned on an overriden func, because balls will be spawned  from SpawnManager.cs        
        //Player texture
        GameObject player = GameObject.FindGameObjectWithTag(Helper.Tags.player);
        player.GetComponent<Renderer>().material.SetTexture("_MainTex", colourpattern[2]);

    } // SetLevelTexture

    private void GetColourPattern()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;


        switch (sceneNumber)
        {
            case 1:
                colourpattern = colourPattern1;
                break;

            case 2:
                colourpattern = colourPattern2;
                break;
            case 3:
                colourpattern = colourPattern3;
                break;
            case 4:
                colourpattern = colourPattern4;
                break;
            case 5:
                colourpattern = colourPattern5;
                break;
            case 6:
                colourpattern = colourPattern6;
                break;
            case 7:
                colourpattern = colourPattern7;
                break;
            case 8:
                colourpattern = colourPattern8;
                break;
            case 9:
                colourpattern = colourPattern9;
                break;
            case 10:
                colourpattern = colourPattern10;
                break;

            default:
                break;
        }
    } // GetColourPattern

    public void SetGameObjectTexture(GameObject obj)
    {
        GetColourPattern();
        obj.GetComponent<Renderer>().material.SetTexture("_MainTex", colourpattern[3]);
    } // SetLevelTexture  

    public void TotalBallSpawnIncrement(int count)
    {
        TotalBallsSpawnd += count;
    }
    public void BallsCollectedIncrement(int count)
    {
        BallsCollected += count;
    }
}
