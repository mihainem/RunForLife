using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]private Bullet bulletPrefab;
    [SerializeField]private Transform spawnPoint;
    [SerializeField] private int maxBullets = 10;
    private Rigidbody[] bullets;

    private void Awake()
    {
        bullets = new Rigidbody[maxBullets];
    }

    private void Start()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            bullets[i] = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation * bulletPrefab.transform.rotation).GetComponent<Rigidbody>();
            bullets[i].gameObject.SetActive(false);
        }
    }

    public void Shoot() 
    {
       // bulletsParent.transform.position = spawnPoint.position;
        for (int i = 0; i < maxBullets; i++) 
        {
            if (!bullets[i].gameObject.activeSelf) 
            {
                bullets[i].gameObject.SetActive(true);
                bullets[i].transform.position = spawnPoint.position;
                bullets[i].transform.rotation = spawnPoint.rotation * bulletPrefab.transform.rotation;
               // bullets[i].velocity = spawnPoint.up * bulletPrefab.speed;
                /*bullets[i].transform.position = spawnPoint.position;
                bullets[i].transform.rotation = spawnPoint.rotation * bulletPrefab.transform.rotation;*/
                return;
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < bullets.Length; i++) 
        {
            if (bullets[i].gameObject.activeSelf) 
            {
                bullets[i].transform.position += bullets[i].transform.forward * bulletPrefab.speed;
                if ((spawnPoint.position - bullets[i].transform.position).magnitude > 30f) 
                {
                    bullets[i].gameObject.SetActive(false);
                }
            }
        }
    }

}
