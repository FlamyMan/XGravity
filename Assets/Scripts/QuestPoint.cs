using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public abstract class QuestPoint : MonoBehaviour
    {
        public UnityEvent OnReached;
    }
}