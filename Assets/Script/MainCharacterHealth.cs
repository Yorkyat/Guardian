using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacterHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public GameStateManager gameStateManager;

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
        gameStateManager.GameOver();
    }

    void InitializeHealthBar()
    {
        hPBar = Instantiate(Resources.Load<HPBar>("Prefabs/UI/HP Bar"), FindObjectOfType<Canvas>().transform.Find("HP Display").transform) as HPBar;
        hPBar.target = gameObject;
        hPBar.Set(startingHealth, currentHealth);
    }
}
