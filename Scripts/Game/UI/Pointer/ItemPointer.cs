using UnityEngine;

namespace Assets.Scripts.UI.Pointer
{
    public class ItemPointer : MonoBehaviour
    {
        private void Start()
        {
            PointerManager.Instance.AddToList(this);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            PointerManager.Instance.RemoveFromList(this);
        }
    }
}