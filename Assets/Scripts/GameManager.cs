using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;

public class GameManager : MonoBehaviour
{
    private Entity enemyEntity;
    World world;
    EntityManager manager;


    private void Start()
    {
        world = World.DefaultGameObjectInjectionWorld;
        Debug.Log($"All Entities { world.GetExistingSystem<EnemySystem>().EntityManager.GetAllEntities().Length}");
        manager = world.EntityManager;
        EntityQuery entityQuery = manager.CreateEntityQuery(ComponentType.ReadOnly<EnemyData>());
        enemyEntity = entityQuery.GetSingletonEntity();
    }
}
