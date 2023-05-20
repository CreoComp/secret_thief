using UnityEngine;

namespace Game.UI.ItemTaking
{
    public class ItemPickingPointerFollow : MonoBehaviour
    {
        [SerializeField] private Camera _playerCamera;
        
        private RectTransform _pointer;
        private Transform _target;

        private void Awake()
        {
            TryGetComponent(out _pointer);
        }

        public void FollowByTarget(Transform target)
        {
            _target = target;
        }

        private void LateUpdate()
        {
            Vector3 realPosition = _playerCamera.WorldToScreenPoint(_target.position);

            float offset = _pointer.sizeDelta.x / 2;
            realPosition.x = Mathf.Clamp(realPosition.x, offset, Screen.width - offset);
            realPosition.y = Mathf.Clamp(realPosition.y, offset, Screen.height - offset);

            _pointer.position = realPosition;
        }
    }
}