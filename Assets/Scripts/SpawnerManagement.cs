using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagement : MonoBehaviour
{
    public bool isSpawned = false;
    public bool isCollidedWithMainPlatform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Helper.Tags.mainPlatform))
        {
            isCollidedWithMainPlatform = true;
        }
        else
            isCollidedWithMainPlatform = false;
    }
}
