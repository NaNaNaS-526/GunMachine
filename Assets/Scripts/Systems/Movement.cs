using Components;
using Leopotam.EcsLite;

namespace Systems
{
    public class Movement : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<Components.Wheel> _wheelPool;
        private EcsPool<InputDirection> _directionPool;

        public void Run(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<Wheel>().Inc<InputDirection>().End();
            _wheelPool = _world.GetPool<Wheel>();
            _directionPool = _world.GetPool<InputDirection>();
            foreach (var entity in _filter)
            {
                ref var wheel = ref _wheelPool.Get(entity);
                ref var direction = ref _directionPool.Get(entity);
                var motor = wheel.wheel.motor;
                motor.motorSpeed = direction.direction * wheel.speed;
                wheel.wheel.motor = motor;
            }
        }
    }
}