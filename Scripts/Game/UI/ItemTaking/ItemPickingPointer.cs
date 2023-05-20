using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.ItemTaking
{
    [RequireComponent(typeof(ItemPickingPointerFollow))]
    public class ItemPickingPointer : MonoBehaviour
    {
        public const int MAX_FILL_VALUE = 1;
        public const int MIN_FILL_VALUE = 0;

        [SerializeField] private ItemPickingPointerFollow _itemPickingPointerFollow;
        [SerializeField] private Image _fillCirclePointer;

        private void Start()
        {
            _itemPickingPointerFollow = GetComponent<ItemPickingPointerFollow>();
        }
        public void EnablePointer()
        {
            gameObject.SetActive(true);
        }
        
        public void DisablePointer()
        {
            gameObject.SetActive(false);
        }

        public void SetFillAmount(float newAmount) =>
            _fillCirclePointer.fillAmount = Mathf.Clamp(newAmount, MIN_FILL_VALUE, MAX_FILL_VALUE);

        public void ResetFillAmount() => _fillCirclePointer.fillAmount = MIN_FILL_VALUE;

        public bool IsFilled() => _fillCirclePointer.fillAmount >= MAX_FILL_VALUE;

        public void FollowByObject(Transform target) => _itemPickingPointerFollow.FollowByTarget(target);
    }
}