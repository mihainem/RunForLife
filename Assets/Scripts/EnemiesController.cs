using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private int enemiesCount;
    [SerializeField] private float maxDistanceToPlayer = 1.3f;
    private NavMeshAgent[] enemies;
    private int killedEnemies = 0;
    private bool startMovement = false;


    private Transform player;
    private void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    private void Update()
    {
        if (!startMovement)
            return;

        if (Time.frameCount % 30 == 0)
            EnemiesRunToPlayer();
    }

    public void SetMovement(bool active) 
    {
        startMovement = active;
    }


    public void EnemiesRunToPlayer()
    {
        foreach (NavMeshAgent agent in enemies)
        {
            if (agent != null)
            {
                agent.SetDestination(player.position);
                if ((agent.transform.position - player.position).magnitude <= maxDistanceToPlayer)
                {
                    GameManager.Instance.FailFinishedLevel();
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
            GameObject obj = Instantiate(ResourcesManager.Instance.Enemy, transform);
            obj.transform.localPosition = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));

            enemies[i] = obj.GetComponent<NavMeshAgent>();
        }
    }

    internal void StartPlay()
    {
        CreateEnemies();
        killedEnemies = 0;
        startMovement = true;
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
            GameManager.Instance.SuccesFinishedLevel();
        }
    }

    private void OnEnable()
    {
        Bullet.OnBulletHitEnemy += IncreaseNoOfKilledEnemies;
    }

    private void OnDisable()
    {
        Bullet.OnBulletHitEnemy -= IncreaseNoOfKilledEnemies;
    }

}
