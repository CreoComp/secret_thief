using System;
using UnityEngine;

namespace Assets.Scripts.Game.Simulation.Enemy
{
    [RequireComponent(typeof(PlayerSpotter))]
    public class EnemyRager : MonoBehaviour
    {
        public static event Action<float> EnemyHadRaged;

        private PlayerSpotter _spotter;

        public void Awake()
        {
            _spotter = GetComponent<PlayerSpotter>();
        }

        private void Update()
        {
            if (_spotter.IsSeeingPlayer())
                EnemyHadRaged?.Invoke(Time.deltaTime);
        }
    }
}
