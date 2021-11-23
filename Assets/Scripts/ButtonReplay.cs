using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonReplay : MonoBehaviour 
{

	void Update()
	{
        if (Input.GetButtonDown("Start"))
        {
            SceneManager.LoadScene("Game");
        }
	}
}
