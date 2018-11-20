using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacterHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public GameStageManager gameStageManager;
    public ScreenFlashing damageFlashing;
    public RandomCameraShake randomCameraShake;

    private Animator anim;
    private MainCharacter mainCharacter;
    private bool isDead;
    private HPBar hPBar;

    void Awake()
    {
        anim = GetComponent<Animator>();
        mainCharacter = GetComponent<MainCharacter>();
    }

    void Start()
    {
        startingHealth += GameManager.manager.playerData.currentHPBuff;

        currentHealth = startingHealth;

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

        if (currentHealth <= (startingHealth / 2) && damageFlashing.incrementCounter < 1)
        {
            damageFlashing.Increment();
            randomCameraShake.magnitude += 0.2f;
        }
        if (currentHealth <= (startingHealth / 4) && damageFlashing.incrementCounter < 2)
        {
            damageFlashing.Increment();
            randomCameraShake.magnitude += 0.2f;
        }
        damageFlashing.flashingCondition = true;

        randomCameraShake.ShakeXY();

        if (currentHealth <= 0)
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
        gameStageManager.GameOver();
    }

    void InitializeHealthBar()
    {
        hPBar = Instantiate(Resources.Load<HPBar>("Prefabs/UI/HP Bar"), FindObjectOfType<Canvas>().transform.Find("HP Display").transform) as HPBar;
        hPBar.target = gameObject;
        hPBar.Set(startingHealth, currentHealth);
    }
}
