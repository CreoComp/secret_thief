using Assets.Scripts.Game.Simulation;
using Assets.Scripts.Game.Simulation.Player;
using Assets.Scripts.Game.UI.PlayerInventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Inventory
{
    public class PlayerInvenoryUI : MonoBehaviour
    {
        private List<InventorySlot> _inventorySlots = new();
        private Player _player;

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.GetComponent<InventorySlot>() == null)
                    continue;
                _inventorySlots.Add(child.GetComponent<InventorySlot>());
            }

            SimulationState.PlayerChanged += SetPlayer;
        }

        private void SetPlayer(Player player)
        {
            if (_player != null)
                _player.PlayerInventory.ItemListChanged -= UpdateInventory;
            _player = player;
            if (_player != null)
                _player.PlayerInventory.ItemListChanged += UpdateInventory;
            UpdateInventory();
        }

        private void UpdateInventory()
        {
            ClearInventory();
            if (_player == null)
                return;

            var inventory = _player.PlayerInventory;
            var items = inventory.Items;
            for (int i = 0; i < Math.Min(_inventorySlots.Count, items.Count); i++)
            {
                var slot = _inventorySlots[i];
                slot.SetItem(items[i].IconInUI);
            }
        }

        private void ClearInventory()
        {
            foreach (var slot in _inventorySlots)
            {
                slot.SetItem(null);
            }
        }

        private void OnDestroy()
        {
            SimulationState.PlayerChanged -= SetPlayer;
            if (_player != null)
                _player.PlayerInventory.ItemListChanged -= UpdateInventory;
        }
    }
}