using Assets.Scripts.Game.Simulation.Player;
using System;
using UnityEngine;

namespace Assets.Scripts.Game.Simulation.ItemToSteal
{
    [Serializable]
    public class Item : MonoBehaviour
    {
        [SerializeField] private Sprite _iconInUI;
        public Sprite IconInUI { get => _iconInUI; }

        [SerializeField] private float _timeToPickSeconds;
        public float TimeToPickSeconds { get => _timeToPickSeconds; }

        [SerializeField] private int _cost;
        public int Cost { get => _cost; }

        private Player.Player _player;

        private void Awake()
        {
            SimulationState.PlayerChanged += OnPlayerChanged;
        }

        private void CheckPickedItem(Item item)
        {

            if (item == this)
                Destroy(gameObject);
        }

        private void OnPlayerChanged(Player.Player player)
        {
            if(_player != null)
            {
                _player.PlayerItemTaker.ItemHadPickedUp -= CheckPickedItem;
            }
            _player = player;
            if(_player != null)
            {
                _player.PlayerItemTaker.ItemHadPickedUp += CheckPickedItem;
            }
        }

        private void OnDestroy()
        {
            if(_player != null)
            {
                _player.PlayerItemTaker.ItemHadPickedUp -= CheckPickedItem;
            }
        }

    }
}
