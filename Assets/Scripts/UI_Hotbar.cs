using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Hotbar : MonoBehaviour
{
    public static UI_Hotbar instance;

    public List<TextMeshProUGUI> slotText;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < slotText.Count; i++)
        {
            slotText[i].text = (i + 1) + ".";
        }
    }

    public void SetSlot(int slotIndex, Ability ability)
    {
        slotText[slotIndex].text = String.Format("{0}. {1}", slotIndex + 1, ability.name);
    }

    public void SetActive(int slotIndex)
    {
        for (int i = 0; i < slotText.Count; i++)
        {
            if (i == slotIndex)
            {
                slotText[i].color = Color.yellow;
            }
            else
            {
                slotText[i].color = Color.white;
            }
        }
    }
}
