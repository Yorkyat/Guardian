using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody mRigidBody;
    public float speed;

    // Use this for initialization
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mRigidBody.velocity = transform.forward * speed;
        Destroy(gameObject, 1.0f);
    }
}
