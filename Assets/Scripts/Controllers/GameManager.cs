using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using System;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] private UIController uiController;

    [SerializeField] private Player player;

    [SerializeField] private int enemiesCount;
    [SerializeField] private Transform enemiesParent;
    private NavMeshAgent[] enemies;
    private int killedEnemies = 0;

    private bool gameStarted = false;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ShowTapToPlay();
    }

    private void Update()
    {
        if (!gameStarted)
            return;

        if (Time.frameCount % 30 == 0)
            EnemiesRunToPlayer();
    }

    private void ShowTapToPlay()
    {
        uiController.tapToPlay.SetActive(true);
    }

    internal void StartTimerToPlay()
    {
        uiController.tapToPlay.StartTimer();
    }

    public void EnemiesRunToPlayer() 
    {
        foreach (NavMeshAgent agent in enemies) 
        {
            if (agent != null) 
            {
                agent.SetDestination(player.transform.position);
                if ((agent.transform.position - player.transform.position).magnitude <= 1f) 
                {
                    StopGame();
                    uiController.losePanel.ShowWindow();
                }
            }
        }
    }

    private void CreateEnemies() 
    {
        DestroyAllEnemies();
        enemies = new NavMeshAgent[enemiesCount];
        for (int i = 0; i < enemies.Length; i++) 
        {
            GameObject obj = Instantiate(ResourcesManager.Instance.Enemy, enemiesParent);
            obj.transform.localPosition = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));

            enemies[i] = obj.GetComponent<NavMeshAgent>();
        }
    }

    private void DestroyAllEnemies()
    {
        if (enemies == null)
            return;

        foreach (NavMeshAgent agent in enemies)
        {
            if (agent != null)
                Destroy(agent.gameObject);
        }
    }

    internal void IncreaseNoOfKilledEnemies()
    {
        killedEnemies++;
        if (killedEnemies >= enemiesCount)
        {
            SuccesFinishedLevel();
        }
    }

    internal void StartPlay()
    {
        player.transform.position = new Vector3(0, 0.5f, 5f);
        CreateEnemies();
        player.SetMovement(true);
        Time.timeScale = 1;
        killedEnemies = 0;
        gameStarted = true;
    }

    internal void RetryLevel()
    {
        ShowTapToPlay();
    }

    internal void PlayNextLevel()
    {
        throw new NotImplementedException();
    }

    internal void SuccesFinishedLevel()
    {
        StopGame();
        uiController.winPanel.ShowWindow();
    }

    private void StopGame()
    {
        gameStarted = false;
        player.SetMovement(false);
    }
}
