using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFix : MonoBehaviour
{
    public Transform fallSpawn;

    void OnTriggerEnter2D(Collider2D col)
    {
        Player.Instance.transform.position = fallSpawn.position;
    }
}
