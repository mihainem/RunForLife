using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

public class Bullet : MonoBehaviour
{
    public static event Action OnBulletHitEnemy;
    public float speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            OnBulletHitEnemy?.Invoke();
            gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
