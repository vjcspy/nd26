using ND26.ECS.Gameplay.Common.Components;
using Unity.Entities;
using UnityEngine;
namespace ND26.ECS.Gameplay.Characters.DummyPlayer
{

    [DisallowMultipleComponent]
    public class DummyPlayerAuthoring : MonoBehaviour
    {
        [Header(header: "Warrior Player Visual Prefab")]
        [SerializeField] public GameObject visualization;
    }

    public class DummyPlayerAuthoringBaker : Baker<DummyPlayerAuthoring>
    {

        public override void Bake(DummyPlayerAuthoring authoring)
        {
            Entity entity = GetEntity(flags: TransformUsageFlags.Dynamic);
            AddComponentObject(entity: entity, component: new VisualizationRefData
            {
                gameObject = authoring.visualization
            });
        }
    }
}
