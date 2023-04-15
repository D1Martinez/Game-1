using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public void EnemyDies(float destroyTime)
    {
        //Archer Death
        Destroy(gameObject, destroyTime);
    }
}
