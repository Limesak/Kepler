using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonStart : MonoBehaviour 
{
	public Main main;

    void StartGame()
    {
        main.StartGame();
    }
}
