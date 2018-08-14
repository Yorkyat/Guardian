using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    public float attackOffset = 1.0f;
    public int attackDamge = 10;

    private Animator anim;
    private GameObject player;
    private MainCharacterHealth mainCharacterHealth;
    private bool playerInRange;
    private float attackTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCharacterHealth = player.GetComponent<MainCharacterHealth>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if(attackTimer >= attackOffset && playerInRange)
        {
            Attack();
        }

        if(mainCharacterHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = false;
        }
    }

    void Attack()
    {
        // reset timer
        attackTimer = 0f;

        anim.Play("slime_attack");

        if(mainCharacterHealth.currentHealth > 0)
        {
            mainCharacterHealth.Damage(attackDamge);
        }
    }
}
