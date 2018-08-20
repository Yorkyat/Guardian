using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacterHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;

    private Animator anim;
    private MainCharacter mainCharacter;
    private bool isDead;
    private HPBar hPBar;

    void Awake()
    {
        anim = GetComponent<Animator>();
        mainCharacter = GetComponent<MainCharacter>();

        currentHealth = startingHealth;
    }

    void Start()
    {
        InitializeHealthBar();
    }

    public void Damage(int amount)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= amount;
        hPBar.Set(startingHealth, currentHealth);

        if(currentHealth <= 0)
        {
            Death();
        }
    }
    
    void Death()
    {
        // Prevent this function is called again
        isDead = true;

        anim.SetTrigger("Dead");

        mainCharacter.enabled = false;
    }

    void InitializeHealthBar()
    {
        hPBar = Instantiate(Resources.Load<HPBar>("Prefabs/HP Bar"), FindObjectOfType<Canvas>().transform) as HPBar;
        hPBar.target = gameObject;
        hPBar.Set(startingHealth, currentHealth);
    }
}
