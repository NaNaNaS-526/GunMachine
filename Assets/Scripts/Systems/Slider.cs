using Components;
using Leopotam.EcsLite;

namespace Systems
{
    public class Slider : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<GunSlider> _gunSliderPool;
        private EcsPool<Gun> _gunPool;

        public void Run(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<Gun>().Inc<GunSlider>().End();
            _gunSliderPool = _world.GetPool<GunSlider>();
            _gunPool = _world.GetPool<Gun>();
            foreach (var entity in _filter)
            {
                ref var gunSlider = ref _gunSliderPool.Get(entity);
                ref var gun = ref _gunPool.Get(entity);
                gun.rotation = gunSlider.slider.value;
            }
        }
    }
}