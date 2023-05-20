namespace Assets.Scripts.Game.Simulation.Enemy.Movement
{
    public abstract class MoveState
    {
        protected EnemyMovement _parent;
        protected MovementStateMachine _stateMachine;
        public MoveState(EnemyMovement parent, MovementStateMachine stateMachine)
        {
            _parent = parent;
            _stateMachine = stateMachine;
        }
        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();
    }
}
