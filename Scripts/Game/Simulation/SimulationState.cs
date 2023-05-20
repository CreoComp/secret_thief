using Assets.Scripts.Game.Simulation.Player;
using System;

namespace Assets.Scripts.Game.Simulation
{
    public class SimulationState
    {
        public static event Action<Player.Player> PlayerChanged;
        public static event Action<SimulationStates> SimulationStateChanged;
        public SimulationStates CurrentSimulationState { get; private set; }
        public Player.Player CurrentPlayingPlayer { get; private set; }
        private static SimulationState _instance;
        public static SimulationState Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SimulationState();
                return _instance;
            }
        }

        public SimulationState()
        {
            CurrentSimulationState = SimulationStates.WaitingToStart;
        }

        private void OnPlayerMove(PlayerMovement movement)
        {
            if(CurrentSimulationState != SimulationStates.WaitingToStart)
            {
                CurrentPlayingPlayer.PlayerMovement.PlayerMoving -= OnPlayerMove;
                return;
            }
            if (CurrentPlayingPlayer == null)
                return;
            
            StartGame();
            CurrentPlayingPlayer.PlayerMovement.PlayerMoving -= OnPlayerMove;
        }

        private void OnPlayerDied(PlayerHealth health)
        {
            if (CurrentPlayingPlayer == null)
                return;
            if (CurrentPlayingPlayer.PlayerHealth == health)
            {
                EndGame();
            }
        }

        private void StartGame()
        {
            CurrentSimulationState = SimulationStates.Started;
            SimulationStateChanged?.Invoke(CurrentSimulationState);
        }

        private void EndGame()
        {
            CurrentSimulationState = SimulationStates.Ended;
            SimulationStateChanged?.Invoke(CurrentSimulationState);
        }

        public void AttachPlayer(Player.Player player)
        {
            CurrentPlayingPlayer = player;
            SubscribeAllEvents();
            PlayerChanged?.Invoke(CurrentPlayingPlayer);
        }

        public void DetachPlayer()
        {
            DescribeAllEvents();
            CurrentPlayingPlayer = null;
            PlayerChanged?.Invoke(CurrentPlayingPlayer);
        }

        private void SubscribeAllEvents()
        {
            if (CurrentPlayingPlayer != null)
            {
                CurrentPlayingPlayer.PlayerMovement.PlayerMoving += OnPlayerMove;
                CurrentPlayingPlayer.PlayerHealth.PlayerHasDied += OnPlayerDied;
            }
        }

        private void DescribeAllEvents()
        {
            if (CurrentPlayingPlayer != null)
            {
                CurrentPlayingPlayer.PlayerMovement.PlayerMoving -= OnPlayerMove;
                CurrentPlayingPlayer.PlayerHealth.PlayerHasDied -= OnPlayerDied;
            }
        }

        ~SimulationState()
        {
            DescribeAllEvents();
        }
    }

    public enum SimulationStates
    {
        WaitingToStart,
        Started,
        Ended
    }
}
