using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class GunRotation : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<Gun> _gunPool;

        public void Run(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<Gun>().End();
            _gunPool = _world.GetPool<Gun>();
            foreach (var entity in _filter)
            {
                ref var gun = ref _gunPool.Get(entity);
                float rotateAmount = gun.rotation * gun.maxRotation;

                gun.gunTransform.rotation = Quaternion.Euler(0.0f, 0.0f, rotateAmount);
            }
        }
    }
}