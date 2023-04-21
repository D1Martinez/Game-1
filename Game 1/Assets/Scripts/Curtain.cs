using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curtain : MonoBehaviour
{
    public Image blackScreen;
    float t = 1f;
    void Start()
    {
        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (t < 1f)
        {
            t += Time.deltaTime / 5f;
            t = Mathf.Clamp(t, 0, 1);
            blackScreen.color = new Color(0, 0, 0, t * 255f);
        }
    }
}
