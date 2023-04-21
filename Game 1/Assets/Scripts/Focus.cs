using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : MonoBehaviour
{
    Transform cam;
    bool inside = false;
    float t = 0f;
    private void Start()
    {
        cam = Camera.main.transform;
    }
    void Update()
    {
        if (inside)
        {
            MoveCamera();
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
            t = 0f;
        }
    }
    void MoveCamera()
    {
        Vector3 camPos = cam.position;
        if (t < 1)
        {
            t += Time.deltaTime/10;
            t = Mathf.Clamp(t, 0, 1);
            cam.position = Vector3.Lerp(camPos, transform.position, Mathf.SmoothStep(0, 1, t));
        }
    }
}
