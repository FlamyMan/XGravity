using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class GravitationChanger : MonoBehaviour
    {
        [SerializeField] private Transform _rotate;
        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private float _angle = 90;

        public void ChangeGravitation()
        {
            if (_rotate == null || _player == null || _rotation == null) throw new NullReferenceException();

            _rotate.Rotate(_rotation, _angle);
            _player.rotation = Quaternion.Euler(0, _player.rotation.eulerAngles.y, 0) ;
        }
    }
}
