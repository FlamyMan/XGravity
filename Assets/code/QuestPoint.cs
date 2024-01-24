using UnityEngine;
using UnityEngine.Events;

namespace Assets.code
{
    public class QuestPoint : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _LookSpeed;

        public float MoveSpeed => _speed;
        public float LookSpeed => _LookSpeed;

        public UnityEvent OnReached;
    }
}
