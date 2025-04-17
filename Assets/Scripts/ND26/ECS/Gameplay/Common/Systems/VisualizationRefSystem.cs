using ND26.ECS.Gameplay.Common.Components;
using ND26.ECS.Gameplay.Common.Proxy;
using Unity.Entities;
using UnityEngine;
using EndSimulationEntityCommandBufferSystem = Unity.Entities.EndSimulationEntityCommandBufferSystem;
using Entity = Unity.Entities.Entity;
using EntityCommandBuffer = Unity.Entities.EntityCommandBuffer;
using EntityQuery = Unity.Entities.EntityQuery;
using ISystem = Unity.Entities.ISystem;
using SimulationSystemGroup = Unity.Entities.SimulationSystemGroup;
using SystemAPI = Unity.Entities.SystemAPI;
using SystemState = Unity.Entities.SystemState;
namespace ND26.ECS.Gameplay.Common.Systems
{
    [UpdateInGroup(groupType: typeof(SimulationSystemGroup))]
    public partial struct VisualizationRefSystem : ISystem
    {
        private EntityQuery _entityQuery;

        public void OnCreate(ref SystemState state)
        {
            _entityQuery = SystemAPI.QueryBuilder()
                .WithAll<VisualizationRefData>()
                .WithNone<VisualizationSyncData>()
                .Build();

            state.RequireForUpdate(query: _entityQuery);
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = CreateECB(state: ref state);
            Entity playerEntity = _entityQuery.GetSingletonEntity();
            VisualizationRefData playerVisualizationRef = _entityQuery.GetSingleton<VisualizationRefData>();
            GameObject playerVisualizationObject = Object.Instantiate(original: playerVisualizationRef.gameObject);
            VisualizationProxy proxy = playerVisualizationObject.GetComponent<VisualizationProxy>();
            if (proxy != null)
            {
                proxy.entity = playerEntity;
                ecb.AddComponent(e: playerEntity, component: new VisualizationSyncData());
                Debug.Log(message: "VisualizationRefSystem: Added VisualizationSyncData to player entity.");
            }
            else
            {
                Debug.LogError(message: "AnimationProxy component not found on the player visualization object.");
            }
        }

        private EntityCommandBuffer CreateECB(ref SystemState state)
        {
            EndSimulationEntityCommandBufferSystem.Singleton ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            return ecbSingleton.CreateCommandBuffer(world: state.WorldUnmanaged);
        }
    }
}
