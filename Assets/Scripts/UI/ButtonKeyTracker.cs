using UnityEngine;
using UnityEngine.UI;

namespace Run_n_gun.Space
{
    public class ButtonKeyTracker : MonoBehaviour
    {
        public KeyCode trackingKey = KeyCode.Escape;
        private Button button;

        private void Start()
        {
            button = GetComponent<Button>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(trackingKey))
            {
                if (button != null && this.gameObject.activeSelf)
                {
                    button.onClick.Invoke();
                }
            }
        }
    }
}