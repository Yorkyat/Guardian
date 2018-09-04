﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject joystick;
    public GameObject fireButton;
    public GameObject pausePanel;

    private bool paused;
    private FixedJoystick joystickScript;
    private Button fireButtonScript;

    void Awake()
    {
        joystickScript = joystick.GetComponent<FixedJoystick>();
        fireButtonScript = fireButton.GetComponent<Button>();
    }

    void Start()
    {
        paused = false;
    }

    public void PauseGame(bool state)
    {
        paused = state;

        if (paused)
        {
            Time.timeScale = 0;
            joystickScript.enabled = false;
            fireButtonScript.enabled = false;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            joystickScript.enabled = true;
            fireButtonScript.enabled = true;
            pausePanel.SetActive(false);
        }
    }
}
