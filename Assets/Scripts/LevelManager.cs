using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
            }

            return _instance;
        }
    }

    public Transform playerSpawnPoint;
    public List<Transform> levels;

    void Start()
    {
        SwitchLevel(0);
    }

    public void SwitchLevel(int levelIndex)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (i == levelIndex)
            {
                levels[i].gameObject.SetActive(true);
            }
            else
            {
                levels[i].gameObject.SetActive(false);
            }
        }

        Player.Instance.transform.position = playerSpawnPoint.position;
    }
}
