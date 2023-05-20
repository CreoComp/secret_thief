using System.Collections;
using Assets.Scripts.Game.Simulation;
using Assets.Scripts.Game.Simulation.ItemToSteal;
using Assets.Scripts.Game.Simulation.Player;
using UnityEngine;

namespace Game.UI.ItemTaking
{
    public class ItemPicking : MonoBehaviour
    {
        [SerializeField] private ItemPickingPointer _itemPickingPointer;
        [SerializeField] private float _fillAmountChangesEverySeconds;

        private double _pickUpStartedTime;
        private Item _currentPickingItem;
        private Player _player;

        private void Awake()
        {
            SimulationState.PlayerChanged += OnPlayerChanged;
        }

        private void OnPlayerChanged(Player player)
        {
            if (_player != null)
            {
                _player.PlayerItemTaker.ItemPickingStarted -= OnItemPickingStarted;
                _player.PlayerItemTaker.PickUpEnd -= OnPickUpEnd;
            }
            _player = player;
            if(_player != null)
            {
                _player.PlayerItemTaker.ItemPickingStarted += OnItemPickingStarted;
                _player.PlayerItemTaker.PickUpEnd += OnPickUpEnd;
            }
        }
        private void OnItemPickingStarted(Item item)
        {
            _currentPickingItem = item;
            _pickUpStartedTime= Time.timeSinceLevelLoadAsDouble;
            _itemPickingPointer.EnablePointer();
            _itemPickingPointer.FollowByObject(item.transform);
        }

        private void Update()
        {
            if (_currentPickingItem == null)
                return;

            double timeLast = Time.timeSinceLevelLoadAsDouble - _pickUpStartedTime;
            _itemPickingPointer.SetFillAmount((float)timeLast / _currentPickingItem.TimeToPickSeconds);
        }

        private void OnPickUpEnd()
        {
            _itemPickingPointer.DisablePointer();
            _currentPickingItem = null;
            _pickUpStartedTime = 0;
        }

        private void OnDestroy()
        {
            SimulationState.PlayerChanged -= OnPlayerChanged;
        }
    }
}