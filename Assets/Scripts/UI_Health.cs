using System;
using TMPro;
using UnityEngine;

public class UI_Health : MonoBehaviour
{
    TextMeshProUGUI textUi;

    void Start()
    {
        textUi = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textUi == null)
        {
            return;
        }

        float playerHealth = Player.Instance.GetComponent<Health>().GetCurrentHealth();
        float healthPercent = playerHealth / 100f * 100;

        textUi.text = String.Format("Health: {0}%", healthPercent);
    }
}
