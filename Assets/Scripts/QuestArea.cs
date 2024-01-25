using UnityEngine;

namespace Assets.Scripts
{
    public class QuestArea : QuestPoint
    {
        private bool _checked = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!_checked)
            {
                OnReached?.Invoke();
                _checked = true;
            }
        }
    }
}
