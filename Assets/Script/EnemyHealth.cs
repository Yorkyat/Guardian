﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public GameObject gameStageManager;

    private Animator anim;
    private CapsuleCollider capsuleCollider;
    private EnemyMovement enemyMovement;
    private bool isDead;
    private bool isSinking;
    private HPBar hPBar;


    void Awake()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyMovement = GetComponent<EnemyMovement>();

        currentHealth = startingHealth;
    }

    void Start()
    {
        gameStageManager = GameObject.Find("Game Stage Manager");
        InitializeHealthBar();
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
        }
    }

    public void Damage(int amount)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= amount;
        hPBar.Set(startingHealth, currentHealth);

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        GetComponent<NavMeshAgent>().enabled = false;
        anim.SetTrigger("Dead");
        gameStageManager.GetComponent<GameStageManager>().DecreaseNoOfEnemy();
    }

    public void StartSinking()
    {
        enemyMovement.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        anim.enabled = false;
        isSinking = true;
        Destroy(gameObject, 2.0f);
    }

    void InitializeHealthBar()
    {
        hPBar = Instantiate(Resources.Load<HPBar>("Prefabs/UI/HP Bar"), FindObjectOfType<Canvas>().transform.Find("HP Display").transform) as HPBar;
        hPBar.target = gameObject;
        hPBar.Set(startingHealth, currentHealth);
    }
}
