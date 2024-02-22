//DeathMenuScript.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuScript : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnMenu()
    {
        Debug.Log("Returning to main menu from scene " + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }
}
