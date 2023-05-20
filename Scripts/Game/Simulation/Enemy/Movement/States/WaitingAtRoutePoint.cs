using UnityEngine;

namespace Assets.Scripts.Game.Simulation.Enemy.Movement.States
{
    public class WaitingAtRoutePoint : MoveState
    {
        private double _startedWaitingAt;
        private float _timeToWait;
        public WaitingAtRoutePoint(EnemyMovement parent, MovementStateMachine stateMachine) : base(parent, stateMachine)
        {
        }

        public override void EnterState()
        {
            _timeToWait = _parent.EnemyRoute[_parent.CurrentDestinationPoint].StayTimeSeconds;
            _startedWaitingAt = Time.timeSinceLevelLoadAsDouble;
        }
        public override void UpdateState()
        {
            if (_parent.PlayerSpotter.IsPlayerSpotted())
            {
                _stateMachine.ChangeState(_stateMachine.CatchingPlayerState);
                return;
            }

            if (_startedWaitingAt + _timeToWait < Time.timeSinceLevelLoadAsDouble)
            {
                _parent.GoToNextRoutePoint();
                _stateMachine.ChangeState(_stateMachine.GoingToRoutePointState);
                return;
            }

            _parent.NavAgent.isStopped = true;
        }

        public override void ExitState()
        {
            _parent.NavAgent.isStopped = false;
            _timeToWait = 0;
            _startedWaitingAt = 0;
        }

    }
}
