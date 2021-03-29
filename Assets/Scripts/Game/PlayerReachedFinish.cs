using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReachedFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player")) 
        {
            GameManager.Instance.SuccesFinishedLevel();
        }
    }
}
