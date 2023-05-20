using UnityEngine;

namespace Assets.Scripts.Game.Simulation.Enemy.Movement.States
{
    public class GoingToRoutePoint : MoveState
    {
        private RoutePoint _destinationPoint;
        public GoingToRoutePoint(EnemyMovement parent, MovementStateMachine stateMachine) : base(parent, stateMachine)
        {
        }

        public override void EnterState()
        {
            _destinationPoint = _parent.EnemyRoute[_parent.CurrentDestinationPoint];
        }

        public override void UpdateState()
        {
            if (_parent.PlayerSpotter.IsPlayerSpotted())
            {
                _stateMachine.ChangeState(_stateMachine.CatchingPlayerState);
                return;
            }

            if (Vector3.Distance(_destinationPoint.position, _parent.transform.position) < _parent.RoutePointsPrecision)
            {
                _stateMachine.ChangeState(_stateMachine.WaitingAtRoutePointState);
                return;
            }

            _parent.NavAgent.SetDestination(_destinationPoint.position);
        }

        public override void ExitState()
        {

        }

    }
}
