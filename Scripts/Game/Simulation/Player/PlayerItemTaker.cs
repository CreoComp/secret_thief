using Assets.Scripts.Game.Simulation.ItemToSteal;
using System;
using UnityEngine;

namespace Assets.Scripts.Game.Simulation.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerItemTaker : MonoBehaviour
    {
        public event Action<Item> ItemPickingStarted;
        public event Action<Item> ItemHadPickedUp;
        public event Action PickUpEnd;

        [SerializeField] private float _distanceToInterruptPicking;
        private double _pickUpStartTime;
        private Item _pickingItem;

        private void OnCollisionEnter(Collision other)
        {


            if (other.gameObject.GetComponent<Item>() == null)
            {
                return;
            }
            _pickUpStartTime = Time.timeSinceLevelLoadAsDouble;
            _pickingItem = other.gameObject.GetComponent<Item>();
            ItemPickingStarted?.Invoke(_pickingItem);

        }

        private void Update()
        {
            if (_pickingItem == null)
                return;

            if (_pickUpStartTime + _pickingItem.TimeToPickSeconds <= Time.timeSinceLevelLoadAsDouble)
            {
                ItemHadPickedUp?.Invoke(_pickingItem);
                EndPicking();
                Debug.Log("Item Picked");
                return;
            }

            if (Vector3.Distance(transform.position, _pickingItem.transform.position) >= _distanceToInterruptPicking)
            {
                EndPicking();
                return;
            }
        }

        private void EndPicking()
        {
            _pickingItem = null;
            _pickUpStartTime = double.PositiveInfinity;
            PickUpEnd?.Invoke();
        }
    }
}
