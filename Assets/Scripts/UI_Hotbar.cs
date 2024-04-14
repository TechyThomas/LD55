using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Hotbar : MonoBehaviour
{
    public static UI_Hotbar _instance;
    public static UI_Hotbar Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UI_Hotbar>();
            }

            return _instance;
        }
    }

    public List<TextMeshProUGUI> slotText;

    public void SetSlot(int slotIndex, Ability ability)
    {
        if (ability == null)
        {
            slotText[slotIndex].text = slotIndex + 1 + ".";
            return;
        }

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
