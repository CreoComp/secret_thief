using Assets.Scripts.Game.Simulation;
using Assets.Scripts.Game.Simulation.ItemToSteal;
using Assets.Scripts.Game.Simulation.Player;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game.UI.Inventory
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class InventoryCost : MonoBehaviour
    {
        private const int DEFAULT_INVENTORY_COST = 0;
        private TextMeshProUGUI _text;
        private Player _player;
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _player = SimulationState.Instance.CurrentPlayingPlayer;
            SimulationState.PlayerChanged += OnPlayerChanged;
        }

        private void OnPlayerChanged(Player player)
        {
            if (_player != null)
            {
                _player.PlayerInventory.ItemListChanged -= OnInventoryChanged;
            }
            _player = player;
            if (_player != null)
            {
                _player.PlayerInventory.ItemListChanged += OnInventoryChanged;
            }
            OnInventoryChanged();
        }

        private void OnInventoryChanged()
        {
            if (_player == null)
            {
                _text.text = DEFAULT_INVENTORY_COST.ToString();
                return;
            }

            int inventoryCost = 0;
            foreach (Item item in _player.PlayerInventory.Items)
            {
                inventoryCost += item.Cost;
            }
            _text.text = inventoryCost.ToString();
        }

        private void OnDestroy()
        {
            SimulationState.PlayerChanged -= OnPlayerChanged;
        }
    }
}
