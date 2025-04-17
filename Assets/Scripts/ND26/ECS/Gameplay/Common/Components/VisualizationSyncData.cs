using Unity.Entities;
using Unity.Mathematics;
namespace ND26.ECS.Gameplay.Common.Components
{
    public struct VisualizationSyncData : IComponentData
    {
        public float3 position;
        public quaternion rotation;
        public float scale;
    }
}
