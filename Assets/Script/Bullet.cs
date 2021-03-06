﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int attackDamge = 10;

    private Rigidbody mRigidBody;
    private GameObject player;
    private MainCharacterHealth mainCharacterHealth;

    // Use this for initialization
    void Awake()
    {
        mRigidBody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCharacterHealth = player.GetComponent<MainCharacterHealth>();
    }

    void Start()
    {
        attackDamge += GameManager.manager.playerData.currentAttackBuff;
    }

    // Update is called once per frame
    void Update()
    {
        mRigidBody.velocity = transform.forward * speed;
        Destroy(gameObject, 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            if(collision.collider.GetComponent<EnemyHealth>().currentHealth > 0 && mainCharacterHealth.currentHealth > 0)
            {
                collision.collider.GetComponent<EnemyHealth>().Damage(attackDamge);
            }
        }
        Destroy(gameObject);
    }
}
