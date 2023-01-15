using System.Collections.Generic;
using System.Linq;

namespace JamECS.Base
{
    public interface IEcsSystem { }

    public interface IEcsInitSystem : IEcsSystem
    {
        void Init(EcsSystems systems);
    }

    public interface IEcsRunSystem : IEcsSystem
    {
        void Run(EcsSystems systems);
    }

    public interface IEcsDestroySystem : IEcsSystem
    {
        void Destroy(EcsSystems systems);
    }

    public class EcsSystems
    {
        private readonly List<IEcsSystem> _allSystems = new List<IEcsSystem>();
        private List<IEcsRunSystem> _runSystems;

        public EcsWorld World { get; }

        public EcsSystems(EcsWorld world) =>
            World = world;

        public EcsSystems Add(IEcsSystem system)
        {
            _allSystems.Add(system);
            return this;
        }

        public void Destroy()
        {
            foreach (var system in _allSystems.OfType<IEcsDestroySystem>())
                system.Destroy(this);

            _allSystems.Clear();
            _runSystems.Clear();
        }

        public void Init()
        {
            foreach (var system in _allSystems.OfType<IEcsInitSystem>())
                system.Init(this);

            _runSystems = _allSystems.OfType<IEcsRunSystem>().ToList();
        }

        public void Run() => _runSystems.ForEach(x => x.Run(this));
    }
}