using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawnManagement : MonoBehaviour
{
    public GameObject spawnerPrefab;
    public GameObject ballPrefab;
    [Range(10,90)] // set the spawner count between 10 and 90
    public int howManySpawner;
    public int ballSpawnCountPerSpawner; //specifies "how many balls wil be spawned" in a pawnerPrefab, increse this value to get harder every level pass
    public float distanceToSpawnBall; // specifies "how much distance between player and the spawner" the spawner will run. Decrease this value to get harder every level pass

    private PlayerController player;
    private float distance; //tracks distance between player and spawnerPrefab    
    public List<GameObject> spawnerList = new List<GameObject>();
    private bool spawned;

    private float randX;
    private float randZ;
    private float spawnOfset = 0.1f;

    private SceneManagement sceneManagement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Helper.Tags.player).GetComponent<PlayerController>();
        sceneManagement = GameObject.FindGameObjectWithTag(Helper.Tags.sceneManagement).GetComponent<SceneManagement>();
        GenerateSpawnerPrefabs();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < spawnerList.Count; i++)
        {
            distance = Vector3.Distance(player.transform.position, spawnerList[i].transform.position);
            Mathf.Abs(distance);
            spawned = spawnerList[i].GetComponent<SpawnerManagement>().isSpawned;
            if(distance < distanceToSpawnBall && !spawned)
            {
                spawnerList[i].GetComponent<SpawnerManagement>().isSpawned = true;
                SpawnBall(spawnerList[i]);                
            }
        }
    } // FixedUpdate
    /// <summary>
    /// This function spawns ballPrefab object on a random position around its spawner object
    /// </summary>
    /// <param name="spawner"></param>
    private void SpawnBall(GameObject spawner)
    {
        for (int i = 0; i < ballSpawnCountPerSpawner; i++)
        {            
            GameObject ball = Instantiate(ballPrefab, new Vector3(
                                                                  spawner.transform.position.x + Random.Range(spawnOfset, - spawnOfset),
                                                                  spawner.transform.position.y + Random.Range(spawnOfset, -spawnOfset),
                                                                  spawner.transform.position.z + Random.Range(spawnOfset, -spawnOfset)),
                                                                  Quaternion.identity, transform);
            sceneManagement.SetGameObjectTexture(ball); // Set ball texture
            ball.GetComponent<Rigidbody>().AddForce(0, 0.5f, 0, ForceMode.Force); // For spawn effect
            sceneManagement.TotalBallSpawnIncrement(1);
        }
        spawner.SetActive(false);
        spawnerList.Remove(spawner);
    }
    /// <summary>
    /// This function generate random Spawners at randon spawn points
    /// and add them to spawnerList.
    /// Spawner count relay on howManySpawner amount. This amount can be increased to get harder level by level.
    /// </summary>
    private void GenerateSpawnerPrefabs()
    {
        if (PlayerPrefs.HasKey(Helper.PlayerPrefs.ballSpawnerCount))
        {
            howManySpawner = PlayerPrefs.GetInt(Helper.PlayerPrefs.ballSpawnerCount);
            //Debug.Log("Spawner count at game start :" + PlayerPrefs.GetInt(Helper.PlayerPrefs.ballSpawnerCount));
        }
        else
        {
            PlayerPrefs.SetInt(Helper.PlayerPrefs.ballSpawnerCount, 10); // 10 spawner for the first time game runs
            howManySpawner = 10; // Set it to according class
            //Debug.Log("Spawner count at the very beginning of game :" + PlayerPrefs.GetInt(Helper.PlayerPrefs.ballSpawnerCount));
        }
        howManySpawner = howManySpawner > 90 ? 90 : howManySpawner; // Don't let spawner count to be more than 90

        List<float> zPOSList = new List<float>();        

        int objPerCheckPoint = howManySpawner / 3;

        #region From start to first checkpoint, set the spawners

        for (int i = 0; i < objPerCheckPoint; i++)
        {
            if (i == 0)
            {
                randZ = GameObject.FindGameObjectWithTag(Helper.Tags.player).transform.position.z + 5f; // first spawnew should be 5 unitf further than player
                zPOSList.Add(randZ);
            }            
            else
            {
                float offset = 37 / (objPerCheckPoint - 1); // 37 -> is the unit between the start point and first checkpoint
                float newRange = zPOSList[i - 1] + offset; // sets a new range to declare distance
                //Debug.Log(newRange);
                randZ = Random.Range(zPOSList[i - 1] + (offset * 0.5f), newRange); // selects a new random spawn point from 2 units forward of previous spawn and newRange.                
                zPOSList.Add(zPOSList[i - 1] + offset);
            }
            //Debug.Log(randZ);
            randX = Random.Range(-1.55f, 1.55f); //x position is between 1.55 and -1.55

            GameObject spawner = Instantiate(spawnerPrefab, Vector3.zero, Quaternion.identity, transform);
            sceneManagement.SetGameObjectTexture(spawner);
            spawner.transform.SetParent(transform);
            spawner.transform.localPosition = new Vector3(randX, 0.33f, randZ); // set it to new local position
            spawner.SetActive(true);
            spawnerList.Add(spawner);            
        }
        #endregion

        zPOSList.Clear(); // Clear the list for second checkpoint

        #region From first checkpoint to second checkpoint, set the spawners

        for (int i = 0; i < objPerCheckPoint; i++)
        {            
            if (i == 0)
            {
                randZ = 55f; // just after first checkpoint
                zPOSList.Add(randZ);
            }
            else
            {
                float offset = 40 / (objPerCheckPoint - 1); // 40 -> is the unit between the first checkpoint and second checkpoint
                float newRange = zPOSList[i - 1] + offset; // sets a new range to declare distance
                //Debug.Log(newRange);
                randZ = Random.Range(zPOSList[i - 1] + (offset / 2), newRange); // selects a new random spawn point from 2 units forward of previous spawn and newRange.                
                zPOSList.Add(zPOSList[i - 1] + offset);
            }
            //Debug.Log(randZ);
            randX = Random.Range(-1.55f, 1.55f); //x position is between 1.55 and -1.55

            GameObject spawner = Instantiate(spawnerPrefab, Vector3.zero, Quaternion.identity, transform);
            sceneManagement.SetGameObjectTexture(spawner);
            spawner.transform.SetParent(transform);
            spawner.transform.localPosition = new Vector3(randX, 0.33f, randZ); // set it to new local position
            spawner.SetActive(true);
            spawnerList.Add(spawner);
        }
        #endregion

        zPOSList.Clear(); // Clear the list for third checkpoint

        #region From second checkpoint to third checkpoint, set the spawners

        int lastStageSpawnerCount = howManySpawner - (objPerCheckPoint * 2);
        for (int i = 0; i < lastStageSpawnerCount; i++)
        {
            if (i == 0)
            {
                randZ = 105f; // just after second checkpoint
                zPOSList.Add(randZ);
            }
            else
            {
                float offset = 40 / (lastStageSpawnerCount - 1); // 40 -> is the unit between the second checkpoint and third checkpoint
                float newRange = zPOSList[i - 1] + offset; // sets a new range to declare distance
                //Debug.Log(newRange);
                randZ = Random.Range(zPOSList[i - 1] + (offset / 2), newRange); // selects a new random spawn point from 2 units forward of previous spawn and newRange.                
                zPOSList.Add(zPOSList[i - 1] + offset);
            }
            //Debug.Log(randZ);
            randX = Random.Range(-1.55f, 1.55f); //x position is between 1.55 and -1.55

            GameObject spawner = Instantiate(spawnerPrefab, Vector3.zero, Quaternion.identity, transform);
            sceneManagement.SetGameObjectTexture(spawner);
            spawner.transform.SetParent(transform);
            spawner.transform.localPosition = new Vector3(randX, 0.33f, randZ); // set it to new local position
            spawner.SetActive(true);
            spawnerList.Add(spawner);
        }
        #endregion

    } // GenerateSpawnerPrefabs   
}
