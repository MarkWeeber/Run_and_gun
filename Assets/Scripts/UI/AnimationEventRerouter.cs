using UnityEngine;
using UnityEngine.Events;

namespace RunAndGun.Space
{
    public class AnimationEventRerouter : MonoBehaviour
    {
        public UnityEvent trigger;

        public void Trigger()
        {
            trigger?.Invoke();
        }
    }
}