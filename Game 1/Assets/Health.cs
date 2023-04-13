using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public void EnemyDies()
    {
        //Archer Death
        Destroy(gameObject, 0.5f);
    }
}
