using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory _instance;
    public static Inventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Inventory>();
            }

            return _instance;
        }
    }

    public List<Ability> abilities = new List<Ability>();
    public List<Ability> hotbarAbilities = new List<Ability>();

    int maxSlots = 3;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (hotbarAbilities.Count < 1 || hotbarAbilities[0] == null) return;
            Player.Instance.SetAbility(hotbarAbilities[0]);
            UI_Hotbar.Instance.SetActive(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (hotbarAbilities.Count < 2 || hotbarAbilities[1] == null) return;
            Player.Instance.SetAbility(hotbarAbilities[1]);
            UI_Hotbar.Instance.SetActive(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (hotbarAbilities.Count < 3 || hotbarAbilities[2] == null) return;
            Player.Instance.SetAbility(hotbarAbilities[2]);
            UI_Hotbar.Instance.SetActive(2);
        }
    }

    public void AddAbility(Ability ability)
    {
        abilities.Add(ability);

        if (hotbarAbilities.Count < maxSlots)
        {
            hotbarAbilities.Add(ability);
            UI_Hotbar.Instance.SetSlot(hotbarAbilities.Count - 1, ability);
        }
    }
}
