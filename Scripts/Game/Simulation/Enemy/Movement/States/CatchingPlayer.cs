using UnityEngine;

namespace Assets.Scripts.Game.Simulation.Enemy.Movement.States
{
    public class CatchingPlayer : MoveState
    {
        public CatchingPlayer(EnemyMovement parent, MovementStateMachine stateMachine) : base(parent, stateMachine)
        {
        }

        public override void EnterState()
        {

        }


        public override void UpdateState()
        {
            if (_parent.PlayerSpotter.IsPlayerSpotted())
            {
                GoToPlayer();
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.GoingToRoutePointState);
            }
        }

        private void GoToPlayer()
        {
            var playerPos = _parent.PlayerSpotter.GetPlayerPosition();
            _parent.NavAgent.SetDestination(_parent.PlayerSpotter.GetPlayerPosition());
            _parent.NavAgent.isStopped = IsPlayerCaught(playerPos);
            if (IsPlayerCaught(playerPos))
            {
                Vector3 pointNextToEnemy = _parent.transform.position + _parent.transform.forward;
                Vector3 smoothRotation = Vector3.Slerp(pointNextToEnemy, playerPos, Time.deltaTime * 1.5f);

                _parent.transform.LookAt(smoothRotation);
            }
        }

        private bool IsPlayerCaught(Vector3 playerPosition)
        {
            float distance = Vector3.Distance(playerPosition, _parent.transform.position);
            return distance < _parent.DistanceToPlayerToBeCaught && _parent.PlayerSpotter.IsPlayerSpotted();
        }
        public override void ExitState()
        {

        }
    }
}
