using System;
using System.Collections.Generic;

namespace JamECS.Base
{
    public interface IEcsComponent { }

    public interface IEcsComponents
    {
        Type Type { get; }
    }

    public class EcsComponents<TComponent> :
        List<TComponent>,
        IEcsComponents where TComponent : IEcsComponent
    {
        public Type Type => typeof(TComponent);

        public EcsComponents(IEnumerable<TComponent> components) =>
            AddRange(components);
    }
}