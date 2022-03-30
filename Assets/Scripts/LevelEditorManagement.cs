using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEditorManagement : MonoBehaviour
{
    public InputField sceneNameInput;
    public Dropdown sceneListDropDown;
    public Button getSelectedSceneButton;
    public Button updateSceneButton;
    public GameObject levelDataParent;

    [Header("SceneData Scriptable Objects Field")]
    public SceneData sceneDataLevel1;
    public SceneData sceneDataLevel2;
    public SceneData sceneDataLevel3;
    public SceneData sceneDataLevel4;
    public SceneData sceneDataLevel5;
    public SceneData sceneDataLevel6;
    public SceneData sceneDataLevel7;
    public SceneData sceneDataLevel8;
    public SceneData sceneDataLevel9;
    public SceneData sceneDataLevel10;
    public SceneData sceneDataLevel11;
    private SceneData selectedSceneData;

    [Header("Prefabs for instantiating level")]
    public GameObject mainPlatformPrefab;
    public GameObject holePlatformPrefab1;
    public GameObject holePlatformPrefab2;
    public GameObject holePlatformPrefab3;
    public GameObject ballPrefab;
    public GameObject environment1Prefab;
    public GameObject environment2Prefab;
    public GameObject environment3Prefab;
    
    private string selectedSceneName = "";
    private List<GameObject> editorSceneObjs = new List<GameObject>();

    private void OnEnable()
    {
        sceneListDropDown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(sceneListDropDown);
        });
    }
    private void OnDisable()
    {
        sceneListDropDown.onValueChanged.RemoveAllListeners();
    }


    public void OnAddSceneButtonClicked()
    {
        sceneNameInput.gameObject.SetActive(true);
    } // OnAddSceneButtonClicked

    public void OnLevelNameInputFieldEntered()
    {               
        //TODO:
        //Create a new scene and save it to assets/Scene folder
        
    } // OnLevelNameInputFieldEntered

    public void OnUpdateSceneButtonClicked()
    {
#if UNITY_EDITOR
        updateSceneButton.gameObject.SetActive(false);
        //Get all scenes added in build settings to list their names
        //Clean the scene first, or all levels will be spawned in a row     
        foreach (Transform obj in levelDataParent.transform)
        {
            if (obj == null)
                continue;
            Destroy(obj.gameObject);
        }
        
        //Then add scene names to dropdown list
        List<string> sceneNames = new List<string>();
        sceneNames.Add("Select Level");
        foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
        {
            string name = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
            name = name.Substring(0, name.Length - 6);
            if(name != "LoadingScene_RunThisFirst")
                sceneNames.Add(name);
        }        
        sceneListDropDown.ClearOptions();
        sceneListDropDown.AddOptions(sceneNames);
        sceneListDropDown.gameObject.SetActive(true);
#endif
    }

    private void DropdownValueChanged(Dropdown dropDown)
    {
        selectedSceneName = dropDown.captionText.text;
        //Debug.Log(selectedSceneName);
        if (selectedSceneName == "Select Level")
            getSelectedSceneButton.gameObject.SetActive(false);
        else
            getSelectedSceneButton.gameObject.SetActive(true);
    }
    /// <summary>
    /// This function works when Get Scene button is clicked
    /// Collects data from SceneData scriptable object
    /// and instantiate correct prefab on correct coordinates
    /// </summary>
    public void OnGetSceneButtonClicked()
    {
        sceneListDropDown.gameObject.SetActive(false);
        getSelectedSceneButton.gameObject.SetActive(false);
        updateSceneButton.gameObject.SetActive(true);
        selectedSceneData = null;

        switch (selectedSceneName)
        {
            case "Level1":
                selectedSceneData = sceneDataLevel1;
                break;
            case "Level2":
                Debug.Log("sceneDataLevel2");
                selectedSceneData = sceneDataLevel2;
                break;
            case "Level3":
                selectedSceneData = sceneDataLevel3;
                break;
            case "Level4":
                selectedSceneData = sceneDataLevel4;
                break;
            case "Level5":
                selectedSceneData = sceneDataLevel5;
                break;
            case "Level6":
                selectedSceneData = sceneDataLevel6;
                break;
            case "Level7":
                selectedSceneData = sceneDataLevel7;
                break;
            case "Level8":
                selectedSceneData = sceneDataLevel8;
                break;
            case "Level9":
                selectedSceneData = sceneDataLevel9;
                break;
            case "Level10":
                selectedSceneData = sceneDataLevel10;
                break;
            //This for test         
            default:
                Debug.LogError("Wrong SceneData selection");
                break;
        }

        foreach (var item in selectedSceneData.SceneObjects)
        {
            if(item.tag == Helper.Tags.mainPlatform)
            {
                GameObject obj = Instantiate(mainPlatformPrefab, item.position, item.rotation, levelDataParent.transform);
                editorSceneObjs.Add(obj);
            }
            else if(item.tag == Helper.Tags.holePlatform1)
            {
                GameObject obj = Instantiate(holePlatformPrefab1, item.position, item.rotation, levelDataParent.transform);
                editorSceneObjs.Add(obj);
            }
            else if (item.tag == Helper.Tags.holePlatform2)
            {
                GameObject obj = Instantiate(holePlatformPrefab2, item.position, item.rotation, levelDataParent.transform);
                editorSceneObjs.Add(obj);
            }
            else if (item.tag == Helper.Tags.holePlatform3)
            {
                GameObject obj = Instantiate(holePlatformPrefab3, item.position, item.rotation, levelDataParent.transform);
                editorSceneObjs.Add(obj);
            }
            else if(item.tag == Helper.Tags.ball)
            {
                GameObject obj = Instantiate(ballPrefab, item.position, item.rotation, levelDataParent.transform);
                editorSceneObjs.Add(obj);
            }
            else if(item.tag == Helper.Tags.environment1)
            {
                GameObject obj = Instantiate(environment1Prefab, item.position, item.rotation, levelDataParent.transform);
                editorSceneObjs.Add(obj);
            }
            else if (item.tag == Helper.Tags.environment2)
            {
                GameObject obj = Instantiate(environment2Prefab, item.position, item.rotation, levelDataParent.transform);
                editorSceneObjs.Add(obj);
            }
            else if (item.tag == Helper.Tags.environment3)
            {
                GameObject obj = Instantiate(environment3Prefab, item.position, item.rotation, levelDataParent.transform);
                editorSceneObjs.Add(obj);
            }
        }
    } // OnGetSceneButtonClicked

    /// <summary>
    /// This function saves new infos for scene objects
    /// After saving, go related scene and UpdateScene() func will update
    /// scene obj positions in SceneObjectDataContainer.cs
    /// Note: Didn't have time to code it.
    /// </summary>
    public void UpdateScene()
    {
        selectedSceneData.SceneObjects.Clear();
        foreach (GameObject obj in editorSceneObjs)
        {
            selectedSceneData.SceneObjects.Add(new SceneData.DataContainer
            {
                position = obj.transform.position,
                rotation = obj.transform.rotation,
                tag = obj.tag
            });
        }
    } // UpdateScene
} // Class
