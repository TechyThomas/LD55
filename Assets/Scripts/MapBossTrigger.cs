using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBossTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject doorTileMap;

    void Start()
    {
        boss.SetActive(false);
        doorTileMap.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        boss.SetActive(true);
        doorTileMap.SetActive(true);


        gameObject.SetActive(false);
    }
}
