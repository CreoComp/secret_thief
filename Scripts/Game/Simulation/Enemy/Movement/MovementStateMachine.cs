using Assets.Scripts.Game.Simulation.Enemy.Movement.States;

namespace Assets.Scripts.Game.Simulation.Enemy.Movement
{
    public class MovementStateMachine
    {
        private MoveState _currentState;

        public CatchingPlayer CatchingPlayerState { get; private set; }
        public GoingToRoutePoint GoingToRoutePointState { get; private set; }
        public WaitingAtRoutePoint WaitingAtRoutePointState { get; private set; }

        public MovementStateMachine(EnemyMovement owner)
        {
            CatchingPlayerState = new CatchingPlayer(owner, this);
            GoingToRoutePointState = new GoingToRoutePoint(owner, this);
            WaitingAtRoutePointState = new WaitingAtRoutePoint(owner, this);

            _currentState = CatchingPlayerState;
            _currentState.EnterState();
        }

        public void Update()
        {
            _currentState.UpdateState();
        }

        public void ChangeState(MoveState state)
        {
            _currentState.ExitState();
            _currentState = state;
            _currentState.EnterState();
        }

    }
}
