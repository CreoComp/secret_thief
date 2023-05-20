using System;
using Assets.Scripts.Game.Simulation.ItemToSteal;
using UnityEngine;


namespace Assets.Scripts.Game.Simulation.Player
{

    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerItemTaker))]
    public class PlayerMovement : MonoBehaviour
    {
        public event Action<GameObject> PlayerHasReachedTrigger;
        public event Action<PlayerMovement> PlayerMoving;

        [SerializeField] private Joystick joystick;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;

        private PlayerItemTaker _taker;
        private CharacterController _characterController;
        public static bool CanMove = true;
        private const float GRAVITY = -9.8f;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _taker = GetComponent<PlayerItemTaker>();
            _taker.ItemPickingStarted += OnStartedPickingItem;
            _taker.PickUpEnd += OnFinishedPickingItem;
        }

        private void Update()
        {
            _characterController.Move(new Vector3(0, GRAVITY * Time.deltaTime, 0));

            if (CanMove && joystick.Direction.magnitude != 0)
            {
                Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
                _characterController.Move(direction * moveSpeed * Time.deltaTime);
                LookToMovementDirection(direction);
                PlayerMoving?.Invoke(this);
            }
        }

        private void LookToMovementDirection(Vector3 direction)
        {
            direction.y = 0;
            direction.z *= -1;
            Vector3 normalizedDirection = direction.normalized;
            float directionAngle = Mathf.Asin(normalizedDirection.z) * Mathf.Rad2Deg + 90;
            if (normalizedDirection.x < 0)
            {
                directionAngle = Mathf.PI - directionAngle;
            }
            float angleToRotate = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.y, directionAngle, rotationSpeed * Time.deltaTime);
            Vector3 EulersToRotate = transform.rotation.eulerAngles;
            EulersToRotate.y = angleToRotate;
            transform.eulerAngles = EulersToRotate;
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerHasReachedTrigger?.Invoke(other.gameObject);
        }

        private void OnStartedPickingItem(Item item)
        {
            CanMove = false;
        }

        private void OnFinishedPickingItem()
        {
            CanMove = true;
        }

        private void OnDestroy()
        {
            _taker.PickUpEnd -= OnFinishedPickingItem;
            _taker.ItemPickingStarted -= OnStartedPickingItem;
        }
    }
}