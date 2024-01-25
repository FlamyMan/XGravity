using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.code
{
    public class ObjectPointFollower : MonoBehaviour
    {
        [SerializeField] private QuestPoint[] _points;

        private int _currentTimelineIndex;

        private void Update()
        {
            if (_currentTimelineIndex >= _points.Length) return;

            QuestPoint currentPoint = _points[_currentTimelineIndex];
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, currentPoint.MoveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, currentPoint.transform.rotation, currentPoint.LookSpeed);
            if(transform.position == currentPoint.transform.position && transform.rotation == currentPoint.transform.rotation)
            {
                _currentTimelineIndex++;
                currentPoint.OnReached?.Invoke();
            }
        }
    } 
}
