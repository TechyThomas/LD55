using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject SpawnPrefab(GameObject prefab, Vector2 pos)
    {
        return Instantiate(prefab, pos, Quaternion.identity);
    }
}
