using System.Collections.Generic;
using System.Linq;

namespace JamECS.Base
{
    public readonly struct EcsEntity
    {
        private readonly List<IEcsComponent> _components;

        public int ID { get; }

        public EcsEntity(int id, List<IEcsComponent> components)
        {
            ID = id;
            _components = components;
        }

        public void Add(IEcsComponent component) =>
            _components.Add(component);

        public TComponent Get<TComponent>() where TComponent : class, IEcsComponent =>
            _components.First(x => x is TComponent) as TComponent;

        public EcsComponents<TComponent> Components<TComponent>() where TComponent : IEcsComponent =>
            new EcsComponents<TComponent>(_components.OfType<TComponent>());

        public void Clear() => _components.Clear();
    }
}