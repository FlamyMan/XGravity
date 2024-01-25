using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class FollowQuestPoint : QuestPoint
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _LookSpeed;

        public float MoveSpeed => _speed;
        public float LookSpeed => _LookSpeed;

        
    }
}
