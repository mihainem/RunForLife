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

    public Player player;
    [SerializeField] private EnemiesController enemiesController;

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

    private void ShowTapToPlay()
    {
        uiController.tapToPlay.SetActive(true);
    }

    internal void StartTimerToPlay()
    {
        uiController.tapToPlay.StartTimer();
    }

    internal void StartPlay()
    {
        player.StartPlay();
        enemiesController.StartPlay();
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

    internal void FailFinishedLevel()
    {
        StopGame();
        uiController.losePanel.ShowWindow();
    }

    private void StopGame()
    {
        enemiesController.SetMovement(false);
        player.SetMovement(false);
    }
}
