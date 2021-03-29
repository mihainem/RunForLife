using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private int noOfLevelParts;
    private GameObject level;
    public bool generateLevel;

    private void OnValidate()
    {
        if (!generateLevel)
            return;

        generateLevel = false;

        if (level != null) 
        {
            DestroyImmediate(level);
        }
        GameObject[] levelParts = ResourcesManager.Instance.LevelParts;
        level = new GameObject();
        level.name = "Level";
        level.transform.position = new Vector3(0f, -0.5f, 0f);
        Vector3 startPosition = Vector3.zero;
            GameObject part;
        for (int i = 0; i < noOfLevelParts; i++)
        {
            if (i <= 1)
            {
                //create starting platforms
                part = Instantiate(levelParts[0], level.transform);
            }
            else if (i == noOfLevelParts - 1) 
            {
                //create finish platform
               // startPosition = part.transform.position + Vector3.forward * part.transform.localScale.z + Vector3.down * 2;
                part = Instantiate(levelParts[1], level.transform);
            }
            else
            {
                //create obstacles platform
              part = Instantiate(levelParts[Random.Range(2, levelParts.Length)], level.transform);
            }

            part.transform.position = startPosition;
            startPosition = part.transform.position + Vector3.forward * part.transform.localScale.z;
        }

    
    }
}
