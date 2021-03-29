using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class EnemySystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        float3 targetPosition = new float3(0,0,0); // GameManager.Instance.player.position;
        JobHandle jobHandle = Entities.
            WithName("EnemySystem").
            ForEach((ref Translation position, ref Rotation rotation, ref EnemyData enemyData) => {

                float3 heading = targetPosition - position.Value;
                heading.y = 0;
                quaternion targetDirection = quaternion.LookRotation(heading, math.up());
                rotation.Value = math.slerp(rotation.Value, targetDirection, 100);
                position.Value += deltaTime * enemyData.speed * math.forward(rotation.Value);
                //  Debug.Log($"position {position.Value} and add delta: {deltaTime} speed { speed } forwardVector { math.forward(rotation.Value)}");   


                if (math.distance(targetPosition, position.Value) < 1)
                {
                    
                    Debug.Log("Player is now touched");
                   // cubeData.currentIndex %= waypointPositions.Length;
                    // cubeData = new CubeData { currentIndex = cubeData.currentIndex };
                }
            }).
            Schedule(inputDeps);
        // ForEach((ref CubeData cubeData) => { }).

      //  waypointPositions.Dispose(jobHandle);

        return jobHandle;
    }
}
