using Assets.Scripts.Game.Simulation.Enemy;
using System;
using UnityEngine;

namespace Assets.Scripts.Game.Simulation.RageSystem
{
    public class RageScale : MonoBehaviour
    {
        public static event Action RageScaleFull;
        public const float MAXIMUM_RAGE = 1f;
        public static float CurrentRage { get; private set; } = 0;
        private const float RAGE_MULTIPLIER = 0.25f;
        private const float RAGE_REDUCING_PER_SECOND = 0.15f;

        private void Start()
        {
            EnemyRager.EnemyHadRaged += OnEnemyRaged;
            ResetRageScale();
        }

        public void ResetRageScale()
        {
            CurrentRage = 0;
        }

        private void Update()
        {
            CurrentRage = Mathf.Max(0, CurrentRage - RAGE_REDUCING_PER_SECOND * Time.deltaTime);
        }

        private void OnEnemyRaged(float rageTime)
        {
            float nextRageValue = CurrentRage + rageTime * RAGE_MULTIPLIER;
            if (CurrentRage < MAXIMUM_RAGE && nextRageValue >= MAXIMUM_RAGE)
            {
                RageScaleFull?.Invoke();
                Debug.Log("Rage invoked");
                CurrentRage = 0;
                return;
            }
            CurrentRage = nextRageValue;
        }

        private void OnDestroy()
        {
            EnemyRager.EnemyHadRaged -= OnEnemyRaged;
        }
    }
}
