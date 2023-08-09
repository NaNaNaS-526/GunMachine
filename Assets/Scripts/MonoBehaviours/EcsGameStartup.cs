using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using Systems;
using UnityEngine;

namespace MonoBehaviours
{
    public class EcsGameStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            AddSystems();
            _systems.ConvertScene();
            _systems.Init();
        }

        private void AddSystems()
        {
            _systems.Add(new PlayerInput());
            _systems.Add(new Slider());
            _systems.Add(new Movement());
            _systems.Add(new GunRotation());
            _systems.Add(new Shooting());
        }

        private void Update()
        {
            _systems.Run();
        }
    }
}