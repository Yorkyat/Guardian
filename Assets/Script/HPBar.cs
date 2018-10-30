using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image hP;
    public GameObject target;

    private UIFollowTarget utf;

    void Awake()
    {
        // Set the gameObject invisible before assigning the position
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        utf = GetComponent<UIFollowTarget>();
    }

    void Start()
    {
        utf.target = target.transform;
        // Set the gameObject visible
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public void Set(int startingHealth, int currentHealth)
    {
        if(currentHealth <= 0)
        {
            hP.fillAmount = 0;
            Destroy(gameObject);
        }
        else
        {
            hP.fillAmount = (float)currentHealth / (float)startingHealth;
        }
    }

}
