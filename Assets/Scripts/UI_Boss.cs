using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Boss : MonoBehaviour
{
    static UI_Boss _instance;
    public static UI_Boss Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UI_Boss>();
            }

            return _instance;
        }
    }

    public TextMeshProUGUI bossText;
    public Image bossHealthBar;

    EnemyBoss bossEnemy;

    void Start()
    {
        HideUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossEnemy == null)
        {
            return;
        }

        float bossHealth = bossEnemy.GetComponent<Health>().GetCurrentHealth();
        float bossMaxHealth = bossEnemy.GetComponent<Health>().health;

        bossHealthBar.fillAmount = Mathf.Lerp(bossHealthBar.fillAmount, bossHealth / bossMaxHealth, 2f * Time.deltaTime);
    }

    public void SetBoss(EnemyBoss boss)
    {
        bossEnemy = boss;

        if (boss == null)
        {
            return;
        }

        bossText.text = boss.GetName();
    }

    public void ShowUI()
    {
        bossText.gameObject.SetActive(true);
        bossHealthBar.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        bossText.gameObject.SetActive(false);
        bossHealthBar.gameObject.SetActive(false);
    }
}
