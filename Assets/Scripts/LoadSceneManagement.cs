using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManagement : MonoBehaviour
{
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

    private void Start()
    {
        SetGameObjectTexture();
    }

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

    } // SetLevelTexture

    private void GetColourPattern()
    {
        int rand = Random.Range(0, 10);


        switch (rand)
        {
            case 0:
                colourpattern = colourPattern1;
                break;

            case 1:
                colourpattern = colourPattern2;
                break;
            case 2:
                colourpattern = colourPattern3;
                break;
            case 3:
                colourpattern = colourPattern4;
                break;
            case 4:
                colourpattern = colourPattern5;
                break;
            case 5:
                colourpattern = colourPattern6;
                break;
            case 6:
                colourpattern = colourPattern7;
                break;
            case 7:
                colourpattern = colourPattern8;
                break;
            case 8:
                colourpattern = colourPattern9;
                break;
            case 9:
                colourpattern = colourPattern10;
                break;

            default:
                break;
        }
    } // GetColourPattern
} // Class
