using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


public class Bullet : MonoBehaviour
{
    public float speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            GameManager.Instance.IncreaseNoOfKilledEnemies();
            gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
