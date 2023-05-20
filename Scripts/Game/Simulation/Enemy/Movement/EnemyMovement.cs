using Assets.Scripts.UI.Menu;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Game.Simulation.Enemy.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PlayerSpotter))]
    public class EnemyMovement : MonoBehaviour
    {
        public float DistanceToPlayerToBeCaught;
        public PlayerSpotter PlayerSpotter { get; private set; }
        public NavMeshAgent NavAgent { get; private set; }
        public List<RoutePoint> EnemyRoute { get => _enemyRoute.ToList(); private set => _enemyRoute = value; }
        public int CurrentDestinationPoint { get; private set; } = 0;
        public float RoutePointsPrecision;

        [SerializeField] private Transform _routeRoot;

        private List<RoutePoint> _enemyRoute = new();
        private MovementStateMachine _stateMachine;

        private void Start()
        {
            PlayerSpotter = GetComponent<PlayerSpotter>();
            NavAgent = GetComponent<NavMeshAgent>();

            for (int i = 0; i < _routeRoot.childCount; i++)
            {
                _enemyRoute.Add(_routeRoot.GetChild(i).GetComponent<RoutePoint>());
            }
            _stateMachine = new MovementStateMachine(this);
        }

        private void Update()
        {
            if (SimulationState.Instance.CurrentSimulationState == SimulationStates.WaitingToStart)
                return;

            _stateMachine.Update();
        }

        public void GoToNextRoutePoint()
        {
            CurrentDestinationPoint = (CurrentDestinationPoint + 1) % _enemyRoute.Count;
        }
    }
}