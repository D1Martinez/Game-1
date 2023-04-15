using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curse : MonoBehaviour
{

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        Debug.Log(transform.position);
    }
}
