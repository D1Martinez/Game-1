using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string otherScene;
    public bool inside = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inside)
        {
            SceneManager.LoadScene(otherScene);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inside = false;
        }
    }
}
