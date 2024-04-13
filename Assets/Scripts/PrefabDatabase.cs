using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDatabase : MonoBehaviour
{
    public static PrefabDatabase instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject SWORD_ATTACK;
}
