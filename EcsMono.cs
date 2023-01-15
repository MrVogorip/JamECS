using UnityEngine;

namespace JamECS.Base
{
    public abstract class EcsMono<TComponent> : MonoBehaviour where TComponent : IEcsComponent
    {
        public TComponent Value;
    }
}