using System;
using UnityEngine;

namespace Assets.Scripts.Game.Simulation.Enemy
{
    public class PlayerSpotter : MonoBehaviour
    {
        [SerializeField] private float maxDistanceToFind;
        [SerializeField] private float HEARING_RADIUS;
        [SerializeField] private float ENEMY_FOV;
        [SerializeField] private float TIME_TO_LOST_PLAYER;
        [SerializeField] private float COOLDOWN_AFTER_DISAPPEARANCE;

        [SerializeField] private Transform _player;

        private float _remainingTimeToLostPlayer = 0;
        private float _remainingTimeForCooldown = 0;

        private void Start()
        {
            SimulationState.PlayerChanged += SetPlayer;
        }

        private void SetPlayer(Player.Player player)
        {
            if (player == null)
                _player = null;
            else
                _player = player.transform;
        }

        private void Update()
        {
            if (IsSeeingPlayer() && _remainingTimeForCooldown == 0)
                _remainingTimeToLostPlayer = TIME_TO_LOST_PLAYER;

            ReduceSpottedRemainingTime();
            ReduceCooldownRemainingTime();
        }

        public bool IsPlayerSpotted() => _remainingTimeToLostPlayer != 0;

        public Vector3 GetPlayerPosition()
        {
            if (IsPlayerSpotted())
                return _player.position;
            throw new InvalidOperationException();
        }
        public bool IsSeeingPlayer() =>
            _player != null &&
              (IsSeeingByEyes() || IsFeelingPlayerAround());

        public bool IsSeeingByEyes() =>
            Vector3.Angle(_player.position - transform.position, transform.forward) < ENEMY_FOV
            && Vector3.Distance(transform.position, _player.position) < maxDistanceToFind
            && !IsPlayerBehindTheWall();

        public bool IsPlayerBehindTheWall()
        {
            bool isPlayerBehindTheWall = true;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _player.position - transform.position, out hit))
            {
                isPlayerBehindTheWall = hit.transform.GetComponent<CharacterController>() == null;
                Debug.DrawRay(transform.position, maxDistanceToFind * (transform.forward + transform.right * Mathf.Tan(ENEMY_FOV / 180 * Mathf.PI)).normalized);
                Debug.DrawRay(transform.position, maxDistanceToFind * (transform.forward + -transform.right * Mathf.Tan(ENEMY_FOV / 180 * Mathf.PI)).normalized);

            }
            return isPlayerBehindTheWall;
        }

        private bool IsFeelingPlayerAround() =>
            Vector3.Distance(transform.position, _player.position) < HEARING_RADIUS;

        private void ReduceSpottedRemainingTime()
        {
            if (_remainingTimeToLostPlayer != 0 && _remainingTimeToLostPlayer - Time.deltaTime <= 0)
            {
                _remainingTimeForCooldown = COOLDOWN_AFTER_DISAPPEARANCE;
                _remainingTimeToLostPlayer = 0;
            }
            else if (_remainingTimeToLostPlayer != 0)
            {
                _remainingTimeToLostPlayer -= Time.deltaTime;
            }
        }

        private void ReduceCooldownRemainingTime()
        {
            _remainingTimeForCooldown = Math.Max(0, _remainingTimeForCooldown - Time.deltaTime);
        }

        private void OnDestroy()
        {
            SimulationState.PlayerChanged -= SetPlayer;
        }
    }
}
