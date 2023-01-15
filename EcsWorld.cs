using System.Collections.Generic;

namespace JamECS.Base
{
    public class EcsWorld
    {
        private readonly List<EcsEntity> _entities = new List<EcsEntity>();
        private int _entityCounter = 0;

        public List<EcsEntity> Entities => _entities;

        public void Destroy()
        {
            foreach (var entity in _entities)
                entity.Clear();

            _entities.Clear();
        }

        public EcsEntity NewEntity(List<IEcsComponent> components)
        {
            _entityCounter++;

            var entity = new EcsEntity(_entityCounter, components);

            _entities.Add(entity);

            return entity;
        }

        public void RemoveEntity(int id)
        {
            var entity = _entities.Find(x => x.ID == id);

            _entities.Remove(entity);
        }

        public EcsFilter Filter() =>
            new EcsFilter(this);
    }
}