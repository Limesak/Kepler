using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStart : MonoBehaviour 
{
	public Main main;

    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            main.StartGame();
        }
        if (Input.GetButtonDown("Quit"))
        {
            Application.Quit();
        }
    }
}
