using Assets.Scripts.Game.Simulation;
using Assets.Scripts.UI.Menu;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Pointer
{
    public class PointerManager : MonoBehaviour
    {

        [SerializeField] private PointerIcon _pointerPrefab;
        private Dictionary<ItemPointer, PointerIcon> _dictionary = new Dictionary<ItemPointer, PointerIcon>();
        [SerializeField] private Transform _playerTransform;
        private Camera _camera;

        public static PointerManager Instance;

        private ItemPointer enemyPointer;
        private PointerIcon pointerIcon;
        private Vector3 toEnemy;
        private Ray ray;
        private float rayMinDistance;
        private int index;
        private Vector3 worldPosition;
        private Quaternion rotation;
        private Vector3 position;
        private Plane[] planes;
        private MenuController menuController;

        private void Awake()
        {
            _camera = Camera.main;
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
        private void Start()
        {
            menuController = FindObjectOfType<MenuController>();

        }
        public void AddToList(ItemPointer itemPointer)
        {
            PointerIcon newPointer = Instantiate(_pointerPrefab, transform);
            _dictionary.Add(itemPointer, newPointer);
        }

        public void RemoveFromList(ItemPointer itemPointer)
        {
            _dictionary.Remove(itemPointer);
        }

        void LateUpdate()
        {
            if (SimulationState.Instance.CurrentSimulationState == SimulationStates.Started)
                CalculateArrowPoint();
        }

        private void CalculateArrowPoint()
        {
            // Left, Right, Down, Up
            planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            foreach (var kvp in _dictionary)
            {

                enemyPointer = kvp.Key;
                pointerIcon = kvp.Value;

                toEnemy = enemyPointer.transform.position - _playerTransform.position;
                ray = new Ray(_playerTransform.position, toEnemy);


                GetMinDistance();

                rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toEnemy.magnitude);

                rotation = GetRotation();

                if (toEnemy.magnitude > rayMinDistance)
                {
                    pointerIcon.Show();
                }
                else
                {
                    pointerIcon.Hide();
                }

                pointerIcon.SetIconPosition(position, rotation);
            }

        }

        private Quaternion GetRotation()
        {
            worldPosition = ray.GetPoint(rayMinDistance);
            position = _camera.WorldToScreenPoint(worldPosition);
            return GetIconRotation(index);
        }
        private void GetMinDistance()
        {
            rayMinDistance = Mathf.Infinity;
            index = 0;

            for (int p = 0; p < 4; p++)
            {
                if (planes[p].Raycast(ray, out float distance))
                {
                    if (distance < rayMinDistance)
                    {
                        rayMinDistance = distance;
                        index = p;
                    }
                }
            }
        }

        Quaternion GetIconRotation(int planeIndex)
        {
            switch (planeIndex)
            {
                case 0:
                    return Quaternion.Euler(0f, 0f, 90f);

                case 1:
                    return Quaternion.Euler(0f, 0f, -90f);

                case 2:
                    return Quaternion.Euler(0f, 0f, 180);

                case 3:
                    return Quaternion.Euler(0f, 0f, 0f);
            }

            return Quaternion.identity;
        }
    }
}