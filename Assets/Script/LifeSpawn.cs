using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpawn : MonoBehaviour {

    public Object item;
    public float timer;

	void Start () {
        if(item == null)
        {
            item = gameObject;
        }

        Destroy(item, timer);
	}
}
