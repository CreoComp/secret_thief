using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.UI.PlayerInventory
{
    [RequireComponent(typeof(Image))]
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Sprite _emptySlotSprite;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetItem(Sprite sprite)
        {
            if (sprite == null)
                _image.sprite = _emptySlotSprite;
            else
                _image.sprite = sprite;
        }
    }
}