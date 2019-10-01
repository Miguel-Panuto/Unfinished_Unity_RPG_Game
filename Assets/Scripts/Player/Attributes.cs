using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attributes : MonoBehaviour
{

    private float maxHealth = 100;
    private float maxMana = 100;
    [SerializeField]
    private float currentHealth = 1;
    [SerializeField]
    private float currentMana = 1;

    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Image manaBar;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    void Update() 
    {
        manaBar.fillAmount = currentMana / maxMana;
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentMana <= 100)
            currentMana += 0.2f;
        if (currentMana > 100)
            currentMana = 100;
    }
    public void TakeDamage(float damage, Vector2 direction)
    {
        currentHealth -= damage;
        GameObject.Find("PlayerFX").GetComponent<BattleControl>().Intensity = 20;
        GameObject.Find("PlayerFX").GetComponent<BattleControl>().setInDodgeToTrue(direction);
    }
    public void UseMana(float mana)
    {
        currentMana -= mana;
    }

    public float GetMana()
    {
        return currentMana;
    }
}
