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
        utf = GetComponent<UIFollowTarget>();
    }

    void Start()
    {
        utf.target = target.transform;
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
