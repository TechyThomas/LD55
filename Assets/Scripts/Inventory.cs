using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Ability> abilities;
    public List<Ability> hotbarAbilities;

    int maxSlots = 3;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        abilities = new List<Ability>();
        hotbarAbilities = new List<Ability>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (hotbarAbilities.Count < 1 || hotbarAbilities[0] == null) return;
            Player.instance.SetAbility(hotbarAbilities[0]);
            UI_Hotbar.instance.SetActive(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (hotbarAbilities.Count < 2 || hotbarAbilities[1] == null) return;
            Player.instance.SetAbility(hotbarAbilities[1]);
            UI_Hotbar.instance.SetActive(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (hotbarAbilities.Count < 3 || hotbarAbilities[2] == null) return;
            Player.instance.SetAbility(hotbarAbilities[2]);
            UI_Hotbar.instance.SetActive(2);
        }
    }

    public void AddAbility(Ability ability)
    {
        abilities.Add(ability);

        if (hotbarAbilities.Count < maxSlots)
        {
            hotbarAbilities.Add(ability);
            UI_Hotbar.instance.SetSlot(hotbarAbilities.Count - 1, ability);
        }
    }
}
