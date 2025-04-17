using ND26.ECS.Gameplay.Common.Components;
using Unity.Entities;
using UnityEngine;
namespace ND26.ECS.Gameplay.Common.Proxy
{
    public class VisualizationProxy : MonoBehaviour
    {
        private Animator _animator;
        private EntityManager _entityManager;

        private Vector3 _lastPosition;
        private Quaternion _lastRotation;
        private Vector3 _lastScale;
        private int _lastState = -1;
        public Entity entity;

        private void Awake()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            // _animator = GetComponent<Animator>();
        }

        private void LateUpdate()
        {
            VisualizationSyncData entityVisualizationData = _entityManager.GetComponentData<VisualizationSyncData>(entity: entity);

            Vector3 newPosition = entityVisualizationData.position;
            Quaternion newRotation = entityVisualizationData.rotation;
            Vector3 newScale = new Vector3(x: entityVisualizationData.scale, y: entityVisualizationData.scale, z: entityVisualizationData.scale);

            // Chỉ set nếu khác để tránh overhead
            if (newPosition != _lastPosition)
            {
                Debug.Log(message: "Set position: " + newPosition);
                transform.position = newPosition;
                _lastPosition = newPosition;
            }

            if (newRotation != _lastRotation)
            {
                transform.rotation = newRotation;
                _lastRotation = newRotation;
            }

            if (newScale != _lastScale)
            {
                transform.localScale = newScale;
                _lastScale = newScale;
            }

            // if (_lastState != newState)
            // {
            //     _animator.SetInteger(name: "state", value: newState);
            //     _lastState = newState;
            // }
        }
    }
}
