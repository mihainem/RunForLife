using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour {
    private static ResourcesManager instance;
    public static ResourcesManager Instance { get {
            if (Application.isPlaying)
                return instance;
            else
                return GameObject.FindObjectOfType<ResourcesManager>(); } }

    private void Awake()
    {
        instance = this;
    }


    private GameObject _enemy;
    public GameObject Enemy 
    {
        get { 
            if (_enemy == null)
                _enemy = Resources.Load<GameObject>("Enemy");
            return _enemy;
        }
    }

    public GameObject[] LevelParts
    {
        get
        {
            return Resources.LoadAll<GameObject>("LevelParts");
        }
    }
}
