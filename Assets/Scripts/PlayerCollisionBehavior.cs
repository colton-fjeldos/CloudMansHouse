//PlayerCollisionBehavior.cs
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject DeathMenu;
    [SerializeField] private GameObject DialogMenu;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Kill"))
        {
            Death();
        }

        
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("DialogTag"))
        {
            OpenDialog();
            trigger.gameObject.SetActive(false);
        }

        if (trigger.gameObject.CompareTag("Finish"))
        {
            UnityEngine.Debug.Log("Next Level");
            NextLevel();
        }
    }

    private void Death()
    {
        UnityEngine.Debug.Log("You have died");
        rb.bodyType = RigidbodyType2D.Static;
        DeathMenu.SetActive(true);
        
    }

    private void OpenDialog()
    {
        UnityEngine.Debug.Log("Opening Dialog");
        DialogMenu.SetActive(true);
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
