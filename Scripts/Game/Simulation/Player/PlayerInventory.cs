using Assets.Scripts.Game.Simulation.ItemToSteal;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game.Simulation.Player
{
    [RequireComponent(typeof(PlayerItemTaker))]
    public class PlayerInventory : MonoBehaviour
    {
        public event Action ItemListChanged;

        private List<Item> _items = new();
        public List<Item> Items { get => _items.ToList(); }
        private PlayerItemTaker _taker;

        private void Start()
        {
            _taker = GetComponent<PlayerItemTaker>();
            _taker.ItemHadPickedUp += AddItem;
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
            ItemListChanged?.Invoke();
        }

        private void ClearInventory()
        {
            Items.Clear();
            ItemListChanged();
        }

        private void OnDestroy()
        {
            _taker.ItemHadPickedUp -= AddItem;
        }

    }
}
