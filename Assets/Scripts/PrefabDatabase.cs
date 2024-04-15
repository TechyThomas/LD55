using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDatabase : MonoBehaviour
{
    public static PrefabDatabase _instance;
    public static PrefabDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PrefabDatabase>();
            }

            return _instance;
        }
    }

    public LayerMask GROUND_LAYER;

    public GameObject SWORD_ATTACK;

    public GameObject SLIME_ENEMY;
    public GameObject SLIME_BALL;
    public GameObject SLIME_TRAP;

    public GameObject SPIDER_ENEMY;

    public GameObject SKELETON_ENEMY;
}
