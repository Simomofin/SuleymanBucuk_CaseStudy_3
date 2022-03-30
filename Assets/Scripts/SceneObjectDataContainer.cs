using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class SceneObjectDataContainer : MonoBehaviour
{    
     
    [SerializeField]
    private GameObject[] sceneObjects;    
    public SceneData sceneData;

    public GameObject mainPlatformPrefab;
    public GameObject holePlatformPrefab1;
    public GameObject holePlatformPrefab2;
    public GameObject holePlatformPrefab3;
    public GameObject ballPrefab;
    public GameObject environment1Prefab;
    public GameObject environment2Prefab;
    public GameObject environment3Prefab;
    public GameObject levelDataParent;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScene();
        GetAllObjectsOnThisScene();
    }

    private void UpdateScene()
    {
        //TODO: Update scene objects via SceneData scriptable object, had no time to code this...
    }


    private void GetAllObjectsOnThisScene()
    {
        //sceneData.SceneObjects.Clear();
        sceneObjects = FindObjectsOfType<GameObject>();        

        foreach (GameObject go in sceneObjects)
        {
            //Add balls to SceneData scriptable object
            if (go.CompareTag(Helper.Tags.ball) || go.CompareTag(Helper.Tags.mainPlatform) || go.CompareTag(Helper.Tags.checkPoint1) 
                                      || go.CompareTag(Helper.Tags.checkPoint2) || go.CompareTag(Helper.Tags.checkPoint3) || go.CompareTag(Helper.Tags.holePlatform1) || go.CompareTag(Helper.Tags.holePlatform2)
                                       || go.CompareTag(Helper.Tags.holePlatform3) || go.CompareTag(Helper.Tags.platformSide) || go.CompareTag(Helper.Tags.environment1)
                                        || go.CompareTag(Helper.Tags.environment2) || go.CompareTag(Helper.Tags.environment3))
            {
                SetDataContainerValues(go);
            }
            //else if (go.CompareTag(Helper.Tags.mainPlatform))
            //{
            //    SetDataContainerValues(go);
            //}
        }
    } // GetAllObjectsOnThisScene
    private void SetDataContainerValues(GameObject gameobj)
    {
        SceneData.DataContainer dataCont = new SceneData.DataContainer()
        {
            position = gameobj.transform.position,
            rotation = gameobj.transform.rotation,
            tag = gameobj.tag
        };        
        //Debug.Log(dataCont.tag + " " + dataCont.rotation + " " + dataCont.position);
        sceneData.SceneObjects.Add(dataCont);        
    } // SetDataContainerValues


} // Class
