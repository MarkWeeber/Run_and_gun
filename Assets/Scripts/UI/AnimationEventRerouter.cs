using UnityEngine;
using UnityEngine.Events;

namespace Run_n_gun.Space
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