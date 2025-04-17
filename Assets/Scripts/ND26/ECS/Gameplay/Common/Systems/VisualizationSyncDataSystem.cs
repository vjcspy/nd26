using ND26.ECS.Gameplay.Common.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
namespace ND26.ECS.Gameplay.Common.Systems
{
    [UpdateInGroup(groupType: typeof(SimulationSystemGroup), OrderFirst = true)]
    public partial struct VisualizationSyncDataSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (localTransform, visualizationSyncData) in
                SystemAPI.Query<RefRO<LocalTransform>, RefRW<VisualizationSyncData>>()
                    // .WithChangeFilter<LocalTransform>()
                )
            {
                visualizationSyncData.ValueRW.position = new float3(
                    x: localTransform.ValueRO.Position.x,
                    y: localTransform.ValueRO.Position.y,
                    z: localTransform.ValueRO.Position.z
                );

                visualizationSyncData.ValueRW.rotation = localTransform.ValueRO.Rotation;
                visualizationSyncData.ValueRW.scale = localTransform.ValueRO.Scale;
            }
        }
    }
}
