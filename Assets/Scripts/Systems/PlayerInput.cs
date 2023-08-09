using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class PlayerInput : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _inputDirectionFilter;
        private EcsFilter _gunFilter;
        private EcsPool<InputDirection> _inputDirectionPool;
        private EcsPool<Gun> _gunPool;
        private float _direction;

        public void Run(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _inputDirectionFilter = _world.Filter<InputDirection>().End();
            _gunFilter = _world.Filter<Gun>().End();
            _inputDirectionPool = _world.GetPool<InputDirection>();
            _gunPool = _world.GetPool<Gun>();
            foreach (var entity in _inputDirectionFilter)
            {
                _direction = Input.GetAxis("Horizontal");
                ref var direction = ref _inputDirectionPool.Get(entity);
                direction.direction = _direction;
            }

            foreach (var entity in _gunFilter)
            {
                bool isShooting = Input.GetKeyDown(KeyCode.Space);
                ref var gun = ref _gunPool.Get(entity);
                gun.isShooting = isShooting;
            }
        }
    }
}