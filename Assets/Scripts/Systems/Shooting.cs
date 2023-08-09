using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class Shooting : IEcsRunSystem
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
                if (gun.isShooting)
                {
                    var newBullet = GameObject.Instantiate(gun.bullet, gun.bulletSpawnPoint.position,
                        gun.bulletSpawnPoint.rotation);
                    newBullet.AddForce(gun.bulletSpawnPoint.right * gun.bulletForce, ForceMode2D.Impulse);
                    //GameObject.Destroy(newBullet.gameObject, 2.0f);
                }
            }
        }
    }
}