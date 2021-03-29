using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;

public class BulletSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        JobHandle jobHandle = Entities
            .WithName("BulletSystem")
            .ForEach((ref Translation position, ref Rotation rotation) => 
            {
               // position.Value += deltaTime * bullet.speed * math.forward(rotation.Value);
            })
            .Schedule(inputDeps);
        return jobHandle;
    }
}
