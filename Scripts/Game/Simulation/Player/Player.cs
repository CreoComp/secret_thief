using UnityEngine;

namespace Assets.Scripts.Game.Simulation.Player
{
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerInventory))]
    [RequireComponent(typeof(PlayerItemTaker))]
    [RequireComponent(typeof(PlayerMovement))]
    public class Player : MonoBehaviour
    {
        public PlayerHealth PlayerHealth { get; private set; }
        public PlayerInventory PlayerInventory { get; private set; }
        public PlayerItemTaker PlayerItemTaker { get; private set; }
        public PlayerMovement PlayerMovement { get; private set; }

        private void Awake()
        {
            PlayerHealth = GetComponent<PlayerHealth>();
            PlayerInventory = GetComponent<PlayerInventory>();
            PlayerItemTaker = GetComponent<PlayerItemTaker>();
            PlayerMovement = GetComponent<PlayerMovement>();

            SimulationState.Instance.AttachPlayer(this);
        }

        private void OnDestroy()
        {
            SimulationState.Instance.DetachPlayer();
        }
    }
}