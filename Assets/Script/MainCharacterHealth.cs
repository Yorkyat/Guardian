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

    void Start()
    {
        anim = GetComponent<Animator>();
        mainCharacter = GetComponent<MainCharacter>();

        currentHealth = startingHealth;
    }

    public void Damage(int amount)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= amount;

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
}
